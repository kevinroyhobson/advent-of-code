using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
    {
        private const string InputPath = "input/2021-12-06.txt";

        public long Puzzle1()
        {
            var initialFish = File.ReadAllText(InputPath)
                                  .Split(',')
                                  .Select(int.Parse);

            long[] numFishByTimerValue = new long[9];
            for (int i = 0; i <= 8; i++)
            {
                numFishByTimerValue[i] = initialFish.Count(fish => fish == i);
            }

            for (int day = 0; day < 80; day++)
            {
                long[] nextNumFishByTimerValue = new long[9];

                nextNumFishByTimerValue[8] = numFishByTimerValue[0];
                nextNumFishByTimerValue[6] = numFishByTimerValue[0];

                for (int i = 0; i < 8; i++)
                {
                    nextNumFishByTimerValue[i] += numFishByTimerValue[i + 1];
                }

                numFishByTimerValue = nextNumFishByTimerValue;
            }

            return numFishByTimerValue.Sum();
        }
    }
}
