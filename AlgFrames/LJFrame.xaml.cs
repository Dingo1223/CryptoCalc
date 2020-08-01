using CryptoCalc.MenuFrames;
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

        }
    }
}
