using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
// in this class, we defined all necessary sub function getting data from serial port for RemoteComm

namespace WindowsFormsApplication1
{
    class RemoteComm
    {
        // burada RemoteComm sanal seri portuna basilacak degisken uzunluklu veriyi daha dogrusu diziyi hazirliyoz
        // State= 1 ise read icin 2 ise write icin donen cevap oldugu anlasilacak 
        // FamilyName= Xtender, ZigBee4Noks vb... 1 gibi gidiyor
        // Bir family'nin birden fazla uyesi varsa 4noks gibi kullanacaz. Birada 4noks modem ID'si 1 ayni Xtender'inkinin de 1 oldugu gibi
        // ObjectID ise okunan datanin kodu.. Bu akim degeri mi, role durumu mu vs... hepsine bir kod verecez. mesela Xtender icin 3000 batarya voltaji bunu kullanacaz. Ancak 4Noks role mesela 5 olsun kodu gibi....
        // VALUE ise bize gelen formatinin ne oldugu onemsiz veri... Ona gore RemoteComm frame'ini hazirlayacaz

        public List<byte> RemoteCommWriteFrame(UInt16 State, UInt16 FamilyName, UInt16 FamilyMemberID, UInt32 ObjectID, float VALUE)
        {
            // Burada oncelikle read data framesi (30byte) ayarlayip gonderecegiz
            Dictionary<uint, List<byte>> XtenderCreateReadFrame = new Dictionary<uint, List<byte>>();
            List<byte> command_RemoteCommWriteFrame = new List<byte>();
            //
            command_RemoteCommWriteFrame.Clear(); // temizleyelim once
            ////////

            ///Start first byte, STATE (1byte)
            command_RemoteCommWriteFrame.Add((byte)State);

            ///FamilyName (1byte)
            command_RemoteCommWriteFrame.Add((byte)FamilyName);

            ///FamilyMemberID (1byte)
            command_RemoteCommWriteFrame.Add((byte)FamilyMemberID);

            ///ObjectID (4byte)
            for (uint i = 0; i < (BitConverter.GetBytes(ObjectID).Length); i++)
            {
                command_RemoteCommWriteFrame.Add(BitConverter.GetBytes(ObjectID)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
            }

            ///VALUE-DATA (4byte)
            for (uint i = 0; i < (BitConverter.GetBytes(VALUE).Length); i++)
            {
                command_RemoteCommWriteFrame.Add(BitConverter.GetBytes(VALUE)[i]); // datayi (muhtemelen 4 byte hale getirip bura yazdirdik)
            }

            return command_RemoteCommWriteFrame;
        }

        public void RemoteCommSerialOpen(SerialPort RemoteCommSerialPortObject, string PortNames) // This value comes from Form opening time
        {

            RemoteCommSerialPortObject.PortName = PortNames;
            RemoteCommSerialPortObject.BaudRate = 9600;//  Xtendar data rate
            RemoteCommSerialPortObject.DataBits = 8;//int formatinda yazdiriliyo
            RemoteCommSerialPortObject.StopBits = System.IO.Ports.StopBits.One;
            RemoteCommSerialPortObject.Parity = System.IO.Ports.Parity.None;
            RemoteCommSerialPortObject.ReadTimeout = 3000;
            RemoteCommSerialPortObject.WriteTimeout = 4000;
            try
            {
                if (RemoteCommSerialPortObject.IsOpen)
                {
                    RemoteCommSerialPortObject.Close();
                }
                RemoteCommSerialPortObject.Open();


            }
            catch (Exception exc)
            {
                //
            }
        }

        public void RemoteCommSerialClose(SerialPort RemoteCommSerialPortObject, string PortNames) // This value comes from Form opening time
        {

            try
            {
                if (!RemoteCommSerialPortObject.IsOpen)
                {
                    RemoteCommSerialPortObject.Open();
                }
                RemoteCommSerialPortObject.Close();


            }
            catch (Exception exc)
            {
                //using (StreamWriter w = File.AppendText("log.txt"))
                //{
                //   // Logging.Log("Couldn't close Xtender serial port, will try again", w);
                //}
            }
        }


    }
}
