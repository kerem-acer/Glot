```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                       | Categories | N    | Locale | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------------- |----------- |----- |------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **string.Contains**              | **Contains**   | **256**  | **Ascii**  |  **2.7391 ns** | **0.3046 ns** | **0.0167 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.Contains same-enc&#39;     | Contains   | 256  | Ascii  |  7.4417 ns | 0.6961 ns | 0.0382 ns |  2.72 |    0.02 |         - |          NA |
| &#39;Text.Contains cross-enc&#39;    | Contains   | 256  | Ascii  | 14.3460 ns | 2.0334 ns | 0.1115 ns |  5.24 |    0.04 |         - |          NA |
| U8String.Contains            | Contains   | 256  | Ascii  |  2.3490 ns | 0.6639 ns | 0.0364 ns |  0.86 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.Contains**              | **Contains**   | **256**  | **Mixed**  |  **2.7002 ns** | **0.4019 ns** | **0.0220 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.Contains same-enc&#39;     | Contains   | 256  | Mixed  |  7.5578 ns | 0.8087 ns | 0.0443 ns |  2.80 |    0.02 |         - |          NA |
| &#39;Text.Contains cross-enc&#39;    | Contains   | 256  | Mixed  | 14.7416 ns | 1.9323 ns | 0.1059 ns |  5.46 |    0.05 |         - |          NA |
| U8String.Contains            | Contains   | 256  | Mixed  |  2.5864 ns | 0.4610 ns | 0.0253 ns |  0.96 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.Contains**              | **Contains**   | **4096** | **Ascii**  |  **2.7681 ns** | **0.3882 ns** | **0.0213 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.Contains same-enc&#39;     | Contains   | 4096 | Ascii  |  7.4301 ns | 0.7545 ns | 0.0414 ns |  2.68 |    0.02 |         - |          NA |
| &#39;Text.Contains cross-enc&#39;    | Contains   | 4096 | Ascii  | 13.6048 ns | 0.7287 ns | 0.0399 ns |  4.92 |    0.03 |         - |          NA |
| U8String.Contains            | Contains   | 4096 | Ascii  |  2.3156 ns | 0.1682 ns | 0.0092 ns |  0.84 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.Contains**              | **Contains**   | **4096** | **Mixed**  |  **2.6852 ns** | **0.3162 ns** | **0.0173 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.Contains same-enc&#39;     | Contains   | 4096 | Mixed  |  7.8112 ns | 0.1233 ns | 0.0068 ns |  2.91 |    0.02 |         - |          NA |
| &#39;Text.Contains cross-enc&#39;    | Contains   | 4096 | Mixed  | 13.8106 ns | 2.0688 ns | 0.1134 ns |  5.14 |    0.05 |         - |          NA |
| U8String.Contains            | Contains   | 4096 | Mixed  |  2.5758 ns | 0.4048 ns | 0.0222 ns |  0.96 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.IndexOf**               | **IndexOf**    | **256**  | **Ascii**  |  **2.6637 ns** | **0.8145 ns** | **0.0446 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.RuneIndexOf same-enc&#39;  | IndexOf    | 256  | Ascii  |  8.8835 ns | 0.1757 ns | 0.0096 ns |  3.34 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf cross-enc&#39; | IndexOf    | 256  | Ascii  | 15.6161 ns | 1.5909 ns | 0.0872 ns |  5.86 |    0.09 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 256  | Ascii  |  2.2739 ns | 0.1634 ns | 0.0090 ns |  0.85 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.IndexOf**               | **IndexOf**    | **256**  | **Mixed**  |  **2.6703 ns** | **0.8380 ns** | **0.0459 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.RuneIndexOf same-enc&#39;  | IndexOf    | 256  | Mixed  |  9.5805 ns | 1.6870 ns | 0.0925 ns |  3.59 |    0.06 |         - |          NA |
| &#39;Text.RuneIndexOf cross-enc&#39; | IndexOf    | 256  | Mixed  | 16.9620 ns | 2.6007 ns | 0.1426 ns |  6.35 |    0.11 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 256  | Mixed  |  2.5288 ns | 0.2772 ns | 0.0152 ns |  0.95 |    0.02 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.IndexOf**               | **IndexOf**    | **4096** | **Ascii**  |  **2.6891 ns** | **0.0958 ns** | **0.0052 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Text.RuneIndexOf same-enc&#39;  | IndexOf    | 4096 | Ascii  |  8.8887 ns | 1.4228 ns | 0.0780 ns |  3.31 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf cross-enc&#39; | IndexOf    | 4096 | Ascii  | 15.8458 ns | 1.4059 ns | 0.0771 ns |  5.89 |    0.03 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 4096 | Ascii  |  2.3084 ns | 0.0375 ns | 0.0021 ns |  0.86 |    0.00 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.IndexOf**               | **IndexOf**    | **4096** | **Mixed**  |  **2.6491 ns** | **0.3486 ns** | **0.0191 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.RuneIndexOf same-enc&#39;  | IndexOf    | 4096 | Mixed  |  9.6037 ns | 2.7412 ns | 0.1503 ns |  3.63 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf cross-enc&#39; | IndexOf    | 4096 | Mixed  | 16.0664 ns | 1.9800 ns | 0.1085 ns |  6.07 |    0.05 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 4096 | Mixed  |  2.5662 ns | 0.1873 ns | 0.0103 ns |  0.97 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.StartsWith**            | **StartsWith** | **256**  | **Ascii**  |  **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Text.StartsWith same-enc&#39;   | StartsWith | 256  | Ascii  |  5.8056 ns | 0.2542 ns | 0.0139 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 256  | Ascii  |  0.1654 ns | 0.0382 ns | 0.0021 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.StartsWith**            | **StartsWith** | **256**  | **Mixed**  |  **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Text.StartsWith same-enc&#39;   | StartsWith | 256  | Mixed  |  6.0277 ns | 0.1843 ns | 0.0101 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 256  | Mixed  |  0.1184 ns | 0.1267 ns | 0.0069 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.StartsWith**            | **StartsWith** | **4096** | **Ascii**  |  **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Text.StartsWith same-enc&#39;   | StartsWith | 4096 | Ascii  |  5.7504 ns | 0.2600 ns | 0.0143 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 4096 | Ascii  |  0.1669 ns | 0.0559 ns | 0.0031 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| **string.StartsWith**            | **StartsWith** | **4096** | **Mixed**  |  **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Text.StartsWith same-enc&#39;   | StartsWith | 4096 | Mixed  |  5.9942 ns | 0.9923 ns | 0.0544 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 4096 | Mixed  |  0.1455 ns | 0.0929 ns | 0.0051 ns |     ? |       ? |         - |           ? |
