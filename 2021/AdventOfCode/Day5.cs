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
            
            public int xStart { get; set; }
            public int yStart { get; set; }
            
            public int xEnd { get; set; }
            public int yEnd { get; set; }
        }
    }
}
