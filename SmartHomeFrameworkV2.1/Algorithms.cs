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

        Ammonit Ammonit4Algorithm= new Ammonit(); // for algorithm
        SerialCOMM SerialCOMM4Algorithm = new SerialCOMM(); // we use for sending data via SerialCOMM class
        DataBaseSQL DataBaseSQL4Algorithm = new DataBaseSQL();
        Xtender Xtender4Algorithm = new Xtender(); // we use for algorithm ...


        /// <summary>
        /// GLOBAL STATICS VARIABLES (WHICH IS ACTIVE OR PASSIVE)
        /// </summary>
        /// 
        public bool _AmmonitState = true; // we are using now
        public bool _XtenderState = true; // we are using now
        public bool _ModBus4NoksState = false;


        /*************************************************/
        public bool AlgorithmStarting(ref SerialPort _xtenderPort, ref SerialCOMM.StandardSerialComStruct StandarStruct) // public calling algorithm
        { 
        
        bool stateAlgorithm= true;
        // Call main algorithms
        //
        Algorithm1(ref _xtenderPort, ref StandarStruct); // do somethings ..................
        //
        //
        return stateAlgorithm;
        }



        /*************************************************/
        private void Algorithm1(ref SerialPort _xtenderPort, ref SerialCOMM.StandardSerialComStruct StandarStruct) // private Algorithm Method1
        { 
           
            // now just save Ammonit Sensors data to database
            // check all hardware which is active or not .........

            if (_AmmonitState == true)
            {
                // Call All Ammonit Sensor Data and Write to dataBase
                Ammonit4Algorithm.Ammonit_AddAllTo_DataBase(Ammonit4Algorithm.GetAmmonitData("169.254.36.137", 40500, 16));
            }

            if (_XtenderState == true)
            {
                // Activate Xtender Serial into General Struct for data comm for SerialWrite...
                StandarStruct.IsXtenderUsing = true; // we understandar , yes now we will use right away ........
                /*#### READ all necessaries  #####*/
                
                // Get all reg addr from xtender excelll .... 

               //3000	battery voltage
                Xtender4Algorithm.XtenderSendReadData((UInt16)3000, ref StandarStruct, ref _xtenderPort);
               //3001	battery temp
                Xtender4Algorithm.XtenderSendReadData((UInt16)3001, ref StandarStruct, ref _xtenderPort);
               //3007	State of charge
                Xtender4Algorithm.XtenderSendReadData((UInt16)3007, ref StandarStruct, ref _xtenderPort);
               //3021	Output voltage AC-out
                Xtender4Algorithm.XtenderSendReadData((UInt16)3021, ref StandarStruct, ref _xtenderPort);
               //3087	output active power
                Xtender4Algorithm.XtenderSendReadData((UInt16)3087, ref StandarStruct, ref _xtenderPort);
               //3088	input active power
                Xtender4Algorithm.XtenderSendReadData((UInt16)3088, ref StandarStruct, ref _xtenderPort);
               //9004	output power of the PV
               //9013	sum of daily energy of PV
               //7002	State of Charge (GENERAL)
            
            // the finish to send over xtender serial com, then deactivate info into StandartSerialComm struct
                StandarStruct.IsXtenderUsing = false; // with this, we will know which will be use ....
            }

        
        }



    }
}
