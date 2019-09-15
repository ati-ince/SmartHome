using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.Data.OleDb;
using System.Text.RegularExpressions;
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;

//////////////////////////

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        /// <summary>
        ///  database
        /// </summary>
        DataBaseSQL database = new DataBaseSQL();
        private static Form1 instance;
        static uint ni = 0;
        ////Xtender XtenderClass = new Xtender();
        Ammonit AmmonitClass = new Ammonit();
        DataBaseSQL DataBaseSQLClass = new DataBaseSQL();
        VoltageRegulator VoltageRegulatorClass = new VoltageRegulator();
        ///////////
        
        /// <summary>
        /// /////
        static UInt64 i = 0;
        /// </summary>
        public static Form1 Instance
        {
            get
            {
                //Create singleton object if it was not created before
                if (instance == null)
                {
                    instance = new Form1();
                }
                return instance;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /////////////////////// AMMONIT KISMI //////////////////////////////
        
        ///

        private void textBox__XtenderRead_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
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
            string s = AmmonitClass.ComingStringFrame();

            // Split string on spaces.
            // ... This will separate all the words.
            string[] words = s.Split('\t');
           
            listBox2.Items.Add(words[0]); // date
            listBox2.Items.Add(words[1]); //wind speed
            listBox2.Items.Add(words[2]); // directin
            database.Ammonit(Convert.ToDouble(words[1]), Convert.ToDouble(words[2]), Convert.ToDouble(words[3]), Convert.ToDouble(words[4]), Convert.ToDouble(words[5]), Convert.ToDouble(words[6]));
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
            AmmonitClass.GetAmmonitData(listBox1, 40500, 16).Count().ToString();
            textBox__XtenderRead.Text = AmmonitClass.ComingStringFrame(); // Burada da FORM ekranina geleni yazdiriyoruz....           
            Ammonit_Add();
              }
        ///

        ///////////////////////////////////////////////////////////////////
    }
}
