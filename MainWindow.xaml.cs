using CryptoCalc.MenuFrames;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainFrame = mainFrame;
            MainFrame.Content = new MainMenuFrame();
        }
    }
}
