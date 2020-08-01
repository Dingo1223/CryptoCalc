using CryptoCalc.MenuFrames;
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

        }
    }
}
