using PuzzleSolver.Config;

namespace PuzzleSolver.Interfaces;

/// <summary>
/// Das Interface 'IParser' stellt eine Methode zur Verfügung, um Eingabeargumente zu parsen und in eine strukturierte Form umzuwandeln.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parst die bereitgestellten Eingabeargumente und konvertiert sie in ein <see cref="Arguments"/> Objekt.
    /// </summary>
    /// <param name="args">Die Eingabeargumente, die von der Kommandozeile oder einer anderen Datenquelle übergeben werden.</param>
    /// <returns>
    /// Ein <see cref="Arguments"/> Objekt, das die geparsten und strukturierten Argumente enthält.
    /// </returns>
    public Arguments Parse(string[] args);
}