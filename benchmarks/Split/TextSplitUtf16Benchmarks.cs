using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextSplitUtf16Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _csv;

    [GlobalSetup]
    public void Setup()
    {
        _csv = EncodedSet.From(TestData.GenerateCsv(N, Locale));
    }

    // --- Split ---

    [BenchmarkCategory("Split"), Benchmark(Baseline = true, Description = "string.Split count")]
    public int StringSplit() => _csv.Str.Split(", ").Length;

    [BenchmarkCategory("Split"), Benchmark(Description = "Text.Split UTF-16 count")]
    public int TextSplit_Utf16()
    {
        var count = 0;
        foreach (var _ in _csv.Utf16.Split(", "))
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
        foreach (var _ in _csv.Str.EnumerateRunes())
        {
            count++;
        }

        return count;
    }

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "Text.EnumerateRunes UTF-16")]
    public int TextEnumerateRunes_Utf16()
    {
        var count = 0;
        foreach (var _ in _csv.Utf16.EnumerateRunes())
        {
            count++;
        }

        return count;
    }
}
