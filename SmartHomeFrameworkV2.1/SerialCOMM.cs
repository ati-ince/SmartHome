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

            // Collect All SerialComm devices Struct
            public  ComPortStruct StructXtender;
            //4Noks and more....

            /*Write SerialPorts*/
            public  void xSrial(ref SerialPort _xSer)
            {
                StructXtender.PortName = _xSer.PortName;
            }
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
        public void ComboBoxComPortNameFilling(string[] GetPortNames, System.Windows.Forms.ComboBox comboBox)
        {
            foreach (string port in GetPortNames) // see all existed port names
            {
                comboBox.Items.Add(port); // normally we use with "this" like this.comboBox ......
            }
        }
        /******************************************************************************************************/


        ///// ComBox GetSelectedComPortNames ////////////////////
        public string GetSelectedPortNamesFromComboBox(string[] GetPortNames, System.Windows.Forms.ComboBox comboBox)
        {
            SByte i_ComPorts = Convert.ToSByte( comboBox.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = GetPortNames[i_ComPorts]; // detect which is selected
          
          return PortName;
        }
        /******************************************************************************************************/

        public void SerialOpen(ref SerialCOMM.ComPortStruct comportstruct_serialOpenClose, ref SerialPort __SerialPortUse) // This value comes from Form opening time
        {
            __SerialPortUse.PortName = comportstruct_serialOpenClose.PortName; // string
            __SerialPortUse.BaudRate = comportstruct_serialOpenClose.BaudRate;//  int
            __SerialPortUse.DataBits = comportstruct_serialOpenClose.DataBits;//int
            __SerialPortUse.StopBits = comportstruct_serialOpenClose.StopBits; // System.IO.Ports.StopBits
            __SerialPortUse.Parity = comportstruct_serialOpenClose.Parity;  // System.IO.Ports.Parity
            __SerialPortUse.ReadTimeout = comportstruct_serialOpenClose.ReadTimeout; // int
            __SerialPortUse.WriteTimeout = comportstruct_serialOpenClose.WriteTimeout; // int
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

        public void SerialClose(ref SerialCOMM.ComPortStruct comportstruct_serialOpenClose, ref SerialPort __SerialPortUse) // This value comes from Form opening time // This value comes from Form opening time
        {
            

            if (__SerialPortUse.IsOpen)
            {
                __SerialPortUse.Close(); // first close the port for change the port adrss which port need to change
                __SerialPortUse.PortName = comportstruct_serialOpenClose.PortName; // which port will close, write the adress
                __SerialPortUse.Open();// open the propoer com port for close
            }
            
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

        public void SerialWrite(ref SerialCOMM.ComPortStruct comportstruct_serialOpenClose, ref SerialPort __SerialPortUse)
        {
            __SerialPortUse.Open(); // may be look closed, you never know !!!
            __SerialPortUse.Write(comportstruct_serialOpenClose.DataFrameWrite.ToArray(), (int)0, (int)comportstruct_serialOpenClose.DataFrameWrite.Count());
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
        public byte[] SerialDataTidyUp(ref SerialCOMM.ComPortStruct comportstruct_serialOpenClose)
        {
            byte[] SerialRegularData;
            // 
            SerialRegularData = comportstruct_serialOpenClose.DataFrameRead; // Do somthings...........................
            //
            return SerialRegularData;
        }
        // **********************************************************//

       
        
     
    }
}
