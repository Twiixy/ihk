using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using ihk24_v1.Puzzle;
using ihk24_v1.Tests;

namespace ihk24_v1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string Pfad = "";
            string DateiName = "";
            string endungsString = "";
            List<string> Endungen = new List<string>();

            Console.WriteLine("Teste Methoden");
            Console.WriteLine("Teste Holzstriefen");
            HolzStreifenTester hsTest = new HolzStreifenTester("a", new List<int> ());

            if (args.Length >= 3)
            {
                Pfad = args[0];
                endungsString += args[1];
                DateiName = args[2];
            }
            else
            {
                Console.Write("Geben Sie den Ordnerpfad ein, in dem die Eingabedateien sind: ");
                Pfad = Console.ReadLine();

                Console.Write("Geben Sie Endungen ein, die im Ordner geöffnet werden sollen (mit einem leerzeichen getrennt z.B. 'txt vm': ");
                endungsString = Console.ReadLine();
                Endungen = new List<string>(endungsString.Split(" "));

                Console.Write("Geben Sie einen Namen für die Ausgabedatei an: ");
                DateiName = Console.ReadLine();
            }



            //Dateien einlesen
            Interpreter interP = new Interpreter(Endungen, Pfad, DateiName);
            //Puzzle erstellen
            List<Holzpuzzel> holzPuzzelList=interP.createPuzzle();


            Stopwatch stopwatch = new Stopwatch();

            int i = 0;
            foreach(Holzpuzzel hp in holzPuzzelList)
            {
                i++;
                stopwatch.Start();
                hp.solve();
                //zeit Stoppen und ausgeben
                stopwatch.Stop();
                Console.WriteLine("Puzzle " + i + " geloest!" + "\nVerstrichene Zeit: " + stopwatch.Elapsed.ToString());
                stopwatch.Reset();
                interP.createAusgabefile(hp.LoesungStreifenList, hp.Ebenen, hp.Breite,hp.Kommentar, hp.DimensionsString,i);
                
            }

            Console.WriteLine("finish");





           


        }
    }
}
