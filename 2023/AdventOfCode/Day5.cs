namespace AdventOfCode;

public class Day5
{
    private const string InputPath = "input/2023-12-05.txt";
    private List<string> _inputLines = File.ReadAllLines(InputPath).ToList();

    private Dictionary<string, AlmanacMap> _mapByConversionType = new();
    
    public long Puzzle1()
    {
        List<long> inputSeeds = _inputLines[0].Replace("seeds: ", string.Empty)
                                              .Split()
                                              .Select(long.Parse)
                                              .ToList();
        LoadAlmanac();

        long minLocation = long.MaxValue;
        foreach (var seed in inputSeeds)
        {
            minLocation = long.Min(minLocation, GetLocationForSeed(seed));
        }

        return minLocation;
    }
    
    public long Puzzle2()
    {
        LoadAlmanac();

        var seedRanges = new List<Tuple<long, long>>();
        var seedInputData = _inputLines[0].Replace("seeds: ", string.Empty)
                                          .Split()
                                          .Select(long.Parse)
                                          .ToList();
        for (int i = 0; i < seedInputData.Count; i += 2)
        {
            seedRanges.Add(Tuple.Create(seedInputData[i], seedInputData[i] + seedInputData[i+1]));
        }
        
        for (long location = 0; location <= long.MaxValue; location++)
        {
            long seed = GetSeedForLocation(location);
            if (seedRanges.Any(range => seed >= range.Item1 && seed < range.Item2))
            {
                return location;
            }
            
            if (location % 1000 == 0)
            {
                Console.WriteLine($"location={location}, seed={seed}");
            }
        }

        throw new Exception("No seed found.");
    }

    private void LoadAlmanac()
    {
        var currentRanges = new List<string>();
        var currentMapName = string.Empty;
        foreach (var line in _inputLines)
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
    }

    private long GetLocationForSeed(long seed)
    {
        long soil = _mapByConversionType["seed-to-soil"].GetDestinationValue(seed);
        long fertilizer = _mapByConversionType["soil-to-fertilizer"].GetDestinationValue(soil);
        long water = _mapByConversionType["fertilizer-to-water"].GetDestinationValue(fertilizer);
        long light = _mapByConversionType["water-to-light"].GetDestinationValue(water);
        long temperature = _mapByConversionType["light-to-temperature"].GetDestinationValue(light);
        long humidity = _mapByConversionType["temperature-to-humidity"].GetDestinationValue(temperature);
        long location = _mapByConversionType["humidity-to-location"].GetDestinationValue(humidity);

        return location;
    }

    private long GetSeedForLocation(long location)
    {
        long humidity = _mapByConversionType["humidity-to-location"].GetSourceValue(location);
        long temperature = _mapByConversionType["temperature-to-humidity"].GetSourceValue(humidity);
        long light = _mapByConversionType["light-to-temperature"].GetSourceValue(temperature);
        long water = _mapByConversionType["water-to-light"].GetSourceValue(light);
        long fertilizer = _mapByConversionType["fertilizer-to-water"].GetSourceValue(water);
        long soil = _mapByConversionType["soil-to-fertilizer"].GetSourceValue(fertilizer);
        long seed = _mapByConversionType["seed-to-soil"].GetSourceValue(soil);

        return seed;
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

        public long GetSourceValue(long destinationValue)
        {
            foreach (var range in _ranges)
            {
                if (destinationValue >= range.DestinationStart && destinationValue < range.DestinationEndExclusive)
                {
                    long offset = destinationValue - range.DestinationStart;
                    return range.SourceStart + offset;
                }
            }

            return destinationValue;
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
        
        public long SourceStart { get; }
        public long DestinationStart { get; }
        private long Length { get; }
        public long SourceEndExclusive => SourceStart + Length;
        public long DestinationEndExclusive => DestinationStart + Length;
    }
}
