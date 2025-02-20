using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databse_app.DAO
{
    internal class insertDAO
    {


        public insertDAO()
        {

        }

        /// <summary>
        /// Přidá nového autora do databáze
        /// </summary>
        /// <param name="jmeno">Jmébo autora</param>
        /// <param name="prijmeni">Příjmení autora</param>
        /// <param name="datumNarozeni">Datum narození autora</param>
        public void AddAutor(string jmeno, string prijmeni, DateTime datumNarozeni)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO autori (jm_au, prijm_au, dat_nar) values (@Jmeno, @Prijmeni, @DatumNarozeni)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@DatumNarozeni", datumNarozeni);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá knihu do databáze
        /// </summary>
        /// <param name="nazev">název knihy</param>
        /// <param name="datumVydani">datum vydání knihy</param>
        /// <param name="jmenoAutora">jméno autora</param>
        /// <param name="prijmeniAutora">prijmeni autora</param>
        /// <exception cref="Exception">pokud autor neexistuje v databázi</exception>
        public void AddKniha(string nazev, DateTime datumVydani, string jmenoAutora, string prijmeniAutora)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                connection.Open();

                // Hledání ID autora podle jména a příjmení
                string findAuthorQuery = "SELECT id_au FROM autori WHERE jm_au = @Jmeno AND prijm_au = @Prijmeni";
                SqlCommand findAuthorCmd = new SqlCommand(findAuthorQuery, connection);
                findAuthorCmd.Parameters.AddWithValue("@Jmeno", jmenoAutora);
                findAuthorCmd.Parameters.AddWithValue("@Prijmeni", prijmeniAutora);

                object result = findAuthorCmd.ExecuteScalar();

                if (result == null)
                {
                    throw new Exception("Autor nebyl nalezen v databázi.");
                }

                int autorId = Convert.ToInt32(result);

                // Přidání knihy s nalezeným ID autora
                string insertBookQuery = "INSERT INTO knihy (nazev_kn, dat_vyd, id_au) VALUES (@Nazev, @DatumVydani, @AutorId)";
                SqlCommand insertBookCmd = new SqlCommand(insertBookQuery, connection);
                insertBookCmd.Parameters.AddWithValue("@Nazev", nazev);
                insertBookCmd.Parameters.AddWithValue("@DatumVydani", datumVydani);
                insertBookCmd.Parameters.AddWithValue("@AutorId", autorId);

                insertBookCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá nového zákazníka do databáze
        /// </summary>
        /// <param name="jmeno">Jméno zákazníka</param>
        /// <param name="prijmeni">Příjmení zákazníka</param>
        /// <param name="telefon">Telefon zákazníka</param>
        /// <param name="datumNarozeni">Datum narození zákazníka</param>
        public void AddZakaznik(string jmeno, string prijmeni, string telefon, DateTime datumNarozeni)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                string query = "INSERT INTO zakaznik (jm_za, prijm_za, tel, dat_nar) VALUES (@Jmeno, @Prijmeni, @Telefon, @DatumNarozeni)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@Telefon", telefon);
                cmd.Parameters.AddWithValue("@DatumNarozeni", datumNarozeni);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Přidá nový produkt do databáze
        /// </summary>
        /// <param name="kod">kód produktu</param>
        /// <param name="nazevKnihy">název knihy</param>
        /// <param name="datumVydani">datum vydání knihy</param>
        public void AddProdukt(int kod, string nazevKnihy, DateTime datumVydani)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                connection.Open();
                string findQuery = "SELECT id_kn FROM knihy WHERE nazev_kn = @NazevKnihy AND dat_vyd = @DatumVydani";
                using (SqlCommand findCmd = new SqlCommand(findQuery, connection))
                {
                    findCmd.Parameters.AddWithValue("@NazevKnihy", nazevKnihy);
                    findCmd.Parameters.AddWithValue("@DatumVydani", datumVydani);

                    object result = findCmd.ExecuteScalar();
                    int? idKniha = result != null ? Convert.ToInt32(result) : (int?)null;

                    if (idKniha == null)
                    {
                        Console.WriteLine("Kniha s tímto názvem a datem vydání nebyla nalezena.");
                        return;
                    }
                    string insertQuery = "INSERT INTO produkt (kod_p, id_kn) VALUES (@Kod, @IdKniha)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@Kod", kod);
                        insertCmd.Parameters.AddWithValue("@IdKniha", idKniha);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Produkt úspěšně přidán.");
            }
        }

        /// <summary>
        /// Přidá novou výpůjčku do databáze
        /// </summary>
        /// <param name="datumVypujceni">datum vypůjčení</param>
        /// <param name="datumVraceni">datum vrácení (null, pokud neni vracena)</param>
        /// <param name="vraceno">True, pokud bylo vráceno, false pokud nebyla vracena</param>
        /// <param name="kodProdukt">kód produktu</param>
        /// <param name="jmenoZakaznika">jméno zákazníka</param>
        /// <param name="prijmeniZakaznika">příjmení zákazníka</param>
        /// <param name="telefonZakaznika">telefon zákazníka</param>
        public void AddVypujcka(DateTime datumVypujceni, DateTime? datumVraceni, bool vraceno, int kodProdukt, string jmenoZakaznika, string prijmeniZakaznika, string telefonZakaznika)
        {
            using (SqlConnection connection = Singleton.GetConnection())
            {
                connection.Open();
                string queryProdukt = "SELECT id_p FROM produkt WHERE kod_p = @KodProdukt";
                SqlCommand cmdProdukt = new SqlCommand(queryProdukt, connection);
                cmdProdukt.Parameters.AddWithValue("@KodProdukt", kodProdukt);

                object resultProdukt = cmdProdukt.ExecuteScalar();
                if (resultProdukt == null)
                {
                    Console.WriteLine("Chyba: Produkt s tímto kódem neexistuje.");
                    return;
                }
                int idProdukt = Convert.ToInt32(resultProdukt);

                string queryZakaznik = "SELECT id_za FROM zakaznik WHERE jm_za = @Jmeno AND prijm_za = @Prijmeni AND tel = @Telefon";
                SqlCommand cmdZakaznik = new SqlCommand(queryZakaznik, connection);
                cmdZakaznik.Parameters.AddWithValue("@Jmeno", jmenoZakaznika);
                cmdZakaznik.Parameters.AddWithValue("@Prijmeni", prijmeniZakaznika);
                cmdZakaznik.Parameters.AddWithValue("@Telefon", telefonZakaznika);

                object resultZakaznik = cmdZakaznik.ExecuteScalar();
                if (resultZakaznik == null)
                {
                    Console.WriteLine("Chyba: Zákazník s těmito údaji neexistuje.");
                    return;
                }
                int idZakaznik = Convert.ToInt32(resultZakaznik);

                string queryVypujcka = "INSERT INTO vypujcky (dat_vypujceni, dat_vraceni, vraceno, id_p, id_za) VALUES (@DatumVypujceni, @DatumVraceni, @Vraceno, @IdProdukt, @IdZakaznik)";
                SqlCommand cmdVypujcka = new SqlCommand(queryVypujcka, connection);
                cmdVypujcka.Parameters.AddWithValue("@DatumVypujceni", datumVypujceni);
                cmdVypujcka.Parameters.AddWithValue("@DatumVraceni", datumVraceni.HasValue ? (object)datumVraceni.Value : DBNull.Value);
                cmdVypujcka.Parameters.AddWithValue("@Vraceno", vraceno);
                cmdVypujcka.Parameters.AddWithValue("@IdProdukt", idProdukt);
                cmdVypujcka.Parameters.AddWithValue("@IdZakaznik", idZakaznik);
                cmdVypujcka.ExecuteNonQuery();
                Console.WriteLine("Výpůjčka úspěšně přidána.");
            }
        }
    }
}
