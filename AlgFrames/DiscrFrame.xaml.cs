using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для DiscrFrame.xaml
    /// </summary>
    public partial class DiscrFrame : Page
    {
        public DiscrFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnSolve_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbG.Text, out BigInteger g) &&
                BigInteger.TryParse(tbA.Text, out BigInteger a) &&
                BigInteger.TryParse(tbM.Text, out BigInteger m))
            {
                tbX.Text = Algorithms.Discr_Solve(g, a, m).ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
