```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Categories | N    | Locale | Mean         | Error         | StdDev       | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------------- |----------- |----- |------- |-------------:|--------------:|-------------:|------:|--------:|-------:|-------:|----------:|------------:|
| **string.Replace**              | **Replace**    | **256**  | **Ascii**  |    **118.23 ns** |     **11.284 ns** |     **0.619 ns** |  **1.00** |    **0.01** | **0.0753** |      **-** |     **632 B** |        **1.00** |
| Text.Replace                | Replace    | 256  | Ascii  |    313.80 ns |      4.971 ns |     0.272 ns |  2.65 |    0.01 | 0.0391 |      - |     328 B |        0.52 |
| Text.ReplacePooled          | Replace    | 256  | Ascii  |    290.99 ns |     31.308 ns |     1.716 ns |  2.46 |    0.02 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 256  | Ascii  |    107.64 ns |      7.673 ns |     0.421 ns |  0.91 |    0.01 | 0.0401 |      - |     336 B |        0.53 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.Replace**              | **Replace**    | **256**  | **Mixed**  |    **118.59 ns** |     **26.011 ns** |     **1.426 ns** |  **1.00** |    **0.01** | **0.0753** |      **-** |     **632 B** |        **1.00** |
| Text.Replace                | Replace    | 256  | Mixed  |    373.98 ns |      7.166 ns |     0.393 ns |  3.15 |    0.03 | 0.0505 |      - |     424 B |        0.67 |
| Text.ReplacePooled          | Replace    | 256  | Mixed  |    368.71 ns |     56.011 ns |     3.070 ns |  3.11 |    0.04 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 256  | Mixed  |    115.36 ns |     16.480 ns |     0.903 ns |  0.97 |    0.01 | 0.0515 |      - |     432 B |        0.68 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.Replace**              | **Replace**    | **4096** | **Ascii**  |  **2,133.39 ns** |    **465.361 ns** |    **25.508 ns** |  **1.00** |    **0.01** | **1.1749** |      **-** |    **9848 B** |        **1.00** |
| Text.Replace                | Replace    | 4096 | Ascii  | 19,370.93 ns |  3,070.162 ns |   168.286 ns |  9.08 |    0.12 | 0.5798 |      - |    4936 B |        0.50 |
| Text.ReplacePooled          | Replace    | 4096 | Ascii  | 18,892.55 ns |  1,343.216 ns |    73.626 ns |  8.86 |    0.10 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 4096 | Ascii  |  1,441.04 ns |    503.173 ns |    27.581 ns |  0.68 |    0.01 | 0.5894 | 0.0095 |    4944 B |        0.50 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.Replace**              | **Replace**    | **4096** | **Mixed**  |  **2,099.32 ns** |    **199.274 ns** |    **10.923 ns** |  **1.00** |    **0.01** | **1.1749** |      **-** |    **9848 B** |        **1.00** |
| Text.Replace                | Replace    | 4096 | Mixed  | 26,804.08 ns |  1,615.892 ns |    88.572 ns | 12.77 |    0.07 | 0.7629 |      - |    6496 B |        0.66 |
| Text.ReplacePooled          | Replace    | 4096 | Mixed  | 25,823.80 ns |    703.290 ns |    38.550 ns | 12.30 |    0.06 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 4096 | Mixed  |  2,097.85 ns |    134.661 ns |     7.381 ns |  1.00 |    0.01 | 0.7744 | 0.0153 |    6496 B |        0.66 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.ToUpperInvariant**     | **ToUpper**    | **256**  | **Ascii**  |     **28.77 ns** |      **4.032 ns** |     **0.221 ns** |  **1.00** |    **0.01** | **0.0669** |      **-** |     **560 B** |        **1.00** |
| Text.ToUpperInvariant       | ToUpper    | 256  | Ascii  |  2,005.90 ns |    205.499 ns |    11.264 ns | 69.72 |    0.57 | 0.0343 |      - |     296 B |        0.53 |
| Text.ToUpperInvariantPooled | ToUpper    | 256  | Ascii  |  2,026.15 ns |    334.791 ns |    18.351 ns | 70.43 |    0.72 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.ToUpperInvariant**     | **ToUpper**    | **256**  | **Mixed**  |    **535.83 ns** |    **252.737 ns** |    **13.853 ns** |  **1.00** |    **0.03** | **0.0668** |      **-** |     **560 B** |        **1.00** |
| Text.ToUpperInvariant       | ToUpper    | 256  | Mixed  |  2,402.28 ns |     83.182 ns |     4.559 ns |  4.49 |    0.10 | 0.0458 |      - |     392 B |        0.70 |
| Text.ToUpperInvariantPooled | ToUpper    | 256  | Mixed  |  2,353.09 ns |    273.283 ns |    14.980 ns |  4.39 |    0.10 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.ToUpperInvariant**     | **ToUpper**    | **4096** | **Ascii**  |    **391.28 ns** |     **56.251 ns** |     **3.083 ns** |  **1.00** |    **0.01** | **1.0295** |      **-** |    **8624 B** |        **1.00** |
| Text.ToUpperInvariant       | ToUpper    | 4096 | Ascii  | 32,329.14 ns |  6,224.965 ns |   341.211 ns | 82.63 |    0.94 | 0.4883 |      - |    4328 B |        0.50 |
| Text.ToUpperInvariantPooled | ToUpper    | 4096 | Ascii  | 32,988.24 ns | 10,582.307 ns |   580.052 ns | 84.31 |    1.41 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| **string.ToUpperInvariant**     | **ToUpper**    | **4096** | **Mixed**  |  **8,583.68 ns** |    **467.955 ns** |    **25.650 ns** |  **1.00** |    **0.00** | **1.0223** |      **-** |    **8624 B** |        **1.00** |
| Text.ToUpperInvariant       | ToUpper    | 4096 | Mixed  | 38,273.33 ns | 19,895.858 ns | 1,090.559 ns |  4.46 |    0.11 | 0.6714 |      - |    5888 B |        0.68 |
| Text.ToUpperInvariantPooled | ToUpper    | 4096 | Mixed  | 38,444.13 ns |  5,284.226 ns |   289.646 ns |  4.48 |    0.03 |      - |      - |         - |        0.00 |
