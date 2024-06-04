using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace BaniaKalkulatorLibrary
{
    public class Save
    {
        public static void Txt(IList<Beverage> beverages, string Path)
        {
            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var s in beverages)
                    {
                        sw.WriteLine(s.Nazwa);
                        sw.WriteLine(s.Ml);
                        sw.WriteLine(s.Procent);
                        sw.WriteLine(s.Cena);
                        sw.WriteLine(s.Spejson);
                        sw.WriteLine(s.Etanol);
                    }
                }
            }
        }
        public static void JSON(IList<Beverage> beverages, string Path)
        {
            try
            {
                string json = JsonSerializer.Serialize(beverages);
                File.WriteAllText(Path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
            }
            finally { }
        }
    }
}
