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
    /// Interaktionslogik für NeuesKontoAnlegen.xaml
    /// </summary>
    /// 
    public partial class NeuesKontoAnlegen : Window
    {
        public NeuesKontoAnlegen()
        {
            InitializeComponent();
        }

        public void btn_onClick_zurückNeuesKontoAnlegen(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            this.Close();
        }

        public void onClick_btn_validierenNeuesKontoAnlegen(object sender, RoutedEventArgs e)
        {
            List<string> l = new List<string>();
            l.Add("txtBox_benutzername");
            l.Add("txtBox_passwortNeuesKonto");
            l.Add("txtBox_wiederholtesPasswortNeuesKonto");
            l.Add("txtBox_emailNeuesKontoAnlegen");
            l.Add("txtBox_wiederholteEmailNeuesKontoAnlegen");


            int testRslt = testFormular(l);
            //Es heißt alle Angaben wurden überprüft und sind korrekt.
            //Die können nun gespeichert werden.
            if (testRslt==1)
            {
                string tmpBenutzername = txtBox_benutzername.Text.ToString();
                string tmpPasswort = txtBox_passwortNeuesKonto.Password.ToString();
                string tmpEmail = txtBox_emailNeuesKontoAnlegen.Text.ToString();
                
                User users = new User(tmpBenutzername, tmpPasswort, tmpEmail);
                string query = "INSERT INTO `users` (`benutzername`, `passwort`, `email`) VALUES('"+tmpBenutzername+"','"+tmpPasswort+"','"+tmpEmail+"');";
                DBConnection dBConnection = new DBConnection();
                //int status = 0;
                string[] tab = new string[2];
                //status = dBConnection.executeInsertQuery(query);
                tab = dBConnection.executeInsertOderUpdateQuery(query);
                if(tab[0] == "ok")
                {
                    MessageBox.Show("Ihr Konto wurde erfolgreich erstellt... ");
                    (new Einloggen()).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(tab[1]);
                }

            }
        }

        //public bool testEmailAdresse(string s)
        //{
        //    Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        //    return regex.IsMatch(s);
        //}
        //Diese Funktion dient zum Testen der Felder unserer verschiedenen Formulare
        // Die liefert den Wert 1, falls alles gut passiert ist. Wenn icht, dann wird der Wert 0 ausgeliefert.
        public int testFormular(List<string> elementeListe)
        {
            
            //Man nimmt an, alles ist ok am Anfang
            int testResult = 1;
            string error;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string element in elementeListe)
            {
                switch (element)
                {
                    case "txtBox_benutzername" :

                        
                        if (txtBox_benutzername.Text == "")
                        {
                            error = "Der Benutzername muss eingegeben werden!";
                            dict.Add("txtBox_benutzername", error);
                        }
                        break;

              /*      case "txtBox_benutzernameEinloggen":


                        if (einloggen.txtBox_benutzernameEinloggen.Text == "")
                        {
                            error = "Der Benutzername muss eingegeben werden!";
                            dict.Add("txtBox_benutzernameEinloggen", error);
                        }
                        break;

                    case "txtBox_passwortEinloggen":


                        if (einloggen.txtBox_passwortEinloggen.Password == "")
                        {
                            error = "Das Passwort muss eingegeben werden!";
                            dict.Add("txtBox_benutzernameEinloggen", error);
                        }
                        break;*/

                    case "txtBox_wiederholtesPasswortNeuesKonto":
                        {
                            if (txtBox_passwortNeuesKonto.Password == "" || txtBox_wiederholtesPasswortNeuesKonto.Password == "")
                            {
                                error = "Das Passwort muss eingegeben werden!";
                                dict.Add("txtBox_passwortNeuesKonto", error);

                            }else if (txtBox_passwortNeuesKonto.Password != txtBox_wiederholtesPasswortNeuesKonto.Password)
                            {
                                error = "Die beiden Passwörter stimmen nicht!";
                                dict.Add("txtBox_passwortNeuesKonto", error);
                            }

                        }
                        break;

                    case "txtBox_wiederholteEmailNeuesKontoAnlegen":
                        {
                            //Falls ein Feld leer ist
                            if (txtBox_wiederholteEmailNeuesKontoAnlegen.Text == "" || txtBox_emailNeuesKontoAnlegen.Text == "")
                            {
                                error = "Die beiden Felder für E-Mail-Adresse müssen ausgefüllt  werden!";
                                dict.Add("txtBox_emailNeuesKontoAnlegen", error);
                            }else //Die beiden Felder sind leer
                            {
                                //Das Format ist nicht korrekt
                                if((!Controllers.testEmailAdresse(txtBox_emailNeuesKontoAnlegen.Text)) || (!Controllers.testEmailAdresse(txtBox_wiederholteEmailNeuesKontoAnlegen.Text)))
                                {
                                    error = "Überprüfen Sie bitte das Format Ihrer eingegebenen E-Mail-Adressen";
                                    dict.Add("txtBox_emailNeuesKontoAnlegen", error);
                                }
                                else
                                {
                                    //Die beiden E-Mail-Adressen stimmen nicht
                                    if (txtBox_wiederholteEmailNeuesKontoAnlegen.Text != txtBox_emailNeuesKontoAnlegen.Text)
                                    {
                                        error = "Die beiden E-Mail-Adressen stimmen nicht";
                                        dict.Add("txtBox_emailNeuesKontoAnlegen", error);
                                    }
                                }

                            }

                        }
                        break;

                }
            }

            int anzahlDerElemente = dict.Count;
            if(anzahlDerElemente>0)
            {
                //Es heißt, es gibt ein Problem irgendwo
                testResult = 0;
                string[] tmp = new string[anzahlDerElemente];
                List<string> tmpl =new List<string>();
                foreach (KeyValuePair<string, string> item in dict)
                {
                    // Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
                    tmpl.Add(item.Value);

                    
 
                }
                string total = "";
                for(int i = 0; i<tmpl.Count; i++)
                {
                    total = total + tmpl[i]+"\n \n";
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
