using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databse_app.DAO;

namespace databse_app.MenuClasses
{
    internal class updateMenu
    {
        private updateDAO _updateDAO;

        private static readonly Dictionary<string, string> ColumnNames = new Dictionary<string, string>
{
    { "jm_au", "Jméno autora" },
    { "prijm_au", "Příjmení autora" },
    { "dat_nar", "Datum narození" },
    { "nazev_kn", "Název knihy" },
    { "dat_vyd", "Datum vydání" },
    { "jm_za", "Jméno zákazníka" },
    { "prijm_za", "Příjmení zákazníka" },
    { "tel", "Telefon" },
    { "kod_p", "Kód produktu" }

};

        public updateMenu()
        {
            _updateDAO = new updateDAO();
        }

        /// <summary>
        /// zobrazí menu pro aktualizaci záznamů v databázi
        /// </summary>
        public void ShowUpdateMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Update MENU ---");
                Console.WriteLine("1 - Upravit autora");
                Console.WriteLine("2 - Upravit knihu");
                Console.WriteLine("3 - Upravit zákazníka");
                Console.WriteLine("4 - Upravit produkt");
                Console.WriteLine("5 - Upravit výpůjčku");
                Console.WriteLine("6 - Zpět");
                Console.Write("Vyberte možnost: ");

                string volba = Console.ReadLine();
                if (volba == "6") break;

                switch (volba)
                {
                    case "1":
                        UpdateEntity("autori", "id_au", new string[] { "jm_au", "prijm_au", "dat_nar" }, "jm_au");
                        break;
                    case "2":
                        UpdateEntity("knihy", "id_kn", new string[] { "nazev_kn", "dat_vyd", "id_au" }, "nazev_kn");
                        break;
                    case "3":
                        UpdateEntity("zakaznik", "id_za", new string[] { "jm_za", "prijm_za", "tel", "dat_nar" }, "jm_za");
                        break;
                    case "4":
                        UpdateEntity("produkt", "id_p", new string[] { "kod_p", "id_kn" }, "kod_p");
                        break;
                    case "5":
                        UpdateEntity("vypujcky", "id_vyp", new string[] { "dat_vypujceni", "dat_vraceni", "vraceno" });
                        break;
                }
            }
        }


        /// <summary>
        /// Umožňuje aktualizaci záznamu v databázi.
        /// </summary>
        /// <param name="tableName">Název tabulky</param>
        /// <param name="idColumn">Sloupec s ID záznamu</param>
        /// <param name="columns">Pole názvů sloupců, které můžeme aktualizovat</param>
        /// <param name="searchColumn">sloupec pro vyhledání záznamu</param>
        private void UpdateEntity(string tableName, string idColumn, string[] columns, string searchColumn = null)
        {
            string searchColumnDisplay = searchColumn != null && ColumnNames.ContainsKey(searchColumn) ? ColumnNames[searchColumn] : searchColumn ?? idColumn;
            string searchValue, searchValue2 = null;

            if (tableName == "zakaznik" || tableName == "autori")
            {
                Console.Write("Zadejte jméno: ");
                searchValue = Console.ReadLine();
                Console.Write("Zadejte příjmení: ");
                searchValue2 = Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Zadejte {searchColumnDisplay}");
                searchValue = Console.ReadLine();
            }

            int? id = null;
            if (searchColumn != null)
            {
                if (searchValue2 != null)
                {
                    id = _updateDAO.GetIdByColumnValue(tableName, "jm_" + tableName.Substring(0, 2), searchValue, idColumn, "prijm_" + tableName.Substring(0, 2), searchValue2);
                }
                else
                {
                    id = _updateDAO.GetIdByColumnValue(tableName, searchColumn, searchValue, idColumn);
                }
            }
            else if (int.TryParse(searchValue, out int parsedId))
            {
                id = parsedId;
            }

            if (id == null)
            {
                Console.WriteLine($"Záznam nenalezen.");
                return;
            }

            Console.WriteLine("Co chcete změnit?");
            for (int i = 0; i < columns.Length; i++)
            {
                string displayName = ColumnNames.ContainsKey(columns[i]) ? ColumnNames[columns[i]] : columns[i];
                Console.WriteLine($"{i + 1} - {displayName}");
            }
            Console.Write("Vyberte možnost: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > columns.Length)
            {
                Console.WriteLine("Neplatná volba.");
                return;
            }

            string columnToUpdate = columns[choice - 1];

            Console.Write($"Zadejte novou hodnotu pro {columnToUpdate}: ");
            string newValue = Console.ReadLine();

            object valueToUpdate = string.IsNullOrEmpty(newValue) ? DBNull.Value :
                DateTime.TryParse(newValue, out DateTime dateValue) ? dateValue :
                int.TryParse(newValue, out int intValue) ? intValue :
                newValue;

            _updateDAO.UpdateRecord(tableName, columnToUpdate, valueToUpdate, idColumn, id.Value);
            Console.WriteLine($"{columnToUpdate} bylo úspěšně aktualizováno.");
        }
    }
}
