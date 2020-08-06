using CryptoCalc.MenuFrames;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.OtherFrames
{
    /// <summary>
    /// Логика взаимодействия для CRSFrame.xaml
    /// </summary>
    public partial class CRSFrame : Page
    {
        public CRSFrame() => InitializeComponent();

        private void BtnToOther_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new OtherFrame();

        private static List<int> GetCoefsFromString(string s)
        {
            List<int> coefs = new List<int>();
            string[] coefs_string = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string coef_string in coefs_string)
            {
                if (int.TryParse(coef_string, out int coef)) coefs.Add(coef);
                else return null;
            }

            return coefs;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbP.Text, out int p))
            {
                List<int> coefs = GetCoefsFromString(tbCoefs.Text);
                if (coefs == null)
                {
                    MessageBox.Show("Коэффициенты указаны неверно");
                    return;
                }

                for (int i = 0; i < coefs.Count - 1; i++)
                {
                    TextBox tb = new TextBox
                    {
                        Width = 60,
                        Text = "",
                        Margin = new Thickness(2, 0, 2, 0)
                    };
                    wpElements.Children.Add(tb);
                }
                btnStart.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Значение P указано неверно");
                return;
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            List<int> LRP = new List<int>();
            foreach (TextBox tb in wpElements.Children)
            {
                if (int.TryParse(tb.Text, out int element)) LRP.Add(element);
                else
                {
                    MessageBox.Show("Элементы последовательности указаны неверно");
                    return;
                }
            }
            if (int.TryParse(tbP.Text, out int p))
            {
                List<int> coefs = GetCoefsFromString(tbCoefs.Text);
                if (coefs == null)
                {
                    MessageBox.Show("Коэффициенты указаны неверно");
                    return;
                }

                LRP = Algorithms.Get_LRP(LRP, coefs, p, out int per);

                string result = "";
                for (int i = 0; i < LRP.Count; i++)
                    result += LRP[i].ToString() + " ";

                tbSeq.Text = result;
                lbPer.Content = per.ToString();
            }
            else
            {
                MessageBox.Show("Значение P указано неверно");
                return;
            }
        }
    }
}
