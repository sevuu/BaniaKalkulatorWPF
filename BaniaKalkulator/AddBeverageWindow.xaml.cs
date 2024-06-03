using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BaniaKalkulatorLibrary;
namespace BaniaKalkulator
{
    /// <summary>
    /// Interaction logic for AddBeverageWindow.xaml
    /// </summary>
    /// 
    public partial class AddBeverageWindow : Window
    {
        public Beverage beverage { get; set; }

        public AddBeverageWindow()
        {
            InitializeComponent();
            beverage = new Beverage();

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            beverage.Nazwa = tbName.Text;
            if (!double.TryParse(tbPrice.Text, out double cena))
            {
                MessageBox.Show("Nie jest to poprawna liczba"); return;
            }
            else { beverage.Cena = cena; }
            if (!double.TryParse(tbMl.Text, out double ml))
            {
                MessageBox.Show("Nie jest to poprawna liczba"); return;
            }
            else { beverage.Ml = ml; }
            if (!double.TryParse(tbVolt.Text, out double v))
            {
                MessageBox.Show("Nie jest to poprawna liczba"); return;
            }
            else { beverage.Procent = v; }
            DialogResult = true;
        }
    }
}
