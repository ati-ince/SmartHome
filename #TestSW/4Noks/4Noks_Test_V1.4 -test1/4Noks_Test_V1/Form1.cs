// @ ATI'nin malidir, hacilamak yasaktir .......
// atahir.ince@gmail.com
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

namespace _4Noks_Test_V1
{
    public partial class Form1 : Form
    {

        private static Form1 instance;

        /////////////////
        static Int16 AddressF, AddressL, Indexx; // fırst and last adress 
        //static Int16 FrameX = 0; // for collected 
        static List<byte> _4NoksUartFrame = new List<byte>();
        static bool UartState = false;

        /// <summary>
        /// //Class's and Struct's

        SerialCOMM.ComPortStruct _4noksStruct = new SerialCOMM.ComPortStruct();// also we have _xtenderStruct._SerialPortObj
        SerialPort _4noksserial = new SerialPort();
        _4Noks _4noksClass = new _4Noks();
        ///////
        /**/
        SerialCOMM _serialcomm = new SerialCOMM();
        /**/
        Logging _log = new Logging();

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
            _4noksserial.DataReceived += new SerialDataReceivedEventHandler(_4NoksSerial_DataReceived);
        }

        void _4NoksSerial_DataReceived (object sender, SerialDataReceivedEventArgs e)
        {
            int Result_4Noks;
            // (#sil)   StandardSerialComStruct.StructXtender.DataFrameRead = new byte[_XtenderSerial.BytesToRead]; // Define Struct element size for read the serial data

            Result_4Noks = _serialcomm.SerialRead(ref _4noksStruct);

            // throw new NotImplementedException(); // this is new version
            this.Invoke(new EventHandler(_4Noks_DisplayText)); // old version and worked before
        }

