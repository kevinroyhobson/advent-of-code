namespace AdventOfCode;

public class Day8
{
    private const string InputPath = "input/2022-12-08.txt";

    public int Puzzle1()
    {
        var treeArray = File.ReadAllLines(InputPath)
                            .Select(line => line.Select(c => new Tree(int.Parse(c.ToString()))).ToArray())
                            .ToArray();
        
        for (int x = 1; x < treeArray.Length - 1; x++)
        {
            for (int y = 1; y < treeArray[x].Length - 1; y++)
            {
                var leftTree = treeArray[x][y-1];
                treeArray[x][y].MaxHeightByDirection[Direction.Left] =
                    Math.Max(leftTree.MaxHeightByDirection[Direction.Left], leftTree.Height);

                var upTree = treeArray[x-1][y];
                treeArray[x][y].MaxHeightByDirection[Direction.Up] =
                    Math.Max(upTree.MaxHeightByDirection[Direction.Up], upTree.Height);
            }
        }
        
        for (int x = treeArray.Length - 2; x > 0; x--)
        {
            for (int y = treeArray[x].Length - 2; y > 0; y--)
            {
                var rightTree = treeArray[x][y+1];
                treeArray[x][y].MaxHeightByDirection[Direction.Right] =
                    Math.Max(rightTree.MaxHeightByDirection[Direction.Right], rightTree.Height);

                var downTree = treeArray[x+1][y];
                treeArray[x][y].MaxHeightByDirection[Direction.Down] =
                    Math.Max(downTree.MaxHeightByDirection[Direction.Down], downTree.Height);
            }
        }
        
        PrintTrees(treeArray);
        return treeArray.SelectMany(row => row.Select(tree => tree))
                        .Count(tree => tree.IsVisible);
    }

    private void PrintTrees(Tree[][] treeArray)
    {
        for (int x = 0; x < treeArray.Length; x++)
        {
            for (int y = 0; y < treeArray[x].Length; y++)
            {
                // Console.Write(treeArray[x][y].Height;
                // Console.Write(treeArray[x][y].MaxHeightByDirection[Direction.Left]);
                Console.Write(treeArray[x][y].IsVisible ? "X" : "O");
            }
            Console.WriteLine();
        }
    }
    
    private class Tree
    {
        public Tree(int height)
        {
            Height = height;
        }
        
        public int Height { get; }
        public Dictionary<Direction, int> MaxHeightByDirection = new()
        {
            {Direction.Left, -1}, 
            {Direction.Right, -1},
            {Direction.Up, -1},
            {Direction.Down, -1}
        };
        
        public bool IsVisible => MaxHeightByDirection.Values.Any(h => h < 0) || 
                                 MaxHeightByDirection.Values.Any(h => h < Height);
    }
    
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
