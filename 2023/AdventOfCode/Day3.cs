namespace AdventOfCode;

public class Day3
{
    private const string InputPath = "input/2023-12-03.txt";
    
    private char [] [] _schematic = File.ReadAllLines(InputPath)
        .Select(line => line.ToCharArray())
        .ToArray();

    private HashSet<Coordinate> _visitedCoordinates = new();
    
    public int Puzzle1()
    {
        int partNumberSum = 0;
        
         for (int y = 0; y < _schematic.Length; y++)
         {
             for (int x = 0; x < _schematic[y].Length; x++)
             {
                 var coordinate = new Coordinate(x, y);
                 if (Char.IsDigit(GetCharAt(coordinate)) && !_visitedCoordinates.Contains(coordinate))
                 {
                     partNumberSum += ProcessPartNumber(coordinate);
                 }

                 _visitedCoordinates.Add(coordinate);
             }
         }

         return partNumberSum;
    }
    
    private int ProcessPartNumber(Coordinate coordinate)
    {
        var adjacentCoordinates = new List<Coordinate>()
        {
            new(coordinate.X - 1, coordinate.Y - 1),
            new(coordinate.X - 1, coordinate.Y),
            new(coordinate.X - 1, coordinate.Y + 1),
        };

        var number = 0;
        while (IsValidCoordinate(coordinate))
        {
            _visitedCoordinates.Add(coordinate);
            adjacentCoordinates.Add(new Coordinate(coordinate.X, coordinate.Y - 1));
            adjacentCoordinates.Add(new Coordinate(coordinate.X, coordinate.Y + 1));
            if (Char.IsDigit(GetCharAt(coordinate)))
            {
                number = number * 10  + GetCharAt(coordinate) - '0';
                coordinate = new Coordinate(coordinate.X + 1, coordinate.Y);
            }
            else
            {
                break;
            }
        }
        
        adjacentCoordinates.Add(new Coordinate(coordinate.X, coordinate.Y - 1));
        adjacentCoordinates.Add(new Coordinate(coordinate.X, coordinate.Y));
        adjacentCoordinates.Add(new Coordinate(coordinate.X, coordinate.Y + 1));
        
        return adjacentCoordinates.Any(IsSymbol) ? number : 0;
    }
    
    private bool IsValidCoordinate(Coordinate coordinate)
    {
        return coordinate.Y >= 0 && coordinate.Y < _schematic.Length &&
               coordinate.X >= 0 && coordinate.X < _schematic[0].Length;
    }
    
    private char GetCharAt(Coordinate coordinate)
    {
        return _schematic[coordinate.Y][coordinate.X];
    }

    private bool IsSymbol(Coordinate coordinate)
    {
        return IsValidCoordinate(coordinate) &&
               !Char.IsDigit(GetCharAt(coordinate)) &&
               GetCharAt(coordinate) != '.';
    }

    private class Coordinate(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        
        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                   X == coordinate.X &&
                   Y == coordinate.Y;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
