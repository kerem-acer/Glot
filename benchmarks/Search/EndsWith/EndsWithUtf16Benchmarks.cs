using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EndsWithUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.EndsWith")]
    public bool StringEndsWith() => Haystack.Str.EndsWith(Needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.EndsWith UTF-8")]
    public bool SpanEndsWith() => Haystack.RawBytes.AsSpan().EndsWith(Needle.RawBytes);

    [Benchmark(Description = "Text.EndsWith UTF-8")]
    public bool TextEndsWith_Utf8() => Haystack.Utf8.EndsWith(Needle.Utf8);

    [Benchmark(Description = "Text.EndsWith UTF-16")]
    public bool TextEndsWith_Utf16() => Haystack.Utf8.EndsWith(Needle.Utf16);

    [Benchmark(Description = "Text.EndsWith UTF-32")]
    public bool TextEndsWith_Utf32() => Haystack.Utf8.EndsWith(Needle.Utf32);

    [Benchmark(Description = "string.EndsWith miss")]
    public bool StringEndsWith_Miss() => Haystack.Str.EndsWith(MissingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.EndsWith UTF-8 miss")]
    public bool SpanEndsWith_Miss() => Haystack.RawBytes.AsSpan().EndsWith(MissingNeedle.RawBytes);

    [Benchmark(Description = "Text.EndsWith UTF-8 miss")]
    public bool TextEndsWith_Utf8_Miss() => Haystack.Utf8.EndsWith(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.EndsWith UTF-16 miss")]
    public bool TextEndsWith_Utf16_Miss() => Haystack.Utf8.EndsWith(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.EndsWith UTF-32 miss")]
    public bool TextEndsWith_Utf32_Miss() => Haystack.Utf8.EndsWith(MissingNeedle.Utf32);
}
