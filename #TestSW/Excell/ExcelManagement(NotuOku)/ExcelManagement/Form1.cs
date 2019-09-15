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
            _exceluse.ExcelCall("xtender_write_komutlari.xlsx", ref _excelStruct);
            //_exceluse.ExcelXtenderWriteRegisterList(ref _excelStruct);// list<int>
            string[] came = _exceluse.XtenderExcelQuestioning(3023, _excelStruct.ExcelReadList);//true false
            textBox1.Text = came[0];//coord
            textBox2.Text = came[1];//state
        }
    
    }
}
