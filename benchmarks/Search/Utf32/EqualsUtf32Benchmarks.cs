using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EqualsUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _a, _b, _diff;

    [GlobalSetup]
    public void Setup()
    {
        _a = EncodedSet.From(TestData.Generate(N, Locale));
        _b = EncodedSet.From(TestData.Generate(N, Locale));
        _diff = EncodedSet.From(TestData.Mutate(_a.Str));
    }

    [Benchmark(Baseline = true, Description = "string.Equals")]
    public bool StringEquals() => _a.Str.Equals(_b.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceEqual UTF-8")]
    public bool SpanSequenceEqual() => _a.RawBytes.AsSpan().SequenceEqual(_b.RawBytes);

    [Benchmark(Description = "Text.Equals UTF-8")]
    public bool TextEquals_Utf8() => _a.Utf8.Equals(_b.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16")]
    public bool TextEquals_Utf16() => _a.Utf8.Equals(_b.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32")]
    public bool TextEquals_Utf32() => _a.Utf8.Equals(_b.Utf32);

    [Benchmark(Description = "string.Equals different")]
    public bool StringEquals_Diff() => _a.Str.Equals(_diff.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.SequenceEqual UTF-8 different")]
    public bool SpanSequenceEqual_Diff() => _a.RawBytes.AsSpan().SequenceEqual(_diff.RawBytes);

    [Benchmark(Description = "Text.Equals UTF-8 different")]
    public bool TextEquals_Utf8_Diff() => _a.Utf8.Equals(_diff.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16 different")]
    public bool TextEquals_Utf16_Diff() => _a.Utf8.Equals(_diff.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32 different")]
    public bool TextEquals_Utf32_Diff() => _a.Utf8.Equals(_diff.Utf32);
}
