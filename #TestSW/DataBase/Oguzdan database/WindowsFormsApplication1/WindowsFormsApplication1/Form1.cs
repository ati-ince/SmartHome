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
     
        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
            AmmonitClass.GetAmmonitData(listBox1, 40500, 16).Count().ToString();
            textBox__XtenderRead.Text = AmmonitClass.ComingStringFrame(); // Burada da FORM ekranina geleni yazdiriyoruz....           
            AmmonitClass.Ammonit_Add();// seperate Ammonit frame and write to database
        }
        ///

        ///////////////////////////////////////////////////////////////////
    }
}
