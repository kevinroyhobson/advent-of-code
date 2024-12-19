namespace AdventOfCode;

public class Day2
{
    private const string _inputPath = "input/2024-12-02.txt";

    public int Puzzle1()
    {
        var reports = File.ReadAllLines(_inputPath)
                          .Select(line => line.Split().Select(int.Parse));

        return reports.Count(IsReportSafe);
    }

    public int Puzzle2()
    {
        var reports = File.ReadAllLines(_inputPath)
                          .Select(line => line.Split().Select(int.Parse));

        return reports.Count(IsReportSafeWithDampener);
    }

    private bool IsReportSafe(IEnumerable<int> report)
    {
        return (IsReportIncreasing(report) || IsReportDecreasing(report)) && GetMaxChange(report) <= 3;
    }

    private bool IsReportIncreasing(IEnumerable<int> report)
    {
        var pairs = report.Zip(report.Skip(1), (current, next) => (current, next));
        return pairs.All(pair => pair.current < pair.next);
    }

    private bool IsReportDecreasing(IEnumerable<int> report)
    {
        var pairs = report.Zip(report.Skip(1), (current, next) => (current, next));
        return pairs.All(pair => pair.current > pair.next);
    }

    private int GetMaxChange(IEnumerable<int> report)
    {
        var pairs = report.Zip(report.Skip(1), (current, next) => (current, next));
        return pairs.Max(pair => Math.Abs(pair.current - pair.next));
    }

    private bool IsReportSafeWithDampener(IEnumerable<int> report)
    {
        if (IsReportSafe(report))
        {
            return true;
        }

        var indexes = Enumerable.Range(0, report.Count());
        return indexes.Any(i => {
            var newReport = new List<int>(report);
            newReport.RemoveAt(i);
            return IsReportSafe(newReport);
        });
    }
}
