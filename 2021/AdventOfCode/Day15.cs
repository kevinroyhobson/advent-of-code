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
        
        private int _xLength;
        private int _yLength;

        private bool[,] _hasPointBeenProcessed;
        private HashSet<Tuple<int, int>> _candidateNextPoints;
        
        public int Puzzle1()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            _xLength = input.First().Length;
            _yLength = input.Count;
            
            _riskLevelMap = new int[_xLength, _yLength];
            for (int y = 0; y < _yLength; y++)
            {
                for (int x = 0; x < _xLength; x++)
                {
                    _riskLevelMap[x, y] = int.Parse(input[y].Substring(x, 1));
                }
            }

            ComputeKnownRiskMap();
            return _knownMinimumRiskMap[_xLength - 1, _yLength - 1].Value;
        }

        public int Puzzle2()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            int tileXLength = input.First().Length;
            int tileYLength = input.Count;

            _xLength = tileXLength * 5;
            _yLength = tileYLength * 5;
            
            _riskLevelMap = new int[_xLength, _yLength];
            for (int y = 0; y < tileXLength; y++)
            {
                for (int x = 0; x < tileYLength; x++)
                {
                    int originalRisk = int.Parse(input[y].Substring(x, 1));

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            int globalX = tileXLength * i + x;
                            int globalY = tileYLength * j + y;
                            int adjustedRisk = GetAdjustedRisk(originalRisk, i, j);
                            _riskLevelMap[globalX, globalY] = adjustedRisk;
                        }
                    }
                }
            }

            ComputeKnownRiskMap();
            return _knownMinimumRiskMap[_xLength - 1, _yLength - 1].Value;
        }

        private int GetAdjustedRisk(int originalRisk, int xOffset, int yOffset)
        {
            int adjustedRisk = originalRisk + xOffset + yOffset;
            if (adjustedRisk > 9)
            {
                adjustedRisk -= 9;
            }

            return adjustedRisk;
        }

        private void ComputeKnownRiskMap()
        {
            _knownMinimumRiskMap = new int?[_xLength, _yLength];
            _hasPointBeenProcessed = new bool[_xLength, _yLength];
            
            _knownMinimumRiskMap[0, 0] = 0;
            _candidateNextPoints = new HashSet<Tuple<int, int>>() {Tuple.Create(0, 0)};

            int numProcessedPoints = 0;
            while (!_hasPointBeenProcessed[_xLength - 1, _yLength - 1])
            {
                var currentPoint = GetNextPointToProcess();
                int x = currentPoint.Item1;
                int y = currentPoint.Item2;

                var neighborsToCheck = new List<Tuple<int, int>>()
                {
                    Tuple.Create(x - 1, y),
                    Tuple.Create(x + 1, y),
                    Tuple.Create(x, y - 1),
                    Tuple.Create(x, y + 1),
                };
                
                foreach (var neighborToCheck in neighborsToCheck.Where(point => IsPointOnMap(point.Item1, point.Item2)))
                {
                    int neighborX = neighborToCheck.Item1;
                    int neighborY = neighborToCheck.Item2;
                    int riskViaThisPath = _knownMinimumRiskMap[x, y].Value + _riskLevelMap[neighborToCheck.Item1, neighborToCheck.Item2];

                    if (!_knownMinimumRiskMap[neighborX, neighborY].HasValue ||
                        riskViaThisPath < _knownMinimumRiskMap[neighborX, neighborY])
                    {
                        _knownMinimumRiskMap[neighborX, neighborY] = riskViaThisPath;
                        if (!_hasPointBeenProcessed[neighborX, neighborY])
                        {
                            _candidateNextPoints.Add(Tuple.Create(neighborX, neighborY));
                        }
                    }
                }

                _hasPointBeenProcessed[x, y] = true;
                _candidateNextPoints.Remove(Tuple.Create(x, y));
                numProcessedPoints++;
                if (numProcessedPoints % 100 == 0)
                {
                    Console.WriteLine($"processed {numProcessedPoints} points");
                }
            }
        }

        private Tuple<int, int> GetNextPointToProcess()
        {
            int minUnprocessedPointRiskValue = int.MaxValue;
            Tuple<int, int> minUnprocessedPoint = null;

            foreach (var candidateNextPoint in _candidateNextPoints)
            {
                int currentRiskValueAtThisPoint = _knownMinimumRiskMap[candidateNextPoint.Item1, candidateNextPoint.Item2].Value;
                if (currentRiskValueAtThisPoint < minUnprocessedPointRiskValue)
                {
                    minUnprocessedPointRiskValue = currentRiskValueAtThisPoint;
                    minUnprocessedPoint = candidateNextPoint;
                }
            }

            return minUnprocessedPoint;
        }

        private bool IsPointOnMap(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _xLength && y < _yLength;
        }
    }
}
