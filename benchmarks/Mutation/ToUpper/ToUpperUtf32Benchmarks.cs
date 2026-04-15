using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ToUpperUtf32Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
    }

    [Benchmark(Description = "Text.ToUpperInvariant UTF-32")]
    public Text TextToUpperInvariant() => _source.Utf32.ToUpperInvariant();

    [Benchmark(Description = "Text.ToUpperInvariantPooled UTF-32")]
    public void TextToUpperInvariantPooled()
    {
        using var result = _source.Utf32.ToUpperInvariantPooled();
    }
}
