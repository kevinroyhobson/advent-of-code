namespace AdventOfCode;

public class Day3
{
    private const string InputPath = "input/2022-12-03.txt";

    public int Puzzle1()
    {
        return File.ReadAllLines(InputPath)
                   .Select(GetItemInBothCompartments)
                   .Select(GetItemPriority)
                   .Sum();
    }
    
    private char GetItemInBothCompartments(string rucksackContents)
    {
        var compartmentLength = rucksackContents.Length / 2;
        var firstCompartment = rucksackContents[..compartmentLength];
        var secondCompartment = rucksackContents[compartmentLength..];

        return GetCommonItem(firstCompartment, secondCompartment);
    }
    
    private int GetItemPriority(char item)
    {
        return item is >= 'a' and <= 'z' 
            ? item - 'a' + 1 
            : item - 'A' + 27;
    }

    public int Puzzle2()
    {
        return File.ReadAllLines(InputPath)
                   .Chunk(3)
                   .Select(GetCommonItem)
                   .Select(GetItemPriority)
                   .Sum();
    }

    // Assumes that there is only one common item amongst all the rucksacks.
    private char  GetCommonItem(params string[] rucksacks)
    {
        var commonItems = rucksacks[0].ToHashSet();
        for (var i = 1; i < rucksacks.Length; i++)
        {
            commonItems.IntersectWith(rucksacks[i]);
        }

        return commonItems.Single();
    }
}
