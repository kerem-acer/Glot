```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  Categories=FromUtf8  

```
| Method                   | N    | Locale | Mean        | Error        | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------------- |----- |------- |------------:|-------------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **Encoding.GetString**       | **256**  | **Ascii**  |    **28.68 ns** |     **3.222 ns** |   **0.177 ns** |  **1.00** |    **0.01** | **0.0641** |      **-** |     **536 B** |        **1.00** |
| &#39;Text.FromUtf8(byte[])&#39;  | 256  | Ascii  |   481.85 ns |   192.696 ns |  10.562 ns | 16.80 |    0.33 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 256  | Ascii  |   485.17 ns |    54.165 ns |   2.969 ns | 16.92 |    0.13 | 0.0334 |      - |     280 B |        0.52 |
| &#39;new U8String(span)&#39;     | 256  | Ascii  |    17.29 ns |     2.627 ns |   0.144 ns |  0.60 |    0.01 | 0.0344 | 0.0000 |     288 B |        0.54 |
| OwnedText.FromUtf8(span) | 256  | Ascii  |    19.38 ns |     1.536 ns |   0.084 ns |  0.68 |    0.00 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| **Encoding.GetString**       | **256**  | **Mixed**  |   **252.38 ns** |    **59.648 ns** |   **3.269 ns** |  **1.00** |    **0.02** | **0.0639** |      **-** |     **536 B** |        **1.00** |
| &#39;Text.FromUtf8(byte[])&#39;  | 256  | Mixed  |   601.93 ns |    18.347 ns |   1.006 ns |  2.39 |    0.03 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 256  | Mixed  |   589.90 ns |   169.147 ns |   9.272 ns |  2.34 |    0.04 | 0.0448 |      - |     376 B |        0.70 |
| &#39;new U8String(span)&#39;     | 256  | Mixed  |   164.02 ns |    25.169 ns |   1.380 ns |  0.65 |    0.01 | 0.0458 |      - |     384 B |        0.72 |
| OwnedText.FromUtf8(span) | 256  | Mixed  |    23.39 ns |     2.251 ns |   0.123 ns |  0.09 |    0.00 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| **Encoding.GetString**       | **4096** | **Ascii**  |   **378.10 ns** |    **61.212 ns** |   **3.355 ns** |  **1.00** |    **0.01** | **0.9813** |      **-** |    **8216 B** |        **1.00** |
| &#39;Text.FromUtf8(byte[])&#39;  | 4096 | Ascii  | 7,776.99 ns | 2,971.372 ns | 162.871 ns | 20.57 |    0.40 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 4096 | Ascii  | 7,737.00 ns |   675.958 ns |  37.052 ns | 20.46 |    0.18 | 0.4883 |      - |    4120 B |        0.50 |
| &#39;new U8String(span)&#39;     | 4096 | Ascii  |   215.98 ns |     4.890 ns |   0.268 ns |  0.57 |    0.00 | 0.4933 | 0.0074 |    4128 B |        0.50 |
| OwnedText.FromUtf8(span) | 4096 | Ascii  |   181.47 ns |    35.722 ns |   1.958 ns |  0.48 |    0.01 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| **Encoding.GetString**       | **4096** | **Mixed**  | **3,930.13 ns** |   **730.440 ns** |  **40.038 ns** |  **1.00** |    **0.01** | **0.9766** |      **-** |    **8216 B** |        **1.00** |
| &#39;Text.FromUtf8(byte[])&#39;  | 4096 | Mixed  | 9,583.22 ns |   933.210 ns |  51.152 ns |  2.44 |    0.02 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 4096 | Mixed  | 9,199.08 ns |   522.968 ns |  28.666 ns |  2.34 |    0.02 | 0.6714 |      - |    5664 B |        0.69 |
| &#39;new U8String(span)&#39;     | 4096 | Mixed  | 2,753.91 ns |   109.910 ns |   6.025 ns |  0.70 |    0.01 | 0.6752 | 0.0114 |    5672 B |        0.69 |
| OwnedText.FromUtf8(span) | 4096 | Mixed  |   254.38 ns |    18.887 ns |   1.035 ns |  0.06 |    0.00 |      - |      - |         - |        0.00 |
