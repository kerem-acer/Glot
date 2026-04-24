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
| **string.Compare**                 | **65536** | **Ascii**  |   **3.430 μs** |  **0.6573 μs** | **0.0360 μs** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1.036 μs |  0.1046 μs | 0.0057 μs |  0.30 |    0.00 |         - |          NA |
| U8String.CompareTo             | 65536 | Ascii  |   1.045 μs |  0.2525 μs | 0.0138 μs |  0.30 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  |   1.034 μs |  0.1916 μs | 0.0105 μs |  0.30 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  |   3.264 μs |  0.0628 μs | 0.0034 μs |  0.95 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  | 246.363 μs |  9.5209 μs | 0.5219 μs | 71.83 |    0.66 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3.186 μs** |  **0.6718 μs** | **0.0368 μs** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3.313 μs |  0.0960 μs | 0.0053 μs |  1.04 |    0.01 |         - |          NA |
| U8String.CompareTo             | 65536 | Cjk    |   3.316 μs |  0.3183 μs | 0.0174 μs |  1.04 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    |   3.324 μs |  0.5684 μs | 0.0312 μs |  1.04 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    |  40.921 μs | 27.3386 μs | 1.4985 μs | 12.84 |    0.43 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    | 278.856 μs | 30.4172 μs | 1.6673 μs | 87.53 |    0.98 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Emoji**  |   **3.340 μs** |  **2.3589 μs** | **0.1293 μs** |  **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Emoji  |   2.336 μs |  0.1122 μs | 0.0061 μs |  0.70 |    0.02 |         - |          NA |
| U8String.CompareTo             | 65536 | Emoji  |   2.499 μs |  5.4880 μs | 0.3008 μs |  0.75 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Emoji  |   2.362 μs |  0.2484 μs | 0.0136 μs |  0.71 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Emoji  |  48.023 μs | 16.5059 μs | 0.9047 μs | 14.39 |    0.53 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Emoji  | 145.737 μs |  3.2329 μs | 0.1772 μs | 43.67 |    1.43 |         - |          NA |
|                                |       |        |            |            |           |       |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3.231 μs** |  **0.1535 μs** | **0.0084 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1.606 μs |  0.1388 μs | 0.0076 μs |  0.50 |    0.00 |         - |          NA |
| U8String.CompareTo             | 65536 | Mixed  |   1.500 μs |  0.1064 μs | 0.0058 μs |  0.46 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  |   1.637 μs |  0.4754 μs | 0.0261 μs |  0.51 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  |  22.178 μs |  3.1616 μs | 0.1733 μs |  6.86 |    0.05 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  | 268.229 μs | 63.7112 μs | 3.4922 μs | 83.01 |    0.95 |         - |          NA |
