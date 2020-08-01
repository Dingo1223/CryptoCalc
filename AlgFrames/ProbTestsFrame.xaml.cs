using CryptoCalc.MenuFrames;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для ProbTestsFrame.xaml
    /// </summary>
    public partial class ProbTestsFrame : Page
    {
        public ProbTestsFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnFermat_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbNum.Text, out BigInteger num) &&
                int.TryParse(tbRepeats.Text, out int repeats))
            {
                lbFermat.Content = ProbTests.Test_Fermat(num, repeats) ? "неизвестно" : "составное";
            }
            else MessageBox.Show("Неверные входные данные");
        }

        private void BtnSolovay_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbNum.Text, out BigInteger num) &&
                int.TryParse(tbRepeats.Text, out int repeats))
            {
                lbSolovay.Content = ProbTests.Test_Solovay(num, repeats) ? "неизвестно" : "составное";
            }
            else MessageBox.Show("Неверные входные данные");
        }

        private void BtnRabin_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbNum.Text, out BigInteger num) &&
                int.TryParse(tbRepeats.Text, out int repeats))
            {
                lbRabin.Content = ProbTests.Test_Rabin(num, repeats) ? "неизвестно" : "составное";
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
