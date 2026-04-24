using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ByteIndexOfUtf32Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.IndexOf")]
    public int StringIndexOf() => _haystack.Str.IndexOf(_needle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.IndexOf UTF-8")]
    public int SpanIndexOf() => _haystack.RawBytes.AsSpan().IndexOf(_needle.RawBytes);

    [Benchmark(Description = "Text.ByteIndexOf UTF-8")]
    public int TextByteIndexOf_Utf8() => _haystack.Utf32.ByteIndexOf(_needle.Utf8);

    [Benchmark(Description = "Text.ByteIndexOf UTF-16")]
    public int TextByteIndexOf_Utf16() => _haystack.Utf32.ByteIndexOf(_needle.Utf16);

    [Benchmark(Description = "Text.ByteIndexOf UTF-32")]
    public int TextByteIndexOf_Utf32() => _haystack.Utf32.ByteIndexOf(_needle.Utf32);

    [Benchmark(Description = "string.IndexOf miss")]
    public int StringIndexOf_Miss() => _haystack.Str.IndexOf(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.IndexOf UTF-8 miss")]
    public int SpanIndexOf_Miss() => _haystack.RawBytes.AsSpan().IndexOf(_missingNeedle.RawBytes);

    [Benchmark(Description = "Text.ByteIndexOf UTF-8 miss")]
    public int TextByteIndexOf_Utf8_Miss() => _haystack.Utf32.ByteIndexOf(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.ByteIndexOf UTF-16 miss")]
    public int TextByteIndexOf_Utf16_Miss() => _haystack.Utf32.ByteIndexOf(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.ByteIndexOf UTF-32 miss")]
    public int TextByteIndexOf_Utf32_Miss() => _haystack.Utf32.ByteIndexOf(_missingNeedle.Utf32);
}
