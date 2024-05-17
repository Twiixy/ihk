using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1.Puzzle
{
    class Holzpuzzel : Holzspielzeug
    {
        private static Kodierung kodi = new Kodierung();
        /// <summary>
        /// True wenn der LoesungStreifenList eine Lösung beinhaltet
        /// </summary>
        private bool isSolved = false;
        /// <summary>
        /// Breite des Puzzles
        /// </summary>
        public int Breite { get; set; }
        /// <summary>
        /// Länge des Puzzles
        /// </summary>
        protected int Laenge { get; set; }
        /// <summary>
        /// Anzahl der Puzzleebenen
        /// </summary>
        public int Ebenen { get; set; }
        /// <summary>
        /// Liste der Holzstreifen
        /// </summary>
        public List<Holzstreifen> Streifen { get; set; }
        /// <summary>
        /// Simulation des ausgebauten Puzzles mittels dreidimensionalen Arrays
        /// </summary>
        public int[,,] Puzzle { get; set; }
        /// <summary>
        /// Kommentar aus dem Eingabefile
        /// </summary>
        public string Kommentar { get; set; }
        /// <summary>
        /// Lösung des Puzzles wenn man die Teile von links nach rechts und unten nach oben platzieren würde
        /// </summary>
        public List<Holzstreifen> LoesungStreifenList { get; set; }
        /// <summary>
        /// Dimension aus Eingabedatei
        /// </summary>
        public string DimensionsString { get; set; }
        public Holzpuzzel(int dimension, int ebenen, string kommentar, string dimensionString, List<Holzstreifen> holzStreifen)
        {
            DimensionsString = dimensionString;
            Breite = dimension;
            Laenge = dimension;
            Ebenen = ebenen;
            Puzzle = new int[dimension, dimension, ebenen];
            Kommentar = kommentar;
            Streifen = holzStreifen;
            LoesungStreifenList = new List<Holzstreifen>();
            pruefeHolzstreifenIds();
            if (!pruefeVollstaendigkeit())
             throw new HolzpuzzelKonstruktorException("Puzzle ist unvollstaendig!");
        }

        /// <summary>
        /// Prüft, ob jeder Streifen die nnotwendige Anzahl an Elementen besitzt und ob es genug Streifen gibt um das Puzzle zu lösen
        /// </summary>
        /// <returns>True falls das Puzzle vollständig ist</returns>
        protected bool pruefeVollstaendigkeit()
        {
            foreach(Holzstreifen hs in Streifen)
            {
                if(hs.Elemente.Count!=Breite) return false;
            }
            if(Streifen.Count!=Ebenen*Breite) return false;
            return true;
        }

        /// <summary>
        /// Prüft ob die Holzstreiben IDs eindeutig sind
        /// </summary>
        /// <returns>True falls die IDs eindeutig sind</returns>
        protected bool pruefeHolzstreifenIds()
        {
            List<string> result= new List<string>();
            var duplikate = Streifen.GroupBy(s => s.ID)
                              .Where(g => g.Count() > 1);
            if (duplikate.Any())
            {
                Console.WriteLine("Folgene Puzzelstreifenids gibt es mehrfach:");
                foreach (var group in duplikate) { 
                    Console.WriteLine(group.Key);
                }
                return false;
            }
            else
            {
                Console.WriteLine("Es gibt keine Duplikate in der ID.");
            }
            return true;
        }

        /// <summary>
        /// Entfernt ein Holzstreifen aus der mitgelieferten Liste
        /// </summary>
        /// <param name="holzstreifenList">Liste von Holzstreifen</param>
        /// <param name="id">Id des zu entfernenden Holzstreifens</param>
        /// <returns></returns>
        public List<Holzstreifen> removeStreifen(List<Holzstreifen> holzstreifenList, string id)
        {
            var result = new List<Holzstreifen>(holzstreifenList);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].ID == id)
                {
                    result.RemoveAt(i);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Prüft ob das Holzpuzzle keine Bedingungen verletzt, indem es die isValidNachfolger Methode von Kodierung nutzt.
        /// </summary>
        /// <param name="speicher">Liste von plazierten Holzstreifen</param>
        /// <returns></returns>
        protected bool checkPuzzleArray(List<Holzstreifen> speicher)
        {
            
            createPuzzleArray(speicher);

            for (int z = 0; z < Ebenen; z++)
            {
                for (int y = 0; y < Breite; y++)
                {
                    for (int x = 0; x < Breite; x++)
                    {

                        if (z == 0)
                        {
                            if (Ebenen == 1)
                            {
                                if (kodi.isValidNachfolger(0, Puzzle[x, y, z], 0) == false)
                                {
                                    return false;
                                }
                            }
                            else if (kodi.isValidNachfolger(0, Puzzle[x, y, z], Puzzle[x, y, z + 1]) == false)
                            {
                                return false;
                            }
                        }
                        else if (z == Ebenen - 1)
                        {
                            if (kodi.isValidNachfolger(Puzzle[x, y, z - 1], Puzzle[x, y, z], 0) == false)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (kodi.isValidNachfolger(Puzzle[x, y, z - 1], Puzzle[x, y, z], Puzzle[x, y, z + 1]) == false)
                            {
                                return false;
                            }
                        }

                    }

                }

            }

            LoesungStreifenList = new List<Holzstreifen>(speicher);
            return true;

        }

        /// <summary>
        /// Erstellt ein dreidimensionales Array, welches eine mögliche Puzzelbox Aufstellung representiert.
        /// </summary>
        /// <param name="speicher">Liste von plazierten Holzstreifen</param>
        protected void createPuzzleArray(List<Holzstreifen> speicher)
        {
            Puzzle = new int[Breite, Breite, Ebenen];
            int currEbene = 0;
            int curStein = 0;
            int counter = 0;
            foreach (Holzstreifen hs in speicher)
            {
                counter++;
                if (currEbene % 2 == 0)
                {
                    //  Puzzle[, ,currEbene]
                    for (int i = 0; i < hs.Elemente.Count; i++)
                    {
                        Puzzle[curStein, i, currEbene] = hs.Elemente[i];
                    }
                    curStein++;
                }
                if (currEbene % 2 == 1)
                {
                    //  Puzzle[, ,currEbene]
                    for (int i = 0; i < hs.Elemente.Count; i++)
                    {
                        Puzzle[i, curStein, currEbene] = hs.Elemente[i];
                    }
                    curStein++;
                }
                if (counter % Laenge == 0)
                {
                    curStein = 0;
                    currEbene++;
                }
            }
        }

        /// <summary>
        /// Löst das Puzzle indem die Methode rekursiv die Methode streifenPlazieren aufruft. 
        /// </summary>
        public void solve()
        {
            foreach (Holzstreifen s in Streifen)
            {
                List<Holzstreifen> result = new List<Holzstreifen>();
                result.Add(s);
                streifenPlazieren(removeStreifen(Streifen, s.ID), result);
                if (isSolved)
                {
                    return;
                }
                s.rotieren('y');
                List<Holzstreifen> result2 = new List<Holzstreifen>();
                result2.Add(s);
                streifenPlazieren(removeStreifen(Streifen, s.ID), result2);
                if (isSolved)
                {
                    return;
                }
                s.rotieren('x');
                List<Holzstreifen> result3 = new List<Holzstreifen>();
                result3.Add(s);
                streifenPlazieren(removeStreifen(Streifen, s.ID), result3);
                if (isSolved)
                {
                    return;
                }
                s.rotieren('y');
                List<Holzstreifen> result4 = new List<Holzstreifen>();
                result4.Add(s);
                streifenPlazieren(removeStreifen(Streifen, s.ID), result4);
                if (isSolved)
                {
                    return;
                }
            }
        }
        /// <summary>
        /// Rekursive Methode, die ein Puzzel-Array erstellt, sobald vorhandeneStreifen leer ist. 
        /// Das Programm bricht ab wenn alle möglichen Holzstreifen-Kombinationen getestet wurden oder eine Lösung gefunden wurde.
        /// </summary>
        /// <param name="vorhandeneStreifen">Liste der noch zu nutzenden Streifen</param>
        /// <param name="speicher">Erbebnisliste der plazierten Holzstreifen</param>
        protected void streifenPlazieren(List<Holzstreifen> vorhandeneStreifen, List<Holzstreifen> speicher)
        {
            if (isSolved)
            {
                return;
            }
            if (vorhandeneStreifen.Count == 0)
            {
                isSolved = checkPuzzleArray(speicher);
            }
            else
            {
                foreach (Holzstreifen s in vorhandeneStreifen)
                {
                    if (speicher.Count > Breite)
                    {
                        if (!checkPuzzleArray(speicher))
                            return;
                    }
                    List<Holzstreifen> result = new List<Holzstreifen>(speicher);
                    result.Add(s);
                    streifenPlazieren(removeStreifen(vorhandeneStreifen, s.ID), result);
                    if (isSolved)
                    {
                        return;
                    }
                    s.rotieren('y');
                    List<Holzstreifen> result2 = new List<Holzstreifen>(speicher);
                    result2.Add(s);
                    streifenPlazieren(removeStreifen(vorhandeneStreifen, s.ID), result2);
                    if (isSolved)
                    {
                        return;
                    }
                    s.rotieren('x');
                    List<Holzstreifen> result3 = new List<Holzstreifen>(speicher);
                    result3.Add(s);
                    streifenPlazieren(removeStreifen(vorhandeneStreifen, s.ID), result3);
                    if (isSolved)
                    {
                        return;
                    }
                    s.rotieren('y');
                    List<Holzstreifen> result4 = new List<Holzstreifen>(speicher);
                    result4.Add(s);
                    streifenPlazieren(removeStreifen(vorhandeneStreifen, s.ID), result4);
                    if (isSolved)
                    {
                        return;
                    }
                }

            }

        }






    }
}
