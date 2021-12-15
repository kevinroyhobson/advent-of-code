using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day15
    {
        private const string InputPath = "input/2021-12-15.txt";

        private int[,] _riskLevelMap;
        private int?[,] _knownMinimumRiskMap;
        
        public int Puzzle1()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            var xLength = input.First().Length;
            var yLength = input.Count;
            
            _riskLevelMap = new int[xLength, yLength];
            _knownMinimumRiskMap = new int?[xLength, yLength];
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    _riskLevelMap[x, y] = int.Parse(input[y].Substring(x, 1));
                }
            }

            var pointsToProcess = new Queue<Tuple<int, int>>();
            _knownMinimumRiskMap[xLength - 1, yLength - 1] = _riskLevelMap[xLength - 1, yLength - 1];
            pointsToProcess.Enqueue(Tuple.Create(xLength - 2, yLength - 1));
            pointsToProcess.Enqueue(Tuple.Create(xLength - 1, yLength - 2));

            while (pointsToProcess.Any())
            {
                var thisPoint = pointsToProcess.Dequeue();
                var x = thisPoint.Item1;
                var y = thisPoint.Item2;

                if (_knownMinimumRiskMap[x, y].HasValue)
                {
                    continue;
                }

                int minimumRiskFromHere = Int32.MaxValue;
                if (IsPointOnMap(x + 1, y))
                {
                    minimumRiskFromHere = Math.Min(minimumRiskFromHere, _knownMinimumRiskMap[x + 1, y].Value);
                }

                if (IsPointOnMap(x, y + 1))
                {
                    minimumRiskFromHere = Math.Min(minimumRiskFromHere, _knownMinimumRiskMap[x, y + 1].Value);
                }

                _knownMinimumRiskMap[x, y] = _riskLevelMap[x, y] + minimumRiskFromHere;

                if (IsPointOnMap(x - 1, y))
                {
                    pointsToProcess.Enqueue(Tuple.Create(x - 1, y));
                }

                if (IsPointOnMap(x, y - 1))
                {
                    pointsToProcess.Enqueue(Tuple.Create(x, y - 1));
                }
            }

            return _knownMinimumRiskMap[0, 0].Value - _riskLevelMap[0, 0];
        }

        private bool IsPointOnMap(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _riskLevelMap.GetLength(0) && y < _riskLevelMap.GetLength(1);
        }
    }
}
