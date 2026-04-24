using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class StartsWithUtf32Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.StartsWith")]
    public bool StringStartsWith() => _haystack.Str.StartsWith(_needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.StartsWith UTF-8")]
    public bool SpanStartsWith() => _haystack.RawBytes.AsSpan().StartsWith(_needle.RawBytes);

    [Benchmark(Description = "Text.StartsWith UTF-8")]
    public bool TextStartsWith_Utf8() => _haystack.Utf32.StartsWith(_needle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16")]
    public bool TextStartsWith_Utf16() => _haystack.Utf32.StartsWith(_needle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32")]
    public bool TextStartsWith_Utf32() => _haystack.Utf32.StartsWith(_needle.Utf32);

    [Benchmark(Description = "string.StartsWith miss")]
    public bool StringStartsWith_Miss() => _haystack.Str.StartsWith(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.StartsWith UTF-8 miss")]
    public bool SpanStartsWith_Miss() => _haystack.RawBytes.AsSpan().StartsWith(_missingNeedle.RawBytes);

    [Benchmark(Description = "Text.StartsWith UTF-8 miss")]
    public bool TextStartsWith_Utf8_Miss() => _haystack.Utf32.StartsWith(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16 miss")]
    public bool TextStartsWith_Utf16_Miss() => _haystack.Utf32.StartsWith(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32 miss")]
    public bool TextStartsWith_Utf32_Miss() => _haystack.Utf32.StartsWith(_missingNeedle.Utf32);
}
