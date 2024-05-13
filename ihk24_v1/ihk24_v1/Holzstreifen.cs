using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    class Holzstreifen
    {
        public List<int> Elemente { get; set; }
        public bool IsUsed { get; set; }

        public Holzstreifen(List<int> element)
        {
            Elemente = element;
            IsUsed = false;
        }

        public void rotieren(int grad)
        {
            if (grad == 90)
            {

            }
        }
    }
}
