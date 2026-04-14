using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class LinkedTextBenchmarks
{
    [PartSizeParams]
    public int PartSize;
    [Params(2, 4, 8, 16)]
    public int Parts;
    [ScriptParams]
    public Script Locale;

    string[] _strings = null!;
    Text[] _texts = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _strings = new string[Parts];
        _texts = new Text[Parts];
        for (var i = 0; i < Parts; i++)
        {
            _strings[i] = full[(i * PartSize)..((i + 1) * PartSize)];
            _texts[i] = Text.FromUtf8(Encoding.UTF8.GetBytes(_strings[i]));
        }
    }

    // --- Create from segments ---

    [BenchmarkCategory("Segments"), Benchmark(Baseline = true, Description = "string.Concat")]
    public string StringConcat() => string.Concat(_strings);

    [BenchmarkCategory("Segments"), Benchmark(Description = "OwnedLinkedTextUtf8.Create")]
    public void LinkedUtf8_Segments()
    {
        using var linked = OwnedLinkedTextUtf8.Create(_texts.AsSpan());
    }

    [BenchmarkCategory("Segments"), Benchmark(Description = "OwnedLinkedTextUtf16.Create")]
    public void LinkedUtf16_Segments()
    {
        using var linked = OwnedLinkedTextUtf16.Create(_texts.AsSpan());
    }
}
