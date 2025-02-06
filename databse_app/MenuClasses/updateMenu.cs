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

        public updateMenu()
        {
            _updateDAO = new updateDAO();
        }

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
                        UpdateEntity("autori", "id_au", new string[] { "jm_au", "prijm_au", "dat_nar" });
                        break;
                    case "2":
                        UpdateEntity("knihy", "id_kn", new string[] { "nazev_kn", "dat_vyd", "id_au" });
                        break;
                    case "3":
                        UpdateEntity("zakaznik", "id_za", new string[] { "jm_za", "prijm_za", "tel", "dat_nar" });
                        break;
                    case "4":
                        UpdateEntity("produkt", "id_p", new string[] { "kod_p", "id_kn" });
                        break;
                    case "5":
                        UpdateEntity("vypujcky", "id_vyp", new string[] { "dat_vypujceni", "dat_vraceni", "vraceno" });
                        break;
                    default:
                        Console.WriteLine("Neplatná volba.");
                        break;
                }
            }
        }

        private void UpdateEntity(string tableName, string idColumn, string[] columns)
        {
            Console.Write($"Zadejte ID záznamu v tabulce {tableName}: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neplatné ID.");
                return;
            }

            if (!_updateDAO.RecordExists(tableName, idColumn, id))
            {
                Console.WriteLine($"Záznam s ID {id} v tabulce {tableName} neexistuje.");
                return;
            }

            Console.WriteLine("Co chcete změnit?");
            for (int i = 0; i < columns.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {columns[i]}");
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

            object valueToUpdate;
            if (string.IsNullOrEmpty(newValue))
            {
                valueToUpdate = DBNull.Value;
            }
            else if (DateTime.TryParse(newValue, out DateTime dateValue))
            {
                valueToUpdate = dateValue;
            }
            else if (int.TryParse(newValue, out int intValue))
            {
                valueToUpdate = intValue;
            }
            else
            {
                valueToUpdate = newValue;
            }

            _updateDAO.UpdateRecord(tableName, columnToUpdate, valueToUpdate, idColumn, id);
            Console.WriteLine($"{columnToUpdate} bylo úspěšně aktualizováno.");
        }
    }
}
