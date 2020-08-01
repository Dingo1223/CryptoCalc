using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для CongrFrame.xaml
    /// </summary>
    public partial class CongrFrame : Page
    {
        public CongrFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();
        
        private void BtnCongr_Solve_Click(object sender, RoutedEventArgs e)
        {
            BigInteger d = 0, x = 0;
            if (BigInteger.TryParse(tbCongr_A.Text, out BigInteger a) &&
                BigInteger.TryParse(tbCongr_B.Text, out BigInteger b) &&
                BigInteger.TryParse(tbCongr_N.Text, out BigInteger n))
            {
                if (rbCongr_RAE.IsChecked == true) d = Algorithms.Modular_RAE(a, b, n, ref x);
                else d = Algorithms.Modular_Rev(a, b, n, ref x);
                if (d == 0) MessageBox.Show("Нет решений!");
                else
                {
                    tbCongr_X1.Text = x.ToString();
                    tbCongr_X.Text = x.ToString() + " + " + n.ToString() + "/" + d.ToString() + " * t";
                }
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
