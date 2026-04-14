using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

public enum TargetEncoding
{
    Utf8,
    Utf16,
    Utf32
}

/// <summary>
/// Creation from raw bytes across all encodings: string vs Text vs OwnedText.
/// </summary>
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextCreationBenchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    [Params(TargetEncoding.Utf8, TargetEncoding.Utf16, TargetEncoding.Utf32)]
    public TargetEncoding Enc;

    byte[] _rawBytes = null!;
    string _source = null!;
    TextEncoding _textEncoding;

    [GlobalSetup]
    public void Setup()
    {
        _source = TestData.Generate(N, Locale);

        _textEncoding = Enc switch
        {
            TargetEncoding.Utf8 => TextEncoding.Utf8,
            TargetEncoding.Utf16 => TextEncoding.Utf16,
            TargetEncoding.Utf32 => TextEncoding.Utf32,
            _ => TextEncoding.Utf8,
        };

        _rawBytes = Enc switch
        {
            TargetEncoding.Utf8 => Encoding.UTF8.GetBytes(_source),
            TargetEncoding.Utf16 => MemoryMarshal.AsBytes(_source.AsSpan()).ToArray(),
            TargetEncoding.Utf32 => ToUtf32Bytes(_source),
            _ => Encoding.UTF8.GetBytes(_source),
        };
    }

    static byte[] ToUtf32Bytes(string s)
    {
        var runes = new List<int>();
        foreach (var rune in s.EnumerateRunes())
        {
            runes.Add(rune.Value);
        }

        return MemoryMarshal.AsBytes(runes.ToArray().AsSpan()).ToArray();
    }
    // --- From raw bytes ---

    [BenchmarkCategory("FromBytes"), Benchmark(Baseline = true, Description = "System baseline")]
    public object SystemBaseline() => Enc switch
    {
        TargetEncoding.Utf8 => Encoding.UTF8.GetString(_rawBytes),
        TargetEncoding.Utf16 => new string(MemoryMarshal.Cast<byte, char>(_rawBytes)),
        TargetEncoding.Utf32 => Encoding.UTF32.GetString(_rawBytes),
        _ => Encoding.UTF8.GetString(_rawBytes),
    };

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(_rawBytes, _textEncoding);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(_rawBytes.AsSpan(), _textEncoding);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytes()
    {
        using var owned = OwnedText.FromBytes(_rawBytes.AsSpan(), _textEncoding);
    }
}
