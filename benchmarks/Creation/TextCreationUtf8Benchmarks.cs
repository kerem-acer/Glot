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

    string _source = null!;
    byte[] _utf8Bytes = null!;
    ImmutableArray<byte> _immutableUtf8;

    [GlobalSetup]
    public void Setup()
    {
        _source = TestData.Generate(N, Locale);
        _utf8Bytes = Encoding.UTF8.GetBytes(_source);
        _immutableUtf8 = ImmutableArray.Create(_utf8Bytes);
    }

    // --- From byte[] ---

    [BenchmarkCategory("FromByteArray"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetString() => Encoding.UTF8.GetString(_utf8Bytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "Text.FromUtf8(byte[])")]
    public Text TextFromUtf8Array() => Text.FromUtf8(_utf8Bytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "new U8String(byte[])")]
    public U8String U8FromArray() => new(_utf8Bytes);

    [BenchmarkCategory("FromByteArray"), Benchmark(Description = "OwnedText.FromUtf8(byte[])")]
    public void OwnedTextFromUtf8Array()
    {
        using var owned = OwnedText.FromBytes(_utf8Bytes.AsSpan(), TextEncoding.Utf8, countRunes: false);
    }

    // --- From ASCII byte[] (rune count = byte count, O(1)) ---

    [BenchmarkCategory("FromAscii"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetStringAscii() => Encoding.UTF8.GetString(_utf8Bytes);

    [BenchmarkCategory("FromAscii"), Benchmark(Description = "Text.FromAscii(byte[])")]
    public Text TextFromAsciiArray() => Text.FromAscii(_utf8Bytes);

    // --- From span ---

    [BenchmarkCategory("FromSpan"), Benchmark(Baseline = true, Description = "Encoding.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF8.GetString(_utf8Bytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "Text.FromUtf8(span)")]
    public Text TextFromUtf8Span() => Text.FromUtf8(_utf8Bytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "Text.FromUtf8(span, countRunes: false)")]
    public Text TextFromUtf8SpanNoRuneCount() => Text.FromUtf8(_utf8Bytes.AsSpan(), countRunes: false);

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "new U8String(span)")]
    public U8String U8FromSpan() => new(_utf8Bytes.AsSpan());

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "OwnedText.FromUtf8(span)")]
    public void OwnedTextFromUtf8Span()
    {
        using var owned = OwnedText.FromUtf8(_utf8Bytes.AsSpan());
    }

    [BenchmarkCategory("FromSpan"), Benchmark(Description = "OwnedText.FromUtf8(span, countRunes: false)")]
    public void OwnedTextFromUtf8SpanNoRuneCount()
    {
        using var owned = OwnedText.FromUtf8(_utf8Bytes.AsSpan(), countRunes: false);
    }

    // --- From ImmutableArray<byte> ---

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetStringImmutable() => Encoding.UTF8.GetString(_utf8Bytes);

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Description = "Text.FromUtf8(ImmutableArray)")]
    public Text TextFromImmutable() => Text.FromUtf8(_immutableUtf8);

    [BenchmarkCategory("FromImmutableArray"), Benchmark(Description = "new U8String(ImmutableArray)")]
    public U8String U8FromImmutable() => new(_immutableUtf8);

    // --- From string ---

    [BenchmarkCategory("FromString"), Benchmark(Baseline = true, Description = "new string(source)")]
    public string StringBaseline() => new(_source);

    [BenchmarkCategory("FromString"), Benchmark(Description = "Text.From(string)")]
    public Text TextFromString() => Text.From(_source);

    [BenchmarkCategory("FromString"), Benchmark(Description = "Text.FromAscii(string)")]
    public Text TextFromAsciiString() => Text.FromAscii(_source);

    [BenchmarkCategory("FromString"), Benchmark(Description = "new U8String(string)")]
    public U8String U8FromString() => new(_source);
}
