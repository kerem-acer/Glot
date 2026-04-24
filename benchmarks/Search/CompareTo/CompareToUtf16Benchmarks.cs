using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class CompareToUtf16Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet A, Diff;

    [GlobalSetup]
    public void Setup()
    {
        A = EncodedSet.From(TestData.Generate(N, Locale));
        Diff = EncodedSet.From(TestData.Mutate(A.Str));
    }

    [Benchmark(Baseline = true, Description = "string.Compare")]
    public int StringCompare() => string.Compare(A.Str, Diff.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceCompareTo UTF-8")]
    public int SpanSequenceCompareTo() => A.RawBytes.AsSpan().SequenceCompareTo(Diff.RawBytes);

    [Benchmark(Description = "Text.CompareTo UTF-8")]
    public int TextCompareTo_Utf8() => A.Utf16.CompareTo(Diff.Utf8);

    [Benchmark(Description = "Text.CompareTo UTF-16")]
    public int TextCompareTo_Utf16() => A.Utf16.CompareTo(Diff.Utf16);

    [Benchmark(Description = "Text.CompareTo UTF-32")]
    public int TextCompareTo_Utf32() => A.Utf16.CompareTo(Diff.Utf32);
}
