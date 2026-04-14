using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class CompareToUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet A, Diff;

    [GlobalSetup]
    public void Setup()
    {
        A = EncodedSet.From(TestData.Generate(N, Locale));
        Diff = EncodedSet.From(TestData.Mutate(A.Str));
    }

    [Benchmark(Description = "Text.CompareTo UTF-8")]
    public int TextCompareTo_Utf8() => A.Utf8.CompareTo(Diff.Utf8);

    [Benchmark(Description = "Text.CompareTo UTF-16")]
    public int TextCompareTo_Utf16() => A.Utf8.CompareTo(Diff.Utf16);

    [Benchmark(Description = "Text.CompareTo UTF-32")]
    public int TextCompareTo_Utf32() => A.Utf8.CompareTo(Diff.Utf32);

}
