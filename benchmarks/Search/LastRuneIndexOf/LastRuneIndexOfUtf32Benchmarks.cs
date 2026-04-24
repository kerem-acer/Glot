using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class LastRuneIndexOfUtf32Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.LastIndexOf")]
    public int StringLastIndexOf() => _haystack.Str.LastIndexOf(_needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.LastIndexOf UTF-8")]
    public int SpanLastIndexOf() => _haystack.RawBytes.AsSpan().LastIndexOf(_needle.RawBytes);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-8")]
    public int TextLastRuneIndexOf_Utf8() => _haystack.Utf32.LastRuneIndexOf(_needle.Utf8);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-16")]
    public int TextLastRuneIndexOf_Utf16() => _haystack.Utf32.LastRuneIndexOf(_needle.Utf16);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-32")]
    public int TextLastRuneIndexOf_Utf32() => _haystack.Utf32.LastRuneIndexOf(_needle.Utf32);

    [Benchmark(Description = "string.LastIndexOf miss")]
    public int StringLastIndexOf_Miss() => _haystack.Str.LastIndexOf(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.LastIndexOf UTF-8 miss")]
    public int SpanLastIndexOf_Miss() => _haystack.RawBytes.AsSpan().LastIndexOf(_missingNeedle.RawBytes);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-8 miss")]
    public int TextLastRuneIndexOf_Utf8_Miss() => _haystack.Utf32.LastRuneIndexOf(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-16 miss")]
    public int TextLastRuneIndexOf_Utf16_Miss() => _haystack.Utf32.LastRuneIndexOf(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-32 miss")]
    public int TextLastRuneIndexOf_Utf32_Miss() => _haystack.Utf32.LastRuneIndexOf(_missingNeedle.Utf32);
}
