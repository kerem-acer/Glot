using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextMutationUtf32Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source, _marker, _replacement;

    [GlobalSetup]
    public void Setup()
    {
        var (markerStr, replacementStr) = TestData.MarkerPair(Locale);
        var raw = TestData.Generate(N, Locale);
        var sb = new System.Text.StringBuilder(raw.Length + N / 10);
        var step = 20;
        for (var i = 0; i < raw.Length; i += step)
        {
            var end = Math.Min(i + step, raw.Length);
            sb.Append(raw, i, end - i);
            if (end < raw.Length)
            {
                sb.Append(markerStr);
            }
        }

        var sourceStr = sb.ToString();
        _source = EncodedSet.From(sourceStr);
        _marker = EncodedSet.From(markerStr);
        _replacement = EncodedSet.From(replacementStr);
    }

    // --- Replace ---

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.Replace UTF-32")]
    public Text TextReplace_Utf32() => _source.Utf32.Replace(_marker.Utf32, _replacement.Utf32);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.ReplacePooled UTF-32")]
    public void TextReplacePooled_Utf32()
    {
        using var result = _source.Utf32.ReplacePooled(_marker.Utf32, _replacement.Utf32);
    }

    // --- ToUpper ---

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariant UTF-32")]
    public Text TextToUpperInvariant_Utf32() => _source.Utf32.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariantPooled UTF-32")]
    public void TextToUpperInvariantPooled_Utf32()
    {
        using var result = _source.Utf32.ToUpperInvariantPooled();
    }
}
