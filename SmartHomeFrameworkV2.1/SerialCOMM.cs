using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace SmartHomeFrameworkV2._1
{
    public class SerialCOMM
    {

        /// <summary>
        /// STRUCTS
        /// </summary>
        
        // All information about serial port are inside evn SerialPort Object referance/address....
        public struct StandardSerialComStruct
        {
            public string[] _GetPortNames; // get port name from Windows
            /////////////
            public SerialPort _SerialPortGeneralObj; // may be we use
            //
            public SerialPort _XtenderSerialPort;
            public SerialPort _ModBus4NoksSerialPort;
            public SerialPort _RemoteCOMMSerialPort;

            ///////////// Collect All SerialComm devices Struct
            public ComPortStruct StructGeneralComPort; // may be we use
            //
            public ComPortStruct StructXtender;
            public ComPortStruct StructModBus4Noks;
            public ComPortStruct StructRemoteCOMM;

            ///////////// in which port active now? (we can fill which is active right time !!!)
            public bool IsXtenderWrite ; // if we wanna write using this port .....
            public bool IsModBus4NoksWrite ;
            public bool IsRemoteCOMMWrite ;
            //
            public bool IsXtenderReceive; // if we wanna receive data from this port ....
            public bool IsModBus4NoksReceive;
            public bool IsRemoteCOMMReceive;
        }
        /******************************************************************************************************/

        public struct ComPortStruct // Information from All ComPorts will use this struct
        {
            public string PortName;
            public int BaudRate;
            public int DataBits;
            public System.IO.Ports.StopBits StopBits;
            public System.IO.Ports.Parity Parity;
            public int ReadTimeout;
            public int WriteTimeout;
            //
            public byte[] DataFrameRead; // will use all reading data inside
            public byte[] DataFrameWrite; // will use all writing data inside
            //
            public string[] SendingTimeFrames; // serialPort Sending Local time collector
            public string[] ReceivingTimeFrames; // serialPort Receiving Local Time Collector

        }
        /******************************************************************************************************/

        /// <summary>
        /// METHODS
        /// </summary>

        // fill the comboBox with available comPort Names
        public void ComboBoxComPortNameFilling(string[] getportnames, System.Windows.Forms.ComboBox comboBox)
        {
            foreach (string port in getportnames) // see all existed port names
            {
                comboBox.Items.Add(port); // normally we use with "this" like this.comboBox ......
            }
        }
        /******************************************************************************************************/

        ///// ComBox GetSelectedComPortNames ////////////////////
        public string GetSelectedPortNamesFromComboBox(ref StandardSerialComStruct StandartStruct_get, System.Windows.Forms.ComboBox comboBox)
        {
            SByte i_ComPorts = Convert.ToSByte( comboBox.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = StandartStruct_get._GetPortNames[i_ComPorts]; // detect which is selected
          
          return PortName;
        }
        /******************************************************************************************************/

        // public void SerialOpen(ref StandardSerialComStruct StandartStruct, ref SerialPort __SerialPortUse)
        public void SerialOpen(ref SerialPort __SerialPort_open)
         {

            try
            {
                if (__SerialPort_open.IsOpen)
                {
                    __SerialPort_open.Close();
                }
                __SerialPort_open.Open();
            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    // Logging.Log("Couldn't open Xtender serial port, will try again", w);
                }
            }
        }
        /******************************************************************************************************/

        public void SerialClose(ref SerialPort __SerialPort_close) 
        {                  
            try
            {
                if (!__SerialPort_close.IsOpen)
                {
                    __SerialPort_close.Open();
                }
                __SerialPort_close.Close();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    //Logging.Log("Couldn't close Xtender serial port, will try again", w);
                }
            }
        }
        /******************************************************************************************************/

        public void SerialWrite(ref SerialCOMM.StandardSerialComStruct sStandardStr_SerWr)
        {
            sStandardStr_SerWr._SerialPortGeneralObj.Open();// may be look closed, you never know !!!
            sStandardStr_SerWr._SerialPortGeneralObj.Write(sStandardStr_SerWr.StructGeneralComPort.DataFrameWrite.ToArray(), (int)0, (int)sStandardStr_SerWr.StructGeneralComPort.DataFrameWrite.Count());  
            ////////////////
            //if (sStandardStr_SerWr.IsXtenderWrite == true)
            //{
            //    sStandardStr_SerWr._XtenderSerialPort.Open();
            //}
            //if (sStandardStr_SerWr.IsModBus4NoksWrite == true)
            //{
            //    sStandardStr_SerWr._ModBus4NoksSerialPort.Open();
            //}
            //if (sStandardStr_SerWr.IsRemoteCOMMWrite == true)
            //{
            //    sStandardStr_SerWr._RemoteCOMMSerialPort.Open();
            //}
        }
        /******************************************************************************************************/

        public int SerialRead(ref SerialCOMM.StandardSerialComStruct sStandardStr_SerRead)
        {
            int readRespond;
            sStandardStr_SerRead._SerialPortGeneralObj.Open();// may be look closed, you never know !!!
            readRespond = sStandardStr_SerRead._SerialPortGeneralObj.Read(sStandardStr_SerRead.StructGeneralComPort.DataFrameRead.ToArray(), (int)0, sStandardStr_SerRead._SerialPortGeneralObj.BytesToRead);
        return readRespond;
        }
        /******************************************************************************************************/

        // Coolect all part of serial comm data and tidy up and do compact 
        public byte[] SerialDataTidyUp(ref SerialCOMM.StandardSerialComStruct sStandarStr)
        {
            byte[] SerialRegularData;
            // 
            SerialRegularData = sStandarStr.StructGeneralComPort.DataFrameRead; // Do somthings...........................
            //
            return SerialRegularData;
        }
        // **********************************************************//

       
        
     
    }
}
