using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ByteIndexOfUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.IndexOf")]
    public int StringIndexOf() => Haystack.Str.IndexOf(Needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.IndexOf UTF-8")]
    public int SpanIndexOf() => Haystack.RawBytes.AsSpan().IndexOf(Needle.RawBytes);

    [Benchmark(Description = "Text.ByteIndexOf UTF-8")]
    public int TextByteIndexOf_Utf8() => Haystack.Utf8.ByteIndexOf(Needle.Utf8);

    [Benchmark(Description = "Text.ByteIndexOf UTF-16")]
    public int TextByteIndexOf_Utf16() => Haystack.Utf8.ByteIndexOf(Needle.Utf16);

    [Benchmark(Description = "Text.ByteIndexOf UTF-32")]
    public int TextByteIndexOf_Utf32() => Haystack.Utf8.ByteIndexOf(Needle.Utf32);

    [Benchmark(Description = "string.IndexOf miss")]
    public int StringIndexOf_Miss() => Haystack.Str.IndexOf(MissingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.IndexOf UTF-8 miss")]
    public int SpanIndexOf_Miss() => Haystack.RawBytes.AsSpan().IndexOf(MissingNeedle.RawBytes);

    [Benchmark(Description = "Text.ByteIndexOf UTF-8 miss")]
    public int TextByteIndexOf_Utf8_Miss() => Haystack.Utf8.ByteIndexOf(MissingNeedle.Utf8);

    [Benchmark(Description = "Text.ByteIndexOf UTF-16 miss")]
    public int TextByteIndexOf_Utf16_Miss() => Haystack.Utf8.ByteIndexOf(MissingNeedle.Utf16);

    [Benchmark(Description = "Text.ByteIndexOf UTF-32 miss")]
    public int TextByteIndexOf_Utf32_Miss() => Haystack.Utf8.ByteIndexOf(MissingNeedle.Utf32);
}
