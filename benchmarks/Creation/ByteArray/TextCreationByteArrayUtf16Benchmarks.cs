using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationByteArrayUtf16Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    byte[] _utf16Bytes = null!;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _utf16Bytes = MemoryMarshal.AsBytes(_source.Str.AsSpan()).ToArray();
    }

    [Benchmark(Baseline = true, Description = "new string(chars)")]
    public string StringFromBytes() => new(MemoryMarshal.Cast<byte, char>(_utf16Bytes));

    [Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(_utf16Bytes, TextEncoding.Utf16);

    [Benchmark(Description = "Text.FromBytes(byte[]) no-count")]
    public Text TextFromBytesArrayNoCount() => Text.FromBytes(_utf16Bytes, TextEncoding.Utf16, countRunes: false);

    [Benchmark(Description = "OwnedText.FromBytes(byte[])")]
    public void OwnedTextFromBytesArray()
    {
        using var owned = OwnedText.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16);
    }

    [Benchmark(Description = "OwnedText.FromBytes(byte[]) no-count")]
    public void OwnedTextFromBytesArrayNoCount()
    {
        using var owned = OwnedText.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16, countRunes: false);
    }
}
