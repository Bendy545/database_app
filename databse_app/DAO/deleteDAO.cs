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
        /// <summary>
        /// Získá ID záznamu
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="columnName">Název sloupce</param>
        /// <param name="value">hledaná hodnota</param>
        /// <param name="idColumn">název sloupce s ID</param>
        /// <returns>ID záznamu</returns>
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

        /// <summary>
        /// Odstraní záznam z databáze na základě ID zjištěného metodou GetIdByColumnValue
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="columnName">Název sloupce</param>
        /// <param name="value"></param>
        /// <param name="idColumn">sloupec obsahujicí ID záznamu</param>
        /// <param name="objectName">Název objektu. Vypíše se když je metoda úspěšná nebo ne</param>
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

        /// <summary>
        /// Odstraní autora na základě jména a příjmení
        /// </summary>
        /// <param name="jmeno">Jméno autora</param>
        /// <param name="prijmeni">Příjmení autora</param>
        public void deleteAutor(string jmeno, string prijmeni) =>
            DeleteRecord("autori", "CONCAT(jm_au, ' ', prijm_au)", $"{jmeno} {prijmeni}", "id_au", "Autor");

        /// <summary>
        /// Odstraní knihu podle jejího názvu1
        /// </summary>
        /// <param name="nazev">název knihy</param>
        public void deleteKniha(string nazev) =>
            DeleteRecord("knihy", "nazev_kn", nazev, "id_kn", "Kniha");

        /// <summary>
        /// Odstraní zákazníka podle jména a příjmení
        /// </summary>
        /// <param name="jmeno">Jméno zákazníka</param>
        /// <param name="prijmeni">Příjmení zákazníka</param>
        public void deleteZakaznik(string jmeno, string prijmeni) =>
            DeleteRecord("zakaznik", "CONCAT(jm_za, ' ', prijm_za)", $"{jmeno} {prijmeni}", "id_za", "Zákazník");

        /// <summary>
        /// Odstraní produkt na základě jeho kódu
        /// </summary>
        /// <param name="kod">Kód produktu</param>
        public void deleteProdukt(string kod) =>
            DeleteRecord("produkt", "kod_p", kod, "id_p", "Produkt");

        /// <summary>
        /// Odstraní výpujčku na základě ID
        /// </summary>
        /// <param name="id">ID výpujčky</param>
        public void deleteVypujcka(int id) =>
            DeleteRecord("vypujcky", "id_vyp", id.ToString(), "id_vyp", "Výpůjčka");
    }
}
