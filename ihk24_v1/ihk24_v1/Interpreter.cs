using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1
{
    class Interpreter
    {
        private List<string> Endung { get; set; }
        private string Pfad { get; set; }
        private string DateiName { get; set; }
        public Interpreter(List<string> endungen, string pfad, string dateiName)
        {
            Endung = endungen;
            Pfad = pfad;
            DateiName = dateiName;
        }


        public List<Holzpuzzel> createPuzzle()
        {
            List<Holzpuzzel> result = new List<Holzpuzzel>();
            // Prüfen Sie, ob der Ordner existiert
            if (Directory.Exists(Pfad))
            {
                // Durchsuchen Sie den Ordner nach Textdateien

                foreach (string p in Endung)
                {
                    string formatP = "";
                    if (!p.StartsWith("."))
                        formatP = "*." + p;
                    else
                        formatP = "*" + p;

                    foreach (string datei in Directory.EnumerateFiles(Pfad, formatP, SearchOption.AllDirectories))
                    {
                        try
                        {

                            string kommentar = "";
                            string dim = "";
                            string[] dateiInhalt = File.ReadAllLines(datei);

                            // Geben Sie den Dateinamen und Inhalt aus
                            Console.WriteLine("Datei: " + datei);
                            List<Holzstreifen> streifenList = new List<Holzstreifen>();
                            foreach (string data in dateiInhalt)
                            {
                                string id = "";
                                //Kommentare rausfiltern
                                if (data.StartsWith("//"))
                                {
                                    kommentar += data + "\n";
                                }
                                else if (data.StartsWith("Dimension"))
                                {
                                    dim = data;
                                }
                                else
                                {
                                    string[] tmp = data.Split(" ");
                                    id = tmp[0];
                                    int[] tmpInt = tmp[1].Split(",").Select(int.Parse).ToArray();
                                    streifenList.Add(new Holzstreifen(id, new List<int>(tmpInt)));

                                }
                            }
                            string[] dimArray = dim.Split(" ");
                            int[] dimIntArray = dimArray[1].Split(",").Select(int.Parse).ToArray();
                            result.Add(new Holzpuzzel(dimIntArray[0], dimIntArray[2], kommentar, dim, streifenList));

                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("Fehler beim Lesen der Datei: " + datei + " - " + e.Message);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Ordner nicht gefunden: " + Pfad);
            }
            return result;
        }



        public void createAusgabefile()
        {


            // textfile erstellen
            string filePath = @"C:\Users\di461643\00000ihk\ihk\";
            filePath += DateiName + ".txt";

            // Write the text to the file
            try
            {
                // Open the file in write mode
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the text to the file
                    writer.Write("test123");//todo ändern
                }

                // Console message indicating success
                Console.WriteLine("Text written to file successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during writing
                Console.WriteLine("Error writing to file: " + ex.Message);
            }


        }


    }

}