        // All jobs into here !!!
        private void _4Noks_DisplayText(object sender, EventArgs e)
        {
            byte[] _4NoksReceivedFrame = _serialcomm.SerialDataTidyUp(ref _4noksStruct); // data clean up is necassary
            //
            if (UartState == false)
            {
                _4NoksUartFrame.AddRange(_4NoksReceivedFrame);
                UartTimer.Enabled = true; // statrt the Timer for UART
            }
      
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // Disable all Disconnection and Stop ButtonsFexcel
            Connect_ModBus4Noks.Enabled = true;

            // Start the LOGGING !!!
            _log.LoggingStart();//sadece ilk program baslarken cagirilacak..

            /**************************************************************/
            // Call all Active ComPort Names for Xtender
            _4noksStruct._GetPortNames = SerialPort.GetPortNames();// will use in almost everywhere

            // fill all ComboBox using available ComPorNames
            _serialcomm.ComboBoxComPortNameFilling(_4noksStruct._GetPortNames, ComboBox_Modbus4Noks);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            /********* Close all COM ports and Stop Algorithm ****************/
            // Close Xtender Serial Port
            // Portu kapatalim
            _serialcomm.SerialClose(ref _4noksStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            /*****/
        }

        private void ComboBox_Modbus4Noks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Connect_ModBus4Noks_Click(object sender, EventArgs e)
        {
            // Port name felan Combodan cekilmekte ve asagida SerialComm icerisinde kullanilmaktadir !!!
            _4noksClass._4NoksComPortSettings(ref _4noksStruct, ref _4noksserial, ComboBox_Modbus4Noks);

            //SerialComm icerisinde port ayarlari ise simdi yapilabilir. 
            _serialcomm.ComPortSettings(ref _4noksStruct); // boylece _XtenderSerialPort ayarlari tamamlanmis oldu. (SerialComm ile genel sekilde halledilebildi)
            
            // Portu acip kullanmaya baslayalim...
            _serialcomm.SerialOpen(ref _4noksStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            //
            _4NoksUartFrame.Clear(); // clear the frame
            
            /**************************************/
            Connect_ModBus4Noks.Enabled = false;
            Disconnect_ModBus4Noks.Enabled = true;
        }

        private void Disconnect_ModBus4Noks_Click(object sender, EventArgs e)
        {
            // Close Xtender Serial Port
            // Portu acip kullanmaya baslayalim...
            _serialcomm.SerialClose(ref _4noksStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            /*****/
            Connect_ModBus4Noks.Enabled = true;
            Disconnect_ModBus4Noks.Enabled = false;

        }

        private void lstDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PoolTimer.Enabled = true; // calısmaya basladı...
            //
            AddressF = 16; // olsun baslangıcta
            Indexx = 16; // tımer ılk pool bu adress ıle baslayacak...
            //
            AddressL= Int16.Parse(textBox1.Text); // check edılec ek son adresımız...
            //
            CheckButton.Enabled = false;
            
        }

        private void PoolTimer_Tick(object sender, EventArgs e)
        {
            _4noksStruct.DataFrameWrite = _4noksClass.ModbusPollingF((ushort)Indexx, 4, 0).ToArray(); // burada gıdecek byte array structa yazılıyor...
            _serialcomm.SerialWrite(ref _4noksStruct);
            //
            Indexx++; //artıralım
            //
            if (Indexx > AddressL)
            {
                PoolTimer.Enabled = false; // son adres te check edılınce durduralım artık....
                CheckButton.Enabled = true;
            }
        }

        private void UartTimer_Tick(object sender, EventArgs e)
        {
            UartTimer.Enabled = false;
            UartState = false; // ready for new frame....
            // And saw into the windows...
            lstDeviceList.Items.Add(BitConverter.ToString(_4NoksUartFrame.ToArray()));
            //
            int payload = -1; 
            if (_4NoksUartFrame.Count() > 4)
            {
                payload= (int)_4NoksUartFrame[3] * 256 + (int)_4NoksUartFrame[4];
            }
            // 0-> Adress, 1-> TypeOfData(Power etc), 2-> OtherSpec, 3-> Data (ActivePowerFirst), 4-> ChecksumData
            listBox1.Items.Add(_4NoksUartFrame[0].ToString());// adress
            listBox2.Items.Add(payload.ToString());// 
            
            // checksum kontrolu
            bool checksum = _4noksClass.CRC16(_4NoksUartFrame.Count, false, ref _4NoksUartFrame);
            listBox3.Items.Add(checksum.ToString());
            ///////
            _4NoksUartFrame.Clear(); // clean for news..
        }

        private List<int> ModbusFrameRender(List<byte> UartList)
        {
            List<int> OutList = new List<int>(); OutList.Clear();
            // 1-> Adress, 2-> TypeOfData(Power etc), 3-> OtherSpec, 4-> Data (ActivePowerFirst), 5-> ChecksumData
            int Addr, DatTyp, Spec, PayLoad, ChecksumS;
            int IndexMFR=0;
            //
            int len = UartList.Count();
            byte[] UartArray = UartList.ToArray();
            /****************************/
            // Adress
            Addr = UartArray[IndexMFR] * 16 + UartArray[IndexMFR++] *1;
            OutList.Add(Addr);
            //
            OutList.Add(len); // bakam neymis
            /*
            //DatTyp
            IndexMFR++;
            DatTyp = UartArray[IndexMFR++] * 16 + UartArray[IndexMFR++] * 1;
            OutList.Add(DatTyp);
            // Spec
            IndexMFR++;
            Spec = UartArray[IndexMFR++] * 16 + UartArray[IndexMFR++] * 1;
            OutList.Add(Spec);
            // Payload
            IndexMFR++;
            PayLoad = UartArray[IndexMFR++] * 4096 + UartArray[IndexMFR++] * 16 * 256;
            IndexMFR++;
            PayLoad = PayLoad + UartArray[IndexMFR++] * 16 + UartArray[IndexMFR++] * 1;
            OutList.Add(PayLoad);
            // Checksum
            IndexMFR++;
            ChecksumS = UartArray[IndexMFR++] * 4096 + UartArray[IndexMFR++] * 16 * 256;
            IndexMFR++;
            ChecksumS = ChecksumS + UartArray[IndexMFR++] * 16 + UartArray[IndexMFR++] * 1;
            OutList.Add(ChecksumS);
            */
            /****************************/
            return OutList;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
