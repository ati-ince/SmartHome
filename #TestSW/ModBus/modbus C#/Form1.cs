using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace modbus
{
    public partial class Form1 : Form
    {
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
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;

            cbFunctionType.SelectedIndex = 0;
        }
        List<byte> outgoingData = new List<byte>();


        private void button1_Click(object sender, EventArgs e)
        {


            outgoingData.Clear();

            try
            {

                byte bDevAddr = Convert.ToByte(numDevAddr.Value);
                byte bFuncType;
                

                if (cbFunctionType.SelectedIndex==0)
                    bFuncType = 0x03;
                else if (cbFunctionType.SelectedIndex == 1)
                    bFuncType = 0x10;
                else
                    return;

                int regAddress = (int)numRegAddress.Value;
               

                outgoingData.Add(bDevAddr);
                outgoingData.Add(bFuncType);
                byte[] regAddr = BitConverter.GetBytes(regAddress);
                outgoingData.Add(regAddr[1]);
                outgoingData.Add(regAddr[0]);
                if (bFuncType == 0x03)
                {
                    outgoingData.Add(0x00);
                    outgoingData.Add(0x02);
                }
                else
                {
                    outgoingData.Add(0x00);
                    outgoingData.Add(0x02);
                    outgoingData.Add(0x04);
                    float val = (float)numValue.Value;
                    byte[] valBytes = BitConverter.GetBytes(val);
                    float den = BitConverter.ToSingle(valBytes, 0);
                    outgoingData.Add(valBytes[0]);
                    outgoingData.Add(valBytes[1]);
                    outgoingData.Add(valBytes[2]);
                    outgoingData.Add(valBytes[3]);
                }
            }
            catch
            {
            }

            if (outgoingData.Count > 0)
            {
                CRC16(outgoingData.Count, false, ref outgoingData);

                serialPort1.Write(outgoingData.ToArray(), 0, outgoingData.Count);

                listBoxResults.Items.Add("Data Sent:");
                string temp="";
                foreach (byte item in outgoingData)
                {
                    temp += Convert.ToInt64(item).ToString() + " ";
                }
                listBoxResults.Items.Add(temp);

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            outgoingData.Clear();

            outgoingData.Add(0x02);
            outgoingData.Add(0x03);
            outgoingData.Add(0x03);

            if (outgoingData.Count > 0)
            {
                CRC16(outgoingData.Count, false, ref outgoingData);

                serialPort1.Write(outgoingData.ToArray(), 0, outgoingData.Count);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private bool CRC16(int datalength, bool check, ref byte[] data)
        {
            uint checksum;
            byte lowCRC;
            byte highCRC;
            int i, j;
            checksum = 0xffff;

            for (j = 0; j < datalength; j++)
            {
                checksum = checksum ^ ((uint)data[j]);
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
                data[datalength] = lowCRC;
                data[datalength + 1] = highCRC;
            }
            return true;
        }

        private bool CRC16(int datalength, bool check, ref List<byte> data)
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

        private void serialPort1_DataReceived_1(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            List<byte> arr = new List<byte>();
            while (serialPort1.BytesToRead > 0)
            {
                arr.Add((byte)serialPort1.ReadByte());
                Thread.Sleep(20);
            }
            string result = "";
            foreach (byte item in arr)
            {
                result += Convert.ToInt64(item).ToString() + " ";
            }
            listBoxResults.Items.Add("Data Received");
            listBoxResults.Items.Add(result);
            listBoxResults.Items.Add(" ");

            if (arr[1] == 0x03)
            {
                float x = BitConverter.ToSingle(arr.ToArray(), 3);
                listBoxResults.Items.Add("Value: "+ x.ToString());
                listBoxResults.Items.Add("---------------------");
            }
            else if (arr[1] == 0x10)
            {
            }
            else
            {
                listBoxResults.Items.Add("Response with error code received");
                listBoxResults.Items.Add("-----------------------------");
            }
            listBoxResults.SelectedIndex = this.listBoxResults.Items.Count - 1;
            listBoxResults.Update();
            //try
            //{
            //    long result = Convert.ToInt64(arr[3]) + Convert.ToInt64(arr[4]) + Convert.ToInt64(arr[5]) + Convert.ToInt64(arr[6]);
            //    rchTxt.Text += result + "\n";
            //}
            //catch(Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString());
            //}
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();
                button1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void cbFunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {

               numValue.Visible= lbValue.Visible = (cbFunctionType.SelectedIndex == 1);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

    }
}
