using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaniaKalkulatorLibrary;
using System.Linq;

namespace BaniaKalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<Beverage> beverages { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            beverages = new List<Beverage>();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Nazwa", Binding = new Binding("Nazwa") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "ml", Binding = new Binding("Ml") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Procent", Binding = new Binding("Procent") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Cena", Binding = new Binding("Cena") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Wsp. Spejsona", Binding = new Binding("Spejson") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Etanol (ml)", Binding = new Binding("Etanol") });
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = beverages;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var okno = new AddBeverageWindow();
            if (okno.ShowDialog() == true)
            {
                var Trunek = okno.beverage;
                beverages.Add(Trunek);
                dataGrid.Items.Refresh();
            }
        }

        private void KalkulujBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var beverage in beverages)
            {
                beverage.Spejsonowanie();
                beverage.Etanolowanie();
                dataGrid.Items.Refresh();
            }
            Beverage? najlepsze = beverages.OrderByDescending(b => b.Spejson).FirstOrDefault();

            MessageBox.Show($"Najopłacalniejszy alkohol: {najlepsze?.Nazwa}");

        }
    }
}