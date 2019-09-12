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
using System.Data.OleDb;
// TCP-IP SOCKET
using System.Net;
using System.Net.Sockets;
// **********************************************************//

namespace SmartHomeFrameworkV2._1
{
    public class Xtender
    {
        /// <summary>
        /// GLOBAL VARIATIONS
        /// </summary>
        OleDbConnection baglanti; // Excel Connection
       

        public void ExcellConnection()
        {
            baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\xtender_write_komutlari.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;'");
        }
        
    }
}
