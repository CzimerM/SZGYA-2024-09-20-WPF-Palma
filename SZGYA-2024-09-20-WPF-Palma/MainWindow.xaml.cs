using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SZGYA_2024_09_20_WPF_Palma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Desszert> desszertek;
        public MainWindow()
        {
            InitializeComponent();
            desszertek = new List<Desszert>();
            var rndGen = new Random();

            var sr = new StreamReader("../../../src/palma.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                desszertek.Add(new Desszert(sr.ReadLine()));
            }
            sr.Close();

            //2
            var f2RandomDesszert = desszertek[rndGen.Next(desszertek.Count)];
            txbMaiAjanlat.Text = $"Mai ajánlatunk: {f2RandomDesszert.Nev}";

            //3
            var legolcsobb = desszertek.MinBy(d => d.Ar);
            txbLegolcsobbSutiNev.Text = legolcsobb.Nev;
            txbLegolcsobbSutiMennyiseg.Text = $"{legolcsobb.Ar} Ft/{legolcsobb.Egyseg}";

            var legdragabb = desszertek.MaxBy(d => d.Ar);
            txbLegdragabbSutiNev.Text = legdragabb.Nev;
            txbLegdragabbSutiMennyiseg.Text = $"{legdragabb.Ar} Ft/{legdragabb.Egyseg}";

            //4
            txbDijnyertesMennyiseg.Text = $"{desszertek.Count(d => d.Dijazott)} féle díjnyertes édességből választhat.";

            //5
            var sw = new StreamWriter("../../../src/lista.txt", false, Encoding.UTF8);
            foreach (var desszert in desszertek.DistinctBy(d => d.Nev).Select(d => new { d.Nev, d.Tipus }))
            {
                sw.WriteLine($"{desszert.Nev} {desszert.Tipus}");
            }
            sw.Close();

            //6
            sw = new StreamWriter("../../../src/stat.txt", false,  Encoding.UTF8);
            foreach (var tipus in desszertek.GroupBy(d => d.Tipus).Select(d => new { nev = d.First().Tipus, mennyiseg = d.Count() }))
            {
                sw.WriteLine($"{tipus.nev}-{tipus.mennyiseg}");
            }; 
            sw.Close();
            
            
        }

        private void btnAjanlatMentese_Click(object sender, RoutedEventArgs e)
        {
            //7
            var f7ajanlat = desszertek.Where(d => d.Tipus.ToLower() == txbTipus.Text.ToLower());
            if (f7ajanlat.Count() == 0 )
            {
                MessageBox.Show("Nincs megfelelő sütink. Kérjük, válaszzon mást!");
            }
            else
            {
                var sw = new StreamWriter("../../../src/ajanlat.txt", false, Encoding.UTF8);
                foreach (var desszert in f7ajanlat)
                {
                    sw.WriteLine($"{desszert.Nev} - {desszert.Ar} Ft/{desszert.Egyseg}");
                }
                sw.Close();
                MessageBox.Show($"{f7ajanlat.Count()} db desszert, átlag ár: {f7ajanlat.Average(d => d.Ar)} Ft");
            }
        }

        private void btnUjSuti_Click(object sender, RoutedEventArgs e)
        {
            var ujDesszert = new Desszert();
            if (txbUjNev.Text == "")
            {
                MessageBox.Show("Kérem adja meg a desszert nevét!", "Hiba!");
                return;
            }
            ujDesszert.Nev = txbUjNev.Text;

            if (txbUjTipus.Text == "")
            {
                MessageBox.Show("Kérem adja meg a desszert típusát!", "Hiba!");
                return;
            }
            ujDesszert.Tipus = txbUjTipus.Text;

            if (txbUjEgyseg.Text == "")
            {
                MessageBox.Show("Kérem adja meg a desszert árának egységét!", "Hiba!");
                return;
            }
            ujDesszert.Egyseg = txbUjEgyseg.Text;

            int ar = -1;
            if (txbUjAr.Text == "")
            {
                MessageBox.Show("Kérem adja meg a desszert árát!", "Hiba!");
                return;
            }
            if (!int.TryParse(txbUjAr.Text, out ar))
            {
                MessageBox.Show("Kérem egész számot adjon meg a desszert árának", "Hiba!");
                return;
            }
            ujDesszert.Ar = ar;

            ujDesszert.Dijazott = (bool)chkUjDijazott.IsChecked;

            var sw = new StreamWriter("../../../src/cuki.txt", true, Encoding.UTF8);
            sw.WriteLine(ujDesszert);
            sw.Close();
            MessageBox.Show("Az új sütemény sikeresen el lett mentve!");
        }
    }
}