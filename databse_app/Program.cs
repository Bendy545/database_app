using System.Data.SqlClient;
using databse_app.MenuClasses;

namespace databse_app
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Menu menu = new Menu();
            menu.Start();
        }
    }
}
