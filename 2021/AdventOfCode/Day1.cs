using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day1
    {
        private const string InputPath = "input/2021-12-01.txt";

        public int Puzzle1()
        {
            var depths = File.ReadLines(InputPath)
                             .Select(int.Parse)
                             .ToList();

            int numIncreases = 0;
            for (int i = 1; i < depths.Count; i++)
            {
                if (depths[i] > depths[i - 1])
                {
                    numIncreases++;
                }
            }

            return numIncreases;
        }

        public int Puzzle2()
        {
            var depths = File.ReadLines(InputPath)
                             .Select(int.Parse)
                             .ToList();

            int numSlidingWindowIncreases = 0;
            int previousSlidingWindowSum = int.MaxValue;
            for (int i = 2; i < depths.Count; i++)
            {
                int currentSlidingWindowSum = depths[i] + depths[i - 1] + depths[i - 2];
                if (currentSlidingWindowSum > previousSlidingWindowSum)
                {
                    numSlidingWindowIncreases++;
                }

                previousSlidingWindowSum = currentSlidingWindowSum;
            }

            return numSlidingWindowIncreases;
        }
    }
}
