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

namespace AmmonitDataToplamaV1
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///  database
        /// </summary>
        private static Form1 instance;
        ////Xtender XtenderClass = new Xtender();
        Ammonit AmmonitClass = new Ammonit();
        DataBaseSQL DataBaseSQLClass = new DataBaseSQL();
        ///////////

        /// <summary>
        /// /////
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

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AmmonitClass.Ammonit_AddAllTo_DataBase(AmmonitClass.GetAmmonitData("169.254.164.31", 40500, 16));
        }
    }
}
