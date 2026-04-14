using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class StartsWithUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.StartsWith")]
    public bool StringStartsWith() => Haystack.Str.StartsWith(Needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Text.StartsWith UTF-8")]
    public bool TextStartsWith_Utf8() => Haystack.Utf8.StartsWith(Needle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16")]
    public bool TextStartsWith_Utf16() => Haystack.Utf8.StartsWith(Needle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32")]
    public bool TextStartsWith_Utf32() => Haystack.Utf8.StartsWith(Needle.Utf32);

    [Benchmark(Description = "string.StartsWith miss")]
    public bool StringStartsWith_Miss() => Haystack.Str.StartsWith(MissingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Text.StartsWith UTF-8 miss")]
    public bool TextStartsWith_Utf8_Miss() => Haystack.Utf8.StartsWith(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.StartsWith UTF-16 miss")]
    public bool TextStartsWith_Utf16_Miss() => Haystack.Utf8.StartsWith(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.StartsWith UTF-32 miss")]
    public bool TextStartsWith_Utf32_Miss() => Haystack.Utf8.StartsWith(MissingNeedle.Utf32);
}
