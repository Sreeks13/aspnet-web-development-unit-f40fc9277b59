namespace ContosoWeb.Models;

public class SleepRecord
{
    public string Date { get; set; } = "";
    public double Hours { get; set; }
    public double Quality { get; set; }
    public double Score => Hours * Quality;
}

public class SleepAnalysis
{
    public List<SleepRecord> Records { get; set; } = new();

    public SleepRecord? BestNight =>
        Records.Count == 0 ? null : Records.MaxBy(x => x.Score);

    public SleepRecord? WorstNight =>
        Records.Count == 0 ? null : Records.MinBy(x => x.Score);

    public double AverageScore =>
        Records.Count == 0 ? 0 : Records.Average(x => x.Score);
}

public class SleepAnalyser
{
    public SleepAnalysis Analyse(string filePath)
    {
        var result = new SleepAnalysis();

        if (!File.Exists(filePath))
            return result;

        foreach (var line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(
                new[] { '|', ',', ';' },
                StringSplitOptions.TrimEntries);

            if (parts.Length < 3)
                continue;

            if (!double.TryParse(
                    parts[1],
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var hours))
                continue;

            if (!double.TryParse(
                    parts[2],
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var quality))
                continue;

            result.Records.Add(new SleepRecord
            {
                Date = parts[0],
                Hours = hours,
                Quality = quality
            });
        }

        return result;
    }
}
