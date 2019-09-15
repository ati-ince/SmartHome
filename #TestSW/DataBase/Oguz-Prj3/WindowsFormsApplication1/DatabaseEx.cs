using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;

namespace Database_Deneme
{
    public class Database
    {
        static string Connect = "Data Source=DESKTOP-CJ4DVUA;Initial Catalog=Smart Home;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        SqlConnection baglanti = new SqlConnection(Connect);
       // static string Connect = "Data Source=MEKZUM;Initial Catalog=AkilliEv;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Xtender(string date, int command, float data)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                   
               
                string kayit = "insert into Xtender(Date_Sql,Date_Read,Command,Data) values (@Date_Sql,@Date_Read,@Command,@Data)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@Date_Sql", trh+" "+zmn);
                komut.Parameters.AddWithValue("@Date_Read", date);
                komut.Parameters.AddWithValue("@Command", command);
                komut.Parameters.AddWithValue("@Data", data);
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {

                    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                    w.WriteLine(trh + "   " + zmn + "    Database_Xtender     " + hata.Message);
                }

            }



        }
        public void fournoks(string date,int adress, char command, float data)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');


                string kayit = "insert into fournoks(Date_Sql,Date_Read,Adress,Command,Data) values (@Date_Sql,@Date_Read,@Adress,@Command,@Data)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@Date_Sql", trh + " " + zmn);
                komut.Parameters.AddWithValue("@Date_Read", date);
                komut.Parameters.AddWithValue("@Adress", adress);
                komut.Parameters.AddWithValue("@Command", command);
                komut.Parameters.AddWithValue("@Data", data);
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {

                    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                    w.WriteLine(trh + "   " + zmn + "    Database_4noks     " + hata.Message);
                }

            }



        }
        public void Wind(string date, int adress, char command, float data)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');


                string kayit = "insert into Wind(Date_Sql,Date_Read,Adress,Command,Data) values (@Date_Sql,@Date_Read,@Adress,@Command,@Data)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@Date_Sql", trh + " " + zmn);
                komut.Parameters.AddWithValue("@Date_Read", date);
                komut.Parameters.AddWithValue("@Adress", adress);
                komut.Parameters.AddWithValue("@Command", command);
                komut.Parameters.AddWithValue("@Data", data);
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {

                    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                    w.WriteLine(trh + "   " + zmn + "    Database_Wind     " + hata.Message);
                }

            }



        }
        public void Solar(string date, int command, float data)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');


                string kayit = "insert into Solar(Date_Sql,Date_Read,Command,Data) values (@Date_Sql,@Date_Read,@Command,@Data)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@Date_Sql", trh + " " + zmn);
                komut.Parameters.AddWithValue("@Date_Read", date);
                komut.Parameters.AddWithValue("@Command", command);
                komut.Parameters.AddWithValue("@Data", data);
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {

                    String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                    String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');
                    w.WriteLine(trh + "   " + zmn + "    Database_Solar     " + hata.Message);
                }

            }



        }
    }

}

