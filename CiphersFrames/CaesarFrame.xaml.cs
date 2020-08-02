using CryptoCalc.MenuFrames;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.CiphersFrames
{
    /// <summary>
    /// Логика взаимодействия для CaesarFrame.xaml
    /// </summary>
    public partial class CaesarFrame : Page
    {
        public CaesarFrame()
        {
            InitializeComponent();
            rbRandom.IsChecked = true;
        }

        public static string Text { get; set; }

        private void BtnToNaiveCiphers_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new NaiveCiphersFrame();

        private void RbRandom_Checked(object sender, RoutedEventArgs e)
        {
            btnDec.Content = "Показать варианты";
            lbVariants.Visibility = Visibility.Visible;
            tbDec.SetValue(Grid.ColumnSpanProperty, 1);
        }

        private void RbKey_Checked(object sender, RoutedEventArgs e)
        {
            btnDec.Content = "Расшифровать";
            lbVariants.Visibility = Visibility.Hidden;
            tbDec.SetValue(Grid.ColumnSpanProperty, 2);
        }

        private void BtnEnc_Click(object sender, RoutedEventArgs e)
        {
            Text = tbEnc.Text;
            if (rbKey.IsChecked == true && int.TryParse(tbKey.Text, out int key))
                tbEnc.Text = Caesar.CaesarCipher(Text, true, key, rbEn.IsChecked == true);
            else if (rbRandom.IsChecked == true)
                tbEnc.Text = Caesar.CaesarCipher(Text, true, new Random().Next(1, 33), rbEn.IsChecked == true);
            else MessageBox.Show("Неверные входные данные");
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            Text = tbDec.Text;
            if (rbKey.IsChecked == true && int.TryParse(tbKey.Text, out int key))
                tbDec.Text = Caesar.CaesarCipher(Text, false, key, rbEn.IsChecked == true);
            else if (rbRandom.IsChecked == true)
            {
                lbVariants.Items.Clear();
                List<RadioButton> rbs = Caesar.GetVariants(Text, rbEn.IsChecked == true, tbDec);
                foreach (RadioButton rb in rbs)
                {
                    lbVariants.Items.Add(rb);
                }
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
