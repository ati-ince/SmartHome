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

namespace SerialApp
{
    // Burada extra olarak ExcelClass yapisina ihtiyac duymaktayiz....
    class Xtender: SerialCOMM // get what we need We can Add One Class and Multiple Interface !!!
    {
        /// <summary>
        /// GLOBAL CLASS
        /// </summary>
        ExcelUsege _exceluse = new ExcelUsege(); // Burada sorgulama vs diger isleri yaptiracagiz... 
        DataBaseSQL databasesql= new DataBaseSQL(); // database kayitlari da buraya...
        /**/

         //_exceluse.ExcelCall("xtender_write_komutlari.xlsx", ref _excelStruct);
         //   //_exceluse.ExcelXtenderWriteRegisterList(ref _excelStruct);// list<int>
         //   string[] came = _exceluse.XtenderExcelQuestioning(3023, _excelStruct.ExcelReadList);//true false
         //   textBox1.Text = came[0];//coord
         //   textBox2.Text = came[1];//state

        // In here, we get the port name from Combobox !!! May be later, we add in here direclty !!!
        // Sadece STRUCT icerisinde gerekli |FIELD'leri dolduruyoruz, ASIL is yine de SERIALCOMM iceriisnde yapilacak... 
        public void XtenderComPortSettings(ref SerialCOMM.ComPortStruct _comportStruct, ref SerialPort _xtenderSerial, System.Windows.Forms.ComboBox ComboBox_XtenderSettings)
        {
            _comportStruct._SerialPortObj = _xtenderSerial;// In here we define Port Obj to class !!!

            // _comportStruct.PortName = "COM1"; // some day may be we need this usage...
            _comportStruct.PortName = GetSelectedPortNamesFromComboBox(_comportStruct._GetPortNames, ComboBox_XtenderSettings);
            _comportStruct.BaudRate = 38400;
            _comportStruct.DataBits = 8;
            _comportStruct.StopBits = System.IO.Ports.StopBits.One;
            _comportStruct.Parity = System.IO.Ports.Parity.Even;
            _comportStruct.ReadTimeout = 3000;
            _comportStruct.WriteTimeout = 4000;
            //
            _comportStruct.PortINFO = "Xtender"; // Log icin kullanilabilir.
        }
        /***************************************************************************************/

        // in this method, xtender inverter read data frame will create using register adress like 3000 and more...
        private List<byte> XtenderReadFrameCreate(UInt16 XtenderReadRegAddr)
        {
        
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
        command_XtenderReadFrame.Add((byte)(XtenderReadRegAddr & 0xFF));
        command_XtenderReadFrame.Add((byte)(XtenderReadRegAddr >> 8 & 0xFF));
        command_XtenderReadFrame.Add(0x00);
        command_XtenderReadFrame.Add(0x00);

        ///Property ID (The last), 2 bytes
        command_XtenderReadFrame.Add(0x01);
        command_XtenderReadFrame.Add(0x00);

        calculateChecksum(ref command_XtenderReadFrame, 14, 10);

        return command_XtenderReadFrame;

        }
        //*************************************************************************************

        // in this method, xtender inverter Write data frame will create using register adress like 1127 (writing register) and more...
        // All data type coded with number: Float = 1, Bool = 0, and INT32 = 2
        // Different data type create different sized Writing Frames , such as Float = 30 byte, Bool = 27 byte
        private List<byte> XtenderCreateWriteFrame(UInt16 XtenderWriteRegAddr, float VALUE, uint DataType)
        {
            Dictionary<uint, List<byte>> XtenderCreateWriteFrame = new Dictionary<uint, List<byte>>();
            List<byte> command_XtenderWriteFrame = new List<byte>();

            command_XtenderWriteFrame.Clear();// empty and clean frame for fill up
            XtenderCreateWriteFrame.Clear();// same as well  

            bool bool_value = true;  // first defination
            /*****************************************/
            if (DataType == 0) //LIKE use with grid allowed register , relay 1-0
            {
                bool_value = true;
            }
            else if (DataType == 1) // Like use with grid feed max current
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

            // Add the register values
            command_XtenderWriteFrame.Add((byte)(XtenderWriteRegAddr & 0xFF));
            command_XtenderWriteFrame.Add((byte)(XtenderWriteRegAddr >> 8 & 0xFF));
            command_XtenderWriteFrame.Add(0x00);
            command_XtenderWriteFrame.Add(0x00);

            ///Property ID (The last), 2 bytes
            command_XtenderWriteFrame.Add(0x05); // write icin 0x05 istenmekte
            command_XtenderWriteFrame.Add(0x00);

            // ADD VALUE IF VALUE is FLOAT
            if (bool_value == false) // FLOAT 30 byte, bool 27 byte
            {
                for (uint i = 0; i < (BitConverter.GetBytes(VALUE).Length); i++)
                {
                    // float value convert to 8byte value (4byte)
                    command_XtenderWriteFrame.Add(BitConverter.GetBytes(VALUE)[i]); 
                }
            }
            else // BOOL
            {
                if (VALUE == 0)
                    command_XtenderWriteFrame.Add(0x00);
                else
                    command_XtenderWriteFrame.Add(0x01);

                // First byte collect all bool information 1 or 0 
                // other bytes just 0x00 
                command_XtenderWriteFrame.Add(0x00);
                command_XtenderWriteFrame.Add(0x00);
                command_XtenderWriteFrame.Add(0x00);
            }

            // the last checksum
            calculateChecksum(ref command_XtenderWriteFrame, 14, 14);

            return command_XtenderWriteFrame;
        }
        //*************************************************************************************

