using PuzzleSolver.Backend;

namespace PuzzleSolver.Interfaces;

public interface IReader
{
    public Puzzle ReadData(string inputFilename); 
}