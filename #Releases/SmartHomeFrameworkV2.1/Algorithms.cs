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
    class Algorithms:SerialCOMM
    {

        /// <summary>
        /// GLOBAL CLASSES
        /// </summary>

        Ammonit Ammonit4Algorithm = new Ammonit(); // for algorithm
        SerialCOMM SerialCOMM4Algorithm = new SerialCOMM(); // we use for sending data via SerialCOMM class
        DataBaseSQL DataBaseSQL4Algorithm = new DataBaseSQL();
        Xtender Xtender4Algorithm = new Xtender(); // we use for algorithm ...
        // Test
        Algorithms _algTest = new Algorithms();

        /// <summary>
        /// GLOBAL STATICS VARIABLES (WHICH IS ACTIVE OR PASSIVE)
        /// </summary>
        /// 
        public bool _AmmonitState = true; // we are using now
        public bool _XtenderState = true; // we are using now
        public bool _ModBus4NoksState = false;

        public static uint XtenderReadRegListIndex = 0;

        /*************************************************/
        public bool AlgorithmStarting(ref ComPortStruct _xtenderComPortStr, ref ExcelUsege.ExcelStruct _excelStruct) // public calling algorithm 
        {
            List<String> AlgorithmList = new List<String>(); AlgorithmList.Clear();
            AlgorithmList.Add("Algorithm1");
            /********/
            bool stateAlgorithm = true;
            // Call main algorithms
            //
            Algorithm1(ref  _xtenderComPortStr,  ref _excelStruct); // do somethings ..................
            //
            //
            return stateAlgorithm;
        }


        /*************************************************/
        private void Algorithm1(ref ComPortStruct _xtenderComPortStr,  ref ExcelUsege.ExcelStruct _excelStruct) // private Algorithm Method1
        {

            // now just save Ammonit Sensors data to database
            // check all hardware which is active or not .........

            /******///(1)Ammonit 
            // Call All Ammonit Sensor Data and Write to dataBase
            Ammonit4Algorithm.Ammonit_AddAllTo_DataBase(Ammonit4Algorithm.GetAmmonitData("169.254.31.136", 40500, 16));

            /******///(2)Xtender 
            // Oncelikle READ yapilacak Xtender Listesini cekelim
            List<int> XtenderREADList = _excelStruct.ExcelReadList;
            //
            // XtenderSendReadData(UInt16 xRegAddr, ref ComPortStruct _comStruct)
            if (XtenderREADList.Count() >= 0)
            {
                Xtender4Algorithm.XtenderSendReadData((ushort)XtenderREADList.ToArray()[XtenderReadRegListIndex], ref _xtenderComPortStr);
                Logging2Txt("Xtender" + " " + "READ_REGISTER" + " " + "SENDING_FRAME: ", XtenderREADList.ToArray()[XtenderReadRegListIndex].ToString());
                XtenderReadRegListIndex++;

                if (XtenderReadRegListIndex == XtenderREADList.Count())
                {
                    XtenderReadRegListIndex = 0;
                }
            }

            
            //
            // Simdi bunun tamamini sorgulayip data baseye yazdiralim
            // Get all reg addr from xtender excelll .... 

        }


    }



}

