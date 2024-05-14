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



        public void createAusgabefile(List<Holzstreifen> streifen, int ebenen, int breite, string kommentar, string dimstring)
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
                    writer.Write(kommentar);//todo ändern
                    writer.Write(dimstring+"\n");//todo ändern
                    writer.Write("Anordnung der Teile" + "\n");//todo ändern
                    int currEbene = 1;
                    int counter = 0;
                    string[,] arr = new string[breite, breite+1];
                    foreach (Holzstreifen hs in streifen)
                    {
                        counter++;
                        if (currEbene % 2 == 1)
                        {
                            for (int a = 0; a < hs.Elemente.Count; a++)
                            {
                                arr[counter - 1, a] = ""+hs.Elemente[a];
                            }
                            arr[counter - 1, hs.Elemente.Count]=hs.ID;
                        }
                        else
                        {
                            if(counter==1)
                                 writer.Write("Ebene " + currEbene + "\n");
                            string ausgabe = "";
                            for(int a = 0; a < hs.Elemente.Count; a++)
                            {
                                ausgabe += hs.Elemente[a] + " ";
                            }
                            ausgabe += hs.ID+"\n";
                            writer.Write(ausgabe);
                        }
                        if (counter == breite)
                        {
                            
                            if (currEbene % 2 == 1)
                            {
                                writer.Write("Ebene " + currEbene + "\n");
                                for (int y = 0; y < breite + 1; y++)
                                {
                                    string ausgabe = "";
                                    
                                    for (int x = 0; x < breite; x++)
                                    {
                                        ausgabe += arr[x, y]+" ";
                                    }
                                    writer.Write(ausgabe+"\n");
                                }
                            }
                            if(currEbene!=ebenen)
                                 writer.Write("\n");
                            arr = new string[breite, breite];
                            currEbene++;
                            counter = 0;
                        }

                    }
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

