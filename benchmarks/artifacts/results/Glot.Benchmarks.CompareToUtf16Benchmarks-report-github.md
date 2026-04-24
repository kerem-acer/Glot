```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean       | Error      | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-----------:|-----------:|----------:|------:|--------:|----------:|------------:|
| **string.Compare**                 | **65536** | **Ascii**  |   **3.421 μs** |  **0.5781 μs** | **0.0317 μs** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1.084 μs |  0.2822 μs | 0.0155 μs |  0.32 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  |   3.335 μs |  1.7111 μs | 0.0938 μs |  0.98 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  |   2.504 μs |  0.1987 μs | 0.0109 μs |  0.73 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  | 261.966 μs | 87.0521 μs | 4.7716 μs | 76.59 |    1.36 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3.174 μs** |  **0.4151 μs** | **0.0228 μs** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3.368 μs |  0.2539 μs | 0.0139 μs |  1.06 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    |  39.272 μs |  2.1635 μs | 0.1186 μs | 12.37 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    |   2.412 μs |  0.1033 μs | 0.0057 μs |  0.76 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    | 310.760 μs | 13.0250 μs | 0.7139 μs | 97.90 |    0.64 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Emoji**  |   **3.228 μs** |  **0.6265 μs** | **0.0343 μs** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Emoji  |   2.342 μs |  0.4703 μs | 0.0258 μs |  0.73 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Emoji  |  45.250 μs | 48.7995 μs | 2.6749 μs | 14.02 |    0.73 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Emoji  |   2.470 μs |  0.3201 μs | 0.0175 μs |  0.77 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Emoji  | 189.031 μs | 19.0716 μs | 1.0454 μs | 58.57 |    0.61 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3.264 μs** |  **0.1777 μs** | **0.0097 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1.590 μs |  0.0499 μs | 0.0027 μs |  0.49 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  |  21.806 μs |  1.4309 μs | 0.0784 μs |  6.68 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  |   2.383 μs |  0.0278 μs | 0.0015 μs |  0.73 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  | 281.232 μs | 51.7011 μs | 2.8339 μs | 86.17 |    0.78 |         - |          NA |
