using CryptoCalc.MenuFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для CongrFrame.xaml
    /// </summary>
    public partial class CongrFrame : Page
    {
        public CongrFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();
        
        private void BtnCongr_Solve_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
