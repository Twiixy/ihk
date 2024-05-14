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
        public string ID { get; set; }
        public bool IsUsed { get; set; }

        public Holzstreifen(string id, List<int> element)
        {
            ID = id;
            Elemente = element;
            IsUsed = false;
        }

        public void rotieren(char achse)
        {
            if ((achse=='y'|| achse == 'Y'))//gradzahl egal da man nur um 180° drehen darf
            {
                List<int> rotierteElemente = new List<int>();
                foreach(int element in Elemente)
                {
                    int elementNumber = element;
                    //aus oberen Halbkugeln werden untere und andersrum
                    if (element == 1)
                        elementNumber = 2;
                    if (element == 2)
                        elementNumber = 1;
                    rotierteElemente.Add(elementNumber);
                }
                Elemente = rotierteElemente;
            }
            if ((achse == 'x' || achse == 'X'))//gradzahl egal da man nur um 180° drehen darf
            {
                List<int> rotierteElemente = new List<int>();
                foreach (int element in Elemente)
                {
                    rotierteElemente.Insert(0, element);
                }
                Elemente = rotierteElemente;
            }
        }
    }
}
