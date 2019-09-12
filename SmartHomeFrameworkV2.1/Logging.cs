using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient; // SQL icin gereken girilmis
using System.Data;
using System.Text;
using System.IO;

namespace SmartHomeFrameworkV2._1
{
    public class Logging
    {
        public void Logging2Txt(string Event, string Comment)
        {
            String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
            String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
            StreamWriter w = File.AppendText("log.txt");
            w.WriteLine(trh + " " + zmn + " " + Event + " " + Comment);  
        }
    }
}
