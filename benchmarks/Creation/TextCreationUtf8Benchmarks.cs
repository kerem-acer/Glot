using System.Collections.Immutable;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextCreationUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    ImmutableArray<byte> _immutableUtf8;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _immutableUtf8 = ImmutableArray.Create(_source.RawBytes);
    }

    // --- From byte[] ---

    [BenchmarkCategory("FromByteArray"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetString() => Encoding.UTF8.GetString(_source.RawBytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "Text.FromUtf8(byte[])")]
    public Text TextFromUtf8Array() => Text.FromUtf8(_source.RawBytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(_source.RawBytes, TextEncoding.Utf8);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "new U8String(byte[])")]
    public U8String U8FromArray() => new(_source.RawBytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "OwnedText.FromUtf8(byte[])")]
    public void OwnedTextFromUtf8Array()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8, countRunes: false);
    }

    // --- From ASCII byte[] ---

    [BenchmarkCategory("FromAscii"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetStringAscii() => Encoding.UTF8.GetString(_source.RawBytes);

    [BenchmarkCategory("FromAscii"), Benchmark(Description = "Text.FromAscii(byte[])")]
    public Text TextFromAsciiArray() => Text.FromAscii(_source.RawBytes);

    // --- From span ---

    [BenchmarkCategory("FromSpan"), Benchmark(Baseline = true, Description = "Encoding.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF8.GetString(_source.RawBytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "Text.FromUtf8(span)")]
    public Text TextFromUtf8Span() => Text.FromUtf8(_source.RawBytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "Text.FromUtf8(span, countRunes: false)")]
    public Text TextFromUtf8SpanNoRuneCount() => Text.FromUtf8(_source.RawBytes.AsSpan(), countRunes: false);

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8);

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "new U8String(span)")]
    public U8String U8FromSpan() => new(_source.RawBytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "OwnedText.FromUtf8(span)")]
    public void OwnedTextFromUtf8Span()
    {
        using var owned = OwnedText.FromUtf8(_source.RawBytes.AsSpan());
    }

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "OwnedText.FromUtf8(span, countRunes: false)")]
    public void OwnedTextFromUtf8SpanNoRuneCount()
    {
        using var owned = OwnedText.FromUtf8(_source.RawBytes.AsSpan(), countRunes: false);
    }

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytesSpan()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8);
    }

    // --- From ImmutableArray<byte> ---

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetStringImmutable() => Encoding.UTF8.GetString(_source.RawBytes);

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Description = "Text.FromUtf8(ImmutableArray)")]
    public Text TextFromImmutable() => Text.FromUtf8(_immutableUtf8);

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Description = "new U8String(ImmutableArray)")]
    public U8String U8FromImmutable() => new(_immutableUtf8);

    // --- From string ---

    [BenchmarkCategory("FromString"), Benchmark(Baseline = true, Description = "new string(source)")]
    public string StringBaseline() => new(_source.Str);

    [BenchmarkCategory("FromString"), Benchmark(Description = "Text.From(string)")]
    public Text TextFromString() => Text.From(_source.Str);

    [BenchmarkCategory("FromString"), Benchmark(Description = "Text.FromAscii(string)")]
    public Text TextFromAsciiString() => Text.FromAscii(_source.Str);

    [BenchmarkCategory("FromString"), Benchmark(Description = "new U8String(string)")]
    public U8String U8FromString() => new(_source.Str);
}
