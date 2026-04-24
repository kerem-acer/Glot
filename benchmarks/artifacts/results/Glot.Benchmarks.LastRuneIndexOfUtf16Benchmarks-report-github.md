```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean          | Error           | StdDev        | Median        | Ratio     | RatioSD  | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |--------------:|----------------:|--------------:|--------------:|----------:|---------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |      **4.432 ns** |       **0.3795 ns** |     **0.0208 ns** |      **4.425 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |      2.309 ns |       0.6372 ns |     0.0349 ns |      2.320 ns |      0.52 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     16.421 ns |       4.1192 ns |     0.2258 ns |     16.361 ns |      3.71 |     0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |      5.150 ns |      20.6651 ns |     1.1327 ns |      4.504 ns |      1.16 |     0.22 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |     17.526 ns |       6.6146 ns |     0.3626 ns |     17.629 ns |      3.95 |     0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |      4.756 ns |       2.5456 ns |     0.1395 ns |      4.726 ns |      1.07 |     0.03 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |      2.066 ns |       1.3643 ns |     0.0748 ns |      2.030 ns |      0.47 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     16.670 ns |       3.4606 ns |     0.1897 ns |     16.598 ns |      3.76 |     0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |      5.229 ns |       0.2542 ns |     0.0139 ns |      5.237 ns |      1.18 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     16.980 ns |      14.6900 ns |     0.8052 ns |     16.532 ns |      3.83 |     0.16 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |      **2.134 ns** |       **0.2980 ns** |     **0.0163 ns** |      **2.140 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |      1.404 ns |       0.0424 ns |     0.0023 ns |      1.404 ns |      0.66 |     0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |     19.408 ns |       3.2427 ns |     0.1777 ns |     19.340 ns |      9.10 |     0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |      2.827 ns |       0.1436 ns |     0.0079 ns |      2.826 ns |      1.33 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |     25.168 ns |       1.6379 ns |     0.0898 ns |     25.119 ns |     11.80 |     0.09 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |      4.782 ns |       1.5859 ns |     0.0869 ns |      4.827 ns |      2.24 |     0.04 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |      5.878 ns |       1.1164 ns |     0.0612 ns |      5.849 ns |      2.76 |     0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     22.099 ns |       5.1319 ns |     0.2813 ns |     21.945 ns |     10.36 |     0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |      4.746 ns |       0.5017 ns |     0.0275 ns |      4.731 ns |      2.22 |     0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |     26.563 ns |       4.1624 ns |     0.2282 ns |     26.605 ns |     12.45 |     0.12 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |      **3.092 ns** |       **0.6058 ns** |     **0.0332 ns** |      **3.078 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |      2.278 ns |       0.1431 ns |     0.0078 ns |      2.282 ns |      0.74 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Emoji  |     26.961 ns |       1.9498 ns |     0.1069 ns |     26.921 ns |      8.72 |     0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Emoji  |     11.681 ns |       0.9609 ns |     0.0527 ns |     11.703 ns |      3.78 |     0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Emoji  |     35.033 ns |       5.8703 ns |     0.3218 ns |     34.989 ns |     11.33 |     0.14 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |      4.784 ns |       0.3779 ns |     0.0207 ns |      4.780 ns |      1.55 |     0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |      4.490 ns |       0.1598 ns |     0.0088 ns |      4.494 ns |      1.45 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     22.938 ns |       6.1478 ns |     0.3370 ns |     23.092 ns |      7.42 |     0.12 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Emoji  |      4.872 ns |       1.3927 ns |     0.0763 ns |      4.842 ns |      1.58 |     0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Emoji  |     27.380 ns |       3.1713 ns |     0.1738 ns |     27.429 ns |      8.86 |     0.10 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |      **4.507 ns** |       **0.9046 ns** |     **0.0496 ns** |      **4.499 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |      2.500 ns |       0.2460 ns |     0.0135 ns |      2.503 ns |      0.55 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     21.519 ns |       5.3426 ns |     0.2928 ns |     21.560 ns |      4.78 |     0.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |     11.611 ns |       0.6452 ns |     0.0354 ns |     11.600 ns |      2.58 |     0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |     21.585 ns |       4.0624 ns |     0.2227 ns |     21.548 ns |      4.79 |     0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |      4.801 ns |       0.5250 ns |     0.0288 ns |      4.813 ns |      1.07 |     0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |      2.594 ns |       0.2116 ns |     0.0116 ns |      2.591 ns |      0.58 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     21.221 ns |       1.4843 ns |     0.0814 ns |     21.236 ns |      4.71 |     0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |      4.726 ns |       0.3090 ns |     0.0169 ns |      4.725 ns |      1.05 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |     26.688 ns |       3.1301 ns |     0.1716 ns |     26.706 ns |      5.92 |     0.07 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |      **2.330 ns** |       **0.1917 ns** |     **0.0105 ns** |      **2.325 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |      1.536 ns |       0.1159 ns |     0.0064 ns |      1.538 ns |      0.66 |     0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     14.787 ns |       3.5022 ns |     0.1920 ns |     14.765 ns |      6.35 |     0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |      2.818 ns |       0.1016 ns |     0.0056 ns |      2.816 ns |      1.21 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |     14.972 ns |       2.0683 ns |     0.1134 ns |     15.037 ns |      6.42 |     0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |    250.783 ns |       2.3832 ns |     0.1306 ns |    250.748 ns |    107.61 |     0.42 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |    131.595 ns |      23.0199 ns |     1.2618 ns |    131.214 ns |     56.47 |     0.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |    263.326 ns |      29.9636 ns |     1.6424 ns |    263.480 ns |    113.00 |     0.75 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |    364.166 ns |       6.8069 ns |     0.3731 ns |    364.132 ns |    156.27 |     0.62 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |    265.147 ns |      25.1252 ns |     1.3772 ns |    265.033 ns |    113.78 |     0.68 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |      **2.104 ns** |       **0.0783 ns** |     **0.0043 ns** |      **2.104 ns** |      **1.00** |     **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |      1.813 ns |       0.1784 ns |     0.0098 ns |      1.815 ns |      0.86 |     0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     19.749 ns |       3.1270 ns |     0.1714 ns |     19.662 ns |      9.39 |     0.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |      2.823 ns |       0.4377 ns |     0.0240 ns |      2.824 ns |      1.34 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |     25.665 ns |      15.3346 ns |     0.8405 ns |     25.260 ns |     12.20 |     0.35 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |    255.484 ns |      33.5093 ns |     1.8368 ns |    255.487 ns |    121.43 |     0.79 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |    374.608 ns |      42.8841 ns |     2.3506 ns |    375.497 ns |    178.05 |     1.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |    269.124 ns |       3.8389 ns |     0.2104 ns |    269.097 ns |    127.91 |     0.24 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |    252.716 ns |      24.5806 ns |     1.3473 ns |    252.444 ns |    120.11 |     0.59 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |    279.903 ns |      75.2937 ns |     4.1271 ns |    277.554 ns |    133.04 |     1.71 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |      **2.926 ns** |       **4.1835 ns** |     **0.2293 ns** |      **2.797 ns** |      **1.00** |     **0.09** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |      1.878 ns |       0.1062 ns |     0.0058 ns |      1.879 ns |      0.64 |     0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Emoji  |     25.666 ns |       0.3455 ns |     0.0189 ns |     25.674 ns |      8.81 |     0.57 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Emoji  |     10.471 ns |       1.3678 ns |     0.0750 ns |     10.509 ns |      3.59 |     0.23 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Emoji  |     32.939 ns |       2.1524 ns |     0.1180 ns |     32.925 ns |     11.30 |     0.73 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |    253.267 ns |      27.3580 ns |     1.4996 ns |    252.467 ns |     86.89 |     5.66 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |    353.936 ns |      71.9151 ns |     3.9419 ns |    353.403 ns |    121.43 |     7.97 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |    268.460 ns |      29.2441 ns |     1.6030 ns |    268.856 ns |     92.11 |     6.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Emoji  |    253.933 ns |      18.1291 ns |     0.9937 ns |    254.064 ns |     87.12 |     5.66 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Emoji  |    273.545 ns |       3.0699 ns |     0.1683 ns |    273.585 ns |     93.85 |     6.09 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |      **3.521 ns** |       **0.4296 ns** |     **0.0235 ns** |      **3.513 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |      2.268 ns |       1.4713 ns |     0.0806 ns |      2.271 ns |      0.64 |     0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |     21.771 ns |       1.1857 ns |     0.0650 ns |     21.796 ns |      6.18 |     0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |     10.654 ns |       2.4501 ns |     0.1343 ns |     10.671 ns |      3.03 |     0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |     23.918 ns |      13.1228 ns |     0.7193 ns |     23.644 ns |      6.79 |     0.18 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |    264.032 ns |      18.7082 ns |     1.0255 ns |    263.468 ns |     74.99 |     0.50 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |    171.901 ns |      45.3901 ns |     2.4880 ns |    171.951 ns |     48.82 |     0.67 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |    275.048 ns |      38.1509 ns |     2.0912 ns |    275.543 ns |     78.12 |     0.68 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |    258.930 ns |      23.9009 ns |     1.3101 ns |    258.297 ns |     73.54 |     0.53 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |    282.786 ns |      74.8547 ns |     4.1030 ns |    283.186 ns |     80.32 |     1.11 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |      **3.552 ns** |       **0.4510 ns** |     **0.0247 ns** |      **3.546 ns** |      **1.00** |     **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |      1.897 ns |       0.5445 ns |     0.0298 ns |      1.893 ns |      0.53 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  |     16.281 ns |       0.6936 ns |     0.0380 ns |     16.278 ns |      4.58 |     0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  |      3.773 ns |       0.2473 ns |     0.0136 ns |      3.767 ns |      1.06 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  |     16.345 ns |       1.0667 ns |     0.0585 ns |     16.364 ns |      4.60 |     0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  |  4,084.379 ns |   1,489.5645 ns |    81.6481 ns |  4,047.828 ns |  1,149.84 |    21.07 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  |  1,656.627 ns |     243.7200 ns |    13.3591 ns |  1,660.592 ns |    466.38 |     4.30 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  |  4,150.389 ns |     731.7349 ns |    40.1089 ns |  4,164.877 ns |  1,168.43 |    12.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  |  5,610.469 ns |   1,096.7058 ns |    60.1141 ns |  5,585.924 ns |  1,579.47 |    17.46 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  |  4,170.297 ns |     517.9868 ns |    28.3926 ns |  4,162.332 ns |  1,174.03 |     9.89 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |      **2.521 ns** |       **0.9239 ns** |     **0.0506 ns** |      **2.497 ns** |      **1.00** |     **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |      1.854 ns |       3.2760 ns |     0.1796 ns |      1.759 ns |      0.74 |     0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     20.641 ns |       0.3751 ns |     0.0206 ns |     20.637 ns |      8.19 |     0.14 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    |      3.056 ns |       0.3161 ns |     0.0173 ns |      3.059 ns |      1.21 |     0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    |     26.288 ns |       0.1119 ns |     0.0061 ns |     26.287 ns |     10.43 |     0.18 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    |  4,130.534 ns |     223.9511 ns |    12.2755 ns |  4,129.186 ns |  1,638.60 |    28.50 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    |  5,091.716 ns |     942.2189 ns |    51.6462 ns |  5,076.428 ns |  2,019.91 |    39.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    |  4,200.959 ns |     432.2168 ns |    23.6913 ns |  4,205.634 ns |  1,666.54 |    29.80 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    |  4,162.148 ns |     345.0231 ns |    18.9119 ns |  4,168.608 ns |  1,651.15 |    29.14 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    |  4,205.804 ns |     561.0940 ns |    30.7555 ns |  4,222.241 ns |  1,668.46 |    30.59 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |      **2.816 ns** |       **0.0270 ns** |     **0.0015 ns** |      **2.817 ns** |      **1.00** |     **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |      2.089 ns |       6.5561 ns |     0.3594 ns |      1.914 ns |      0.74 |     0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Emoji  |     43.245 ns |     492.6278 ns |    27.0026 ns |     27.730 ns |     15.35 |     8.30 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Emoji  |     85.965 ns |      29.4126 ns |     1.6122 ns |     86.262 ns |     30.52 |     0.50 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Emoji  |    191.980 ns |     142.0120 ns |     7.7842 ns |    194.777 ns |     68.16 |     2.39 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  | 21,774.208 ns |  18,332.0067 ns | 1,004.8391 ns | 22,029.769 ns |  7,731.08 |   309.00 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 42,961.826 ns | 138,314.8630 ns | 7,581.5044 ns | 42,831.014 ns | 15,253.88 | 2,331.23 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 27,326.152 ns |  62,944.2637 ns | 3,450.1875 ns | 26,751.946 ns |  9,702.33 | 1,060.90 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 21,697.187 ns |  59,486.4571 ns | 3,260.6535 ns | 20,954.038 ns |  7,703.73 | 1,002.62 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 20,583.300 ns |  65,558.8751 ns | 3,593.5032 ns | 20,233.307 ns |  7,308.24 | 1,104.96 |         - |          NA |
|                                    |       |        |               |                 |               |               |           |          |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **30.989 ns** |      **11.8084 ns** |     **0.6473 ns** |     **30.680 ns** |      **1.00** |     **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     19.196 ns |       4.4182 ns |     0.2422 ns |     19.140 ns |      0.62 |     0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     92.254 ns |     155.2883 ns |     8.5119 ns |     89.606 ns |      2.98 |     0.24 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  |     60.239 ns |     136.3697 ns |     7.4749 ns |     58.376 ns |      1.94 |     0.21 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    152.678 ns |     177.9124 ns |     9.7520 ns |    153.100 ns |      4.93 |     0.29 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 25,830.879 ns |  25,987.1705 ns | 1,424.4445 ns | 25,710.110 ns |    833.78 |    42.52 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 13,859.128 ns |   8,262.2885 ns |   452.8839 ns | 13,677.649 ns |    447.35 |    14.98 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 21,894.609 ns |  29,675.1452 ns | 1,626.5948 ns | 22,146.812 ns |    706.73 |    47.20 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 20,253.291 ns |  20,050.9716 ns | 1,099.0614 ns | 19,989.218 ns |    653.75 |    32.88 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 22,153.885 ns |  29,140.3627 ns | 1,597.2816 ns | 22,690.556 ns |    715.10 |    46.45 |         - |          NA |
