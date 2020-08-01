using CryptoCalc.MenuFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для DiophFrame.xaml
    /// </summary>
    public partial class DiophFrame : Page
    {
        public DiophFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnDioph_Solve_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
