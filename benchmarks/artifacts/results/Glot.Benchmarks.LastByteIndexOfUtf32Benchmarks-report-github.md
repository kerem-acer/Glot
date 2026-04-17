```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean         | Error          | StdDev        | Median       | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|---------------:|--------------:|-------------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.360 ns** |      **1.3835 ns** |     **0.0758 ns** |     **4.368 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     3.407 ns |     33.3624 ns |     1.8287 ns |     2.379 ns |     0.78 |    0.36 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |     3.307 ns |      4.1366 ns |     0.2267 ns |     3.242 ns |     0.76 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |     8.793 ns |      4.9410 ns |     0.2708 ns |     8.671 ns |     2.02 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |    10.069 ns |      4.2879 ns |     0.2350 ns |    10.058 ns |     2.31 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     5.248 ns |     11.8460 ns |     0.6493 ns |     4.954 ns |     1.20 |    0.13 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.095 ns |      1.0339 ns |     0.0567 ns |     2.120 ns |     0.48 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     2.852 ns |      0.6460 ns |     0.0354 ns |     2.839 ns |     0.65 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     9.538 ns |     62.7642 ns |     3.4403 ns |     7.588 ns |     2.19 |    0.68 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.347 ns |     10.3040 ns |     0.5648 ns |     8.599 ns |     1.91 |    0.12 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.082 ns** |      **2.0932 ns** |     **0.1147 ns** |     **2.025 ns** |     **1.00** |    **0.07** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.333 ns |      0.1510 ns |     0.0083 ns |     1.335 ns |     0.64 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |     2.487 ns |      0.4423 ns |     0.0242 ns |     2.498 ns |     1.20 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |    27.552 ns |    127.5003 ns |     6.9887 ns |    23.557 ns |    13.26 |    2.98 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |    18.940 ns |      0.9742 ns |     0.0534 ns |    18.927 ns |     9.11 |    0.42 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     4.814 ns |      1.0479 ns |     0.0574 ns |     4.814 ns |     2.32 |    0.11 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.833 ns |      1.6951 ns |     0.0929 ns |     5.795 ns |     2.81 |    0.14 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.737 ns |      0.5255 ns |     0.0288 ns |     6.747 ns |     3.24 |    0.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.245 ns |      3.4355 ns |     0.1883 ns |    16.217 ns |     7.82 |    0.37 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    22.330 ns |     47.2735 ns |     2.5912 ns |    20.834 ns |    10.75 |    1.19 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.213 ns** |      **0.1517 ns** |     **0.0083 ns** |     **4.216 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.485 ns |      0.2886 ns |     0.0158 ns |     2.492 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |     3.248 ns |      0.0274 ns |     0.0015 ns |     3.247 ns |     0.77 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |     9.009 ns |      0.5574 ns |     0.0306 ns |     9.022 ns |     2.14 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |    11.826 ns |     88.2910 ns |     4.8395 ns |     9.064 ns |     2.81 |    0.99 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     4.739 ns |      0.8895 ns |     0.0488 ns |     4.718 ns |     1.12 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.592 ns |      0.9061 ns |     0.0497 ns |     2.573 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.277 ns |      1.5264 ns |     0.0837 ns |     3.236 ns |     0.78 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    12.160 ns |      0.8136 ns |     0.0446 ns |    12.177 ns |     2.89 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    18.110 ns |      1.4921 ns |     0.0818 ns |    18.137 ns |     4.30 |    0.02 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.262 ns** |      **0.2515 ns** |     **0.0138 ns** |     **2.255 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.833 ns |      8.4730 ns |     0.4644 ns |     1.710 ns |     0.81 |    0.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     2.479 ns |      0.3716 ns |     0.0204 ns |     2.471 ns |     1.10 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     8.408 ns |      1.0013 ns |     0.0549 ns |     8.387 ns |     3.72 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |     8.442 ns |      6.3573 ns |     0.3485 ns |     8.264 ns |     3.73 |    0.13 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   266.603 ns |     27.4207 ns |     1.5030 ns |   266.764 ns |   117.88 |    0.85 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   136.804 ns |      8.0480 ns |     0.4411 ns |   136.588 ns |    60.49 |    0.36 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   138.011 ns |     14.5694 ns |     0.7986 ns |   138.015 ns |    61.02 |    0.44 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   141.543 ns |     13.6847 ns |     0.7501 ns |   141.226 ns |    62.58 |    0.44 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   141.596 ns |     11.3498 ns |     0.6221 ns |   141.793 ns |    62.61 |    0.41 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.025 ns** |      **0.1749 ns** |     **0.0096 ns** |     **2.021 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.720 ns |      0.1013 ns |     0.0056 ns |     1.719 ns |     0.85 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     2.749 ns |      0.5269 ns |     0.0289 ns |     2.748 ns |     1.36 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    22.606 ns |      1.3265 ns |     0.0727 ns |    22.606 ns |    11.16 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    19.095 ns |      2.4739 ns |     0.1356 ns |    19.168 ns |     9.43 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   273.812 ns |    166.3005 ns |     9.1155 ns |   269.094 ns |   135.22 |    3.94 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   402.202 ns |    205.6977 ns |    11.2750 ns |   406.373 ns |   198.62 |    4.89 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   406.324 ns |    466.9394 ns |    25.5945 ns |   392.720 ns |   200.65 |   10.98 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   401.738 ns |     38.8018 ns |     2.1269 ns |   400.592 ns |   198.39 |    1.22 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   411.818 ns |     31.3128 ns |     1.7164 ns |   411.953 ns |   203.37 |    1.11 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.474 ns** |      **0.1107 ns** |     **0.0061 ns** |     **3.473 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     3.151 ns |     31.2068 ns |     1.7106 ns |     2.172 ns |     0.91 |    0.43 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     3.124 ns |      4.6143 ns |     0.2529 ns |     2.983 ns |     0.90 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     8.852 ns |      0.2721 ns |     0.0149 ns |     8.849 ns |     2.55 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |     8.907 ns |      1.2581 ns |     0.0690 ns |     8.895 ns |     2.56 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   266.105 ns |     39.1054 ns |     2.1435 ns |   265.651 ns |    76.61 |    0.55 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   176.370 ns |    119.3801 ns |     6.5436 ns |   172.890 ns |    50.77 |    1.63 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   173.489 ns |      4.0560 ns |     0.2223 ns |   173.546 ns |    49.94 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   191.074 ns |    153.9231 ns |     8.4370 ns |   194.923 ns |    55.01 |    2.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   191.432 ns |     46.3545 ns |     2.5408 ns |   189.972 ns |    55.11 |    0.64 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.496 ns** |      **0.0675 ns** |     **0.0037 ns** |     **3.494 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.857 ns |      0.1977 ns |     0.0108 ns |     1.859 ns |     0.53 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     3.568 ns |     28.5268 ns |     1.5636 ns |     2.678 ns |     1.02 |    0.39 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     9.461 ns |     33.8118 ns |     1.8533 ns |     8.415 ns |     2.71 |    0.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |     8.482 ns |      3.5399 ns |     0.1940 ns |     8.385 ns |     2.43 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,026.825 ns |    195.1428 ns |    10.6964 ns | 4,031.532 ns | 1,151.82 |    2.85 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,853.960 ns |  7,064.2779 ns |   387.2169 ns | 1,631.302 ns |   530.30 |   95.92 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 2,010.019 ns |    311.2854 ns |    17.0626 ns | 2,000.718 ns |   574.94 |    4.26 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,011.771 ns |     30.2181 ns |     1.6564 ns | 2,011.469 ns |   575.44 |    0.67 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,489.154 ns | 13,414.9178 ns |   735.3169 ns | 2,098.297 ns |   711.99 |  182.15 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.464 ns** |      **0.1759 ns** |     **0.0096 ns** |     **2.458 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.761 ns |      0.0399 ns |     0.0022 ns |     1.759 ns |     0.71 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     2.726 ns |      0.0815 ns |     0.0045 ns |     2.726 ns |     1.11 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    22.798 ns |      3.9271 ns |     0.2153 ns |    22.695 ns |     9.25 |    0.08 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    19.639 ns |     18.8699 ns |     1.0343 ns |    19.148 ns |     7.97 |    0.36 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 5,181.703 ns | 37,377.9494 ns | 2,048.8115 ns | 4,005.522 ns | 2,103.40 |  720.29 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,883.716 ns |    191.1728 ns |    10.4788 ns | 4,888.657 ns | 1,982.44 |    7.65 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 6,050.986 ns |    219.4176 ns |    12.0270 ns | 6,055.246 ns | 2,456.27 |    9.32 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,035.044 ns |    215.8492 ns |    11.8314 ns | 6,034.810 ns | 2,449.79 |    9.27 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,887.850 ns |     66.0731 ns |     3.6217 ns | 4,888.648 ns | 1,984.12 |    6.83 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.829 ns** |      **1.6661 ns** |     **0.0913 ns** |     **2.778 ns** |     **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     2.857 ns |     34.5234 ns |     1.8923 ns |     1.765 ns |     1.01 |    0.58 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     2.953 ns |      5.6518 ns |     0.3098 ns |     2.778 ns |     1.04 |    0.10 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     8.933 ns |      1.1136 ns |     0.0610 ns |     8.919 ns |     3.16 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |     8.972 ns |      0.3182 ns |     0.0174 ns |     8.975 ns |     3.17 |    0.09 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,139.221 ns |    578.1796 ns |    31.6920 ns | 4,147.923 ns | 1,464.29 |   41.35 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,182.643 ns |    199.3630 ns |    10.9278 ns | 2,176.999 ns |   772.13 |   21.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,659.689 ns |     89.8291 ns |     4.9238 ns | 2,658.971 ns |   940.89 |   25.87 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,680.099 ns |    258.6614 ns |    14.1781 ns | 2,686.806 ns |   948.11 |   26.38 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,352.758 ns |  2,688.4572 ns |   147.3634 ns | 2,317.015 ns |   832.31 |   50.61 |         - |          NA |
