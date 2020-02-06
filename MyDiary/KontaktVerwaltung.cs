using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyDiary
{
    public partial class KontaktVerwaltung : Form
    {
        public KontaktVerwaltung()
        {
            this.BackColor = Color.LightBlue;
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("ID", 100);
            listView1.Columns.Add("Vorname", 150);
            listView1.Columns.Add("Nachname", 150);
            listView1.Columns.Add("Telefonnummer", 150);
            listView1.Columns.Add("E-Mail-Adresse", 250);
            kontakteListeAnzeigen();
        }

        private void einElementzurListeHinzufuegen(string id, string vorname, string nachname, string telefonnummer, string email)
        {
            // Definition einer Zeile
            string[] zeile = {id, vorname, nachname, telefonnummer, email };
            ListViewItem item = new ListViewItem(zeile);
            listView1.Items.Add(item);
        }

        


        public void kontakteListeAnzeigen()
        {
            DBConnection dBConnection = new DBConnection();
            MySqlDataReader reader = null;

            //string query = "SELECT vorname, nachname, telefonnummer, email FROM  kontakte ORDER BY vorname ASC";
            string query = "SELECT * FROM  kontakte ORDER BY kontakt_id ASC";
            //string query = "SELECT * FROM kontakte";
            reader = dBConnection.selectManyFieldsInTheDatabase(query);
            if (reader != null)
            {
                //if (reader.HasRows)
                //{
                    listView1.Items.Clear();//Wir wollen keinen Kontakt mehrmal haben.
                    System.Diagnostics.Debug.WriteLine("Ein paar Kontakte sind vorhanden");
                    while (reader.Read())
                    {
                        einElementzurListeHinzufuegen(reader.GetString("kontakt_id"), reader.GetString("vorname"), reader.GetString("nachname"), reader.GetString("telefonnummer"), reader.GetString("email"));

                    }

                //}
                //else
                //{
                 //   System.Diagnostics.Debug.WriteLine("leer");
                //}
            }
            else
            {
                System.Windows.MessageBox.Show("Problem");
            }

        }

        private void KontaktVerwaltung_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void onClick_btn_allesloeschenKontaktVerwaltung(object sender, EventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Wollen Sie wirklich alle Ihre Kontakte löschen?", "Kontakte löschen", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string loeschenQuery = "TRUNCATE kontakte";
                DBConnection dBConnection = new DBConnection();

                string[] rslt = dBConnection.executeInsertOderUpdateQuery(loeschenQuery);
                if (rslt[0] == "ok")
                {
                    
                    kontakteListeAnzeigen();
                    setLeer();
                    System.Windows.MessageBox.Show("Alle Kontakte wurden gelöscht...");
                }
                else
                {
                    System.Windows.MessageBox.Show("Ein Problem ist aufgetretten...");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //Funktion zum Hinzufügen eines neuen Kontakts
        private void onClick_btn_hinzufuegenKontaktVerwaltung(object sender, EventArgs e)
        {
            List<string> l = new List<string>();
            l.Add("txtBox_vornameNeuenKontaktErstellen");
            l.Add("txtBox_NachnameNeuenKontaktErstellen");
            l.Add("txtBox_telefonnummerNeuenKontaktErstellen");
            l.Add("txtBox_emailNeuenKontaktErstellen");


            int testRslt = testFormular(l);
            if (testRslt == 1)
            {
                if (Controllers.istEinKontaktSchonVorhanden(txtBox_vornameNeuenKontaktErstellen.Text.ToString(), "vorname", "kontakte") &&
                    Controllers.istEinKontaktSchonVorhanden(txtBox_nachnameNeuenKontaktErstellen.Text.ToString(), "nachname", "kontakte") && Controllers.istEinKontaktSchonVorhanden(txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString(), "telefonnummer", "kontakte") && Controllers.istEinKontaktSchonVorhanden(txtBox_emailNeuenKontaktErstellen.Text.ToString(), "email", "kontakte"))
                {
                    System.Windows.MessageBox.Show("Dieser Kontakt ist schon vorhanden!");
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

                    if (tab[0] == "ok")
                    {
                        kontakteListeAnzeigen();//Die Listview wird aktualisiert.
                        System.Diagnostics.Debug.WriteLine("Der Kontakt wurde erfolgreich erstellt...");
                        System.Windows.MessageBox.Show("Der Kontakt wurde erfolgreich erstellt... ");
                        setLeer();
                       
                        

                    }
                    else
                    {
                        System.Windows.MessageBox.Show(tab[1]);
                    }
                }


                //MessageBox.Show("Alles ist okay!");
            }
        }

        //Diese Methode dient zum Leer-Machen der verschiedenen Formularfelder
        public  void setLeer()
        {
            txtBox_vornameNeuenKontaktErstellen.Text = "";
            txtBox_nachnameNeuenKontaktErstellen.Text = "";
            txtBox_emailNeuenKontaktErstellen.Text = "";
            txtBox_telefonnummerNeuenKontaktErstellen.Text = "";
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
                            error = "Der Vorname muss eingegeben werden!";
                            dict.Add("txtBox_vornameNeuenKontaktErstellen", error);
                        }
                        break;

                    case "txtBox_NachnameNeuenKontaktErstellen":


                        if (txtBox_nachnameNeuenKontaktErstellen.Text == "")
                        {
                            error = "Der Nachname muss eingegeben werden!";
                            dict.Add("txtBox_NachnameNeuenKontaktErstellen", error);
                        }
                        break;

                    case "txtBox_telefonnummerNeuenKontaktErstellen":


                        if (txtBox_telefonnummerNeuenKontaktErstellen.Text == "")
                        {
                            error = "Die Telefonnummer muss eingegeben werden!";
                            dict.Add("txtBox_telefonnummerNeuenKontaktErstellen", error);
                        }
                        else
                        {
                            if (!Controllers.testTelefonnummer(txtBox_telefonnummerNeuenKontaktErstellen.Text))
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
                List<string> tmpl = new List<string>();
                foreach (KeyValuePair<string, string> item in dict)
                {
                    // Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
                    tmpl.Add(item.Value);

                    Console.WriteLine("Erreur au niveau de: " + item.Key + " contenu erreur: " + item.Value);

                }

                //Der Rückgabewert(eine Zeichenkette) wird erzeugt.
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
                MessageBoxResult result = System.Windows.MessageBox.Show(total, caption, buttons, icon);
                //MessageBox.Show("Some text", "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            return testResult;
        }

        private void btn_aktualisierenKontaktVerwaltung_Click(object sender, EventArgs e)
        {
            //Falls kein Kontakt ausgewählt wurde...
            if (txtBox_vornameNeuenKontaktErstellen.Text.ToString() == "" && txtBox_nachnameNeuenKontaktErstellen.Text.ToString() == "" &&
                txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString() == "" &&
                txtBox_emailNeuenKontaktErstellen.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Waehlen Sie bitte einen Kontakt...");
            }
            else
                if (txtBox_vornameNeuenKontaktErstellen.Text.ToString() != "" && txtBox_nachnameNeuenKontaktErstellen.Text.ToString() != "" &&
                    txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString() != "" &&
                     txtBox_emailNeuenKontaktErstellen.Text.ToString() != "")
            {

                List<string> l = new List<string>();
                l.Add("txtBox_vornameNeuenKontaktErstellen");
                l.Add("txtBox_NachnameNeuenKontaktErstellen");
                l.Add("txtBox_telefonnummerNeuenKontaktErstellen");
                l.Add("txtBox_emailNeuenKontaktErstellen");


                int testRslt = testFormular(l);
                if (testRslt == 1)
                {


                    string id = listView1.SelectedItems[0].SubItems[0].Text;
                    string updateQuery = "UPDATE kontakte SET vorname = '" + txtBox_vornameNeuenKontaktErstellen.Text.ToString() + "', nachname = '" + txtBox_nachnameNeuenKontaktErstellen.Text.ToString() + "', telefonnummer = '" + txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString() + "', email = '" + txtBox_emailNeuenKontaktErstellen.Text.ToString() + "' WHERE kontakt_id = " + Int32.Parse(id) + "";
                    DBConnection dBConnection = new DBConnection();

                    string[] rslt = dBConnection.executeInsertOderUpdateQuery(updateQuery);
                    if (rslt[0] == "ok")
                    {

                        kontakteListeAnzeigen();
                        setLeer();
                        System.Windows.MessageBox.Show("Der Kontakt wurde erfolgreich aktualisiert...");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Ein Problem ist aufgetretten...");
                    }

                }

            }       
            
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = listView1.SelectedItems[0].SubItems[0].Text; 
            txtBox_vornameNeuenKontaktErstellen.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtBox_nachnameNeuenKontaktErstellen.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtBox_telefonnummerNeuenKontaktErstellen.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtBox_emailNeuenKontaktErstellen.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void btn_loeschenKontaktVerwaltung_Click(object sender, EventArgs e)
        {
            //Falls kein Kontakt ausgewählt wurde...
            if (txtBox_vornameNeuenKontaktErstellen.Text.ToString() == "" && txtBox_nachnameNeuenKontaktErstellen.Text.ToString() == "" &&
                txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString() == "" &&
                txtBox_emailNeuenKontaktErstellen.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Waehlen Sie bitte einen Kontakt...");
            }
            else
                if (txtBox_vornameNeuenKontaktErstellen.Text.ToString() != "" && txtBox_nachnameNeuenKontaktErstellen.Text.ToString() != "" &&
                    txtBox_telefonnummerNeuenKontaktErstellen.Text.ToString() != "" &&
                     txtBox_emailNeuenKontaktErstellen.Text.ToString() != "")
            {

                string id = listView1.SelectedItems[0].SubItems[0].Text;
                string loeschenQuery = "DELETE FROM kontakte WHERE kontakt_id = " + Int32.Parse(id) + "";
                DBConnection dBConnection = new DBConnection();

                string[] rslt = dBConnection.executeInsertOderUpdateQuery(loeschenQuery);
                if (rslt[0] == "ok")
                {
                    kontakteListeAnzeigen();
                    setLeer();
                    System.Windows.MessageBox.Show("Der Kontakt wurde erfolgreich gelöscht...");
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("Ein Problem ist aufgetretten...");
                }

            }
        }
    }
}
