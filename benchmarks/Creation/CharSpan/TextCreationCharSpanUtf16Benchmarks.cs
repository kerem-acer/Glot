using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationCharSpanUtf16Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
    }

    [Benchmark(Baseline = true, Description = "new string(span)")]
    public string StringFromCharSpan() => new(_source.Str.AsSpan());

    [Benchmark(Description = "Text.FromChars(span)")]
    public Text TextFromCharsSpan() => Text.FromChars(_source.Str.AsSpan());

    [Benchmark(Description = "Text.FromChars(span) no-count")]
    public Text TextFromCharsSpanNoCount() => Text.FromChars(_source.Str.AsSpan(), countRunes: false);

    [Benchmark(Description = "OwnedText.FromChars(span)")]
    public void OwnedTextFromChars()
    {
        using var owned = OwnedText.FromChars(_source.Str.AsSpan());
    }

    [Benchmark(Description = "OwnedText.FromChars(span) no-count")]
    public void OwnedTextFromCharsNoCount()
    {
        using var owned = OwnedText.FromChars(_source.Str.AsSpan(), countRunes: false);
    }
}
