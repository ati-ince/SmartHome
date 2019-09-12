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
        // serial port object and data arrays
        //_SerialPort4SerialCOMM
        SerialPort _SerialPort4SerialCOMM = new SerialPort();
        ////////////
        // Burada gelen parca datalari bir sekilde birlestirecegiz. Parca parca olmamasi icin tamamainin.  
        public byte[] SerialDataTidyUp(byte[] SerialData)
        {
            byte[] SerialRegularData;
            // 
            SerialRegularData = SerialData; // Bu kisimda ise yapilcak islemler...
            //
            return SerialRegularData;
        }

        // Buraya Seriali actiktan sonra yazilacak ozellikleri de ekleyelim, Baudrate vb... iste alttaki sirasi ile 
        public void SerialOpen(string PortNames) // This value comes from Form opening time
        {
            _SerialPort4SerialCOMM.PortName = PortNames;
            _SerialPort4SerialCOMM.BaudRate = 38400;//  Xtendar data rate
            _SerialPort4SerialCOMM.DataBits = 8;//int formatinda yazdiriliyo
            _SerialPort4SerialCOMM.StopBits = System.IO.Ports.StopBits.One;
            _SerialPort4SerialCOMM.Parity = System.IO.Ports.Parity.Even;
            _SerialPort4SerialCOMM.ReadTimeout = 3000;
            _SerialPort4SerialCOMM.WriteTimeout = 4000;
            try
            {
                if (_SerialPort4SerialCOMM.IsOpen)
                {
                    _SerialPort4SerialCOMM.Close();
                }
                _SerialPort4SerialCOMM.Open();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    // Logging.Log("Couldn't open Xtender serial port, will try again", w);
                }
            }
        }

        public void SerialClose(string PortNames) // This value comes from Form opening time
        {
            _SerialPort4SerialCOMM.PortName = PortNames;
            _SerialPort4SerialCOMM.BaudRate = 38400;//  Xtendar data rate
            _SerialPort4SerialCOMM.DataBits = 8;//int formatinda yazdiriliyo
            _SerialPort4SerialCOMM.StopBits = System.IO.Ports.StopBits.One;
            _SerialPort4SerialCOMM.Parity = System.IO.Ports.Parity.Even;
            _SerialPort4SerialCOMM.ReadTimeout = 3000;
            _SerialPort4SerialCOMM.WriteTimeout = 4000;

            try
            {
                if (!_SerialPort4SerialCOMM.IsOpen)
                {
                    _SerialPort4SerialCOMM.Open();
                }
                _SerialPort4SerialCOMM.Close();


            }
            catch (Exception exc)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Logging.Log("Couldn't close Xtender serial port, will try again", w);
                }
            }
        }


        // Bunu da tamami icin kullanacagiz. Ama burada port ismi de eklenecek
        //SerialCOMM icerisinde SerialWrite icerisine ayrica hangi  SerialObject yani COM portuna hangi array yazdirilacak onu da ekliyoruz. 
        public void SerialWrite(byte[] SendingData)
        {
            _SerialPort4SerialCOMM.Write(SendingData.ToArray(), (int)0, (int)SendingData.Count());

        }
        
     
    }
}
