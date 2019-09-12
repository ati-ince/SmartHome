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
// Deneme amacli xtender excel dosyasini DEBUG icerisine attim
// buna ek olarak daha sonraki uygulamalrda da kullanim icin klasik bir excell dosya acilip icerisine data yazimi,
// yeni excel dosya create edilip veri yazimi (loglama amacli ornegin, yada data toplama)
// islerini yapacagiz !!!
// **********************************************************//
namespace SmartHomeFrameworkV2._1
{
    public class ExcelUsege : Logging
    {
        /// <summary>
        /// GLOBAL CLASS
        /// </summary>

        public struct ExcelStruct
        {
            public DataTable table;
            public OleDbConnection connection; // Excel Connection (that uses Ole Connection for Excel Apps)
            //
            public List<int> ExcelWriteList;
            public List<int> ExcelReadList;
            public List<int> BufferList;
            //
            public string FileName;
            public string FileDirectory;
        }

        // CALL EXCEL FILE
        // we dont need direction, it will use in same file(debug)
        // it will use from USER that means
        public void ExcelCall(string NameofFile, ref ExcelStruct excelstr) 
        {
            excelstr.FileDirectory=Directory.GetCurrentDirectory();
            excelstr.FileName = NameofFile;// with file type .xlsx or .xls
            // Connect excel file 
            ExcellConnection(ref excelstr);
            //and fill the table from excel
            FilltheTablefromExcel(ref excelstr);
            // Fill the READ LIST
            ExcelXtederReadRegisterList(ref excelstr);
            // Fill the WRITE LIST
            ExcelXtenderWriteRegisterList(ref excelstr);
            //
            Logging2Txt("XtenderExcelCall", " ExcelKullanimaAlindi");
            //
        }

        public List<int> ExcelXtederReadRegisterList(ref ExcelStruct excelstr) // if need use the return list
        {
            Int32 data;
            List<int> ListBuffer = new List<int>();
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
            //
            Logging2Txt("XtenderExcelReadRegister", ListBuffer.Count().ToString() + " ElemanDosyadanOkunmustur");
            //
            return excelstr.ExcelReadList;
        }
        //*************************************************************************************

        public List<int> ExcelXtenderWriteRegisterList(ref ExcelStruct excelstr)
        {
            Int32 data;
            List<int> ListBuffer = new List<int>();
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
            //
            Logging2Txt("XtenderExcelWriteRegister", ListBuffer.Count().ToString() + " ElemanDosyadanOkunmustur");
            //
            return excelstr.ExcelWriteList;
        }

        // Excell read and fill the Grid after give the number to Grid for Excel .............
        private void FilltheTablefromExcel(ref ExcelStruct excelstr)
        {
            excelstr.connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", excelstr.connection);
            //
            da.Fill(excelstr.table);
            excelstr.connection.Close(); ;
        }
        //*************************************************************************************

        // Excel connction obj
        private void ExcellConnection(ref ExcelStruct excelstr)
        {
            excelstr.table = new DataTable();
            // file directory ve file name bizim tarafimizdan istenildigi yerde calisabiliyor...
            excelstr.connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='"+excelstr.FileDirectory+"/"+excelstr.FileName+"';Extended Properties='Excel 12.0 xml;HDR=YES;'");
        }
        /***************************************************************************************/

        // In here, will questioning read excell by form 
         //When doing questioning, we seach in the datagridview list the register adres inside the list? 
        public string[] XtenderExcelQuestioning(int tutuklu, List<int> liste)
        {
            string[] ret = new string[2];
           int[] listDizi = liste.ToArray();

           for (int i = 0; i < listDizi.Length; i++)
           {
               if (listDizi[i] == tutuklu)
               {
                   ret[0] = i.ToString();
                   ret[1] = "TRUE";
                   break;
               }
               else
               {
                   ret[0] = (-1).ToString();
                   ret[1] = "FALSE";
               }

           }
           //
           Logging2Txt("XtenderExcelQuestioning", " " + ret[0].ToString() +"RegisterNo"+ ret[1].ToString());
           //
            return ret;
        }
        //*************************************************************************************

        
    }
}
