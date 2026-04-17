```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | N     | Locale | Mean           | Error           | StdDev         | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |---------------:|----------------:|---------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **2.0326 ns** |       **0.0637 ns** |      **0.0035 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      1.1636 ns |       0.1224 ns |      0.0067 ns |  0.57 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      0.5403 ns |       0.1226 ns |      0.0067 ns |  0.27 |    0.00 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **1.9968 ns** |       **0.9021 ns** |      **0.0494 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      3.0718 ns |       0.4206 ns |      0.0231 ns |  1.54 |    0.03 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      0.6392 ns |       3.1250 ns |      0.1713 ns |  0.32 |    0.07 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **2.1724 ns** |       **6.1539 ns** |      **0.3373 ns** |  **1.01** |    **0.19** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      1.3181 ns |       0.3140 ns |      0.0172 ns |  0.62 |    0.08 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      0.5269 ns |       0.2122 ns |      0.0116 ns |  0.25 |    0.03 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **146.7150 ns** |      **27.3440 ns** |      **1.4988 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     20.6332 ns |       2.9188 ns |      0.1600 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     19.8271 ns |       5.5411 ns |      0.3037 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **141.1014 ns** |      **72.5838 ns** |      **3.9786 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     63.9659 ns |      24.3969 ns |      1.3373 ns |  0.45 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     23.8783 ns |     132.4989 ns |      7.2627 ns |  0.17 |    0.04 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **143.3198 ns** |      **21.2427 ns** |      **1.1644 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     30.0785 ns |       2.6256 ns |      0.1439 ns |  0.21 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     22.1080 ns |      20.2910 ns |      1.1122 ns |  0.15 |    0.01 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **37,919.1046 ns** |   **7,292.8055 ns** |    **399.7433 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,301.4679 ns |   1,023.4380 ns |     56.0981 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Ascii  |  3,686.4317 ns |     190.3485 ns |     10.4336 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **39,116.5763 ns** |  **11,224.5800 ns** |    **615.2571 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 15,623.2204 ns |     339.7498 ns |     18.6228 ns |  0.40 |    0.01 |         - |          NA |
| Text.GetHashCode          | 65536 | Cjk    |  4,254.6882 ns |  15,959.7414 ns |    874.8073 ns |  0.11 |    0.02 |         - |          NA |
|                           |       |        |                |                 |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **46,747.1496 ns** | **191,940.8477 ns** | **10,520.9256 ns** |  **1.03** |    **0.27** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  8,359.8484 ns |  42,859.4456 ns |  2,349.2708 ns |  0.18 |    0.06 |         - |          NA |
| Text.GetHashCode          | 65536 | Mixed  |  3,839.7264 ns |   1,741.2066 ns |     95.4414 ns |  0.08 |    0.01 |         - |          NA |
