```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean         | Error         | StdDev      | Median       | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|--------------:|------------:|-------------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **2.662 ns** |     **0.6467 ns** |   **0.0355 ns** |     **2.653 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     2.612 ns |    17.5348 ns |   0.9611 ns |     2.058 ns |     0.98 |    0.31 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Ascii  |     2.935 ns |     0.5304 ns |   0.0291 ns |     2.947 ns |     1.10 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Ascii  |     8.676 ns |     0.6278 ns |   0.0344 ns |     8.682 ns |     3.26 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Ascii  |     8.352 ns |     0.2479 ns |   0.0136 ns |     8.347 ns |     3.14 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     4.799 ns |     4.0794 ns |   0.2236 ns |     4.896 ns |     1.80 |    0.08 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.525 ns |     4.6035 ns |   0.2523 ns |     2.385 ns |     0.95 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.324 ns |     2.0967 ns |   0.1149 ns |     3.260 ns |     1.25 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.192 ns |     0.6075 ns |   0.0333 ns |     8.206 ns |     3.08 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.381 ns |     0.5983 ns |   0.0328 ns |     8.374 ns |     3.15 |    0.04 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **2.037 ns** |     **2.4019 ns** |   **0.1317 ns** |     **1.970 ns** |     **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     2.030 ns |    11.6050 ns |   0.6361 ns |     1.711 ns |     1.00 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Cjk    |     2.854 ns |     0.9992 ns |   0.0548 ns |     2.836 ns |     1.41 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Cjk    |    23.892 ns |     1.6135 ns |   0.0884 ns |    23.910 ns |    11.76 |    0.64 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Cjk    |    18.676 ns |     0.9348 ns |   0.0512 ns |    18.690 ns |     9.20 |    0.50 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     4.557 ns |     0.8007 ns |   0.0439 ns |     4.541 ns |     2.24 |    0.12 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     6.257 ns |     9.7915 ns |   0.5367 ns |     6.128 ns |     3.08 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.411 ns |     1.8776 ns |   0.1029 ns |     6.434 ns |     3.16 |    0.18 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    15.415 ns |    16.7892 ns |   0.9203 ns |    14.945 ns |     7.59 |    0.57 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    20.190 ns |     2.2977 ns |   0.1259 ns |    20.208 ns |     9.94 |    0.54 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **2.217 ns** |     **0.0464 ns** |   **0.0025 ns** |     **2.218 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     1.953 ns |     0.6991 ns |   0.0383 ns |     1.931 ns |     0.88 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Mixed  |     2.974 ns |     0.2859 ns |   0.0157 ns |     2.981 ns |     1.34 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Mixed  |     9.022 ns |     3.0703 ns |   0.1683 ns |     8.936 ns |     4.07 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Mixed  |     8.835 ns |     0.7384 ns |   0.0405 ns |     8.838 ns |     3.98 |    0.02 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     5.536 ns |    28.8457 ns |   1.5811 ns |     4.701 ns |     2.50 |    0.62 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.909 ns |     0.8219 ns |   0.0451 ns |     2.902 ns |     1.31 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.526 ns |     0.1741 ns |   0.0095 ns |     3.527 ns |     1.59 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    14.493 ns |    56.1922 ns |   3.0801 ns |    12.806 ns |     6.54 |    1.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    19.929 ns |    32.1075 ns |   1.7599 ns |    20.005 ns |     8.99 |    0.69 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **2.962 ns** |     **0.7415 ns** |   **0.0406 ns** |     **2.954 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     2.056 ns |     0.3574 ns |   0.0196 ns |     2.061 ns |     0.69 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     3.940 ns |    31.0998 ns |   1.7047 ns |     2.969 ns |     1.33 |    0.50 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     8.692 ns |     6.3163 ns |   0.3462 ns |     8.494 ns |     2.93 |    0.11 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Ascii  |    10.427 ns |    59.2174 ns |   3.2459 ns |     8.603 ns |     3.52 |    0.95 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   259.370 ns |    34.5189 ns |   1.8921 ns |   258.481 ns |    87.58 |    1.18 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   112.137 ns |   469.1866 ns |  25.7177 ns |    98.106 ns |    37.87 |    7.53 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   100.587 ns |   107.2330 ns |   5.8778 ns |    97.819 ns |    33.97 |    1.77 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   107.871 ns |     2.5530 ns |   0.1399 ns |   107.878 ns |    36.43 |    0.43 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   103.156 ns |    17.4053 ns |   0.9540 ns |   103.455 ns |    34.83 |    0.50 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **1.941 ns** |     **0.1685 ns** |   **0.0092 ns** |     **1.939 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     1.662 ns |     0.1824 ns |   0.0100 ns |     1.665 ns |     0.86 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     2.773 ns |     0.2490 ns |   0.0136 ns |     2.776 ns |     1.43 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    27.395 ns |   120.3376 ns |   6.5961 ns |    23.737 ns |    14.11 |    2.94 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    19.664 ns |    18.9687 ns |   1.0397 ns |    19.438 ns |    10.13 |    0.47 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   262.518 ns |    32.7221 ns |   1.7936 ns |   263.124 ns |   135.25 |    0.97 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   280.054 ns |    65.6457 ns |   3.5983 ns |   279.675 ns |   144.28 |    1.71 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   278.725 ns |    77.5646 ns |   4.2516 ns |   278.828 ns |   143.60 |    1.99 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   284.675 ns |    71.0018 ns |   3.8918 ns |   285.745 ns |   146.66 |    1.84 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   303.829 ns |    13.2554 ns |   0.7266 ns |   304.009 ns |   156.53 |    0.72 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **2.257 ns** |     **0.1917 ns** |   **0.0105 ns** |     **2.252 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     2.028 ns |     0.2282 ns |   0.0125 ns |     2.025 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     2.882 ns |     0.2599 ns |   0.0142 ns |     2.888 ns |     1.28 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     8.876 ns |     0.6663 ns |   0.0365 ns |     8.882 ns |     3.93 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Mixed  |    11.213 ns |    64.7871 ns |   3.5512 ns |     9.241 ns |     4.97 |    1.36 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   258.828 ns |    10.7687 ns |   0.5903 ns |   259.060 ns |   114.66 |    0.51 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   123.581 ns |    15.2508 ns |   0.8359 ns |   123.232 ns |    54.75 |    0.39 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   128.343 ns |    36.5035 ns |   2.0009 ns |   128.725 ns |    56.85 |    0.80 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   133.139 ns |    53.1117 ns |   2.9112 ns |   131.561 ns |    58.98 |    1.14 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   143.889 ns |    21.6730 ns |   1.1880 ns |   144.252 ns |    63.74 |    0.52 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **2.845 ns** |     **2.7413 ns** |   **0.1503 ns** |     **2.928 ns** |     **1.00** |    **0.07** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     2.245 ns |     2.0259 ns |   0.1110 ns |     2.253 ns |     0.79 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     2.919 ns |     0.2457 ns |   0.0135 ns |     2.914 ns |     1.03 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     8.764 ns |     1.5914 ns |   0.0872 ns |     8.796 ns |     3.09 |    0.15 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Ascii  |     8.414 ns |     1.6724 ns |   0.0917 ns |     8.385 ns |     2.96 |    0.14 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,132.547 ns | 1,922.9464 ns | 105.4032 ns | 4,104.168 ns | 1,455.39 |   75.83 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,471.549 ns |   159.5197 ns |   8.7438 ns | 1,467.142 ns |   518.25 |   24.59 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,514.385 ns |   845.3887 ns |  46.3386 ns | 1,520.375 ns |   533.33 |   28.87 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 1,435.186 ns |   323.0984 ns |  17.7101 ns | 1,444.017 ns |   505.44 |   24.45 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 1,446.352 ns |   237.6860 ns |  13.0284 ns | 1,452.706 ns |   509.37 |   24.36 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **2.470 ns** |    **17.4438 ns** |   **0.9562 ns** |     **1.959 ns** |     **1.09** |    **0.48** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     2.231 ns |    16.8413 ns |   0.9231 ns |     1.708 ns |     0.98 |    0.45 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     2.723 ns |     0.4175 ns |   0.0229 ns |     2.715 ns |     1.20 |    0.33 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    23.864 ns |     3.2155 ns |   0.1763 ns |    23.873 ns |    10.52 |    2.89 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    19.482 ns |     1.3221 ns |   0.0725 ns |    19.487 ns |     8.59 |    2.36 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,080.602 ns |   667.4946 ns |  36.5876 ns | 4,101.342 ns | 1,799.26 |  494.66 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,579.327 ns | 3,214.1590 ns | 176.1789 ns | 4,478.933 ns | 2,019.16 |  559.21 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,456.033 ns |   989.4055 ns |  54.2327 ns | 4,453.733 ns | 1,964.80 |  540.36 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,601.481 ns | 2,028.9730 ns | 111.2149 ns | 4,554.769 ns | 2,028.93 |  559.29 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,409.185 ns |   411.9035 ns |  22.5778 ns | 4,406.521 ns | 1,944.14 |  534.34 |         - |          NA |
|                                |       |        |              |               |             |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **2.167 ns** |     **0.0753 ns** |   **0.0041 ns** |     **2.165 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     2.632 ns |    17.9685 ns |   0.9849 ns |     2.080 ns |     1.21 |    0.39 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     3.003 ns |     4.5601 ns |   0.2500 ns |     2.873 ns |     1.39 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     9.127 ns |     2.4891 ns |   0.1364 ns |     9.166 ns |     4.21 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Mixed  |     9.038 ns |     1.8612 ns |   0.1020 ns |     9.015 ns |     4.17 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 3,975.326 ns |   209.4167 ns |  11.4788 ns | 3,975.658 ns | 1,834.52 |    5.50 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 1,981.188 ns |   302.5028 ns |  16.5812 ns | 1,983.014 ns |   914.27 |    6.80 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,009.742 ns |   751.7679 ns |  41.2069 ns | 2,029.434 ns |   927.45 |   16.54 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 1,894.188 ns |   140.4635 ns |   7.6993 ns | 1,891.206 ns |   874.12 |    3.40 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 1,991.938 ns |    82.7348 ns |   4.5350 ns | 1,992.197 ns |   919.23 |    2.36 |         - |          NA |
