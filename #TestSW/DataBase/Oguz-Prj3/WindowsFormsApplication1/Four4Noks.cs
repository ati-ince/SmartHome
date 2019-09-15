using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
// in this class, we defined all necessary sub function getting data from serial port for Four4Noks Modbus-RTU Serial Communication

namespace WindowsFormsApplication1
{
    class Four4Noks
    {
        public List<byte> ModbusPollingF(UInt16 AddressForCheck, UInt16 Command, UInt16 valueX) // Ana hatlari ile network uzerinde sorgu yapilma fonksiyonu bu olacak... 
        {
            // Burada ornegin network'e kimler bagli? Tum networkte cesitli data okuma vs.. hepsi bununla yapilacak...
            // Yazilimin bir yerinde bir Union tanimlayip olasi Unicast Query islerini toplayalim... Daha sonrasinda elbette !!!
            // Simdilik network'teki uyelerin adresini ogrenmeye calistigimiz icun ne gelirse gelsin 0 olsun degeri sadece ilk ise odaklanalim
            List<byte> ReturnFrame = new List<byte>(); ReturnFrame.Clear(); // temisle, bunu kullanacaz hazirlanan Frame vs icun !!!
            ///////////////////////////////////////////////////////////
            List<byte> outpolling = new List<byte>(); outpolling.Clear(); // temisle
            outpolling.Add(0x01);
            //
            if (Command == 4) // aktif guc
            {
                outpolling[0] = Convert.ToByte(AddressForCheck);
                outpolling.Add(0x04);
                outpolling.Add(0x00);
                outpolling.Add(0x05);
                outpolling.Add(0x00);
                outpolling.Add(0x01);
                ReturnFrame = outpolling;
            }

            else if (Command == 5) // aktif guc ?????? // role ac kapa yapacaz, 1 ve 2 kullanilacak !!!!
            {
                outpolling[0] = Convert.ToByte(AddressForCheck);
                outpolling.Add(0x05);
                outpolling.Add(0x00);
                outpolling.Add((byte)valueX);
                outpolling.Add(0xFF);
                outpolling.Add(0x00);
                ReturnFrame = outpolling;
            }

            // role acmak icun
            //param2.Text = "05";
            //param3.Text = "00";
            //param4.Text = "01";
            //param5.Text = "255";
            //param6.Text = "00";


            // role kapatmak icin
            //param2.Text = "05";
            //param3.Text = "00";
            //param4.Text = "02";
            //param5.Text = "255";
            //param6.Text = "00";

            return ReturnFrame;
        }

        public bool CRC16(int datalength, bool check, ref List<byte> data)
        {
            int checksum;
            byte lowCRC;
            byte highCRC;
            int i, j;
            checksum = 0xffff;

            for (j = 0; j < datalength; j++)
            {
                checksum = checksum ^ ((int)data[j]);
                for (i = 8; i > 0; i--)
                {
                    if (checksum % 2 == 1)
                    {
                        checksum = (checksum >> 1) ^ 0xa001;
                    }
                    else
                    {
                        checksum >>= 1;
                    }
                }
            }


            highCRC = (byte)(checksum >> 8);
            checksum <<= 8;
            lowCRC = (byte)(checksum >> 8);

            if (true == check)
            {
                if ((data[datalength + 1] == highCRC) && (data[datalength] == lowCRC))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                data.Add(lowCRC);
                data.Add(highCRC);
            }
            return true;
        }

        public void Z4NoksSerialOpen(SerialPort Four4NoksSerialPortObject, string PortNames) // This value comes from Form opening time
        {

            Four4NoksSerialPortObject.PortName = PortNames;
            Four4NoksSerialPortObject.BaudRate = 9600;//  Xtendar data rate
            Four4NoksSerialPortObject.DataBits = 8;//int formatinda yazdiriliyo
            Four4NoksSerialPortObject.StopBits = System.IO.Ports.StopBits.One;
            Four4NoksSerialPortObject.Parity = System.IO.Ports.Parity.None;
            Four4NoksSerialPortObject.ReadTimeout = 400;
            Four4NoksSerialPortObject.WriteTimeout = 50;
            try
            {
                if (Four4NoksSerialPortObject.IsOpen)
                {
                    Four4NoksSerialPortObject.Close();
                }
                Four4NoksSerialPortObject.Open();


            }
            catch (Exception exc)
            {
                //using (StreamWriter w = File.AppendText("log.txt"))
                //{
                //    //Logging.Log("Couldn't open Xtender serial port, will try again", w);
                //}
            }
        }

        public void Z4NoksSerialClose(SerialPort Four4NoksSerialPortObject, string PortNames) // This value comes from Form opening time
        {

            try
            {
                if (!Four4NoksSerialPortObject.IsOpen)
                {
                    Four4NoksSerialPortObject.Open();
                }
                Four4NoksSerialPortObject.Close();


            }
            catch (Exception exc)
            {
                //using (StreamWriter w = File.AppendText("log.txt"))
                //{
                //    // Logging.Log("Couldn't close Xtender serial port, will try again", w);
                //}
            }
        }




    }
}
