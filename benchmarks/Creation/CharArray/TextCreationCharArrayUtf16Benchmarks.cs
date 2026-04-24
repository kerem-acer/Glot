using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationCharArrayUtf16Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    char[] _chars = null!;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _chars = _source.Str.ToCharArray();
    }

    [Benchmark(Baseline = true, Description = "new string(char[])")]
    public string StringFromChars() => new(_chars);

    [Benchmark(Description = "Text.FromChars(char[])")]
    public Text TextFromCharsArray() => Text.FromChars(_chars);

    [Benchmark(Description = "Text.FromChars(char[]) no-count")]
    public Text TextFromCharsArrayNoCount() => Text.FromChars(_chars, countRunes: false);

    [Benchmark(Description = "OwnedText.FromChars(span)")]
    public void OwnedTextFromChars()
    {
        using var owned = OwnedText.FromChars(_chars.AsSpan());
    }

    [Benchmark(Description = "OwnedText.FromChars(span) no-count")]
    public void OwnedTextFromCharsNoCount()
    {
        using var owned = OwnedText.FromChars(_chars.AsSpan(), countRunes: false);
    }
}
