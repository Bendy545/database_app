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
        /// <param name="searchColumn">Název sloupce</param>
        /// <param name="searchValue">Hledaná hodnota</param>
        /// <param name="idColumn">Název sloupce s ID</param>
        /// <returns>ID záznamu</returns>
        public int? GetIdByColumnValue(string tableName, string searchColumn, string searchValue, string idColumn)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query;

                if (int.TryParse(searchValue, out _))
                {
                    query = $"SELECT {idColumn} FROM {tableName} WHERE {searchColumn} = @SearchValue";
                }
                else
                {
                    query = $"SELECT {idColumn} FROM {tableName} WHERE {searchColumn} COLLATE Latin1_General_CI_AI = @SearchValue";
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
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
