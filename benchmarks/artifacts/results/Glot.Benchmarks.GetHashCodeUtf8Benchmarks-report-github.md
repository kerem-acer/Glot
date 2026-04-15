```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host] : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  Dry    : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
| Method                    | N | Locale | Mean       | Error | Ratio | Allocated | Alloc Ratio |
|-------------------------- |-- |------- |-----------:|------:|------:|----------:|------------:|
| string.GetHashCode        | 8 | Ascii  |   117.6 μs |    NA |  1.00 |         - |          NA |
| &#39;HashCode.AddBytes UTF-8&#39; | 8 | Ascii  |   137.6 μs |    NA |  1.17 |         - |          NA |
| U8String.GetHashCode      | 8 | Ascii  | 1,593.9 μs |    NA | 13.56 |         - |          NA |
| Text.GetHashCode          | 8 | Ascii  |   475.3 μs |    NA |  4.04 |         - |          NA |
