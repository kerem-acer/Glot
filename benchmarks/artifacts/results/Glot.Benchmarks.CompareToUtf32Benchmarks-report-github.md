```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean       | Error       | StdDev     | Ratio  | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-----------:|------------:|-----------:|-------:|--------:|----------:|------------:|
| **string.Compare**                 | **65536** | **Ascii**  |   **3.464 μs** |   **1.4208 μs** |  **0.0779 μs** |   **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1.108 μs |   0.8482 μs |  0.0465 μs |   0.32 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  | 252.135 μs |  16.5090 μs |  0.9049 μs |  72.82 |    1.42 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  | 254.510 μs |  18.8720 μs |  1.0344 μs |  73.50 |    1.44 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  |   4.657 μs |   0.9083 μs |  0.0498 μs |   1.34 |    0.03 |         - |          NA |
|                                |       |        |            |             |            |        |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3.205 μs** |   **0.2126 μs** |  **0.0117 μs** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3.337 μs |   0.1900 μs |  0.0104 μs |   1.04 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    | 283.919 μs |  74.2599 μs |  4.0704 μs |  88.59 |    1.13 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    | 320.993 μs |  71.9163 μs |  3.9420 μs | 100.16 |    1.11 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    |   4.820 μs |   0.3259 μs |  0.0179 μs |   1.50 |    0.01 |         - |          NA |
|                                |       |        |            |             |            |        |         |           |             |
| **string.Compare**                 | **65536** | **Emoji**  |   **3.360 μs** |   **1.0782 μs** |  **0.0591 μs** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Emoji  |   2.390 μs |   0.1522 μs |  0.0083 μs |   0.71 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Emoji  | 157.673 μs | 247.0397 μs | 13.5411 μs |  46.94 |    3.56 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Emoji  | 191.967 μs |  34.8302 μs |  1.9092 μs |  57.14 |    0.99 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Emoji  |   2.427 μs |   0.1914 μs |  0.0105 μs |   0.72 |    0.01 |         - |          NA |
|                                |       |        |            |             |            |        |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3.335 μs** |   **0.1987 μs** |  **0.0109 μs** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1.607 μs |   0.0807 μs |  0.0044 μs |   0.48 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  | 259.890 μs |  35.5504 μs |  1.9486 μs |  77.92 |    0.55 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  | 295.586 μs |  52.9174 μs |  2.9006 μs |  88.63 |    0.79 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  |   4.253 μs |   0.2618 μs |  0.0144 μs |   1.28 |    0.01 |         - |          NA |
