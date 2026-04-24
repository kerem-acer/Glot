using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationSpanUtf16Benchmarks
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
    public string StringFromBytesSpan() => new(MemoryMarshal.Cast<byte, char>(_utf16Bytes.AsSpan()));

    [Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16);

    [Benchmark(Description = "Text.FromBytes(span) no-count")]
    public Text TextFromBytesSpanNoCount() => Text.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16, countRunes: false);

    [Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytes()
    {
        using var owned = OwnedText.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16);
    }

    [Benchmark(Description = "OwnedText.FromBytes(span) no-count")]
    public void OwnedTextFromBytesNoCount()
    {
        using var owned = OwnedText.FromBytes(_utf16Bytes.AsSpan(), TextEncoding.Utf16, countRunes: false);
    }
}
