using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databse_app.DAO;

namespace databse_app.MenuClasses
{
    internal class insertMenu
    {
        private insertDAO _insertDAO;

        public insertMenu()
        {
            _insertDAO = new insertDAO();
        }

        /// <summary>
        /// zobrazí menu pro vložení dat do databáze
        /// </summary>
        public void ShowInsertMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Insert MENU ---");
                Console.WriteLine("1 - Přidat autora");
                Console.WriteLine("2 - Přidat knihu");
                Console.WriteLine("3 - Přidat zákazníka");
                Console.WriteLine("4 - Přidat produkt");
                Console.WriteLine("5 - Přidat výpůjčku");
                Console.WriteLine("6 - Zpět");
                Console.Write("Vyberte možnost: ");

                switch (Console.ReadLine())
                {
                    case "1": 
                        AddAutor();
                        break;
                    case "2": 
                        AddKniha(); 
                        break;
                    case "3": 
                        AddZakaznik(); 
                        break;
                    case "4": 
                        AddProdukt(); 
                        break;
                    case "5": 
                        AddVypujcka(); 
                        break;
                    case "6": return;
                    default: Console.WriteLine("Neplatná volba, zkuste to znovu"); break;
                }
            }
        }

        /// <summary>
        /// Umožňuje přidat nového autora
        /// </summary>
        private void AddAutor()
        {
            Console.Write("Jméno autora: ");
            string jmeno = Console.ReadLine();
            Console.Write("Příjmení autora: ");
            string prijmeni = Console.ReadLine();
            Console.Write("Datum narození (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datumNarozeni))
            {
                Console.WriteLine("Neplatný formát data.");
                return;
            }
            _insertDAO.AddAutor(jmeno, prijmeni, datumNarozeni);
            Console.WriteLine("Autor přidán.");
        }

        /// <summary>
        /// Umožňuje přidat novou knihu 
        /// </summary>
        private void AddKniha()
        {
            Console.Write("Název knihy: ");
            string nazev = Console.ReadLine();
            Console.Write("Datum vydání (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datumVydani))
            {
                Console.WriteLine("Neplatný formát data.");
                return;
            }
            Console.Write("Jméno autora: ");
            string jmenoAutora = Console.ReadLine();

            Console.Write("Příjmení autora: ");
            string prijmeniAutora = Console.ReadLine();
            try
            {
                _insertDAO.AddKniha(nazev, datumVydani, jmenoAutora, prijmeniAutora);
                Console.WriteLine("Kniha přidána.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při přidávání knihy: {ex.Message}");
            }
        }

        /// <summary>
        /// Umožňuje přidat nového zákazníka
        /// </summary>
        private void AddZakaznik()
        {
            Console.Write("Jméno zákazníka: ");
            string jmeno = Console.ReadLine();
            Console.Write("Příjmení zákazníka: ");
            string prijmeni = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Datum narození (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datumNarozeni))
            {
                Console.WriteLine("Neplatný formát data.");
                return;
            }
            _insertDAO.AddZakaznik(jmeno, prijmeni, telefon, datumNarozeni);
            Console.WriteLine("Zákazník přidán.");
        }

        /// <summary>
        /// Umožňuje přidat nový produkt do databáze
        /// </summary>
        private void AddProdukt()
        {
            Console.Write("Kód produktu: ");
            if (!int.TryParse(Console.ReadLine(), out int kod))
            {
                Console.WriteLine("Neplatný kód produktu.");
                return;
            }
            Console.Write("Název knihy: ");
            string nazevKnihy = Console.ReadLine();

            Console.Write("Datum vydání knihy (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datumVydani))
            {
                Console.WriteLine("Neplatný formát data.");
                return;
            }
            _insertDAO.AddProdukt(kod, nazevKnihy, datumVydani);
        }

        /// <summary>
        /// Umožnuje přidat novou výpujčku do databáze
        /// </summary>
        private void AddVypujcka()
        {
            Console.Write("Datum výpůjčky (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime datumVypujceni))
            {
                Console.WriteLine("Neplatný formát data.");
                return;
            }

            Console.Write("Datum vrácení (YYYY-MM-DD, Enter pro neznámé): ");
            string datVraceniInput = Console.ReadLine();
            DateTime? datumVraceni = string.IsNullOrEmpty(datVraceniInput) ? null : DateTime.Parse(datVraceniInput);

            Console.Write("Vráceno? (1 = Ano, 0 = Ne): ");
            bool vraceno = Console.ReadLine() == "1";

            Console.Write("Kód produktu: ");
            if (!int.TryParse(Console.ReadLine(), out int kodProdukt))
            {
                Console.WriteLine("Neplatný kód produktu.");
                return;
            }

            Console.Write("Jméno zákazníka: ");
            string jmenoZakaznika = Console.ReadLine();

            Console.Write("Příjmení zákazníka: ");
            string prijmeniZakaznika = Console.ReadLine();

            Console.Write("Telefon zákazníka: ");
            string telefonZakaznika = Console.ReadLine();

            _insertDAO.AddVypujcka(datumVypujceni, datumVraceni, vraceno, kodProdukt, jmenoZakaznika, prijmeniZakaznika, telefonZakaznika);
        }
    }
}
