using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.DAO
{
    internal class deleteDAO
    {


        public deleteDAO()
        {

        }

        private bool RecordExists(string tableName, string columnName, int id)
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

        private void DeleteRecord(string tableName, string columnName, int id, string objectName)
        {
            if (!RecordExists(tableName, columnName, id))
            {
                Console.WriteLine($"{objectName} s tímto ID neexistuje.");
                return;
            }

            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"DELETE FROM {tableName} WHERE {columnName} = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"{objectName} úspěšně smazán.");
                }
            }
        }

        public void deleteAutor(int id) => DeleteRecord("autori", "id_au", id, "Autor");
        public void deleteKniha(int id) => DeleteRecord("knihy", "id_kn", id, "Kniha");
        public void deleteZakaznik(int id) => DeleteRecord("zakaznik", "id_za", id, "Zákazník");
        public void deleteProdukt(int id) => DeleteRecord("produkt", "id_p", id, "Produkt");
        public void deleteVypujcka(int id) => DeleteRecord("vypujcky", "id_vyp", id, "Výpůjčka");
    }
}
