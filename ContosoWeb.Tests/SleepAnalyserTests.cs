using ContosoWeb.Models;

namespace ContosoWeb.Tests;

public class SleepAnalyserTests
{
    [Fact]
    public void Analyse_CalculatesScoresAndAverage()
    {
        var file = Path.GetTempFileName();

        File.WriteAllLines(file, new[]
        {
            "Night1|8|8",
            "Night2|6|5",
            "Night3|9|9"
        });

        try
        {
            var analyser = new SleepAnalyser();
            var result = analyser.Analyse(file);

            Assert.Equal(3, result.Records.Count);
            Assert.Equal(64, result.Records[0].Score);
            Assert.Equal(30, result.Records[1].Score);
            Assert.Equal(81, result.Records[2].Score);

            Assert.Equal("Night3", result.BestNight!.Date);
            Assert.Equal("Night2", result.WorstNight!.Date);
            Assert.Equal(175.0 / 3.0, result.AverageScore, 5);
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void Analyse_PreservesInputOrder()
    {
        var file = Path.GetTempFileName();

        File.WriteAllLines(file, new[]
        {
            "A|7|7",
            "B|8|8",
            "C|6|6"
        });

        try
        {
            var result = new SleepAnalyser().Analyse(file);

            Assert.Equal("A", result.Records[0].Date);
            Assert.Equal("B", result.Records[1].Date);
            Assert.Equal("C", result.Records[2].Date);
        }
        finally
        {
            File.Delete(file);
        }
    }
}
