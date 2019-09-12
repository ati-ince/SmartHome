// **********************************************************//
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
using System.Text.RegularExpressions;
// Excel
using System.Data.OleDb; // use for -ofcourse- excell 
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;
// **********************************************************//
// Burada hazirlayacagim Class ile SH uygulmasinda Excel kullanimi saglanacaktir.
// buna ek olarak daha sonraki uygulamalrda da kullanim icin klasik bir excell dosya acilip icerisine data yazimi,
// yeni excel dosya create edilip veri yazimi (loglama amacli ornegin, yada data toplama)
// islerini yapacagiz !!!
// **********************************************************//
namespace SmartHomeFrameworkV2._1
{
    public class ExcelUsege
    {
        /// <summary>
        /// GLOBAL CLASS
        /// </summary>

        public struct ExcelStruct
        {
            public DataTable table;
            public OleDbConnection connection; // Excel Connection
            //
            public List<int> ExcelWriteList;
            public List<int> ExcelReadList;
        }


        public List<int> ExcelXtederReadRegisterList(ref ExcelStruct excelstr) // if need use the return list
        {
            Int32 data;
            List<int> ListBuffer = new List<int>();
            //
            excelstr.ExcelReadList.Clear();// we use for collect the LIST............

            ///// READ icin
            for (int x = 2; x < excelstr.table.Rows.Count; x++)
            {
                if (excelstr.table.Rows[x][5].GetType().ToString() != "System.DBNull")
                {
                    data = Convert.ToInt32(excelstr.table.Rows[x][5]);
                    if (data >= 3000)
                    {
                        ListBuffer.Add(data);
                    }
                }
            }

            excelstr.ExcelReadList = ListBuffer;// we've got the LIST
            return excelstr.ExcelReadList;
        }
        //*************************************************************************************

        public List<int> ExcelXtenderWriteRegisterList(ref ExcelStruct excelstr)
        {
            Int32 data;
            List<int> ListBuffer = new List<int>();
            //
            excelstr.ExcelWriteList.Clear();
            //
            ///// WRITE icin
            for (int x = 2; x < excelstr.table.Rows.Count; x++)
            {
                if (excelstr.table.Rows[x][0].GetType().ToString() != "System.DBNull")
                {
                    data = Convert.ToInt32(excelstr.table.Rows[x][0]);
                    if (data >= 1000)
                    {
                        ListBuffer.Add(data);
                    }
                }
            }
            //
            excelstr.ExcelWriteList = ListBuffer;// we've got the LIST
            return excelstr.ExcelWriteList;
        }

        // Excell read and fill the Grid after give the number to Grid for Excel .............
        public void FilltheTablefromExcel(ref ExcelStruct excelstr)
        {
            ExcellConnection(ref excelstr); // first, connect to excel
            excelstr.connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", excelstr.connection);
            //
            da.Fill(excelstr.table);
            excelstr.connection.Close(); ;
        }
        //*************************************************************************************

        // Excel connction obj
        public void ExcellConnection(ref ExcelStruct excelstr)
        {
            excelstr.table = new DataTable();
            excelstr.connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\xtender_write_komutlari.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;'");
        }
        /***************************************************************************************/

        // In here, will questioning read excell by form 
        // When doing questioning, we seach in the datagridview list the register adres inside the list? 

        public bool[] XtenderExcelQuestioning()
        {
            bool[] rETURN = { false, false }; // first bit defines

        

        }
        //*************************************************************************************

        
    }
}
