using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class GetHashCodeUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "string.GetHashCode")]
    public int StringGetHashCode() => A.Str.GetHashCode();

    [Benchmark(Description = "Text.GetHashCode")]
    public int TextGetHashCode() => A.Utf16.GetHashCode();
}
