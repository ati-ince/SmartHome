// @ ATI'nin malidir, hacilamak yasaktir .......
// atahir.ince@gmail.com

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
//
using System.Data.OleDb; // excel den veri cekmek amaci ile kullaniyoruz



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private static Form1 instance;
        /////////////////
        SerialPort _XtenderSerial = new SerialPort(); // define the Xtender Serial Port Object
        SerialPort _RemoteCommSerial = new SerialPort(); // RemoteComm Virtual 
        SerialPort _4NoksSerial = new SerialPort(); // 4Noks  Modbus Serial RTU
        //////////////////////////////
        Ammonit AmmonitClass = new Ammonit(); // ammonit class eklendi
        /////////////////////////////
        ////////////////
        /// <summary>
        ///  Xtender icin excel baglantisi
        /// </summary>
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\xtender_write_komutlari.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;'");
        ///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////
        string[] _GetPortNames = System.IO.Ports.SerialPort.GetPortNames(); // Windowsun gordugu Sanal seri Portlarin ismini cekmek amaciyla
        ////////////////

        byte[] RxByte_Xtender; // Seri Porttan alinan byte dizisi icin kullanilacak global dizi
        byte[] RxByte_RemoteComm;
        byte[] RxByte_4Noks;
        /////////////////
        public static bool Z4Noks_Polling=false; // polling yaparken bunun durumu istek gelmesi ile true is bitince de false haline geri getirilecek !!!!!
        public static UInt16 Z4Noks_Polling_StartAdress = 0; // 0' i baslangic adresi olarak simdilik belirleyelim.
        public static UInt16 Z4Noks_Polling_StopAdress = 255;
        ///
        // we add some static datacoming number
        public static uint Z4NoksComingFrameCounter = 0;
        // 4Noks read mi gelmis
        public static bool Z4NoksReadCheck = false;
        // and add some byte array the indis 7
        public static List<byte> Z4NoksComingFramePartially = new List<byte>();
        //
        public List<byte> RemoteSerial4NoksActivePowerFrame = new List<byte>();
        /////////// xtender icin uzunluklar
        const uint Write_A_Column = 20;
        const uint Read_E_Column = 17;
        ///////////////////////////////////////////////////////////////////
        List<byte> ModBusFrameGlobal = new List<byte>();
        //
        List<byte> ModBusComingFrame= new List<byte>();
        // 
        public static bool Z4NoksActivePower=false;

        public static byte Check_control_value = 0;// if 0, not push any thing
        /// Bunu cesitli check isleri icin kullanabiliriz, xtender read/write basildi vb gibi.... Belki de hic kullanmayabiliriz...
        ///////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Xtender dan reading yaparken ornek olarak kullaniyorum, normal calisirken gerek yok sadece bu test amacli
        /// </summary>
        string[] XtenderReadList_string = new string[] { "3000", "3001", "3005", "3007", "3011", "3012", "3019", "3020", "3021", "3023", "3030" };//int formatinda yazdiiliyor
        UInt16[] XtenderReadList_uint16 = new UInt16[] { 3000, 3001, 3005, 3007, 3011, 3012, 3019 ,3020, 3021, 3023,3030 };//int formatinda yazdiiliyor, secmesi ve kullanmasi kolay olsun diye
        /////////////////////////////////////////////////

        /// <summary>
        /// 4Noks testi yaparken kullanacagimiz dizileri de buraya tanimlayalim, icini dolduracagiz daha sonra
        /// </summary>
        ////////////////////////////////////////
        // Benim dosyam C içinde cagdasdag isminde bir dosyaydı siz bunun yerini değiştirebilirsiniz.
        

        /// </summary>
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

        public Form1()
        {
            InitializeComponent();
            _XtenderSerial.DataReceived += new SerialDataReceivedEventHandler(Xtender_DataReceivedHandler);//bunu eventi gormesi için ekledik
            _RemoteCommSerial.DataReceived += new SerialDataReceivedEventHandler(RemoteComm_DataReceivedHandler);
            _4NoksSerial.DataReceived += new SerialDataReceivedEventHandler(Z4Noks_DataReceivedHandler);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get PortNames for Xtender
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
            SoftStartButtom.Enabled = false;
            SoftStopButtom.Enabled  = false;
            XtenderPanel.Enabled = false;
            Z4NoksPanel.Enabled = false;
            ////
            XtenderDisconnectButtom.Enabled = false;
            RemoteControlDisconnectButtom.Enabled = false;
            ZigBee4NoksDisconnectButtom.Enabled = false;
            /// /////////////////////////////////////////////////////
            /// //// Excel den veri cekelim, xtender parametreleri
            doldur(); /////
            // /////////////////////////////////////////////////////

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            if (_XtenderSerial.IsOpen) _XtenderSerial.Close();
            if (_RemoteCommSerial.IsOpen) _RemoteCommSerial.Close();
            if (_4NoksSerial.IsOpen) _4NoksSerial.Close();
        }

        private void comboBox_ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) // XtenderConnectButtom
        {
            XtenderSerialOpen(XtenderPortNameFromForm());// Getand use the Port name 
            XtenderConnectButtom.Enabled = false;
            XtenderDisconnectButtom.Enabled = true;
            // Xtender Read Yapilabilecek Secenekleri Sunalim
            for (int i = 0; i < XtenderReadList_string.Length; i++)//Baudrate
            {
                this.comboBox_XtenderRead.Items.Add(XtenderReadList_string[i]);
            }
            // xtender panel aktif olsun artik
            XtenderPanel.Enabled = true;
            //////////////////////////////////////
        }

        private void ZigBee4NoksConnectButtom_Click(object sender, EventArgs e) //ZigBee4NoksConnectButtom
        {
            Z4NoksSerialOpen(Z4NoksPortNameFromForm());
            //
            ZigBee4NoksConnectButtom.Enabled = false;
            ZigBee4NoksDisconnectButtom.Enabled = true;

            // 4Noks Read Yapilabilecek Secenekleri Sunalim Oncelikle 
            for (int i = 0; i < XtenderReadList_string.Length; i++)//Baudrate
            {
                this.comboBox_XtenderRead.Items.Add(XtenderReadList_string[i]);
            }

            // artik 4Noks test panelini aktif edebiliriz
            Z4NoksPanel.Enabled = true;
            //////////////////////////////////////
        }
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

        private string Z4NoksPortNameFromForm()
        {
            SByte i_ComPorts = Convert.ToSByte(comboBox1.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = _GetPortNames[i_ComPorts]; // And choose what user want, and get the Name ... 
            return PortName;
        }

        private UInt16 XtenderReadFromForm()
        {
            SByte i_Xread = Convert.ToSByte(comboBox_XtenderRead.SelectedIndex.ToString());
            if (i_Xread == (-1)) { i_Xread++; }
            UInt16 XreadNumber = XtenderReadList_uint16[i_Xread]; // And choose what user want, and get the Name ... 
            return XreadNumber;
        }

        private void button2_Click(object sender, EventArgs e) // XtenderDisconnectButtom
        {
            XtenderSerialClose(XtenderPortNameFromForm());
            XtenderConnectButtom.Enabled = true;
            XtenderDisconnectButtom.Enabled = false;
            // artik xtender test panelini pasif edebiliriz
            XtenderPanel.Enabled = false;
            //////////////////////////////////////
        }

        private void button4_Click(object sender, EventArgs e) // XtenderDataReadButton
        {
            if (!_XtenderSerial.IsOpen) return;//else
            ////////////////////////////////////////////////
            
            _XtenderSerial.Write(XtenderReadFrame(XtenderReadFromForm()).ToArray(), 0, XtenderReadFrame(XtenderReadFromForm()).Count);

            // read komutu 1 olsun
            Check_control_value = 1;


        }

        // burada RemoteComm sanal seri portuna basilacak degisken uzunluklu veriyi daha dogrusu diziyi hazirliyoz
        // State= 1 ise read icin 2 ise write icin donen cevap oldugu anlasilacak 
        // FamilyName= Xtender, ZigBee4Noks vb... 1 gibi gidiyor
        // Bir family'nin birden fazla uyesi varsa 4noks gibi kullanacaz. Birada 4noks modem ID'si 1 ayni Xtender'inkinin de 1 oldugu gibi
        // ObjectID ise okunan datanin kodu.. Bu akim degeri mi, role durumu mu vs... hepsine bir kod verecez. mesela Xtender icin 3000 batarya voltaji bunu kullanacaz. Ancak 4Noks role mesela 5 olsun kodu gibi....
        // VALUE ise bize gelen formatinin ne oldugu onemsiz veri... Ona gore RemoteComm frame'ini hazirlayacaz
        private List<byte> RemoteCommWriteFrame(UInt16 State, UInt16 FamilyName, UInt16 FamilyMemberID, UInt32 ObjectID, float VALUE) 
        {
            // Burada oncelikle read data framesi (30byte) ayarlayip gonderecegiz
            Dictionary<uint, List<byte>> XtenderCreateReadFrame = new Dictionary<uint, List<byte>>();
            List<byte> command_RemoteCommWriteFrame = new List<byte>();
            //
            command_RemoteCommWriteFrame.Clear(); // temizleyelim once
            ////////

            ///Start first byte, STATE (1byte)
            command_RemoteCommWriteFrame.Add((byte) State);

            ///FamilyName (1byte)
            command_RemoteCommWriteFrame.Add((byte)FamilyName);

            ///FamilyMemberID (1byte)
            command_RemoteCommWriteFrame.Add((byte)FamilyMemberID);

            ///ObjectID (4byte)
            for (uint i = 0; i < (BitConverter.GetBytes(ObjectID).Length); i++)
            {
                command_RemoteCommWriteFrame.Add(BitConverter.GetBytes(ObjectID)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
            }

            ///VALUE-DATA (4byte)
            for (uint i = 0; i < (BitConverter.GetBytes(VALUE).Length); i++)
            {
                command_RemoteCommWriteFrame.Add(BitConverter.GetBytes(VALUE)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
            }

            return command_RemoteCommWriteFrame;
        }

        private List<byte> XtenderReadFrame(UInt16 XreadRegAddr)
        {
            // Burada oncelikle read data framesi (30byte) ayarlayip gonderecegiz
            Dictionary<uint, List<byte>> XtenderCreateReadFrame = new Dictionary<uint, List<byte>>();
            List<byte> command_XtenderReadFrame = new List<byte>();

            command_XtenderReadFrame.Clear();// empty and clean frame for fill up
            XtenderCreateReadFrame.Clear();// same as well  

            ///Start byte (always 0xAA according to manual)
            command_XtenderReadFrame.Add(0xAA);

            ///Frame flag (always 0x00 according to manual)
            command_XtenderReadFrame.Add(0x00);

            ///Source address (always 0x01 according to manual), 4 bytes
            command_XtenderReadFrame.Add(0x01);
            command_XtenderReadFrame.Add(0x00);
            command_XtenderReadFrame.Add(0x00);
            command_XtenderReadFrame.Add(0x00);

            ///Destionation address (0x65 = 101 for XTH devices (RCC 3)), 4 bytes
            command_XtenderReadFrame.Add(0x65);
            command_XtenderReadFrame.Add(0x00);
            command_XtenderReadFrame.Add(0x00);
            command_XtenderReadFrame.Add(0x00);

            ///Data length (10 always), 2 bytes
            command_XtenderReadFrame.Add(0x0A);
            command_XtenderReadFrame.Add(0x00);

            ///Calculate and add checksum
            calculateChecksum(ref command_XtenderReadFrame, 1, 11);

            // flags
            command_XtenderReadFrame.Add(0x00);

            // service id
            command_XtenderReadFrame.Add(0x01);

            // object type
            command_XtenderReadFrame.Add(0x01);
            command_XtenderReadFrame.Add(0x00);

            // register degerini ekle
            command_XtenderReadFrame.Add((byte)(XreadRegAddr & 0xFF));
            command_XtenderReadFrame.Add((byte)(XreadRegAddr >> 8 & 0xFF));
            command_XtenderReadFrame.Add(0x00);
            command_XtenderReadFrame.Add(0x00);

            ///Property ID (The last), 2 bytes
            command_XtenderReadFrame.Add(0x01);
            command_XtenderReadFrame.Add(0x00);

            calculateChecksum(ref command_XtenderReadFrame, 14, 10);

            return command_XtenderReadFrame;
             
        }

        private List<byte> XtenderWriteFrame(UInt16 XreadRegAddr, float VALUE, uint DataType)
        {
            // NOT: 1 float, 0 bool ve INT32 ise 2 , simdilik INT32 yok...
            // Burada oncelikle read data framesi (30byte) ayarlayip gonderecegiz
            Dictionary<uint, List<byte>> XtenderCreateWriteFrame = new Dictionary<uint, List<byte>>();
            List<byte> command_XtenderWriteFrame = new List<byte>();

            command_XtenderWriteFrame.Clear();// empty and clean frame for fill up
            XtenderCreateWriteFrame.Clear();// same as well  

            // float ile bool ayıralim (BUNU SONRA DIZIYE DOLDURARAK IS YAPACAZ!!!!!!!!!)
            //
            bool bool_value=true;
            //////////////////////////////////////////////////////
            if (DataType == 0) //grid feed allowed (ROLE ASLINDA)
            {
                bool_value=true;
            }
            else if (DataType == 1) //grid feed max current
            {
                bool_value = false;
            }
            ///Start byte (always 0xAA according to manual)
            command_XtenderWriteFrame.Add(0xAA);

            ///Frame flag (always 0x00 according to manual)
            command_XtenderWriteFrame.Add(0x00);

            ///Source address (always 0x01 according to manual), 4 bytes
            command_XtenderWriteFrame.Add(0x01);
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);
                 

            ///Destionation address (0x65 = 101 for XTH devices (RCC 3)), 4 bytes
            command_XtenderWriteFrame.Add(0x65);
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);

            ///Data length (10 always), 2 bytes
            command_XtenderWriteFrame.Add(0x0E);
            command_XtenderWriteFrame.Add(0x00); // burada fark yok
            
            ///Calculate and add checksum
            calculateChecksum(ref command_XtenderWriteFrame, 1, 11);    
            
            // flags
            command_XtenderWriteFrame.Add(0x00);

            // service id
            command_XtenderWriteFrame.Add(0x02); // write mean 0x02 olmali imis

            // object type
            command_XtenderWriteFrame.Add(0x02); // burasi da 0x02 olmali imis
            command_XtenderWriteFrame.Add(0x00);

            // register degerini ekle
            command_XtenderWriteFrame.Add((byte)(XreadRegAddr & 0xFF));
            command_XtenderWriteFrame.Add((byte)(XreadRegAddr >> 8 & 0xFF));
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);

            ///Property ID (The last), 2 bytes
            command_XtenderWriteFrame.Add(0x05); // write icin 0x05 istenmekte
            command_XtenderWriteFrame.Add(0x00);

            // VALUE degerini ekle
            if (bool_value == false) // FLOAT ve BOOL arasi uzunluk 3byte farkli (float)
            {
                for (uint i = 0; i < (BitConverter.GetBytes(VALUE).Length); i++)
                {
                    command_XtenderWriteFrame.Add(BitConverter.GetBytes(VALUE)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
                }
            }
            else // BOOL
            { 
                if (VALUE==0)
                    command_XtenderWriteFrame.Add(0x00);
                else
                    command_XtenderWriteFrame.Add(0x01);

                // ilk byte 01 yada 00 ile ac kapa yapiyor geri kalan datalar (3byte) sadece 0'dan olusuyor !!!
                command_XtenderWriteFrame.Add(0x00);
                command_XtenderWriteFrame.Add(0x00);
                command_XtenderWriteFrame.Add(0x00);
            }

            // the last checksum
            calculateChecksum(ref command_XtenderWriteFrame, 14, 14);

            return command_XtenderWriteFrame;

        }

        private bool calculateChecksum(ref List<byte> lBytes, int startIndex, int count, bool readOnly = false)
        {
            //// Logging.Log(">>>>>>calculateChecksum-xtender",w);
            uint A = 0xFF;
            uint B = 0x00;


            for (int i = startIndex; i < startIndex + count; i++)
            {
                A = (A + (uint)lBytes[i]) % 0x100;
                B = (B + A) % 0x100;
            }

            byte tempA = (byte)(A & 0xFF);
            byte tempB = (byte)(B & 0xFF);

            if (!readOnly)
            {
                lBytes.Add(tempA);
                lBytes.Add(tempB);
            }
            else
            {
                if (lBytes[lBytes.Count - 2] != A || lBytes[lBytes.Count - 1] != B)
                {
                    //// Logging.Log("<<<<<<calculateChecksum-xtender",w);
                    return false;
                }
            }
            //// Logging.Log("<<<<<<calculateChecksum-xtender",w);
            return true;
        }

        private void XtenderSerialOpen(string PortNames) // This value comes from Form opening time
        {

            _XtenderSerial.PortName = PortNames;
            _XtenderSerial.BaudRate = 38400;//  Xtendar data rate
            _XtenderSerial.DataBits = 8;//int formatinda yazdiriliyo
            _XtenderSerial.StopBits = System.IO.Ports.StopBits.One;
            _XtenderSerial.Parity = System.IO.Ports.Parity.Even;
            _XtenderSerial.ReadTimeout = 3000;
            _XtenderSerial.WriteTimeout = 4000;
            try
            {
                if (_XtenderSerial.IsOpen)
                {
                    _XtenderSerial.Close();
                }
                _XtenderSerial.Open();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    //Logging.Log("Couldn't open Xtender serial port, will try again", w);
                }
            }
        }

        private void RemoteCommSerialOpen(string PortNames) // This value comes from Form opening time
        {

            _RemoteCommSerial.PortName = PortNames;
            _RemoteCommSerial.BaudRate = 9600;//  Xtendar data rate
            _RemoteCommSerial.DataBits = 8;//int formatinda yazdiriliyo
            _RemoteCommSerial.StopBits = System.IO.Ports.StopBits.One;
            _RemoteCommSerial.Parity = System.IO.Ports.Parity.None;
            _RemoteCommSerial.ReadTimeout = 3000;
            _RemoteCommSerial.WriteTimeout = 4000;
            try
            {
                if (_RemoteCommSerial.IsOpen)
                {
                    _RemoteCommSerial.Close();
                }
                _RemoteCommSerial.Open();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    //Logging.Log("Couldn't open Xtender serial port, will try again", w);
                }
            }
        }

        private void Z4NoksSerialOpen(string PortNames) // This value comes from Form opening time
        {        

            _4NoksSerial.PortName = PortNames;
            _4NoksSerial.BaudRate = 9600;//  Xtendar data rate
            _4NoksSerial.DataBits = 8;//int formatinda yazdiriliyo
            _4NoksSerial.StopBits = System.IO.Ports.StopBits.One;
            _4NoksSerial.Parity = System.IO.Ports.Parity.None;
            _4NoksSerial.ReadTimeout = 400;
            _4NoksSerial.WriteTimeout = 50;
            try
            {
                if (_4NoksSerial.IsOpen)
                {
                    _4NoksSerial.Close();
                }
                _4NoksSerial.Open();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    //Logging.Log("Couldn't open Xtender serial port, will try again", w);
                }
            }
        }


        private void XtenderSerialClose(string PortNames) // This value comes from Form opening time
        {
         
            try
            {
                if (!_XtenderSerial.IsOpen)
                {
                    _XtenderSerial.Open();
                }
                _XtenderSerial.Close();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                   // Logging.Log("Couldn't close Xtender serial port, will try again", w);
                }
            }
        }

        private void RemoteCommSerialClose(string PortNames) // This value comes from Form opening time
        {
            
            try
            {
                if (!_RemoteCommSerial.IsOpen)
                {
                    _RemoteCommSerial.Open();
                }
                _RemoteCommSerial.Close();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    // Logging.Log("Couldn't close Xtender serial port, will try again", w);
                }
            }
        }


        private void Z4NoksSerialClose(string PortNames) // This value comes from Form opening time
        {
            
            try
            {
                if (!_4NoksSerial.IsOpen)
                {
                    _4NoksSerial.Open();
                }
                _4NoksSerial.Close();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    // Logging.Log("Couldn't close Xtender serial port, will try again", w);
                }
            }
        }


        public void XtenderWrite(byte[] SendingData)
        {
            _XtenderSerial.Write(SendingData.ToArray(), (int)0, (int)SendingData.Count());

        }

        public void RemoteCommWrite(byte[] SendingData)
        {
            _RemoteCommSerial.Write(SendingData.ToArray(), (int)0, (int)SendingData.Count());

        }

        private void button3_Click(object sender, EventArgs e) // XtenderDataWriteButton
        {
            if (!_XtenderSerial.IsOpen) return;//else
            ////////////////////////////////////////////////

            // Burada yine ComBox dan yazilabilecek registerleri ve yazmak istediguimiz degeri girecez
            // read komutu 1 olsun
            Check_control_value = 2;
        }

        private void textBox__XtenderRead_TextChanged(object sender, EventArgs e)
        {

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
        private void Z4Noks_DisplayText(object sender, EventArgs e)
        {
            double valuee = 0;
            byte[] RemoteComm_4Noks_ActivePowerValue = new byte[] { 0, 0, 0, 0 }; // empty

            ///////////////
            ///////////////      
            if (Z4NoksReadCheck == true)
            {
                RemoteSerial4NoksActivePowerFrame.Clear();
                //
                valuee = System.Math.Pow(16, 2) * Z4NoksComingFramePartially.ToArray()[3] + Z4NoksComingFramePartially.ToArray()[4];
                textBox1.Text = valuee.ToString();
                textBox3.Text = Z4NoksComingFramePartially.Count.ToString();
                //
                Z4NoksComingFramePartially.Clear();// yeni seyler icin temizlik 
                //
                Z4NoksReadCheck = false; // geri temizleyelim...
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
                        retrn = xtender_excel_sorgulama(((uint)1), ((UInt32)ObjID)); // write 2 olacak, sorgu edilecek 3 uint donuyor

                        // float ve bool ıkısını decimal deneyelımç Sonra da dizi ile yapariz...
                        // sorgudan gecerse yazdiracaz...
                        if (retrn[0] == 1) // read cevabi mi?
                        {
                            
                            if (retrn[1] == 1) // dogru mu? buldu mu sorgu da?
                            {
                                // her sey tamam sa.... (Burada kontrol ettik dogru data gonderiyoruz karsi tarafa, gonderilen xtender read frame kontrol edildi...) 
                                _XtenderSerial.Write(XtenderReadFrame((UInt16)ObjID).ToArray(), 0, XtenderReadFrame((UInt16)ObjID).Count);
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
                        retrn=xtender_excel_sorgulama(((uint)2), ((UInt32)ObjID)); // write 2 olacak, sorgu edilecek 3 uint donuyor
 
                        // float ve bool ıkısını decimal deneyelımç Sonra da dizi ile yapariz...
                        // sorgudan gecerse yazdiracaz...
                        if (retrn[0]==2) // yazdirma cevabi mi?
                        {
                          if (retrn[1]==1) // dogru mu? buldu mu sorgu da?
                          {
                              // her sey tamam sa....
                              _XtenderSerial.Write(XtenderWriteFrame(((UInt16)ObjID), VALUE, retrn[2]).ToArray(), 0, XtenderWriteFrame(((UInt16)ObjID), VALUE, retrn[2]).Count);
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
                            Z4NoksReadCheck = true; // 4noks icin read geldi . kaydedek
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
                            ModBusComingFrame.Clear();

                            bool checksum = false;
                            ///////////////////////////////////////////////
                            UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
                            ///////////////////////////////////////////////////////
                            ModBusFrameGlobal = ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), 0); // we have to use thus frame pointer or adress, read ederken 3. kisma rasgele bir sey yazsak olur... 0 yazdim ama bos olmayacak bir tek
                            // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
                            // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

                            checksum = CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);


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
                            ModBusComingFrame.Clear();

                            bool checksum = false;
                            ///////////////////////////////////////////////
                            UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
                            ///////////////////////////////////////////////////////
                            ModBusFrameGlobal = ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), ((UInt16)RxByte_RemoteComm[7])); // we have to use thus frame pointer or adress
                            // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
                            // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

                            checksum = CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);

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

            if (RxByte_RemoteComm[1] == 3)// Bu RemoteComm sanal portundan 3 ile bir frame gelirse bunun Ammonit icin oldugunu bilecez
            {
                if (RxByte_RemoteComm[0] == 1)
                {
                    if (RxByte_RemoteComm.Length == 7)// Burada alinan datalari uyarlamak gerekiyor... READ icin gelen data su (7 byte olacak)
                    {
                        for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)
                        {
                            RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                        }
                        ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 1, 2, 6 vs yi yazdirabildik sorguya...
                        ///
                        //textBox51.Text = ObjID.ToString();
                        // Bu kisimda gelen frame de ObjID ile hangi sorgu yapilacagi ogreniliyor..
                        /////  Bu kisma ise Ammonit sorgu fonksiyonu ekleniyor...
                        AmmonitClass.GetAmmonitData(listBox1, 40500, 16).Count().ToString();
                        float AmmonitData = AmmonitClass.Ammonit_Give((uint)ObjID);
                        //
                        textBox51.Text = AmmonitData.ToString();
                        //////
                        /////
                        List<byte> RemoteCommWriteBuff = new List<byte>(); RemoteCommWriteBuff.Clear();// temisleyelim
                        RemoteCommWriteBuff.Clear(); // temisleyelim
                        RemoteCommWriteBuff.Add(0x01);// read cevabi
                        RemoteCommWriteBuff.Add(0x03);// Ammonit ailesi
                        RemoteCommWriteBuff.Add(0x01);// Ammonit 1. numarali adres
                        // ###
                        // ObjID (4byte)
                        for (uint i = 0; i < (BitConverter.GetBytes(ObjID).Length); i++)
                        {
                            RemoteCommWriteBuff.Add(BitConverter.GetBytes(ObjID)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
                        }
                        //
                        
                        ///VALUE-DATA (4byte)
                        for (uint i = 0; i < (BitConverter.GetBytes(AmmonitData).Length); i++)
                        {
                            RemoteCommWriteBuff.Add(BitConverter.GetBytes(AmmonitData)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
                        }
                        ////
                        _RemoteCommSerial.Write(RemoteCommWriteBuff.ToArray(), 0, RemoteCommWriteBuff.Count);// her sey tamam sa....
                        ////////////////////////////////////////////////////////////////////////////////////////////
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
                        RemoteCommWriteBuff = RemoteCommWriteFrame(1, 1, 1, ((ushort)RxByte_Xtender[18]), ((ushort)RxByte_Xtender[24]));     
                        
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
                        RemoteCommWriteBuff = RemoteCommWriteFrame(1, 1, 1, (ushort)BitConverter.ToUInt32(buffX, 0), ReadValue_xtender);
                        
                    }
                    
                
            }
            else if (XtenderReadFrame(XtenderReadFromForm()).Count == 26) // burasi da writing cevabi Xtender'dan gelen
            {
                for (uint i = 0; i < write_buff.Length; i++)
                {
                    write_buff[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
                }
                object_wrt = BitConverter.ToUInt32(write_buff, 0);// galiba ObjectID mesela 3000 i yazdirabildik sorguya...
                //
                RemoteCommWriteBuff = RemoteCommWriteFrame(2, 1, 1, object_wrt, Write_roger);// 1 basarili manasinda
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

        private void comboBox_XtenderRead_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public TextWriter w { get; set; }  // ?????? Bunu ne diye yazmistim acaba ???????

        private void RemoteControlConnectButtom_Click(object sender, EventArgs e) // RemoteControlConnectButtom
        {
            RemoteCommSerialOpen(RemoteCommPortNameFromForm());
            //
            RemoteControlConnectButtom.Enabled = false;
            RemoteControlDisconnectButtom.Enabled = true;
        }

        private void RemoteControlDisconnectButtom_Click(object sender, EventArgs e) // RemoteControlDisconnectButtom
        {
            RemoteCommSerialClose(RemoteCommPortNameFromForm());
            //
            RemoteControlConnectButtom.Enabled = true;
            RemoteControlDisconnectButtom.Enabled = false;
            // Burada panellerin aktif-pasif durumlarina karismiyoruz !!!!!
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Bu kisma Hangi Plug ve Sensorler Networke bagli ise onlari sorgulayip ComBox' a yazdirma isini yaptiracaz
            if (!_4NoksSerial.IsOpen) return;//sorgulamasin baglanmaya calisalim ....
            /////////////////////////////////////////////////////////////////////////
            Z4Noks_Polling = true; // Tusa basildigimi boylece anlamis oluyoruz...
            Z4Noks_Polling_StartAdress = 45; // Baslangic adresi simdilik 0 olsun sonra bir yerden cekeriz.
            Z4Noks_Polling_StopAdress = 255; // Daha sonra otomatik cekecez bunu da
            //////////////////////////////////////////////////////////////////////////
            ModBusFrameGlobal.Clear(); // clear this MF
            ModBusComingFrame.Clear();

            bool checksum = false;
            //
            ModBusFrameGlobal = ModbusPollingF((UInt16)Z4Noks_Polling_StartAdress, 5, 2); // we have to use thus frame pointer or adress

            checksum = CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);

            if (checksum == true)
                if (ModBusFrameGlobal.Count == 8)
                    _4NoksSerial.Write(ModBusFrameGlobal.ToArray(), 0, ModBusFrameGlobal.Count);

            // burada polling bitene kadar polling tusunu pasif kilalim ...... 
            //button1.Enabled = false; // bu tus polling bitene kadar pasif kalsin.... Ve diger 4Noks tuslari da elbet !!
        }



        private void ZigBee4NoksDisconnectButtom_Click(object sender, EventArgs e) // ZigBee4NoksDisconnectButtom
        {
            Z4NoksSerialClose(Z4NoksPortNameFromForm());
            //
            ZigBee4NoksDisconnectButtom.Enabled = false;
            ZigBee4NoksConnectButtom.Enabled = true;
            // artik 4noks test panelini pasif edebiliriz
            Z4NoksPanel.Enabled = false;
            //////////////////////////////////////
        }

       

        private List<byte> ModbusPollingF(UInt16 AddressForCheck, UInt16 Command, UInt16 valueX) // Ana hatlari ile network uzerinde sorgu yapilma fonksiyonu bu olacak... 
        {
            // Burada ornegin network'e kimler bagli? Tum networkte cesitli data okuma vs.. hepsi bununla yapilacak...
            // Yazilimin bir yerinde bir Union tanimlayip olasi Unicast Query islerini toplayalim... Daha sonrasinda elbette !!!
            // Simdilik network'teki uyelerin adresini ogrenmeye calistigimiz icun ne gelirse gelsin 0 olsun degeri sadece ilk ise odaklanalim
            List<byte> ReturnFrame = new List<byte>(); ReturnFrame.Clear(); // temisle, bunu kullanacaz hazirlanan Frame vs icun !!!
            ///////////////////////////////////////////////////////////
            List<byte> outpolling = new List<byte>(); outpolling.Clear(); // temisle
            outpolling.Add(0x01);
            //
            if (Command == 4) // aktif guc
            {
                    outpolling[0] = Convert.ToByte(AddressForCheck);
                    outpolling.Add(0x04);
                    outpolling.Add(0x00);
                    outpolling.Add(0x05);
                    outpolling.Add(0x00);
                    outpolling.Add(0x01);
            ReturnFrame = outpolling;
            }

            else if (Command == 5) // aktif guc ?????? // role ac kapa yapacaz, 1 ve 2 kullanilacak !!!!
            {
                outpolling[0] = Convert.ToByte(AddressForCheck);
                outpolling.Add(0x05);
                outpolling.Add(0x00);
                outpolling.Add((byte) valueX);
                outpolling.Add(0xFF);
                outpolling.Add(0x00);
            ReturnFrame = outpolling;            
            }

            // role acmak icun
            //param2.Text = "05";
            //param3.Text = "00";
            //param4.Text = "01";
            //param5.Text = "255";
            //param6.Text = "00";


            // role kapatmak icin
            //param2.Text = "05";
            //param3.Text = "00";
            //param4.Text = "02";
            //param5.Text = "255";
            //param6.Text = "00";

            return ReturnFrame;
        }

        static bool CRC16(int datalength, bool check, ref List<byte> data)
        {
            int checksum;
            byte lowCRC;
            byte highCRC;
            int i, j;
            checksum = 0xffff;

            for (j = 0; j < datalength; j++)
            {
                checksum = checksum ^ ((int)data[j]);
                for (i = 8; i > 0; i--)
                {
                    if (checksum % 2 == 1)
                    {
                        checksum = (checksum >> 1) ^ 0xa001;
                    }
                    else
                    {
                        checksum >>= 1;
                    }
                }
            }


            highCRC = (byte)(checksum >> 8);
            checksum <<= 8;
            lowCRC = (byte)(checksum >> 8);

            if (true == check)
            {
                if ((data[datalength + 1] == highCRC) && (data[datalength] == lowCRC))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                data.Add(lowCRC);
                data.Add(highCRC);
            }
            return true;
        }

        

        private void rchTxt_TextChanged(object sender, EventArgs e)
        {

        }

        public void doldur()
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

        private uint[] xtender_excel_sorgulama(uint State, UInt32 Command)
        {
            // state=1 ise okuma, 2 ise yazdirma oldugu anlasilacak
            // 1 ise F5, 2 ise F1 kullanilacak
            uint[] ReturnValue = new uint[] {0,0,0};
            ////////
            ReturnValue[0] = State;

            if (State == 1) // the means read
            {     
                 // F5 olacak 5, 2. elemandan baslayacak . 
                 //
                
                for (int i = 2; i < 17; i++)
                {
                    
                    if (  dataGridView1.Rows[i].Cells[5].Value.ToString() == Command.ToString())
                    {
                        
                        ReturnValue[1] = 1; // bulduk ve bu deger deger dogru ise
                        ReturnValue[2] = 1; // simdilik daima float
                    }
                          
                }

                 
                    
            }
            else if (State == 2) // means write
            {
                // F1 olacak 1, 2. elemandan baslayacak
                for (int i = 2; i < 19; i++)
                {
                    textBox3.Text = dataGridView1.Rows[18].Cells[0].Value.ToString();
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == Command.ToString())
                    {
                        ReturnValue[1] = 1; // bulduk ve bu deger dogru
                        // simdi turunu bulalim
                        ReturnValue[2] = ((uint)Convert.ToUInt16(dataGridView1.Rows[i].Cells[0 + 3].Value.ToString()));

                    }
                }
            }


           

            return ReturnValue;
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

        private void SoftStopButtom_Click(object sender, EventArgs e) // SoftStopButtom
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

        private void textBox51_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
    }



}


