using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EqualsUtf16Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet A, B, Diff;

    [GlobalSetup]
    public void Setup()
    {
        A = EncodedSet.From(TestData.Generate(N, Locale));
        B = EncodedSet.From(TestData.Generate(N, Locale));
        Diff = EncodedSet.From(TestData.Mutate(A.Str));
    }

    [Benchmark(Baseline = true, Description = "string.Equals")]
    public bool StringEquals() => string.Equals(A.Str, B.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceEqual UTF-8")]
    public bool SpanSequenceEqual() => A.RawBytes.AsSpan().SequenceEqual(B.RawBytes);

    [Benchmark(Description = "Text.Equals UTF-8")]
    public bool TextEquals_Utf8() => A.Utf16.Equals(B.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16")]
    public bool TextEquals_Utf16() => A.Utf16.Equals(B.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32")]
    public bool TextEquals_Utf32() => A.Utf16.Equals(B.Utf32);

    [Benchmark(Description = "string.Equals different")]
    public bool StringEquals_Diff() => string.Equals(A.Str, Diff.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceEqual UTF-8 different")]
    public bool SpanSequenceEqual_Diff() => A.RawBytes.AsSpan().SequenceEqual(Diff.RawBytes);

    [Benchmark(Description = "Text.Equals UTF-8 different")]
    public bool TextEquals_Utf8_Diff() => A.Utf16.Equals(Diff.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16 different")]
    public bool TextEquals_Utf16_Diff() => A.Utf16.Equals(Diff.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32 different")]
    public bool TextEquals_Utf32_Diff() => A.Utf16.Equals(Diff.Utf32);
}
