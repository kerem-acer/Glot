using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextSplitUtf8Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _csv;
    EncodedSet _separator;

    [GlobalSetup]
    public void Setup()
    {
        _csv = EncodedSet.From(TestData.GenerateCsv(N, Locale));
        _separator = EncodedSet.From(", ");
    }

    // --- Split ---

    [BenchmarkCategory("Split"), Benchmark(Baseline = true, Description = "Span.IndexOf UTF-8")]
    public int SpanIndexOf()
    {
        var haystack = _csv.RawBytes.AsSpan();
        var needle = _separator.RawBytes.AsSpan();
        var count = 1;
        int idx;
        while ((idx = haystack.IndexOf(needle)) >= 0)
        {
            count++;
            haystack = haystack[(idx + needle.Length)..];
        }

        return count;
    }

    [BenchmarkCategory("Split"), Benchmark(Description = "U8String.Split count")]
    public int U8Split()
    {
        var count = 0;
        foreach (var _ in _csv.U8.Split(", "u8))
        {
            count++;
        }

        return count;
    }

    [BenchmarkCategory("Split"), Benchmark(Description = "Text.Split UTF-8 count")]
    public int TextSplit_Utf8()
    {
        var count = 0;
        foreach (var _ in _csv.Utf8.Split(", "))
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

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "Span.DecodeFromUtf8 count")]
    public int SpanDecodeFromUtf8()
    {
        var span = _csv.RawBytes.AsSpan();
        var count = 0;
        while (!span.IsEmpty)
        {
            Rune.DecodeFromUtf8(span, out _, out var consumed);
            span = span[consumed..];
            count++;
        }

        return count;
    }

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "U8String.Runes")]
    public int U8Runes()
    {
        var count = 0;
        foreach (var _ in _csv.U8.Runes)
        {
            count++;
        }

        return count;
    }

    [BenchmarkCategory("EnumerateRunes"), Benchmark(Description = "Text.EnumerateRunes UTF-8")]
    public int TextEnumerateRunes_Utf8()
    {
        var count = 0;
        foreach (var _ in _csv.Utf8.EnumerateRunes())
        {
            count++;
        }

        return count;
    }
}
