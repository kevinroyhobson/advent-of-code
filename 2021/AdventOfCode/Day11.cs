using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day11
    {
        private const string InputPath = "input/2021-12-11.txt";

        public int Puzzle1()
        {
            var grid = new OctopusGrid(InputPath);

            for (int step = 0; step < 100; step++)
            {
                grid.Step();                
            }
            
            return grid.NumFlashesSoFar;
        }

        private class OctopusGrid
        {
            private int[,] _theGrid;
            public int NumFlashesSoFar { get; private set; }
            
            public OctopusGrid(string inputPath)
            {
                _theGrid = new int[10, 10];
                var input = File.ReadLines(inputPath)
                                .ToArray();

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        _theGrid[x, y] = int.Parse(input[x].Substring(y, 1));
                    }
                }
            }

            public void Step()
            {
                var pointsToFlash = new Queue<Tuple<int, int>>();
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        _theGrid[x, y]++;
                        if (_theGrid[x, y] == 10)
                        {
                            pointsToFlash.Enqueue(Tuple.Create(x, y));
                        }
                    }
                }

                while (pointsToFlash.Any())
                {
                    var thisPoint = pointsToFlash.Dequeue();
                    NumFlashesSoFar++;
                    int x = thisPoint.Item1;
                    int y = thisPoint.Item2;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (!IsPointOnGrid(x + i, y + j) || (i == 0 && j == 0))
                            {
                                continue;
                            }

                            _theGrid[x + i, y + j]++;
                            if (_theGrid[x + i, y + j] == 10)
                            {
                                pointsToFlash.Enqueue(Tuple.Create(x + i, y + j));
                            }
                        }
                    }
                }

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (_theGrid[x, y] >= 10)
                        {
                            _theGrid[x, y] = 0;
                        }
                    }
                }
            }

            private bool IsPointOnGrid(int x, int y)
            {
                return x is >= 0 and < 10 && y is >= 0 and < 10;
            }
        }
    }
}
