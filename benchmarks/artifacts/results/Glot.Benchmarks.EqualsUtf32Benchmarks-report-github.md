```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean            | Error           | StdDev         | Ratio   | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |----------------:|----------------:|---------------:|--------:|--------:|----------:|------------:|
| **string.Equals**                        | **65536** | **Ascii**  |   **2,451.2437 ns** |     **205.8372 ns** |     **11.2826 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Ascii  |   1,061.3757 ns |     149.1703 ns |      8.1765 ns |    0.43 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Ascii  | 282,431.3691 ns |  43,094.0419 ns |  2,362.1299 ns |  115.22 |    0.95 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Ascii  | 367,442.4097 ns |  19,548.8743 ns |  1,071.5398 ns |  149.90 |    0.71 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Ascii  |   4,345.1960 ns |     635.8462 ns |     34.8529 ns |    1.77 |    0.01 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Ascii  |   2,585.2293 ns |     811.3516 ns |     44.4729 ns |    1.05 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Ascii  |   1,021.1638 ns |     195.6926 ns |     10.7266 ns |    0.42 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Ascii  | 279,227.7899 ns |  16,286.6232 ns |    892.7248 ns |  113.91 |    0.55 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Ascii  | 368,965.1082 ns |  64,645.4277 ns |  3,543.4340 ns |  150.52 |    1.39 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Ascii  |   4,595.9025 ns |      16.3592 ns |      0.8967 ns |    1.87 |    0.01 |         - |          NA |
|                                      |       |        |                 |                 |                |         |         |           |             |
| **string.Equals**                        | **65536** | **Cjk**    |   **2,424.7529 ns** |     **181.6780 ns** |      **9.9584 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Cjk    |   3,440.6559 ns |      89.5096 ns |      4.9063 ns |   1.419 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Cjk    | 325,309.5366 ns | 237,658.7021 ns | 13,026.8755 ns | 134.163 |    4.68 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Cjk    | 368,673.2992 ns |  48,789.6472 ns |  2,674.3252 ns | 152.047 |    1.10 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Cjk    |   4,573.8181 ns |     186.8553 ns |     10.2422 ns |   1.886 |    0.01 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Cjk    |   2,438.5473 ns |      88.1499 ns |      4.8318 ns |   1.006 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Cjk    |       0.0000 ns |       0.0000 ns |      0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Cjk    | 319,176.9071 ns |  26,919.8412 ns |  1,475.5673 ns | 131.634 |    0.71 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Cjk    | 370,931.8511 ns |  72,090.5724 ns |  3,951.5275 ns | 152.979 |    1.51 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Cjk    |   4,555.9877 ns |     438.2769 ns |     24.0234 ns |   1.879 |    0.01 |         - |          NA |
|                                      |       |        |                 |                 |                |         |         |           |             |
| **string.Equals**                        | **65536** | **Emoji**  |   **2,451.7868 ns** |      **71.9067 ns** |      **3.9415 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Emoji  |   2,302.8030 ns |      78.0693 ns |      4.2792 ns |   0.939 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Emoji  | 167,072.8590 ns |  19,262.4417 ns |  1,055.8394 ns |  68.143 |    0.38 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Emoji  | 199,194.7428 ns |  10,202.8088 ns |    559.2504 ns |  81.245 |    0.23 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Emoji  |   2,329.0727 ns |      64.5543 ns |      3.5384 ns |   0.950 |    0.00 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Emoji  |   2,390.7339 ns |     281.8580 ns |     15.4496 ns |   0.975 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Emoji  |       0.0000 ns |       0.0000 ns |      0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Emoji  |       0.9974 ns |       0.4470 ns |      0.0245 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Emoji  |       1.0212 ns |       0.3159 ns |      0.0173 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Emoji  |       1.3901 ns |       0.1471 ns |      0.0081 ns |   0.001 |    0.00 |         - |          NA |
|                                      |       |        |                 |                 |                |         |         |           |             |
| **string.Equals**                        | **65536** | **Mixed**  |   **2,397.8393 ns** |     **299.2785 ns** |     **16.4045 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Mixed  |   1,664.5229 ns |   1,740.2689 ns |     95.3900 ns |    0.69 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Mixed  | 312,248.2166 ns |  29,947.9920 ns |  1,641.5505 ns |  130.22 |    0.98 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Mixed  | 382,916.2531 ns | 194,460.2464 ns | 10,659.0223 ns |  159.70 |    3.97 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Mixed  |   4,279.5675 ns |     275.0857 ns |     15.0784 ns |    1.78 |    0.01 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Mixed  |   2,452.6296 ns |      88.7758 ns |      4.8661 ns |    1.02 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Mixed  |   1,643.1931 ns |     250.5258 ns |     13.7322 ns |    0.69 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Mixed  | 325,391.4862 ns | 552,709.5867 ns | 30,295.8776 ns |  135.71 |   10.97 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Mixed  | 370,184.0820 ns |  21,124.8423 ns |  1,157.9239 ns |  154.39 |    1.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Mixed  |   4,279.5393 ns |     620.9112 ns |     34.0342 ns |    1.78 |    0.02 |         - |          NA |
