```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  Categories=FromString  

```
| Method                 | N    | Locale | Mean          | Error       | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |----- |------- |--------------:|------------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **&#39;new string(source)&#39;**   | **256**  | **Ascii**  |    **23.0513 ns** |  **12.4238 ns** |  **0.6810 ns** | **1.001** |    **0.04** | **0.0641** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)      | 256  | Ascii  |    18.7955 ns |  10.5796 ns |  0.5799 ns | 0.816 |    0.03 |      - |      - |         - |        0.00 |
| Text.FromAscii(string) | 256  | Ascii  |     0.1619 ns |   1.0895 ns |  0.0597 ns | 0.007 |    0.00 |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256  | Ascii  |    23.5675 ns |  21.3719 ns |  1.1715 ns | 1.023 |    0.05 | 0.0344 | 0.0000 |     288 B |        0.54 |
|                        |      |        |               |             |            |       |         |        |        |           |             |
| **&#39;new string(source)&#39;**   | **256**  | **Mixed**  |    **25.6741 ns** |  **99.6008 ns** |  **5.4595 ns** |  **1.03** |    **0.26** | **0.0641** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)      | 256  | Mixed  |    18.0154 ns |   0.6948 ns |  0.0381 ns |  0.72 |    0.12 |      - |      - |         - |        0.00 |
| Text.FromAscii(string) | 256  | Mixed  |     0.2816 ns |   0.2071 ns |  0.0114 ns |  0.01 |    0.00 |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256  | Mixed  |   151.3856 ns |  22.7286 ns |  1.2458 ns |  6.06 |    1.01 | 0.0458 |      - |     384 B |        0.72 |
|                        |      |        |               |             |            |       |         |        |        |           |             |
| **&#39;new string(source)&#39;**   | **4096** | **Ascii**  |   **307.1771 ns** |  **18.9728 ns** |  **1.0400 ns** | **1.000** |    **0.00** | **0.9813** |      **-** |    **8216 B** |        **1.00** |
| Text.From(string)      | 4096 | Ascii  |   285.0527 ns |  43.7299 ns |  2.3970 ns | 0.928 |    0.01 |      - |      - |         - |        0.00 |
| Text.FromAscii(string) | 4096 | Ascii  |     0.2296 ns |   0.1712 ns |  0.0094 ns | 0.001 |    0.00 |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39; | 4096 | Ascii  |   345.8597 ns | 405.0124 ns | 22.2001 ns | 1.126 |    0.06 | 0.4930 | 0.0072 |    4128 B |        0.50 |
|                        |      |        |               |             |            |       |         |        |        |           |             |
| **&#39;new string(source)&#39;**   | **4096** | **Mixed**  |   **304.7078 ns** |  **52.7111 ns** |  **2.8893 ns** | **1.000** |    **0.01** | **0.9813** |      **-** |    **8216 B** |        **1.00** |
| Text.From(string)      | 4096 | Mixed  |   280.7236 ns |  50.6994 ns |  2.7790 ns | 0.921 |    0.01 |      - |      - |         - |        0.00 |
| Text.FromAscii(string) | 4096 | Mixed  |     0.2936 ns |   0.2615 ns |  0.0143 ns | 0.001 |    0.00 |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39; | 4096 | Mixed  | 1,923.2598 ns | 193.5763 ns | 10.6106 ns | 6.312 |    0.06 | 0.6752 | 0.0114 |    5672 B |        0.69 |
