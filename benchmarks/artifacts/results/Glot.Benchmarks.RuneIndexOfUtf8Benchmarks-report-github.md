```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
| Method                         | N     | Locale | Mean        | Error | Ratio | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |------------:|------:|------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |    **91.21 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |   159.83 μs |    NA |  1.75 |         - |          NA |
| U8String.IndexOf               | 64    | Ascii  |   280.62 μs |    NA |  3.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Ascii  |   288.42 μs |    NA |  3.16 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Ascii  |   725.96 μs |    NA |  7.96 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Ascii  | 1,127.08 μs |    NA | 12.36 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |   135.00 μs |    NA |  1.48 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |   119.62 μs |    NA |  1.31 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 64    | Ascii  |   256.12 μs |    NA |  2.81 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |   248.00 μs |    NA |  2.72 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |   834.29 μs |    NA |  9.15 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |   754.67 μs |    NA |  8.27 |         - |          NA |
|                                |       |        |             |       |       |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |   **116.54 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |   118.17 μs |    NA |  1.01 |         - |          NA |
| U8String.IndexOf               | 4096  | Ascii  |   252.96 μs |    NA |  2.17 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Ascii  |   206.83 μs |    NA |  1.77 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Ascii  | 1,984.96 μs |    NA | 17.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Ascii  |   761.25 μs |    NA |  6.53 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   105.54 μs |    NA |  0.91 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   187.12 μs |    NA |  1.61 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096  | Ascii  |   240.79 μs |    NA |  2.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   265.42 μs |    NA |  2.28 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   843.71 μs |    NA |  7.24 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   786.17 μs |    NA |  6.75 |         - |          NA |
|                                |       |        |             |       |       |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |    **94.21 μs** |    **NA** |  **1.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |   154.29 μs |    NA |  1.64 |         - |          NA |
| U8String.IndexOf               | 65536 | Ascii  |   218.17 μs |    NA |  2.32 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Ascii  |   268.96 μs |    NA |  2.85 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Ascii  |   677.29 μs |    NA |  7.19 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Ascii  |   730.00 μs |    NA |  7.75 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  |   120.12 μs |    NA |  1.28 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  |   164.58 μs |    NA |  1.75 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 65536 | Ascii  |   216.71 μs |    NA |  2.30 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  |   181.17 μs |    NA |  1.92 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  |   678.00 μs |    NA |  7.20 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  |   687.50 μs |    NA |  7.30 |         - |          NA |
