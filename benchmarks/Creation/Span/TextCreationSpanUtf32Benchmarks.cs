using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationSpanUtf32Benchmarks
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

    [Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF32.GetString(_utf32Bytes.AsSpan());

    [Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32);

    [Benchmark(Description = "Text.FromBytes(span) no-count")]
    public Text TextFromBytesSpanNoCount() => Text.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32, countRunes: false);

    [Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytes()
    {
        using var owned = OwnedText.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32);
    }

    [Benchmark(Description = "OwnedText.FromBytes(span) no-count")]
    public void OwnedTextFromBytesNoCount()
    {
        using var owned = OwnedText.FromBytes(_utf32Bytes.AsSpan(), TextEncoding.Utf32, countRunes: false);
    }
}
