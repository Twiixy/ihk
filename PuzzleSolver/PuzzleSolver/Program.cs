using PuzzleSolver;
using PuzzleSolver.Backend;

Worker worker = new Worker(new Reader(), new Writer(), new DefaultParser());
worker.DoWork(args);