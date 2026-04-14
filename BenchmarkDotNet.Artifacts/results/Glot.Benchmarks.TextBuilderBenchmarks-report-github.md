```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                                | PartSize | Parts | Locale | Mean        | Error     | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|-------------------------------------- |--------- |------ |------- |------------:|----------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| **&#39;StringBuilder -&gt; ToString&#39;**           | **8**        | **4**     | **Ascii**  |    **30.14 ns** |  **1.982 ns** | **0.109 ns** |  **1.00** |    **0.00** | **0.0353** |      **-** |     **296 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 8        | 4     | Ascii  |    32.49 ns |  5.720 ns | 0.314 ns |  1.08 |    0.01 | 0.0067 |      - |      56 B |        0.19 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 8        | 4     | Ascii  |    42.80 ns |  0.313 ns | 0.017 ns |  1.42 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 8        | 4     | Ascii  |    38.64 ns |  2.935 ns | 0.161 ns |  1.28 |    0.01 | 0.0067 |      - |      56 B |        0.19 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **8**        | **4**     | **Mixed**  |    **31.11 ns** | **39.548 ns** | **2.168 ns** |  **1.00** |    **0.08** | **0.0353** |      **-** |     **296 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 8        | 4     | Mixed  |    34.58 ns | 19.060 ns | 1.045 ns |  1.11 |    0.07 | 0.0086 |      - |      72 B |        0.24 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 8        | 4     | Mixed  |    43.22 ns |  4.874 ns | 0.267 ns |  1.39 |    0.08 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 8        | 4     | Mixed  |    49.54 ns |  4.636 ns | 0.254 ns |  1.60 |    0.09 | 0.0086 |      - |      72 B |        0.24 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **8**        | **16**    | **Ascii**  |    **83.02 ns** | **13.760 ns** | **0.754 ns** |  **1.00** |    **0.01** | **0.0985** | **0.0001** |     **824 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 8        | 16    | Ascii  |    98.78 ns |  9.827 ns | 0.539 ns |  1.19 |    0.01 | 0.0181 |      - |     152 B |        0.18 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 8        | 16    | Ascii  |   108.07 ns |  3.405 ns | 0.187 ns |  1.30 |    0.01 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 8        | 16    | Ascii  |   131.52 ns | 15.416 ns | 0.845 ns |  1.58 |    0.02 | 0.0181 |      - |     152 B |        0.18 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **8**        | **16**    | **Mixed**  |    **86.91 ns** |  **6.474 ns** | **0.355 ns** |  **1.00** |    **0.00** | **0.0985** | **0.0001** |     **824 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 8        | 16    | Mixed  |   101.59 ns |  6.555 ns | 0.359 ns |  1.17 |    0.01 | 0.0248 |      - |     208 B |        0.25 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 8        | 16    | Mixed  |   106.72 ns |  2.121 ns | 0.116 ns |  1.23 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 8        | 16    | Mixed  |   309.61 ns | 62.552 ns | 3.429 ns |  3.56 |    0.04 | 0.0782 |      - |     656 B |        0.80 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **64**       | **4**     | **Ascii**  |    **87.75 ns** | **10.598 ns** | **0.581 ns** |  **1.00** |    **0.01** | **0.1596** | **0.0005** |    **1336 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 64       | 4     | Ascii  |    44.87 ns |  2.258 ns | 0.124 ns |  0.51 |    0.00 | 0.0334 |      - |     280 B |        0.21 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 64       | 4     | Ascii  |    44.15 ns |  2.664 ns | 0.146 ns |  0.50 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 64       | 4     | Ascii  |    72.34 ns |  4.544 ns | 0.249 ns |  0.82 |    0.01 | 0.0334 |      - |     280 B |        0.21 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **64**       | **4**     | **Mixed**  |    **86.57 ns** | **12.096 ns** | **0.663 ns** |  **1.00** |    **0.01** | **0.1596** | **0.0005** |    **1336 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 64       | 4     | Mixed  |    58.51 ns |  9.597 ns | 0.526 ns |  0.68 |    0.01 | 0.0459 |      - |     384 B |        0.29 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 64       | 4     | Mixed  |    45.26 ns |  1.711 ns | 0.094 ns |  0.52 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 64       | 4     | Mixed  |   309.66 ns |  5.755 ns | 0.315 ns |  3.58 |    0.02 | 0.0992 |      - |     832 B |        0.62 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **64**       | **16**    | **Ascii**  |   **238.95 ns** |  **6.692 ns** | **0.367 ns** |  **1.00** |    **0.00** | **0.5441** | **0.0048** |    **4552 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 64       | 16    | Ascii  |   161.68 ns | 16.606 ns | 0.910 ns |  0.68 |    0.00 | 0.1252 | 0.0005 |    1048 B |        0.23 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 64       | 16    | Ascii  |   124.70 ns | 14.259 ns | 0.782 ns |  0.52 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 64       | 16    | Ascii  |   297.83 ns | 24.803 ns | 1.360 ns |  1.25 |    0.01 | 0.1249 | 0.0005 |    1048 B |        0.23 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| **&#39;StringBuilder -&gt; ToString&#39;**           | **64**       | **16**    | **Mixed**  |   **233.99 ns** | **19.634 ns** | **1.076 ns** |  **1.00** |    **0.01** | **0.5441** | **0.0050** |    **4552 B** |        **1.00** |
| &#39;TextBuilder -&gt; ToText&#39;               | 64       | 16    | Mixed  |   199.68 ns | 19.114 ns | 1.048 ns |  0.85 |    0.01 | 0.1721 | 0.0007 |    1440 B |        0.32 |
| &#39;TextBuilder -&gt; ToOwnedText&#39;          | 64       | 16    | Mixed  |   144.49 ns | 11.782 ns | 0.646 ns |  0.62 |    0.00 |      - |      - |         - |        0.00 |
| &#39;TextBuilder cross-enc UTF-16-&gt;UTF-8&#39; | 64       | 16    | Mixed  | 1,019.51 ns | 17.183 ns | 0.942 ns |  4.36 |    0.02 | 0.2785 |      - |    2336 B |        0.51 |
