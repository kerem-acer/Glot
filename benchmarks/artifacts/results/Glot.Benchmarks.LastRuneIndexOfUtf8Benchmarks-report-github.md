```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
| Method                             | N     | Locale | Mean     | Error | Ratio | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |---------:|------:|------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  | **155.1 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  | 147.8 μs |    NA |  0.95 |         - |          NA |
| U8String.LastIndexOf               | 64    | Ascii  | 236.6 μs |    NA |  1.53 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  | 278.7 μs |    NA |  1.80 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  | 768.3 μs |    NA |  4.95 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  | 899.7 μs |    NA |  5.80 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  | 171.2 μs |    NA |  1.10 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  | 140.4 μs |    NA |  0.91 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Ascii  | 218.9 μs |    NA |  1.41 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  | 284.8 μs |    NA |  1.84 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  | 756.3 μs |    NA |  4.88 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  | 910.1 μs |    NA |  5.87 |         - |          NA |
|                                    |       |        |          |       |       |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  | **143.6 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  | 168.7 μs |    NA |  1.17 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Ascii  | 271.7 μs |    NA |  1.89 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  | 261.0 μs |    NA |  1.82 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  | 724.1 μs |    NA |  5.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  | 786.2 μs |    NA |  5.47 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  | 144.8 μs |    NA |  1.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  | 166.2 μs |    NA |  1.16 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Ascii  | 240.8 μs |    NA |  1.68 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  | 228.1 μs |    NA |  1.59 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  | 752.3 μs |    NA |  5.24 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  | 880.6 μs |    NA |  6.13 |         - |          NA |
|                                    |       |        |          |       |       |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  | **159.0 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  | 119.7 μs |    NA |  0.75 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Ascii  | 252.2 μs |    NA |  1.59 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  | 290.1 μs |    NA |  1.83 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  | 781.4 μs |    NA |  4.92 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  | 975.5 μs |    NA |  6.14 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 183.5 μs |    NA |  1.15 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 158.9 μs |    NA |  1.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Ascii  | 245.0 μs |    NA |  1.54 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 345.7 μs |    NA |  2.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 741.6 μs |    NA |  4.67 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 837.1 μs |    NA |  5.27 |         - |          NA |
