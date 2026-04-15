using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ToUpperUtf8Benchmarks
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

    [Benchmark(Description = "Text.ToUpperInvariant UTF-8")]
    public Text TextToUpperInvariant() => _source.Utf8.ToUpperInvariant();

    [Benchmark(Description = "Text.ToUpperInvariantPooled UTF-8")]
    public void TextToUpperInvariantPooled()
    {
        using var result = _source.Utf8.ToUpperInvariantPooled();
    }
}
