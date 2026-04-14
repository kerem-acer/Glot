```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                 | Categories     | N    | Locale | Mean         | Error         | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |--------------- |----- |------- |-------------:|--------------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **string.EnumerateRunes**  | **EnumerateRunes** | **256**  | **Ascii**  |    **387.01 ns** |    **146.636 ns** |   **8.038 ns** |  **1.00** |    **0.03** |      **-** |      **-** |         **-** |          **NA** |
| Text.EnumerateRunes    | EnumerateRunes | 256  | Ascii  |    573.89 ns |     13.559 ns |   0.743 ns |  1.48 |    0.03 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 256  | Ascii  |     79.50 ns |      9.182 ns |   0.503 ns |  0.21 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **string.EnumerateRunes**  | **EnumerateRunes** | **256**  | **Mixed**  |    **383.75 ns** |     **25.815 ns** |   **1.415 ns** |  **1.00** |    **0.00** |      **-** |      **-** |         **-** |          **NA** |
| Text.EnumerateRunes    | EnumerateRunes | 256  | Mixed  |    584.86 ns |     87.290 ns |   4.785 ns |  1.52 |    0.01 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 256  | Mixed  |     77.82 ns |      4.991 ns |   0.274 ns |  0.20 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **string.EnumerateRunes**  | **EnumerateRunes** | **4096** | **Ascii**  |  **1,661.10 ns** |    **568.054 ns** |  **31.137 ns** |  **1.00** |    **0.02** |      **-** |      **-** |         **-** |          **NA** |
| Text.EnumerateRunes    | EnumerateRunes | 4096 | Ascii  |  9,097.09 ns |  1,878.798 ns | 102.983 ns |  5.48 |    0.10 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 4096 | Ascii  |  1,245.00 ns |    130.323 ns |   7.143 ns |  0.75 |    0.01 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **string.EnumerateRunes**  | **EnumerateRunes** | **4096** | **Mixed**  |  **6,266.30 ns** |  **2,861.890 ns** | **156.870 ns** |  **1.00** |    **0.03** |      **-** |      **-** |         **-** |          **NA** |
| Text.EnumerateRunes    | EnumerateRunes | 4096 | Mixed  |  9,172.33 ns |    602.219 ns |  33.010 ns |  1.46 |    0.03 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 4096 | Mixed  |  1,188.10 ns |    156.558 ns |   8.581 ns |  0.19 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**   | **Split**          | **256**  | **Ascii**  |    **278.78 ns** |     **46.461 ns** |   **2.547 ns** |  **1.00** |    **0.01** | **0.1683** | **0.0005** |    **1408 B** |        **1.00** |
| &#39;Text.Split count&#39;     | Split          | 256  | Ascii  |  2,080.46 ns |    126.163 ns |   6.915 ns |  7.46 |    0.06 |      - |      - |         - |        0.00 |
| &#39;U8String.Split count&#39; | Split          | 256  | Ascii  |    227.43 ns |     68.777 ns |   3.770 ns |  0.82 |    0.01 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**   | **Split**          | **256**  | **Mixed**  |    **322.16 ns** |     **19.408 ns** |   **1.064 ns** |  **1.00** |    **0.00** | **0.1683** | **0.0005** |    **1408 B** |        **1.00** |
| &#39;Text.Split count&#39;     | Split          | 256  | Mixed  |  1,955.40 ns |    478.027 ns |  26.202 ns |  6.07 |    0.07 |      - |      - |         - |        0.00 |
| &#39;U8String.Split count&#39; | Split          | 256  | Mixed  |    233.23 ns |     30.778 ns |   1.687 ns |  0.72 |    0.00 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**   | **Split**          | **4096** | **Ascii**  |  **4,601.54 ns** |  **1,158.196 ns** |  **63.485 ns** |  **1.00** |    **0.02** | **2.6169** | **0.1907** |   **21896 B** |        **1.00** |
| &#39;Text.Split count&#39;     | Split          | 4096 | Ascii  | 33,007.07 ns |  2,217.912 ns | 121.571 ns |  7.17 |    0.09 |      - |      - |         - |        0.00 |
| &#39;U8String.Split count&#39; | Split          | 4096 | Ascii  |  2,335.20 ns |  5,975.375 ns | 327.530 ns |  0.51 |    0.06 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**   | **Split**          | **4096** | **Mixed**  |  **4,777.24 ns** |  **1,105.380 ns** |  **60.590 ns** |  **1.00** |    **0.02** | **2.6169** | **0.1907** |   **21896 B** |        **1.00** |
| &#39;Text.Split count&#39;     | Split          | 4096 | Mixed  | 31,683.68 ns | 13,615.891 ns | 746.333 ns |  6.63 |    0.15 |      - |      - |         - |        0.00 |
| &#39;U8String.Split count&#39; | Split          | 4096 | Mixed  |  2,506.68 ns |  1,189.830 ns |  65.219 ns |  0.52 |    0.01 |      - |      - |         - |        0.00 |
