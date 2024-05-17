﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ihk24_v1.Puzzle;
using ihk24_v1.Tests;

namespace ihk24_v1
{
    /// <summary>
    /// Klasse um Holzpuzzel aus Textfiles zu erstellen und gelöste Puzzel als Ergebnisfile zu erstellen
    /// </summary>
    class Interpreter
    {
        /// <summary>
        /// Endungen der zu suchenden Dateien in einem Ordner.
        /// </summary>
        protected List<string> Endung { get; set; }
        /// <summary>
        /// Absoluterpfad zum Ordner, der die Textfiles zur Erstellung des Programms hat.
        /// </summary>
        private string Pfad { get; set; }
        /// <summary>
        /// Name der Lösungsdatei, die erstellt wird.
        /// </summary>
        private string DateiName { get; set; }
        /// <summary>
        /// Erstellt ein Interpreterobjekt mit den notwendigen Parametern
        /// </summary>
        /// <param name="endungen">Endungen der zu suchenden Dateien in einem Ordner.</param>
        /// <param name="pfad">Absoluterpfad zum Ordner, der die Textfiles zur Erstellung des Programms hat.</param>
        /// <param name="dateiName">Name der Lösungsdatei, die erstellt wird.</param>
        public Interpreter(List<string> endungen, string pfad, string dateiName)
        {
            Endung = endungen;
            Pfad = pfad;
            DateiName = dateiName;
        }


        /// <summary>
        /// Erstellt Holzpuzzel mithilfe von Textdateien
        /// </summary>
        /// <returns>Gibt eine Liste aus erstellten Puzzel zurück.</returns>
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
                                    try
                                    {
                                        int[] tmpInt = tmp[1].Split(",").Select(int.Parse).ToArray();
                                        streifenList.Add(new Holzstreifen(id, new List<int>(tmpInt)));
                                    }
                                    catch
                                    {
                                        Console.WriteLine(data+" kann nicht in ein Holzstreifen umgewandelt werden.");
                                    }

                                }
                            }
                            string[] dimArray = dim.Split(" ");
                            int[] dimIntArray=new int[0];
                            try
                            {  
                            dimIntArray = dimArray[1].Split(",").Select(int.Parse).ToArray();
                            }
                            catch
                            {
                               
                            }
                            //wenn es kommentare und dimensionsangaben gibt, wird das Puzzle erstellt
                            if (kommentar.Length > 0 && dim.Length > 0 && streifenList.Count > 0)
                            {
                                try
                                {
                                    result.Add(new Holzpuzzel(dimIntArray[0], dimIntArray[2], kommentar, dim, streifenList));
                                }
                                catch { }
                                
                            }
                            else
                                Console.WriteLine("Mit der Datei "+ datei+" konnte kein Holzpuzzle erstellt werden.");

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


        /// <summary>
        /// Erstellt ein Ausgabefile mit der geforderten Formatierung
        /// </summary>
        /// <param name="streifen">Liste der plazierten Streifen</param>
        /// <param name="ebenen">Anzahl der Holzpuzzelebenen</param>
        /// <param name="breite">Anzahl der möglichen zu plazierenden Holzstreifen pro Ebene</param>
        /// <param name="kommentar">Kommentar aus dem Eingabefile</param>
        /// <param name="dimstring">Dimensionsstring aus dem Eingabefile</param>
        /// <param name="number">Nummer des gelösten Puzzels</param>
        public void createAusgabefile(List<Holzstreifen> streifen, int ebenen, int breite, string kommentar, string dimstring,int number)
        {



            // textfile erstellen
            // string filePath = @"C:\Users\di461643\00000ihk\ihk\";
            //string filePath = @"C:\Users\Anwender\Desktop\ihksgit\currProjekt\ihk\";
            string filePath = Pfad;
            filePath +="/"+ DateiName+number + ".txt";
           

            // Write the text to the file
            try
            {
                // Open the file in write mode
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    if (streifen.Count < ebenen * breite)
                    {
                        writer.Write("Keine Loesung gefunden!");
                    }
                    else
                    {
                        writer.Write(kommentar);
                        writer.Write(dimstring + "\n");
                        writer.Write("Anordnung der Teile" + "\n");
                        int currEbene = 1;
                        int counter = 0;
                        string[,] arr = new string[breite, breite + 1];
                        try
                        {
                            foreach (Holzstreifen hs in streifen)
                            {
                                counter++;
                                if (currEbene % 2 == 1)
                                {
                                    for (int a = 0; a < hs.Elemente.Count; a++)
                                    {
                                        arr[counter - 1, a] = "" + hs.Elemente[a];
                                    }
                                    arr[counter - 1, hs.Elemente.Count] = hs.ID;
                                }
                                else
                                {
                                    if (counter == 1)
                                        writer.Write("Ebene " + currEbene + "\n");
                                    string ausgabe = "";
                                    for (int a = 0; a < hs.Elemente.Count; a++)
                                    {
                                        ausgabe += hs.Elemente[a] + " ";
                                    }
                                    ausgabe += hs.ID + "\n";
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
                                                ausgabe += arr[x, y] + " ";
                                            }
                                            writer.Write(ausgabe + "\n");
                                        }
                                    }
                                    if (currEbene != ebenen)
                                        writer.Write("\n");
                                    arr = new string[breite, breite + 1];
                                    currEbene++;
                                    counter = 0;
                                }

                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                //File konnte nicht beschrieben werden
                Console.WriteLine("Error writing to file: " + ex.Message);
            }


        }


    }

}
