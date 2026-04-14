using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextConcatUtf8Benchmarks
{
    [PartSizeParams]
    public int PartSize;

    [Params(2, 4, 16, 64, 256)]
    public int Parts;

    [ScriptParams]
    public Script Locale;

    EncodedSet[] _parts = null!;
    byte[][] _byteArrays = null!;
    Text[] _textsUtf8 = null!;
    Text[] _textsUtf16 = null!;
    Text[] _textsUtf32 = null!;
    U8String[] _u8Strings = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _parts = new EncodedSet[Parts];
        _byteArrays = new byte[Parts][];
        _textsUtf8 = new Text[Parts];
        _textsUtf16 = new Text[Parts];
        _textsUtf32 = new Text[Parts];
        _u8Strings = new U8String[Parts];
        for (var i = 0; i < Parts; i++)
        {
            var p = EncodedSet.From(full[(i * PartSize)..((i + 1) * PartSize)]);
            _parts[i] = p;
            _byteArrays[i] = p.RawBytes;
            _textsUtf8[i] = p.Utf8;
            _textsUtf16[i] = p.Utf16;
            _textsUtf32[i] = p.Utf32;
            _u8Strings[i] = p.U8;
        }
    }

    [Benchmark(Baseline = true, Description = "byte[] concat UTF-8")]
    public byte[] RawUtf8Concat()
    {
        var totalLen = 0;
        foreach (var b in _byteArrays)
        {
            totalLen += b.Length;
        }

        var result = new byte[totalLen];
        var offset = 0;
        foreach (var b in _byteArrays)
        {
            b.CopyTo(result, offset);
            offset += b.Length;
        }

        return result;
    }

    [Benchmark(Description = "U8String.Concat")]
    public U8String U8Concat() => U8String.Concat(_u8Strings);

    [Benchmark(Description = "Text.Concat UTF-8")]
    public Text TextConcat() => Text.Concat(_textsUtf8);

    [Benchmark(Description = "Text.Concat UTF-16→UTF-8")]
    public Text TextConcat_Utf16() => Text.Concat(_textsUtf16, TextEncoding.Utf8);

    [Benchmark(Description = "Text.Concat UTF-32→UTF-8")]
    public Text TextConcat_Utf32() => Text.Concat(_textsUtf32, TextEncoding.Utf8);

    [Benchmark(Description = "Text.ConcatPooled UTF-8")]
    public void TextConcatPooled()
    {
        using var result = Text.ConcatPooled(_textsUtf8);
    }

    [Benchmark(Description = "Text.ConcatPooled UTF-16→UTF-8")]
    public void TextConcatPooled_Utf16()
    {
        using var result = Text.ConcatPooled(_textsUtf16, TextEncoding.Utf8);
    }

    [Benchmark(Description = "Text.ConcatPooled UTF-32→UTF-8")]
    public void TextConcatPooled_Utf32()
    {
        using var result = Text.ConcatPooled(_textsUtf32, TextEncoding.Utf8);
    }

    [Benchmark(Description = "LinkedTextUtf8.Create")]
    public LinkedTextUtf8 LinkedUtf8Create() => LinkedTextUtf8.Create(_textsUtf8.AsSpan());

    [Benchmark(Description = "OwnedLinkedTextUtf8.Create")]
    public void OwnedLinkedUtf8()
    {
        using var linked = OwnedLinkedTextUtf8.Create(_textsUtf8.AsSpan());
    }
}
