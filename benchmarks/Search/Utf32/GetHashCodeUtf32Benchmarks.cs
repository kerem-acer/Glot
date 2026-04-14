using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class GetHashCodeUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet A;

    [GlobalSetup]
    public void Setup()
    {
        A = EncodedSet.From(TestData.Generate(N, Locale));
    }

    [Benchmark(Description = "Text.GetHashCode")]
    public int TextGetHashCode() => A.Utf32.GetHashCode();
}
