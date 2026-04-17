```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean           | Error          | StdDev        | Median         | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |---------------:|---------------:|--------------:|---------------:|------:|--------:|----------:|------------:|
| **string.Compare**                 | **8**     | **Ascii**  |       **1.081 ns** |      **2.3158 ns** |     **0.1269 ns** |       **1.010 ns** |  **1.01** |    **0.14** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Ascii  |       2.991 ns |      5.9091 ns |     0.3239 ns |       2.891 ns |  2.79 |    0.37 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Ascii  |       2.592 ns |      0.8402 ns |     0.0461 ns |       2.568 ns |  2.42 |    0.23 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Ascii  |       8.543 ns |      0.8983 ns |     0.0492 ns |       8.517 ns |  7.97 |    0.76 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Ascii  |      26.105 ns |     24.0737 ns |     1.3196 ns |      25.419 ns | 24.35 |    2.55 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **8**     | **Cjk**    |       **1.018 ns** |      **0.1367 ns** |     **0.0075 ns** |       **1.017 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Cjk    |       4.783 ns |      0.5347 ns |     0.0293 ns |       4.777 ns |  4.70 |    0.04 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Cjk    |       6.480 ns |     30.3018 ns |     1.6609 ns |       5.553 ns |  6.37 |    1.41 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Cjk    |      15.736 ns |      9.4178 ns |     0.5162 ns |      15.981 ns | 15.46 |    0.45 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Cjk    |      40.180 ns |    235.1962 ns |    12.8919 ns |      33.859 ns | 39.49 |   10.97 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **8**     | **Mixed**  |       **1.550 ns** |     **16.8592 ns** |     **0.9241 ns** |       **1.039 ns** |  **1.21** |    **0.81** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Mixed  |       2.773 ns |      7.8745 ns |     0.4316 ns |       2.559 ns |  2.17 |    0.89 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Mixed  |       2.748 ns |      0.3442 ns |     0.0189 ns |       2.758 ns |  2.15 |    0.83 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Mixed  |       8.585 ns |      1.8860 ns |     0.1034 ns |       8.535 ns |  6.73 |    2.59 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Mixed  |      25.489 ns |      3.5444 ns |     0.1943 ns |      25.466 ns | 19.97 |    7.69 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **256**   | **Ascii**  |      **13.296 ns** |     **62.0076 ns** |     **3.3988 ns** |      **11.350 ns** |  **1.04** |    **0.31** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Ascii  |       8.702 ns |      0.6317 ns |     0.0346 ns |       8.713 ns |  0.68 |    0.13 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Ascii  |       9.284 ns |      0.7160 ns |     0.0392 ns |       9.273 ns |  0.73 |    0.14 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Ascii  |      29.398 ns |    123.3032 ns |     6.7587 ns |      25.541 ns |  2.30 |    0.64 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Ascii  |     599.803 ns |     24.1764 ns |     1.3252 ns |     599.919 ns | 46.89 |    9.05 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **256**   | **Cjk**    |      **13.296 ns** |     **60.1465 ns** |     **3.2968 ns** |      **11.433 ns** |  **1.04** |    **0.30** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Cjk    |      19.720 ns |     36.1383 ns |     1.9809 ns |      20.534 ns |  1.54 |    0.32 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Cjk    |      21.726 ns |    124.2649 ns |     6.8114 ns |      17.841 ns |  1.69 |    0.57 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Cjk    |     168.155 ns |      1.7454 ns |     0.0957 ns |     168.116 ns | 13.12 |    2.46 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Cjk    |     874.261 ns |  4,016.2696 ns |   220.1453 ns |     751.431 ns | 68.20 |   19.81 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **256**   | **Mixed**  |      **12.421 ns** |     **25.8902 ns** |     **1.4191 ns** |      **11.637 ns** |  **1.01** |    **0.14** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Mixed  |      10.069 ns |      3.8917 ns |     0.2133 ns |      10.041 ns |  0.82 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Mixed  |      12.336 ns |     64.8823 ns |     3.5564 ns |      10.344 ns |  1.00 |    0.27 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Mixed  |      97.969 ns |     33.7057 ns |     1.8475 ns |      97.036 ns |  7.95 |    0.75 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Mixed  |     732.246 ns |  3,758.0419 ns |   205.9910 ns |     619.246 ns | 59.44 |   15.55 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **65536** | **Ascii**  |   **3,677.570 ns** |  **8,494.3046 ns** |   **465.6015 ns** |   **3,416.883 ns** |  **1.01** |    **0.15** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1,052.719 ns |    284.0700 ns |    15.5708 ns |   1,048.756 ns |  0.29 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  |   1,058.921 ns |    166.9247 ns |     9.1497 ns |   1,059.688 ns |  0.29 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  |   2,920.297 ns |    312.5444 ns |    17.1316 ns |   2,927.429 ns |  0.80 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  | 150,930.844 ns | 59,576.2805 ns | 3,265.5770 ns | 149,271.454 ns | 41.45 |    4.31 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3,645.788 ns** |  **7,627.8760 ns** |   **418.1096 ns** |   **3,827.632 ns** |  **1.01** |    **0.15** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3,345.269 ns |     62.5996 ns |     3.4313 ns |   3,346.224 ns |  0.93 |    0.10 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    |   3,315.215 ns |    110.0591 ns |     6.0327 ns |   3,317.205 ns |  0.92 |    0.10 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    |  38,814.250 ns |  5,756.2724 ns |   315.5207 ns |  38,891.754 ns | 10.75 |    1.14 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    | 189,577.433 ns | 79,552.7075 ns | 4,360.5524 ns | 190,410.217 ns | 52.49 |    5.65 |         - |          NA |
|                                |       |        |                |                |               |                |       |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3,254.186 ns** |     **96.5332 ns** |     **5.2913 ns** |   **3,254.950 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1,657.484 ns |  2,069.5629 ns |   113.4397 ns |   1,602.616 ns |  0.51 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  |   1,624.424 ns |    467.3719 ns |    25.6182 ns |   1,612.671 ns |  0.50 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  |  20,440.412 ns |  2,702.3150 ns |   148.1230 ns |  20,374.966 ns |  6.28 |    0.04 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  | 159,599.470 ns | 12,918.8644 ns |   708.1266 ns | 159,495.168 ns | 49.04 |    0.20 |         - |          NA |
