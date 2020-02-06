using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyDiary
{
    //In dieser Klasse werden ein paar Methoden zur Überprüfung von verschiedenen Feldern dieser App implementiert.
    class Controllers
    {
        //Ist das Format der E-Mail-Adresse korrekt?
        public static bool testEmailAdresse(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        //Ist das Format der eingegebenen Telefonnummer korrekt?
        public static bool testTelefonnummer(string telefonnumer)
        {
            Regex regex = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");
            return regex.IsMatch(telefonnumer);

        }


        //An dieser Stelle wird geprüft, ob ein Element in einer Tabelle der Datenbank enthalten ist.
        public static bool istDasElementInDerTabelleEnthalten(string[] tabelle, string element)
        {
            bool rslt = false;
            if (tabelle.Contains(element))
            {
                rslt = true;
            }
            else
            {
                rslt = false;
            }
            return rslt;
        }



        public static string[] convertStringIntoArray(string oneString)
        {
            char[] separ = { ' ' };
            string[] result = oneString.Split(separ);
            return result;
        }



        public static bool istEinKontaktSchonVorhanden(string wertZumTesten, string feldInDerTabelle, string tabelle)
        {
            bool result = false;
            string rsltQuery = "";
            //string feldInDerTabelle = "";
            DBConnection dBConnection = new DBConnection();
            //rsltQuery = dBConnection.executeSelectQuery("telefonnummer", tabelle);
            rsltQuery = dBConnection.executeSelectQuery(feldInDerTabelle, tabelle);
            string[] tabNummers = Controllers.convertStringIntoArray(rsltQuery);
            result = Controllers.istDasElementInDerTabelleEnthalten(tabNummers, wertZumTesten);
            return result;
        }

    }
}
