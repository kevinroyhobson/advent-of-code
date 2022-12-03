namespace AdventOfCode;

public class Day1
{
    private const string InputPath = "input/2022-12-01.txt";
    
    public int Puzzle1()
    {
        return GetElfCalorieCounts().Max();
    }
    
    public int Puzzle2()
    {
        return GetElfCalorieCounts()
            .OrderByDescending(cc => cc)
            .Take(3)
            .Sum();
    }

    private IEnumerable<int> GetElfCalorieCounts()
    {
        var input = File.ReadLines(InputPath).ToList();
        var currentElfCalories = 0;

        foreach (var item in input)
        {
            if (item.IsNullOrEmpty())
            {
                yield return currentElfCalories;
                currentElfCalories = 0;
            }
            else
            {
                currentElfCalories += int.Parse(item);
            }
        }
        
        yield return currentElfCalories;
    }
}
