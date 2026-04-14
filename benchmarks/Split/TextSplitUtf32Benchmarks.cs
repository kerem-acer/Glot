using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextSplitUtf32Benchmarks
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

    [BenchmarkCategory("Split"), Benchmark(Description = "Text.Split UTF-32 count")]
    public int TextSplit_Utf32()
    {
        var count = 0;
        foreach (var _ in _csv.Utf32.Split(", "))
        {
            count++;
        }

        return count;
    }

    // --- EnumerateRunes ---

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "Text.EnumerateRunes UTF-32")]
    public int TextEnumerateRunes_Utf32()
    {
        var count = 0;
        foreach (var _ in _csv.Utf32.EnumerateRunes())
        {
            count++;
        }

        return count;
    }
}
