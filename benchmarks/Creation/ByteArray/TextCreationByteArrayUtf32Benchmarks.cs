using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationByteArrayUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    byte[] _utf32Bytes = null!;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _utf32Bytes = MemoryMarshal.AsBytes(TestData.ToCodePoints(_source.Str).AsSpan()).ToArray();
    }

    [Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString")]
    public string EncodingGetString() => Encoding.UTF32.GetString(_utf32Bytes);

    [Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(_utf32Bytes, TextEncoding.Utf32);

    [Benchmark(Description = "Text.FromBytes(byte[]) no-count")]
    public Text TextFromBytesArrayNoCount() => Text.FromBytes(_utf32Bytes, TextEncoding.Utf32, countRunes: false);

    [Benchmark(Description = "OwnedText.FromBytes(byte[])")]
    public void OwnedTextFromBytesArray()
    {
        using var owned = OwnedText.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32);
    }

    [Benchmark(Description = "OwnedText.FromBytes(byte[]) no-count")]
    public void OwnedTextFromBytesArrayNoCount()
    {
        using var owned = OwnedText.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32, countRunes: false);
    }
}
