using System.Text;

public class Day1
{
    private const string InputPath = "input/2023-12-01.txt";
    
    public int ThePuzzle()
    {
        return File.ReadAllLines(InputPath)
            .Select(line => 10 * GetFirstInt(line) + GetLastInt(line))
            .Sum();
    }

    private Dictionary<string, int> _numberStrings = new Dictionary<string, int>()
    {
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
    };
    
    private int GetFirstInt(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            foreach (var numberString in _numberStrings)
            {
                if (line.Substring(i).StartsWith(numberString.Key))
                {
                    return numberString.Value;
                }
            }
        }

        throw new ApplicationException("No integer found in line");
    }
    
    private int GetLastInt(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            foreach (var numberString in _numberStrings)
            {
                if (line.Substring(i).StartsWith(numberString.Key))
                {
                    return numberString.Value;
                }
            }
        }

        throw new ApplicationException("No integer found in line");
    }
}
