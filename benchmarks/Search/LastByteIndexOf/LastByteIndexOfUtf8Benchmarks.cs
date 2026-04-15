using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class LastByteIndexOfUtf8Benchmarks
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

    [Benchmark(Description = "U8String.LastIndexOf")]
    public int U8LastIndexOf() => _haystack.U8.LastIndexOf(_needle.U8);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-8")]
    public int TextLastByteIndexOf_Utf8() => _haystack.Utf8.LastByteIndexOf(_needle.Utf8);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-16")]
    public int TextLastByteIndexOf_Utf16() => _haystack.Utf8.LastByteIndexOf(_needle.Utf16);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-32")]
    public int TextLastByteIndexOf_Utf32() => _haystack.Utf8.LastByteIndexOf(_needle.Utf32);

    [Benchmark(Description = "string.LastIndexOf miss")]
    public int StringLastIndexOf_Miss() => _haystack.Str.LastIndexOf(_missingNeedle.Str, StringComparison.Ordinal);

    [Benchmark(Description = "Span.LastIndexOf UTF-8 miss")]
    public int SpanLastIndexOf_Miss() => _haystack.RawBytes.AsSpan().LastIndexOf(_missingNeedle.RawBytes);

    [Benchmark(Description = "U8String.LastIndexOf miss")]
    public int U8LastIndexOf_Miss() => _haystack.U8.LastIndexOf(_missingNeedle.U8);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-8 miss")]
    public int TextLastByteIndexOf_Utf8_Miss() => _haystack.Utf8.LastByteIndexOf(_missingNeedle.Utf8);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-16 miss")]
    public int TextLastByteIndexOf_Utf16_Miss() => _haystack.Utf8.LastByteIndexOf(_missingNeedle.Utf16);

    [Benchmark(Description = "Text.LastByteIndexOf UTF-32 miss")]
    public int TextLastByteIndexOf_Utf32_Miss() => _haystack.Utf8.LastByteIndexOf(_missingNeedle.Utf32);
}
