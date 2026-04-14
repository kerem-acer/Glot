using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TextMutationBenchmarks
{
    [SearchSizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    string _source = null!;
    string _markerStr = null!;
    string _replacementStr = null!;
    Text _sourceUtf8;
    Text _markerUtf8;
    Text _replacementUtf8;
    U8String _u8Source;
    U8String _u8Marker;
    U8String _u8Replacement;

    [GlobalSetup]
    public void Setup()
    {
        (_markerStr, _replacementStr) = TestData.MarkerPair(Locale);

        // Build source with markers injected at regular intervals
        var raw = TestData.Generate(N, Locale);
        var sb = new System.Text.StringBuilder(raw.Length + N / 10);
        var step = 20;
        for (var i = 0; i < raw.Length; i += step)
        {
            var end = Math.Min(i + step, raw.Length);
            sb.Append(raw, i, end - i);
            if (end < raw.Length)
            {
                sb.Append(_markerStr);
            }
        }

        _source = sb.ToString();
        _sourceUtf8 = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(_source));
        _markerUtf8 = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(_markerStr));
        _replacementUtf8 = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(_replacementStr));
        _u8Source = new U8String(System.Text.Encoding.UTF8.GetBytes(_source));
        _u8Marker = new U8String(System.Text.Encoding.UTF8.GetBytes(_markerStr));
        _u8Replacement = new U8String(System.Text.Encoding.UTF8.GetBytes(_replacementStr));
    }

    // --- Replace ---

    [BenchmarkCategory("Replace"), Benchmark(Baseline = true, Description = "string.Replace")]
    public string StringReplace() => _source.Replace(_markerStr, _replacementStr);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.Replace")]
    public Text TextReplace() => _sourceUtf8.Replace(_markerUtf8, _replacementUtf8);

    [BenchmarkCategory("Replace"), Benchmark(Description = "Text.ReplacePooled")]
    public void TextReplacePooled()
    {
        using var result = _sourceUtf8.ReplacePooled(_markerUtf8, _replacementUtf8);
    }

    [BenchmarkCategory("Replace"), Benchmark(Description = "U8String.Replace")]
    public U8String U8Replace() => _u8Source.Replace(_u8Marker, _u8Replacement);

    // --- ToUpper ---

    [BenchmarkCategory("ToUpper"), Benchmark(Baseline = true, Description = "string.ToUpperInvariant")]
    public string StringToUpper() => _source.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariant")]
    public Text TextToUpper() => _sourceUtf8.ToUpperInvariant();

    [BenchmarkCategory("ToUpper"), Benchmark(Description = "Text.ToUpperInvariantPooled")]
    public void TextToUpperPooled()
    {
        using var result = _sourceUtf8.ToUpperInvariantPooled();
    }
}
