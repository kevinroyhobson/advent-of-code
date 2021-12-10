using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day10
    {
        private const string InputPath = "input/2021-12-10.txt";

        public int Puzzle1()
        {
            return File.ReadLines(InputPath)
                        .Select(line => new ChunkLine(line))
                        .Sum(chunkLine => chunkLine.GetCorruptedLineScore());
        }

        public long Puzzle2()
        {
            var lineCompletionScores = File.ReadLines(InputPath)
                                           .Select(line => new ChunkLine(line))
                                           .Where(chunkLine => chunkLine.GetCorruptedLineScore() == 0)
                                           .Select(chunkLine => chunkLine.GetLineCompletionScore())
                                           .OrderBy(score => score)
                                           .ToArray();

            int middleIndex = (lineCompletionScores.Length / 2);
            return lineCompletionScores[middleIndex];
        }

        private class ChunkLine
        {
            private string _line;

            public ChunkLine(string line)
            {
                _line = line;
            }

            public int GetCorruptedLineScore()
            {
                var openingCharacterStack = new Stack<char>();
                foreach (var character in _line)
                {
                    if (_closingCharacterByOpeningCharacter.ContainsKey(character))
                    {
                        openingCharacterStack.Push(character);
                    }
                    else
                    {
                        var mostRecentOpeningCharacter = openingCharacterStack.Pop();
                        if (character != _closingCharacterByOpeningCharacter[mostRecentOpeningCharacter])
                        {
                            return _errorScoreByClosingCharacter[character];
                        }
                    }
                }

                return 0;
            }

            public long GetLineCompletionScore()
            {
                var openingCharacterStack = new Stack<char>();
                foreach (var character in _line)
                {
                    if (_closingCharacterByOpeningCharacter.ContainsKey(character))
                    {
                        openingCharacterStack.Push(character);
                    }
                    else
                    {
                        openingCharacterStack.Pop();
                    }
                }

                string completionString = string.Empty;
                while (openingCharacterStack.Any())
                {
                    char mostRecentOpeningCharacter = openingCharacterStack.Pop();
                    completionString += _closingCharacterByOpeningCharacter[mostRecentOpeningCharacter];
                }

                long completionScore = 0;
                foreach (var closingCharacter in completionString)
                {
                    completionScore *= 5;
                    completionScore += _completionScoreByClosingCharacter[closingCharacter];
                }

                return completionScore;
            }

            private readonly Dictionary<char, char> _closingCharacterByOpeningCharacter = new()
            {
                {'(', ')'},
                {'[', ']'},
                {'{', '}'},
                {'<', '>'}
            };

            private readonly Dictionary<char, int> _errorScoreByClosingCharacter = new()
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };

            private readonly Dictionary<char, int> _completionScoreByClosingCharacter = new()
            {
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4}
            };
        }
    }
}
