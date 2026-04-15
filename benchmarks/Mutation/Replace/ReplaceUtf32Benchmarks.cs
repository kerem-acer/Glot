using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class ReplaceUtf32Benchmarks
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

    [Benchmark(Description = "Text.Replace UTF-32")]
    public Text TextReplace() => _source.Utf32.Replace(_marker.Utf32, _replacement.Utf32);

    [Benchmark(Description = "Text.ReplacePooled UTF-32")]
    public void TextReplacePooled()
    {
        using var result = _source.Utf32.ReplacePooled(_marker.Utf32, _replacement.Utf32);
    }
}
