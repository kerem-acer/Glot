using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ToUpperUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.ToUpperInvariant")]
    public string StringToUpperInvariant() => _source.Str.ToUpperInvariant();

    [Benchmark(Description = "Text.ToUpperInvariant UTF-16")]
    public Text TextToUpperInvariant() => _source.Utf16.ToUpperInvariant();

    [Benchmark(Description = "Text.ToUpperInvariantPooled UTF-16")]
    public void TextToUpperInvariantPooled()
    {
        using var result = _source.Utf16.ToUpperInvariantPooled();
    }
}
