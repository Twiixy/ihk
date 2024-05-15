using System.Data;
using System.Net;
using PuzzleSolver.Interfaces;

namespace PuzzleSolver.Backend;

public class Reader : IReader
{
    public Puzzle ReadData(string inputFilename)
    {
        Puzzle puzzle = null;
        var comments = new List<string>();
        var puzzlePieces = new List<PuzzlePiece>();

        using (StreamReader streamReader = new StreamReader(inputFilename))
        {
            string row;
            while ((row = streamReader.ReadLine()) != null)
            {
                if (row.StartsWith("//"))
                {
                    comments.Add(row);
                    continue;
                }

                if (row.StartsWith("Dimension"))
                {
                    var dimensionLine = row.Split([' ', '\t', ',']);
                    if (!uint.TryParse(dimensionLine[1].Trim(), out uint x) ||
                        !uint.TryParse(dimensionLine[2].Trim(), out uint y) ||
                        !uint.TryParse(dimensionLine[3].Trim(), out uint z))
                    {
                        throw new ArgumentException(
                            "Ungültige Dimensionen. Alle Dimensionen müssen positive ganze Zahlen sein.");
                    }

                    puzzle = new Puzzle(x, y, z);
                    continue;
                }

                var inputLine = row.Split([' ', '\t', ',']);
                PuzzlePiece puzzlePiece = new PuzzlePiece(inputLine.Length - 1)
                {
                    Label = inputLine[0].Trim()
                };

                for (int i = 1; i < inputLine.Length; i++)
                {
                    if (!uint.TryParse(inputLine[i], out uint value))
                    {
                        throw new ArgumentException($"Ungültige Kodierung: {inputLine[i]}");
                    }
                    puzzlePiece.Vector[i - 1] = value;
                }
                puzzlePieces.Add(puzzlePiece);
            }
        }
        if (puzzle == null)
        {
            throw new Exception("Input File wrong. Aufgabe sagt aber syntaktisch wäre alles richtig angegeben...");
        }
        puzzle.Comments = comments;
        puzzle.AddPieces(puzzlePieces);
        return puzzle;
    }
}