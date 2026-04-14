using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextInterpolationUtf32Benchmarks
{
    [PartSizeParams]
    public int PartSize;

    [ScriptParams]
    public Script Locale;

    EncodedSet _p1, _p2, _p3, _p4, _p5, _p6, _p7, _p8;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * 8, Locale);
        _p1 = EncodedSet.From(full[..PartSize]);
        _p2 = EncodedSet.From(full[PartSize..(PartSize * 2)]);
        _p3 = EncodedSet.From(full[(PartSize * 2)..(PartSize * 3)]);
        _p4 = EncodedSet.From(full[(PartSize * 3)..(PartSize * 4)]);
        _p5 = EncodedSet.From(full[(PartSize * 4)..(PartSize * 5)]);
        _p6 = EncodedSet.From(full[(PartSize * 5)..(PartSize * 6)]);
        _p7 = EncodedSet.From(full[(PartSize * 6)..(PartSize * 7)]);
        _p8 = EncodedSet.From(full[(PartSize * 7)..]);
    }

    // --- 2 parts ---

    [BenchmarkCategory("2 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-32")]
    public Text TextCreate2() => Text.Create($"{_p1.Utf32}{_p2.Utf32}");

    [BenchmarkCategory("2 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-32")]
    public void OwnedTextCreate2()
    {
        using var result = OwnedText.Create($"{_p1.Utf32}{_p2.Utf32}");
    }

    // --- 4 parts ---

    [BenchmarkCategory("4 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-32")]
    public Text TextCreate4() => Text.Create($"{_p1.Utf32}{_p2.Utf32}{_p3.Utf32}{_p4.Utf32}");

    [BenchmarkCategory("4 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-32")]
    public void OwnedTextCreate4()
    {
        using var result = OwnedText.Create($"{_p1.Utf32}{_p2.Utf32}{_p3.Utf32}{_p4.Utf32}");
    }

    // --- 8 parts ---

    [BenchmarkCategory("8 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-32")]
    public Text TextCreate8() => Text.Create($"{_p1.Utf32}{_p2.Utf32}{_p3.Utf32}{_p4.Utf32}{_p5.Utf32}{_p6.Utf32}{_p7.Utf32}{_p8.Utf32}");

    [BenchmarkCategory("8 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-32")]
    public void OwnedTextCreate8()
    {
        using var result = OwnedText.Create($"{_p1.Utf32}{_p2.Utf32}{_p3.Utf32}{_p4.Utf32}{_p5.Utf32}{_p6.Utf32}{_p7.Utf32}{_p8.Utf32}");
    }
}
