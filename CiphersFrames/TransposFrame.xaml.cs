using CryptoCalc.MenuFrames;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.CiphersFrames
{
    /// <summary>
    /// Логика взаимодействия для TransposFrame.xaml
    /// </summary>
    public partial class TransposFrame : Page
    {
        public TransposFrame()
        {
            InitializeComponent();
            rbRu.IsChecked = true;
        }

        private void BtnToNaiveCiphers_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new NaiveCiphersFrame();

        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e) => 
            tbEncKey.Text = tbDecKey.Text = Transpos.GenerateKey(rbEn.IsChecked == true);

        private void RbLang_Checked(object sender, RoutedEventArgs e)
        {
            lbFreqStd.Items.Clear();
            foreach (KeyValuePair<char, double> item in Transpos.GetFreqDct(rbEn.IsChecked == true))
                lbFreqStd.Items.Add(item.Key.ToString() + " - " + item.Value.ToString());
        }

        private void BtnEnc_Click(object sender, RoutedEventArgs e)
        {
            string text = tbEnc.Text, key = tbEncKey.Text;
            tbEnc.Text = Transpos.TransposCipher(text, key, true, rbEn.IsChecked == true);
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            string text = tbDec.Text, key = tbDecKey.Text;
            tbDec.Text = Transpos.TransposCipher(text, key, false, rbEn.IsChecked == true);
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            string text = tbDecNoKey.Text;
            Dictionary<char, double> freqText = Transpos.GetTextFreq(ref text, rbEn.IsChecked == true);
            foreach (KeyValuePair<char, double> item in freqText)
                lbFreqText.Items.Add(item.Key.ToString() + " - " + item.Value.ToString("F3"));

            tbDecNoKey.Text = text;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            string text = tbDecNoKey.Text;
            tbDecNoKey.Text = Transpos.SwitchLetters(text, tbFrom.Text, tbTo.Text, rbEn.IsChecked == true, false, out bool success);
            if (success)
            {
                lbChanges.Items.Add(tbFrom.Text + " --> " + tbTo.Text);
                tbFrom.Text = tbTo.Text = "";
                lbCount.Content = lbChanges.Items.Count;
            }
        }

        private void BtnCancelLast_Click(object sender, RoutedEventArgs e)
        {
            if (lbChanges.Items.Count == 0) return;

            string change = lbChanges.Items[lbChanges.Items.Count - 1].ToString();
            string from = change[0].ToString(), to = change[change.Length - 1].ToString();

            string text = tbDecNoKey.Text;
            tbDecNoKey.Text = Transpos.SwitchLetters(text, from, to, rbEn.IsChecked == true, true, out bool success);
            if (success)
            {
                lbChanges.Items.RemoveAt(lbChanges.Items.Count - 1);
                lbCount.Content = lbChanges.Items.Count;
            }
        }

        private void BtnCancelSelected_Click(object sender, RoutedEventArgs e)
        {
            if (lbChanges.SelectedIndex == -1) return;

            string change = lbChanges.SelectedItem.ToString();
            string from = change[0].ToString(), to = change[change.Length - 1].ToString();

            string text = tbDecNoKey.Text;
            tbDecNoKey.Text = Transpos.SwitchLetters(text, from, to, rbEn.IsChecked == true, true, out bool success);
            if (success)
            {
                lbChanges.Items.RemoveAt(lbChanges.SelectedIndex);
                lbCount.Content = lbChanges.Items.Count;
            }
        }
    }
}
