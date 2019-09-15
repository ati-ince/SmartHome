// @ ATI'nin malidir, hacilamak yasaktir .......
// atahir.ince@gmail.com
// Yazilim Smart Home Energy Management (SHEM) sistemi olmasi amaci ile gelistirilmistir. 
// Yazilimda Xtender, 4Noks, Virtual Serial Port kullanilmaktadir. 
// Bu birimler ile haberlesildiktan sonra verilerin database ye eklenmesi amaci ile SQL baglantisi kurulacaktir. 
// Daha sonra baska haberlesme birimleride SHEM yazilimina eklenerek adapte edilecektir.
//////// Sirada eklenecek birimler sunlardir:
// SQL DataBase 
// Ammonit Hava istasyonu
// .... ve devaminda kullanilacak cesitli building otomasyon birimleri eklenecektir.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.Data.OleDb; // Excel den veri cekebilmek amaci ile tanitilmistir. Xtender komutlari ile beraber kullanilmasi amaciyla. 



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private static Form1 instance;

        // Kullanacagimiz Objeleri Class'lardan cekelim, Boylece yazilimi daha kontrol edilebilir hale getirecegiz.
        Xtender XtenderClass = new Xtender();
        RemoteComm RemoteCommClass = new RemoteComm();
        Four4Noks Four4NoksClass = new Four4Noks();

        ///  Kullanilacak tum Serial Port Object'ler tanimlanmistir.
        SerialPort _XtenderSerial = new SerialPort(); // define the Xtender Serial Port Object
        SerialPort _RemoteCommSerial = new SerialPort(); // RemoteComm Virtual 
        SerialPort _4NoksSerial = new SerialPort(); // 4Noks  Modbus Serial RTU

        ///  Xtender icin excel baglantisi yapilmistir. Simdilik sadece D klasorunden cekilse de, daha sonra calisma klasorune eklenecektir. 
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\xtender_write_komutlari.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;'");

        ///  Windows'un tanidigi/gordugu Virtual Serial Port'lari getirmektedir.
        string[] _GetPortNames = System.IO.Ports.SerialPort.GetPortNames(); // Windowsun gordugu Sanal seri Portlarin ismini cekmek amaciyla

        ///  Gelen ve Gonderilen List'ler bulunmaktadir. 
        public static List<byte> Z4NoksComingFramePartially = new List<byte>();// 4Noks modem cevabi parcali olmasi sebebi ile gelen parca veriler listeye eklenmekte
        public List<byte> RemoteSerial4NoksActivePowerFrame = new List<byte>();
        public List<byte> ModBusFrameGlobal = new List<byte>(); // Bu ise 4Noks ile ModBus haberlesmesi amaci ile doldurulan global Frame olmaktadir, illa 4Noks ile kullanilacak diye bir sey soz konusu degil, baska ModBus birimler ile de kullanilabilir ...

        ///  Burada kullanilan Serial Port'lardan gelen-giden datalarin yazilacagi GLOBAL Byte dizileri tanimlanmistir.  
        byte[] RxByte_Xtender;
        byte[] RxByte_RemoteComm;
        byte[] RxByte_4Noks;

        ///  Yazilimda Kullanilan degiskenler
        public static uint Z4NoksComingFrameCounter = 0; // Bu kisimda 4Noks datalari (ornegin 7byte gelen cevap) duzgun sekilde tek bir frame icerisinde gelemeyip parca parca gelme sorununu cozmek icin bu statik degiskeni kullanacagiz...

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public static Form1 Instance
        {
            get
            {
                //Create singleton object if it was not created before
                if (instance == null)
                {
                    instance = new Form1();
                }
                return instance;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // FORM1 OPEN and CLOSE
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
            // Bu kisimda EVENT mantigi ile data alarm'larini kuruyoruz/tanitiyoruz..
            _XtenderSerial.DataReceived += new SerialDataReceivedEventHandler(Xtender_DataReceivedHandler);//bunu eventi gormesi için ekledik
            _RemoteCommSerial.DataReceived += new SerialDataReceivedEventHandler(RemoteComm_DataReceivedHandler);
            _4NoksSerial.DataReceived += new SerialDataReceivedEventHandler(Z4Noks_DataReceivedHandler);
            
        }

        private void Form1_Load(object sender, EventArgs e) // Form Yuklenirken...
        {
            // Seri Haberlesme Kullanan (Xtender, 4Noks, Remote Comm ve digerleri) birimler icin Windows Virtual Porlari cekiyoruz..
            ///////
            foreach (string port in _GetPortNames)//See all exist PortNames
            {
                this.comboBox_ComPorts.Items.Add(port);
            }
            // Get PortNames for RemoteComm
            foreach (string port in _GetPortNames)//See all exist PortNames
            {
                this.comboBox2.Items.Add(port);// RemoteComm combox'u (adini sonra halledecez..)
            }
            // Get PortNames for 4Noks
            foreach (string port in _GetPortNames)//See all exist PortNames
            {
                this.comboBox1.Items.Add(port);// RemoteComm combox'u (adini sonra halledecez..)
            }

            /// aktif pasif kisimlari yazalim
            XtenderPanel.Enabled = false;
            Z4NoksPanel.Enabled = false;
            ////
            XtenderDisconnectButtom.Enabled = false;
            RemoteControlDisconnectButtom.Enabled = false;
            ZigBee4NoksDisconnectButtom.Enabled = false;

            /// //// Excel den veri cekelim, xtender parametreleri
            doldur(); /////
        }

        // FORM CLOSE FN
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            // Form kapanirken acik kapoali tum seri portlari hata olmamasi amaciyla kapatiyoruz...
            if (_XtenderSerial.IsOpen) _XtenderSerial.Close();
            if (_RemoteCommSerial.IsOpen) _RemoteCommSerial.Close();
            if (_4NoksSerial.IsOpen) _4NoksSerial.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Serial Ports CONNECT and DISCONNECT FN
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void XtenderConnectButtom_Click(object sender, EventArgs e) // XtenderConnectButtom
        {
            XtenderClass.XtenderSerialOpen(_XtenderSerial, XtenderPortNameFromForm()); 
            XtenderConnectButtom.Enabled = false;
            XtenderDisconnectButtom.Enabled = true;
            // Xtender Read Yapilabilecek Secenekleri Sunalim
            //for (int i = 0; i < XtenderReadList_string.Length; i++)//Baudrate
            //{
            //    this.comboBox_XtenderRead.Items.Add(XtenderReadList_string[i]);
            //}
            // xtender panel aktif olsun artik
            XtenderPanel.Enabled = true;
        }

        private void ZigBee4NoksConnectButtom_Click(object sender, EventArgs e) //ZigBee4NoksConnectButtom
        {
            Four4NoksClass.Z4NoksSerialOpen(_4NoksSerial ,Four4NoksPortNameFromForm());
            //
            ZigBee4NoksConnectButtom.Enabled = false;
            ZigBee4NoksDisconnectButtom.Enabled = true;

            // 4Noks Read Yapilabilecek Secenekleri Sunalim Oncelikle 
            //for (int i = 0; i < XtenderReadList_string.Length; i++)//Baudrate
            //{
            //    this.comboBox_XtenderRead.Items.Add(XtenderReadList_string[i]);
            //}

            // artik 4Noks test panelini aktif edebiliriz
            Z4NoksPanel.Enabled = true;
        }

        private void RemoteControlConnectButtom_Click(object sender, EventArgs e) // RemoteControlConnectButtom
        {
            RemoteCommClass.RemoteCommSerialOpen(_RemoteCommSerial, RemoteCommPortNameFromForm());
            //
            RemoteControlConnectButtom.Enabled = false;
            RemoteControlDisconnectButtom.Enabled = true;
        }

        private void XtenderDisconnectButtom_Click(object sender, EventArgs e) // XtenderDisconnectButtom
        {
            XtenderClass.XtenderSerialClose(_XtenderSerial, XtenderPortNameFromForm()); 
            XtenderConnectButtom.Enabled = true;
            XtenderDisconnectButtom.Enabled = false;
            // artik xtender test panelini pasif edebiliriz
            XtenderPanel.Enabled = false;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET Port Names FROM FORM1
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string XtenderPortNameFromForm()
        {
            SByte i_ComPorts = Convert.ToSByte(comboBox_ComPorts.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = _GetPortNames[i_ComPorts]; // And choose what user want, and get the Name ... 
            return PortName;
        }

        private string RemoteCommPortNameFromForm()
        {
            SByte i_ComPorts = Convert.ToSByte( comboBox2.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = _GetPortNames[i_ComPorts]; // And choose what user want, and get the Name ... 
            return PortName;
        }

        private string Four4NoksPortNameFromForm()
        {
            SByte i_ComPorts = Convert.ToSByte(comboBox1.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = _GetPortNames[i_ComPorts]; // And choose what user want, and get the Name ... 
            return PortName;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // SOMETHINGS
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //private void button3_Click(object sender, EventArgs e) // XtenderDataWriteButton
        //{
        //    if (!_XtenderSerial.IsOpen) return;//else
        //    //////////////////////////////////////////////
        //}

        private void textBox__XtenderRead_TextChanged(object sender, EventArgs e)
        {
            // Bunu bos fonksiyonlar kismina al ve silme !!!!!!!!!
        }

        private void Xtender_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {   // Event for receiving data
            // Read the buffer to text box.
            //SerialPort sp = (SerialPort)sender;
            int sonuc_xtender;
            RxByte_Xtender = new byte[_XtenderSerial.BytesToRead];
            sonuc_xtender = _XtenderSerial.Read(RxByte_Xtender, 0, _XtenderSerial.BytesToRead);//i'll change string to byte 
            //in this place i have to decide whic data is came....
  
            //
            this.Invoke(new EventHandler(Xtender_DisplayText));
        }

        private void RemoteComm_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {   // Event for receiving data
            // Read the buffer to text box.
            //SerialPort sp = (SerialPort)sender;
            int sonuc_remotecomm;
            RxByte_RemoteComm = new byte[_RemoteCommSerial.BytesToRead];
            sonuc_remotecomm = _RemoteCommSerial.Read(RxByte_RemoteComm, 0, _RemoteCommSerial.BytesToRead);//i'll change string to byte 
            //in this place i have to decide whic data is came....
            this.Invoke(new EventHandler(RemoteComm_DisplayText));
            //
        }
        // 4NOKSCOMING
        private void Z4Noks_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {   // Event for receiving data
             //Read the buffer to text box.
            SerialPort sp = (SerialPort)sender;
            int sonuc_4noks;
            
            RxByte_4Noks = new byte[_4NoksSerial.BytesToRead];
            sonuc_4noks = _4NoksSerial.Read(RxByte_4Noks, 0, _4NoksSerial.BytesToRead);//i'll change string to byte 
            //in this place i have to decide whic data is came....

            if ( (RxByte_4Noks.Length == 7) && (Z4NoksComingFrameCounter==0) )
            {
                // eger tum data dogru gelmis, 7 byte ise o zaman isleyelim
                Z4NoksComingFrameCounter = 0; // burada sifirlayalim....
                Z4NoksComingFramePartially.AddRange(RxByte_4Noks);
                this.Invoke(new EventHandler(Z4Noks_DisplayText));
            }
            else if ( (RxByte_4Noks.Length != 7) )
            {
                Z4NoksComingFrameCounter = Z4NoksComingFrameCounter + (uint)RxByte_4Noks.Length;
                Z4NoksComingFramePartially.AddRange(RxByte_4Noks);

                if (Z4NoksComingFrameCounter >= 7)
                {
                    Z4NoksComingFrameCounter = 0; // burada sifirlayalim....
                    this.Invoke(new EventHandler(Z4Noks_DisplayText));
                }
            }
            
        }

        // 4NoksRespond 
        // Bu kisimda artik Yeni Four4Noks, Xtender vs.. diger fonksiyon ve data isleme kisimlarimizi dis Class lardan cekerek kullanabiliriz !!!!!!!!!
        private void Z4Noks_DisplayText(object sender, EventArgs e)
        {
            double valuee = 0;
            byte[] RemoteComm_4Noks_ActivePowerValue = new byte[] { 0, 0, 0, 0 }; // empty

            ///////////////
            ///////////////      
            if (true)
            {
                RemoteSerial4NoksActivePowerFrame.Clear();
                //
                valuee = System.Math.Pow(16, 2) * Z4NoksComingFramePartially.ToArray()[3] + Z4NoksComingFramePartially.ToArray()[4];
                textBox1.Text = valuee.ToString();
                textBox3.Text = Z4NoksComingFramePartially.Count.ToString();
                //
                Z4NoksComingFramePartially.Clear();// yeni seyler icin temizlik 
                //

                // seri porttan basalim cevabi...
                for (uint say=0;say<7;say++ )
                         RemoteSerial4NoksActivePowerFrame.Add(RxByte_RemoteComm[say]); // 7ye kadar ekleyelim daha sonra data olacak...
                 
                ////// data 4byte yapacaz doubleyi

                ///VALUE-DATA (4byte)
                /// double datayi float yapalim 4 byte olmasi icin, yoksa 8 byte oluyor...
                float ActivePower = (float)valuee;
                textBox3.Text = ActivePower.ToString();
                for (uint i = 0; i < (BitConverter.GetBytes(ActivePower).Length); i++)
                {
                    RemoteSerial4NoksActivePowerFrame.Add(BitConverter.GetBytes(ActivePower)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
                }
                // galiba hallettik
                _RemoteCommSerial.Write(RemoteSerial4NoksActivePowerFrame.ToArray(), 0, RemoteSerial4NoksActivePowerFrame.Count);// her sey tamam sa....
            }
            
        }

        // REMOTEDISPLAYTEXT
        private void RemoteComm_DisplayText(object sender, EventArgs e)
        {
            float VALUE = 0;
            byte[] RemoteComm_Coming_ObjectID_Frame= new byte[] {0,0,0,0}; // empty
            UInt32 ObjID;
            ///////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////
            if (RxByte_RemoteComm[1] == 1)// Bu RemoteComm sanal portundan 1 ile bir frame gelirse bunun xtender icin oldugunu bilecez
            {
                if (RxByte_RemoteComm[0] == 1)// 1 ile read olsun
                {
                    if (RxByte_RemoteComm.Length == 7) // read 7byte gelmesi gerekiyor, check ediyoz
                    {
                        // Burada alinan datalari uyarlamak gerekiyor... READ icin gelen data su (7 byte olacak)
                        // 01(read), 01(xtend), 01(ntw master), 4byte frame (objID)    Oncelikle ObjectID formatini duzenleyelim
                        for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)
                        {
                            RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                        }
                        ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 3000 i yazdirabildik sorguya...
                        //
                       
                        //
                        //// xtender sorgusu yapalim... 
                        uint[] retrn = new uint[3];// geri donusu yazdiralim
                        //
                        retrn = XtenderClass.xtender_excel_sorgulama(dataGridView1, ((uint)1), ((UInt32)ObjID));// write 2 olacak, sorgu edilecek 3 uint donuyor
                        // float ve bool ıkısını decimal deneyelımç Sonra da dizi ile yapariz...
                        // sorgudan gecerse yazdiracaz...
                        if (retrn[0] == 1) // read cevabi mi?
                        {
                            
                            if (retrn[1] == 1) // dogru mu? buldu mu sorgu da?
                            {
                                // her sey tamam sa.... (Burada kontrol ettik dogru data gonderiyoruz karsi tarafa, gonderilen xtender read frame kontrol edildi...) 
                                _XtenderSerial.Write(XtenderClass.XtenderReadFrame((UInt16)ObjID).ToArray(), 0, XtenderClass.XtenderReadFrame((UInt16)ObjID).Count);
                            
                            }
                        }
                        

                    }
                }
                //
                if (RxByte_RemoteComm[0] == 2)// write 2 ile basliyor gelirken
                {
                    if (RxByte_RemoteComm.Length == 11) // write 11byte gelmesi gerekiyor, check ediyoz 
                    {
                        // Burada alinan datalari uyarlamak gerekiyor... READ icin gelen data su (7 byte olacak)
                        // 02(read), 01(xtend), 01(ntw master), 4byte frame (objID), 4byte frame (VALUE)   ObjID icin read kisminda halletmistik simdi ise VALUE de eklenecek
                        // ObjectID 4byte arraydan unit32 yapilacak
                        for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++) // write de de read ile ayni seklide ObjectID alabiliyoz
                        {
                            RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                        }
                        ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 3000 i yazdirabildik sorguya...            
                        // VALUE 4byte arraydan float yapilacak
                        for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++) // write de de read ile ayni seklide ObjectID alabiliyoz
                        {
                            RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 7];// 3. byteden basliyor
                        }
                        VALUE = BitConverter.ToSingle(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 3000 i yazdirabildik sorguya...            
                        //// xtender sorgusu yapalim... 
                        uint[] retrn = new uint[3];// geri donusu yazdiralim
                        retrn=XtenderClass.xtender_excel_sorgulama(dataGridView1, ((uint)2), ((UInt32)ObjID)); // write 2 olacak, sorgu edilecek 3 uint donuyor
 
                        // float ve bool ıkısını decimal deneyelımç Sonra da dizi ile yapariz...
                        // sorgudan gecerse yazdiracaz...
                        if (retrn[0]==2) // yazdirma cevabi mi?
                        {
                          if (retrn[1]==1) // dogru mu? buldu mu sorgu da?
                          {
                              // her sey tamam sa....
                              _XtenderSerial.Write(XtenderClass.XtenderWriteFrame(((UInt16)ObjID), VALUE, retrn[2]).ToArray(), 0, XtenderClass.XtenderWriteFrame(((UInt16)ObjID), VALUE, retrn[2]).Count);
                          }
                        }
                        
                        
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////
            // Burada 4Noks Read su sekilde olmakta
            // [01 (read)] [02 (4NoksFamily)] [XX (4Noks Address)] [xx (Command-ObjectID)] -> Command digerlerinde oldugu gibi 4Byte yer kaplasin ki ayni fn lari kullanabileleim!!!
            // 
            if (RxByte_RemoteComm[1] == 2)// Bu RemoteComm sanal portundan 2 ile bir frame gelirse bunun 4Noks icin oldugunu bilecez
            {
                if (RxByte_RemoteComm.Length == 7 || RxByte_RemoteComm.Length == 8)// write 8byte gelmesi, read icin 7 olmasi lazim gerekiyor, 
                {
                    // READ READ READ ...... yanlis yere yazma, evet biliyorum oprtalik karisik su anda !!!!!!!!!!!!!!!!!!!!
                    if (RxByte_RemoteComm[0] == 1)// check ediyoz 1 ile READ olsun  
                    {
                        if (RxByte_RemoteComm.Length == 7)// Burada alinan datalari uyarlamak gerekiyor... read icin gelen data su (7 byte olacak)
                        {
                           
                            ///////////////////////////////////////////////////////////////
                            // textBox3.Text = RxByte_RemoteComm.Length.ToString(); // Ok check ettik 7 data ulasiyor en azindan bunu basalim simdi porta 
                            //// 01(write), 02(4Noks), xx(adress), 4byte frame (objID)    Oncelikle ObjectID formatini duzenleyelim
                            ///
                            for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)// 4byte dolduruyoruz...
                            {
                                RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                            }
                            ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 1,3, 5 vs... yazdirabildik sorguya...
                            //
                            
                            // Burada oncelikle 4Noks FrameCreate olusturacaz
                            ModBusFrameGlobal.Clear(); // clear this MF


                            bool checksum = false;
                            ///////////////////////////////////////////////
                            UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
                            ///////////////////////////////////////////////////////
                            ModBusFrameGlobal = Four4NoksClass.ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), 0); // we have to use thus frame pointer or adress, read ederken 3. kisma rasgele bir sey yazsak olur... 0 yazdim ama bos olmayacak bir tek
                            // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
                            // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

                            checksum = Four4NoksClass.CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);


                            if (checksum == true)
                            {                                
                                // Bu kisimda data yazdirildiktan sonra bir check sureci olmali 
                                _4NoksSerial.Write(ModBusFrameGlobal.ToArray(), 0, ModBusFrameGlobal.Count); // istenilen yaptirilabiliyor...
                                // check sureci icin mini bekleme
                                //System.Threading.Thread.Sleep(49); // 49ms bekleyelim...
                                // Umariz bu anda bir veri neyin gelmez, daha sonra Artificial Event olusturup onu yaptiririz. Simdilik bole olsun
                            }
                        }
                    
                    }

                    else if (RxByte_RemoteComm[0] == 2) // check ediyoz 2 ile write olsun   
                    {
                        if (RxByte_RemoteComm.Length == 8)// Burada alinan datalari uyarlamak gerekiyor... WRITE icin gelen data su (8 byte olacak)
                        {

                            //// 02(write), 02(4Noks), xx(adress), Command, ObjID, 4byte frame (objID)    Oncelikle ObjectID formatini duzenleyelim
                            ///
                            for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)// 4byte dolduruyoruz...
                            {
                                RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                            }
                            ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 1,3, 5 vs... yazdirabildik sorguya...
                            //

                            // Burada oncelikle 4Noks FrameCreate olusturacaz
                            ModBusFrameGlobal.Clear(); // clear this MF


                            bool checksum = false;
                            ///////////////////////////////////////////////
                            UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
                            ///////////////////////////////////////////////////////
                            ModBusFrameGlobal = Four4NoksClass.ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), ((UInt16)RxByte_RemoteComm[7])); // we have to use thus frame pointer or adress
                            // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
                            // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

                            
                            checksum = Four4NoksClass.CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);

                            if (checksum == true)
                            {
                                
                                // Bu kisimda data yazdirildiktan sonra bir check sureci olmali 
                                _4NoksSerial.Write(ModBusFrameGlobal.ToArray(), 0, ModBusFrameGlobal.Count); // istenilen yaptirilabiliyor...
                                // check sureci icin mini bekleme
                                //System.Threading.Thread.Sleep(49); // 49ms bekleyelim...
                                // Umariz bu anda bir veri neyin gelmez, daha sonra Artificial Event olusturup onu yaptiririz. Simdilik bole olsun
                            }
                            // Yazdirdik diye geri donus verelim kullaniciya...
                            // 02(write cevabi) 02(4noks family) 2D (adres, 45 mesela=2D) 05 00 00 00(yazdirilan komut, 4byte) 01 (1 ise open yapildi 2 ise close)
                            List<byte> RemoteCommWriteBuff = new List<byte>(); RemoteCommWriteBuff.Clear();// temisleyelim
                            RemoteCommWriteBuff.Clear(); // temisleyelim
                            RemoteCommWriteBuff.Add(0x02);// write cevabi
                            RemoteCommWriteBuff.Add(0x02);// 4Noks ailesi
                            RemoteCommWriteBuff.AddRange(ModBusFrameGlobal);// geri kalanini ekleyelim framenin
                            // Burasi guzel oldu. Yazdirilan modbus frame basina 0x02 ile write cevabi ve 0x02 ile de 4noksun cevabi diye yazdirdik...
                            // Kullanici boylece check edebilecek...
                            _RemoteCommSerial.Write(RemoteCommWriteBuff.ToArray(), 0, RemoteCommWriteBuff.Count);// her sey tamam sa....
                            ////////////////////////////////////////////////////////////////////////////////////////////

                        }
                    }        
                                
                       
                    
                }
            }

            
        }
        // XTENDERDISPLAYTEXT
        private void Xtender_DisplayText(object sender, EventArgs e)
        {
            float ReadValue_xtender=0;
            List<byte> RemoteCommWriteBuff = new List<byte>(); RemoteCommWriteBuff.Clear();// temisleyelim
            byte[] write_buff = new byte[] { 0, 0, 0, 0 };
            UInt32 object_wrt;
            float Write_roger; Write_roger = 2/((float)Math.Pow(10,-38));

            // remote gonderip check edelim .... FRAME DOGRU MU 
            // EVET KONTROL EDILDI KARSI TARAF XTENDER DAN gelen frame dogru bize ulasmakta !!!!!!!!!!!!!!!!!!!!
           // _RemoteCommSerial.Write(RxByte_Xtender, 0, RxByte_Xtender.Length); // xtender read response frameee
            //
            if (RxByte_Xtender.Length >= 28) // bool icin 28, float icin 30 byte read valiue cavap framesi alacaz
            {
                // su anda sadece obj id ile geleni karsilastirip test amacli yapiyoruz ANCAK
                // normalde yazilimda gelen data bagimsiz sekilde kontrol edilmeli (gecikme vs hatalari ile karsilasmamak amaciyla)
                // Ilk basta write yada read cevabi oldugu bilinmedigine gore gelen frame uzunluguna bakilacak...
                // devaminda 24-26 ise Write cevabi; 28-30 ise READ cevabi oldugu anlasilacak. O bilgiye gore isleme sokulacak
                // Alinan frame checksumlari (2tane) kontrol edilecek hatali ise atilacak, dogru ise devam edilecek
                // write yada read cevabi oldugu belirtilerek su formatta seri porttan basacaz (BURASI ONEMLI) -> GELEN FRAME SANAL SERI PORTTAN
                // [01, 1or2 , ObjectID, Data]
                // 1 read cevabi, 2 write cevabi oldugunu gosterecek.... DATA ise float yada bool olma durumuna gore cesitli uzunluklarda gelebilir. Ya 4byte ya 1 byte
                
                
                    
                
                    
                    if (RxByte_Xtender.Length == 28)
                    {
                        
                        textBox__XtenderRead.Text = RxByte_Xtender[24].ToString();
                        // 18 ObjID ve 24 ise value BOOL degeri                      
                        RemoteCommWriteBuff = RemoteCommClass.RemoteCommWriteFrame(1, 1, 1, ((ushort)RxByte_Xtender[18]), ((ushort)RxByte_Xtender[24]));     
                        
                    }
                    else // 30 byte float ise yani
                    {
                       
                        ReadValue_xtender = BitConverter.ToSingle(RxByte_Xtender, 24); // alinan 4byte data float veri tipine donusturulmekte
                        textBox__XtenderRead.Text = ReadValue_xtender.ToString();// ekranda da gosteriyoruz
                        byte[] buffX = { 0, 0, 0, 0 };
                        for (uint x=0;x<4;x++)
                        {
                            buffX[x] = RxByte_Xtender[x + 18];
                        }                           
                        // RemoteComm a da yazdiralim 9600 ile                        
                        RemoteCommWriteBuff = RemoteCommClass.RemoteCommWriteFrame(1, 1, 1, (ushort)BitConverter.ToUInt32(buffX, 0), ReadValue_xtender);
                        
                    }
                    
                
            }
            else if (RxByte_Xtender.Length == 26) // burasi da writing cevabi Xtender'dan gelen
            {
                for (uint i = 0; i < write_buff.Length; i++)
                {
                    write_buff[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                }
                object_wrt = BitConverter.ToUInt32(write_buff, 0);// galiba ObjectID mesela 3000 i yazdirabildik sorguya...
                //
                RemoteCommWriteBuff = RemoteCommClass.RemoteCommWriteFrame(2, 1, 1, object_wrt, Write_roger);// 1 basarili manasinda
            }
            else
                textBox__XtenderRead.Text = "Xtender Read/Write Failed!";

            // son olarak ta gelen datayi RemoteComm dan basalim State=1 ise read, 1 ise xtender vs...
            //NOT= Tum bunlari UNION gibi bir hale sokup okunakli ve yazmasi kolay hale sokalim!!!!!!!!!!!!!
            // List<byte> RemoteCommWriteFrame(UInt16 State, UInt16 FamilyName, UInt16 FamilyMemberID, UInt16 ObjectID, float VALUE)
            // Yukarida hazirladigimiz frame'i yazdiralim !!!!!!
            if (_RemoteCommSerial.IsOpen)
            {
                _RemoteCommSerial.Write(RemoteCommWriteBuff.ToArray(), 0, RemoteCommWriteBuff.Count);

            }
           
        }

        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e) // SoftStartButtom
        {

        }

        private void RemoteControlDisconnectButtom_Click(object sender, EventArgs e) // RemoteControlDisconnectButtom
        {
            RemoteCommClass.RemoteCommSerialClose(_RemoteCommSerial, RemoteCommPortNameFromForm());
            //
            RemoteControlConnectButtom.Enabled = true;
            RemoteControlDisconnectButtom.Enabled = false;
            // Burada panellerin aktif-pasif durumlarina karismiyoruz !!!!!
        }


        private void ZigBee4NoksDisconnectButtom_Click(object sender, EventArgs e) // ZigBee4NoksDisconnectButtom
        {
            Four4NoksClass.Z4NoksSerialClose(_4NoksSerial, Four4NoksPortNameFromForm());
            //
            ZigBee4NoksDisconnectButtom.Enabled = false;
            ZigBee4NoksConnectButtom.Enabled = true;
            // artik 4noks test panelini pasif edebiliriz
            Z4NoksPanel.Enabled = false;
            //////////////////////////////////////
        }

        public void doldur() // Burada yazilan excell form'a dosenmekte ve sorgularda kullanilmaktadir. Daha sonra o kismi gizli yapacagiz ki yanlislikla formati bozulmasin...
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            baglanti.Close();
            ///
            GridNumaralandir(dataGridView1);
        }


        private bool XtenderSorgulama(uint State, UInt32 Command)
        {

            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        public static void GridNumaralandir(DataGridView dataGridView)
        { 
            if (dataGridView != null)
            {
                for (int count = 0; (count <= (dataGridView.Rows.Count - 1)); count++)
                {
                    string sayi = (count + 1).ToString();
                    dataGridView.Rows[count].HeaderCell.Value = sayi;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

  


       
    }



}


