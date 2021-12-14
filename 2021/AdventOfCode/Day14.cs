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

        public long ThePuzzle(int stepsToExecute)
        {
            var originalPolymerStateString = File.ReadLines(InputPath).First();
            var polymerState = GetPolymerState(originalPolymerStateString);
            
            _insertionCharacterByPair = File.ReadLines(InputPath)
                                            .Skip(2)
                                            .ToDictionary(line => line.Split("->", StringSplitOptions.TrimEntries)[0],
                                                          line => line.Split("->", StringSplitOptions.TrimEntries)[1]);

            for (int step = 0; step < stepsToExecute; step++)
            {
                polymerState = GetNextPolymerState(polymerState);
                Console.WriteLine($"Executed step {step + 1}");
            }

            var countByCharacter = new Dictionary<char, long>();
            foreach (var pair in polymerState)
            {
                if (!countByCharacter.ContainsKey(pair.Key[0]))
                {
                    countByCharacter[pair.Key[0]] = 0;
                }

                if (!countByCharacter.ContainsKey(pair.Key[1]))
                {
                    countByCharacter[pair.Key[1]] = 0;
                }

                countByCharacter[pair.Key[0]] += pair.Value;
                countByCharacter[pair.Key[1]] += pair.Value;
            }

            // The first and last characters -- which will be the same in the original string and the final string,
            // are the only characters which are not referenced twice in the dict representation of the polymer. So
            // increment those characters and divide each character occurrence value by 2.
            countByCharacter[originalPolymerStateString.First()]++;
            countByCharacter[originalPolymerStateString.Last()]++;
            var sortedCounts = countByCharacter.Select(count => count.Value / 2)
                                               .OrderByDescending(count => count);
            return sortedCounts.First() - sortedCounts.Last();
        }

        private Dictionary<string, long> GetPolymerState(string polymerStateString)
        {
            var polymerState = new Dictionary<string, long>();
            for (int i = 0; i < polymerStateString.Length - 1; i++)
            {
                var thisPair = polymerStateString.Substring(i, 2);
                if (!polymerState.ContainsKey(thisPair))
                {
                    polymerState[thisPair] = 0;
                }
                polymerState[thisPair]++;
            }

            return polymerState;
        }

        private Dictionary<string, long> GetNextPolymerState(Dictionary<string, long> polymerState)
        {
            var nextPolymerState = new Dictionary<string, long>();
            
            foreach (var currentPolymerPair in polymerState)
            {
                if (_insertionCharacterByPair.ContainsKey(currentPolymerPair.Key))
                {
                    var insertedCharacter = _insertionCharacterByPair[currentPolymerPair.Key];
                    var newPair1 = currentPolymerPair.Key[0] + insertedCharacter;
                    var newPair2 = insertedCharacter + currentPolymerPair.Key[1];

                    if (!nextPolymerState.ContainsKey(newPair1))
                    {
                        nextPolymerState[newPair1] = 0;
                    }

                    if (!nextPolymerState.ContainsKey(newPair2))
                    {
                        nextPolymerState[newPair2] = 0;
                    }
                    
                    nextPolymerState[newPair1] += currentPolymerPair.Value;
                    nextPolymerState[newPair2] += currentPolymerPair.Value;
                }
                else
                {
                    nextPolymerState[currentPolymerPair.Key] = currentPolymerPair.Value;
                }
            }

            return nextPolymerState;
        }
    }
}
