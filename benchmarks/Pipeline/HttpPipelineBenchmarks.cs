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
    EncodedSet _needle;
    EncodedSet _marker;
    EncodedSet _replacement;

    [GlobalSetup]
    public void Setup()
    {
        _needle = EncodedSet.From(TestData.Needle(Locale));
        var (markerStr, replacementStr) = TestData.MarkerPair(Locale);
        _marker = EncodedSet.From(markerStr);
        _replacement = EncodedSet.From(replacementStr);

        var msg = TestData.Generate(N, Locale);
        _jsonPayload = JsonSerializer.SerializeToUtf8Bytes(new
        {
            source = "stripe.payment",
            level = "error",
            message = msg + markerStr + msg,
            tags = TestData.GenerateCsv(Math.Max(32, N / 4), Locale)
        });
    }

    [Benchmark(Baseline = true, Description = "String pipeline")]
    public byte[] StringPipeline()
    {
        var evt = JsonSerializer.Deserialize<StringEvent>(_jsonPayload, StringOptions)!;
        _ = evt.Message.Contains(_needle.Str, StringComparison.Ordinal);
        foreach (var tag in evt.Tags.Split(','))
        {
            if (tag.Trim().Length > 0)
            {
                break;
            }
        }
        _ = evt.Message.Replace(_marker.Str, _replacement.Str);
        return JsonSerializer.SerializeToUtf8Bytes(evt, StringOptions);
    }

    [Benchmark(Description = "Glot pipeline")]
    public byte[] GlotPipeline()
    {
        var evt = JsonSerializer.Deserialize<GlotEvent>(_jsonPayload, GlotOptions)!;
        _ = evt.Message.Contains(_needle.Str);
        foreach (var tag in evt.Tags.Split(","))
        {
            if (!tag.Trim().IsEmpty)
            {
                break;
            }
        }
        using var sanitized = evt.Message.ReplacePooled(_marker.Str, _replacement.Str);
        return JsonSerializer.SerializeToUtf8Bytes(evt, GlotOptions);
    }

    [Benchmark(Description = "Glot pooled pipeline")]
    public void GlotPipeline_Pooled()
    {
        var evt = JsonSerializer.Deserialize<GlotEvent>(_jsonPayload, GlotOptions)!;
        _ = evt.Message.Contains(_needle.Str);
        using var sanitized = evt.Message.ReplacePooled(_marker.Str, _replacement.Str);
        using var json = JsonSerializer.SerializeToUtf8OwnedText(evt, GlotOptions);
    }
}
