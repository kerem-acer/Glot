using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EndsWithUtf32Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _haystack, _needle, _missingNeedle;

    [GlobalSetup]
    public void Setup()
    {
        _haystack = EncodedSet.From(TestData.Generate(N, Locale));
        _needle = EncodedSet.From(TestData.Needle(Locale));
        _missingNeedle = EncodedSet.From(TestData.MissingNeedle(Locale));
    }

    [Benchmark(Baseline = true, Description = "string.EndsWith")]
    public bool StringEndsWith() => _haystack.Str.EndsWith(_needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.EndsWith UTF-8")]
    public bool SpanEndsWith() => _haystack.RawBytes.AsSpan().EndsWith(_needle.RawBytes);

    [Benchmark(Description = "Text.EndsWith UTF-8")]
    public bool TextEndsWith_Utf8() => _haystack.Utf8.EndsWith(_needle.Utf8);

    [Benchmark(Description = "Text.EndsWith UTF-16")]
    public bool TextEndsWith_Utf16() => _haystack.Utf8.EndsWith(_needle.Utf16);

    [Benchmark(Description = "Text.EndsWith UTF-32")]
    public bool TextEndsWith_Utf32() => _haystack.Utf8.EndsWith(_needle.Utf32);

    [Benchmark(Description = "string.EndsWith miss")]
    public bool StringEndsWith_Miss() => _haystack.Str.EndsWith(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.EndsWith UTF-8 miss")]
    public bool SpanEndsWith_Miss() => _haystack.RawBytes.AsSpan().EndsWith(_missingNeedle.RawBytes);

    [Benchmark(Description = "Text.EndsWith UTF-8 miss")]
    public bool TextEndsWith_Utf8_Miss() => _haystack.Utf8.EndsWith(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.EndsWith UTF-16 miss")]
    public bool TextEndsWith_Utf16_Miss() => _haystack.Utf8.EndsWith(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.EndsWith UTF-32 miss")]
    public bool TextEndsWith_Utf32_Miss() => _haystack.Utf8.EndsWith(_missingNeedle.Utf32);
}
