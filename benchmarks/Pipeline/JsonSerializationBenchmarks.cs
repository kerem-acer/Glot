using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Glot.SystemTextJson;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class JsonSerializationBenchmarks
{
    static readonly JsonSerializerOptions GlotOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new TextJsonConverter() }
    };

    static readonly JsonSerializerOptions StringOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [EqualitySizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    byte[] _jsonBytes = null!;
    GlotEvent _glotEvent = null!;
    StringEvent _stringEvent = null!;
    string _needle = null!;

    [GlobalSetup]
    public void Setup()
    {
        var source = TestData.Generate(Math.Max(4, N / 8), Locale);
        var level = TestData.Generate(Math.Max(4, N / 16), Locale);
        var message = TestData.Generate(N, Locale);
        var tags = TestData.GenerateCsv(Math.Max(8, N / 4), Locale);
        _needle = TestData.Needle(Locale);

        _jsonBytes = JsonSerializer.SerializeToUtf8Bytes(
            new StringEvent(source, level, message, tags), StringOptions);
        _glotEvent = JsonSerializer.Deserialize<GlotEvent>(_jsonBytes, GlotOptions)!;
        _stringEvent = JsonSerializer.Deserialize<StringEvent>(_jsonBytes, StringOptions)!;
    }

    // --- Deserialize ---

    [BenchmarkCategory("Deserialize"), Benchmark(Baseline = true, Description = "Deserialize to string")]
    public StringEvent Deserialize_String()
        => JsonSerializer.Deserialize<StringEvent>(_jsonBytes, StringOptions)!;

    [BenchmarkCategory("Deserialize"), Benchmark(Description = "Deserialize to Text")]
    public GlotEvent Deserialize_Text()
        => JsonSerializer.Deserialize<GlotEvent>(_jsonBytes, GlotOptions)!;

    // --- Serialize ---

    [BenchmarkCategory("Serialize"), Benchmark(Baseline = true, Description = "Serialize string to bytes")]
    public byte[] Serialize_String()
        => JsonSerializer.SerializeToUtf8Bytes(_stringEvent, StringOptions);

    [BenchmarkCategory("Serialize"), Benchmark(Description = "Serialize Text to bytes")]
    public byte[] Serialize_Text()
        => JsonSerializer.SerializeToUtf8Bytes(_glotEvent, GlotOptions);

    [BenchmarkCategory("Serialize"), Benchmark(Description = "SerializeToUtf8OwnedText")]
    public void Serialize_OwnedText()
    {
        using var result = JsonSerializer.SerializeToUtf8OwnedText(_glotEvent, GlotOptions);
    }

    // --- RoundTrip ---

    [BenchmarkCategory("RoundTrip"), Benchmark(Baseline = true, Description = "Round trip: string")]
    public byte[] RoundTrip_String()
    {
        var evt = JsonSerializer.Deserialize<StringEvent>(_jsonBytes, StringOptions)!;
        _ = evt.Message.Contains(_needle, StringComparison.Ordinal);
        return JsonSerializer.SerializeToUtf8Bytes(evt, StringOptions);
    }

    [BenchmarkCategory("RoundTrip"), Benchmark(Description = "Round trip: Text")]
    public byte[] RoundTrip_Text()
    {
        var evt = JsonSerializer.Deserialize<GlotEvent>(_jsonBytes, GlotOptions)!;
        _ = evt.Message.Contains(_needle);
        return JsonSerializer.SerializeToUtf8Bytes(evt, GlotOptions);
    }
}
