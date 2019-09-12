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
        /// GLOBAL CLASSES
        /// </summary>
        SerialPort _XtenderSerial = new SerialPort(); // define the Xtender Serial Port Object
        //
        Ammonit Ammonit = new Ammonit(); // define Ammonit class to use
        DataBaseSQL DataBaseSQL = new DataBaseSQL(); // define database class to use
        Algorithms Algorithms = new Algorithms(); // Algorithms and Variables all are inside
        SerialCOMM SerialCOMM = new SerialCOMM(); // define SerialCOMM class to use
        Xtender Xtender = new Xtender();


        /// <summary>
        /// GLOBAL Struct
        /// </summary>
        SerialCOMM.StandardSerialComStruct StandardSerialComStruct = new SerialCOMM.StandardSerialComStruct();
        // we define main struct to use in any situations......

        /// <summary>
        /// GLOBAL VARIATIONS
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
            _XtenderSerial.DataReceived += new SerialDataReceivedEventHandler(_XtenderSerial_DataReceived); 

        }

        // this method may be change to old version of my code
        void _XtenderSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int Result_Xtender;
        // (#sil)   StandardSerialComStruct.StructXtender.DataFrameRead = new byte[_XtenderSerial.BytesToRead]; // Define Struct element size for read the serial data
            
            Result_Xtender = SerialCOMM.SerialRead(ref StandardSerialComStruct);

            // throw new NotImplementedException(); // this is new version
            this.Invoke(new EventHandler(_Xtender_DisplayText)); // old version and worked before
        }

        private void _Xtender_DisplayText(object sender, EventArgs e)
        {
            byte[] XtenderReceivedFrame = SerialCOMM.SerialDataTidyUp(ref StandardSerialComStruct); // data clean up is necassary
            // do what necassarly will ........
            // we get the frame (raw) from Xtender and Write to DataBase now ..................
            //public List<float> XtenderDataRendering(byte[] XtenderReceivedFrame)
            // first, luckly all Read From Xtender is FLOAT ....... In future may do and write Xtender write value in the DataBase, but nit NOW !!!
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            // Disable all Disconnection and Stop Buttons
            Stop_Algorithm.Enabled = false;
            Disconnect_Xtender.Enabled = false;
            Disconnect_ModBus4Noks.Enabled = false;
            Disconnect_RemoteCOMM.Enabled = false;
            Disconnect_Ammonit.Enabled = false;

            // Call all Active ComPort Names
            StandardSerialComStruct._GetPortNames = SerialPort.GetPortNames(); // will use in almost everywhere

            // fill all ComboBox using available ComPorNames
            SerialCOMM.ComboBoxComPortNameFilling(StandardSerialComStruct._GetPortNames, ComboBox_Xtender);
            
            // fill the XtenderExcell
            Xtender.FillTheExcel(DataGridViewXtenderExcel); // fill the excell in the Xtender Class;

            // all serial obj is passive now.... We use into Serial for Read and Write these.....
            StandardSerialComStruct.IsModBus4NoksWrite = false;
            StandardSerialComStruct.IsRemoteCOMMWrite = false;
            StandardSerialComStruct.IsXtenderWrite = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            /********* Close all COM ports and Stop Algorithm ****************/
            // Close Xtender Serial Port
            SerialCOMM.SerialClose(ref _XtenderSerial);
        }

        private void Connect_Xtender_Click(object sender, EventArgs e)
        {
            // in here, we get all COM port info from comBox (Xtender PortName)
            // and write all necassaries into _XtenderSerial
            Xtender.XtenderComPortSettings(ref _XtenderSerial, ref StandardSerialComStruct, ComboBox_Xtender); 

            // Open Xtender SerialPort
            SerialCOMM.SerialOpen(ref _XtenderSerial);// send the memory adress to acces real struct/

            //
            Connect_Xtender.Enabled = false;
            Disconnect_Xtender.Enabled = true;   
            ///////////////////

            
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
            SerialCOMM.SerialClose(ref _XtenderSerial);

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
            // Activate the Ammonit
            Algorithms._AmmonitState = true;
        }

        private void Disconnect_Ammonit_Click(object sender, EventArgs e)
        {
            // DeActivate the Ammonit
            Algorithms._AmmonitState = false; // with this Ammonit dont work in the Algorithm and never Call Ammonit Sensor Data
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
            //(every 60 second will triggered)
            // Call Algorithms every tick
            Algorithms.AlgorithmStarting(ref _XtenderSerial, ref StandardSerialComStruct);
            // do lots of things in the algorithm class   
        }

    
    }
 
}
