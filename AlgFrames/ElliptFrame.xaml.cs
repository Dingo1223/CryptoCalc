using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для ElliptFrame.xaml
    /// </summary>
    public partial class ElliptFrame : Page
    {
        public ElliptFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbA.Text, out BigInteger a) &&
                BigInteger.TryParse(tbB.Text, out BigInteger b) &&
                BigInteger.TryParse(tbP.Text, out BigInteger p))
            {
                tbRes.Text = Algorithms.EC_Ord(a, b, p).ToString();
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
