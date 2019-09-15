using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ExcelManagement
{
    public partial class Form1 : Form
    {
        // Class reDef
        ExcelUsege _exceluse = new ExcelUsege();//public void ExcelCall(string NameofFile, ref ExcelStruct excelstr)
        
        // Class.Struct reDef
        ExcelUsege.ExcelStruct _excelStruct = new ExcelUsege.ExcelStruct();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            _exceluse.ExcelCall4Noks("Plugs.xlsx", ref _excelStruct);
            //
            string buffx;
            for (uint i = 0; i < _excelStruct.table.Rows.Count; i++)
            {
                //buffx = _excelStruct.table.Rows[(int)i][0].ToString() + "#" + _excelStruct.table.Rows[(int)i][1].ToString() + "#" + _excelStruct.table.Rows[(int)i][2].ToString() + "*";
                buffx = _excelStruct.table.Rows[(int)i][0].ToString();
                _excelStruct.PlugsInfo.Add(buffx);
            }
            //
            textBox1.Text = _excelStruct.table.Rows[0][0].ToString();
            

            //textBox1.Text = _excelStruct.table.Rows[0][0].ToString();
            //textBox2.Text = _excelStruct.table.Rows[0][1].ToString();
            //textBox3.Text = _excelStruct.table.Rows[0][2].ToString();
        }
    
    }
}
