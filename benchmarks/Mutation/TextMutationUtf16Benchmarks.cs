using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextMutationUtf16Benchmarks
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

    [BenchmarkCategory("Replace"), Benchmark(Baseline = true, Description = "string.Replace")]
    public string StringReplace() => _source.Str.Replace(_marker.Str, _replacement.Str);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.Replace UTF-16")]
    public Text TextReplace_Utf16() => _source.Utf16.Replace(_marker.Utf16, _replacement.Utf16);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.ReplacePooled UTF-16")]
    public void TextReplacePooled_Utf16()
    {
        using var result = _source.Utf16.ReplacePooled(_marker.Utf16, _replacement.Utf16);
    }

    // --- ToUpper ---

    [BenchmarkCategory("ToUpper"), Benchmark(Baseline = true, Description = "string.ToUpperInvariant")]
    public string StringToUpperInvariant() => _source.Str.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariant UTF-16")]
    public Text TextToUpperInvariant_Utf16() => _source.Utf16.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariantPooled UTF-16")]
    public void TextToUpperInvariantPooled_Utf16()
    {
        using var result = _source.Utf16.ToUpperInvariantPooled();
    }
}
