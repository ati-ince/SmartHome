using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace _4Noks_Configurator
{
    public partial class Form1 : Form
    {
        byte byte1, byte2, byte3, byte4, byte5, byte6;
        bool bDeviceAddress = false;
        List<byte> outgoingData = new List<byte>();
        SerialPort sp;
        object _lockObject = new object();
        Thread polling;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            string[] ports = SerialPort.GetPortNames();
            foreach (string item in ports)
            {
                cmbComPorts.Items.Add(item);
            }
            cmbComPorts.SelectedIndex = 0;
            cmbBaud.SelectedIndex = 2;

            polling = new Thread(new ThreadStart(ModbusPollingF));
        }

        private void ModbusPollingF()
        {
            List<byte> outpolling = new List<byte>();
            outpolling.Add(0x01);
            sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);

            rchTxt.Text += "Ağ üzerindeki cihazlar aranıyor.." + Environment.NewLine;
            for (int i = (int)nUpstart.Value; i <= (int)nUpEnd.Value; i++)
            {
                outpolling[0] = Convert.ToByte(i);
                outpolling.Add(0x04);
                outpolling.Add(0x00);
                outpolling.Add(0x00);
                outpolling.Add(0x00);
                outpolling.Add(0x01);

                SerialSend(outpolling);

                Thread.Sleep(50);

                byte reci = 0x01;
                do
                {
                    try
                    {
                        reci = (byte)sp.ReadByte();
                    }
                    catch (TimeoutException)
                    {
                        break;
                    }
                }
                while (reci != i);

                if (reci == i)
                {
                    lstDeviceList.Items.Add(i);
                }
            }
            rchTxt.Text += "Cihaz arama tamamlandı." + Environment.NewLine;
            btnClose.Enabled = true;
            btnSend.Enabled = true;
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }

        /// <summary>
        /// Modbus CRC Check
        /// </summary>
        /// <param name="datalength"></param>
        /// <param name="check"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static bool CRC16(int datalength, bool check, ref List<byte> data)
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

        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort(cmbComPorts.SelectedItem.ToString(), int.Parse(cmbBaud.SelectedItem.ToString()), Parity.None, 8, StopBits.One);
                sp.ReadTimeout = 400;
                sp.WriteTimeout = 50;
                sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                if (!sp.IsOpen)
                {
                    sp.PortName = cmbComPorts.SelectedItem.ToString();
                    sp.Open();
                }
            }
            catch (Exception)
            {

            }
            if (sp.IsOpen)
            {
                btnOpen.Enabled = false;
                btnClose.Enabled = false;
                btnSend.Enabled = false;
                cmbBaud.Enabled = false;
                cmbComPorts.Enabled = false;
                lstDeviceList.Items.Clear();
                try
                {
                    polling.Abort();
                    polling = new Thread(new ThreadStart(ModbusPollingF));
                }
                catch (Exception)
                {

                    throw;
                }
                polling.Start();
                rchTxt.Text += "> " + cmbComPorts.SelectedItem.ToString() + " açıldı" + Environment.NewLine;
            }
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> arr = new List<byte>();
            while (sp.BytesToRead > 0)
            {
                arr.Add((byte)sp.ReadByte());
            }
            foreach (byte item in arr)
            {
                rchTxt.Text += Convert.ToInt32(item).ToString() + " ";
            }
            rchTxt.SelectionStart = rchTxt.Text.Length + 100;
            rchTxt.ScrollToCaret();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                sp.Close();
            }
            catch (Exception)
            {

            }
            if (!sp.IsOpen)
            {
                btnClose.Enabled = false;
                btnOpen.Enabled = true;
                btnSend.Enabled = false;
                cmbBaud.Enabled = true;
                cmbComPorts.Enabled = true;
                rchTxt.Text += "> " + cmbComPorts.SelectedItem.ToString() + " kapatıldı" + Environment.NewLine;
            }
        }

        private void btnDeviceAddress_Click(object sender, EventArgs e)
        {
            bDeviceAddress = true;
            param1.BackColor = Color.Red;
            param2.Text = "06";
            param3.Text = "00";
            param4.Text = "02";
            param5.Text = "00";
            param6.BackColor = Color.Red;
        }

        private void btnOpenRelay_Click(object sender, EventArgs e)
        {
            param1.BackColor = Color.Red;
            param2.Text = "05";
            param3.Text = "00";
            param4.Text = "01";
            param5.Text = "255";
            param6.Text = "00";
            param6.BackColor = Color.White;
            bDeviceAddress = false;
        }

        private void btnCloseRelay_Click(object sender, EventArgs e)
        {
            param1.BackColor = Color.Red;
            param2.Text = "05";
            param3.Text = "00";
            param4.Text = "02";
            param5.Text = "255";
            param6.Text = "00";
            param6.BackColor = Color.White;
            bDeviceAddress = false;
        }

        private void btnActivePower_Click(object sender, EventArgs e)
        {
            param1.BackColor = Color.Red;
            param2.Text = "04";
            param3.Text = "00";
            param4.Text = "05";
            param5.Text = "00";
            param6.Text = "01";
            param6.BackColor = Color.White;
            bDeviceAddress = false;
        }

        private void SendDeviceAddress()
        {
            /**********************************************/
            outgoingData.Clear();

            outgoingData.Add(byte1);
            outgoingData.Add(0x06);
            outgoingData.Add(0x00);
            outgoingData.Add(0x00);
            outgoingData.Add(0x19);
            outgoingData.Add(0x79);

            if (outgoingData.Count > 0)
            {
                CRC16(outgoingData.Count, false, ref outgoingData);

                sp.Write(outgoingData.ToArray(), 0, outgoingData.Count);
            }

            Thread.Sleep(500);
            /**********************************************/
            outgoingData.Clear();

            outgoingData.Add(byte1);
            outgoingData.Add(0x06);
            outgoingData.Add(0x00);
            outgoingData.Add(0x02);
            outgoingData.Add(0x00);
            outgoingData.Add(byte6);

            if (outgoingData.Count > 0)
            {
                CRC16(outgoingData.Count, false, ref outgoingData);

                sp.Write(outgoingData.ToArray(), 0, outgoingData.Count);
            }
            Thread.Sleep(500);
            /**********************************************/
            //EN SON COILSTATUS[0]=1
            outgoingData.Clear();

            outgoingData.Add(byte1);
            outgoingData.Add(0x05);
            outgoingData.Add(0x00);     //0x19
            outgoingData.Add(0x00);    //0x79
            outgoingData.Add(0xFF);
            outgoingData.Add(0x00);

            if (outgoingData.Count > 0)
            {
                CRC16(outgoingData.Count, false, ref outgoingData);

                sp.Write(outgoingData.ToArray(), 0, outgoingData.Count);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            rchTxt.Text += Environment.NewLine;
            //SEND DATA
            outgoingData.Clear();

            byte1 = Convert.ToByte(param1.Text);
            byte2 = Convert.ToByte(param2.Text);
            byte3 = Convert.ToByte(param3.Text);
            byte4 = Convert.ToByte(param4.Text);

            outgoingData.Add(byte1);
            outgoingData.Add(byte2);
            outgoingData.Add(byte3);
            outgoingData.Add(byte4);
            if (!String.IsNullOrEmpty(param5.Text))
            {
                //int int5 = int.Parse(param5.Text);
                byte5 = Convert.ToByte(param5.Text);
                outgoingData.Add(byte5);
            }
            if (!String.IsNullOrEmpty(param6.Text))
            {
                //int int6 = int.Parse(param6.Text);
                byte6 = Convert.ToByte(param6.Text);
                outgoingData.Add(byte6);
            }

            if (bDeviceAddress)
            {
                SendDeviceAddress();
                bDeviceAddress = false;
                ClearParams();
                return;
            }
            try
            {
                //int int1 = int.Parse(param1.Text);
                //int int2 = int.Parse(param2.Text);
                //int int3 = int.Parse(param3.Text);
                //int int4 = int.Parse(param4.Text);
            }
            catch
            {
            }

            SerialSend(outgoingData);
        }

        private void SerialSend(List<byte> outgoing)
        {
            lock (_lockObject)
            {
                if (outgoing.Count > 0)
                {
                    CRC16(outgoing.Count, false, ref outgoing);

                    sp.Write(outgoing.ToArray(), 0, outgoing.Count);
                }
            }
        }

        private void ClearParams()
        {
            param1.Text = "";
            param2.Text = "";
            param3.Text = "";
            param4.Text = "";
            param5.Text = "";
            param6.Text = "";
        }

        private void lstDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            param1.Text = lstDeviceList.SelectedItem.ToString();
        }
    }
}
