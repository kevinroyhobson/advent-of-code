using System;
using System.IO;

namespace AdventOfCode
{
    public class Day2
    {
        private const string InputPath = "input/2021-12-02.txt";

        public int Puzzle1()
        {
            int horizontalPosition = 0;
            int depth = 0;

            var commands = File.ReadLines(InputPath);

            foreach (var command in commands)
            {
                var commandComponents = command.Split(' ');
                string commandType = commandComponents[0];
                int amount = int.Parse(commandComponents[1]);

                switch (commandType)
                {
                    case "forward":
                        horizontalPosition += amount;
                        break;
                    
                    case "down":
                        depth += amount;
                        break;
                        
                    case "up":
                        depth -= amount;
                        break;
                    
                    default:
                        throw new ArgumentException("Unknown command type.");
                }
            }

            return horizontalPosition * depth;
        }
        
        public int Puzzle2()
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            var commands = File.ReadLines(InputPath);

            foreach (var command in commands)
            {
                var commandComponents = command.Split(' ');
                string commandType = commandComponents[0];
                int amount = int.Parse(commandComponents[1]);

                switch (commandType)
                {
                    case "forward":
                        horizontalPosition += amount;
                        depth += (aim * amount);
                        break;
                    
                    case "down":
                        aim += amount;
                        break;
                        
                    case "up":
                        aim -= amount;
                        break;
                    
                    default:
                        throw new ArgumentException("Unknown command type.");
                }
            }

            return horizontalPosition * depth;
        }
    }
}