using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextCreationUtf16Benchmarks
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

    // --- From string ---

    [BenchmarkCategory("FromString"), Benchmark(Baseline = true, Description = "new string(source)")]
    public string StringBaseline() => new(_source.Str);

    [BenchmarkCategory("FromString"), Benchmark(Description = "Text.From(string)")]
    public Text TextFromString() => Text.From(_source.Str);

    // --- From char[] ---

    [BenchmarkCategory("FromCharArray"), Benchmark(Baseline = true, Description = "new string(char[])")]
    public string StringFromChars() => new(_source.Str.ToCharArray());

    [BenchmarkCategory("FromCharArray"), Benchmark(Description = "Text.FromChars(char[])")]
    public Text TextFromCharsArray() => Text.FromChars(_source.Str.ToCharArray());

    // --- From ReadOnlySpan<char> ---

    [BenchmarkCategory("FromCharSpan"), Benchmark(Baseline = true, Description = "new string(span)")]
    public string StringFromCharSpan() => new(_source.Str.AsSpan());

    [BenchmarkCategory("FromCharSpan"), Benchmark(Description = "Text.FromChars(span)")]
    public Text TextFromCharsSpan() => Text.FromChars(_source.Str.AsSpan());

    // --- From raw bytes ---

    [BenchmarkCategory("FromBytes"), Benchmark(Baseline = true, Description = "new string(chars)")]
    public string StringFromBytes() => new(MemoryMarshal.Cast<byte, char>(MemoryMarshal.AsBytes(_source.Str.AsSpan())));

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(byte[])")]
    public Text TextFromBytesArray() => Text.FromBytes(MemoryMarshal.AsBytes(_source.Str.AsSpan()).ToArray(), TextEncoding.Utf16);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "Text.FromBytes(span)")]
    public Text TextFromBytesSpan() => Text.FromBytes(MemoryMarshal.AsBytes(_source.Str.AsSpan()), TextEncoding.Utf16);

    [BenchmarkCategory("FromBytes"), Benchmark(Description = "OwnedText.FromBytes(span)")]
    public void OwnedTextFromBytes()
    {
        using var owned = OwnedText.FromBytes(MemoryMarshal.AsBytes(_source.Str.AsSpan()), TextEncoding.Utf16);
    }
}
