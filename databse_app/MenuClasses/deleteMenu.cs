using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databse_app.DAO;

namespace databse_app.MenuClasses
{
    internal class deleteMenu
    {
        private deleteDAO _deleteDAO;

        public deleteMenu()
        {
            _deleteDAO = new deleteDAO();
        }
        public void ShowDeleteMenu()
        {
            Console.WriteLine("\n--- Delete MENU ---");
            Console.WriteLine("1 - Smazat autora");
            Console.WriteLine("2 - Smazat knihu");
            Console.WriteLine("3 - Smazat zákazníka");
            Console.WriteLine("4 - Smazat produkt");
            Console.WriteLine("5 - Smazat výpůjčku");
            Console.WriteLine("6 - Zpět");
            Console.Write("Vyberte možnost: ");

            string volba = Console.ReadLine();

            if (volba == "6") return;

            Console.Write("Zadejte ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neplatné ID");
                return;
            }

            switch (volba)
            {
                case "1":
                    _deleteDAO.deleteAutor(id);
                    break;
                case "2":
                    _deleteDAO.deleteKniha(id);
                    break;
                case "3":
                    _deleteDAO.deleteZakaznik(id);
                    break;
                case "4":
                    _deleteDAO.deleteProdukt(id);
                    break;
                case "5":
                    _deleteDAO.deleteVypujcka(id);
                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
