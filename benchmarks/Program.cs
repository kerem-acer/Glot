using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance
    .WithArtifactsPath("benchmarks/artifacts");
var bdnArgs = new List<string>();

foreach (var arg in args)
{
    if (arg == "--no-overhead")
    {
        config = config.AddJob(Job.Default
            .WithEvaluateOverhead(false)
            .AsMutator());
    }
    else if (arg == "--loose")
    {
        config = config.AddJob(Job.Default
            .WithMaxRelativeError(0.1)
            .AsMutator());
    }
    else if (arg.StartsWith("--param:", StringComparison.Ordinal))
    {
        var kv = arg["--param:".Length..];
        var eqIdx = kv.IndexOf('=');
        if (eqIdx > 0)
        {
            var paramName = kv[..eqIdx];
            var values = kv[(eqIdx + 1)..].Split(',').ToHashSet(StringComparer.OrdinalIgnoreCase);
            config = config.AddFilter(new SimpleFilter(b =>
                b.Parameters.Items
                    .Where(p => p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase))
                    .All(p => values.Contains(p.Value?.ToString() ?? ""))));
        }
    }
    else
    {
        bdnArgs.Add(arg);
    }
}

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(bdnArgs.ToArray(), config);
