namespace AdventOfCode;

public class Day1
{
    private const string _inputPath = "input/2024-12-01.txt";

    public int Puzzle1()
    {
        var lines = File.ReadAllLines(_inputPath);
        var left = lines.Select(x => x.Split().First())
                        .Select(int.Parse)
                        .OrderBy(x => x);
        var right = lines.Select( x=> x.Split().Last())
                         .Select(int.Parse)
                         .OrderBy(x => x);

        var distances = left.Zip(right, (l, r) => Math.Abs(l - r));
        return distances.Sum();
    }

    public int Puzzle2()
    {
        var lines = File.ReadAllLines(_inputPath);
        var left = lines.Select(x => x.Split().First())
                        .Select(int.Parse);
        var rightOccurrencesDict = lines.Select( x=> x.Split().Last())
                         .Select(int.Parse)
                         .GroupBy(x => x)
                         .ToDictionary(x => x.Key, x => x.Count());

        var similarities = left.Select(l => l * rightOccurrencesDict.GetValueOrDefault(l));
        return similarities.Sum();
    }
}
