using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1.Puzzle
{
    /// <summary>
    /// Abstracte Klasse um neue Arten von Puzzlen speichern zu können
    /// </summary>
    abstract class Holzspielzeug
    {
        /// <summary>
        /// Name des Herstellers
        /// </summary>
        protected string Herstellername { get; set; }
        /// <summary>
        /// Preis des Produkts
        /// </summary>
        protected int Preis { get; set; }
    }
}
