using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;
//////////////////////////

namespace SmartHomeFrameworkV2._1
{
    class Ammonit
    {

        // Burada DataBase ye eklemek icin gerekli baglantilari kurmus olduk.
        DataBaseSQL database = new DataBaseSQL(); 
        

        public static string OutWorld;
        ///////////////////////////////

        public string GetAmmonitData_Listbox(System.Windows.Forms.ListBox Listbox, ushort registerAddress, ushort numOfRegs)
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
                        //// geleni kopyalayalim ////
                        //CommCome = bytes;
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
            return OutWorld;// Burada data disariya string olarak cikartilmaktadir !!!!
            // Mecburen static bir yere yazabilmekteyiz//
        }
        /////
        public float AmmonitBytesToFloat(byte[] bytes, int startIndex)
        {
            byte[] tempArray = new byte[4];
            tempArray[0] = bytes[startIndex + 3];
            tempArray[1] = bytes[startIndex + 2];
            tempArray[2] = bytes[startIndex + 1];
            tempArray[3] = bytes[startIndex];

            return BitConverter.ToSingle(tempArray, 0);

        }
        //////
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

        /// <summary>
        ///  Bu fonksiyon ile DataBaseye ekleme yapiyoruz.
        /// </summary>
        /// public void Ammonit( double wind_speed, double Wind_Direction, double Humidity, double Temperature, double Air_Pressure, double Irradiance)

        public void Ammonit_AddAllTo_DataBase(string AmmonitComingString)
        {

            // Burada da DataBese'e ekleme yapmaktayiz.
            // GetAmmonitData ciktisi gelen uzun string kullaniliyor....
            database.Ammonit(
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "WindSpeed")),
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "WindDirection")),
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "Humidity")),
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "Temperature")),
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "AirPressure")),
            Convert.ToDouble(Ammonit_Split_GetData(AmmonitComingString, "Irradiance"))
            );
        }
        //
        public string Ammonit_Split_GetData(string AmmonitComingString, string FrameType)
        {
            // Burada da GetAmmonitData ciktisi gelen uzun string istedigimiz parcalara ayriliyor ve string olarak geri donus veriyor. 
            uint indis = 0;
            string OutData; // datanin disari cikartilan hali ise budur.


            // Split string on spaces.
            // ... This will separate all the words.
            string[] words = AmmonitComingString.Split('\t');
            /* foreach (string word in words)
             {*/
            //listBox2.Items.Add(words[0]); // date
            //listBox2.Items.Add(words[1]); //wind speed
            //listBox2.Items.Add(words[2]); // wind directin
            //listBox2.Items.Add(words[3]); // Humidity
            //listBox2.Items.Add(words[4]);//Temperature
            //listBox2.Items.Add(words[5]);//Air_Pressure
            //listBox2.Items.Add(words[6]);//Irradiance
            /**********************************************************/
            //
            switch (FrameType)
            {
                case "Date": indis = 0;
                    break;
                case "WindSpeed": indis = 1;
                    break;
                case "WindDirection": indis = 2;
                    break;
                case "Humidity": indis = 3;
                    break;
                case "Temperature": indis = 4;
                    break;
                case "AirPressure": indis = 5;
                    break;
                case "Irradiance": indis = 6;
                    break;
                ///
                //// Devaminda diger eklemeler yapilacaktir. Diger datalarinda cekilmesi amaci ile
                ///
                default:
                    break;

            }
            OutData = words[indis];
            //
            return OutData;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Datayi ise cekiyoruz burada..
        public string GetAmmonitData(string IPadress, ushort registerAddress, ushort numOfRegs)
        {
            byte[] bytes = new byte[1024];
            List<byte> lByteCommand = new List<byte>();


            // 
            //IPadress= "169.254.36.137"
            ///
            //////////////////
            // Connect to a remote device.
            try
            {

                IPAddress ipAddress = IPAddress.Parse(IPadress);
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
                        //// geleni kopyalayalim ////
                        //CommCome = bytes;
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
            return OutWorld;// Burada data disariya string olarak cikartilmaktadir !!!!
            // Mecburen static bir yere yazabilmekteyiz//
        }
    }
}
