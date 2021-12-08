using System;
using System.Collections.Generic;
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

        public int Puzzle2()
        {
            var entries = File.ReadLines(InputPath)
                              .Select(inputLine => new Entry(inputLine));

            return entries.Sum(entry => entry.GetOutputValue());
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

            public int GetOutputValue()
            {
                var valueByPattern = GetValueByPattern();
                return valueByPattern[GetOrderedString(OutputValues[0])] * 1000 +
                       valueByPattern[GetOrderedString(OutputValues[1])] * 100 +
                       valueByPattern[GetOrderedString(OutputValues[2])] * 10 +
                       valueByPattern[GetOrderedString(OutputValues[3])];
            }

            private Dictionary<string, int> GetValueByPattern()
            {
                string sevenPattern = SignalPatterns.Single(pattern => pattern.Length == 3);
                string onePattern = SignalPatterns.Single(pattern => pattern.Length == 2);
                string fourPattern = SignalPatterns.Single(pattern => pattern.Length == 4);
                string eightPattern = SignalPatterns.Single(pattern => pattern.Length == 7);
                
                var appearanceCountBySegment = new Dictionary<char, int>();
                foreach (var segment in SignalPatterns.SelectMany(pattern => pattern.ToCharArray()))
                {
                    if (!appearanceCountBySegment.ContainsKey(segment))
                    {
                        appearanceCountBySegment[segment] = 0;
                    }
                    appearanceCountBySegment[segment]++;
                }

                char bottomRightSegment = appearanceCountBySegment.Single(segment => segment.Value == 9).Key;
                char bottomLeftSegment = appearanceCountBySegment.Single(segment => segment.Value == 4).Key;
                char topRightSegment = onePattern.Except(new[] {bottomRightSegment}).Single();

                string sixPattern = SignalPatterns.Single(pattern => pattern.Length == 6 &&
                                                                     !pattern.Contains(topRightSegment));
                string ninePattern = SignalPatterns.Single(pattern => pattern.Length == 6 &&
                                                                      !pattern.Contains(bottomLeftSegment));
                string zeroPattern = SignalPatterns.Single(pattern => pattern.Length == 6 &&
                                                                      pattern != sixPattern &&
                                                                      pattern != ninePattern);

                char middleSegment = eightPattern.Except(zeroPattern).Single();
                char topLeftSegment = fourPattern.Except(onePattern).Except(new[] {middleSegment}).Single();

                string twoPattern = SignalPatterns.Single(pattern => pattern.Length == 5 &&
                                                                     !pattern.Contains(topLeftSegment) &&
                                                                     !pattern.Contains(bottomRightSegment));
                string threePattern = SignalPatterns.Single(pattern => pattern.Length == 5 &&
                                                                       !pattern.Contains(topLeftSegment) &&
                                                                       !pattern.Contains(bottomLeftSegment));
                string fivePattern = SignalPatterns.Single(pattern => pattern.Length == 5 &&
                                                                      !pattern.Contains(topRightSegment) &&
                                                                      !pattern.Contains(bottomLeftSegment));
                
                return new Dictionary<string, int>
                {
                    [GetOrderedString(zeroPattern)] = 0,
                    [GetOrderedString(onePattern)] = 1,
                    [GetOrderedString(twoPattern)] = 2,
                    [GetOrderedString(threePattern)] = 3,
                    [GetOrderedString(fourPattern)] = 4,
                    [GetOrderedString(fivePattern)] = 5,
                    [GetOrderedString(sixPattern)] = 6,
                    [GetOrderedString(sevenPattern)] = 7,
                    [GetOrderedString(eightPattern)] = 8,
                    [GetOrderedString(ninePattern)] = 9
                };
            }

            private string GetOrderedString(string str)
            {
                char[] characters = str.ToCharArray();
                Array.Sort(characters);
                return new string(characters);
            }
        }
    }
}
