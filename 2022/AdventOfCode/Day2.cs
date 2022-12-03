namespace AdventOfCode;

public class Day2
{
    private const string InputPath = "input/2022-12-02.txt";

    public int Puzzle1()
    {
        return File.ReadAllLines(InputPath)
                   .Select(GetRoundAccordingToPart1)
                   .Select(GetScore)
                   .Sum();
    }
    
    public int Puzzle2()
    {
        return File.ReadAllLines(InputPath)
                   .Select(GetRoundAccordingToPart2)
                   .Select(GetScore)
                   .Sum();
    }
    
    private enum Shape
    {
        Rock = 1, 
        Paper = 2, 
        Scissors = 3
    }
    
    private class Round
    {
        public Shape ElfShape { get; init; }
        public Shape YourShape { get; init; }
    }

    private Round GetRoundAccordingToPart1(string line)
    {
        return new Round() { ElfShape = GetShape(line[0]), YourShape = GetShape(line[2]) };
    }
    
    private Round GetRoundAccordingToPart2(string line)
    {
        var elfShape = GetShape(line[0]);
        return new Round() {ElfShape = elfShape, YourShape = GetYourShape(elfShape, line[2])};
    }
    
    private Shape GetShape(char c)
    {
        return c switch
        {
            'A' => Shape.Rock,
            'B' => Shape.Paper,
            'C' => Shape.Scissors,
            'X' => Shape.Rock,
            'Y' => Shape.Paper,
            'Z' => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }

    private Shape GetYourShape(Shape elfShape, char desiredOutcome)
    {
        return desiredOutcome switch
        {
            'X' => GetLosingShape(elfShape),
            'Y' => elfShape,
            'Z' => GetWinningShape(elfShape),
            _ => throw new ArgumentOutOfRangeException(nameof(desiredOutcome), desiredOutcome, null)
        };
    }

    private Shape GetWinningShape(Shape opponentShape)
    {
        return opponentShape switch
        {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(opponentShape), opponentShape, null)
        };
    }
    
    private Shape GetLosingShape(Shape opponentShape)
    {
        return opponentShape switch
        {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(opponentShape), opponentShape, null)
        };
    }

    private int GetScore(Round round)
    {
        return (int)round.YourShape + (DidYouWin(round) ? 6 : 0) + (DidYouTie(round) ? 3 : 0);
    }

    private bool DidYouWin(Round round)
    {
        return round.YourShape == Shape.Rock && round.ElfShape == Shape.Scissors ||
               round.YourShape == Shape.Paper && round.ElfShape == Shape.Rock ||
               round.YourShape == Shape.Scissors && round.ElfShape == Shape.Paper;
    }

    private bool DidYouTie(Round round)
    {
        return round.YourShape == round.ElfShape;
    }
}
