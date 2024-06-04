using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BaniaKalkulatorLibrary
{
    public class Load
    {
        public static List<Beverage>? JSON(string Path)
        {
            try
            {
                string json = File.ReadAllText(Path);
                List<Beverage>? beverages = new List<Beverage>();
                beverages = JsonSerializer.Deserialize<List<Beverage>>(json);

                return beverages;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Błąd json"); return null; }

        }
    }
}
