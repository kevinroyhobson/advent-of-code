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

    private bool IsGamePossible(string game)
    {
        var revealedCubeSets = game.Split(':')[1].Split(';');
        foreach (var revealedCubeSet in revealedCubeSets)
        {
            var revealedCubes = new RevealedCubes(revealedCubeSet);
            if (revealedCubes.GetCubeCount(CubeColor.Red) > 12 ||
                revealedCubes.GetCubeCount(CubeColor.Green) > 13 ||
                revealedCubes.GetCubeCount(CubeColor.Blue) > 14)
            {
                return false;
            }
        }

        return true;
    }

    private int GetGameId(string game)
    {
        return Int32.Parse(game.Split(':')[0].Replace("Game ", string.Empty));
    }
    
    private enum CubeColor
    {
        Red,
        Green,
        Blue
    }
    
    private class RevealedCubes
    {
        private Dictionary<CubeColor, int> _cubeCountByColor = new();
        
        public RevealedCubes(string revealed)
        {
            var cubeColorCounts = revealed.Split(',', StringSplitOptions.TrimEntries);
            foreach (var cubeColorCount in cubeColorCounts)
            {
                var tokens = cubeColorCount.Split(' ');
                var count = Int32.Parse(tokens[0]);
                var color = Enum.Parse<CubeColor>(tokens[1], true);
                _cubeCountByColor[color] = count;
            }
        }
        
        public int GetCubeCount(CubeColor color)
        {
            return _cubeCountByColor.GetValueOrDefault(color);
        }
    }
}