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

        private int? GetIdByColumnValue(string tableName, string columnName, string value, string idColumn)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"SELECT {idColumn} FROM {tableName} WHERE {columnName} = @value";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@value", value);
                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null ? (int?)result : null;
                }
            }
        }

        private void DeleteRecord(string tableName, string columnName, string value, string idColumn, string objectName)
        {
            int? id = GetIdByColumnValue(tableName, columnName, value, idColumn);
            if (id == null)
            {
                Console.WriteLine($"{objectName} s touto hodnotou neexistuje.");
                return;
            }

            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"DELETE FROM {tableName} WHERE {idColumn} = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"{objectName} úspěšně smazán.");
                }
            }
        }

        public void deleteAutor(string jmeno, string prijmeni) =>
            DeleteRecord("autori", "CONCAT(jm_au, ' ', prijm_au)", $"{jmeno} {prijmeni}", "id_au", "Autor");

        public void deleteKniha(string nazev) =>
            DeleteRecord("knihy", "nazev_kn", nazev, "id_kn", "Kniha");

        public void deleteZakaznik(string jmeno, string prijmeni) =>
            DeleteRecord("zakaznik", "CONCAT(jm_za, ' ', prijm_za)", $"{jmeno} {prijmeni}", "id_za", "Zákazník");

        public void deleteProdukt(string kod) =>
            DeleteRecord("produkt", "kod_p", kod, "id_p", "Produkt");

        public void deleteVypujcka(int id) =>
            DeleteRecord("vypujcky", "id_vyp", id.ToString(), "id_vyp", "Výpůjčka");
    }
}
