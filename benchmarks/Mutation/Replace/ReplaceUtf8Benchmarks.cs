using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ReplaceUtf8Benchmarks
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

        _source = EncodedSet.From(sb.ToString());
        _marker = EncodedSet.From(markerStr);
        _replacement = EncodedSet.From(replacementStr);
    }

    [Benchmark(Baseline = true, Description = "string.Replace")]
    public string StringReplace() => _source.Str.Replace(_marker.Str, _replacement.Str);

    [Benchmark(Description = "U8String.Replace")]
    public U8String U8Replace() => _source.U8.Replace(_marker.U8, _replacement.U8);

    [Benchmark(Description = "Text.Replace UTF-8")]
    public Text TextReplace() => _source.Utf8.Replace(_marker.Utf8, _replacement.Utf8);

    [Benchmark(Description = "Text.ReplacePooled UTF-8")]
    public void TextReplacePooled()
    {
        using var result = _source.Utf8.ReplacePooled(_marker.Utf8, _replacement.Utf8);
    }
}
