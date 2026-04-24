using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class LastRuneIndexOfUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.LastIndexOf")]
    public int StringLastIndexOf() => Haystack.Str.LastIndexOf(Needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.LastIndexOf UTF-8")]
    public int SpanLastIndexOf() => Haystack.RawBytes.AsSpan().LastIndexOf(Needle.RawBytes);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-8")]
    public int TextLastRuneIndexOf_Utf8() => Haystack.Utf16.LastRuneIndexOf(Needle.Utf8);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-16")]
    public int TextLastRuneIndexOf_Utf16() => Haystack.Utf16.LastRuneIndexOf(Needle.Utf16);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-32")]
    public int TextLastRuneIndexOf_Utf32() => Haystack.Utf16.LastRuneIndexOf(Needle.Utf32);

    [Benchmark(Description = "string.LastIndexOf miss")]
    public int StringLastIndexOf_Miss() => Haystack.Str.LastIndexOf(MissingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.LastIndexOf UTF-8 miss")]
    public int SpanLastIndexOf_Miss() => Haystack.RawBytes.AsSpan().LastIndexOf(MissingNeedle.RawBytes);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-8 miss")]
    public int TextLastRuneIndexOf_Utf8_Miss() => Haystack.Utf16.LastRuneIndexOf(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-16 miss")]
    public int TextLastRuneIndexOf_Utf16_Miss() => Haystack.Utf16.LastRuneIndexOf(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.LastRuneIndexOf UTF-32 miss")]
    public int TextLastRuneIndexOf_Utf32_Miss() => Haystack.Utf16.LastRuneIndexOf(MissingNeedle.Utf32);
}
