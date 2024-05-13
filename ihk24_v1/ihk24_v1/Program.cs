using System;
using System.IO;

namespace ihk24_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fordern Sie den Benutzer auf, einen Ordnerpfad einzugeben
            Console.Write("Geben Sie den Ordnerpfad ein: ");
            string ordnerPfad = Console.ReadLine();
            string ausgabe = "";

            // Prüfen Sie, ob der Ordner existiert
            if (Directory.Exists(ordnerPfad))
            {
                // Durchsuchen Sie den Ordner nach Textdateien
                foreach (string datei in Directory.EnumerateFiles(ordnerPfad, "*.txt", SearchOption.AllDirectories))
                {
                    try
                    {
                        // Lesen Sie den Inhalt der Datei
                        string dateiInhalt = File.ReadAllText(datei);

                        // Geben Sie den Dateinamen und Inhalt aus
                        Console.WriteLine("Datei: " + datei);
                        Console.WriteLine(dateiInhalt);
                        ausgabe += dateiInhalt;
                        Console.WriteLine("----------------------");
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Fehler beim Lesen der Datei: " + datei + " - " + e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Ordner nicht gefunden: " + ordnerPfad);
            }






            // textfile erstellen
            string filePath = @"C:\Users\Anwender\test\MyFile.txt";
            string textContent = "This is the text to be written to the file.";

            // Write the text to the file
            try
            {
                // Open the file in write mode
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the text to the file
                    writer.Write(ausgabe);
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
