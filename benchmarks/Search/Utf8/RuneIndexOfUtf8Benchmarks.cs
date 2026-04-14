using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class RuneIndexOfUtf8Benchmarks
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

    [Benchmark(Baseline = true, Description = "U8String.IndexOf")]
    public int U8IndexOf() => _haystack.U8.IndexOf(_needle.U8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-8")]
    public int TextRuneIndexOf_Utf8() => _haystack.Utf8.RuneIndexOf(_needle.Utf8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-16")]
    public int TextRuneIndexOf_Utf16() => _haystack.Utf8.RuneIndexOf(_needle.Utf16);

    [Benchmark(Description = "Text.RuneIndexOf UTF-32")]
    public int TextRuneIndexOf_Utf32() => _haystack.Utf8.RuneIndexOf(_needle.Utf32);

    [Benchmark(Description = "U8String.IndexOf miss")]
    public int U8IndexOf_Miss() => _haystack.U8.IndexOf(_missingNeedle.U8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-8 miss")]
    public int TextRuneIndexOf_Utf8_Miss() => _haystack.Utf8.RuneIndexOf(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.RuneIndexOf UTF-16 miss")]
    public int TextRuneIndexOf_Utf16_Miss() => _haystack.Utf8.RuneIndexOf(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.RuneIndexOf UTF-32 miss")]
    public int TextRuneIndexOf_Utf32_Miss() => _haystack.Utf8.RuneIndexOf(_missingNeedle.Utf32);
}
