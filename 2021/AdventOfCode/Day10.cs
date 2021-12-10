using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day10
    {
        private const string InputPath = "input/2021-12-10.txt";

        private Dictionary<char, char> _closingCharacterByOpeningCharacter = new Dictionary<char, char> {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        private Dictionary<char, int> _errorScoreByClosingCharacter = new Dictionary<char, int>
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

        public int Puzzle1()
        {
            var input = File.ReadLines(InputPath);

            int totalSyntaxErrorScore = 0;
            
            foreach (var line in input)
            {
                var openingCharacterStack = new Stack<char>();
                foreach (var character in line)
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
                            totalSyntaxErrorScore += _errorScoreByClosingCharacter[character];
                        }
                    }
                }
            }

            return totalSyntaxErrorScore;
        }
    }
}
