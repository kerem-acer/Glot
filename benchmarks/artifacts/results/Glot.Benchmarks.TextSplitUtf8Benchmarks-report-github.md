```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Categories     | N     | Locale | Mean          | Error          | StdDev        | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |--------------- |------ |------- |--------------:|---------------:|--------------:|------:|--------:|----------:|------------:|
| **string.EnumerateRunes**       | **EnumerateRunes** | **64**    | **Ascii**  |      **80.27 ns** |      **16.153 ns** |      **0.885 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 64    | Ascii  |      33.95 ns |      14.156 ns |      0.776 ns |  0.42 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 64    | Ascii  |      17.23 ns |       0.395 ns |      0.022 ns |  0.21 |    0.00 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 64    | Ascii  |     146.49 ns |       8.423 ns |      0.462 ns |  1.83 |    0.02 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **64**    | **Cjk**    |      **86.93 ns** |      **39.020 ns** |      **2.139 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 64    | Cjk    |      60.44 ns |       1.804 ns |      0.099 ns |  0.70 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 64    | Cjk    |      28.60 ns |      35.800 ns |      1.962 ns |  0.33 |    0.02 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 64    | Cjk    |     195.97 ns |      28.541 ns |      1.564 ns |  2.26 |    0.05 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **64**    | **Mixed**  |      **75.43 ns** |      **18.151 ns** |      **0.995 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 64    | Mixed  |      46.68 ns |      12.764 ns |      0.700 ns |  0.62 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 64    | Mixed  |      17.77 ns |       0.591 ns |      0.032 ns |  0.24 |    0.00 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 64    | Mixed  |     157.16 ns |       8.884 ns |      0.487 ns |  2.08 |    0.02 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096**  | **Ascii**  |   **1,685.73 ns** |     **880.178 ns** |     **48.246 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096  | Ascii  |   2,256.83 ns |      68.859 ns |      3.774 ns |  1.34 |    0.03 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096  | Ascii  |   1,249.38 ns |     168.489 ns |      9.235 ns |  0.74 |    0.02 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096  | Ascii  |   9,056.58 ns |   1,293.775 ns |     70.916 ns |  5.38 |    0.14 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096**  | **Cjk**    |   **1,713.57 ns** |   **1,465.829 ns** |     **80.347 ns** |  **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096  | Cjk    |   4,825.73 ns |     463.774 ns |     25.421 ns |  2.82 |    0.12 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096  | Cjk    |   1,457.49 ns |     317.735 ns |     17.416 ns |  0.85 |    0.04 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096  | Cjk    |  11,333.45 ns |   8,943.686 ns |    490.234 ns |  6.62 |    0.37 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096**  | **Mixed**  |   **6,103.34 ns** |     **156.722 ns** |      **8.590 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096  | Mixed  |   2,912.67 ns |     111.355 ns |      6.104 ns |  0.48 |    0.00 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096  | Mixed  |   1,191.22 ns |     277.078 ns |     15.188 ns |  0.20 |    0.00 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096  | Mixed  |   9,539.95 ns |     492.836 ns |     27.014 ns |  1.56 |    0.00 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **65536** | **Ascii**  |  **75,656.07 ns** |  **69,516.192 ns** |  **3,810.417 ns** |  **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 65536 | Ascii  |  38,172.38 ns |   1,421.966 ns |     77.943 ns |  0.51 |    0.02 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 65536 | Ascii  |  19,648.02 ns |   1,358.024 ns |     74.438 ns |  0.26 |    0.01 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 65536 | Ascii  | 146,961.27 ns |  78,693.049 ns |  4,313.432 ns |  1.95 |    0.10 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **65536** | **Cjk**    |  **72,335.21 ns** | **250,754.887 ns** | **13,744.722 ns** |  **1.03** |    **0.25** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 65536 | Cjk    |  62,823.14 ns |     865.418 ns |     47.436 ns |  0.89 |    0.16 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 65536 | Cjk    |  25,411.45 ns |  14,114.750 ns |    773.677 ns |  0.36 |    0.07 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 65536 | Cjk    | 168,609.67 ns |   9,982.465 ns |    547.173 ns |  2.39 |    0.43 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **65536** | **Mixed**  |  **98,854.49 ns** |  **17,231.787 ns** |    **944.532 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 65536 | Mixed  |  49,123.95 ns |  27,509.755 ns |  1,507.903 ns |  0.50 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 65536 | Mixed  |  19,134.86 ns |   4,333.678 ns |    237.544 ns |  0.19 |    0.00 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 65536 | Mixed  | 152,766.83 ns |  22,860.360 ns |  1,253.053 ns |  1.55 |    0.02 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **64**    | **Ascii**  |      **36.36 ns** |       **2.453 ns** |      **0.134 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 64    | Ascii  |      32.06 ns |       1.126 ns |      0.062 ns |  0.88 |    0.00 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 64    | Ascii  |     523.66 ns |      35.542 ns |      1.948 ns | 14.40 |    0.07 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **64**    | **Cjk**    |     **105.89 ns** |      **39.891 ns** |      **2.187 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 64    | Cjk    |      91.35 ns |      11.541 ns |      0.633 ns |  0.86 |    0.02 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 64    | Cjk    |     525.01 ns |      52.111 ns |      2.856 ns |  4.96 |    0.09 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **64**    | **Mixed**  |      **37.17 ns** |       **5.117 ns** |      **0.280 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 64    | Mixed  |      32.91 ns |       0.872 ns |      0.048 ns |  0.89 |    0.01 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 64    | Mixed  |     477.72 ns |      92.801 ns |      5.087 ns | 12.85 |    0.15 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096**  | **Ascii**  |   **1,837.32 ns** |   **1,000.060 ns** |     **54.817 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096  | Ascii  |   2,434.45 ns |   2,635.015 ns |    144.434 ns |  1.33 |    0.08 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096  | Ascii  |  33,122.42 ns |   2,954.474 ns |    161.945 ns | 18.04 |    0.47 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096**  | **Cjk**    |   **5,704.51 ns** |   **9,371.090 ns** |    **513.661 ns** |  **1.01** |    **0.11** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096  | Cjk    |   4,613.72 ns |   3,760.990 ns |    206.153 ns |  0.81 |    0.07 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096  | Cjk    |  31,784.46 ns |   1,113.779 ns |     61.050 ns |  5.60 |    0.42 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096**  | **Mixed**  |   **2,176.64 ns** |   **2,747.191 ns** |    **150.583 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096  | Mixed  |   2,174.76 ns |   3,186.491 ns |    174.662 ns |  1.00 |    0.09 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096  | Mixed  |  32,158.64 ns |  15,205.200 ns |    833.448 ns | 14.82 |    0.98 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **65536** | **Ascii**  |  **37,306.16 ns** |  **55,088.710 ns** |  **3,019.598 ns** |  **1.00** |    **0.10** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 65536 | Ascii  |  33,954.87 ns |  72,831.822 ns |  3,992.158 ns |  0.91 |    0.11 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 65536 | Ascii  | 527,869.25 ns |  10,009.890 ns |    548.676 ns | 14.21 |    1.04 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **65536** | **Cjk**    |  **83,504.02 ns** |  **17,959.569 ns** |    **984.425 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 65536 | Cjk    |  75,720.95 ns | 113,035.955 ns |  6,195.882 ns |  0.91 |    0.06 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 65536 | Cjk    | 525,518.28 ns |  60,480.112 ns |  3,315.119 ns |  6.29 |    0.07 |         - |          NA |
|                             |                |       |        |               |                |               |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **65536** | **Mixed**  |  **28,071.69 ns** |   **2,244.025 ns** |    **123.003 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 65536 | Mixed  |  26,781.99 ns |  15,304.456 ns |    838.889 ns |  0.95 |    0.03 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 65536 | Mixed  | 491,997.30 ns |  52,141.481 ns |  2,858.051 ns | 17.53 |    0.11 |         - |          NA |
