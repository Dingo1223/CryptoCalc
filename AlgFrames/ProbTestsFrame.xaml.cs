using CryptoCalc.MenuFrames;
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

        }

        private void BtnSolovay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRabin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
