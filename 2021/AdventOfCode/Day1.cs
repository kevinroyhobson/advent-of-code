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
    }
}
