using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day8
    {
        private const string InputPath = "input/2021-12-08.txt";

        public int Puzzle1()
        {
            var entries = File.ReadLines(InputPath)
                              .Select(inputLine => new Entry(inputLine));

            return entries.SelectMany(entry => entry.OutputValues)
                          .Count(outputValue => outputValue.Length == 2 ||
                                                outputValue.Length == 4 ||
                                                outputValue.Length == 3 ||
                                                outputValue.Length == 7);
        }

        private class Entry
        {
            public string[] SignalPatterns { get; }
            public string[] OutputValues { get; }

            public Entry(string input)
            {
                SignalPatterns = input.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                OutputValues = input.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
