using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.MenuFrames
{
    /// <summary>
    /// Логика взаимодействия для BoolFuncsFrame.xaml
    /// </summary>
    public partial class BoolFuncsFrame : Page
    {
        public BoolFuncsFrame() => InitializeComponent();

        private void BtnToMainMenu_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new MainMenuFrame();

        private void BtnBoolFunc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBoolGenerate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
