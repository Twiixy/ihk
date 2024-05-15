using System.Text;
using PuzzleSolver.Config;
using PuzzleSolver.Interfaces;

namespace PuzzleSolver.Backend;

public class DefaultParser : IParser
{
    /// <summary>
    /// Parst die gegebenen Eingabeargumente und erzeugt daraus eine Instanz von <see cref="Arguments"/>.
    /// </summary>
    /// <param name="args">Die Eingabeargumente, die vom Benutzer übergeben wurden.</param>
    /// <returns>Eine Instanz von <see cref="Arguments"/>, die die geparsten Daten enthält.</returns>
    /// <exception cref="ArgumentException">Wird geworfen, wenn keine Argumente übergeben wurden oder spezifische erwartete Argumente fehlen.</exception>
    public Arguments Parse(string[] args) //TODO: Anpassen an Aufgabenstellung
    {
        string inputFilename = string.Empty;
        if (args == null || args.Length == 0)
        {
            throw new ArgumentException("No arguments given! Call 'Aufgabe.exe --help' to get further information...");
        }
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Equals("--help"))
            {
                PrintHelpText();
                Environment.Exit(0);
            }
            else if (args[i].Equals("--file", StringComparison.OrdinalIgnoreCase) || args[i].Equals("-f", StringComparison.OrdinalIgnoreCase) || args[i].Equals("/file", StringComparison.OrdinalIgnoreCase))
            {
                inputFilename = args[++i];
            }
            else
            {
                throw new ArgumentException("Does not know argument!");
            }
        }
        if (string.IsNullOrEmpty(inputFilename))
        {
            throw new ArgumentException("No inputDir given! Call 'Aufgabe.exe --help' to get further information...");
        }
        return new Arguments
        {
            InputFile = inputFilename
        };
    }

    /// <summary>
    /// Gibt einen Hilfetext auf der Konsole aus, der Informationen über die Anwendung beinhaltet.
    /// </summary>
    /// <remarks>
    /// Die Methode verwendet einen <see cref="System.Text.StringBuilder"/> zur effizienten Erstellung
    /// des Ausgabetextes.
    /// </remarks>
    private void PrintHelpText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("NAME\n");
        sb.Append("\tConsolenApp1.exe\n\n"); //TODO: Anpassen an Aufgabenstellung
        sb.Append("ÜBERSICHT\n");
        sb.Append("\tErstellt asynchron Ausgabedateien für die Simulation einer ConsolenTestApp anhand eines übergebenen Verzeichnisses,\n\twobei die Dateien ein bestimmtes Eingabeformat in diesem Verzeichnis benötigen\n\n");
        sb.Append("SYNTAX\n");
        sb.Append("\tConsoleApp1.exe [--file | -d | /file <string>] [--help | -h | /help]\n\n");
        sb.Append("BESCHREIBUNG\n");
        sb.Append("\tErstellt Ausgabedateien für die Simulation einer ConsolenTestApp anhand eines übergebenen Verzeichnisses,\n\twobei die Dateien ein bestimmtes Eingabeformat in diesem Verzeichnis benötigen\n\n");
        sb.Append("PARAMETER\n");
        sb.Append("\t--file | -f | /file <string>\n");
        sb.Append("\t\tName der Datei, die eingelesen werden soll.\n\n");
        sb.Append("\t\tErforderlich?\t\t\ttrue\n");
        sb.Append("\t\tStandardwert\t\t\tNone\n\n");
        //TODO: weiteres angepasst an Aufgabenstellung
        Console.WriteLine(sb.ToString());
    }
}
