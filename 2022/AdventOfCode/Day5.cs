namespace AdventOfCode;

public class Day5
{
    private const string InputPath = "input/2022-12-05.txt";

    private Dictionary<int, Stack<char>> _stacks = new();
    
    public string Puzzle1()
    {
        var lines = File.ReadAllLines(InputPath);
        var initialInput = lines.Where(l => !l.StartsWith("move") && !l.IsNullOrEmpty());
        var instructions = lines.Where(l => l.StartsWith("move"));

        InitStacks(initialInput.ToList());
        foreach (var instruction in instructions)
        {
            ApplyInstruction(instruction);
        }

        var result = string.Empty;
        for (int i = 1; i <= _stacks.Count(); i++)
        {
            result += _stacks[i].Pop();
        }

        return result;
    }


    private void InitStacks(List<string> initialInput)
    {
        var numStacks = initialInput.Last()
                                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(int.Parse)
                                    .Max();

        foreach (int i in Enumerable.Range(1, numStacks))
        {
            _stacks[i] = new Stack<char>();
        }

        initialInput = initialInput.SkipLast(1).ToList();
        initialInput.Reverse();
        foreach (string line in initialInput)
        {
            foreach (int stackNumber in Enumerable.Range(1, numStacks))
            {
                int stackIndex = GetStackInputIndex(stackNumber);
                char crate = line[stackIndex];
                if (crate != ' ')
                {
                    _stacks[stackNumber].Push(crate);
                }
            }
        }
    }
    
    private int GetStackInputIndex(int stackNumber)
    {
        return 1 + (stackNumber - 1) * 4;
    }

    private void ApplyInstruction(string instruction)
    {
        var instructionTokens = instruction.Split();
        var numToMove = int.Parse(instructionTokens[1]);
        var fromStack = int.Parse(instructionTokens[3]);
        var toStack = int.Parse(instructionTokens[5]);
        
        for (int i = 0; i < numToMove; i++)
        {
            _stacks[toStack].Push(_stacks[fromStack].Pop());
        }
    }
    
    
}
