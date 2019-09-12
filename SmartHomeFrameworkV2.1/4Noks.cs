// **********************************************************//
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
using System.Text.RegularExpressions;
// Excel
using System.Data.OleDb;
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;
// **********************************************************//
// in this class, we defined all necessary sub function getting data from serial port for Four4Noks Modbus-RTU Serial Communication



// 4Noks Open/Close kismini unutmayalim.. Ve Kurulum kismi elbette ---> Xtender ile benzer sekidle SerialCOMM'u cagirip ac kapa yapacaz..
// 

namespace SmartHomeFrameworkV2._1
{
    class _4Noks : SerialCOMM
    {

        public List<byte> ModbusPollingF(UInt16 AddressForCheck, UInt16 Command, UInt16 valueX) // Ana hatlari ile network uzerinde sorgu yapilma fonksiyonu bu olacak... 
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

            else if (Command == 5) // aktif guc ?????? // role ac kapa yapacaz, 1 ve 2 kullanilacak !!!! 1-> Elektrıgı dısarı verebılıyor.
            {
                outpolling[0] = Convert.ToByte(AddressForCheck);
                outpolling.Add(0x05);
                outpolling.Add(0x00);
                outpolling.Add((byte)valueX);
                outpolling.Add(0xFF);
                outpolling.Add(0x00);
                ReturnFrame = outpolling;
            }

            //// CRC de ekleyelım....
            CRC16(outpolling.Count, false, ref ReturnFrame);
           

            return ReturnFrame;
        }

        //

        public bool CRC16(int datalength, bool check, ref List<byte> data) // bosu bosuna check tanımlamıslar, ıslevsız bu !!!
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


        public void _4NoksComPortSettings (ref SerialCOMM.ComPortStruct _comportStruct, ref SerialPort _4noksserial, System.Windows.Forms.ComboBox ComboBox_4NoksSettings)
        {
            _comportStruct._SerialPortObj = _4noksserial;// In here we define Port Obj to class !!!

            // _comportStruct.PortName = "COM1"; // some day may be we need this usage...
            _comportStruct.PortName = GetSelectedPortNamesFromComboBox(_comportStruct._GetPortNames, ComboBox_4NoksSettings);
            _comportStruct.BaudRate = 9600;
            _comportStruct.DataBits = 8;
            _comportStruct.StopBits = System.IO.Ports.StopBits.One;
            _comportStruct.Parity = System.IO.Ports.Parity.None;
            _comportStruct.ReadTimeout = 400;
            _comportStruct.WriteTimeout = 50;
            //
            _comportStruct.PortINFO = "4Noks"; // Log icin kullanilabilir.
        }



        //////////////////

        // 4NOKSCOMING
        //private void Z4Noks_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        //{   // Event for receiving data
        //    //Read the buffer to text box.
        //    SerialPort sp = (SerialPort)sender;
        //    int sonuc_4noks;

        //    RxByte_4Noks = new byte[_4NoksSerial.BytesToRead];
        //    sonuc_4noks = _4NoksSerial.Read(RxByte_4Noks, 0, _4NoksSerial.BytesToRead);//i'll change string to byte 
        //    //in this place i have to decide whic data is came....

        //    if ((RxByte_4Noks.Length == 7) && (Z4NoksComingFrameCounter == 0))
        //    {
        //        // eger tum data dogru gelmis, 7 byte ise o zaman isleyelim
        //        Z4NoksComingFrameCounter = 0; // burada sifirlayalim....
        //        Z4NoksComingFramePartially.AddRange(RxByte_4Noks);
        //        this.Invoke(new EventHandler(Z4Noks_DisplayText));
        //    }
        //    else if ((RxByte_4Noks.Length != 7))
        //    {
        //        Z4NoksComingFrameCounter = Z4NoksComingFrameCounter + (uint)RxByte_4Noks.Length;
        //        Z4NoksComingFramePartially.AddRange(RxByte_4Noks);

        //        if (Z4NoksComingFrameCounter >= 7)
        //        {
        //            Z4NoksComingFrameCounter = 0; // burada sifirlayalim....
        //            this.Invoke(new EventHandler(Z4Noks_DisplayText));
        //        }
        //    }

        //}

        //////////////////

        // 4NoksRespond 
        //private void Z4Noks_DisplayText(object sender, EventArgs e)
        //{
        //    double valuee = 0;
        //    byte[] RemoteComm_4Noks_ActivePowerValue = new byte[] { 0, 0, 0, 0 }; // empty

