using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day7
    {
        private const string InputPath = "input/2021-12-07.txt";

        public long Puzzle1()
        {
            var crabPositions = File.ReadAllText(InputPath)
                                    .Split(',')
                                    .Select(int.Parse);

            int minCrabPosition = crabPositions.Min();
            int maxCrabPosition = crabPositions.Max();
            
            int minFuelRequired = Int32.MaxValue;

            for (int candidateAlignmentPosition = minCrabPosition; 
                 candidateAlignmentPosition <= maxCrabPosition;
                 candidateAlignmentPosition++)
            {
                int totalFuelRequired = crabPositions.Sum(crab => Math.Abs(candidateAlignmentPosition - crab));
                if (totalFuelRequired < minFuelRequired)
                {
                    minFuelRequired = totalFuelRequired;
                }
            }

            return minFuelRequired;
        }
        
        public long Puzzle2()
        {
            var crabPositions = File.ReadAllText(InputPath)
                                    .Split(',')
                                    .Select(int.Parse);

            int minCrabPosition = crabPositions.Min();
            int maxCrabPosition = crabPositions.Max();
            
            int minFuelRequired = Int32.MaxValue;

            for (int candidateAlignmentPosition = minCrabPosition; 
                candidateAlignmentPosition <= maxCrabPosition;
                candidateAlignmentPosition++)
            {
                int totalFuelRequired = crabPositions.Sum(crab => GetFuelRequiredToMoveDistance(Math.Abs(candidateAlignmentPosition - crab)));
                if (totalFuelRequired < minFuelRequired)
                {
                    minFuelRequired = totalFuelRequired;
                }
            }

            return minFuelRequired;
        }

        private int GetFuelRequiredToMoveDistance(int distance)
        {
            if (distance == 0)
            {
                return 0;
            }
            
            if (!_fuelRequiredByDistance.ContainsKey(distance))
            {
                _fuelRequiredByDistance[distance] = GetFuelRequiredToMoveDistance(distance - 1) + distance;
            }
            
            return _fuelRequiredByDistance[distance];
        }
        private readonly Dictionary<int, int> _fuelRequiredByDistance = new();
    }
}
