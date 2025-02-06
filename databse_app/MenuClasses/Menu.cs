using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace databse_app.MenuClasses
{
    internal class Menu
    {
        private insertMenu _insertMenu;
        private deleteMenu _deleteMenu;
        private updateMenu _updateMenu;
        private selectMenu _selectMenu;
        private importMenu _importMenu;
        public Menu()
        {
            _insertMenu = new insertMenu();
            _deleteMenu = new deleteMenu();
            _updateMenu = new updateMenu();
            _selectMenu = new selectMenu();
            _importMenu = new importMenu();
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("--- MENU ---");
                Console.WriteLine("1 - Přidat data");
                Console.WriteLine("2 - Smazat data");
                Console.WriteLine("3 - upravit data");
                Console.WriteLine("4 - zobrazeni dat");
                Console.WriteLine("5 - import dat z csv");
                Console.WriteLine("6 - Konec");
                Console.Write("Vyberte možnost: ");

                string volba = Console.ReadLine();

                switch (volba)
                {
                    case "1":
                        _insertMenu.ShowInsertMenu();
                        break;
                    case "2":
                        _deleteMenu.ShowDeleteMenu();
                        break;
                    case "3":
                        _updateMenu.ShowUpdateMenu();
                        break;
                    case "4":
                        _selectMenu.ShowSelectMenu();
                        break;
                    case "5":
                        _importMenu.ImportData();
                        break;
                    case "6": 
                        return;
                    default:
                        Console.WriteLine("Neplatna volba, zkuzte to znovu");
                        break;
                }
            }
        }
    }
}
