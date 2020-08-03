using CryptoCalc.MenuFrames;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoCalc.CiphersFrames
{
    /// <summary>
    /// Логика взаимодействия для VigenereFrame.xaml
    /// </summary>
    public partial class VigenereFrame : Page
    {
        public VigenereFrame()
        {
            InitializeComponent();
            rbRu.IsChecked = true;
        }

        private void BtnToNaiveCiphers_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new NaiveCiphersFrame();

        private void RbLang_Checked(object sender, RoutedEventArgs e)
        {
            lbFreqStd.Items.Clear();
            foreach (KeyValuePair<char, double> item in Vigenere.GetFreqDct(rbEn.IsChecked == true))
                lbFreqStd.Items.Add(item.Key.ToString() + " - " + item.Value.ToString());
        }

        private void BtnEnc_Click(object sender, RoutedEventArgs e)
        {
            string text = tbEnc.Text, key = tbEncKey.Text;
            tbEnc.Text = Vigenere.Encrypt(text, key, true, rbEn.IsChecked == true);
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            string text = tbDec.Text, key = tbDecKey.Text;
            tbDec.Text = Vigenere.Encrypt(text, key, false, rbEn.IsChecked == true);
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            tbLengths.Text = "";
            string text = tbDecNoKey.Text;
            List<int> lengths = Vigenere.GetPossibleLengths(ref text, rbEn.IsChecked == true);
            tbDecNoKey.Text = text;
            Vigenere.DefaultTextForDec = text;
            foreach (int len in lengths) tbLengths.Text += len.ToString() + " ";
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbSelectedLength.Text, out int len))
            {
                MessageBox.Show("Введена неверная длина ключа");
                return;
            }

            Vigenere.KeyLength = len;
            btnClear.IsEnabled = true;
            spKey.Children.Clear();
            cbxPosition.Items.Clear();

            for (int i = 0; i < len; i++)
            {
                Label l = new Label()
                {
                    Content = "_",
                    Width = 32
                };
                spKey.Children.Add(l);
                cbxPosition.Items.Add(i + 1);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbDecNoKey.Text = Vigenere.DefaultTextForDec;
            spKey.Children.Clear();
            cbxPosition.Items.Clear();
        }

        private void CbxPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPosition.SelectedIndex == -1) return;
            lbFreqText.Items.Clear();
            foreach (KeyValuePair<char, double> item in
                Vigenere.GetFreqText(rbEn.IsChecked == true, cbxPosition.SelectedIndex))
            {
                lbFreqText.Items.Add(item.Key.ToString() + " - " + item.Value.ToString("F3"));
            }
        }

        private void LbFreqText_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int def_index = lbFreqStd.SelectedIndex, new_index = lbFreqText.SelectedIndex, 
                position = cbxPosition.SelectedIndex;
            if (def_index == -1 || new_index == -1 || position == -1) return;

            int shift = def_index <= new_index ? new_index - def_index : lbFreqStd.Items.Count - def_index + 1;

            string text = tbDecNoKey.Text;
            tbDecNoKey.Text = Vigenere.ChangeLetters(text, shift, position, rbEn.IsChecked == true, out char keyChar);
            (spKey.Children[position] as Label).Content = keyChar;
        }
    }
}
