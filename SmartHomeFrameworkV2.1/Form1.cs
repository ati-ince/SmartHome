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
        SerialPort _RemoteCommSerial = new SerialPort(); // RemoteComm Virtual 
        SerialPort _4NoksSerial = new SerialPort(); // 4Noks  Modbus Serial RTU
        //////////////////////////////////////////////////////////////////////////////////////
        Ammonit Ammonit = new Ammonit();
        DataBaseSQL DataBaseSQL = new DataBaseSQL();
        Algorithms Algorithms = new Algorithms(); // Algorithms and Variables

        /// <summary>
        /// GLOBAL VARIATIONS
        /// </summary>
        string[] _GetPortNames; // Active ComPort Names 
  

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
            _GetPortNames = System.IO.Ports.SerialPort.GetPortNames();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Bu program kapanirken eğer açık kalmışsa portu kapatıyor, baya guzel bir ozellik
        {
            // Close all COM ports and Stop Algorithm
        }

        private void Connect_Xtender_Click(object sender, EventArgs e)
        {
            // Open Xtender SerialPort
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
            // Start Timer 
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
            // Call Algorithms every tick
            Algorithms.AlgorithmStarting(); // do lots of things in the algorithm class

            
        }

    
    }
 
}
