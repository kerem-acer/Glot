using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationStringUtf16Benchmarks
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

    [Benchmark(Baseline = true, Description = "new string(source)")]
    public string StringBaseline() => new(_source.Str);

    [Benchmark(Description = "Text.From(string)")]
    public Text TextFromString() => Text.From(_source.Str);

    [Benchmark(Description = "Text.From(string) no-count")]
    public Text TextFromStringNoCount() => Text.From(_source.Str, countRunes: false);

    [Benchmark(Description = "Text.FromAscii(string)")]
    public Text TextFromAsciiString() => Text.FromAscii(_source.Str);

    [Benchmark(Description = "new U8String(string)")]
    public U8String U8FromString() => new(_source.Str);

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
