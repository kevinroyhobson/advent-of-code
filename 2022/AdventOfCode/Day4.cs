namespace AdventOfCode;

public class Day4
{
    private const string InputPath = "input/2022-12-04.txt";

    public int Puzzle1()
    {
        return File.ReadAllLines(InputPath)
                   .Select(line => new CleaningPair(line))
                   .Count(IsAnyRangeFullyContained);
    }

    private class CleaningPair
    {
        public CleaningRange Elf1 { get; }
        public CleaningRange Elf2 { get; }
        
        public CleaningPair(string input)
        {
            var assignments = input.Split(",");
            Elf1 = new CleaningRange(assignments[0]);
            Elf2 = new CleaningRange(assignments[1]);
        }
    }
    
    private class CleaningRange
    {
        public int Start { get; }
        public int End { get; }

        public CleaningRange(string input)
        {
            var range = input.Split("-");
            Start = int.Parse(range[0]);
            End = int.Parse(range[1]);
        }
    }

    private bool IsAnyRangeFullyContained(CleaningPair cleaningPair)
    {
        return cleaningPair.Elf1.Start <= cleaningPair.Elf2.Start && cleaningPair.Elf1.End >= cleaningPair.Elf2.End ||
               cleaningPair.Elf2.Start <= cleaningPair.Elf1.Start && cleaningPair.Elf2.End >= cleaningPair.Elf1.End;
    }
}
