using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace MyDiary
{
    /// <summary>
    /// Interaktionslogik für MeinTagebuch.xaml
    /// </summary>
    public partial class MeinTagebuch : Window
    {
        public MeinTagebuch()
        {
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

        public void onClick_btn_neueNotizErstellen(object sender, RoutedEventArgs e)
        {
            (new NeueNotizErstellen()).Show();
            this.Close();
        }

        private void onClick_btn_gespeicherteNotizenAnschauen(object sender, RoutedEventArgs e)
        {
            NotizenAnzeiger notizenAnzeiger = new NotizenAnzeiger();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

            // this is the path that you are checking.
            string path = @"C:\MyDiary\Meine_Notizen"; 
           
            if (Directory.Exists(path))
            {
                dlg.InitialDirectory = path;
            }
            else
            {
                dlg.InitialDirectory = @"C:\";
            }

            //dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|Word Files(*.docx)|*.docx|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)

            {
                RichTextBox richTextBox = new RichTextBox();
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(notizenAnzeiger.rtb_eineNotiznzeigen.Document.ContentStart, notizenAnzeiger.rtb_eineNotiznzeigen.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                notizenAnzeiger.Show();
                fileStream.Close();
            }
        }
    }
}
