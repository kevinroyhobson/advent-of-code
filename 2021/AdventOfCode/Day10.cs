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
        }
    }
}
