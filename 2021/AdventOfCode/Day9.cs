using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        private const string InputPath = "input/2021-12-09.txt";

        public int Puzzle1()
        {
            var heightMap = GetHeightMap();

            int riskLevelSum = 0;
            for (int x = 1; x < heightMap.GetUpperBound(0); x++)
            {
                for (int y = 1; y < heightMap.GetUpperBound(1); y++)
                {
                    int thisHeight = heightMap[x, y];
                    if (heightMap[x - 1, y] > thisHeight &&
                        heightMap[x + 1, y] > thisHeight &&
                        heightMap[x, y - 1] > thisHeight &&
                        heightMap[x, y + 1] > thisHeight)
                    {
                        riskLevelSum += (thisHeight + 1);
                    }
                }
            }

            return riskLevelSum;
        }

        private int[,] GetHeightMap()
        {
            var input = File.ReadLines(InputPath)
                            .ToArray();

            var heightMap = new int[input[0].Length + 2, input.Length + 2];
            for (int y = -1; y <= input.Length; y++)
            {
                for (int x = -1; x <= input[0].Length; x++)
                {
                    if (y == -1 || x == -1 || y == input.Length || x == input[0].Length)
                    {
                        heightMap[x + 1, y + 1] = int.MaxValue;
                    }
                    else
                    {
                        heightMap[x + 1, y + 1] = int.Parse(input[x].Substring(y, 1));
                    }
                }
            }

            return heightMap;
        }
    }
}
