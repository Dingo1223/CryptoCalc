using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для EAFrame.xaml
    /// </summary>
    public partial class EAFrame : Page
    {
        public EAFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnEA_Solve_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbEA_A.Text, out BigInteger a) && BigInteger.TryParse(tbEA_B.Text, out BigInteger b))
            {
                tbEA_Res.Text = Algorithms.Euclid(a, b).ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
