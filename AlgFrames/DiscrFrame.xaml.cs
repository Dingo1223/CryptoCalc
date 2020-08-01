using CryptoCalc.MenuFrames;
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

        }
    }
}
