using System;
using System.Collections.Generic;
using System.IO;

namespace ihk24_v1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string Pfad = "";
            string DateiName = "";
            List<string> Endungen = new List<string>();

            Console.Write("Geben Sie den Ordnerpfad ein: ");
            //Pfad = Console.ReadLine();
            Pfad = "C:\\Users\\di461643\\00000ihk\\ihk\\eingaben";

            Console.Write("Geben Sie Endungen ein, die im Ordner geöffnet werden sollen (mit einem leerzeichen getrennt z.B. 'txt vm': ");
            //string endungsString = Console.ReadLine();
            string endungsString = "txt";
            Endungen =new List<string>(endungsString.Split(" "));

            Console.Write("Geben Sie einen Namen für die Resulttextdatei an: ");
            //DateiName = Console.ReadLine();
            DateiName = "a";

            //Dateien einlesen
            Interpreter interP = new Interpreter(Endungen, Pfad, DateiName);
            //Puzzle erstellen
            List<Holzpuzzel> holzPuzzelList=interP.createPuzzle();

            int solverx = 1;
             
            holzPuzzelList[solverx].solve();

            List<Holzstreifen> test =holzPuzzelList[solverx].LoesungStreifenList;

            interP.createAusgabefile(test, holzPuzzelList[solverx].Ebenen, holzPuzzelList[solverx].Breite, holzPuzzelList[solverx].Kommentar, holzPuzzelList[solverx].DimensionsString);


            Console.WriteLine("finish");





           


        }
    }
}
