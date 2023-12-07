﻿namespace AdventOfCode;

public class Day4
{
    private const string InputPath = "input/2023-12-04.txt";
    private List<string> _inputLines = File.ReadAllLines(InputPath).ToList();

    public long Puzzle1()
    {
        return _inputLines.Select(l => new ScratchCard(l))
            .Select(c => c.GetScore())
            .Sum();
    }

    private class ScratchCard
    {
        private HashSet<int> _winningNumbers;
        private IEnumerable<int> _ticketNumbers;
        
        public ScratchCard(string inputLine)
        {
            _winningNumbers = inputLine.Split(':')[1]
                                       .Split('|')[0]
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(Int32.Parse)
                                       .ToHashSet();
            
            _ticketNumbers = inputLine.Split(':')[1]
                                      .Split('|')[1]
                                      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(Int32.Parse);
        }

        public int GetScore()
        {
            var matchingNumbers = _ticketNumbers.Count(_winningNumbers.Contains);
            return matchingNumbers > 0
                ? (int) Math.Pow(2, matchingNumbers - 1)
                : 0;
        }
    }
}
