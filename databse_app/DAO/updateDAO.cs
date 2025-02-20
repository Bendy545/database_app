using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.DAO
{
    internal class updateDAO
    {



        public updateDAO()
        {

        }

        /// <summary>
        /// Získá ID záznamu 
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="searchColumn1">První sloupec pro hledání</param>
        /// <param name="searchValue1">Hodnota prvního sloupce</param>
        /// <param name="searchColumn2">Druhý sloupec pro hledání</param>
        /// <param name="searchValue2">Hodnota druhého sloupce</param>
        /// <param name="idColumn">Název ID sloupce</param>
        /// <returns>ID záznamu nebo null</returns>
        public int? GetIdByColumnValue(string tableName, string searchColumn1, string searchValue1, string idColumn, string searchColumn2 = null, string searchValue2 = null)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"SELECT {idColumn} FROM {tableName} WHERE {searchColumn1} = @SearchValue1";
                if (!string.IsNullOrEmpty(searchColumn2))
                {
                    query += $" AND {searchColumn2} = @SearchValue2";
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SearchValue1", searchValue1);
                    if (!string.IsNullOrEmpty(searchColumn2))
                    {
                        cmd.Parameters.AddWithValue("@SearchValue2", searchValue2);
                    }
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? (int?)result : null;
                }
            }
        }

        /// <summary>
        /// Aktualizuje záznam v databázi
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="columnToUpdate">Název sloupce, který se aktualizuje</param>
        /// <param name="newValue">Nová hodnota</param>
        /// <param name="idColumn">Název sloupce s ID</param>
        /// <param name="id">ID záznamu k aktualizaci</param>
        public void UpdateRecord(string tableName, string columnToUpdate, object newValue, string idColumn, int id)
        {

            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"UPDATE {tableName} SET {columnToUpdate} = @NewValue WHERE {idColumn} = @Id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NewValue", newValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
