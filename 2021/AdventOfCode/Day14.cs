using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day14
    {
        private const string InputPath = "input/2021-12-14.txt";

        private Dictionary<string, string> _insertionCharacterByPair;

        public int Puzzle1()
        {
            var polymerState = File.ReadLines(InputPath).First();

            _insertionCharacterByPair = File.ReadLines(InputPath)
                                            .Skip(2)
                                            .ToDictionary(line => line.Split("->", StringSplitOptions.TrimEntries)[0],
                                                          line => line.Split("->", StringSplitOptions.TrimEntries)[1]);

            for (int step = 0; step < 10; step++)
            {
                polymerState = GetNextPolymerState(polymerState);
            }

            var countByCharacter = new Dictionary<char, int>();
            for (int i = 0; i < polymerState.Length; i++)
            {
                if (!countByCharacter.ContainsKey(polymerState[i]))
                {
                    countByCharacter[polymerState[i]] = 0;
                }

                countByCharacter[polymerState[i]]++;
            }

            var sortedCounts = countByCharacter.OrderByDescending(count => count.Value);
            return sortedCounts.First().Value - sortedCounts.Last().Value;
        }

        private string GetNextPolymerState(string polymerState)
        {
            string nextPolymerState = String.Empty;

            for (int i = 0; i < polymerState.Length - 1; i++)
            {
                string thisPair = polymerState.Substring(i, 2);
                nextPolymerState += thisPair[0];
                if (_insertionCharacterByPair.ContainsKey(thisPair))
                {
                    nextPolymerState += _insertionCharacterByPair[thisPair];
                }
            }
            nextPolymerState += polymerState[^1];

            return nextPolymerState;
        }
    }
}
