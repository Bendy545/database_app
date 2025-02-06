using databse_app.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app
{
    internal class CSV
    {
        private insertDAO _insertDAO;

        public CSV()
        {
            _insertDAO = new insertDAO();
        }
        public void ImportAuthors(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            {

                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 3)
                    {
                        string jmAu = values[0];
                        string prijmAu = values[1];
                        DateTime datNar;

                        if (DateTime.TryParse(values[2], out datNar))
                        {
                            _insertDAO.AddAutor(jmAu, prijmAu, datNar);
                        }
                        else
                        {
                            Console.WriteLine($"Neplatné datum narození pro autora: {jmAu} {prijmAu}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Neplatný formát CSV řádku pro autora: {line}");
                    }
                }
            }
        }

        public void ImportCustomers(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 4)
                    {
                        string jmZa = values[0];
                        string prijmZa = values[1];
                        string tel = values[2];
                        DateTime datNar;

                        if (DateTime.TryParse(values[3], out datNar))
                        {
                            _insertDAO.AddZakaznik(jmZa, prijmZa, tel, datNar);
                        }
                        else
                        {
                            Console.WriteLine($"Neplatné datum narození pro zákazníka: {jmZa} {prijmZa}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Neplatný formát CSV řádku pro zákazníka: {line}");
                    }
                }
            }
        }
    }
}
