namespace AdventOfCode;

public class Day2
{
    private const string InputPath = "input/2023-12-02.txt";
    
    public int Puzzle1()
    {
        return File.ReadAllLines(InputPath)
            .Where(IsGamePossible)
            .Select(GetGameId)
            .Sum();
    }
    
    public int Puzzle2()
    {
        return File.ReadAllLines(InputPath)
            .Select(GetGamePower)
            .Sum();
    }

    private bool IsGamePossible(string game)
    {
        var revealedCubes = new RevealedCubes(game);
        return revealedCubes.GetMaximumSeenCount(CubeColor.Red) <= 12 &&
               revealedCubes.GetMaximumSeenCount(CubeColor.Green) <= 13 &&
               revealedCubes.GetMaximumSeenCount(CubeColor.Blue) <= 14;
    }

    private int GetGameId(string game)
    {
        return Int32.Parse(game.Split(':')[0].Replace("Game ", string.Empty));
    }

    private int GetGamePower(string game)
    {
        var revealedCubes = new RevealedCubes(game);
        return revealedCubes.GetMaximumSeenCount(CubeColor.Red) *
               revealedCubes.GetMaximumSeenCount(CubeColor.Green) *
               revealedCubes.GetMaximumSeenCount(CubeColor.Blue);
    }
    
    private enum CubeColor
    {
        Red,
        Green,
        Blue
    }
    
    private class RevealedCubes
    {
        private Dictionary<CubeColor, int> _maxCubesSeenByColor = new();
        
        public RevealedCubes(string game)
        {
            var cubeColorCounts = game.Split(':')[1].Split(new [] {',', ';'}, StringSplitOptions.TrimEntries);
            foreach (var cubeColorCount in cubeColorCounts)
            {
                var tokens = cubeColorCount.Split(' ');
                var count = Int32.Parse(tokens[0]);
                var color = Enum.Parse<CubeColor>(tokens[1], true);
                
                _maxCubesSeenByColor[color] = int.Max(count, _maxCubesSeenByColor.GetValueOrDefault(color));
            }
        }
        
        public int GetMaximumSeenCount(CubeColor color)
        {
            return _maxCubesSeenByColor.GetValueOrDefault(color);
        }
    }
}