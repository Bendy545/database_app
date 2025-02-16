using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databse_app.DAO;

namespace databse_app.MenuClasses
{
    internal class selectMenu
    {
        private selectDAO _selectDAO;

        public selectMenu()
        {
            _selectDAO = new selectDAO();
        }

        /// <summary>
        /// zobrazí menu pro zobrazení dat z databáze
        /// </summary>
        public void ShowSelectMenu() 
        {
            Console.WriteLine("\n--- Select MENU ---");
            Console.WriteLine("1 - Zobrazit autory");
            Console.WriteLine("2 - Zobrazit knihy");
            Console.WriteLine("3 - Zobrazit produkty");
            Console.WriteLine("4 - Zobrazit zákazníky");
            Console.WriteLine("5 - Zobrazit vypůjčky");
            Console.WriteLine("6 - Zpět");
            Console.Write("Vyberte možnost: ");

            string volba = Console.ReadLine();

            switch (volba)
            {
                case "1":
                    _selectDAO.GetAutori();
                    break;
                case "2":
                    _selectDAO.GetKnihy();
                    break;
                case "3":
                    _selectDAO.GetProdukty();
                    break;
                case "4":
                    _selectDAO.GetZakaznici();
                    break;
                case "5":
                    _selectDAO.GetVypujcky();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Neplatná volba, zkuste to znovu.");
                    break;
            }
        }
    }
}
