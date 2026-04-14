using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class CompareToUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet _a, _diff;

    [GlobalSetup]
    public void Setup()
    {
        _a = EncodedSet.From(TestData.Generate(N, Locale));
        _diff = EncodedSet.From(TestData.Mutate(_a.Str));
    }

    [Benchmark(Baseline = true, Description = "U8String.CompareTo")]
    public int U8CompareTo() => _a.U8.CompareTo(_diff.U8);

    [Benchmark(Description = "Text.CompareTo UTF-8")]
    public int TextCompareTo_Utf8() => _a.Utf8.CompareTo(_diff.Utf8);

    [Benchmark(Description = "Text.CompareTo UTF-16")]
    public int TextCompareTo_Utf16() => _a.Utf8.CompareTo(_diff.Utf16);

    [Benchmark(Description = "Text.CompareTo UTF-32")]
    public int TextCompareTo_Utf32() => _a.Utf8.CompareTo(_diff.Utf32);

}
