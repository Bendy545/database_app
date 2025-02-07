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
            Console.WriteLine("1 - Smazat autora (jméno a příjmení)");
            Console.WriteLine("2 - Smazat knihu (název)");
            Console.WriteLine("3 - Smazat zákazníka (jméno a příjmení)");
            Console.WriteLine("4 - Smazat produkt (kód produktu)");
            Console.WriteLine("5 - Smazat výpůjčku (ID)");
            Console.WriteLine("6 - Zpět");
            Console.Write("Vyberte možnost: ");

            string volba = Console.ReadLine();
            if (volba == "6") return;

            switch (volba)
            {
                case "1":
                    Console.Write("Zadejte jméno autora: ");
                    string jmenoAutor = Console.ReadLine();
                    Console.Write("Zadejte příjmení autora: ");
                    string prijmeniAutor = Console.ReadLine();
                    _deleteDAO.deleteAutor(jmenoAutor, prijmeniAutor);
                    break;

                case "2":
                    Console.Write("Zadejte název knihy: ");
                    string nazevKnihy = Console.ReadLine();
                    _deleteDAO.deleteKniha(nazevKnihy);
                    break;

                case "3":
                    Console.Write("Zadejte jméno zákazníka: ");
                    string jmenoZakaznik = Console.ReadLine();
                    Console.Write("Zadejte příjmení zákazníka: ");
                    string prijmeniZakaznik = Console.ReadLine();
                    _deleteDAO.deleteZakaznik(jmenoZakaznik, prijmeniZakaznik);
                    break;

                case "4":
                    Console.Write("Zadejte kód produktu: ");
                    string kodProduktu = Console.ReadLine();
                    _deleteDAO.deleteProdukt(kodProduktu);
                    break;

                case "5":
                    Console.Write("Zadejte ID výpůjčky: ");
                    if (!int.TryParse(Console.ReadLine(), out int idVypujcky))
                    {
                        Console.WriteLine("Neplatné ID");
                        return;
                    }
                    _deleteDAO.deleteVypujcka(idVypujcky);
                    break;

                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
