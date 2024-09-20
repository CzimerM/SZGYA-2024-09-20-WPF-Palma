using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZGYA_2024_09_20_WPF_Palma
{
    internal class Desszert
    {
        public string Nev { get; set; }
        public string Tipus {  get; set; }
        public bool Dijazott { get; set; }
        public int Ar { get; set; }
        public string Egyseg { get; set; }

        public Desszert(string sor)
        {
            var adatok = sor.Split(';');
            this.Nev = adatok[0];
            this.Tipus = adatok[1];
            this.Dijazott = bool.Parse(adatok[2]);
            this.Ar = int.Parse(adatok[3]);
            this.Egyseg = adatok[4];
        }

        public override string ToString()
        {
            return $"{this.Nev};{this.Tipus};{this.Dijazott.ToString().ToLower()};{this.Ar};{this.Egyseg}";
        }

        public Desszert()
        {

        }
    }
}
