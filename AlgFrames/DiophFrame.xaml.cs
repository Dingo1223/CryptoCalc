using CryptoCalc.MenuFrames;
using System.Numerics;
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
            BigInteger x = 1, y = 0;
            if (BigInteger.TryParse(tbDioph_A.Text, out BigInteger a) &&
                BigInteger.TryParse(tbDioph_B.Text, out BigInteger b) &&
                BigInteger.TryParse(tbDioph_C.Text, out BigInteger c))
            {
                BigInteger d = Algorithms.Diophantine(a, b, c, ref x, ref y);
                if (d == 0) MessageBox.Show("Нет решений!");
                else
                {
                    tbDioph_X1.Text = x.ToString();
                    tbDioph_Y1.Text = y.ToString();
                    tbDioph_X.Text = x.ToString() + " + " + b.ToString() + "/" + d.ToString() + " * t";
                    tbDioph_Y.Text = y.ToString() + " - " + a.ToString() + "/" + d.ToString() + " * t";
                }
            }
            else MessageBox.Show("Неверные входные данные");
        }
    }
}
