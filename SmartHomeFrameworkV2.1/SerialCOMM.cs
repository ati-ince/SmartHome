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
            //
            public SerialPort SerialPortGeneralObj;

            // Collect All SerialComm devices Struct
            public ComPortStruct StructGeneralComPort;
            //
            public ComPortStruct StructXtender;
            public ComPortStruct StructModBus4Noks;
            public ComPortStruct StructRemoteCOMM;
            
            // which port active now? (we can fill which is active right time !!!)
            public bool IsXtenderUsing ;
            public bool IsModBus4NoksUsing ;
            public bool IsRemoteCOMMIsUsing ;
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
            public List<byte> DataFrameRead; // will use all reading data inside
            public List<byte> DataFrameWrite; // will use all writing data inside
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
        public string GetSelectedPortNamesFromComboBox(ref StandardSerialComStruct StandartStruct, System.Windows.Forms.ComboBox comboBox)
        {
            SByte i_ComPorts = Convert.ToSByte( comboBox.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = StandartStruct._GetPortNames[i_ComPorts]; // detect which is selected
          
          return PortName;
        }
        /******************************************************************************************************/

        // public void SerialOpen(ref StandardSerialComStruct StandartStruct, ref SerialPort __SerialPortUse)
        public void SerialOpen(ref SerialPort __SerialPortUse)
         {

            //__SerialPortUse.PortName = StandartStruct.StructGeneralComPort.PortName; ; // string
            //__SerialPortUse.BaudRate = StandartStruct.StructGeneralComPort.BaudRate;//  int
            //__SerialPortUse.DataBits = StandartStruct.StructGeneralComPort.DataBits;//int
            //__SerialPortUse.StopBits = StandartStruct.StructGeneralComPort.StopBits; // System.IO.Ports.StopBits
            //__SerialPortUse.Parity = StandartStruct.StructGeneralComPort.Parity;  // System.IO.Ports.Parity
            //__SerialPortUse.ReadTimeout = StandartStruct.StructGeneralComPort.ReadTimeout; // int
            //__SerialPortUse.WriteTimeout = StandartStruct.StructGeneralComPort.WriteTimeout; // int
            try
            {
                if (__SerialPortUse.IsOpen)
                {
                    __SerialPortUse.Close();
                }
                __SerialPortUse.Open();
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

        public void SerialClose(ref SerialPort __SerialPortUse) // This value comes from Form opening time // This value comes from Form opening time
        {                  
            try
            {
                if (!__SerialPortUse.IsOpen)
                {
                    __SerialPortUse.Open();
                }
                __SerialPortUse.Close();


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

        public void SerialWrite(ref SerialCOMM.StandardSerialComStruct standartSerialPortStruct, ref SerialPort __SerialPortUse)
        {
            __SerialPortUse.Open(); // may be look closed, you never know !!!
            __SerialPortUse.Write(standartSerialPortStruct.StructGeneralComPort.DataFrameWrite.ToArray(), (int)0, (int)standartSerialPortStruct.StructGeneralComPort.DataFrameWrite.Count());
        }
        /******************************************************************************************************/

        public int SerialRead(ref SerialCOMM.ComPortStruct comportstruct_serialOpenClose, ref SerialPort __SerialPortUse)
        {
            int readRespond;
            __SerialPortUse.Open(); // may be look closed, you never know !!!
            readRespond = __SerialPortUse.Read(comportstruct_serialOpenClose.DataFrameRead.ToArray(), (int)0, __SerialPortUse.BytesToRead );
        return readRespond;
        }
        /******************************************************************************************************/

        // Coolect all part of serial comm data and tidy up and do compact 
        public List<byte> SerialDataTidyUp(ref SerialCOMM.StandardSerialComStruct sStandarStr)
        {
            List<byte> SerialRegularData;
            // 
            SerialRegularData = sStandarStr.StructGeneralComPort.DataFrameRead; // Do somthings...........................
            //
            return SerialRegularData;
        }
        // **********************************************************//

       
        
     
    }
}
