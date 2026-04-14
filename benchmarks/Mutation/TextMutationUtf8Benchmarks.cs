using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextMutationUtf8Benchmarks
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

    [BenchmarkCategory("Replace"), Benchmark(Description = "U8String.Replace")]
    public U8String U8Replace() => _source.U8.Replace(_marker.U8, _replacement.U8);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.Replace UTF-8")]
    public Text TextReplace_Utf8() => _source.Utf8.Replace(_marker.Utf8, _replacement.Utf8);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.ReplacePooled UTF-8")]
    public void TextReplacePooled_Utf8()
    {
        using var result = _source.Utf8.ReplacePooled(_marker.Utf8, _replacement.Utf8);
    }

    // --- ToUpper ---

    [BenchmarkCategory("ToUpper"), Benchmark(Baseline = true, Description = "string.ToUpperInvariant")]
    public string StringToUpperInvariant() => _source.Str.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariant UTF-8")]
    public Text TextToUpperInvariant_Utf8() => _source.Utf8.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariantPooled UTF-8")]
    public void TextToUpperInvariantPooled_Utf8()
    {
        using var result = _source.Utf8.ToUpperInvariantPooled();
    }
}
