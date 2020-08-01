using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для EAEFrame.xaml
    /// </summary>
    public partial class EAEFrame : Page
    {
        public EAEFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnEAE_Solve_Click(object sender, RoutedEventArgs e)
        {
            BigInteger d, x = 1, y = 0;
            if (BigInteger.TryParse(tbEAE_A.Text, out BigInteger a) && 
                BigInteger.TryParse(tbEAE_B.Text, out BigInteger b))
            {
                d = Algorithms.Euclid_Ext(BigInteger.Abs(a), BigInteger.Abs(b), ref x, ref y, 0, 1);
                tbEAE_X.Text = (a < 0 ? -x : x).ToString();
                tbEAE_Y.Text = (b < 0 ? -y : y).ToString();
                tbEAE_Res.Text = d.ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
