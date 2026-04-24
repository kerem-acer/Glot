using System.Collections.Immutable;
using System.Text;
using BenchmarkDotNet.Attributes;
using U8;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class TextCreationImmutableArrayUtf8Benchmarks
{
    [EqualitySizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _source;
    ImmutableArray<byte> _immutableUtf8;

    [GlobalSetup]
    public void Setup()
    {
        _source = EncodedSet.From(TestData.Generate(N, Locale));
        _immutableUtf8 = [.._source.RawBytes];
    }

    [Benchmark(Baseline = true, Description = "Encoding.GetString")]
    public string EncodingGetString() => Encoding.UTF8.GetString(_source.RawBytes);

    [Benchmark(Description = "Text.FromUtf8(ImmutableArray)")]
    public Text TextFromImmutable() => Text.FromUtf8(_immutableUtf8);

    [Benchmark(Description = "Text.FromUtf8(ImmutableArray) no-count")]
    public Text TextFromImmutableNoCount() => Text.FromUtf8(_immutableUtf8, countRunes: false);

    [Benchmark(Description = "new U8String(ImmutableArray)")]
    public U8String U8FromImmutable() => new(_immutableUtf8);
}
