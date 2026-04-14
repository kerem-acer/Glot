using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextCreationUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    int[] _codePoints = null!;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _codePoints = ToCodePoints(_source.Str);
    }

    static int[] ToCodePoints(string s)
    {
        var runes = new List<int>();
        foreach (var rune in s.EnumerateRunes())
        {
            runes.Add(rune.Value);
        }

        return runes.ToArray();
    }

    // --- From int[] ---

    [BenchmarkCategory("FromIntArray"), Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString")]
    public string EncodingGetString() => Encoding.UTF32.GetString(MemoryMarshal.AsBytes(_codePoints.AsSpan()));

    [BenchmarkCategory("FromIntArray"), Benchmark(Description = "Text.FromUtf32(int[])")]
    public Text TextFromUtf32Array() => Text.FromUtf32(_codePoints);

    // --- From ReadOnlySpan<int> ---

    [BenchmarkCategory("FromIntSpan"), Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF32.GetString(MemoryMarshal.AsBytes(_codePoints.AsSpan()));

    [BenchmarkCategory("FromIntSpan"), Benchmark(Description = "Text.FromUtf32(span)")]
    public Text TextFromUtf32Span() => Text.FromUtf32(_codePoints.AsSpan());

    // --- From raw bytes ---

    [BenchmarkCategory("FromBytes"), Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString")]
    public string EncodingGetStringBytes() => Encoding.UTF32.GetString(MemoryMarshal.AsBytes(_codePoints.AsSpan()));

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(MemoryMarshal.AsBytes(_codePoints.AsSpan()).ToArray(), TextEncoding.Utf32);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(MemoryMarshal.AsBytes(_codePoints.AsSpan()), TextEncoding.Utf32);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytes()
    {
        using var owned = OwnedText.FromBytes(MemoryMarshal.AsBytes(_codePoints.AsSpan()), TextEncoding.Utf32);
    }
}
