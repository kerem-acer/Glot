using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextConcatUtf16Benchmarks
{
    [PartSizeParams]
    public int PartSize;

    [Params(2, 4, 16, 64, 256)]
    public int Parts;

    [ScriptParams]
    public Script Locale;

    EncodedSet[] _parts = null!;
    string[] _strings = null!;
    Text[] _textsUtf8 = null!;
    Text[] _textsUtf16 = null!;
    Text[] _textsUtf32 = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _parts = new EncodedSet[Parts];
        _strings = new string[Parts];
        _textsUtf8 = new Text[Parts];
        _textsUtf16 = new Text[Parts];
        _textsUtf32 = new Text[Parts];
        for (var i = 0; i < Parts; i++)
        {
            var p = EncodedSet.From(full[(i * PartSize)..((i + 1) * PartSize)]);
            _parts[i] = p;
            _strings[i] = p.Str;
            _textsUtf8[i] = p.Utf8;
            _textsUtf16[i] = p.Utf16;
            _textsUtf32[i] = p.Utf32;
        }
    }

    [Benchmark(Baseline = true, Description = "string.Concat")]
    public string StringConcat() => string.Concat(_strings);

    [Benchmark(Description = "Text.Concat UTF-16")]
    public Text TextConcat() => Text.Concat(_textsUtf16);

    [Benchmark(Description = "Text.Concat UTF-8→UTF-16")]
    public Text TextConcat_Utf8() => Text.Concat(_textsUtf8, TextEncoding.Utf16);

    [Benchmark(Description = "Text.Concat UTF-32→UTF-16")]
    public Text TextConcat_Utf32() => Text.Concat(_textsUtf32, TextEncoding.Utf16);

    [Benchmark(Description = "Text.ConcatPooled UTF-16")]
    public void TextConcatPooled()
    {
        using var result = Text.ConcatPooled(_textsUtf16);
    }

    [Benchmark(Description = "Text.ConcatPooled UTF-8→UTF-16")]
    public void TextConcatPooled_Utf8()
    {
        using var result = Text.ConcatPooled(_textsUtf8, TextEncoding.Utf16);
    }

    [Benchmark(Description = "Text.ConcatPooled UTF-32→UTF-16")]
    public void TextConcatPooled_Utf32()
    {
        using var result = Text.ConcatPooled(_textsUtf32, TextEncoding.Utf16);
    }

    [Benchmark(Description = "LinkedTextUtf16.Create")]
    public LinkedTextUtf16 LinkedUtf16Create() => LinkedTextUtf16.Create(_textsUtf16.AsSpan());

    [Benchmark(Description = "OwnedLinkedTextUtf16.Create")]
    public void OwnedLinkedUtf16()
    {
        using var linked = OwnedLinkedTextUtf16.Create(_textsUtf16.AsSpan());
    }
}
