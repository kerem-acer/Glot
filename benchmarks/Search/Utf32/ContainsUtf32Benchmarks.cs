using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ContainsUtf32Benchmarks
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

    [Benchmark(Description = "Text.Contains UTF-8")]
    public bool TextContains_Utf8() => Haystack.Utf8.Contains(Needle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16")]
    public bool TextContains_Utf16() => Haystack.Utf8.Contains(Needle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32")]
    public bool TextContains_Utf32() => Haystack.Utf8.Contains(Needle.Utf32);

    [Benchmark(Description = "Text.Contains UTF-8 miss")]
    public bool TextContains_Utf8_Miss() => Haystack.Utf8.Contains(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.Contains UTF-16 miss")]
    public bool TextContains_Utf16_Miss() => Haystack.Utf8.Contains(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.Contains UTF-32 miss")]
    public bool TextContains_Utf32_Miss() => Haystack.Utf8.Contains(MissingNeedle.Utf32);
}
