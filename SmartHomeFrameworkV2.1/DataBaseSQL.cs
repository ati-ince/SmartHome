using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient; // SQL icin gereken girilmis
using System.Data;
using System.Text;
using System.IO;

namespace SmartHomeFrameworkV2._1
{
    public class DataBaseSQL : Logging
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConnectionInformation()
        {
            // Disaridan nasil yazilacagi bilgisi ogrenilebilsin
            //string connection = "Data Source=SMARTHOME;Initial Catalog=SmartHome;Integrated Security=True";
            return "Data Source=SMARTHOME;Initial Catalog=SmartHome;Integrated Security=True";
        }
        //
        static string Connect = "";// simdilik burasi bos deger ama disaridan bilgi alindiktan sonra ici doldurulacak
        /// </summary>
        public void ConnectionInitialize(string DataSource)
        {
            Connect = DataSource; // Boylece disaridan DataBase baglantisi kurulacak bilgi alinmis oldu. Devaminda DataBase baslatilabilir. 
        }
 
        /// <summary>
        /// Burada yazilima adresin dirasiran girilmesine imkan taniyalim, normalde yukaridaki gibi idi,
        /// </summary>
        SqlConnection baglanti = new SqlConnection(Connect);
        //
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

                komut.Parameters.AddWithValue("@Date_Sql", trh + " " + zmn);
                komut.Parameters.AddWithValue("@Date_Read", date);
                komut.Parameters.AddWithValue("@Command", command);
                komut.Parameters.AddWithValue("@Data", data);
                komut.ExecuteNonQuery();
                baglanti.Close();
                //
                Logging2Txt("Database_Xtender", " Register:"+command.ToString()+" WriteToDataBase");

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Logging2Txt("Database_Xtender", " " + hata.Message);
                }

            }



        }
        public void fournoks(string date, int adress, char command, float data)
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
                //
                Logging2Txt("Database_4noks", " Adress:" + adress.ToString() +" Command:" + command.ToString() +" WriteToDataBase");
            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Logging2Txt("Database_4noks", " " + hata.Message);
                }

            }



        }
        public void Ammonit( double wind_speed, double Wind_Direction, double Humidity, double Temperature, double Air_Pressure, double Irradiance)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


                String trh = DateTime.Now.ToShortDateString().Replace('/', '.');
                String zmn = DateTime.Now.ToLongTimeString().Replace(':', '.');


                string kayit = "insert into Ammonit(Date,Wind_Speed,Wind_Direction,Humidity,Temperature,Air_Pressure,Irradiance) values (@Date,@Wind_Speed,@Wind_Direction,@Humidity,@Temperature,@Air_Pressure,@Irradiance)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@Date", trh + " " + zmn);
                komut.Parameters.AddWithValue("@Wind_Speed", wind_speed);
                komut.Parameters.AddWithValue("@Wind_Direction", Wind_Direction);
                komut.Parameters.AddWithValue("@Humidity", Humidity);
                komut.Parameters.AddWithValue("@Temperature", Temperature);
                komut.Parameters.AddWithValue("@Air_Pressure", Air_Pressure);
                komut.Parameters.AddWithValue("@Irradiance", Irradiance);
                komut.ExecuteNonQuery();
                baglanti.Close();
                //
                Logging2Txt("Database_Ammonit", " AllAmmonitParameters"+" WriteToDataBase");

            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Logging2Txt("Database_Ammonit", " " + hata.Message);
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
                //
                Logging2Txt("Database_Solar", " SolarCommand:" + command.ToString() + " WriteToDataBase");
            }
            catch (Exception hata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Logging2Txt("Database_Solar", " " + hata.Message);
                }

            }

        }
    }

}

