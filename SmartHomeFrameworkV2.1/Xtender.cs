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

namespace SmartHomeFrameworkV2._1
{
    public class Xtender  
    {
        /// <summary>
        /// GLOBAL CLASS
        /// </summary>
        SerialCOMM serialcomm = new SerialCOMM();
        OleDbConnection connection; // Excel Connection
        DataBaseSQL databasesql = new DataBaseSQL();


        /// <summary>
        /// METHODS .........
        /// </summary>


        // Excel connction
        public void ExcellConnection()
        {
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\xtender_write_komutlari.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;'");
        }
        /***************************************************************************************/

        public void XtenderComPortSettings(ref SerialCOMM.ComPortStruct xtendercomportstruct,ref  SerialCOMM.StandardSerialComStruct standartserialcomstruct_xtenderportsettings, System.Windows.Forms.ComboBox ComboBox_XtenderSettings)
        {
            //
            xtendercomportstruct.PortName = serialcomm.GetSelectedPortNamesFromComboBox(standartserialcomstruct_xtenderportsettings._GetPortNames, ComboBox_XtenderSettings);
            //Necessary settings ..........................................
            xtendercomportstruct.BaudRate = 38400;
            xtendercomportstruct.DataBits = 8;
            xtendercomportstruct.StopBits = System.IO.Ports.StopBits.One;
            xtendercomportstruct.Parity = System.IO.Ports.Parity.Even;
            xtendercomportstruct.ReadTimeout = 3000;
            xtendercomportstruct.WriteTimeout = 4000;
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

        // Excell read and fill the Grid after give the number to Grid for Excel .............
        public void FillTheExcel(System.Windows.Forms.DataGridView DataGridViewXtenderExcel)
        {
            ExcellConnection(); // first, connect to excel
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            DataGridViewXtenderExcel.DataSource = dt.DefaultView;
            connection.Close();
            ///
            GridNumeratorForExcell(DataGridViewXtenderExcel);
        }
        //*************************************************************************************

        //We use this giving numbers to Grig for excell
        private static void GridNumeratorForExcell(System.Windows.Forms.DataGridView DataGridViewXtenderExcel)
        {
            if (DataGridViewXtenderExcel != null)
            {
                for (int count = 0; (count <= (DataGridViewXtenderExcel.Rows.Count - 1)); count++)
                {
                    string sayi = (count + 1).ToString();
                    DataGridViewXtenderExcel.Rows[count].HeaderCell.Value = sayi;
                }
            }
        }
        //*************************************************************************************


        // In here, will questioning read excell by form 
        // When doing questioning, we seach in the datagridview list the register adres inside the list? 

        public uint[] Xtender_Excell_Questioning(System.Windows.Forms.DataGridView DataGridViewXtenderExcel, uint State, UInt32 Command)
        {
            // State = 1 is READ, State=2 is WRITE
            // In list, F6 use for 1 (Read), F1 use for 2 (Write)  (But seems F6 use for READ in the List)
            // [0]. colon for write [5]. colon for read . F1 and F6 
            uint[] ReturnValue = new uint[] { 0, 0, 0 };
            ////////
            ReturnValue[0] = State;

            if (State == 1) // the means read
            {

                //

                for (int i = 2; i < 17; i++)
                {

                    if (DataGridViewXtenderExcel.Rows[i].Cells[5].Value.ToString() == Command.ToString())
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
                    if (DataGridViewXtenderExcel.Rows[i].Cells[0].Value.ToString() == Command.ToString())
                    {
                        ReturnValue[1] = 1; // bulduk ve bu deger dogru
                        // simdi turunu bulalim
                        ReturnValue[2] = ((uint)Convert.ToUInt16(DataGridViewXtenderExcel.Rows[i].Cells[0 + 3].Value.ToString()));

                    }
                }
            }

            return ReturnValue;
        }
        //*************************************************************************************

        // Now, write the Xtender read value to DataBase
        // public void Xtender(string date, int command, float data)
        public void Xtender_AddTo_DataBase(byte[] XtenderReceivedFrame)
        {
            List<float> XtenderDataRenderedOutput= new List<float>();
            XtenderDataRenderedOutput.Clear(); // clean and empty
            //
            XtenderDataRenderedOutput = XtenderDataRendering (XtenderReceivedFrame) ;
            // Now divide the list and write to the DataBase ....
        }
        //*************************************************************************************
    }
}
