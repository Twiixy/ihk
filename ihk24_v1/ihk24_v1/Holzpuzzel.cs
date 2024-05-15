using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    class Holzpuzzel : Holzspielzeug
    {
        private  bool isSolved = false;
        public int Breite { get; set; }
        protected int Laenge { get; set; }
        public int Ebenen { get; set; }
        public List<Holzstreifen> Streifen { get; set; }
        public int[,,] Puzzle { get; set; }
        public string Kommentar { get; set; }

        public List<Holzstreifen> LoesungStreifenList { get; set; }

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
        private bool checkPuzzleArray(List<Holzstreifen> speicher)
        {
            Kodierung kodi = new Kodierung();
            createPuzzleArray(speicher);

            for (int z = 0; z < Ebenen; z++)
            {
                for (int y = 0; y < Breite; y++)
                {
                    for (int x = 0; x < Breite; x++)
                    {

                        if (z == 0)
                        {
                            if (kodi.isValidNachfolger(0, Puzzle[x, y, z], Puzzle[x, y, z + 1]) == false)
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

            LoesungStreifenList = new List<Holzstreifen> (speicher);
            return true;

        }

        /// <summary>
        /// Erstellt ein dreidimensionales Array, welches eine mögliche Puzzelbox Aufstellung representiert.
        /// </summary>
        /// <param name="speicher">Liste von plazierten Holzstreifen</param>
        private void createPuzzleArray(List<Holzstreifen> speicher)
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
                //aendert auch die streifen in s //todo maybe
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
        private void streifenPlazieren(List<Holzstreifen> vorhandeneStreifen, List<Holzstreifen> speicher)
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
                    //aendert auch die streifen in s //todo maybe
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
