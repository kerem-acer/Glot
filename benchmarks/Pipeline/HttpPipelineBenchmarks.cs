using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Glot.SystemTextJson;

namespace Glot.Benchmarks;

[MemoryDiagnoser]
public class HttpPipelineBenchmarks
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

    [SearchSizeParams]
    public int N;
    [ScriptParams]
    public Script Locale;

    byte[] _jsonPayload = null!;
    string _needleStr = null!;
    string _markerStr = null!;
    string _replacementStr = null!;

    [GlobalSetup]
    public void Setup()
    {
        _needleStr = TestData.Needle(Locale);
        (_markerStr, _replacementStr) = TestData.MarkerPair(Locale);

        var msg = TestData.Generate(N, Locale);
        _jsonPayload = JsonSerializer.SerializeToUtf8Bytes(new
        {
            source = "stripe.payment",
            level = "error",
            message = msg + _markerStr + msg,
            tags = TestData.GenerateCsv(Math.Max(32, N / 4), Locale)
        });
    }

    [Benchmark(Baseline = true, Description = "String pipeline")]
    public byte[] StringPipeline()
    {
        var evt = JsonSerializer.Deserialize<StringEvent>(_jsonPayload, StringOptions)!;

        _ = evt.Message.Contains(_needleStr, StringComparison.Ordinal);

        foreach (var tag in evt.Tags.Split(','))
        {
            if (tag.Trim().Length > 0)
            {
                break;
            }
        }

        _ = evt.Message.Replace(_markerStr, _replacementStr);
        return JsonSerializer.SerializeToUtf8Bytes(evt, StringOptions);
    }

    [Benchmark(Description = "Glot pipeline")]
    public byte[] GlotPipeline()
    {
        var evt = JsonSerializer.Deserialize<GlotEvent>(_jsonPayload, GlotOptions)!;

        _ = evt.Message.Contains(_needleStr);

        foreach (var tag in evt.Tags.Split(","))
        {
            if (!tag.Trim().IsEmpty)
            {
                break;
            }
        }

        using var sanitized = evt.Message.ReplacePooled(_markerStr, _replacementStr);
        return JsonSerializer.SerializeToUtf8Bytes(evt, GlotOptions);
    }

    [Benchmark(Description = "Glot pooled pipeline")]
    public void GlotPipeline_Pooled()
    {
        var evt = JsonSerializer.Deserialize<GlotEvent>(_jsonPayload, GlotOptions)!;

        _ = evt.Message.Contains(_needleStr);

        using var sanitized = evt.Message.ReplacePooled(_markerStr, _replacementStr);
        using var json = JsonSerializer.SerializeToUtf8OwnedText(evt, GlotOptions);
    }

}
