using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class CompareToUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _a, _diff;

    [GlobalSetup]
    public void Setup()
    {
        _a = EncodedSet.From(TestData.Generate(N, Locale));
        _diff = EncodedSet.From(TestData.Mutate(_a.Str));
    }

    [Benchmark(Baseline = true, Description = "string.Compare")]
    public int StringCompare() => string.Compare(_a.Str, _diff.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceCompareTo UTF-8")]
    public int SpanSequenceCompareTo() => _a.RawBytes.AsSpan().SequenceCompareTo(_diff.RawBytes);

    [Benchmark(Description = "Text.CompareTo UTF-8")]
    public int TextCompareTo_Utf8() => _a.Utf8.CompareTo(_diff.Utf8);

    [Benchmark(Description = "Text.CompareTo UTF-16")]
    public int TextCompareTo_Utf16() => _a.Utf8.CompareTo(_diff.Utf16);

    [Benchmark(Description = "Text.CompareTo UTF-32")]
    public int TextCompareTo_Utf32() => _a.Utf8.CompareTo(_diff.Utf32);
}
