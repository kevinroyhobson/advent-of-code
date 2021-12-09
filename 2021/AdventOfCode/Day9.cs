using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        private const string InputPath = "input/2021-12-09.txt";

        private int[,] _heightMap;
        
        public int Puzzle1()
        {
            _heightMap = GetHeightMap();

            int riskLevelSum = 0;
            for (int x = 1; x < _heightMap.GetUpperBound(0); x++)
            {
                for (int y = 1; y < _heightMap.GetUpperBound(1); y++)
                {
                    int thisHeight = _heightMap[x, y];
                    if (_heightMap[x - 1, y] > thisHeight &&
                        _heightMap[x + 1, y] > thisHeight &&
                        _heightMap[x, y - 1] > thisHeight &&
                        _heightMap[x, y + 1] > thisHeight)
                    {
                        riskLevelSum += (thisHeight + 1);
                    }
                }
            }

            return riskLevelSum;
        }

        public int Puzzle2()
        {
            _heightMap = GetHeightMap();
            var basinSizes = new List<int>();

            for (int x = 1; x < _heightMap.GetUpperBound(0); x++)
            {
                for (int y = 1; y < _heightMap.GetUpperBound(1); y++)
                {
                    int thisHeight = _heightMap[x, y];
                    if (_heightMap[x - 1, y] > thisHeight &&
                        _heightMap[x + 1, y] > thisHeight &&
                        _heightMap[x, y - 1] > thisHeight &&
                        _heightMap[x, y + 1] > thisHeight)
                    {
                        basinSizes.Add(GetSizeOfBasin(x, y));
                    }
                }
            }

            basinSizes = basinSizes.OrderByDescending(size => size).ToList();
            return basinSizes[0] * basinSizes[1] * basinSizes[2];
        }

        private int GetSizeOfBasin(int x, int y)
        {
            int basinSize = 0;
            
            var pointsToCheck = new Queue<Tuple<int,int>>();
            pointsToCheck.Enqueue(Tuple.Create(x, y));

            var checkedPoints = new HashSet<Tuple<int, int>>();

            while (pointsToCheck.Any())
            {
                var currentPoint = pointsToCheck.Dequeue();
                if (!checkedPoints.Contains(currentPoint) &&
                    _heightMap[currentPoint.Item1, currentPoint.Item2] < 9)
                {
                    basinSize++;
                    checkedPoints.Add(currentPoint);
                    
                    pointsToCheck.Enqueue(Tuple.Create(currentPoint.Item1 - 1, currentPoint.Item2));
                    pointsToCheck.Enqueue(Tuple.Create(currentPoint.Item1 + 1, currentPoint.Item2));
                    pointsToCheck.Enqueue(Tuple.Create(currentPoint.Item1, currentPoint.Item2 - 1));
                    pointsToCheck.Enqueue(Tuple.Create(currentPoint.Item1, currentPoint.Item2 + 1));
                }
            }

            return basinSize;
        }

        private int[,] GetHeightMap()
        {
            var input = File.ReadLines(InputPath)
                            .ToArray();

            var heightMap = new int[input[0].Length + 2, input.Length + 2];
            for (int y = -1; y <= input.Length; y++)
            {
                for (int x = -1; x <= input[0].Length; x++)
                {
                    if (y == -1 || x == -1 || y == input.Length || x == input[0].Length)
                    {
                        heightMap[x + 1, y + 1] = int.MaxValue;
                    }
                    else
                    {
                        heightMap[x + 1, y + 1] = int.Parse(input[x].Substring(y, 1));
                    }
                }
            }

            return heightMap;
        }
    }
}
