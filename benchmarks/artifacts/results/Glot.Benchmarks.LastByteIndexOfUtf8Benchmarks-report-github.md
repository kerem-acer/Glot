```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]    : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  MediumRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
| Method                            | N   | Locale | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------------- |---- |------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| string.LastIndexOf                | 256 | Ascii  |  2.648 ns | 0.0115 ns | 0.0165 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8&#39;          | 256 | Ascii  |  1.458 ns | 0.0047 ns | 0.0064 ns |  0.55 |    0.00 |         - |          NA |
| U8String.LastIndexOf              | 256 | Ascii  |  1.651 ns | 0.0054 ns | 0.0078 ns |  0.62 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;      | 256 | Ascii  |  2.497 ns | 0.0112 ns | 0.0164 ns |  0.94 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;         | 256 | Ascii  | 16.900 ns | 0.0714 ns | 0.1047 ns |  6.38 |    0.05 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;     | 256 | Ascii  |  8.317 ns | 0.0346 ns | 0.0507 ns |  3.14 |    0.03 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;       | 256 | Ascii  |  8.434 ns | 0.0701 ns | 0.0983 ns |  3.18 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39; | 256 | Ascii  |  8.859 ns | 0.0352 ns | 0.0505 ns |  3.35 |    0.03 |         - |          NA |
