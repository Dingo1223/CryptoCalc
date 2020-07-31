using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.MenuFrames
{
    /// <summary>
    /// Логика взаимодействия для MainMenuFrame.xaml
    /// </summary>
    public partial class MainMenuFrame : Page
    {
        public MainMenuFrame() => InitializeComponent();

        private void BtnBaseAlgorithms_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnNaiveCiphers_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new NaiveCiphersFrame();

        private void BtnBoolFuncs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BoolFuncsFrame();

        private void BtnOther_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new OtherFrame();
    }
}
