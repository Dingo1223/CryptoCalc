using CryptoCalc.MenuFrames;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.BoolFrames
{
    /// <summary>
    /// Логика взаимодействия для BoolGenerateFrame.xaml
    /// </summary>
    public partial class BoolGenerateFrame : Page
    {
        public BoolGenerateFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BoolFuncsFrame();

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbK.Text, out int k) || !int.TryParse(tbNonLin.Text, out int nonLin))
            {
                MessageBox.Show("Неверные входные данные");
                return;
            }

            pb.Visibility = Visibility.Visible;
            List<int> vector = await GetFuncAsync(k, nonLin);
            pb.Visibility = Visibility.Hidden;
            string vector_str = "";
            foreach (int i in vector) vector_str += i.ToString();
            BoolFuncs.GetCharacteristics(vector_str, out string anf, out int deg, out _, out _, out _, out double nLin);

            tbVect.Text = vector_str;
            tbANF.Text = anf;
            lbDeg.Content = deg.ToString();
            lbNonLin.Content = nLin.ToString();
        }

        private static async Task<List<int>> GetFuncAsync(int k, int nonLin) => 
            await Task.Run(() => BoolFuncs.GetFuncWithChars(k, nonLin));
    }
}
