```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N  | Locale | Mean      | Error      | StdDev     | Median    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |--- |------- |----------:|-----------:|-----------:|----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64** | **Ascii**  |  **8.733 ns** |  **22.023 ns** |  **1.2072 ns** |  **8.211 ns** |  **1.01** |    **0.17** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Ascii  |  6.560 ns |  20.874 ns |  1.1442 ns |  6.063 ns |  0.76 |    0.14 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Ascii  | 50.149 ns |  54.687 ns |  2.9976 ns | 49.783 ns |  5.81 |    0.72 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Ascii  | 14.620 ns |  54.998 ns |  3.0147 ns | 13.202 ns |  1.69 |    0.36 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Ascii  | 34.852 ns |  38.520 ns |  2.1114 ns | 34.022 ns |  4.04 |    0.50 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Ascii  |  5.142 ns |  25.558 ns |  1.4009 ns |  4.474 ns |  0.60 |    0.16 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Ascii  |  6.004 ns |   5.422 ns |  0.2972 ns |  5.850 ns |  0.70 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Ascii  | 44.975 ns |  18.538 ns |  1.0161 ns | 44.541 ns |  5.21 |    0.59 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Ascii  | 18.456 ns |   4.905 ns |  0.2689 ns | 18.415 ns |  2.14 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Ascii  | 46.244 ns |  36.264 ns |  1.9878 ns | 47.083 ns |  5.36 |    0.63 |         - |          NA |
|                             |    |        |           |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Cjk**    |  **5.159 ns** |  **14.442 ns** |  **0.7916 ns** |  **5.563 ns** |  **1.02** |    **0.20** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Cjk    |  7.094 ns |   5.559 ns |  0.3047 ns |  6.924 ns |  1.40 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Cjk    | 80.733 ns | 484.459 ns | 26.5548 ns | 73.538 ns | 15.92 |    5.13 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Cjk    | 20.871 ns |  80.039 ns |  4.3872 ns | 18.567 ns |  4.12 |    0.96 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Cjk    | 40.757 ns | 103.443 ns |  5.6701 ns | 39.224 ns |  8.04 |    1.52 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Cjk    | 10.376 ns |  88.719 ns |  4.8630 ns |  7.571 ns |  2.05 |    0.89 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Cjk    |  5.125 ns |  23.774 ns |  1.3032 ns |  5.571 ns |  1.01 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Cjk    | 48.038 ns |  29.288 ns |  1.6054 ns | 47.694 ns |  9.47 |    1.41 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Cjk    | 14.841 ns |  37.343 ns |  2.0469 ns | 14.492 ns |  2.93 |    0.55 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Cjk    | 37.327 ns |  39.750 ns |  2.1789 ns | 36.348 ns |  7.36 |    1.14 |         - |          NA |
|                             |    |        |           |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Emoji**  |  **7.034 ns** |  **18.572 ns** |  **1.0180 ns** |  **6.661 ns** |  **1.01** |    **0.17** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Emoji  |  4.564 ns |  15.768 ns |  0.8643 ns |  4.811 ns |  0.66 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Emoji  | 67.796 ns | 150.949 ns |  8.2740 ns | 63.781 ns |  9.77 |    1.55 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Emoji  | 22.030 ns |  18.124 ns |  0.9934 ns | 21.605 ns |  3.17 |    0.39 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Emoji  | 10.054 ns |   1.201 ns |  0.0658 ns | 10.089 ns |  1.45 |    0.17 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Emoji  | 11.064 ns |  93.935 ns |  5.1489 ns |  9.511 ns |  1.59 |    0.67 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Emoji  |  6.473 ns |  26.672 ns |  1.4620 ns |  6.129 ns |  0.93 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Emoji  | 73.950 ns |  95.554 ns |  5.2377 ns | 71.593 ns | 10.65 |    1.42 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Emoji  | 13.342 ns |  30.069 ns |  1.6482 ns | 13.575 ns |  1.92 |    0.31 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Emoji  | 66.687 ns | 166.374 ns |  9.1195 ns | 68.964 ns |  9.61 |    1.61 |         - |          NA |
|                             |    |        |           |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Mixed**  |  **4.048 ns** |  **15.300 ns** |  **0.8387 ns** |  **3.619 ns** |  **1.03** |    **0.25** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Mixed  |  4.940 ns |   8.908 ns |  0.4883 ns |  5.051 ns |  1.25 |    0.23 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Mixed  | 47.188 ns |  28.379 ns |  1.5556 ns | 47.242 ns | 11.96 |    1.95 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Mixed  | 21.768 ns | 120.355 ns |  6.5971 ns | 19.373 ns |  5.52 |    1.71 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Mixed  | 37.031 ns |  34.521 ns |  1.8922 ns | 38.044 ns |  9.39 |    1.57 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Mixed  |  7.977 ns |  25.978 ns |  1.4239 ns |  8.515 ns |  2.02 |    0.45 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Mixed  |  9.204 ns |  21.075 ns |  1.1552 ns |  8.912 ns |  2.33 |    0.45 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Mixed  |  8.460 ns |   1.025 ns |  0.0562 ns |  8.482 ns |  2.14 |    0.34 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Mixed  | 17.773 ns |  27.213 ns |  1.4916 ns | 18.079 ns |  4.51 |    0.80 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Mixed  | 72.988 ns | 559.055 ns | 30.6437 ns | 57.318 ns | 18.50 |    7.43 |         - |          NA |
