using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.DAO
{
    internal class insertDAO
    {


        public insertDAO()
        {

        }

        /// <summary>
        /// Kontroluje zda záznam v databázi existuje
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="columnName">Název sloupce</param>
        /// <param name="id">Hledané ID</param>
        /// <returns></returns>
        public bool RecordExists(string tableName, string columnName, int id)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                connection.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        /// <summary>
        /// Přidá nového autora do databáze
        /// </summary>
        /// <param name="jmeno">Jmébo autora</param>
        /// <param name="prijmeni">Příjmení autora</param>
        /// <param name="datumNarozeni">Datum narození autora</param>
        public void AddAutor(string jmeno, string prijmeni, DateTime datumNarozeni)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO autori (jm_au, prijm_au, dat_nar) values (@Jmeno, @Prijmeni, @DatumNarozeni)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@DatumNarozeni", datumNarozeni);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá novou knihu do databáze
        /// </summary>
        /// <param name="nazev">Název knihy</param>
        /// <param name="datumVydani">Datum vydání knihy</param>
        /// <param name="autorId">ID autora knihy</param>
        public void AddKniha(string nazev, DateTime datumVydani, int autorId)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO knihy (nazev_kn, dat_vyd, id_au) VALUES (@Nazev, @DatumVydani, @AutorId)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nazev", nazev);
                cmd.Parameters.AddWithValue("@DatumVydani", datumVydani);
                cmd.Parameters.AddWithValue("@AutorId", autorId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá nového zákazníka do databáze
        /// </summary>
        /// <param name="jmeno">Jméno zákazníka</param>
        /// <param name="prijmeni">Příjmení zákazníka</param>
        /// <param name="telefon">Telefon zákazníka</param>
        /// <param name="datumNarozeni">Datum narození zákazníka</param>
        public void AddZakaznik(string jmeno, string prijmeni, string telefon, DateTime datumNarozeni)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO zakaznik (jm_za, prijm_za, tel, dat_nar) VALUES (@Jmeno, @Prijmeni, @Telefon, @DatumNarozeni)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@Telefon", telefon);
                cmd.Parameters.AddWithValue("@DatumNarozeni", datumNarozeni);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá nový produkt do databáze
        /// </summary>
        /// <param name="kod">Kód produktu</param>
        /// <param name="idKniha">ID knihy</param>
        public void AddProdukt(int kod, int? idKniha)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO produkt (kod_p, id_kn) VALUES (@Kod, @IdKniha)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Kod", kod);

                if (idKniha.HasValue)
                {
                    cmd.Parameters.AddWithValue("@IdKniha", idKniha);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdKniha", DBNull.Value);
                }

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá novou výpujčku do databáze
        /// </summary>
        /// <param name="datumVypujceni">Datum vypůjčení</param>
        /// <param name="datumVraceni">Datum vrácení (může být null)</param>
        /// <param name="vraceno">True nebo False. Určuje zda produkt byl vrácen nebo ne.</param>
        /// <param name="idProdukt">ID produktu</param>
        /// <param name="idZakaznik">ID zákazníka</param>
        public void AddVypujcka(DateTime datumVypujceni, DateTime? datumVraceni, bool vraceno, int idProdukt, int idZakaznik)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO vypujcky (dat_vypujceni, dat_vraceni, vraceno, id_p, id_za) VALUES (@DatumVypujceni, @DatumVraceni, @Vraceno, @IdProdukt, @IdZakaznik)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DatumVypujceni", datumVypujceni);

                if (datumVraceni.HasValue)
                {
                    cmd.Parameters.AddWithValue("@DatumVraceni", datumVraceni);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DatumVraceni", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Vraceno", vraceno);
                cmd.Parameters.AddWithValue("@IdProdukt", idProdukt);
                cmd.Parameters.AddWithValue("@IdZakaznik", idZakaznik);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
