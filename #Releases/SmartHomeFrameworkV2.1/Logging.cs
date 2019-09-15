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
        /**/
        System.IO.StreamWriter file;
        static string FileName;
        //


        // Log start file burada olusturulacak.. Boylece her yazilimda bir tane dosya olacak.
        //public void LoggingStart()
        //{ 
        //    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
        //    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
        //        string[] words = zmn.Split(' ');
        //        string Time="";
        //        for (int i = 0; i < words.Length; i++)
        //        {
        //           Time +=words[i];
        //        }
                
        //    //
        //        FileName = "LogFile" + "_" + trh + "_" + Time + ".txt";
        //}

        public void LoggingStart()
        {
            FileName = "LogFile" + ".txt";
        }


        public void Logging2Txt(string Event, string Comment)
        {
            String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
            String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                string[] words = zmn.Split(' ');
                string Time = "";
                for (int i = 0; i < words.Length; i++)
                {
                    Time += words[i];
                }
            //
            file = new System.IO.StreamWriter(FileName, true);
            file.WriteLine(trh + " " + Time + " " + Event + " " + Comment);
            file.Close();
        }

    }
}
