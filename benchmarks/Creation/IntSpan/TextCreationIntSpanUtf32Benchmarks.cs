using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationIntSpanUtf32Benchmarks
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
        _codePoints = TestData.ToCodePoints(_source.Str);
    }

    [Benchmark(Baseline = true, Description = "Encoding.UTF32.GetString(span)")]
    public string EncodingGetStringSpan() => Encoding.UTF32.GetString(MemoryMarshal.AsBytes(_codePoints.AsSpan()));

    [Benchmark(Description = "Text.FromUtf32(span)")]
    public Text TextFromUtf32Span() => Text.FromUtf32(_codePoints.AsSpan());

    [Benchmark(Description = "OwnedText.FromUtf32(span)")]
    public void OwnedTextFromUtf32()
    {
        using var owned = OwnedText.FromUtf32(_codePoints.AsSpan());
    }
}