        // This private method use for calculation when creating a frame
        private bool calculateChecksum(ref List<byte> lBytes, int startIndex, int count, bool readOnly = false)
        {
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
                    return false;
                }
            }
        return true;
        }
        //*************************************************************************************

        
        // All important job do inside.........................................................
        // But now, we just get the a bit of information. We will use list for getting the nformation from this rendering method. 
        private List<float> XtenderDataRendering(byte[] XtenderReceivedFrame)
        {
            List<float> XtenderDataRendering_output= new List<float>();
            XtenderDataRendering_output.Clear(); // clean and empty for carring important informations
            /******############*****/
            // Just now, we render just Read float value, in the List we fill [0]=0, and [1]=1 for this purpose....
            float FrameType =  0; // read = 0, write is = 1; 
            float DataType = 1 ; // Float = 1, Bool = 0, and INT32 = 2
            //
            float ReadValue_xtender;
            UInt16 ReadRegister_xtender;
            byte[] RxByte_Xtender = XtenderReceivedFrame; // may be will change the name later ............
            /*########################*/

            if (RxByte_Xtender.Length >= 28) // Reading Respond from Xtender
            // Frame from Xtender, bool = 28 and float 30 byte
            {     
                if (RxByte_Xtender.Length == 28) // BOOL frame, 28 byte
                {
                   // do something
                }
                else if (RxByte_Xtender.Length == 30 ) // 30 byte FLOAT coming from Xtender Read Responde ...........
                {
                    // 4 byte coming value convert to Float value
                    ReadValue_xtender = BitConverter.ToSingle(RxByte_Xtender, 24); // [24],[25],[26],[27] use that byte to convert Float valu
                    
                    // 4 byte coming value convert to Uint16 Register Adress/Code
                    ReadRegister_xtender = BitConverter.ToUInt16(RxByte_Xtender, 18);

                    // Now, fill the 3 element float List for anything (show, database and so on....)
                    XtenderDataRendering_output.Add(0); //means reading frame type
                    XtenderDataRendering_output.Add(1); // means FLOAT datatype
                    XtenderDataRendering_output.Add(ReadValue_xtender); // Xtender Read responde
                    XtenderDataRendering_output.Add((float)ReadRegister_xtender); // Xtender Read Register Adress (when read this list we convert to ushort again)
                }
            }
            else if (RxByte_Xtender.Length == 26) // Writing Frame respond from Xtender
            {
                // do something ............    
            }
            // and write to the Remote Comm ....
            /*##################*/

        return XtenderDataRendering_output;
        }
        //*************************************************************************************

       

        // Now, write the Xtender read value to DataBase
        // With this purpose, we use cming data (Datareceive Handle fn)
        public void Xtender_AddTo_DataBase(byte[] XtenderReceivedFrame)
        {
            List<float> XtenderDataRenderedOutput= new List<float>();
            XtenderDataRenderedOutput.Clear(); // clean and empty
            //
            XtenderDataRenderedOutput = XtenderDataRendering (XtenderReceivedFrame) ;
            // Now divide the list and write to the DataBase ....
        }
        //*************************************************************************************

        // xtender Read Preparing and sending over serial port
        public void XtenderSendReadData(UInt16 xRegAddr, ref ComPortStruct _comStruct)
        {
            _comStruct.DataFrameWrite = XtenderReadFrameCreate(xRegAddr).ToArray(); // convert List to Array (byte[] array)
            SerialWrite(ref _comStruct); // Hazirlanan Liste SerialPort'a yazdirildi        
        }
        //*************************************************************************************
    }
}