        //    ///////////////
        //    ///////////////      
        //    if (Z4NoksReadCheck == true)
        //    {
        //        RemoteSerial4NoksActivePowerFrame.Clear();
        //        //
        //        valuee = System.Math.Pow(16, 2) * Z4NoksComingFramePartially.ToArray()[3] + Z4NoksComingFramePartially.ToArray()[4];
        //        textBox51.Text = valuee.ToString();
        //        textBox51.Text = Z4NoksComingFramePartially.Count.ToString();
        //        //
        //        Z4NoksComingFramePartially.Clear();// yeni seyler icin temizlik 
        //        //
        //        Z4NoksReadCheck = false; // geri temizleyelim...
        //        // seri porttan basalim cevabi...
        //        for (uint say = 0; say < 7; say++)
        //            RemoteSerial4NoksActivePowerFrame.Add(RxByte_RemoteComm[say]); // 7ye kadar ekleyelim daha sonra data olacak...

        //        ////// data 4byte yapacaz doubleyi

        //        ///VALUE-DATA (4byte)
        //        /// double datayi float yapalim 4 byte olmasi icin, yoksa 8 byte oluyor...
        //        float ActivePower = (float)valuee;
        //        textBox51.Text = ActivePower.ToString();
        //        for (uint i = 0; i < (BitConverter.GetBytes(ActivePower).Length); i++)
        //        {
        //            RemoteSerial4NoksActivePowerFrame.Add(BitConverter.GetBytes(ActivePower)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
        //        }
        //        // galiba hallettik
        //        _RemoteCommSerial.Write(RemoteSerial4NoksActivePowerFrame.ToArray(), 0, RemoteSerial4NoksActivePowerFrame.Count);// her sey tamam sa....
        //    }

        //}

        /////////


        // Burada nasil 4Noks gonderilecek data hazirlaniyor ----- > Bunu da REMOTE COMM'dan cekeyim ornek olsun...
        /////////////////////


            ///////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////
            // Burada 4Noks Read su sekilde olmakta
            // [01 (read)] [02 (4NoksFamily)] [XX (4Noks Address)] [xx (Command-ObjectID)] -> Command digerlerinde oldugu gibi 4Byte yer kaplasin ki ayni fn lari kullanabileleim!!!
            // 
            //if (RxByte_RemoteComm[1] == 2)// Bu RemoteComm sanal portundan 2 ile bir frame gelirse bunun 4Noks icin oldugunu bilecez
            //{
            //    if (RxByte_RemoteComm.Length == 7 || RxByte_RemoteComm.Length == 8)// write 8byte gelmesi, read icin 7 olmasi lazim gerekiyor, 
            //    {
            //        // READ READ READ ...... yanlis yere yazma, evet biliyorum oprtalik karisik su anda !!!!!!!!!!!!!!!!!!!!
            //        if (RxByte_RemoteComm[0] == 1)// check ediyoz 1 ile READ olsun  
            //        {
            //            if (RxByte_RemoteComm.Length == 7)// Burada alinan datalari uyarlamak gerekiyor... read icin gelen data su (7 byte olacak)
            //            {
            //                Z4NoksReadCheck = true; // 4noks icin read geldi . kaydedek
            //                ///////////////////////////////////////////////////////////////
            //                // textBox3.Text = RxByte_RemoteComm.Length.ToString(); // Ok check ettik 7 data ulasiyor en azindan bunu basalim simdi porta 
            //                //// 01(write), 02(4Noks), xx(adress), 4byte frame (objID)    Oncelikle ObjectID formatini duzenleyelim
            //                ///
            //                for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)// 4byte dolduruyoruz...
            //                {
            //                    RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
            //                }
            //                ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 1,3, 5 vs... yazdirabildik sorguya...
            //                //
                            
            //                // Burada oncelikle 4Noks FrameCreate olusturacaz
            //                ModBusFrameGlobal.Clear(); // clear this MF
            //                ModBusComingFrame.Clear();

            //                bool checksum = false;
            //                ///////////////////////////////////////////////
            //                UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
            //                ///////////////////////////////////////////////////////
            //                ModBusFrameGlobal = ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), 0); // we have to use thus frame pointer or adress, read ederken 3. kisma rasgele bir sey yazsak olur... 0 yazdim ama bos olmayacak bir tek
            //                // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
            //                // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

            //                checksum = CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);


