using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app
{
    internal class Singleton
    {

    private static string _connectionString;

    static Singleton()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["WindowsAuthentication"].ConnectionString;
    }

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}

}
