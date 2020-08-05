using CryptoCalc.MenuFrames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CryptoCalc.OtherFrames
{
    /// <summary>
    /// Логика взаимодействия для SecretFrame.xaml
    /// </summary>
    public partial class SecretFrame : Page
    {
        public SecretFrame() => InitializeComponent();

        private void BtnToOther_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new OtherFrame();

        private void BtnSplit_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbN.Text, out int n) && int.TryParse(tbT.Text, out int t) &&
                int.TryParse(tbP.Text, out int p) && int.TryParse(tbK.Text, out int k))
            {
                if (t > n)
                {
                    MessageBox.Show("Пороговое значение должно быть меньше числа участников");
                    return;
                }
                if (p <= n || p <= k || !Algorithms.IsPrime(p))
                {
                    MessageBox.Show("P должно быть простым числом, больше числа участников и скрываемого ключа");
                    return;
                }

                List<int> func_coefs = Algorithms.GenerateF(n, t, k, p, out Dictionary<int, int> subkeys);
                string func = "";
                for (int i = t - 1; i > 0; i--)
                    func += func_coefs[(t - 1) - i] + "*x^" + i + " + ";
                func += k.ToString();
                tbF.Text = func;

                lbSubkeys.Items.Clear();
                foreach (KeyValuePair<int, int> subkey in subkeys)
                    lbSubkeys.Items.Add(subkey.Key.ToString() + "," + subkey.Value.ToString());
            }
            else
            {
                MessageBox.Show("Неверные входные значения");
                return;
            }
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbT_dec.Text, out int t) && int.TryParse(tbP_dec.Text, out int p))
            {
                if (!Algorithms.IsPrime(p))
                {
                    MessageBox.Show("P должно быть простым числом");
                    return;
                }

                for (int i = 0; i < t; i++)
                {
                    TextBox tb = new TextBox()
                    {
                        Text = "",
                        Width = 200
                    };
                    lbSubekys_dec.Items.Add(tb);
                }
                btnRestore.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Неверные входные значения");
                return;
            }
        }

        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbT_dec.Text, out int t) && int.TryParse(tbP_dec.Text, out int p))
            {
                if (!Algorithms.IsPrime(p))
                {
                    MessageBox.Show("P должно быть простым числом");
                    return;
                }

                Dictionary<int, int> subkeyes = new Dictionary<int, int>();
                Regex subkey_format = new Regex(@"^[1-9][0-9]*,[0-9]+$");
                foreach (TextBox tb in lbSubekys_dec.Items)
                {
                    string s = tb.Text;
                    int i = s.IndexOf(',');
                    if (subkey_format.IsMatch(s))
                        subkeyes.Add(int.Parse(s.Substring(0, i)), int.Parse(s.Substring(i + 1, s.Length - i - 1)));
                    else
                    {
                        MessageBox.Show("Подключ " + s + " введён неверно");
                        return;
                    }
                }
                int k = rbSLE.IsChecked == true ? Algorithms.Gaussian(subkeyes, t, p) : Algorithms.Lagrange(subkeyes, p);
                tbK_dec.Text = k.ToString();
            }
            else
            {
                MessageBox.Show("Неверные входные значения");
                return;
            }
        }
    }
}
