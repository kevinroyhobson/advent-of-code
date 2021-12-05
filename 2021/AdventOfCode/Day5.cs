using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day5
    {
        private const string InputPath = "input/2021-12-05.txt";

        public int Puzzle1()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            var ventLines = input.Select(i => new VentLine(i));
            var ventLineEdges = new[]
            {
                ventLines.Max(vl => vl.xStart),
                ventLines.Max(vl => vl.xEnd),
                ventLines.Max(vl => vl.yStart),
                ventLines.Max(vl => vl.yEnd)
            };

            var oceanSize = ventLineEdges.Max() + 1;

            int[,] oceanMap = new int[oceanSize, oceanSize];
            foreach (var ventLine in ventLines)
            {
                if (ventLine.xStart == ventLine.xEnd)
                {
                    int yMin = Math.Min(ventLine.yStart, ventLine.yEnd);
                    int yMax = Math.Max(ventLine.yStart, ventLine.yEnd);
                    for (int y = yMin; y <= yMax; y++)
                    {
                        oceanMap[ventLine.xStart, y]++;
                    }
                }

                if (ventLine.yStart == ventLine.yEnd)
                {
                    int xMin = Math.Min(ventLine.xStart, ventLine.xEnd);
                    int xMax = Math.Max(ventLine.xStart, ventLine.xEnd);
                    for (int x = xMin; x <= xMax; x++)
                    {
                        oceanMap[x, ventLine.yStart]++;
                    }
                }
            }

            int numDangerousAreas = 0;
            foreach (var location in oceanMap)
            {
                if (location > 1)
                {
                    numDangerousAreas++;
                }
            }

            return numDangerousAreas;
        }
        
        public int Puzzle2()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            var ventLines = input.Select(i => new VentLine(i));
            var ventLineEdges = new[]
            {
                ventLines.Max(vl => vl.xStart),
                ventLines.Max(vl => vl.xEnd),
                ventLines.Max(vl => vl.yStart),
                ventLines.Max(vl => vl.yEnd)
            };

            var oceanSize = ventLineEdges.Max() + 1;

            int[,] oceanMap = new int[oceanSize, oceanSize];
            foreach (var ventLine in ventLines)
            {
                if (ventLine.xStart == ventLine.xEnd)
                {
                    int yMin = Math.Min(ventLine.yStart, ventLine.yEnd);
                    int yMax = Math.Max(ventLine.yStart, ventLine.yEnd);
                    for (int y = yMin; y <= yMax; y++)
                    {
                        oceanMap[ventLine.xStart, y]++;
                    }
                }

                else if (ventLine.yStart == ventLine.yEnd)
                {
                    int xMin = Math.Min(ventLine.xStart, ventLine.xEnd);
                    int xMax = Math.Max(ventLine.xStart, ventLine.xEnd);
                    for (int x = xMin; x <= xMax; x++)
                    {
                        oceanMap[x, ventLine.yStart]++;
                    }
                }

                else // Line is perfectly diagonal
                {
                    int xStep = ventLine.xEnd - ventLine.xStart > 0 ? 1 : -1;
                    int yStep = ventLine.yEnd - ventLine.yStart > 0 ? 1 : -1;
                    int numLocations = Math.Abs(ventLine.xEnd - ventLine.xStart) + 1;

                    for (int i = 0; i < numLocations; i++)
                    {
                        int thisX = ventLine.xStart + (i * xStep);
                        int thisY = ventLine.yStart + (i * yStep);
                        oceanMap[thisX, thisY]++;
                    }
                }
            }

            int numDangerousAreas = 0;
            foreach (var location in oceanMap)
            {
                if (location > 1)
                {
                    numDangerousAreas++;
                }
            }

            return numDangerousAreas;
        }
        
        private class VentLine
        {
            public VentLine(string inputLine)
            {
                xStart = int.Parse(inputLine.Split("->", StringSplitOptions.RemoveEmptyEntries)
                                            [0].Split(",")
                                            [0]);
                yStart = int.Parse(inputLine.Split("->", StringSplitOptions.RemoveEmptyEntries)
                                            [0].Split(",")
                                            [1]);
                
                xEnd = int.Parse(inputLine.Split("->", StringSplitOptions.RemoveEmptyEntries)
                                          [1].Split(",")
                                          [0]);
                yEnd = int.Parse(inputLine.Split("->", StringSplitOptions.RemoveEmptyEntries)
                                          [1].Split(",")
                                          [1]);
            }
            
            public int xStart { get; }
            public int yStart { get; }
            
            public int xEnd { get; }
            public int yEnd { get; }
        }
    }
}
