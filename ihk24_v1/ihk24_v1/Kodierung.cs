using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    /// <summary>
    /// Klasse um Puzzelstreifenelemente auf Gültigkeit zu prüfen (in Bezug auf die Vorraussetzungen für ein lösbares Puzzel) 
    /// </summary>
    class Kodierung
    {
        /// <summary>
        /// Map um die Bedeutung der Zahlen zu erfahren
        /// </summary>
        Dictionary<int, String> Map;
        /// <summary>
        /// Map der gültigen nachfolger Elemente für die folgende Ebene
        /// </summary>
        Dictionary<int, List<int>> o_nachfolger;
        /// <summary>
        /// Map der gültigen nachfolger Elemente für die vorherige Ebene
        /// </summary>
        Dictionary<int, List<int>> u_nachfolger;

        /// <summary>
        /// Erstellt Decodierungsmaps für gültige Elemente der folgenden Ebenen
        /// </summary>
        public Kodierung()
        {
            //Map um die Bedeutung der Zahlen zu erfahren
            Map = new Dictionary<int, string>()
{
  { 0,"Loch"},
  { 1,"Halbkugel nur oben" },
  { 2,"Halbkugel nur unten" },
  { 3,"Halbkugel oben und unten" },
  { 4,"Oben und unten leer" }
};
            //Map der gültigen nachfolger Elemente für die folgende Ebene
            o_nachfolger = new Dictionary<int, List<int>>()
{
  { 0, new List<int>() { 0,1,2,3,4 } },
  { 1, new List<int>() { 0 } },
  { 2, new List<int>() { 0,4,1} },
  { 3, new List<int>() { 0 } },
  { 4, new List<int>() { 1,4,0 } }
};
            //Map der gültigen nachfolger Elemente für die vorherige Ebene
            u_nachfolger = new Dictionary<int, List<int>>()
{
  { 0, new List<int>() { 0,1,2,3,4 } },
  { 1, new List<int>() { 0, 4, 2 } },
  { 2, new List<int>() { 0} },
  { 3, new List<int>() { 0 } },
  { 4, new List<int>() { 2,4,0 } }
};

        }

        /// <summary>
        /// Prüft ob das aktuelle Streifenelement an der stelle stehen darf
        /// </summary>
        /// <param name="untenZahl">Das Streifenelement was sich auf der vorherigen Ebene befindet (in Bezug auf das akutelle Element)</param>
        /// <param name="aktuellZahl">Die (x,y,z) Koordinate des zu betrachtenden Streifenelements</param>
        /// <param name="obenZahl">Das Streifenelement was sich auf der nächsten Ebene befindet (in Bezug auf das akutelle Element)</param>
        /// <returns>Gibt true zurück, falls das Streifenelement an der Stelle stehen darf</returns>
        public bool isValidNachfolger(int untenZahl, int aktuellZahl, int obenZahl)
        {
            //keine 2 Halbkugeln in ein Loch
            if (aktuellZahl == 0 && (untenZahl == 1 || untenZahl == 3) && (obenZahl == 2 || obenZahl == 3))
                return false;
            return u_nachfolger[aktuellZahl].Contains(untenZahl) && o_nachfolger[aktuellZahl].Contains(obenZahl);
        }

    }
}






