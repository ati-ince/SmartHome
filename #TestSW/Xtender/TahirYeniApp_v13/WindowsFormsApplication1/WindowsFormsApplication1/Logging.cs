using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///////////////////////////////////////////////

namespace WindowsFormsApplication1
{
    class Logging
    {       
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\n");
            w.WriteLine("{0}", DateTime.Now.ToString());
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-----------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        
    }
}
