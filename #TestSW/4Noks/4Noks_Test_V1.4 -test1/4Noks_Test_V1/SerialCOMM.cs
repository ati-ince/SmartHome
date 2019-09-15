using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient; // SQL icin gereken girilmis
using System.Data;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace _4Noks_Test_V1
{
    class SerialCOMM : Logging // Always we need the Log what is going on , DataBase SQL
    {

        /// Globals
        
        ///////////////////////////////////////////

        public struct ComPortStruct // Information from All ComPorts will use this struct
        {
            public string[] _GetPortNames; // get port name from Windows
            public string PortINFO; // name etc (Xtender, 4Noks, Xbee et al)
            public bool Condition; // if write Condition=1, read Condition=0 we will know what is going on
            /////////////
            public SerialPort _SerialPortObj; // may be we use
            //
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
        Logging _log = new Logging();
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
        public string GetSelectedPortNamesFromComboBox(string[] getportnames, System.Windows.Forms.ComboBox comboBox)
        {
            SByte i_ComPorts = Convert.ToSByte( comboBox.SelectedIndex.ToString());
            if (i_ComPorts == (-1)) { i_ComPorts++; }
            string PortName = getportnames[i_ComPorts]; // detect which is selected
          
          return PortName;
        }
        /******************************************************************************************************/

        // public void SerialOpen(ref StandardSerialComStruct StandartStruct, ref SerialPort __SerialPortUse)
        public void SerialOpen(ref ComPortStruct _comportstr)
         {
            SerialPort __SerialPort_open = _comportstr._SerialPortObj; // struct icerisinden cektik.
            try
            {
                if (__SerialPort_open.IsOpen)
                {
                    __SerialPort_open.Close();
                }
                __SerialPort_open.Open();
                // log
                
                Logging2Txt(__SerialPort_open.PortName, _comportstr.PortINFO +" IsOpened!"); // This port now open !
            }
            catch (Exception exc)
            {
                // log
                Logging2Txt(__SerialPort_open.PortName, _comportstr.PortINFO + " Couldn'tOpened!"); // This port now open !
            
            }
        }
        /******************************************************************************************************/

        public void SerialClose(ref ComPortStruct _comportstr)
        {
            SerialPort __SerialPort_close = _comportstr._SerialPortObj; // struct icerisinden cektik.      
            try
            {
                if (!__SerialPort_close.IsOpen)
                {
                    __SerialPort_close.Open();
                }
                __SerialPort_close.Close();
                //
                Logging2Txt(__SerialPort_close.PortName, _comportstr.PortINFO + " IsClosed!"); // This port now open ! 

            }
            catch (Exception exc)
            {
                Logging2Txt(__SerialPort_close.PortName, _comportstr.PortINFO + " Couldn'tClosed!"); // This port now open ! 

            }
        }
        /******************************************************************************************************/

        // Only we need Struct for anything . _xtender, xbee etc....
        public void SerialWrite(ref ComPortStruct _comPortStr)
        {
            if (_comPortStr._SerialPortObj.IsOpen == false)
                _comPortStr._SerialPortObj.Open();// may be look closed, you never know !!!
            _comPortStr._SerialPortObj.Write(_comPortStr.DataFrameWrite.ToArray(), 0,_comPortStr.DataFrameWrite.Count() ); 
            //
            Logging2Txt("SerialWrite 4Noks", _comPortStr.PortName);

            int len = _comPortStr.DataFrameWrite.Length;

            Logging2Txt("SerialWrite 4Noks", "Frame = " + System.Text.Encoding.ASCII.GetString(_comPortStr.DataFrameWrite,0,len));
        }
        /******************************************************************************************************/

        // Only we need Struct for anything . _xtender, xbee etc....
        public int SerialRead(ref ComPortStruct _comPortStr)
        {
            int readRespond=0 ;
            byte[] buffXt = new byte[_comPortStr._SerialPortObj.BytesToRead];

            Logging2Txt("SERIAL_INFO" + "PortName: ", _comPortStr._SerialPortObj.PortName);
                
            readRespond = _comPortStr._SerialPortObj.Read(buffXt, (int)0, _comPortStr._SerialPortObj.BytesToRead);
            //
            _comPortStr.DataFrameRead = buffXt;

            // geleni de struct icerisine yazmistik.
            return readRespond;
        }
        /******************************************************************************************************/
       
        // Coolect all part of serial comm data and tidy up and do compact 
        // Simdilik bir sey yapmiyoruz, datanin duzgun geldigini farz ediyoruz. 
        public byte[] SerialDataTidyUp(ref ComPortStruct _comPortStr)
        {
            byte[] SerialRegularData;
            // 
            SerialRegularData = _comPortStr.DataFrameRead; // Do somthings...........................
            //
            return SerialRegularData;
        }
        // **********************************************************//

        // In this method, we set the SerialPort obj specs usimg particular SerialPort and its struch which is use for collection all info about that port. 
        public void ComPortSettings(ref ComPortStruct _comPortStr)
        {
            _comPortStr._SerialPortObj.PortName = _comPortStr.PortName;
            _comPortStr._SerialPortObj.BaudRate = _comPortStr.BaudRate;
            _comPortStr._SerialPortObj.DataBits = _comPortStr.DataBits;
            _comPortStr._SerialPortObj.StopBits = _comPortStr.StopBits;
            _comPortStr._SerialPortObj.Parity = _comPortStr.Parity;
            _comPortStr._SerialPortObj.ReadTimeout = _comPortStr.ReadTimeout;
            _comPortStr._SerialPortObj.WriteTimeout = _comPortStr.WriteTimeout;
        }

        /// BURALAR SONRA SILINEBILIR
        public string Arr2Str(byte[] arr)
        {
            string outp="";

            for (uint i = 0; i < arr.Length; i++)
            {
                outp = outp + arr[i].ToString() + " ";
            } 
            
            return outp;
        }
     
    } 
}
