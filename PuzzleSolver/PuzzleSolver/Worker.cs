using PuzzleSolver.Backend;
using PuzzleSolver.Interfaces;

namespace PuzzleSolver;

public class Worker
{
    private readonly IReader Reader;

    private readonly IWriter Writer;

    private readonly IParser Parser;

    public Worker(IReader reader, IWriter writer, IParser parser)
    {
        this.Reader = reader;
        this.Writer = writer;
        this.Parser = parser;
    }

    public void DoWork(string[] args)
    {
        var arguments = Parser.Parse(args);
        Console.WriteLine("STARTING SIMULATION");
        Puzzle puzzle = Reader.ReadData(arguments.InputFile);
        var ergebnis = puzzle.Solve();
        Writer.WriteData("output-file", ergebnis, puzzle.Comments);
        Console.WriteLine("FINISHED SIMULATION");
    }
}