using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SensorDataTest
{
    public partial class Form1 : Form
    {
        List<Thread> listOfThreads;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listOfThreads = new List<Thread>();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            listOfThreads.Clear();

            for (int i = 0; i < nThread.Value; i++)
            {
                ThreadStart tempStart = new ThreadStart(TEMPF);
                Thread tempThread = new Thread(tempStart);
                listOfThreads.Add(tempThread);
            }
            Thread databaseThread = new Thread(new ThreadStart(DATABASEF));
            listOfThreads.Add(databaseThread);
            foreach (Thread item in listOfThreads)
            {
                item.Start();
            }
            btnStart.Enabled = false;
        }

        void TEMPF()
        {
            Random val = new Random((int)DateTime.Now.Ticks);
            int RandomNum = val.Next(1, 190);

            for (; ; )
            {
                RandomNum = val.Next(1, 20);
                Thread.Sleep(RandomNum);
                RandomNum = val.Next(100000, 900000);
                for (int i = 0; i < RandomNum; i++)
                {
                    double x = Math.Sqrt(val.NextDouble() * val.Next(32000));
                    double y = Math.Sqrt(val.NextDouble() * val.Next(32000));
                    double z = Math.Round(x * y);
                }
                Ping pinger = new Ping();
                try
                {
                    pinger.Send("www.google.com");
                }
                catch (Exception)
                {

                }
            }
        }
        void InsertData(int device_Id, int sensor_Id, int sensorTypeParameter_Id, decimal value, DateTime RecordTime)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            using (akilliEvDataContext db = new akilliEvDataContext())
            {
                tbl_SensorData Newdata = new tbl_SensorData();
                Newdata.Device_Id = device_Id;
                Newdata.Sensor_Id = sensor_Id;
                Newdata.SensorTypeParameter_Id = sensorTypeParameter_Id;
                Newdata.Value = value;
                Newdata.RecordTime = RecordTime;
                db.tbl_SensorDatas.InsertOnSubmit(Newdata);
                db.SubmitChanges();
            }
            timer.Stop();
            lblMinInsertTime.Text = timer.ElapsedMilliseconds.ToString();
        }

        /// <summary>
        /// Insert new Sensor Data through ADO.NET to 
        /// INSERT_SENSORDATA stored procedure
        /// </summary>
        /// <param name="Sensor_Id">Sensor ID</param>
        /// <param name="SensorTypeParameter_Id">SensorTypeParameter ID</param>
        /// <param name="Value">decimal Value</param>
        /// <param name="RecordTime">date RecordTime</param>
        /// <param name="Device_Id">Device ID</param>
        /// <returns></returns>
        internal void Insert_SensorData(int Sensor_Id, int SensorTypeParameter_Id, decimal Value, DateTime RecordTime, int Device_Id)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=dbAkilliEv;Integrated Security=True");
            SqlCommand comm = new SqlCommand("INSERT_SENSORDATA", conn);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@Sensor_Id", Sensor_Id));
            comm.Parameters.Add(new SqlParameter("@SensorTypeParameter_Id", SensorTypeParameter_Id));
            comm.Parameters.Add(new SqlParameter("@Device_Id", Device_Id));
            comm.Parameters.Add(new SqlParameter("@Value", Value));
            comm.Parameters.Add(new SqlParameter("RecordTime", RecordTime));
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            timer.Stop();
            this.lblMinInsertTime.Text = timer.ElapsedMilliseconds.ToString();
        }



        void DATABASEF()
        {
            for (; ; )
            {
                int numParameters = (int)nParameter.Value * (int)nDevice.Value;
                Stopwatch timer = new Stopwatch();
                timer.Start();
                for (int i = 0; i < numParameters; i++)
                {
                    Random val = new Random((int)DateTime.Now.Ticks);

                    //storedprocedure
                    //Insert_SensorData(2, 13, (decimal)10.005, DateTime.Now, 4);

                    //InsertData(4, 2, 13, (decimal)10.005, DateTime.Now);
                    using (akilliEvDataContext db = new akilliEvDataContext())
                    {
                        tbl_SensorData Newdata = new tbl_SensorData();
                        Newdata.Device_Id = db.tbl_Devices.FirstOrDefault(x => x.Device_Id == new Random().Next(4, 7)).Device_Id;
                        Newdata.Sensor_Id = db.tbl_Sensors.FirstOrDefault(x => x.Sensor_Id == new Random().Next(2, 5)).Sensor_Id;
                        Newdata.SensorTypeParameter_Id = db.tbl_SensorTypeParameters.FirstOrDefault(y => y.SensorTypeParameter_Id == new Random().Next(13, 17)).SensorTypeParameter_Id;
                        double k = Math.Sqrt(val.NextDouble() * val.Next(32000));
                        double l = Math.Sqrt(val.NextDouble() * val.Next(32000));
                        double m = Math.Round(k * l);
                        Newdata.Value = (decimal)m;
                        Newdata.RecordTime = DateTime.Now;
                        db.tbl_SensorDatas.InsertOnSubmit(Newdata);
                        db.SubmitChanges();
                    }
                }
                timer.Stop();
                lblElapsed.Text = timer.ElapsedMilliseconds.ToString();
                lblPerParameter.Text = (((double)timer.ElapsedMilliseconds / (double)numParameters)).ToString();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (Thread item in listOfThreads)
            {
                item.Abort();
            }
            btnStart.Enabled = true;
        }
    }
}
