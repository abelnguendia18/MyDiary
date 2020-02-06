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

namespace MyDiary
{
    /// <summary>
    /// Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        string mPseudo { get; set; }
        public HomeWindow()
        {
            InitializeComponent();
        }

        public HomeWindow(string pseudo)
        {
            mPseudo = pseudo;
            InitializeComponent();
        }

          

        public void onClick_btn_meinTagebuch(object sender, RoutedEventArgs e)
        {
            (new MeinTagebuch()).Show();
            this.Close();
          
        }
        public void onClick_btn_meineKontakte(object sender, RoutedEventArgs e)
        {
            (new KontaktVerwaltung()).ShowDialog();
            //this.Close();
        }

        public void onClick_btn_termineUndErinnerungen(object sender, RoutedEventArgs e)
        {
           (new TermineAnzeiger()).ShowDialog();
        }

        //public void onClick_btn_eMails(object sender, RoutedEventArgs e)
        //{
        //    (new E_Mails()).Show();
        //    this.Close();
        //}
    }
}
