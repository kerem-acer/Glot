using System.Text;
using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationSpanUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
    }

    [Benchmark(Baseline = true, Description = "Encoding.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF8.GetString(_source.RawBytes.AsSpan());

    [Benchmark(Description = "Text.FromUtf8(span)")]
    public Text TextFromUtf8Span() => Text.FromUtf8(_source.RawBytes.AsSpan());

    [Benchmark(Description = "Text.FromUtf8(span) no-count")]
    public Text TextFromUtf8SpanNoCount() => Text.FromUtf8(_source.RawBytes.AsSpan(), countRunes: false);

    [Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8);

    [Benchmark(Description = "Text.FromBytes(span) no-count")]
    public Text TextFromBytesSpanNoCount() => Text.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8, countRunes: false);

    [Benchmark(Description = "new U8String(span)")]
    public U8String U8FromSpan() => new(_source.RawBytes.AsSpan());

    [Benchmark(Description = "OwnedText.FromUtf8(span)")]
    public void OwnedTextFromUtf8Span()
    {
        using var owned = OwnedText.FromUtf8(_source.RawBytes.AsSpan());
    }

    [Benchmark(Description = "OwnedText.FromUtf8(span) no-count")]
    public void OwnedTextFromUtf8SpanNoCount()
    {
        using var owned = OwnedText.FromUtf8(_source.RawBytes.AsSpan(), countRunes: false);
    }

    [Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytesSpan()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8);
    }

    [Benchmark(Description = "OwnedText.FromBytes(span) no-count")]
    public void OwnedTextFromBytesSpanNoCount()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8, countRunes: false);
    }
}
