public class Day1
{
    private const string InputPath = "input/2023-12-01.txt";
    public int Puzzle1()
    {
        return File.ReadAllLines(InputPath)
            .Select(line => 10 * GetFirstInt(line) + GetLastInt(line))
            .Sum();
    }
    
    private int GetFirstInt(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                return Int32.Parse(line[i].ToString());
            }
        }

        throw new ApplicationException("No integer found in line");
    }
    
    private int GetLastInt(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                return Int32.Parse(line[i].ToString());
            }
        }

        throw new ApplicationException("No integer found in line");
    }
    
    
}


