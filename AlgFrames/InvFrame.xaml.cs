using CryptoCalc.MenuFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для InvFrame.xaml
    /// </summary>
    public partial class InvFrame : Page
    {
        public InvFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();
        
        private void BtnInv_Solve_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
