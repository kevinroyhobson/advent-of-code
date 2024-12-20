using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode;

public class Day4
{
    private const string _inputPath = "input/2024-12-04.txt";
    private char[][] _grid;

    public Day4()
    {
        _grid = File.ReadAllLines(_inputPath)
                    .Select(line => line.ToCharArray())
                    .ToArray();
    }

    public int Puzzle1()
    {
        int numXmases = 0;
        for (int x = 0; x < _grid[0].Length; x++)
        {
            for (int y = 0; y < _grid.Length; y++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        if (IsWordMatch("XMAS", x, y, i, j))
                        {
                            numXmases++;
                        }
                    }
                }
            }
        }

        return numXmases;
    }

    private bool IsWordMatch(string word, int x, int y, int xDirection, int yDirection)
    {
        if (x < 0 || x >= _grid[0].Length || y < 0 || y >= _grid.Length)
        {
            return false;
        }

        if (word.Length == 1)
        {
            return _grid[y][x] == word[0];
        }

        return _grid[y][x] == word[0] && IsWordMatch(word[1..], x + xDirection, y + yDirection, xDirection, yDirection);
    }

}