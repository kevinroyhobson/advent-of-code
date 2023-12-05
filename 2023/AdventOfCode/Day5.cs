namespace AdventOfCode;

public class Day5
{
    private const string InputPath = "input/2023-12-05.txt";

    private Dictionary<string, AlmanacMap> _mapByConversionType = new();
    
    public long Puzzle1()
    {
        var inputLines = File.ReadAllLines(InputPath);

        List<long> inputSeeds = inputLines[0].Replace("seeds: ", string.Empty)
                                             .Split()
                                             .Select(long.Parse)
                                             .ToList();

        var currentRanges = new List<string>();
        var currentMapName = string.Empty;
        foreach (var line in inputLines)
        {
            if (line.EndsWith("map:"))
            {
                currentMapName = line.Split()[0];
                currentRanges.Clear();
            }
            else if (line == string.Empty && currentMapName != string.Empty)
            {
                _mapByConversionType[currentMapName] = new AlmanacMap(currentRanges);
            }
            else
            {
                currentRanges.Add(line);
            }
        }

        long minLocation = long.MaxValue;
        foreach (var seed in inputSeeds)
        {
            minLocation = long.Min(minLocation, GetLocationForSeedType(seed));
        }

        return minLocation;
    }

    private long GetLocationForSeedType(long seedType)
    {
        long soilType = _mapByConversionType["seed-to-soil"].GetDestinationValue(seedType);
        long fertilizerType = _mapByConversionType["soil-to-fertilizer"].GetDestinationValue(soilType);
        long waterType = _mapByConversionType["fertilizer-to-water"].GetDestinationValue(fertilizerType);
        long lightType = _mapByConversionType["water-to-light"].GetDestinationValue(waterType);
        long temperatureType = _mapByConversionType["light-to-temperature"].GetDestinationValue(lightType);
        long humidityType = _mapByConversionType["temperature-to-humidity"].GetDestinationValue(temperatureType);
        long locationType = _mapByConversionType["humidity-to-location"].GetDestinationValue(humidityType);

        return locationType;
    }

    private class AlmanacMap
    {
        private readonly List<AlmanacRange> _ranges;

        public AlmanacMap(List<string> rangeStrings)
        {
            _ranges = rangeStrings.Select(r => new AlmanacRange(r)).ToList();
        }

        public long GetDestinationValue(long sourceValue)
        {
            foreach (var range in _ranges)
            {
                if (sourceValue >= range.SourceStart && sourceValue < range.SourceEndExclusive)
                {
                    long offset = sourceValue - range.SourceStart;
                    return range.DestinationStart + offset;
                }
            }

            return sourceValue;
        }
    }

    private class AlmanacRange
    {
        public AlmanacRange(string rangeLine)
        {
            var tokens = rangeLine.Split();
            DestinationStart = long.Parse(tokens[0]);
            SourceStart = long.Parse(tokens[1]);
            Length = long.Parse(tokens[2]);
        }
        
        public long SourceStart { get; set; }
        public long DestinationStart { get; set; }
        private long Length { get; set; }
        public long SourceEndExclusive => SourceStart + Length;
    }
    
}
