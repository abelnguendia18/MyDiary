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
using System.Text.RegularExpressions;

namespace MyDiary
{
    /// <summary>
    /// Interaktionslogik für NeuenKontaktErstellen.xaml
    /// </summary>

     public partial class NeuenKontaktErstellen : Window
        {
        public NeuenKontaktErstellen()
        {
            InitializeComponent();
        }


        public void onClick_btn_speichernNeuenKontaktErstellen(object sender, RoutedEventArgs e)
        {
            (new KontaktVerwaltung()).ShowDialog();
            List<string> l = new List<string>();
            l.Add("txtBox_vornameNeuenKontaktErstellen");
            l.Add("txtBox_NachnameNeuenKontaktErstellen");
            l.Add("txtBox_telefonnummerNeuenKontaktErstellen");
            l.Add("txtBox_emailNeuenKontaktErstellen");


            int testRslt = testFormular(l);
            if(testRslt == 1)
            {
                if(Controllers.istEinKontaktSchonVorhanden(txtBox_vornameNeuenKontaktErstellen.Text.ToString(), "vorname", "kontakte") &&
                    Controllers.istEinKontaktSchonVorhanden(txtBox_nachnameNeuenKontaktErstellen.Text.ToString(), "nachname", "kontakte") && Controllers.istEinKontaktSchonVorhanden(txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString(), "telefonnummer", "kontakte") && Controllers.istEinKontaktSchonVorhanden(txtBox_emailNeuenKontaktErstellen.Text.ToString(), "email", "kontakte"))
                {
                    MessageBox.Show("Dieser Kontakt ist schon vorhanden!");
                }
                else
                {
                    string tmpVorname = txtBox_vornameNeuenKontaktErstellen.Text.ToString();
                    string tmpNachname = txtBox_nachnameNeuenKontaktErstellen.Text.ToString();
                    string tmpTelefonnumer = txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString();
                    string tmpEmailAdresse = txtBox_emailNeuenKontaktErstellen.Text.ToString();
                   // Kontakt kontakt = new Kontakt(tmpVorname, tmpNachname, tmpTelefonnumer, tmpEmailAdresse);
                    string query = "INSERT INTO `kontakte` (`vorname`, `nachname`, `telefonnummer`,`email`) VALUES('" + tmpVorname + "','" + tmpNachname + "','" + tmpTelefonnumer + "','" + tmpEmailAdresse + "');";
                    DBConnection dBConnection = new DBConnection();
                    int status = 0;
                    string[] tab = new string[2];
                    tab = dBConnection.executeInsertOderUpdateQuery(query);
                    //status = dBConnection.executeInsertQuery(query);

                    if (tab[0] =="ok")
                    {
                        System.Diagnostics.Debug.WriteLine("Der Kontakt wurde erfolgreich erstellt...");
                         MessageBox.Show("Der Kontakt wurde erfolgreich erstellt... ");
                        (new MeineKontakte()).Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(tab[1]);
                    }
                }


                //MessageBox.Show("Alles ist okay!");
            }
        }

        public void onClick_btn_abbrechenNeuenKontaktErstellen(object sender, RoutedEventArgs e)
        {
            (new MeineKontakte()).Show();
            this.Close();
        }




        //Diese Funtion dient zum Testen der Felden unserer verschiedenen Formulare
        // Die liefert den Wert 1, falls alles gut passiert ist. Wenn icht, dann wird der Wert 0 ausgeliefert.
        public int testFormular(List<string> elementeListe)
        {

            //Man nimmt an, alles ist nicht ok am Anfang
            int testResult = 1;
            string error;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string element in elementeListe)
            {
                switch (element)
                {

                    case "txtBox_vornameNeuenKontaktErstellen":


                        if (txtBox_vornameNeuenKontaktErstellen.Text == "")
                        {
                            error = "Ihr Vorname muss eingegeben werden!";
                            dict.Add("txtBox_vornameNeuenKontaktErstellen", error);
                        }
                        break;

                    case "txtBox_NachnameNeuenKontaktErstellen":


                        if (txtBox_nachnameNeuenKontaktErstellen.Text == "")
                        {
                            error = "Ihr Nachname muss eingegeben werden!";
                            dict.Add("txtBox_nachnameNeuenKontaktErstellen", error);
                        }
                        break;

                    case "txtBox_telefonnummerNeuenKontaktErstellen":


                        if (txtBox_telefonnummerNeuenKontaktErstellen.Text == "")
                        {
                            error = "Ihre Telefonnummer muss eingegeben werden!";
                            dict.Add("txtBox_telefonnummerNeuenKontaktErstellen", error);
                        }else
                        {
                            if(!Controllers.testTelefonnummer(txtBox_telefonnummerNeuenKontaktErstellen.Text))
                            {
                                error = "Das Format der Telefonnummer ist nicht korrekt. Ein Beispiel wäre: +491234567890";
                                dict.Add("txtBox_telefonnummerNeuenKontaktErstellen", error);
                            }

                        }
                        break;
                        

                        case "txtBox_emailNeuenKontaktErstellen":


                        if (txtBox_emailNeuenKontaktErstellen.Text == "")
                        {
                            error = "Die E-Mail-Adresse muss eingegeben werden!";
                            dict.Add("txtBox_emailNeuenKontaktErstellen", error);
                        }
                        else
                        {
                            if (!Controllers.testEmailAdresse(txtBox_emailNeuenKontaktErstellen.Text))
                            {
                                error = "Überprüfen Sie bitte das Format der eigegebenen E-Mail-Adresse!";
                                dict.Add("txtBox_emailNeuenKontaktErstellen", error);
                            }

                        }
                        break;


                }
            }

            int anzahlDerElemente = dict.Count;
            if (anzahlDerElemente > 0)
            {
                //Es heißt, es gibt ein Problem irgendwo
                testResult = 0;
                //string[] tmp = new string[anzahlDerElemente];
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
