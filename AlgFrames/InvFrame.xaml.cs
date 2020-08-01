using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для InvFrame.xaml
    /// </summary>
    public partial class InvFrame : Page
    {
        public InvFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();
        
        private void BtnInv_Solve_Click(object sender, RoutedEventArgs e)
        {
            BigInteger res = 0;
            if (BigInteger.TryParse(tbInv_A.Text, out BigInteger a) &&
                BigInteger.TryParse(tbInv_N.Text, out BigInteger n))
            {
                if (rbInv_RAE.IsChecked == true) res = Algorithms.Reverse_RAE(a, n);
                else
                {
                    if (!Algorithms.IsPrime(n))
                    {
                        MessageBox.Show("Модуль должен быть простым");
                        return;
                    }
                    res = Algorithms.Reverse_Pow(a, n);
                }
                tbInv_Res.Text = res.ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
