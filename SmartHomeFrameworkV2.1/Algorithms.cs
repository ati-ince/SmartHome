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
    public class Algorithms
    {

        /// <summary>
        /// GLOBAL CLASSES
        /// </summary>
        //SerialPort _XtenderSerial = new SerialPort(); // define the Xtender Serial Port Object
        //SerialPort _RemoteCommSerial = new SerialPort(); // RemoteComm Virtual 
        //SerialPort _4NoksSerial = new SerialPort(); // 4Noks  Modbus Serial RTU
        Ammonit Ammonit4Algorithm= new Ammonit(); // for algorithm
        //DataBaseSQL _DataBaseSQL = new DataBaseSQL();
        //Algorithms _Algorithms = new Algorithms(); // Algorithms and Variables


        /// <summary>
        /// GLOBAL STATICS VARIABLES (WHICH IS ACTIVE OR PASSIVE)
        /// </summary>
        /// 
        public bool _AmmonitState = false;
        public bool _XtenderState = false;
        public bool _ModBus4NoksState = false;


        /*************************************************/
        public bool AlgorithmStarting() // public calling algorithm
        { 
        
        bool stateAlgorithm= true;
        // Call main algorithms
        //
        Algorithm1(); // do somethings ..................
        //
        //
        return stateAlgorithm;
        }



        /*************************************************/
        private void Algorithm1() // private Algorithm Method1
        { 
           
            // now just save Ammonit Sensors data to database
            // check all hardware which is active or not .........

            if (_AmmonitState == true)
            {
                // Call All Ammonit Sensor Data and Write to dataBase
                Ammonit4Algorithm.Ammonit_AddAllTo_DataBase(Ammonit4Algorithm.GetAmmonitData("169.254.36.137", 40500, 16));
            }

        
        }



    }
}
