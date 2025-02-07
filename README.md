# database_app

Tato databázová aplikace napsána v C# slouží k správě knihovny.

## Systémové požadavky

Pokud tyto aplikace nemáte tak je nainstalujte: Visual Studio, SQL Server Managment Studio

## Nastavení projektu 

1. Vytvoření databáze:
   - V SQL Server Managment Studio klikneme na tlačítko create querry a napíšeme: **create database knihovna**; a poté klikneme na Execute
     
![createDatabase](https://github.com/user-attachments/assets/200ab74a-6b0d-4314-80f9-b373d3d15f44)

   - Po vytvoření databáze tak použijeme následující skript, který je uložený ve složce soubory pod jménem databaze.txt
   - obsah souboru zkopírujeme a v SQL Server Managment Studio vytvoříme nové querry a zkopírovaný obsah vložíme do querry a klikneme Execute

2. Vytvoření projektu
   - zkopírujeme URL repozitáře a ve visual studiu zvolíme možnost clone repository kam následně vložíme URL a vytvoříme projekt
   - aby projekt fungoval tak musíme nainstalovat následující package: System.Configuration.ConfigurationManager, System.Data.SqlClient, CsvHelper
   - tyto package muzeme vyhledat a nainstalovat pomoci NuGet Package Manager ve Visual Studio
3. Konfigurace připojení 
   - v souboru App.config je nakonfigurováno připojení k databázi pomocí Windows Authentication a nebo SQL Server Authentication. Ujistěte se že máte správně nastavený název serveru a databáze v následujícím připojovacím řetězci:
     
![ConfFile](https://github.com/user-attachments/assets/01b7d4af-a7d0-4c4c-99a3-4e800420a108)


   ### Windows Authentication
   - Pokud k připojení do databáze používáte Windows Authentication tak vyplňte **Data Source**: názvem vašeho serveru, **Initial Catalog**: název databáze = knihovna
   ### SQL Server Authentication
   - Pokud k připojení do databáze používáte SQL Server Authentication tak vyplňte **Server**: název serveru, **Database**: název databáze, **User ID**: název uživatele, **Password**: heslo

   ### Konfigurace třídy Singleton.cs
   - V této třídě přepíšete zvýrazněné pole buď na "WindowsAuthentication" a nebo na "SqlAuthentication"
   
![SingletonClass](https://github.com/user-attachments/assets/d77031b8-f75a-4cc5-8643-d7cf8d4e2088)
