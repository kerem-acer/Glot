using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ContainsUtf32Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.Contains")]
    public bool StringContains() => _haystack.Str.Contains(_needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.Contains UTF-8")]
    public bool SpanContains() => _haystack.RawBytes.AsSpan().IndexOf(_needle.RawBytes) >= 0;

    [Benchmark(Description = "Text.Contains UTF-8")]
    public bool TextContains_Utf8() => _haystack.Utf32.Contains(_needle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16")]
    public bool TextContains_Utf16() => _haystack.Utf32.Contains(_needle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32")]
    public bool TextContains_Utf32() => _haystack.Utf32.Contains(_needle.Utf32);

    [Benchmark(Description = "string.Contains miss")]
    public bool StringContains_Miss() => _haystack.Str.Contains(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.Contains UTF-8 miss")]
    public bool SpanContains_Miss() => _haystack.RawBytes.AsSpan().IndexOf(_missingNeedle.RawBytes) >= 0;

    [Benchmark(Description = "Text.Contains UTF-8 miss")]
    public bool TextContains_Utf8_Miss() => _haystack.Utf32.Contains(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16 miss")]
    public bool TextContains_Utf16_Miss() => _haystack.Utf32.Contains(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32 miss")]
    public bool TextContains_Utf32_Miss() => _haystack.Utf32.Contains(_missingNeedle.Utf32);
}
