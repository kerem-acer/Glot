using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class EqualsUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    EncodedSet _a, _b, _diff;

    [GlobalSetup]
    public void Setup()
    {
        _a = EncodedSet.From(TestData.Generate(N, Locale));
        _b = EncodedSet.From(TestData.Generate(N, Locale));
        _diff = EncodedSet.From(TestData.Mutate(_a.Str));
    }

    [Benchmark(Baseline = true, Description = "U8String.Equals")]
    public bool U8Equals() => _a.U8.Equals(_b.U8);

    [Benchmark(Description = "Text.Equals UTF-8")]
    public bool TextEquals_Utf8() => _a.Utf8.Equals(_b.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16")]
    public bool TextEquals_Utf16() => _a.Utf8.Equals(_b.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32")]
    public bool TextEquals_Utf32() => _a.Utf8.Equals(_b.Utf32);

    [Benchmark(Description = "U8String.Equals different")]
    public bool U8Equals_Diff() => _a.U8.Equals(_diff.U8);

    [Benchmark(Description = "Text.Equals UTF-8 different")]
    public bool TextEquals_Utf8_Diff() => _a.Utf8.Equals(_diff.Utf8);

    [Benchmark(Description = "Text.Equals UTF-16 different")]
    public bool TextEquals_Utf16_Diff() => _a.Utf8.Equals(_diff.Utf16);

    [Benchmark(Description = "Text.Equals UTF-32 different")]
    public bool TextEquals_Utf32_Diff() => _a.Utf8.Equals(_diff.Utf32);

}
