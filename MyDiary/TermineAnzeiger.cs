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
    public partial class TermineAnzeiger : Form
    {

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public TermineAnzeiger()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(15);
            timer.Start();
            timer.Tick += checkAlarms;

            this.BackColor = Color.LightBlue;
            lv_termine.View = View.Details;
            lv_abgelaufeneTermine.View = View.Details;
            lv_termine.FullRowSelect = true;
            lv_termine.Columns.Add("Termin-ID", 100);
            lv_termine.Columns.Add("Titel", 300);
            lv_termine.Columns.Add("Uhrzeit", 150);
            lv_abgelaufeneTermine.Columns.Add("Termin-ID", 100);
            lv_abgelaufeneTermine.Columns.Add("Titel", 300);
            lv_abgelaufeneTermine.Columns.Add("Uhrzeit", 150);

            termineListeAnzeigen(lv_termine, 0);// 0 für vorgemerkte Termine
            termineListeAnzeigen(lv_abgelaufeneTermine, 1);// 1 für abgelaufene Termine

        }
        // Der Wecker wird von dieser Methode verwaltet
        public void checkAlarms(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_termine.Items.Count; i++)
            {
                int tmpId = Int32.Parse(lv_termine.Items[i].SubItems[0].Text);
                string tmpTitel = lv_termine.Items[i].SubItems[1].Text;
                string tmpUhrzeit = lv_termine.Items[i].SubItems[2].Text;
                string[] result = convertStringToArray(tmpUhrzeit);
                int tmpStunden = Int32.Parse(result[0]);
                int tmpMinuten = Int32.Parse(result[1]);
                Termin einTermin = new Termin(tmpTitel, tmpStunden, tmpMinuten);
                DBConnection dBConnection = new DBConnection();
                //MySqlDataReader reader = null;
                //Die Abfrage wird vorbereitet...
                string updateQuery = "UPDATE termine SET termin_status = '1' WHERE termin_id ="+tmpId+"; ";


                if (einTermin.isActive())// Es heißt, es ist Zeit
                {
                    einTermin.klingeln();

                    // Der Termin ist vorbei und wird dann in die Liste der abgelaufenen Termine verschoben.
                    string[] result2 = dBConnection.executeInsertOderUpdateQuery(updateQuery);
                    if (result2[0] == "ok")
                    {
                        //lv_abgelaufeneTermine.Clear();
                        //lv_termine.Clear();
                        // Die beiden Listview werden dann aktualisiert...
                        termineListeAnzeigen(lv_termine, 0);
                        termineListeAnzeigen(lv_abgelaufeneTermine, 1);
                    }else
                    {
                        System.Windows.Forms.MessageBox.Show("Problem beim Aktualisieren der Listview der abgelaufenen Termine");
                    }
                }
               
            }
        }



        private void einElementzurListeHinzufuegen(ListView eineListView, string id, string titel, string uhrzeit)
        {
            // Definition einer Zeile
            string[] zeile = { id, titel, uhrzeit };
            ListViewItem item = new ListViewItem(zeile);
            eineListView.Items.Add(item);
        }
        // Es git 2 ListView, je nach dem Status des Termins(0 für vorgemerkte Termine und 1 für abgelaufene Termine) wird 
        //entscheidet, in welcher Listview das Element gespeichert wird...
        public void termineListeAnzeigen(ListView eineListView, int termin_status)
        {
            DBConnection dBConnection = new DBConnection();
            MySqlDataReader reader = null;

            string query = "SELECT * FROM  termine  WHERE termin_status = '"+termin_status+"' ORDER BY termin_id ASC ;";

            reader = dBConnection.selectManyFieldsInTheDatabase(query);
            if (reader != null)
            {
                //if (reader.HasRows)
                //{
                eineListView.Items.Clear();//Wir wollen keinen Termin mehrmals haben.
                System.Diagnostics.Debug.WriteLine("Ein paar Termine sind vorhanden");
                while (reader.Read())
                {
                    einElementzurListeHinzufuegen(eineListView, reader.GetString("termin_id"), reader.GetString("titel"), reader.GetUInt32("termin_h")+":"+reader.GetUInt32("termin_m"));

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

        private void Termine_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btn_termineHinzufuegen_Click(object sender, EventArgs e)
        {
            List<string> liste = new List<string>();
            liste.Add("txtBox_titel");
            liste.Add("txtBox_stunden");
            liste.Add("txtBox_minuten");
          int i = testFormular(liste);
            if(i == 1)// Alle Informationen sind korrekt
            {
                string tmpTitel = txtBox_titel.Text.ToString();
                int tmpStunden, tmpMinuten;
                Int32.TryParse(txtBox_stunden.Text, out tmpStunden);
                Int32.TryParse(txtBox_minuten.Text, out tmpMinuten);


                string query = "INSERT INTO `termine` (`titel`, `termin_h`, `termin_m`) VALUES('" + tmpTitel + "','" + tmpStunden + "','" + tmpMinuten + "');";
                DBConnection dBConnection = new DBConnection();
                string[] tab = new string[2];
                tab = dBConnection.executeInsertOderUpdateQuery(query);
                if(tab[0] == "ok")
                {
                    termineListeAnzeigen(lv_termine, 0);
                    System.Windows.MessageBox.Show("Der Termin wurde erfolgreich erstellt...");
                    setLeer();
                }
                else
                {
                    System.Windows.MessageBox.Show(tab[1]);
                }
            }
        }


        //Diese Methode dient zum Leer-Machen der verschiedenen Formularfelder
        public void setLeer()
        {
            txtBox_titel.Text = "";
            txtBox_stunden.Text = "";
            txtBox_minuten.Text = "";
         
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

                    case "txtBox_titel":


                        if (txtBox_titel.Text == "")
                        {
                            error = "Der Titel muss eingegeben werden!";
                            dict.Add("txtBox_titel", error);
                        }
                        break;

                    case "txtBox_stunden":


                        if (txtBox_stunden.Text.Trim() == "")
                        {
                            error = "Das Feld für Stunden muss mit einer Zahl zwischen 0 und 23 ausgefüllt werden!";
                            dict.Add("txtBox_stunden", error);
                        }
                        else // Das Feld ist nicht leer
                        {
                            int stunden;
                            // Check if the point entered is numeric or not
                            if (Int32.TryParse(txtBox_stunden.Text, out stunden))
                            {   //Eine Zahl wurde eingegeben...
                                if (0 <= stunden && stunden <= 23)
                                {
                                   // System.Windows.MessageBox.Show("" + stunden);
                                }
                                else
                                {
                                    error = "Das Feld für Stunden muss mit einer Zahl zwischen 0 und 23 ausgefüllt werden!";
                                    dict.Add("txtBox_stunden", error);
                                }
                                // Do what you want to do if numeric
                                //System.Windows.MessageBox.Show(""+stunden);
                            }
                            else
                            {
                                // Do what you want to do if not numeric
                               //System.Windows.Forms.MessageBox.Show("non numeric");
                                error = "Das Feld für Stunden muss mit einer Zahl zwischen 0 und 23 ausgefüllt werden!";
                                dict.Add("txtBox_stunden", error);
                            }

                        }
                        break;

                    case "txtBox_minuten":


                        if (txtBox_minuten.Text == "")
                        {
                            error = "Das Feld für Minuten muss mit einer Zahl zwischen 0 und 59 ausgefüllt werden!";
                            dict.Add("txtBox_minuten", error);
                        }
                        else
                        {
                            int minuten;
                            // Check if the point entered is numeric or not
                            if (Int32.TryParse(txtBox_minuten.Text, out minuten))
                            {
                                // Do what you want to do if numeric
                                if(0 <= minuten && minuten <= 59)
                                {
                                    //System.Windows.MessageBox.Show("" + minuten);
                                }
                                else
                                {
                                    error = "Das Feld für Minuten muss mit einer Zahl zwischen 0 und 59 ausgefüllt werden!";
                                    dict.Add("txtBox_minuten", error);
                                }
                                
                            }
                            else
                            {
                                // Do what you want to do if not numeric
                               // System.Windows.Forms.MessageBox.Show("non numeric");
                                error = "Das Feld für Minuten muss mit einer Zahl zwischen 0 und 23 ausgefüllt werden!";
                                dict.Add("txtBox_minuten", error);
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
                System.Windows.MessageBoxButton buttons = MessageBoxButton.OK;
                System.Windows.MessageBoxImage icon = System.Windows.MessageBoxImage.Exclamation;
                // Show message box
                System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show(total, caption, buttons, icon);
                //MessageBox.Show("Some text", "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            return testResult;
        }


        private void mouseClick_lv_termine(object sender, MouseEventArgs e)
        {
            // Stunden und Minuten werden Konkateniert und zur Listview hinzugefügt, deshalb soll man die trennen
            string[] tmp = convertStringToArray(lv_termine.SelectedItems[0].SubItems[2].Text.ToString()); 
            string id = lv_termine.SelectedItems[0].SubItems[0].Text;
            txtBox_titel.Text = lv_termine.SelectedItems[0].SubItems[1].Text;
            txtBox_stunden.Text = tmp[0];
            txtBox_minuten.Text = tmp[1];
            
            
        }

        public  string [] convertStringToArray(string zeichenkette)
        {
            
            char [] separator = { ':' };
            string [] result = zeichenkette.Split(separator);
            return result;

        }

        private void btn_termineLoeschen_Click(object sender, EventArgs e)
        {

            //Falls kein Termin ausgewählt wurde...
            if (txtBox_titel.Text.ToString() == "" && txtBox_stunden.Text.ToString() == "" &&
                txtBox_minuten.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Wählen Sie bitte einen Termin aus...");
            }
            else
                if (txtBox_titel.Text.ToString() != "" && txtBox_stunden.Text.ToString() != "" &&
                    txtBox_minuten.Text.ToString() != "")
            {

                string id = lv_termine.SelectedItems[0].SubItems[0].Text;
                string loeschenQuery = "DELETE FROM termine WHERE termin_id = " + Int32.Parse(id) + "";
                DBConnection dBConnection = new DBConnection();

                string[] rslt = dBConnection.executeInsertOderUpdateQuery(loeschenQuery);
                if (rslt[0] == "ok")
                {
                    termineListeAnzeigen(lv_termine, 0);
                    setLeer();
                    System.Windows.MessageBox.Show("Der Termin wurde erfolgreich gelöscht...");

                }
                else
                {
                    System.Windows.MessageBox.Show("Ein Problem ist aufgetretten...");
                }

            }

        }

        private void gb_termine_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
