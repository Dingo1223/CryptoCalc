using CryptoCalc.MenuFrames;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.AlgFrames
{
    /// <summary>
    /// Логика взаимодействия для GKOFrame.xaml
    /// </summary>
    public partial class GKOFrame : Page
    {
        public GKOFrame() => InitializeComponent();

        private List<PartGKO> PartsGKO = new List<PartGKO>();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BaseAlgorithmsFrame();

        private void BtnGKO_Add_Click(object sender, RoutedEventArgs e)
        {
            if (BigInteger.TryParse(tbGKO_A.Text, out BigInteger a) && 
                BigInteger.TryParse(tbGKO_B.Text, out BigInteger b) &&
                BigInteger.TryParse(tbGKO_N.Text, out BigInteger n))
            {
                lbGKO.Items.Add(tbGKO_A.Text + " * x ≡ " + tbGKO_B.Text + " ( mod " + tbGKO_N.Text + ")");
                PartsGKO.Add(new PartGKO(a, b, n));
                tbGKO_A.Text = "";
                tbGKO_B.Text = "";
                tbGKO_N.Text = "";
            }
            else MessageBox.Show("Неверные входные данные");
        }

        private void BtnGKO_Clear_Click(object sender, RoutedEventArgs e)
        {
            PartsGKO.Clear();
            lbGKO.Items.Clear();
        }

        private void BtnGKO_Solve_Click(object sender, RoutedEventArgs e)
        {
            GKO_X x = Algorithms.GKO(PartsGKO);
            tbGKO_X1.Text = x.A.ToString();
            tbGKO_X.Text = x.A.ToString() + " + " + x.N.ToString() + " * t";
        }
    }
}
