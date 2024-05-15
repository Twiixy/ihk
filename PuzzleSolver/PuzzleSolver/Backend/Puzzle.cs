namespace PuzzleSolver.Backend;

public class Puzzle
{
    public uint X { get; private set; }
    public uint Y { get; private set; }
    public uint Z { get; private set; }

    private List<PuzzlePiece> PuzzlePieces { get; set; }

    public List<string> Comments { get; set; } = new List<string>();

    /// <summary>
    /// Erzeugt ein leeres Puzzle anhand der übergebenen Dimension
    /// </summary>
    /// <param name="x">Anzahl der Spalten</param>
    /// <param name="y">Anzahl der Zeilen</param>
    /// <param name="z">Anzahl der Ebenen</param>
    public Puzzle(uint x, uint y, uint z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Fügt einem leeren <see cref="Puzzle"/> eine Liste von <see cref="PuzzlePiece"/> hinzu
    /// </summary>
    /// <param name="pieces"><see cref="List{T}"/> von <see cref="PuzzlePiece"/></param>
    public void AddPieces(List<PuzzlePiece> pieces)
    {
        PuzzlePieces = pieces;
    }


    public Stack<PuzzlePiece[]> Solve()
    {
        var stages = new Stack<PuzzlePiece[]>();
        uint[,,] puzzle = new uint[Z, X, Y];
        var visited = PuzzlePieces.ToDictionary(piece => piece.Label, piece => false);
        var ret = Solve(puzzle, visited, 0, 0);
        if (!ret)
        {
            throw new Exception("Keine Lösung zum Puzzle möglich...");
        }
        // ermittle Ebenen des Puzzles, füge dem Stack hinzu und gib zurück zum Printen
        return stages;
    }

    private bool Solve(uint[,,] puzzle, Dictionary<string, bool> visited, int z, int index)
    {
        //Ebene voll ?
        if (index >= puzzle.GetLength(1))
        {
            z += 1;
            index = 0;
        }
        // wenn alle Puzzlepiece benutzt wurden oder Ebene max, dann ist man fertig
        if (visited.All(visit => visit.Value) || z >= puzzle.GetLength(0))
        {
            return true;
        }
        // versuchen alle noch möglichen PuzzlePieces zu setzen in allen Zeilen bzw. Spalten je nach Ausrichtung in der gerade sich Befindenen
        var notVisited = visited.Where(visit => !visit.Value).Select(visit => visit.Key);
        var leftOvers = PuzzlePieces.Where(piece => notVisited.Contains(piece.Label)).ToList();
        foreach (var leftOver in leftOvers)
        {
            var clonedLeftOver = leftOver.Clone();
            // checken ob man an der jetzigen Position den Vektor des PuzzlePieces setzen kann insgesamt 4 mal je nach Direction
            var toCheck = GetVector(puzzle, z, index);
            if (toCheck == null || IsValidPosition(toCheck, clonedLeftOver))
            {
                //platzieren des Puzzleteils
                visited[clonedLeftOver.Label] = true;
                PlaceVector(puzzle, clonedLeftOver, z, index);
                // restliche Puzzleteile setzen an nachfolgenden Positionen
                if (Solve(puzzle, visited, z, index + 1))
                {
                    return true;
                }
                // das Setzen hat nicht zum Ergebnis geführt  -> Vektor wieder entfernen (BACKTRACK)
                visited[clonedLeftOver.Label] = false;
            }

            clonedLeftOver.Turn2D();
            if (toCheck == null || IsValidPosition(toCheck, clonedLeftOver))
            {
                visited[clonedLeftOver.Label] = true;
                PlaceVector(puzzle, clonedLeftOver, z, index);
                if (Solve(puzzle, visited, z, index + 1))
                {
                    return true;
                }
                visited[clonedLeftOver.Label] = false;
            }

            clonedLeftOver.Turn3D();
            if (toCheck == null || IsValidPosition(toCheck, clonedLeftOver))
            {
                visited[clonedLeftOver.Label] = true;
                PlaceVector(puzzle, clonedLeftOver, z, index);
                if (Solve(puzzle, visited, z, index + 1))
                {
                    return true;
                }
                visited[clonedLeftOver.Label] = false;
            }

            clonedLeftOver.Turn2D();
            if (toCheck == null || IsValidPosition(toCheck, clonedLeftOver))
            {
                visited[clonedLeftOver.Label] = true;
                PlaceVector(puzzle, clonedLeftOver, z, index);
                if (Solve(puzzle, visited, z, index + 1))
                {
                    return true;
                }
                visited[clonedLeftOver.Label] = false;
            }
        }
        // keine Lösung zum Puzzle vorhanden
        return false;
    }

    private void PlaceVector(uint[,,] puzzle, PuzzlePiece piece, int z, int index)
    {
        if ((z + 1) % 2 == 0)
        {
            //zeilenweise
            FillRow(puzzle, piece, z, index);
        }
        else
        {
            //spaltenweise
            FillColumn(puzzle, piece, z, index);
        }
    }

    private uint[] GetVector(uint[,,] puzzle, int z, int index)
    {
        uint[] toCheck;
        if ((z + 1) % 2 == 0)
        {
            //zeilenweise
            toCheck = GetRow(puzzle, z - 1, index);
        }
        else
        {
            //spaltenweise
            // Spezialfall erste Ebene
            if (z == 0)
            {
                toCheck = null;
            }
            else
            {
                toCheck = GetColumn(puzzle, z - 1, index);
            }
        }
        return toCheck;
    }

    private void FillRow(uint[,,] puzzle, PuzzlePiece piece, int z, int y)
    {
        for (int i = 0; i < piece.Vector.Length; i++)
        {
            puzzle[z, y, i] = piece.Vector[i];
        }
    }

    private void FillColumn(uint[,,] puzzle, PuzzlePiece piece, int z, int x)
    {
        for (int i = 0; i < piece.Vector.Length; i++)
        {
            puzzle[z, i, x] = piece.Vector[i];
        }
    }

    private uint[] GetRow(uint[,,] puzzle, int z, int y)
    {
        var vector = new uint[puzzle.GetLength(1)];
        for (int i = 0; i < puzzle.GetLength(1); i++)
        {
            vector[i] = puzzle[z, y, i];
        }
        return vector;
    }

    private uint[] GetColumn(uint[,,] puzzle, int z, int x)
    {
        var vector = new uint[puzzle.GetLength(1)];
        for (int i = 0; i < puzzle.GetLength(1); i++)
        {
            vector[i] = puzzle[z, i, x];
        }
        return vector;
    }

    private bool IsValidPosition(uint[] toCheck, PuzzlePiece toSet)
    {
        for (int i = 0; i < toCheck.Length; i++)
        {
            if (toCheck[i] == 0)
            {
                continue;
            }

            if (toCheck[i] == 1)
            {
                if (toSet.Vector[i] != 0)
                {
                    return false;
                }
            }

            if (toCheck[i] == 2)
            {
                if (toSet.Vector[i] == 2 ||
                    toSet.Vector[i] == 3)
                {
                    return false;
                }
            }

            if (toCheck[i] == 3)
            {
                if (toSet.Vector[i] != 0)
                {
                    return false;
                }
            }

            if (toCheck[i] == 4)
            {
                if (toSet.Vector[i] == 2 ||
                    toSet.Vector[i] == 3)
                {
                    return false;
                }
            }
        }
        return true;
    }
}