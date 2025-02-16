using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.DAO
{
    internal class selectDAO
    {


        public selectDAO()
        {

        }

        /// <summary>
        /// Vypíše seznam autorů z databáze
        /// </summary>
        public void GetAutori()
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "SELECT * FROM autori";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Seznam autorů:");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine($"ID: {reader["id_au"]}, Jméno: {reader["jm_au"]}, Příjmení: {reader["prijm_au"]}, Datum narození: {reader["dat_nar"]}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
        }

        /// <summary>
        /// Vypíše seznam knih z databáze
        /// </summary>
        public void GetKnihy()
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "SELECT * FROM knihy";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Seznam knih:");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine($"ID: {reader["id_kn"]}, Název: {reader["nazev_kn"]}, Datum vydání: {reader["dat_vyd"]}, Autor ID: {reader["id_au"]}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
        }

        /// <summary>
        /// Vypíše seznam produktů z databáze
        /// </summary>
        public void GetProdukty()
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "SELECT * FROM produkt";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Seznam produktů:");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine($"ID: {reader["id_p"]}, Kód: {reader["kod_p"]}, Kniha ID: {reader["id_kn"]}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
        }

        /// <summary>
        /// Vypíše seznam zákazníků z databáze
        /// </summary>
        public void GetZakaznici()
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "SELECT * FROM zakaznik";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Seznam zákazníků:");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine($"ID: {reader["id_za"]}, Jméno: {reader["jm_za"]}, Příjmení: {reader["prijm_za"]}, Telefon: {reader["tel"]}, Datum narození: {reader["dat_nar"]}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
        }

        /// <summary>
        /// Vypíše seznam výpujček z databáze
        /// </summary>
        public void GetVypujcky()
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "SELECT * FROM vypujcky";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Seznam vypůjček:");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine($"ID: {reader["id_vyp"]}, Datum vypůjčení: {reader["dat_vypujceni"]}, Datum vrácení: {reader["dat_vraceni"]}, Vraceno: {reader["vraceno"]}, Produkt ID: {reader["id_p"]}, Zákazník ID: {reader["id_za"]}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
        }
    }
}
