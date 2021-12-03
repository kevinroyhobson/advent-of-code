using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day3
    {
        private const string InputPath = "input/2021-12-03.txt";

        public int Puzzle1()
        {
            var binaryStrings = File.ReadLines(InputPath)
                                    .ToArray();

            string gammaRateString = string.Empty;
            string epsilonRateString = string.Empty;

            int binaryWidth = binaryStrings.First().Length;
            for (int i = 0; i < binaryWidth; i++)
            {
                int numZeroes = binaryStrings.Count(b => b[i] == '0');
                int numOnes = binaryStrings.Count(b => b[i] == '1');

                var gammaValue = numZeroes > numOnes ? "0" : "1";
                var epsilonValue = numZeroes > numOnes ? "1" : "0";

                gammaRateString += gammaValue;
                epsilonRateString += epsilonValue;
            }

            int gammaRate = Convert.ToInt32(gammaRateString, 2);
            int epsilonRate = Convert.ToInt32(epsilonRateString, 2);

            return gammaRate * epsilonRate;
        }
    }
}