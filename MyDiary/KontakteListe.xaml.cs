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
    /// <summary>
    /// Interaktionslogik für KontakteListe.xaml
    /// </summary>
    public partial class KontakteListe : Window
    {
        public KontakteListe()
        {

            InitializeComponent();
            this.kontakteListeAnzeigen();

        }

        public void kontakteListeAnzeigen()
        {
            DBConnection dBConnection = new DBConnection();
            MySqlDataReader reader = null;
 
            string query = "SELECT vorname, nachname, telefonnummer, email FROM  kontakte ORDER BY vorname ASC";
            //string query = "SELECT * FROM kontakte";
            reader = dBConnection.selectManyFieldsInTheDatabase(query);
            if(reader != null)
            {
                if(reader.HasRows)
                {
                    //MessageBox.Show("Ok");
                    System.Diagnostics.Debug.WriteLine("Ein paar Kontakte sind vorhanden");
                    while (reader.Read())
                    {
                        lv_kontakteListe.Items.Add(new Kontakt { mVorname = reader.GetString("vorname"),
                            mNachname = reader.GetString("nachname"), mTelefonnummer = reader.GetString("telefonnummer"),
                            mEmailAdresse = reader.GetString("email")
                        });
                    }
                    
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("leer");
                }
            }
            else
            {
                MessageBox.Show("Problem");
            }
            
        }
    }
}
