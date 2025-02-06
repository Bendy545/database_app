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

        public bool RecordExists(string tableName, string columnName, int id)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @Id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public void UpdateRecord(string tableName, string columnToUpdate, object newValue, string idColumn, int id)
        {
            if (!RecordExists(tableName, idColumn, id))
            {
                Console.WriteLine($"{tableName} s ID {id} neexistuje.");
                return;
            }

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
