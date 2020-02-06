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
using Microsoft.Win32;//damit OpenfileDialog funktioniert
using System.IO;

namespace MyDiary
{
    /// <summary>
    /// Interaktionslogik für NeueNotizErstellen.xaml
    /// </summary>
    public partial class NeueNotizErstellen : Window
    {
        public NeueNotizErstellen()
        {
            InitializeComponent();
            datePicker_neueNotizErstellen.SelectedDate = DateTime.Today;
            cmb_fontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmb_fontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }


        
        public void onClick_btn_zurückNeueNotizErstellen(object sender, RoutedEventArgs e)
        {
            (new MeinTagebuch()).Show();
            this.Close();
        }

        public void onClick_btn_speichernNeueNotizErstellen(object sender, RoutedEventArgs e)
        {
            string myText = new TextRange(rtb_editorNeueNotizErstelen.Document.ContentStart, rtb_editorNeueNotizErstelen.Document.ContentEnd).Text;
            if (myText.Trim() == "")
            {
                MessageBox.Show("Ein leeres Dokument kann nicht gespeichert werden.");
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
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
                dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                dlg.FileName = "Notiz_von_" + datePicker_neueNotizErstellen.Text.ToString();
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtb_editorNeueNotizErstelen.Document.ContentStart, rtb_editorNeueNotizErstelen.Document.ContentEnd);
                    try
                    {
                        range.Save(fileStream, DataFormats.Rtf);
                        MessageBox.Show("Die Notiz wurde erfolgreich gespeichert...");
                        rtb_editorNeueNotizErstelen.Document.Blocks.Clear();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Es ist ein Problem augetretten.");
                    }


                }
            }
        }

        public void TxtBox_neueNotizenErstellen_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       //public void onClick_btn_attachNeueNotizErstellen(object sender, RoutedEventArgs e)
       // {
       //     Image oneImage = new Image();
       //     BitmapImage src = new BitmapImage();
       //     OpenFileDialog openFileDialog = new OpenFileDialog();
       //     src.BeginInit();
       //     if (openFileDialog.ShowDialog() == true)
       //     {
       //         try
       //         {
       //             src.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
       //             src.EndInit();
       //             oneImage.Source = src;
       //             oneImage.Stretch = Stretch.Uniform;
       //             //oneImage.Height = 80;
       //             imagesPanel.Children.Add(oneImage);
       //             // imagesPanel.Children.Add(new Separator());
       //         }
       //         catch(Exception)
       //         {
       //             MessageBox.Show("Sie können nur Fotos auswählen ");
       //         }
       //     }
       // }

       //Diese Funktion dient zum Öffnen einer Datei, die sich irgendwo im lokalen Speicher befindet. 
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
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
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            //dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|Word Files(*.docx)|*.docx|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
               
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                
                TextRange range = new TextRange(rtb_editorNeueNotizErstelen.Document.ContentStart, rtb_editorNeueNotizErstelen.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        //Wenn man den eigegebenen Text speichern will
            private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
            {


                string myText = new TextRange(rtb_editorNeueNotizErstelen.Document.ContentStart,        rtb_editorNeueNotizErstelen.Document.ContentEnd).Text;
                if (myText.Trim()=="")
                {
                    MessageBox.Show("Ein leeres Dokument kann nicht gespeichert werden.");
                }
                else
                {
                SaveFileDialog dlg = new SaveFileDialog();
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
                dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                dlg.FileName ="Notiz_von_"+ datePicker_neueNotizErstellen.Text.ToString();
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtb_editorNeueNotizErstelen.Document.ContentStart, rtb_editorNeueNotizErstelen.Document.ContentEnd);
                    try
                    {
                        range.Save(fileStream, DataFormats.Rtf);
                        MessageBox.Show("Die Notiz wurde erfolgreich gespeichert...");
                        rtb_editorNeueNotizErstelen.Document.Blocks.Clear();
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("Es ist ein Problem augetretten.");
                    }
                    

                }

            }

        }

        private void cmb_fontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_fontFamily.SelectedItem != null)
                rtb_editorNeueNotizErstelen.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmb_fontFamily.SelectedItem);
        }

        private void cmb_fontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtb_editorNeueNotizErstelen.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmb_fontSize.Text);
        }

        private void rtb_editorNeueNotizErstellen_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtb_editorNeueNotizErstelen.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btn_boldNeueNotizErstellen.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtb_editorNeueNotizErstelen.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btn_italicNeueNotizErstellen.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtb_editorNeueNotizErstelen.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btn_underlineNeueNotizErstellen.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtb_editorNeueNotizErstelen.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmb_fontFamily.SelectedItem = temp;
            temp = rtb_editorNeueNotizErstelen.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmb_fontSize.Text = temp.ToString();
        }
    }

}
