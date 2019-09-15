using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;
//////////////////////////

namespace WindowsFormsApplication1
{
    class Ammonit
    {
        DataBaseSQL database = new DataBaseSQL();
        // Dis Dunyaya bir sekilde ulasmak icin statik STRING FRAME ekleniyor
        public static string OutWorld;
        //
        public static byte[] CommCome= new byte[1024];
        //
        public float AmmonitBytesToFloat(byte[] bytes, int startIndex)
        {
            byte[] tempArray = new byte[4];
            tempArray[0] = bytes[startIndex + 3];
            tempArray[1] = bytes[startIndex + 2];
            tempArray[2] = bytes[startIndex + 1];
            tempArray[3] = bytes[startIndex];

            return BitConverter.ToSingle(tempArray, 0);

        }
        //
        public string ComingStringFrame()
        {
            return OutWorld;
        }
        ///
        public byte[] GetAmmonitData(System.Windows.Forms.ListBox Listbox, ushort registerAddress, ushort numOfRegs)
        {
            byte[] bytes = new byte[1024];
            List<byte> lByteCommand = new List<byte>();
            
           
            //////////////////
            // Connect to a remote device.
            try
            {

                IPAddress ipAddress = IPAddress.Parse("169.254.36.137");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 502);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    //Debug.WriteLine("Socket connected to {0}",
                        //sender.RemoteEndPoint.ToString());
                    lByteCommand = PrepareAmmonitCommandMessage(registerAddress, numOfRegs);
                    // Encode the data string into a byte array.
                    byte[] msg = lByteCommand.ToArray();

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);
                    //Standard analog register read command is 12 bytes
                    if (12 != bytesSent)
                    {
                        //Debug.WriteLine("Couldn't send modbus analog register read command successfully!");
                    }
                    else
                    {
                        // Receive the response from the remote device.
                        int bytesRec = sender.Receive(bytes);
                        // geleni kopyalayalim ////
                        CommCome = bytes;
                        ///////
                        ///Response byte length is 
                        ///9 + (4 bytes register length x register num) bytes
                        if ((9 + numOfRegs * 4) != bytesRec)
                        {
                           // Debug.WriteLine("Couldn't receive modbus analog register read response successfully!");
                        }
                        else
                        {
                            string dataLine = DateTime.Now.ToLongTimeString() + "\t";

                            for (int regIndex = 0; regIndex < numOfRegs; regIndex++)
                            {
                                if (bytes.Length >= (9 + regIndex * 4 + 4))
                                {
                                    float val = AmmonitBytesToFloat(bytes, 9 + regIndex * 4);

                                    dataLine += val.ToString() + "\t";
                                }
                                else
                                {
                                    //Debug.WriteLine("There is a big problem dude!");
                                }
                            }
                            OutWorld = dataLine;
                            Listbox.Items.Add(dataLine);
                            Listbox.SelectedIndex = Listbox.Items.Count - 1;
                        }
                    }
                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    //Debug.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    //WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    //Debug.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.ToString());
            }
            return lByteCommand.ToArray();
        }
        ///
        public List<byte> PrepareAmmonitCommandMessage(ushort registerAddress, ushort numOfRegs)
        {
            List<byte> lByteCommand = new List<byte>();

            //Transaction identifier
            lByteCommand.Add(0x00);
            lByteCommand.Add(0x01);

            //Protocol identifier
            lByteCommand.Add(0x00);
            lByteCommand.Add(0x00);

            //Message Length
            lByteCommand.Add(0x00);
            lByteCommand.Add(0x06);

            //Unit identifier
            lByteCommand.Add(0x01);

            //Function code
            lByteCommand.Add(0x03);

            //Data address
            byte[] byteArrRegAddr = BitConverter.GetBytes(registerAddress);
            lByteCommand.Add(byteArrRegAddr[1]);
            lByteCommand.Add(byteArrRegAddr[0]);


            //Total number of registers bytes ( each register is 2 bytes)
            ushort numOfRegBytes = (ushort)(numOfRegs * 2);
            byte[] byteArrNumRegBytes = BitConverter.GetBytes(numOfRegBytes);
            lByteCommand.Add(byteArrNumRegBytes[1]);
            lByteCommand.Add(byteArrNumRegBytes[0]);

            return lByteCommand;
        }

        public void Ammonit_Add()
        {

            /* string date;
             string wind_s;
             string direction;
             string hum;
             string temp;
             string air_p;
             string irradience;*/
            string AmmonitComing = ComingStringFrame();

            // Split string on spaces.
            // ... This will separate all the words.
            string[] words = AmmonitComing.Split('\t');
            /* foreach (string word in words)
             {*/
            //listBox2.Items.Add(words[0]); // date
            //listBox2.Items.Add(words[1]); //wind speed
            //listBox2.Items.Add(words[2]); // directin
            //database.Ammonit(Convert.ToDouble(words[1]), Convert.ToDouble(words[2]), Convert.ToDouble(words[3]), Convert.ToDouble(words[4]), Convert.ToDouble(words[5]), Convert.ToDouble(words[6]));
        }
    }
}
