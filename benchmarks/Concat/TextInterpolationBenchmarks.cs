using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextInterpolationBenchmarks
{
    [PartSizeParams]
    public int PartSize;
    [ScriptParams]
    public Script Locale;

    string _s1 = null!;
    string _s2 = null!;
    string _s3 = null!;
    string _s4 = null!;
    string _s5 = null!;
    string _s6 = null!;
    string _s7 = null!;
    string _s8 = null!;
    Text _t1;
    Text _t2;
    Text _t3;
    Text _t4;
    Text _t5;
    Text _t6;
    Text _t7;
    Text _t8;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * 8, Locale);
        _s1 = full[..PartSize];
        _s2 = full[PartSize..(PartSize * 2)];
        _s3 = full[(PartSize * 2)..(PartSize * 3)];
        _s4 = full[(PartSize * 3)..(PartSize * 4)];
        _s5 = full[(PartSize * 4)..(PartSize * 5)];
        _s6 = full[(PartSize * 5)..(PartSize * 6)];
        _s7 = full[(PartSize * 6)..(PartSize * 7)];
        _s8 = full[(PartSize * 7)..];
        _t1 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s1));
        _t2 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s2));
        _t3 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s3));
        _t4 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s4));
        _t5 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s5));
        _t6 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s6));
        _t7 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s7));
        _t8 = Text.FromUtf8(Encoding.UTF8.GetBytes(_s8));
    }

    // --- 2 parts (total = PartSize * 2) ---

    [BenchmarkCategory("2 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String2() => $"{_s1}{_s2}";

    [BenchmarkCategory("2 parts"), Benchmark(Description = "Text.Create $\"...\"")]
    public Text TextCreate2() => Text.Create($"{_t1}{_t2}");

    [BenchmarkCategory("2 parts"), Benchmark(Description = "OwnedText.Create $\"...\"")]
    public void OwnedTextCreate2()
    {
        using var result = OwnedText.Create($"{_t1}{_t2}");
    }

    // --- 4 parts (total = PartSize * 4) ---

    [BenchmarkCategory("4 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String4() => $"{_s1}{_s2}{_s3}{_s4}";

    [BenchmarkCategory("4 parts"), Benchmark(Description = "Text.Create $\"...\"")]
    public Text TextCreate4() => Text.Create($"{_t1}{_t2}{_t3}{_t4}");

    [BenchmarkCategory("4 parts"), Benchmark(Description = "OwnedText.Create $\"...\"")]
    public void OwnedTextCreate4()
    {
        using var result = OwnedText.Create($"{_t1}{_t2}{_t3}{_t4}");
    }

    // --- 8 parts (total = PartSize * 8) ---

    [BenchmarkCategory("8 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String8() => $"{_s1}{_s2}{_s3}{_s4}{_s5}{_s6}{_s7}{_s8}";

    [BenchmarkCategory("8 parts"), Benchmark(Description = "Text.Create $\"...\"")]
    public Text TextCreate8() => Text.Create($"{_t1}{_t2}{_t3}{_t4}{_t5}{_t6}{_t7}{_t8}");

    [BenchmarkCategory("8 parts"), Benchmark(Description = "OwnedText.Create $\"...\"")]
    public void OwnedTextCreate8()
    {
        using var result = OwnedText.Create($"{_t1}{_t2}{_t3}{_t4}{_t5}{_t6}{_t7}{_t8}");
    }
}
