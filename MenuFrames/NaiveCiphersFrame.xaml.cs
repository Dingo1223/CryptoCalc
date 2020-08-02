using CryptoCalc.CiphersFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.MenuFrames
{
    /// <summary>
    /// Логика взаимодействия для NaiveCiphersFrame.xaml
    /// </summary>
    public partial class NaiveCiphersFrame : Page
    {
        public NaiveCiphersFrame() => InitializeComponent();

        private void BtnToMainMenu_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new MainMenuFrame();

        private void BtnCaesar_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new CaesarFrame();

        private void BtnTranspos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVigenere_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
