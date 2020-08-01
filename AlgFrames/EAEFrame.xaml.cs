using CryptoCalc.MenuFrames;
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

        }
    }
}
