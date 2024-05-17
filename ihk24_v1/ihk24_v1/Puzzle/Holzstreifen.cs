using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1.Puzzle
{
    /// <summary>
    /// Klasse um die Holzstreifen zu simulieren. Enthält eine Liste der Holzstreifenelemente, die ID und ein bool-Wert, ob das Teil genutzt wurde.
    /// </summary>
    class Holzstreifen
    {
        /// <summary>
        /// Liste der Holzstreifenelemente
        /// </summary>
        public List<int> Elemente { get; set; }
        /// <summary>
        /// Id des Holzstreifens
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// True, falls das Holzstreifenteil genutzt wurde
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Erstellt ein Holzstreifen
        /// </summary>
        /// <param name="id">Id/Name des Holzstreifens</param>
        /// <param name="element">Liste der angeordneten Elemente (Zahlen mit Kodierung kodiert)</param>
        public Holzstreifen(string id, List<int> element)
        {
            ID = id;
            Elemente = element;
            IsUsed = false;
        }

        /// <summary>
        /// Rotiert ein Holzpuzzelstreifen um die x oder y achse und speichert das Ergebnis in Elemente
        /// </summary>
        /// <param name="achse">Gibt die Rotierungsachse an</param>
        public void rotieren(char achse)
        {
            if (achse == 'y' || achse == 'Y')//gradzahl egal da man nur um 180° drehen darf
            {
                List<int> rotierteElemente = new List<int>();
                foreach (int element in Elemente)
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
            if (achse == 'x' || achse == 'X')//gradzahl egal da man nur um 180° drehen darf
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
