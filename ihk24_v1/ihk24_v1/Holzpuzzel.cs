using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    class Holzpuzzel: Holzspielzeug
    {
        protected int Breite { get; set; }
        protected int Laenge { get; set; }
        protected int Ebenen { get; set; }
        public List<Holzstreifen> Streifen { get; set; }
        int[,,] Puzzle { get; set; }
        public string Kommentar { get; set; }

        public Holzpuzzel(int dimension, int ebenen, string kommentar)
        {
            Breite = dimension;
            Laenge = dimension;
            Ebenen = ebenen;
            Puzzle = new int[dimension, dimension, ebenen];
            Kommentar = kommentar;
        }
    }
}
