using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для QuadrFrame.xaml
    /// </summary>
    public partial class QuadrFrame : Page
    {
        public QuadrFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnQuadr_Solve_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbQuadr_A.Text, out BigInteger a) &&
                BigInteger.TryParse(tbQuadr_N.Text, out BigInteger n))
            {
                Algorithms.Quadr_Solve(a, n, out BigInteger x1, out BigInteger x2);
                tbQuadr_X1.Text = x1.ToString();
                tbQuadr_X2.Text = x2.ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
