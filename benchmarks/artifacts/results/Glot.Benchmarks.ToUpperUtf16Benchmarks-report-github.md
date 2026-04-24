```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                               | N     | Locale | Mean          | Error        | StdDev       | Ratio | RatioSD | Gen0   | Gen1   | Gen2   | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |--------------:|-------------:|-------------:|------:|--------:|-------:|-------:|-------:|----------:|------------:|
| **string.ToUpperInvariant**              | **64**    | **Ascii**  |      **14.32 ns** |     **0.109 ns** |     **0.091 ns** |  **1.00** |    **0.01** | **0.0181** |      **-** |      **-** |     **152 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 64    | Ascii  |      17.60 ns |     0.120 ns |     0.107 ns |  1.23 |    0.01 | 0.0181 |      - |      - |     152 B |        1.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 64    | Ascii  |      37.51 ns |     0.417 ns |     0.390 ns |  2.62 |    0.03 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **64**    | **Cjk**    |     **125.89 ns** |     **3.523 ns** |     **3.123 ns** |  **1.00** |    **0.03** | **0.0174** |      **-** |      **-** |     **152 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 64    | Cjk    |     469.50 ns |     5.813 ns |     5.438 ns |  3.73 |    0.10 |      - |      - |      - |         - |        0.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 64    | Cjk    |     490.17 ns |    10.778 ns |     9.000 ns |  3.90 |    0.11 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **64**    | **Emoji**  |     **109.84 ns** |     **2.992 ns** |     **2.798 ns** |  **1.00** |    **0.03** | **0.0180** |      **-** |      **-** |     **152 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 64    | Emoji  |     171.12 ns |     2.007 ns |     1.779 ns |  1.56 |    0.04 |      - |      - |      - |         - |        0.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 64    | Emoji  |     193.04 ns |     3.441 ns |     3.218 ns |  1.76 |    0.05 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **64**    | **Mixed**  |     **126.83 ns** |     **2.250 ns** |     **2.104 ns** |  **1.00** |    **0.02** | **0.0181** |      **-** |      **-** |     **152 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 64    | Mixed  |     564.58 ns |     3.577 ns |     3.171 ns |  4.45 |    0.08 | 0.0150 |      - |      - |     152 B |        1.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 64    | Mixed  |     582.15 ns |     6.148 ns |     5.751 ns |  4.59 |    0.09 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **4096**  | **Ascii**  |     **426.66 ns** |     **4.255 ns** |     **3.553 ns** |  **1.00** |    **0.01** | **0.9801** |      **-** |      **-** |    **8216 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 4096  | Ascii  |     501.29 ns |    12.885 ns |    11.422 ns |  1.17 |    0.03 | 0.9808 | 0.0295 |      - |    8216 B |        1.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 4096  | Ascii  |     518.02 ns |     5.484 ns |     5.130 ns |  1.21 |    0.02 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **4096**  | **Cjk**    |   **7,254.33 ns** |   **153.363 ns** |   **143.456 ns** |  **1.00** |    **0.03** | **0.9549** |      **-** |      **-** |    **8216 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 4096  | Cjk    |  28,840.49 ns |   248.178 ns |   220.003 ns |  3.98 |    0.08 |      - |      - |      - |       1 B |       0.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 4096  | Cjk    |  29,797.65 ns | 1,050.475 ns |   982.615 ns |  4.11 |    0.15 |      - |      - |      - |       2 B |       0.000 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **4096**  | **Emoji**  |   **6,218.52 ns** |   **202.485 ns** |   **189.405 ns** |  **1.00** |    **0.04** | **0.9810** |      **-** |      **-** |    **8216 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 4096  | Emoji  |  10,211.47 ns |   150.562 ns |   140.836 ns |  1.64 |    0.05 |      - |      - |      - |       1 B |       0.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 4096  | Emoji  |  10,586.85 ns |   273.291 ns |   255.637 ns |  1.70 |    0.06 |      - |      - |      - |       1 B |       0.000 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **4096**  | **Mixed**  |   **8,059.91 ns** |   **115.573 ns** |    **96.508 ns** |  **1.00** |    **0.02** | **0.9673** |      **-** |      **-** |    **8216 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 4096  | Mixed  |  33,821.14 ns |   293.841 ns |   274.859 ns |  4.20 |    0.06 | 0.8993 |      - |      - |    8218 B |       1.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 4096  | Mixed  |  32,968.33 ns |   548.924 ns |   513.463 ns |  4.09 |    0.08 |      - |      - |      - |       2 B |       0.000 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **65536** | **Ascii**  |  **51,401.11 ns** |   **950.856 ns** |   **889.431 ns** |  **1.00** |    **0.02** | **0.5342** | **0.5342** | **0.5342** |  **131102 B** |        **1.00** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 65536 | Ascii  |  34,874.44 ns | 1,042.498 ns |   870.533 ns |  0.68 |    0.02 | 0.3551 | 0.3551 | 0.3551 |  131091 B |        1.00 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 65536 | Ascii  |   7,964.40 ns |    60.028 ns |    56.150 ns |  0.15 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **65536** | **Cjk**    | **119,708.60 ns** | **1,936.466 ns** | **1,811.371 ns** |  **1.00** |    **0.02** |      **-** |      **-** |      **-** |  **131102 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 65536 | Cjk    | 463,826.36 ns | 4,552.203 ns | 4,258.134 ns |  3.88 |    0.07 |      - |      - |      - |      23 B |       0.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 65536 | Cjk    | 470,468.89 ns | 4,752.299 ns | 4,445.304 ns |  3.93 |    0.07 |      - |      - |      - |      24 B |       0.000 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **65536** | **Emoji**  | **103,378.60 ns** | **1,818.608 ns** | **1,612.149 ns** |  **1.00** |    **0.02** |      **-** |      **-** |      **-** |  **131102 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 65536 | Emoji  | 166,161.99 ns | 4,077.913 ns | 3,814.483 ns |  1.61 |    0.04 |      - |      - |      - |       4 B |       0.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 65536 | Emoji  | 167,826.25 ns | 3,338.515 ns | 2,959.507 ns |  1.62 |    0.04 |      - |      - |      - |       8 B |       0.000 |
|                                      |       |        |               |              |              |       |         |        |        |        |           |             |
| **string.ToUpperInvariant**              | **65536** | **Mixed**  | **133,859.38 ns** | **2,847.863 ns** | **2,663.893 ns** |  **1.00** |    **0.03** |      **-** |      **-** |      **-** |  **131103 B** |       **1.000** |
| &#39;Text.ToUpperInvariant UTF-16&#39;       | 65536 | Mixed  | 534,402.89 ns | 5,566.373 ns | 5,206.789 ns |  3.99 |    0.09 |      - |      - |      - |  131124 B |       1.000 |
| &#39;Text.ToUpperInvariantPooled UTF-16&#39; | 65536 | Mixed  | 521,278.03 ns | 4,656.561 ns | 4,127.920 ns |  3.90 |    0.08 |      - |      - |      - |      28 B |       0.000 |
