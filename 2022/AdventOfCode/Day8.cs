namespace AdventOfCode;

public class Day8
{
    private const string InputPath = "input/2022-12-08.txt";
    private Tree[][] _trees;
    
    
    public int Puzzle1()
    {
        _trees = File.ReadAllLines(InputPath)
                            .Select(line => line.Select(c => new Tree(int.Parse(c.ToString()))).ToArray())
                            .ToArray();
        
        for (int x = 1; x < _trees.Length - 1; x++)
        {
            for (int y = 1; y < _trees[x].Length - 1; y++)
            {
                var leftTree = _trees[x][y-1];
                _trees[x][y].MaxHeightByDirection[Direction.Left] =
                    Math.Max(leftTree.MaxHeightByDirection[Direction.Left], leftTree.Height);

                var upTree = _trees[x-1][y];
                _trees[x][y].MaxHeightByDirection[Direction.Up] =
                    Math.Max(upTree.MaxHeightByDirection[Direction.Up], upTree.Height);
            }
        }
        
        for (int x = _trees.Length - 2; x > 0; x--)
        {
            for (int y = _trees[x].Length - 2; y > 0; y--)
            {
                var rightTree = _trees[x][y+1];
                _trees[x][y].MaxHeightByDirection[Direction.Right] =
                    Math.Max(rightTree.MaxHeightByDirection[Direction.Right], rightTree.Height);

                var downTree = _trees[x+1][y];
                _trees[x][y].MaxHeightByDirection[Direction.Down] =
                    Math.Max(downTree.MaxHeightByDirection[Direction.Down], downTree.Height);
            }
        }
        
        return _trees.SelectMany(row => row.Select(tree => tree))
                        .Count(tree => tree.IsVisible);
    }

    public int Puzzle2()
    {
        _trees = File.ReadAllLines(InputPath)
                     .Select(line => line.Select(c => new Tree(int.Parse(c.ToString()))).ToArray())
                     .ToArray();

        int maxScenicScore = 0;
        for (int x = 0; x < _trees.Length; x++)
        {
            for (int y = 0; y < _trees[x].Length; y++)
            {
                int viewableTreesUp = 0;
                for (int i = x - 1; i >= 0; i--)
                {
                    viewableTreesUp++;
                    if (_trees[i][y].Height >= _trees[x][y].Height)
                    {
                        break;
                    }
                }

                int viewableTreesDown = 0;
                for (int i = x + 1; i < _trees.Length; i++)
                {
                    viewableTreesDown++;
                    if (_trees[i][y].Height >= _trees[x][y].Height)
                    {
                        break;
                    }
                }
                
                int viewableTreesLeft = 0;
                for (int j = y - 1; j >= 0; j--)
                {
                    viewableTreesLeft++;
                    if (_trees[x][j].Height >= _trees[x][y].Height)
                    {
                        break;
                    }
                }
                
                int viewableTreesRight = 0;
                for (int j = y + 1; j < _trees[x].Length; j++)
                {
                    viewableTreesRight++;
                    if (_trees[x][j].Height >= _trees[x][y].Height)
                    {
                        break;
                    }
                }

                int thisScenicScore = viewableTreesUp * viewableTreesDown * viewableTreesLeft * viewableTreesRight;
                maxScenicScore = Math.Max(maxScenicScore, thisScenicScore);
            }
        }

        return maxScenicScore;
    }

    private void PrintTrees()
    {
        for (int x = 0; x < _trees.Length; x++)
        {
            for (int y = 0; y < _trees[x].Length; y++)
            {
                // Console.Write(treeArray[x][y].Height;
                // Console.Write(treeArray[x][y].MaxHeightByDirection[Direction.Left]);
                Console.Write(_trees[x][y].IsVisible ? "X" : "O");
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
