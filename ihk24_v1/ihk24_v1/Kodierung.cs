using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    class Kodierung
    {
        Dictionary<int, String> Map;
        Dictionary<int, List<int>> o_nachfolger;
        Dictionary<int, List<int>> u_nachfolger;

        public Kodierung()
        {
            Map = new Dictionary<int, string>()
{
  { 0,"Loch"},
  { 1,"Halbkugel nur oben" },
  { 2,"Halbkugel nur unten" },
  { 3,"Halbkugel oben und unten" },
  { 4,"Oben und unten leer" }
};

            o_nachfolger = new Dictionary<int, List<int>>()
{
  { 0, new List<int>() { 0,1,2,3,4 } },
  { 1, new List<int>() { 0 } },
  { 2, new List<int>() { 0,4,1} },
  { 3, new List<int>() { 0 } },
  { 4, new List<int>() { 1,4,0 } }
};

            u_nachfolger = new Dictionary<int, List<int>>()
{
  { 0, new List<int>() { 0,1,2,3,4 } },
  { 1, new List<int>() { 0, 4, 2 } },
  { 2, new List<int>() { 0} },
  { 3, new List<int>() { 0 } },
  { 4, new List<int>() { 2,4,0 } }
};




        }

        public bool isValidNachfolger(int untenZahl, int aktuellZahl, int obenZahl)
        {
            return u_nachfolger[aktuellZahl].Contains(untenZahl) && o_nachfolger[aktuellZahl].Contains(obenZahl);
        }



    }
}






