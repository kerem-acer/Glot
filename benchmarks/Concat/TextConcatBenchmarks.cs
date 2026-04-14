using System.Text;
using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextConcatBenchmarks
{
    [PartSizeParams]
    public int PartSize;
    [Params(2, 4, 16, 64, 256)]
    public int Parts;
    [ScriptParams]
    public Script Locale;

    string[] _strings = null!;
    Text[] _texts = null!;
    U8String[] _u8Strings = null!;

    [GlobalSetup]
    public void Setup()
    {
        var full = TestData.Generate(PartSize * Parts, Locale);

        _strings = new string[Parts];
        _texts = new Text[Parts];
        _u8Strings = new U8String[Parts];
        for (var i = 0; i < Parts; i++)
        {
            _strings[i] = full[(i * PartSize)..((i + 1) * PartSize)];
            var bytes = Encoding.UTF8.GetBytes(_strings[i]);
            _texts[i] = Text.FromUtf8(bytes);
            _u8Strings[i] = new U8String(bytes);
        }
    }

    [Benchmark(Baseline = true, Description = "string.Concat")]
    public string StringConcat() => string.Concat(_strings);

    [Benchmark(Description = "Text.Concat")]
    public Text TextConcat() => Text.Concat(_texts);

    [Benchmark(Description = "Text.ConcatPooled")]
    public void TextConcatPooled()
    {
        using var result = Text.ConcatPooled(_texts);
    }

    [Benchmark(Description = "U8String.Concat")]
    public U8String U8Concat() => U8String.Concat(_u8Strings);
}
