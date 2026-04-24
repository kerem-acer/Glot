using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ContainsUtf16Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet Haystack, Needle, MissingNeedle;

    [GlobalSetup]
    public void Setup()
    {
        Haystack = EncodedSet.From(TestData.Generate(N, Locale));
        Needle = EncodedSet.From(TestData.Needle(Locale));
        MissingNeedle = EncodedSet.From(TestData.MissingNeedle(Locale));
    }

    [Benchmark(Baseline = true, Description = "string.Contains")]
    public bool StringContains() => Haystack.Str.Contains(Needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.Contains UTF-8")]
    public bool SpanContains() => Haystack.RawBytes.AsSpan().IndexOf(Needle.RawBytes) >= 0;

    [Benchmark(Description = "Text.Contains UTF-8")]
    public bool TextContains_Utf8() => Haystack.Utf16.Contains(Needle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16")]
    public bool TextContains_Utf16() => Haystack.Utf16.Contains(Needle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32")]
    public bool TextContains_Utf32() => Haystack.Utf16.Contains(Needle.Utf32);

    [Benchmark(Description = "string.Contains miss")]
    public bool StringContains_Miss() => Haystack.Str.Contains(MissingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.Contains UTF-8 miss")]
    public bool SpanContains_Miss() => Haystack.RawBytes.AsSpan().IndexOf(MissingNeedle.RawBytes) >= 0;

    [Benchmark(Description = "Text.Contains UTF-8 miss")]
    public bool TextContains_Utf8_Miss() => Haystack.Utf16.Contains(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16 miss")]
    public bool TextContains_Utf16_Miss() => Haystack.Utf16.Contains(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32 miss")]
    public bool TextContains_Utf32_Miss() => Haystack.Utf16.Contains(MissingNeedle.Utf32);
}
