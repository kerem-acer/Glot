```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N  | Locale | Mean       | Error      | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |--- |------- |-----------:|-----------:|----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64** | **Ascii**  |  **0.5370 ns** |  **0.0345 ns** | **0.0019 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Ascii  |  0.5286 ns |  0.0511 ns | 0.0028 ns |  0.98 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Ascii  | 26.1525 ns | 15.3768 ns | 0.8429 ns | 48.70 |    1.37 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Ascii  | 29.8199 ns |  3.4033 ns | 0.1865 ns | 55.53 |    0.35 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Ascii  |  1.7143 ns |  0.1643 ns | 0.0090 ns |  3.19 |    0.02 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Ascii  |  0.5660 ns |  0.3671 ns | 0.0201 ns |  1.05 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Ascii  |  0.5373 ns |  0.0564 ns | 0.0031 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Ascii  | 29.5522 ns |  3.8662 ns | 0.2119 ns | 55.03 |    0.38 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Ascii  | 35.3671 ns |  0.3520 ns | 0.0193 ns | 65.86 |    0.20 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Ascii  |  1.7948 ns |  0.6371 ns | 0.0349 ns |  3.34 |    0.06 |         - |          NA |
|                             |    |        |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Cjk**    |  **0.5458 ns** |  **0.8225 ns** | **0.0451 ns** |  **1.00** |    **0.10** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Cjk    |  0.5158 ns |  0.1053 ns | 0.0058 ns |  0.95 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Cjk    | 22.4112 ns |  1.5872 ns | 0.0870 ns | 41.24 |    2.82 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Cjk    | 24.1035 ns |  1.4879 ns | 0.0816 ns | 44.35 |    3.03 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Cjk    |  1.7043 ns |  0.0475 ns | 0.0026 ns |  3.14 |    0.21 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Cjk    |  0.5462 ns |  0.1415 ns | 0.0078 ns |  1.01 |    0.07 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Cjk    |  0.5117 ns |  0.0360 ns | 0.0020 ns |  0.94 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Cjk    | 22.4898 ns |  4.2693 ns | 0.2340 ns | 41.38 |    2.85 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Cjk    | 24.1081 ns |  2.4479 ns | 0.1342 ns | 44.36 |    3.04 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Cjk    |  1.6414 ns |  0.2177 ns | 0.0119 ns |  3.02 |    0.21 |         - |          NA |
|                             |    |        |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Emoji**  |  **0.5443 ns** |  **0.0300 ns** | **0.0016 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Emoji  |  0.5238 ns |  0.1548 ns | 0.0085 ns |  0.96 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Emoji  | 22.9663 ns |  1.7072 ns | 0.0936 ns | 42.19 |    0.19 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Emoji  | 25.1418 ns |  0.2682 ns | 0.0147 ns | 46.19 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Emoji  |  1.7184 ns |  0.0641 ns | 0.0035 ns |  3.16 |    0.01 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Emoji  |  0.5661 ns |  0.0881 ns | 0.0048 ns |  1.04 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Emoji  |  0.5344 ns |  0.0626 ns | 0.0034 ns |  0.98 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Emoji  | 22.6423 ns |  3.0654 ns | 0.1680 ns | 41.60 |    0.29 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Emoji  | 25.0921 ns |  2.4865 ns | 0.1363 ns | 46.10 |    0.25 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Emoji  |  1.6436 ns |  0.2091 ns | 0.0115 ns |  3.02 |    0.02 |         - |          NA |
|                             |    |        |            |            |           |       |         |           |             |
| **string.EndsWith**             | **64** | **Mixed**  |  **0.5504 ns** |  **0.0385 ns** | **0.0021 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64 | Mixed  |  0.5201 ns |  0.0132 ns | 0.0007 ns |  0.95 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64 | Mixed  | 35.4831 ns | 31.9963 ns | 1.7538 ns | 64.47 |    2.77 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64 | Mixed  | 41.1409 ns |  6.0749 ns | 0.3330 ns | 74.75 |    0.58 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64 | Mixed  |  1.8271 ns |  0.2663 ns | 0.0146 ns |  3.32 |    0.03 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64 | Mixed  |  0.5490 ns |  0.1243 ns | 0.0068 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64 | Mixed  |  0.5819 ns |  0.1856 ns | 0.0102 ns |  1.06 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64 | Mixed  | 22.0114 ns |  1.1707 ns | 0.0642 ns | 39.99 |    0.17 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64 | Mixed  | 23.8863 ns |  2.5183 ns | 0.1380 ns | 43.40 |    0.26 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64 | Mixed  |  1.6494 ns |  0.0443 ns | 0.0024 ns |  3.00 |    0.01 |         - |          NA |
