using CryptoCalc.AlgFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.MenuFrames
{
    /// <summary>
    /// Логика взаимодействия для BaseAlgorithmsFrame.xaml
    /// </summary>
    public partial class BaseAlgorithmsFrame : Page
    {
        public BaseAlgorithmsFrame() => InitializeComponent();

        private void BtnToMainMenu_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new MainMenuFrame();

        private void BtnEA_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new EAFrame();

        private void BtnEAE_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new EAEFrame();

        private void BtnEAG_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new EAGFrame();

        private void BtnDioph_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new DiophFrame();

        private void BtnCongr_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new CongrFrame();

        private void BtnGKO_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new GKOFrame();

        private void BtnInv_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new InvFrame();

        private void BtnLJ_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new LJFrame();

        private void BtnQuadr_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new QuadrFrame();

        private void BtnProbTests_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new ProbTestsFrame();

        private void BtnDiscr_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new DiscrFrame();

        private void BtnEllipt_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new ElliptFrame();
    }
}
