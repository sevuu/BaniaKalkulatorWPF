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
using Microsoft.Win32;
using System.Reflection;
using System.IO;

namespace BaniaKalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<Beverage> beverages { get; set; }
        string DataFile = "database.json";
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

            if (!File.Exists(DataFile))
            {
                File.WriteAllText(DataFile, "[]");
                Save.JSON(beverages, DataFile);
            }
            importFromFileLogic("JSON", DataFile);
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

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem is Beverage doUsuniecia)
            {
                beverages.Remove(doUsuniecia);
                dataGrid.Items.Refresh();
            }
        }

        private void KalkulujBtn_Click(object sender, RoutedEventArgs e)
        {
            bool tmp=false;
            foreach (var beverage in beverages)
            {
                if (beverage.Nazwa.ToLower() == "żubr" || beverage.Nazwa.ToLower() == "zubr") { tmp = true; }
                beverage.Spejsonowanie();
                beverage.Etanolowanie();
                dataGrid.Items.Refresh();
            }
            Beverage? najlepsze = beverages.OrderByDescending(b => b.Spejson).FirstOrDefault();

            if (tmp == true && (najlepsze?._nazwa.ToLower() != "żubr" || najlepsze?._nazwa.ToLower() != "zubr"))
            {
                MessageBox.Show($"Najopłacalniejszy alkohol: {najlepsze?.Nazwa}, ale powinieneś kupić Żubra.");
            } else 
                MessageBox.Show($"Najopłacalniejszy alkohol: {najlepsze?.Nazwa}");

            Save.JSON(beverages, DataFile);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog() { FileName = "TaniaBaniaExport", DefaultExt = ".json", Filter = "Documents in JSON format (.json)|*.json" };
            if (d.ShowDialog() == true)
            {
                if (d.FilterIndex == 1) { Save.JSON(beverages, d.FileName); }
            }
        }

        private void importFromFileLogic(String methodName, String path)
        {
            MethodInfo ?method = typeof(Load).GetMethod(methodName);
            if (method != null)
            {
                try
                {
                    beverages = (List<Beverage>)method.Invoke(null, new object[] { path });

                    dataGrid.ItemsSource = beverages;
                    dataGrid.Items.Refresh();
                    if(path != DataFile)
                        MessageBox.Show("Importowanie zakończyło się sukcesem", "Information");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Nie mozna wskazac metody");
            }
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog() { FileName = "TaniaBaniaExport", DefaultExt = ".txt", Filter = "Documents in JSON format (.json)|*.json" };
            if (d.ShowDialog() == true)
            {
                if (d.FilterIndex == 1) { importFromFileLogic("JSON", d.FileName); }
            }
        }

   
    }
}