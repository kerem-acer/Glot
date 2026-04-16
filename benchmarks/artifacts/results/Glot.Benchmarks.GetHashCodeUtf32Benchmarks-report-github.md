```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | N     | Locale | Mean          | Error          | StdDev        | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |--------------:|---------------:|--------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **2.014 ns** |      **0.2783 ns** |     **0.0153 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      1.183 ns |      0.1009 ns |     0.0055 ns |  0.59 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      2.740 ns |      0.1006 ns |     0.0055 ns |  1.36 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Latin**  |      **2.026 ns** |      **0.2522 ns** |     **0.0138 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Latin  |      3.343 ns |      0.7373 ns |     0.0404 ns |  1.65 |    0.02 |         - |          NA |
| Text.GetHashCode          | 8     | Latin  |      2.718 ns |      0.4105 ns |     0.0225 ns |  1.34 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **2.013 ns** |      **0.0670 ns** |     **0.0037 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      3.092 ns |      0.0892 ns |     0.0049 ns |  1.54 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      2.755 ns |      0.3064 ns |     0.0168 ns |  1.37 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Emoji**  |      **2.005 ns** |      **0.0171 ns** |     **0.0009 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Emoji  |      2.002 ns |      0.2574 ns |     0.0141 ns |  1.00 |    0.01 |         - |          NA |
| Text.GetHashCode          | 8     | Emoji  |      1.744 ns |      0.1585 ns |     0.0087 ns |  0.87 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **2.000 ns** |      **0.1996 ns** |     **0.0109 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      1.306 ns |      0.1774 ns |     0.0097 ns |  0.65 |    0.01 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      2.750 ns |      0.1637 ns |     0.0090 ns |  1.37 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Ascii**  |     **28.381 ns** |      **5.2690 ns** |     **0.2888 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Ascii  |      5.625 ns |      0.2378 ns |     0.0130 ns |  0.20 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Ascii  |     14.037 ns |      0.3938 ns |     0.0216 ns |  0.49 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Latin**  |     **28.316 ns** |      **7.0218 ns** |     **0.3849 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Latin  |      8.384 ns |      0.5975 ns |     0.0328 ns |  0.30 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Latin  |     14.051 ns |      1.5938 ns |     0.0874 ns |  0.50 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Cjk**    |     **28.333 ns** |      **8.0314 ns** |     **0.4402 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Cjk    |     15.810 ns |      2.5455 ns |     0.1395 ns |  0.56 |    0.01 |         - |          NA |
| Text.GetHashCode          | 64    | Cjk    |     13.971 ns |      1.3667 ns |     0.0749 ns |  0.49 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Emoji**  |     **28.448 ns** |     **10.7087 ns** |     **0.5870 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Emoji  |     10.874 ns |      4.6166 ns |     0.2530 ns |  0.38 |    0.01 |         - |          NA |
| Text.GetHashCode          | 64    | Emoji  |      5.997 ns |      0.9793 ns |     0.0537 ns |  0.21 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Mixed**  |     **28.506 ns** |     **10.7146 ns** |     **0.5873 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Mixed  |      8.651 ns |      0.3882 ns |     0.0213 ns |  0.30 |    0.01 |         - |          NA |
| Text.GetHashCode          | 64    | Mixed  |     14.017 ns |      0.3912 ns |     0.0214 ns |  0.49 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **139.174 ns** |      **0.7725 ns** |     **0.0423 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     20.533 ns |      4.0463 ns |     0.2218 ns |  0.15 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     33.554 ns |      5.6639 ns |     0.3105 ns |  0.24 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Latin**  |    **139.187 ns** |      **0.5267 ns** |     **0.0289 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Latin  |     27.380 ns |      5.2167 ns |     0.2859 ns |  0.20 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Latin  |     33.582 ns |      6.8646 ns |     0.3763 ns |  0.24 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **139.277 ns** |     **10.3150 ns** |     **0.5654 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     68.061 ns |     35.2775 ns |     1.9337 ns |  0.49 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     32.985 ns |      1.3850 ns |     0.0759 ns |  0.24 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Emoji**  |    **141.010 ns** |     **27.0651 ns** |     **1.4835 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Emoji  |     40.758 ns |     10.2227 ns |     0.5603 ns |  0.29 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Emoji  |     19.838 ns |      0.5895 ns |     0.0323 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **141.807 ns** |     **73.5458 ns** |     **4.0313 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     32.271 ns |     10.9039 ns |     0.5977 ns |  0.23 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     33.776 ns |      8.9528 ns |     0.4907 ns |  0.24 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Ascii**  |  **2,377.661 ns** |     **18.8759 ns** |     **1.0347 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Ascii  |    324.205 ns |     34.9921 ns |     1.9180 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Ascii  |    464.623 ns |     72.6329 ns |     3.9813 ns |  0.20 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Latin**  |  **2,384.578 ns** |    **293.5610 ns** |    **16.0911 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Latin  |    389.896 ns |      5.4214 ns |     0.2972 ns |  0.16 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Latin  |    460.990 ns |     65.5856 ns |     3.5950 ns |  0.19 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Cjk**    |  **2,404.404 ns** |    **938.6683 ns** |    **51.4516 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Cjk    |    971.773 ns |    200.6068 ns |    10.9959 ns |  0.40 |    0.01 |         - |          NA |
| Text.GetHashCode          | 4096  | Cjk    |    459.307 ns |     32.9443 ns |     1.8058 ns |  0.19 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Emoji**  |  **2,378.729 ns** |     **95.4120 ns** |     **5.2299 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Emoji  |    646.639 ns |     29.5876 ns |     1.6218 ns |  0.27 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Emoji  |    231.915 ns |     20.5921 ns |     1.1287 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Mixed**  |  **2,425.954 ns** |    **958.5385 ns** |    **52.5407 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Mixed  |    425.891 ns |     60.1132 ns |     3.2950 ns |  0.18 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Mixed  |    445.443 ns |     53.2980 ns |     2.9214 ns |  0.18 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **37,912.839 ns** |    **523.1045 ns** |    **28.6731 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,169.420 ns |  1,038.6577 ns |    56.9323 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Ascii  |  7,329.059 ns |    152.9366 ns |     8.3830 ns |  0.19 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Latin**  | **37,973.640 ns** |  **1,703.2462 ns** |    **93.3607 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Latin  |  6,220.576 ns |    441.9234 ns |    24.2233 ns |  0.16 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Latin  |  7,428.612 ns |    772.0004 ns |    42.3159 ns |  0.20 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **38,208.561 ns** |    **772.2019 ns** |    **42.3270 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 15,481.654 ns |    750.5762 ns |    41.1416 ns |  0.41 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Cjk    |  7,464.217 ns |  1,367.1918 ns |    74.9404 ns |  0.20 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Emoji**  | **38,772.060 ns** | **26,228.6000 ns** | **1,437.6781 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Emoji  | 10,377.508 ns |  1,705.8017 ns |    93.5007 ns |  0.27 |    0.01 |         - |          NA |
| Text.GetHashCode          | 65536 | Emoji  |  3,668.581 ns |     52.2399 ns |     2.8634 ns |  0.09 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **39,280.133 ns** | **32,505.1238 ns** | **1,781.7155 ns** |  **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  6,860.319 ns |  1,173.0421 ns |    64.2984 ns |  0.17 |    0.01 |         - |          NA |
| Text.GetHashCode          | 65536 | Mixed  |  7,036.732 ns |    121.0515 ns |     6.6352 ns |  0.18 |    0.01 |         - |          NA |
