using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для LJFrame.xaml
    /// </summary>
    public partial class LJFrame : Page
    {
        public LJFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnLJ_Solve_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbLJ_A.Text, out BigInteger a) &&
                BigInteger.TryParse(tbLJ_B.Text, out BigInteger b))
            {
                tbLJ_Res.Text = Algorithms.Jacobi(a, b).ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
