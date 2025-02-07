using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.MenuClasses
{
    internal class importMenu
    {

        private CSV csv;

        public importMenu()
        {
            csv = new CSV();
        }

        public void ImportData()
        {

            Console.WriteLine("Vyberte typ souboru pro import:");
            Console.WriteLine("1 - Import autorů");
            Console.WriteLine("2 - Import zákazníků");
            Console.Write("Vyberte možnost: ");

            string volba = Console.ReadLine();

            string soubor;
            switch (volba)
            {
                case "1":
                    soubor = @"autori.csv";
                    csv.ImportAuthors(soubor);
                    break;
                case "2":
                    soubor = @"zakaznici.csv";
                    csv.ImportCustomers(soubor);
                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
