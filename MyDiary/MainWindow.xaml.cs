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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDiary
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btn_sichRegistrierenClick(object sender, RoutedEventArgs e)
        {
            
            (new NeuesKontoAnlegen()).Show();
            this.Close();
        }
        public void btn_sichEinloggenClick(object sender, RoutedEventArgs e)
        {
            
            (new Einloggen()).Show();
            this.Close();
        }
    }
}
