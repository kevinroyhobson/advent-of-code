using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day3
{
    private const string _inputPath = "input/2024-12-03.txt";

    public int Puzzle1()
    {
        string input = File.ReadAllText(_inputPath);
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        
        MatchCollection matches = Regex.Matches(input, pattern);
        return matches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
    }

    public int Puzzle2()
    {
        string input = File.ReadAllText(_inputPath);
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
        
        int retVal = 0;
        bool isMultiplyingEnabled = true;
        MatchCollection matches = Regex.Matches(input, pattern);
        foreach(Match match in matches)
        {
            if(match.Value == "do()")
            {
                isMultiplyingEnabled = true;
            }
            else if(match.Value == "don't()")
            {
                isMultiplyingEnabled = false;
            }
            else if(isMultiplyingEnabled)
            {
                retVal += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
            }
        }

        return retVal;
    }
}
