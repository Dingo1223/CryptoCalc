using CryptoCalc.MenuFrames;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для EAGFrame.xaml
    /// </summary>
    public partial class EAGFrame : Page
    {
        public EAGFrame() => InitializeComponent();

        private List<BigInteger> As = new List<BigInteger>();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnEAG_Solve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEAG_Add_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbEAG_New.Text, out BigInteger a))
            {
                lbEAG.Items.Add(tbEAG_New.Text);
                As.Add(a);
            }
            else MessageBox.Show("Неверные входные данные");
        }

        private void BtnEAG_Clean_Click(object sender, RoutedEventArgs e)
        {
            As.Clear();
            lbEAG.Items.Clear();
        }
    }
}
