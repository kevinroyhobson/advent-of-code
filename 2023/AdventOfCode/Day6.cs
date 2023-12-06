namespace AdventOfCode;

public class Day6
{
    private const string InputPath = "input/2023-12-06.txt";
    private List<string> _inputLines = File.ReadAllLines(InputPath).ToList();
    
    public long Puzzle1()
    {
        var times = _inputLines[0].Replace("Time:", string.Empty)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(Int32.Parse)
            .ToList();
        
        var distances = _inputLines[1].Replace("Distance:", string.Empty)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(Int32.Parse)
            .ToList();

        int retVal = 1;
        for (int i = 0; i < times.Count; i++)
        {
            int time = times[i];
            int distance = distances[i];

            retVal *= Enumerable.Range(0, time).Count(t => t * (time - t) > distance);
        }

        return retVal;
    }
}
