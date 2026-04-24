using System.Text;
using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationByteArrayUtf8Benchmarks
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

    [Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetString() => Encoding.UTF8.GetString(_source.RawBytes);

    [Benchmark(Description = "Text.FromUtf8(byte[])")]
    public Text TextFromUtf8Array() => Text.FromUtf8(_source.RawBytes);

    [Benchmark(Description = "Text.FromUtf8(byte[]) no-count")]
    public Text TextFromUtf8ArrayNoCount() => Text.FromUtf8(_source.RawBytes, countRunes: false);

    [Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(_source.RawBytes, TextEncoding.Utf8);

    [Benchmark(Description = "Text.FromBytes(byte[]) no-count")]
    public Text TextFromBytesArrayNoCount() => Text.FromBytes(_source.RawBytes, TextEncoding.Utf8, countRunes: false);

    [Benchmark(Description = "new U8String(byte[])")]
    public U8String U8FromArray() => new(_source.RawBytes);

    [Benchmark(Description = "OwnedText.FromUtf8(byte[])")]
    public void OwnedTextFromUtf8Array()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8);
    }

    [Benchmark(Description = "OwnedText.FromUtf8(byte[]) no-count")]
    public void OwnedTextFromUtf8ArrayNoCount()
    {
        using var owned = OwnedText.FromBytes(_source.RawBytes.AsSpan(), TextEncoding.Utf8, countRunes: false);
    }

    [Benchmark(Description = "Text.FromAscii(byte[])")]
    public Text TextFromAsciiArray() => Text.FromAscii(_source.RawBytes);

    [Benchmark(Description = "OwnedText.FromAscii(byte[])")]
    public void OwnedTextFromAsciiArray()
    {
        using var owned = OwnedText.FromAscii(_source.RawBytes.AsSpan());
    }
}
