using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace MyDiary
{

    public partial class Einloggen : Window
    {
        public Einloggen()
        {
            InitializeComponent();
        }

        public void btn_validierenEinloggenClick(object sender, RoutedEventArgs e)
        {
            
            string queryStatusBenutzernamen = "";
            string queryStatusPasswoerter = "";
            int testFormulareStatus = 0;
            string query = "";
            DBConnection dBConnection = new DBConnection();
            List<string> listeElemente = new List<string>();         
            string tmpBenutzername = txtBox_benutzernameEinloggen.Text.ToString();
            string tmpPasswort = txtBox_passwortEinloggen.Password.ToString();
            listeElemente.Add("txtBox_benutzernameEinloggen");
            listeElemente.Add("txtBox_passwortEinloggen");
            testFormulareStatus = testFormulare(listeElemente);
            //if(InternetChecker.IsInternetConnection())
            //{
            //    MessageBox.Show("ok");
            //}
            //else
            //{
            //    MessageBox.Show("Ko");
            //}
            //Wenn die Eingabedaten korrekt sind, ...
            if(testFormulareStatus ==1)
            {
                
                queryStatusBenutzernamen = dBConnection.executeSelectQuery("benutzername","users");
                queryStatusPasswoerter = dBConnection.executeSelectQuery("passwort", "users");
                char[] separ = {' '};
                string[] tmpTabelleBenutzernamen = Controllers.convertStringIntoArray(queryStatusBenutzernamen);
                string[] tmpTabellePasswoerter = Controllers.convertStringIntoArray(queryStatusPasswoerter);

                //Wenn der Benutzer existiert, ...
                if (Controllers.istDasElementInDerTabelleEnthalten(tmpTabelleBenutzernamen, txtBox_benutzernameEinloggen.Text.ToString()))
                {
                    //Und wenn das Passwort korrekt ist, ...
                    if(Controllers.istDasElementInDerTabelleEnthalten(tmpTabellePasswoerter, txtBox_passwortEinloggen.Password.ToString()))
                    {
                        HomeWindow homeWindow = new HomeWindow(txtBox_benutzernameEinloggen.Text.ToString());
                        homeWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Das Passwort ist falsch...");
                    }
                }
                else
                {
                    MessageBox.Show("Dieser Benutzer existiert leider nicht.");
                }


            }

           



            //(new HomeWindow()).Show();
            //this.Close();
        }

        public void btn_zurückEinloggenClick(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            this.Close();
        }



        public int testFormulare(List<string> elementeListe)
        {

            //Man nimmt an, alles ist ok am Anfang
            int testResult = 1;
            string error;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string element in elementeListe)
            {
                switch (element)
                {
                    case "txtBox_benutzernameEinloggen":


                        if (txtBox_benutzernameEinloggen.Text.Trim() == "")
                        {
                            error = "Der Benutzername muss eingegeben werden!";
                            dict.Add("txtBox_benutzernameEinloggen", error);
                        }
                        break;

                    case "txtBox_passwortEinloggen":

                            if(txtBox_passwortEinloggen.Password.Trim() == "")
                            {
                                 error = "Das Passwort muss eingegeben werden!";
                                 dict.Add("txtBox_passwortEinloggen", error);
                        }

                        break;



                }

            }

            int anzahlDerElemente = dict.Count;
            if (anzahlDerElemente > 0)
            {
                //Es heißt, es gibt ein Problem irgendwo
                testResult = 0;
                string[] tmp = new string[anzahlDerElemente];
                List<string> tmpl = new List<string>();
                foreach (KeyValuePair<string, string> item in dict)
                {
                    // Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
                    tmpl.Add(item.Value);

                    Console.WriteLine("Erreur au niveau de: " + item.Key + " contenu erreur: " + item.Value);

                }
                string total = "";
                for (int i = 0; i < tmpl.Count; i++)
                {
                    total = total + tmpl[i] + "\n \n";
                }
                // MessageBox.Show(total);
                // Configure message box
                string caption = "Falsche oder Fehlende Angaben";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                // Show message box
                MessageBoxResult result = MessageBox.Show(total, caption, buttons, icon);
                //MessageBox.Show("Some text", "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            return testResult;
        }




    }

}
