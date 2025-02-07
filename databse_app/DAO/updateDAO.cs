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
