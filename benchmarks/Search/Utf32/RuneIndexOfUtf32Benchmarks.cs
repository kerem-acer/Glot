using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class RuneIndexOfUtf32Benchmarks
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

    [Benchmark(Description = "Text.RuneIndexOf UTF-8")]
    public int TextRuneIndexOf_Utf8() => Haystack.Utf8.RuneIndexOf(Needle.Utf8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-16")]
    public int TextRuneIndexOf_Utf16() => Haystack.Utf8.RuneIndexOf(Needle.Utf16);

    [Benchmark(Description = "Text.RuneIndexOf UTF-32")]
    public int TextRuneIndexOf_Utf32() => Haystack.Utf8.RuneIndexOf(Needle.Utf32);

    [Benchmark(Description = "Text.RuneIndexOf UTF-8 miss")]
    public int TextRuneIndexOf_Utf8_Miss() => Haystack.Utf8.RuneIndexOf(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-16 miss")]
    public int TextRuneIndexOf_Utf16_Miss() => Haystack.Utf8.RuneIndexOf(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.RuneIndexOf UTF-32 miss")]
    public int TextRuneIndexOf_Utf32_Miss() => Haystack.Utf8.RuneIndexOf(MissingNeedle.Utf32);
}
