using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class StartsWithUtf8Benchmarks
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

    [Benchmark(Baseline = true, Description = "U8String.StartsWith")]
    public bool U8StartsWith() => _haystack.U8.StartsWith(_needle.U8);

    [Benchmark(Description = "Text.StartsWith UTF-8")]
    public bool TextStartsWith_Utf8() => _haystack.Utf8.StartsWith(_needle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16")]
    public bool TextStartsWith_Utf16() => _haystack.Utf8.StartsWith(_needle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32")]
    public bool TextStartsWith_Utf32() => _haystack.Utf8.StartsWith(_needle.Utf32);

    [Benchmark(Description = "U8String.StartsWith miss")]
    public bool U8StartsWith_Miss() => _haystack.U8.StartsWith(_missingNeedle.U8);

    [Benchmark(Description = "Text.StartsWith UTF-8 miss")]
    public bool TextStartsWith_Utf8_Miss() => _haystack.Utf8.StartsWith(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16 miss")]
    public bool TextStartsWith_Utf16_Miss() => _haystack.Utf8.StartsWith(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32 miss")]
    public bool TextStartsWith_Utf32_Miss() => _haystack.Utf8.StartsWith(_missingNeedle.Utf32);
}
