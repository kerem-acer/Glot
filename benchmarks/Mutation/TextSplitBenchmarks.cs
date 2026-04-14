using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextSplitBenchmarks
{
    [SearchSizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    string _csv = null!;
    Text _csvUtf8;
    U8String _u8Csv;

    [GlobalSetup]
    public void Setup()
    {
        // Ensure enough data for multiple segments
        _csv = TestData.GenerateCsv(N, Locale);
        _csvUtf8 = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(_csv));
        _u8Csv = new U8String(System.Text.Encoding.UTF8.GetBytes(_csv));
    }

    // --- Split count ---

    [BenchmarkCategory("Split"), Benchmark(Baseline = true, Description = "string.Split count")]
    public int StringSplit_Count() => _csv.Split(", ").Length;

    [BenchmarkCategory("Split"), Benchmark(Description = "Text.Split count")]
    public int TextSplit_Count()
    {
        var count = 0;
        foreach (var _ in _csvUtf8.Split(", "))
        {
            count++;
        }
        return count;
    }

    [BenchmarkCategory("Split"), Benchmark(Description = "U8String.Split count")]
    public int U8Split_Count()
    {
        var count = 0;
        foreach (var _ in _u8Csv.Split(", "u8))
        {
            count++;
        }
        return count;
    }

    // --- EnumerateRunes ---

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Baseline = true, Description = "string.EnumerateRunes")]
    public int StringEnumerateRunes()
    {
        var count = 0;
        foreach (var _ in _csv.EnumerateRunes())
        {
            count++;
        }
        return count;
    }

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "Text.EnumerateRunes")]
    public int TextEnumerateRunes()
    {
        var count = 0;
        foreach (var _ in _csvUtf8.EnumerateRunes())
        {
            count++;
        }
        return count;
    }

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "U8String.Runes")]
    public int U8EnumerateRunes()
    {
        var count = 0;
        foreach (var _ in _u8Csv.Runes)
        {
            count++;
        }
        return count;
    }
}
