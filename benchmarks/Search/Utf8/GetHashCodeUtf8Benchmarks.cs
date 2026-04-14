using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class GetHashCodeUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet _a;

    [GlobalSetup]
    public void Setup()
    {
        _a = EncodedSet.From(TestData.Generate(N, Locale));
    }

    [Benchmark(Baseline = true, Description = "U8String.GetHashCode")]
    public int U8GetHashCode() => _a.U8.GetHashCode();

    [Benchmark(Description = "Text.GetHashCode")]
    public int TextGetHashCode() => _a.Utf8.GetHashCode();
}
