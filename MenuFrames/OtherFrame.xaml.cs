using CryptoCalc.OtherFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.MenuFrames
{
    /// <summary>
    /// Логика взаимодействия для OtherFrame.xaml
    /// </summary>
    public partial class OtherFrame : Page
    {
        public OtherFrame() => InitializeComponent();

        private void BtnToMainMenu_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new MainMenuFrame();

        private void BtnSecret_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new SecretFrame();

        private void BtnCRS_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new CRSFrame();
    }
}
