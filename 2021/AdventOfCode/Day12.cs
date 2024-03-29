using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day12
    {
        private const string InputPath = "input/2021-12-12.txt";

        public int ThePuzzle()
        {
            var startCave = GetStartCaveFromInput();
            return GetNumPathsToEndFromCave(new List<Cave>(), startCave);
        }

        private Cave GetStartCaveFromInput()
        {
            var caveByName = new Dictionary<string, Cave>();
            
            foreach (var connection in File.ReadLines(InputPath))
            {
                var caveNames = connection.Split('-');
                foreach (var caveName in caveNames)
                {
                    if (!caveByName.ContainsKey(caveName))
                    {
                        caveByName[caveName] = new Cave(caveName);
                    }
                }
                
                caveByName[caveNames[0]].AddCaveConnection(caveByName[caveNames[1]]);
                caveByName[caveNames[1]].AddCaveConnection(caveByName[caveNames[0]]);
            }

            return caveByName["start"];
        }

        private int GetNumPathsToEndFromCave(List<Cave> pathSoFar, Cave cave)
        {
            if (cave.Name == "end")
            {
                return 1;
            }
            
            var nextPathSoFar = new List<Cave>(pathSoFar) { cave };
            var validNextCaves = cave.ConnectedCaves.Where(connectedCave => IsValidNextCave(nextPathSoFar, connectedCave));
            return validNextCaves.Sum(nextCave => GetNumPathsToEndFromCave(nextPathSoFar, nextCave));
        }

        private bool IsValidNextCave(List<Cave> pathSoFar, Cave nextCave)
        {
            if (nextCave.Name == "start")
            {
                return false;
            }

            if (HasAnySmallCaveBeenVisitedTwice(pathSoFar))
            {
                return !pathSoFar.Any(cave => cave.Name == nextCave.Name && cave.IsSmallCave);
            }

            return true;
        }

        private bool HasAnySmallCaveBeenVisitedTwice(List<Cave> pathSoFar)
        {
            var visitedCaveNames = new HashSet<string>();
            foreach (var cave in pathSoFar)
            {
                if (cave.IsSmallCave && visitedCaveNames.Contains(cave.Name))
                {
                    return true;
                }
                visitedCaveNames.Add(cave.Name);
            }

            return false;
        }

        private class Cave
        {
            public string Name { get; }
            public List<Cave> ConnectedCaves { get; }
            public bool IsSmallCave => Name[0] is >= 'a' and <= 'z';

            public Cave(string name)
            {
                Name = name;
                ConnectedCaves = new List<Cave>();
            }

            public void AddCaveConnection(Cave connectedCave)
            {
                ConnectedCaves.Add(connectedCave);
            }
        }
    }
}
