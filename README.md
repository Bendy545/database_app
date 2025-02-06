# databse_app

Tato databázová aplikace napsána v C# slouží k správě knihovny.

## Systémové požadavky

Pokud tyto aplikace nemáte tak je nainstalujte: Visual Studio, SQL Server Managment Studio

## Nastavení projektu 

1. Vytvoření databáze:
   - V SQL Server Managment Studio zmáčknete tlačítko create querry a napíšete: **create database knihovna**; a zmáčknete Execute
     
   ![createDatabase](https://github.com/user-attachments/assets/200ab74a-6b0d-4314-80f9-b373d3d15f44)

   - Po vytvoření databáze tak použijeme následující skript, který je uložený ve složce souborz pod jménem databáze.txt
   - obsah souboru zkopírujeme a v SQL Server Managment Studio vytvoříme nové querry a zkopírovaný obsah vložíme do querry a žmáčkneme Execute
     
3. Konfigurace připojení 
   - v souboru App.config je nakonfigurováno připojení k databázi pomocí Windows Authentication a nebo SQL Server Authentication. Ujistěte se že máte správně nastavený název serveru a databáze v následujícím připojovacím řetězci:
     


   ### Windows Authentication
   - Pokud k připojení do databáze používáte Windows Authentication tak 
   ### SQL Server Authentication
