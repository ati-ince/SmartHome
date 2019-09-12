using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient; // SQL icin gereken girilmis
using System.Data;
using System.Text;
using System.IO;

namespace SerialApp
{
    public class Logging
    {
        /**/
        System.IO.StreamWriter file;
        static string FileName;
        //


        // Log start file burada olusturulacak.. Boylece her yazilimda bir tane dosya olacak.
        public void LoggingStart()
        { 
            String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
            String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
            //
            FileName="LogFile" + "_" + trh + "_" + zmn + ".txt";
        }

        // Dosyaya her log girisi yapilacaginda bu cagirilmakta..
        public void Logging2Txt(string Event, string Comment)
        {
            String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
            String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
            //
            file = new System.IO.StreamWriter(FileName, true);
            file.WriteLine(trh + " " + zmn + " " + Event + " " + Comment);
            file.Close();
        }

    }
}
