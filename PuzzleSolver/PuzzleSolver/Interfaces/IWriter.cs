using PuzzleSolver.Backend;

namespace PuzzleSolver.Interfaces;

/// <summary>
/// Das Interface 'IWriter' definiert eine standardisierte Methode zum Schreiben von Daten in eine Ausgabedatei.
/// </summary>
public interface IWriter
{
    /// <summary>
    /// Schreibt Daten in die angegebene Ausgabedatei.
    /// </summary>
    /// <param name="outputFile">Der Pfad der Ausgabedatei, in die die Daten geschrieben werden sollen.</param>
    /// <remarks>
    /// Implementierungen dieses Interfaces sollten die spezifische Logik zum Öffnen, Schreiben und Schließen
    /// der Datei sowie die Behandlung möglicher Ausnahmen bereitstellen.
    /// </remarks>
    public void WriteData(string outputFile, Stack<PuzzlePiece[]> data, List<string> comments);
}