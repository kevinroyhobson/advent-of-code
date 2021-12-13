using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day13
    {
        private const string InputPath = "input/2021-12-13.txt";

        public int Puzzle1()
        {
            var points = File.ReadLines(InputPath)
                             .Where(line => line != String.Empty)
                             .Where(line => !line.StartsWith("fold"))
                             .Select(line => Tuple.Create(int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])))
                             .ToList();

            var foldInstruction = File.ReadLines(InputPath)
                                      .First(line => line.StartsWith("fold"));

            var paperGrid = new bool[points.Max(point => point.Item1) + 1, points.Max(point => point.Item2) + 1];
            foreach (var point in points)
            {
                paperGrid[point.Item1, point.Item2] = true;
            }

            paperGrid = ApplyFold(foldInstruction, paperGrid);
            int numDots = 0;
            for (int x = 0; x < paperGrid.GetLength(0); x++)
            {
                for (int y = 0; y < paperGrid.GetLength(1); y++)
                {
                    if (paperGrid[x, y])
                    {
                        numDots++;
                    }
                }
            }

            return numDots;
        }
        
        public void Puzzle2()
        {
            var points = File.ReadLines(InputPath)
                             .Where(line => line != String.Empty)
                             .Where(line => !line.StartsWith("fold"))
                             .Select(line => Tuple.Create(int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])))
                             .ToList();

            var foldInstructions = File.ReadLines(InputPath)
                                       .Where(line => line.StartsWith("fold"));

            var paperGrid = new bool[points.Max(point => point.Item1) + 1, points.Max(point => point.Item2) + 1];
            foreach (var point in points)
            {
                paperGrid[point.Item1, point.Item2] = true;
            }

            foreach (var foldInstruction in foldInstructions)
            {
                paperGrid = ApplyFold(foldInstruction, paperGrid);
            }

            for (int y = 0; y < paperGrid.GetLength(1); y++)
            {
                for (int x = 0; x < paperGrid.GetLength(0); x++)
                {
                    Console.Write(paperGrid[x, y] ? "#" : ".");

                }
                Console.WriteLine();
            }
        }

        private bool[,] ApplyFold(string foldInstruction, bool[,] paperGrid)
        {
            string axis = foldInstruction.Split(' ')[2].Split('=')[0];
            int foldLine = int.Parse(foldInstruction.Split(' ')[2].Split('=')[1]);

            bool[,] newGrid = null;

            if (axis == "x")
            {
                newGrid = new bool[foldLine, paperGrid.GetLength(1)];
                for (int x = 0; x < foldLine; x++)
                {
                    for (int y = 0; y < newGrid.GetLength(1); y++)
                    {
                        newGrid[x, y] = paperGrid[x, y];
                    }
                }

                for (int i = 0; foldLine + i < paperGrid.GetLength(0); i++)
                {
                    for (int y = 0; y < newGrid.GetLength(1); y++)
                    {
                        if (paperGrid[foldLine + i, y])
                        {
                            newGrid[foldLine - i, y] = true;
                        }
                    }
                }
            }

            if (axis == "y")
            {
                newGrid = new bool[paperGrid.GetLength(0), foldLine];
                for (int x = 0; x < paperGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < foldLine; y++)
                    {
                        newGrid[x, y] = paperGrid[x, y];
                    }
                }

                for (int x = 0; x < newGrid.GetLength(0); x++)
                {
                    for (int j = 0; foldLine + j < paperGrid.GetLength(1); j++)
                    {
                        if (paperGrid[x, foldLine + j])
                        {
                            newGrid[x, foldLine - j] = true;
                        }
                    }
                }
            }

            return newGrid;
        }
    }
}
