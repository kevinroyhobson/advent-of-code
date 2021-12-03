using System;
using System.Collections.Generic;
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

        public int Puzzle2()
        {
            var binaryStrings = File.ReadLines(InputPath)
                                    .ToList();
            
            return GetOxygenGeneratorRating(binaryStrings) * 
                   GetCarbonDioxideScrubberRating(binaryStrings);
        }

        private int GetOxygenGeneratorRating(List<string> binaryStrings)
        {
            int binaryStringIndex = 0;
            while (binaryStrings.Count > 1)
            {
                int numZeroes = binaryStrings.Count(b => b[binaryStringIndex] == '0');
                int numOnes = binaryStrings.Count(b => b[binaryStringIndex] == '1');

                binaryStrings = numZeroes > numOnes 
                                    ? binaryStrings.Where(b => b[binaryStringIndex] == '0').ToList()
                                    : binaryStrings.Where(b => b[binaryStringIndex] == '1').ToList();

                binaryStringIndex++;
            }

            return Convert.ToInt32(binaryStrings.Single(), 2);
        }

        private int GetCarbonDioxideScrubberRating(List<string> binaryStrings)
        {
            int binaryStringIndex = 0;
            while (binaryStrings.Count > 1)
            {
                int numZeroes = binaryStrings.Count(b => b[binaryStringIndex] == '0');
                int numOnes = binaryStrings.Count(b => b[binaryStringIndex] == '1');

                binaryStrings = numOnes < numZeroes 
                                    ? binaryStrings.Where(b => b[binaryStringIndex] == '1').ToList()
                                    : binaryStrings.Where(b => b[binaryStringIndex] == '0').ToList();

                binaryStringIndex++;
            }

            return Convert.ToInt32(binaryStrings.Single(), 2);
        }
    }
}