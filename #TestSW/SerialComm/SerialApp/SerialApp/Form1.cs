// @ ATI'nin malidir, hacilamak yasaktir .......
// atahir.ince@gmail.com
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
//

// **********************************************************//

namespace SerialApp
{
    public partial class Form1 : Form 
    {
        //Class's and Struct's
        /**/
        SerialCOMM.ComPortStruct _xtenderStruct = new SerialCOMM.ComPortStruct(); // also we have _xtenderStruct._SerialPortObj
        SerialPort _xtenderSerial = new SerialPort();
        Xtender XtenderClass= new Xtender();
        /**/
        SerialCOMM _serialcomm = new SerialCOMM();
        /**/
        Logging _log = new Logging();
        /**/

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _log.LoggingStart();//sadece ilk program baslarken cagirilacak..
            /**/
            _xtenderStruct._GetPortNames = SerialPort.GetPortNames(); // get all port names from Windows...
            // fill all ComboBox using available ComPorNames
            _serialcomm.ComboBoxComPortNameFilling(_xtenderStruct._GetPortNames, comboBox1);
            //
            
        }

        private void button1_Click(object sender, EventArgs e) // Xtender Denemesi icin .....
        {
            ///
           

            // Port name felan Combodan cekilmekte ve asagida SerialComm icerisinde kullanilmaktadir !!!
            XtenderClass.XtenderComPortSettings(ref _xtenderStruct, ref _xtenderSerial, comboBox1); // oncelikle struct icerisine gereken degerler SerialComm oncesionde cekiliyor. 
            //SerialComm icerisinde port ayarlari ise simdi yapilabilir. 
            _serialcomm.ComPortSettings(ref _xtenderStruct); // boylece _XtenderSerialPort ayarlari tamamlanmis oldu. (SerialComm ile genel sekilde halledilebildi)
            // Portu acip kullanmaya baslayalim...
            _serialcomm.SerialOpen(ref _xtenderStruct); // gerekli SerialPort ve icerikleri _struct ile aktarilmis oldu !!!

        }
    }
}