            //                if (checksum == true)
            //                {                                
            //                    // Bu kisimda data yazdirildiktan sonra bir check sureci olmali 
            //                    _4NoksSerial.Write(ModBusFrameGlobal.ToArray(), 0, ModBusFrameGlobal.Count); // istenilen yaptirilabiliyor...
            //                    // check sureci icin mini bekleme
            //                    //System.Threading.Thread.Sleep(49); // 49ms bekleyelim...
            //                    // Umariz bu anda bir veri neyin gelmez, daha sonra Artificial Event olusturup onu yaptiririz. Simdilik bole olsun
            //                }
            //            }
                    
            //        }

            //        else if (RxByte_RemoteComm[0] == 2) // check ediyoz 2 ile write olsun   
            //        {
            //            if (RxByte_RemoteComm.Length == 8)// Burada alinan datalari uyarlamak gerekiyor... WRITE icin gelen data su (8 byte olacak)
            //            {

            //                //// 02(write), 02(4Noks), xx(adress), Command, ObjID, 4byte frame (objID)    Oncelikle ObjectID formatini duzenleyelim
            //                ///
            //                for (uint i = 0; i < RemoteComm_Coming_ObjectID_Frame.Length; i++)// 4byte dolduruyoruz...
            //                {
            //                    RemoteComm_Coming_ObjectID_Frame[i] = RxByte_RemoteComm[i + 3];// 3. byteden basliyor
            //                }
            //                ObjID = BitConverter.ToUInt32(RemoteComm_Coming_ObjectID_Frame, 0);// galiba ObjectID mesela 1,3, 5 vs... yazdirabildik sorguya...
            //                //

            //                // Burada oncelikle 4Noks FrameCreate olusturacaz
            //                ModBusFrameGlobal.Clear(); // clear this MF
            //                ModBusComingFrame.Clear();

            //                bool checksum = false;
            //                ///////////////////////////////////////////////
            //                UInt16 Z4Noks_Address = (UInt16)RxByte_RemoteComm[2];
            //                ///////////////////////////////////////////////////////
            //                ModBusFrameGlobal = ModbusPollingF((UInt16)Z4Noks_Address, ((UInt16)ObjID), ((UInt16)RxByte_RemoteComm[7])); // we have to use thus frame pointer or adress
            //                // Adres, Komut (4aktif guc okuma, 5 ise role ac kapa. 1 ac, 2 kapa olacak)
            //                // Burada konulan 0 bir ise yaramamakta, ama bir sey koymak zorundayiz fn bozulmamasi icin.

            //                checksum = CRC16(ModBusFrameGlobal.Count, false, ref ModBusFrameGlobal);

            //                if (checksum == true)
            //                {
                                
            //                    // Bu kisimda data yazdirildiktan sonra bir check sureci olmali 
            //                    _4NoksSerial.Write(ModBusFrameGlobal.ToArray(), 0, ModBusFrameGlobal.Count); // istenilen yaptirilabiliyor...
            //                    // check sureci icin mini bekleme
            //                    //System.Threading.Thread.Sleep(49); // 49ms bekleyelim...
            //                    // Umariz bu anda bir veri neyin gelmez, daha sonra Artificial Event olusturup onu yaptiririz. Simdilik bole olsun
            //                }
            //                // Yazdirdik diye geri donus verelim kullaniciya...
            //                // 02(write cevabi) 02(4noks family) 2D (adres, 45 mesela=2D) 05 00 00 00(yazdirilan komut, 4byte) 01 (1 ise open yapildi 2 ise close)
            //                List<byte> RemoteCommWriteBuff = new List<byte>(); RemoteCommWriteBuff.Clear();// temisleyelim
            //                RemoteCommWriteBuff.Clear(); // temisleyelim
            //                RemoteCommWriteBuff.Add(0x02);// write cevabi
            //                RemoteCommWriteBuff.Add(0x02);// 4Noks ailesi
            //                RemoteCommWriteBuff.AddRange(ModBusFrameGlobal);// geri kalanini ekleyelim framenin
            //                // Burasi guzel oldu. Yazdirilan modbus frame basina 0x02 ile write cevabi ve 0x02 ile de 4noksun cevabi diye yazdirdik...
            //                // Kullanici boylece check edebilecek...
            //                _RemoteCommSerial.Write(RemoteCommWriteBuff.ToArray(), 0, RemoteCommWriteBuff.Count);// her sey tamam sa....
            //                ////////////////////////////////////////////////////////////////////////////////////////////

            //            }
            //        }

            //////////////////////////////////////
            //////////////////////////////////////
            //////////////////////////////////////
    }
}
