using CryptoCalc.MenuFrames;
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

        }
    }
}
