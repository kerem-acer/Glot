using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextInterpolationUtf16Benchmarks
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

    [BenchmarkCategory("2 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String2() => $"{_p1.Str}{_p2.Str}";

    [BenchmarkCategory("2 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-16")]
    public Text TextCreate2() => Text.Create($"{_p1.Utf16}{_p2.Utf16}");

    [BenchmarkCategory("2 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-16")]
    public void OwnedTextCreate2()
    {
        using var result = OwnedText.Create($"{_p1.Utf16}{_p2.Utf16}");
    }

    [BenchmarkCategory("2 parts"), Benchmark(Description = "LinkedTextUtf16 $\"...\"")]
    public LinkedTextUtf16 LinkedUtf16Create2() => LinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}");

    [BenchmarkCategory("2 parts"), Benchmark(Description = "OwnedLinkedTextUtf16 $\"...\"")]
    public void OwnedLinkedUtf16Create2()
    {
        using var result = OwnedLinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}");
    }

    // --- 4 parts ---

    [BenchmarkCategory("4 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String4() => $"{_p1.Str}{_p2.Str}{_p3.Str}{_p4.Str}";

    [BenchmarkCategory("4 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-16")]
    public Text TextCreate4() => Text.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}");

    [BenchmarkCategory("4 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-16")]
    public void OwnedTextCreate4()
    {
        using var result = OwnedText.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}");
    }

    [BenchmarkCategory("4 parts"), Benchmark(Description = "LinkedTextUtf16 $\"...\"")]
    public LinkedTextUtf16 LinkedUtf16Create4() => LinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}");

    [BenchmarkCategory("4 parts"), Benchmark(Description = "OwnedLinkedTextUtf16 $\"...\"")]
    public void OwnedLinkedUtf16Create4()
    {
        using var result = OwnedLinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}");
    }

    // --- 8 parts ---

    [BenchmarkCategory("8 parts"), Benchmark(Baseline = true, Description = "string $\"...\"")]
    public string String8() => $"{_p1.Str}{_p2.Str}{_p3.Str}{_p4.Str}{_p5.Str}{_p6.Str}{_p7.Str}{_p8.Str}";

    [BenchmarkCategory("8 parts"), Benchmark(Description = "Text.Create $\"...\" UTF-16")]
    public Text TextCreate8() => Text.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}{_p5.Utf16}{_p6.Utf16}{_p7.Utf16}{_p8.Utf16}");

    [BenchmarkCategory("8 parts"), Benchmark(Description = "OwnedText.Create $\"...\" UTF-16")]
    public void OwnedTextCreate8()
    {
        using var result = OwnedText.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}{_p5.Utf16}{_p6.Utf16}{_p7.Utf16}{_p8.Utf16}");
    }

    [BenchmarkCategory("8 parts"), Benchmark(Description = "LinkedTextUtf16 $\"...\"")]
    public LinkedTextUtf16 LinkedUtf16Create8() => LinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}{_p5.Utf16}{_p6.Utf16}{_p7.Utf16}{_p8.Utf16}");

    [BenchmarkCategory("8 parts"), Benchmark(Description = "OwnedLinkedTextUtf16 $\"...\"")]
    public void OwnedLinkedUtf16Create8()
    {
        using var result = OwnedLinkedTextUtf16.Create($"{_p1.Utf16}{_p2.Utf16}{_p3.Utf16}{_p4.Utf16}{_p5.Utf16}{_p6.Utf16}{_p7.Utf16}{_p8.Utf16}");
    }
}
