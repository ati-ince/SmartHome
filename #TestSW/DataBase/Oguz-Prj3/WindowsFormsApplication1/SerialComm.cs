using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{


    class SerialComm
    {
        SerialPort _SerialPort = new SerialPort();
        // serial port object and data arrays
        /// <summary>
        /// ////////////////////////////////////////////////////////////7


        public void XtenderSerialOpen(string PortNames) // This value comes from Form opening time
        {
            _SerialPort.PortName = PortNames;
            _SerialPort.BaudRate = 38400;//  Xtendar data rate
            _SerialPort.DataBits = 8;//int formatinda yazdiriliyo
            _SerialPort.StopBits = System.IO.Ports.StopBits.One;
            _SerialPort.Parity = System.IO.Ports.Parity.Even;
            _SerialPort.ReadTimeout = 3000;
            _SerialPort.WriteTimeout = 4000;
            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                }
                _SerialPort.Open();


            }
            catch (Exception exc)
            {
                //using (StreamWriter w = File.AppendText("log.txt"))
                //{
                //    Logging.Log("Couldn't open Xtender serial port, will try again", w);
                //}
            }
        }

        public void XtenderSerialClose(string PortNames) // This value comes from Form opening time
        {
            _SerialPort.PortName = PortNames;
            _SerialPort.BaudRate = 38400;//  Xtendar data rate
            _SerialPort.DataBits = 8;//int formatinda yazdiriliyo
            _SerialPort.StopBits = System.IO.Ports.StopBits.One;
            _SerialPort.Parity = System.IO.Ports.Parity.Even;
            _SerialPort.ReadTimeout = 3000;
            _SerialPort.WriteTimeout = 4000;

            try
            {
                if (!_SerialPort.IsOpen)
                {
                    _SerialPort.Open();
                }
                _SerialPort.Close();


            }
            catch (Exception exc)
            {
                //using (StreamWriter w = File.AppendText("log.txt"))
                //{
                //    Logging.Log("Couldn't close Xtender serial port, will try again", w);
                //}
            }
        }

        public void XtenderWrite(byte[] SendingData)
        {
            _SerialPort.Write(SendingData.ToArray(), (int)0, (int)SendingData.Count());

        }

    }
}
