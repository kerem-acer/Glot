```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                         | N     | Locale | Mean         | Error      | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **2.664 ns** |  **0.0466 ns** |  **0.0435 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     2.040 ns |  0.0547 ns |  0.0512 ns |     0.77 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Ascii  |    19.592 ns |  0.1477 ns |  0.1234 ns |     7.36 |    0.13 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Ascii  |     7.679 ns |  0.1020 ns |  0.0954 ns |     2.88 |    0.06 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Ascii  |    20.506 ns |  0.0845 ns |  0.0706 ns |     7.70 |    0.12 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     4.439 ns |  0.0362 ns |  0.0302 ns |     1.67 |    0.03 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.768 ns |  0.0602 ns |  0.0563 ns |     1.04 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |    17.940 ns |  0.0897 ns |  0.0701 ns |     6.74 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.307 ns |  0.2173 ns |  0.2032 ns |     3.12 |    0.09 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |    19.026 ns |  0.1806 ns |  0.1410 ns |     7.14 |    0.12 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **1.271 ns** |  **0.0167 ns** |  **0.0140 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     1.616 ns |  0.0198 ns |  0.0165 ns |     1.27 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Cjk    |    27.621 ns |  0.3995 ns |  0.3541 ns |    21.73 |    0.35 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Cjk    |     5.956 ns |  0.1362 ns |  0.1274 ns |     4.69 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Cjk    |    34.668 ns |  0.3298 ns |  0.2754 ns |    27.27 |    0.35 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     4.454 ns |  0.0349 ns |  0.0291 ns |     3.50 |    0.04 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     7.736 ns |  0.1354 ns |  0.1200 ns |     6.09 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |    28.753 ns |  0.3973 ns |  0.3318 ns |    22.62 |    0.35 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |     7.153 ns |  0.1247 ns |  0.1166 ns |     5.63 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    35.319 ns |  0.2312 ns |  0.1931 ns |    27.78 |    0.33 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Emoji**  |     **1.841 ns** |  **0.0316 ns** |  **0.0295 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Emoji  |     1.666 ns |  0.0218 ns |  0.0193 ns |     0.91 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Emoji  |    27.633 ns |  0.2171 ns |  0.2031 ns |    15.01 |    0.26 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Emoji  |     5.667 ns |  0.0722 ns |  0.0676 ns |     3.08 |    0.06 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Emoji  |    35.272 ns |  0.2325 ns |  0.1942 ns |    19.16 |    0.31 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Emoji  |     4.405 ns |  0.0334 ns |  0.0261 ns |     2.39 |    0.04 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Emoji  |     6.861 ns |  0.1009 ns |  0.0788 ns |     3.73 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Emoji  |    28.427 ns |  0.2149 ns |  0.1678 ns |    15.44 |    0.25 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Emoji  |     7.198 ns |  0.1535 ns |  0.1436 ns |     3.91 |    0.10 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Emoji  |    35.556 ns |  0.2975 ns |  0.2484 ns |    19.32 |    0.33 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **2.161 ns** |  **0.0134 ns** |  **0.0112 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     1.965 ns |  0.0154 ns |  0.0120 ns |     0.91 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Mixed  |    20.631 ns |  0.2848 ns |  0.2378 ns |     9.55 |    0.12 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Mixed  |     9.550 ns |  0.1703 ns |  0.1593 ns |     4.42 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Mixed  |    21.807 ns |  0.1956 ns |  0.1633 ns |    10.09 |    0.09 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     4.386 ns |  0.0278 ns |  0.0217 ns |     2.03 |    0.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     3.268 ns |  0.0316 ns |  0.0264 ns |     1.51 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |    28.129 ns |  0.3072 ns |  0.2566 ns |    13.02 |    0.13 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |     7.233 ns |  0.1758 ns |  0.1645 ns |     3.35 |    0.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    35.417 ns |  0.5084 ns |  0.4755 ns |    16.39 |    0.23 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **2.686 ns** |  **0.0459 ns** |  **0.0383 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     2.010 ns |  0.0194 ns |  0.0172 ns |     0.75 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Ascii  |    19.798 ns |  0.4151 ns |  0.3883 ns |     7.37 |    0.17 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Ascii  |     7.606 ns |  0.1225 ns |  0.1023 ns |     2.83 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Ascii  |    20.547 ns |  0.1058 ns |  0.0883 ns |     7.65 |    0.11 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   258.132 ns |  1.0610 ns |  0.8283 ns |    96.13 |    1.35 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   131.972 ns |  1.3010 ns |  1.0864 ns |    49.15 |    0.78 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   271.954 ns |  2.4991 ns |  2.0868 ns |   101.28 |    1.58 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   447.690 ns | 10.6921 ns | 10.0014 ns |   166.73 |    4.27 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   278.925 ns |  7.6694 ns |  6.7987 ns |   103.88 |    2.83 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **1.267 ns** |  **0.0155 ns** |  **0.0129 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     1.598 ns |  0.0227 ns |  0.0212 ns |     1.26 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Cjk    |    27.690 ns |  0.2680 ns |  0.2238 ns |    21.85 |    0.27 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Cjk    |     5.880 ns |  0.0877 ns |  0.0778 ns |     4.64 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Cjk    |    34.289 ns |  0.2111 ns |  0.1648 ns |    27.06 |    0.29 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   257.815 ns |  3.8813 ns |  3.4407 ns |   203.43 |    3.29 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   380.784 ns |  3.1668 ns |  2.6444 ns |   300.46 |    3.55 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   282.457 ns |  3.7898 ns |  3.1647 ns |   222.87 |    3.24 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   265.238 ns |  8.3505 ns |  7.4025 ns |   209.29 |    6.00 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   289.479 ns |  2.1718 ns |  1.8136 ns |   228.41 |    2.62 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Emoji**  |     **1.841 ns** |  **0.0320 ns** |  **0.0299 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Emoji  |     1.660 ns |  0.0194 ns |  0.0162 ns |     0.90 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Emoji  |    27.642 ns |  0.2223 ns |  0.1736 ns |    15.02 |    0.25 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Emoji  |     5.592 ns |  0.0975 ns |  0.0912 ns |     3.04 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Emoji  |    35.354 ns |  0.5871 ns |  0.5492 ns |    19.21 |    0.42 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Emoji  |   268.930 ns |  6.5374 ns |  6.1151 ns |   146.12 |    3.94 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   405.200 ns |  4.8422 ns |  4.0434 ns |   220.16 |    4.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   282.008 ns |  2.3194 ns |  1.8108 ns |   153.23 |    2.56 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   259.060 ns |  1.5552 ns |  1.2987 ns |   140.76 |    2.29 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   291.272 ns |  5.1365 ns |  4.8047 ns |   158.26 |    3.53 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **2.174 ns** |  **0.0142 ns** |  **0.0125 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     1.947 ns |  0.0118 ns |  0.0092 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Mixed  |    20.659 ns |  0.3027 ns |  0.2363 ns |     9.50 |    0.12 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Mixed  |     9.387 ns |  0.1172 ns |  0.1096 ns |     4.32 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Mixed  |    21.859 ns |  0.2578 ns |  0.2412 ns |    10.06 |    0.12 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   265.231 ns |  4.4555 ns |  4.1676 ns |   122.02 |    1.98 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   169.829 ns |  2.2136 ns |  1.8484 ns |    78.13 |    0.93 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   284.262 ns |  5.0338 ns |  4.7087 ns |   130.77 |    2.22 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   264.655 ns |  1.6073 ns |  1.3421 ns |   121.75 |    0.90 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   288.785 ns |  1.3945 ns |  1.0887 ns |   132.85 |    0.88 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **2.550 ns** |  **0.0249 ns** |  **0.0208 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     2.016 ns |  0.0227 ns |  0.0201 ns |     0.79 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Ascii  |    19.642 ns |  0.3067 ns |  0.2869 ns |     7.70 |    0.12 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Ascii  |     7.577 ns |  0.1204 ns |  0.1126 ns |     2.97 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Ascii  |    20.981 ns |  0.4296 ns |  0.3809 ns |     8.23 |    0.16 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,004.406 ns | 31.3684 ns | 26.1940 ns | 1,570.28 |   15.76 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 2,007.313 ns | 32.3060 ns | 28.6384 ns |   787.14 |   12.47 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 4,066.339 ns | 65.0871 ns | 60.8826 ns | 1,594.56 |   26.27 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 7,000.485 ns | 66.6298 ns | 55.6389 ns | 2,745.15 |   30.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 4,039.352 ns | 41.2674 ns | 34.4601 ns | 1,583.98 |   17.96 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **1.261 ns** |  **0.0087 ns** |  **0.0068 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     1.628 ns |  0.0102 ns |  0.0080 ns |     1.29 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Cjk    |    27.737 ns |  0.2307 ns |  0.1926 ns |    22.00 |    0.19 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Cjk    |     5.940 ns |  0.1049 ns |  0.0981 ns |     4.71 |    0.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Cjk    |    34.587 ns |  0.3257 ns |  0.3047 ns |    27.44 |    0.27 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,018.000 ns | 38.6794 ns | 32.2991 ns | 3,187.39 |   29.71 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 6,015.075 ns | 55.2778 ns | 46.1595 ns | 4,771.62 |   43.09 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,052.429 ns | 43.6950 ns | 36.4873 ns | 3,214.70 |   32.49 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 3,971.070 ns | 16.6848 ns | 13.0264 ns | 3,150.16 |   19.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,036.197 ns | 24.4839 ns | 19.1154 ns | 3,201.82 |   22.08 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Emoji**  |     **1.855 ns** |  **0.0173 ns** |  **0.0144 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Emoji  |     1.657 ns |  0.0160 ns |  0.0133 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Emoji  |    28.309 ns |  0.7147 ns |  0.6336 ns |    15.26 |    0.35 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Emoji  |     5.543 ns |  0.0954 ns |  0.0846 ns |     2.99 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Emoji  |    35.222 ns |  0.4583 ns |  0.4287 ns |    18.99 |    0.27 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Emoji  | 4,018.979 ns | 57.6846 ns | 51.1359 ns | 2,166.47 |   31.19 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 6,421.279 ns | 62.5264 ns | 52.2124 ns | 3,461.45 |   37.52 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 4,035.886 ns | 35.8958 ns | 29.9746 ns | 2,175.58 |   22.54 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 3,973.668 ns | 29.4216 ns | 24.5684 ns | 2,142.04 |   20.50 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 4,048.065 ns | 31.5243 ns | 24.6121 ns | 2,182.14 |   20.73 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **2.194 ns** |  **0.0449 ns** |  **0.0420 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     1.998 ns |  0.0879 ns |  0.0779 ns |     0.91 |    0.04 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Mixed  |    20.881 ns |  0.3054 ns |  0.2857 ns |     9.52 |    0.21 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Mixed  |     9.357 ns |  0.1125 ns |  0.1052 ns |     4.27 |    0.09 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    21.821 ns |  0.1610 ns |  0.1344 ns |     9.95 |    0.19 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 3,987.283 ns | 21.5411 ns | 16.8179 ns | 1,818.18 |   33.98 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,602.489 ns | 36.9257 ns | 32.7336 ns | 1,186.72 |   26.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 4,021.586 ns | 26.3189 ns | 21.9774 ns | 1,833.82 |   34.82 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 3,987.923 ns | 27.1092 ns | 22.6374 ns | 1,818.47 |   34.63 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 4,028.509 ns | 32.4573 ns | 25.3405 ns | 1,836.98 |   35.31 |         - |          NA |
