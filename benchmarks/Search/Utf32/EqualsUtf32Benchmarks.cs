using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EqualsUtf32Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet A, B, Diff;

    [GlobalSetup]
    public void Setup()
    {
        A = EncodedSet.From(TestData.Generate(N, Locale));
        B = EncodedSet.From(TestData.Generate(N, Locale));
        Diff = EncodedSet.From(TestData.Mutate(A.Str));
    }

    [Benchmark(Description = "Text.Equals UTF-8")]
    public bool TextEquals_Utf8() => A.Utf8.Equals(B.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16")]
    public bool TextEquals_Utf16() => A.Utf8.Equals(B.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32")]
    public bool TextEquals_Utf32() => A.Utf8.Equals(B.Utf32);

    [Benchmark(Description = "Text.Equals UTF-8 different")]
    public bool TextEquals_Utf8_Diff() => A.Utf8.Equals(Diff.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16 different")]
    public bool TextEquals_Utf16_Diff() => A.Utf8.Equals(Diff.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32 different")]
    public bool TextEquals_Utf32_Diff() => A.Utf8.Equals(Diff.Utf32);

}
