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


namespace SmartHomeFrameworkV2._1
{
    public partial class Form1 : Form  
    {
        private static Form1 instance;

        /// <summary>
        //Class's and Struct's
        /**/
        SerialCOMM.ComPortStruct _xtenderStruct = new SerialCOMM.ComPortStruct(); // also we have _xtenderStruct._SerialPortObj
        SerialPort _xtenderSerial = new SerialPort();
        Xtender XtenderClass = new Xtender();
        ExcelUsege _exceluse = new ExcelUsege(); // excel kullanimi amaci ile
        ExcelUsege.ExcelStruct _excelStruct = new ExcelUsege.ExcelStruct(); // excel icin structor 
        /**/
        SerialCOMM _serialcomm = new SerialCOMM();
        /**/
        Logging _log = new Logging();
        /**/
        Algorithms AlgorithmClass = new Algorithms();
 

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
            _xtenderSerial.DataReceived += new SerialDataReceivedEventHandler(_XtenderSerial_DataReceived); 

        }

        // this method may be change to old version of my code
        void _XtenderSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int Result_Xtender;
        // (#sil)   StandardSerialComStruct.StructXtender.DataFrameRead = new byte[_XtenderSerial.BytesToRead]; // Define Struct element size for read the serial data

            Result_Xtender = _serialcomm.SerialRead(ref _xtenderStruct);

            // throw new NotImplementedException(); // this is new version
            this.Invoke(new EventHandler(_Xtender_DisplayText)); // old version and worked before
        }

        private void _Xtender_DisplayText(object sender, EventArgs e)
        {
            byte[] XtenderReceivedFrame = _serialcomm.SerialDataTidyUp(ref _xtenderStruct); // data clean up is necassary
            // do what necassarly will ........
            // we get the frame (raw) from Xtender and Write to DataBase now ..................
            //public List<float> XtenderDataRendering(byte[] XtenderReceivedFrame)
            // first, luckly all Read From Xtender is FLOAT ....... In future may do and write Xtender write value in the DataBase, but nit NOW !!!
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            // Disable all Disconnection and Stop ButtonsFexcel
            Connect_Xtender.Enabled = true;
            Disconnect_Xtender.Enabled = false;
            //// OTHERS
            Connect_Ammonit.Enabled = false;
            Connect_ModBus4Noks.Enabled = false;
            Connect_RemoteCOMM.Enabled = false;
            //
            Disconnect_Ammonit.Enabled = false;
            Disconnect_ModBus4Noks.Enabled = false;
            Disconnect_RemoteCOMM.Enabled = false;
            // ALGORITHM
            Start_Algorithm.Enabled = false;
            Stop_Algorithm.Enabled = false;
            

            // Start the LOGGING !!!
            _log.LoggingStart();//sadece ilk program baslarken cagirilacak..

            // Call all Active ComPort Names for Xtender
            _xtenderStruct._GetPortNames = SerialPort.GetPortNames(); // will use in almost everywhere

            // fill all ComboBox using available ComPorNames
            _serialcomm.ComboBoxComPortNameFilling(_xtenderStruct._GetPortNames, ComboBox_Xtender);
            
            // Excell Call and USE
            _exceluse.ExcelCall("xtender_write_komutlari.xlsx", ref _excelStruct);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            /********* Close all COM ports and Stop Algorithm ****************/
            // Close Xtender Serial Port
            // Portu kapatalim
            _serialcomm.SerialClose(ref _xtenderStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            /*****/
        }

        private void Connect_Xtender_Click(object sender, EventArgs e)
        {
            // Port name felan Combodan cekilmekte ve asagida SerialComm icerisinde kullanilmaktadir !!!
            XtenderClass.XtenderComPortSettings(ref _xtenderStruct, ref _xtenderSerial, ComboBox_Xtender); // oncelikle struct icerisine gereken degerler SerialComm oncesionde cekiliyor. 
            //SerialComm icerisinde port ayarlari ise simdi yapilabilir. 
            _serialcomm.ComPortSettings(ref _xtenderStruct); // boylece _XtenderSerialPort ayarlari tamamlanmis oldu. (SerialComm ile genel sekilde halledilebildi)
            // Portu acip kullanmaya baslayalim...
            _serialcomm.SerialOpen(ref _xtenderStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            /*****/
            Connect_Xtender.Enabled = false;
            Disconnect_Xtender.Enabled = true;
            ///////////////////
            // ALGORITHM
            Start_Algorithm.Enabled = true;
            Stop_Algorithm.Enabled = false;
            
        }

        private void Connect_ModBus4Noks_Click(object sender, EventArgs e)
        {
            // Open 4Noks Serial Port
        }

        private void Connect_RemoteCOMM_Click(object sender, EventArgs e)
        {
            // Open RemoteCOMM Serial Port
        }

        private void Disconnect_Xtender_Click(object sender, EventArgs e)
        {
            // Close Xtender Serial Port
            // Portu acip kullanmaya baslayalim...
            _serialcomm.SerialClose(ref _xtenderStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!
            /*****/
            Connect_Xtender.Enabled = true;
            Disconnect_Xtender.Enabled = false;
        }

        private void Disconnect_ModBus4Noks_Click(object sender, EventArgs e)
        {
           // Close 4Noks Serial port
        }

        private void Disconnect_RemoteCOMM_Click(object sender, EventArgs e)
        {
            // Close RemoteCOMM Serial Port
        }

        private void Connect_Ammonit_Click(object sender, EventArgs e)
        {

        }

        private void Disconnect_Ammonit_Click(object sender, EventArgs e)
        {

        }

        private void Start_Algorithm_Click(object sender, EventArgs e)
        {
            // Start Timer (every 60 second will triggered)
            TimerAlgorithm.Enabled = true; // Call the algorithm in the Timer 
            Start_Algorithm.Enabled = false; // close button activity
        }

        private void Stop_Algorithm_Click(object sender, EventArgs e)
        {
            // Stop Algorithm (Timer etc)
            TimerAlgorithm.Enabled = false; 
            Start_Algorithm.Enabled = true; // start again button activity
        }

        private void TimerAlgorithm_Tick(object sender, EventArgs e)
        {
            //(every 5 second will triggered) // istenildigi gibi degistirilebilir... Simdilik 5 sec
            // Call Algorithms every tick

            AlgorithmClass.AlgorithmStarting(ref  _xtenderStruct, ref _excelStruct); // Sira ile hangisi eklenirse buraya eklenecek !!!
            // do lots of things in the algorithm class   
        }

    
    }
 
}
