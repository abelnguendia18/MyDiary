using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MyDiary
{
    public partial class DBConnection
    {
        //Alles, was etwas mit Datenbank zu tun hat, wird in dieser Klasse implementiert.
        //Da diese Klasse "partial" ist, kann ein anderer  dessen Teil irgendwo implementiert werden.
            public const string DB_NAME = "mydiary_db";
            public const string ADRESSE = "127.0.0.1";
            public const string USER = "root";
            public const string PASSWORT = "";
            public const int PORT = 3306;
            MySqlConnection myConnection;
            MySqlDataReader myReader;
            MySqlCommand commandDatabase;
            //static string error = "";

        //Methode zur Verbindung mit der Datenbank
        public MySqlConnection verbindungMitDerDatenbank()
            {

                string url = "datasource=" + ADRESSE + ";port=" + PORT + ";username=" + USER + ";password=" + PASSWORT + ";database=" + DB_NAME;

                MySqlConnection datenbankVerbindung = new MySqlConnection(url);


                return datenbankVerbindung;
            }
            // Diese Funktion liefert den String "ok", falls alles gut gelaufen ist; und "ko" anderenfalls .
            //public int executeInsertQuery(string query)
            public string [] executeInsertOderUpdateQuery(string query)
            {
            //int status = 1;
                string status = "ok";
                string error = "";
                string[] tab = new string[2];
                myConnection = verbindungMitDerDatenbank();
                
                commandDatabase = new MySqlCommand(query, myConnection);
                try
                {
                    myConnection.Open();
                    System.Diagnostics.Debug.WriteLine("Verbindung mit der Datenbank ist erfolgreich gewesen...");
               
                    myReader = commandDatabase.ExecuteReader();



                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Fehler bei der Verbindung mit der Datenbank: "+e.Message);
                    error = "Fehler bei der Verbindung mit der Datenbank: " + e.Message;
                    // Ein Problem ist beim Ausführen des Query aufgetreten.
                    status = "ko";
                }
                myConnection.Close();
                tab[0] = status;
                tab[1] = error;
                return tab;
            }


        
        public string executeSelectQuery(string inWelchemFeld, string inWelcherTabelle)
        {
             string query = "SELECT " +inWelchemFeld+" FROM "+inWelcherTabelle;
             string ergebnis = "";

             myConnection = verbindungMitDerDatenbank();
            
             commandDatabase = new MySqlCommand(query, myConnection);
            try
            {
                myConnection.Open();
                System.Diagnostics.Debug.WriteLine("Verbindung mit der Datenbank ist erfolgreich gewesen...");

                myReader = commandDatabase.ExecuteReader();
                if(myReader.HasRows)
                {

                    while(myReader.Read())
                    {
                        ergebnis = ergebnis+ myReader.GetString(inWelchemFeld)+" ";
                    }
                }




            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Fehler bei der Verbindung mit der Datenbank: " + e.Message);
                // Ein Problem ist beim Ausführen des Query aufgetreten.
                //status = 0;
            }
            myConnection.Close();
            return ergebnis;
        }

        public MySqlDataReader selectManyFieldsInTheDatabase(string query)
        {
            MySqlDataReader tmp_DataReader = null;
            myConnection = verbindungMitDerDatenbank();
            commandDatabase = new MySqlCommand(query, myConnection);
            try
                {
                  myConnection.Open();
                  System.Diagnostics.Debug.WriteLine("Verbindung mit der Datenbank ist erfolgreich gewesen...");
                  tmp_DataReader = commandDatabase.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Fehler bei der Verbindung mit der Datenbank: " + e.Message);
            }

            //myConnection.Close();

            return tmp_DataReader;
        }




    }
}

