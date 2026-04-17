```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | N     | Locale | Mean           | Error           | StdDev         | Median         | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |---------------:|----------------:|---------------:|---------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **2.0507 ns** |       **0.7464 ns** |      **0.0409 ns** |      **2.0306 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      1.1854 ns |       0.1519 ns |      0.0083 ns |      1.1811 ns |  0.58 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Ascii  |      0.2714 ns |       0.1163 ns |      0.0064 ns |      0.2691 ns |  0.13 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      0.7700 ns |       8.1924 ns |      0.4491 ns |      0.5150 ns |  0.38 |    0.19 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **2.7125 ns** |      **14.9544 ns** |      **0.8197 ns** |      **2.2458 ns** |  **1.05** |    **0.37** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      3.0175 ns |       0.4672 ns |      0.0256 ns |      3.0298 ns |  1.17 |    0.26 |         - |          NA |
| U8String.GetHashCode      | 8     | Cjk    |      1.4491 ns |       0.0843 ns |      0.0046 ns |      1.4496 ns |  0.56 |    0.13 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      1.4745 ns |       0.2998 ns |      0.0164 ns |      1.4746 ns |  0.57 |    0.13 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **1.9332 ns** |       **0.2519 ns** |      **0.0138 ns** |      **1.9322 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      1.2933 ns |       0.3237 ns |      0.0177 ns |      1.2873 ns |  0.67 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Mixed  |      0.6223 ns |      10.9878 ns |      0.6023 ns |      0.2976 ns |  0.32 |    0.27 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      0.5817 ns |       0.2853 ns |      0.0156 ns |      0.5814 ns |  0.30 |    0.01 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **150.8853 ns** |      **35.5696 ns** |      **1.9497 ns** |    **150.5219 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     20.8810 ns |       3.0872 ns |      0.1692 ns |     20.8156 ns |  0.14 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 256   | Ascii  |     15.4581 ns |      58.5488 ns |      3.2093 ns |     13.6309 ns |  0.10 |    0.02 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     10.9385 ns |      40.2708 ns |      2.2074 ns |      9.6653 ns |  0.07 |    0.01 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **152.3838 ns** |     **212.7215 ns** |     **11.6600 ns** |    **147.7259 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     64.7911 ns |       9.1691 ns |      0.5026 ns |     64.8325 ns |  0.43 |    0.03 |         - |          NA |
| U8String.GetHashCode      | 256   | Cjk    |     27.6428 ns |      16.3030 ns |      0.8936 ns |     27.2779 ns |  0.18 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     23.6276 ns |       5.0400 ns |      0.2763 ns |     23.5495 ns |  0.16 |    0.01 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **148.7379 ns** |      **31.1625 ns** |      **1.7081 ns** |    **148.3464 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     32.7121 ns |      18.6722 ns |      1.0235 ns |     32.9379 ns |  0.22 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 256   | Mixed  |     20.5015 ns |     123.5917 ns |      6.7745 ns |     16.5909 ns |  0.14 |    0.04 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     12.8396 ns |       1.7280 ns |      0.0947 ns |     12.8939 ns |  0.09 |    0.00 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **38,111.0517 ns** |   **3,827.6208 ns** |    **209.8048 ns** | **38,220.9371 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,474.5564 ns |   9,401.0501 ns |    515.3033 ns |  5,179.8420 ns |  0.14 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 65536 | Ascii  |  1,869.6230 ns |     123.2939 ns |      6.7582 ns |  1,869.2013 ns |  0.05 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Ascii  |  1,833.3729 ns |     101.9643 ns |      5.5890 ns |  1,832.2828 ns |  0.05 |    0.00 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **38,229.3540 ns** |   **5,503.3491 ns** |    **301.6571 ns** | **38,306.6050 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 16,858.7460 ns |  28,366.7151 ns |  1,554.8754 ns | 16,540.6456 ns |  0.44 |    0.04 |         - |          NA |
| U8String.GetHashCode      | 65536 | Cjk    |  6,531.2596 ns |  29,750.5333 ns |  1,630.7271 ns |  5,603.7467 ns |  0.17 |    0.04 |         - |          NA |
| Text.GetHashCode          | 65536 | Cjk    |  5,915.2661 ns |  10,988.8670 ns |    602.3369 ns |  5,570.6161 ns |  0.15 |    0.01 |         - |          NA |
|                           |       |        |                |                 |                |                |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **45,509.3731 ns** | **235,123.0581 ns** | **12,887.8883 ns** | **38,447.7335 ns** |  **1.05** |    **0.34** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  7,272.0096 ns |  12,172.5926 ns |    667.2209 ns |  6,888.4298 ns |  0.17 |    0.04 |         - |          NA |
| U8String.GetHashCode      | 65536 | Mixed  |  2,995.1713 ns |  17,020.9507 ns |    932.9757 ns |  2,482.0179 ns |  0.07 |    0.02 |         - |          NA |
| Text.GetHashCode          | 65536 | Mixed  |  2,470.2047 ns |     382.5143 ns |     20.9669 ns |  2,458.8769 ns |  0.06 |    0.01 |         - |          NA |
