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
