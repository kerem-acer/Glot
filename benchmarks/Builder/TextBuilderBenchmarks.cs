using System.Text;
using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextBuilderBenchmarks
{
    [PartSizeParams]
    public int PartSize;
    [Params(2, 4, 16, 64, 256)]
    public int Parts;
    [ScriptParams]
    public Script Locale;

    Text[] _textParts = null!;
    string[] _stringParts = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _stringParts = new string[Parts];
        _textParts = new Text[Parts];
        for (var i = 0; i < Parts; i++)
        {
            _stringParts[i] = full[(i * PartSize)..((i + 1) * PartSize)];
            _textParts[i] = Text.FromUtf8(Encoding.UTF8.GetBytes(_stringParts[i]));
        }
    }

    [Benchmark(Baseline = true, Description = "StringBuilder -> ToString")]
    public string StringBuilder_ToString()
    {
        var sb = new StringBuilder();
        foreach (var part in _stringParts)
        {
            sb.Append(part);
        }
        return sb.ToString();
    }

    [Benchmark(Description = "TextBuilder -> ToText")]
    public Text TextBuilder_ToText()
    {
        var builder = new TextBuilder(TextEncoding.Utf8);
        try
        {
            foreach (var part in _textParts)
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

    [Benchmark(Description = "TextBuilder -> ToOwnedText")]
    public void TextBuilder_ToOwnedText()
    {
        var builder = new TextBuilder(TextEncoding.Utf8);
        try
        {
            foreach (var part in _textParts)
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

    [Benchmark(Description = "TextBuilder cross-enc UTF-16->UTF-8")]
    public Text TextBuilder_CrossEncoding()
    {
        var builder = new TextBuilder(TextEncoding.Utf8);
        try
        {
            foreach (var part in _stringParts)
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
