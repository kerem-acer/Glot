using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextBuilderUtf32Benchmarks
{
    [PartSizeParams]
    public int PartSize;

    [Params(2, 4, 16, 64, 256)]
    public int Parts;

    [ScriptParams]
    public Script Locale;

    EncodedSet[] _parts = null!;
    Text[] _textsUtf8 = null!;
    Text[] _textsUtf16 = null!;
    Text[] _textsUtf32 = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _parts = new EncodedSet[Parts];
        _textsUtf8 = new Text[Parts];
        _textsUtf16 = new Text[Parts];
        _textsUtf32 = new Text[Parts];
        for (var i = 0; i < Parts; i++)
        {
            var p = EncodedSet.From(full[(i * PartSize)..((i + 1) * PartSize)]);
            _parts[i] = p;
            _textsUtf8[i] = p.Utf8;
            _textsUtf16[i] = p.Utf16;
            _textsUtf32[i] = p.Utf32;
        }
    }

    [Benchmark(Description = "TextBuilder UTF-32 -> ToText")]
    public Text TextBuilder_ToText()
    {
        var builder = new TextBuilder(TextEncoding.Utf32);
        try
        {
            foreach (var part in _textsUtf32)
            {
                builder.Append(part);
            }

            return builder.ToText();
        }
        finally
        {
            builder.Dispose();
        }
    }

    [Benchmark(Description = "TextBuilder UTF-32 -> ToOwnedText")]
    public void TextBuilder_ToOwnedText()
    {
        var builder = new TextBuilder(TextEncoding.Utf32);
        try
        {
            foreach (var part in _textsUtf32)
            {
                builder.Append(part);
            }

            using var owned = builder.ToOwnedText();
        }
        finally
        {
            builder.Dispose();
        }
    }

    [Benchmark(Description = "TextBuilder UTF-8→UTF-32")]
    public Text TextBuilder_CrossUtf8()
    {
        var builder = new TextBuilder(TextEncoding.Utf32);
        try
        {
            foreach (var part in _textsUtf8)
            {
                builder.Append(part);
            }

            return builder.ToText();
        }
        finally
        {
            builder.Dispose();
        }
    }

    [Benchmark(Description = "TextBuilder UTF-16→UTF-32")]
    public Text TextBuilder_CrossUtf16()
    {
        var builder = new TextBuilder(TextEncoding.Utf32);
        try
        {
            foreach (var part in _textsUtf16)
            {
                builder.Append(part);
            }

            return builder.ToText();
        }
        finally
        {
            builder.Dispose();
        }
    }
}
