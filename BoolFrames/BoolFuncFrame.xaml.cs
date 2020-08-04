using CryptoCalc.MenuFrames;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CryptoCalc.BoolFrames
{
    /// <summary>
    /// Логика взаимодействия для BoolFuncFrame.xaml
    /// </summary>
    public partial class BoolFuncFrame : Page
    {
        public BoolFuncFrame() => InitializeComponent();

        private void BtnToBaseAlgs_Click(object sender, RoutedEventArgs e) => MainWindow.MainFrame.Content = new BoolFuncsFrame();

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbN.Text, out int n))
            {
                MessageBox.Show("Число переменных задано неверно");
                return;
            }

            tbVect.Text = BoolFuncs.GenerateVector(n);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BoolFuncs.GetCharacteristics(tbVect.Text, out string anf, out int deg, out int weight,
                    out List<int> fourier, out List<int> ua, out _);

                tbANF.Text = anf;
                lbDeg.Content = deg.ToString();
                lbWeight.Content = weight.ToString();

                string fourier_str = "";
                foreach (int i in fourier) fourier_str += i.ToString() + " ";
                tbFur.Text = fourier_str;

                string ua_str = "";
                foreach (int i in ua) ua_str += i.ToString() + " ";
                tbUA.Text = ua_str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
