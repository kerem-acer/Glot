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
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.344 ns** |      **1.9193 ns** |     **0.1052 ns** |     **4.286 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.288 ns |      0.0323 ns |     0.0018 ns |     2.288 ns |     0.53 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     5.496 ns |      0.1656 ns |     0.0091 ns |     5.492 ns |     1.27 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |    11.535 ns |      0.2141 ns |     0.0117 ns |    11.537 ns |     2.66 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |    11.517 ns |      1.9084 ns |     0.1046 ns |    11.568 ns |     2.65 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     5.969 ns |     33.7085 ns |     1.8477 ns |     4.927 ns |     1.37 |    0.37 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.057 ns |      0.5538 ns |     0.0304 ns |     2.052 ns |     0.47 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.000 ns |      0.7075 ns |     0.0388 ns |     2.986 ns |     0.69 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     7.973 ns |      1.7616 ns |     0.0966 ns |     8.027 ns |     1.84 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     9.162 ns |      7.4729 ns |     0.4096 ns |     9.330 ns |     2.11 |    0.09 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.116 ns** |      **0.2110 ns** |     **0.0116 ns** |     **2.112 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.434 ns |      0.2850 ns |     0.0156 ns |     1.437 ns |     0.68 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |     6.576 ns |      0.8862 ns |     0.0486 ns |     6.555 ns |     3.11 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |    27.641 ns |      5.2369 ns |     0.2871 ns |    27.535 ns |    13.06 |    0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |    26.596 ns |     15.8851 ns |     0.8707 ns |    26.972 ns |    12.57 |    0.36 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     4.848 ns |      0.9612 ns |     0.0527 ns |     4.820 ns |     2.29 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.952 ns |      0.6593 ns |     0.0361 ns |     5.943 ns |     2.81 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.851 ns |      0.3253 ns |     0.0178 ns |     6.859 ns |     3.24 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.882 ns |     11.7509 ns |     0.6441 ns |    17.083 ns |     7.98 |    0.27 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    23.042 ns |      0.5841 ns |     0.0320 ns |    23.024 ns |    10.89 |    0.05 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.850 ns** |      **6.7638 ns** |     **0.3707 ns** |     **5.001 ns** |     **1.00** |    **0.10** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.562 ns |      0.3112 ns |     0.0171 ns |     2.565 ns |     0.53 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     6.800 ns |      1.7437 ns |     0.0956 ns |     6.775 ns |     1.41 |    0.10 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |    12.711 ns |      1.4207 ns |     0.0779 ns |    12.721 ns |     2.63 |    0.18 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |    12.853 ns |      2.4347 ns |     0.1335 ns |    12.849 ns |     2.66 |    0.19 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     6.131 ns |     37.5072 ns |     2.0559 ns |     4.955 ns |     1.27 |    0.38 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.864 ns |      0.5034 ns |     0.0276 ns |     2.860 ns |     0.59 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.471 ns |      0.9605 ns |     0.0526 ns |     3.490 ns |     0.72 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |    13.583 ns |      2.0417 ns |     0.1119 ns |    13.626 ns |     2.81 |    0.19 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    19.401 ns |      1.9352 ns |     0.1061 ns |    19.376 ns |     4.02 |    0.28 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.380 ns** |      **0.4447 ns** |     **0.0244 ns** |     **2.389 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.529 ns |      0.3423 ns |     0.0188 ns |     1.537 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     9.945 ns |     86.9829 ns |     4.7678 ns |     7.195 ns |     4.18 |    1.74 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |    13.827 ns |      3.0064 ns |     0.1648 ns |    13.789 ns |     5.81 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |    13.676 ns |      1.6133 ns |     0.0884 ns |    13.704 ns |     5.75 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   269.416 ns |     26.1483 ns |     1.4333 ns |   269.627 ns |   113.19 |    1.14 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   147.405 ns |    158.5102 ns |     8.6885 ns |   147.257 ns |    61.93 |    3.21 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   139.024 ns |     30.0341 ns |     1.6463 ns |   139.525 ns |    58.41 |    0.79 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   143.718 ns |     40.5681 ns |     2.2237 ns |   142.774 ns |    60.38 |    0.97 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   144.064 ns |     17.0180 ns |     0.9328 ns |   143.570 ns |    60.53 |    0.64 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.141 ns** |      **0.4770 ns** |     **0.0261 ns** |     **2.139 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     2.434 ns |     19.1199 ns |     1.0480 ns |     1.859 ns |     1.14 |    0.42 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     9.710 ns |      6.6930 ns |     0.3669 ns |     9.524 ns |     4.54 |    0.16 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |    29.878 ns |     16.5018 ns |     0.9045 ns |    29.583 ns |    13.95 |    0.39 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |    26.927 ns |     20.2641 ns |     1.1107 ns |    26.298 ns |    12.58 |    0.47 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   269.460 ns |     15.1225 ns |     0.8289 ns |   269.735 ns |   125.85 |    1.37 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   409.181 ns |    233.8093 ns |    12.8159 ns |   413.993 ns |   191.11 |    5.56 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   398.470 ns |     34.0309 ns |     1.8653 ns |   399.155 ns |   186.11 |    2.10 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   421.809 ns |    297.6361 ns |    16.3144 ns |   413.775 ns |   197.01 |    6.92 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   423.483 ns |     73.8684 ns |     4.0490 ns |   421.998 ns |   197.79 |    2.65 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **4.711 ns** |     **34.9316 ns** |     **1.9147 ns** |     **3.638 ns** |     **1.10** |    **0.51** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.280 ns |      0.5912 ns |     0.0324 ns |     2.286 ns |     0.53 |    0.15 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |    10.390 ns |      0.3057 ns |     0.0168 ns |    10.386 ns |     2.42 |    0.69 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |    16.910 ns |      3.8119 ns |     0.2089 ns |    17.004 ns |     3.94 |    1.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |    17.082 ns |      0.7551 ns |     0.0414 ns |    17.070 ns |     3.98 |    1.14 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   265.173 ns |      9.1156 ns |     0.4997 ns |   265.111 ns |    61.81 |   17.63 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   183.621 ns |    202.1097 ns |    11.0783 ns |   178.269 ns |    42.80 |   12.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   179.130 ns |     28.7269 ns |     1.5746 ns |   178.943 ns |    41.75 |   11.91 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   202.777 ns |    441.4765 ns |    24.1988 ns |   189.121 ns |    47.26 |   14.40 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   195.289 ns |     20.1644 ns |     1.1053 ns |   195.211 ns |    45.52 |   12.99 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.645 ns** |      **1.2468 ns** |     **0.0683 ns** |     **3.634 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     2.495 ns |     16.8040 ns |     0.9211 ns |     1.969 ns |     0.68 |    0.22 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  |    10.282 ns |      2.9405 ns |     0.1612 ns |    10.215 ns |     2.82 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  |    16.931 ns |      1.5871 ns |     0.0870 ns |    16.906 ns |     4.65 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  |    18.100 ns |     30.8486 ns |     1.6909 ns |    17.236 ns |     4.97 |    0.41 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 5,271.048 ns | 34,788.3955 ns | 1,906.8694 ns | 4,208.736 ns | 1,446.60 |  453.87 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,783.172 ns |  1,640.5716 ns |    89.9253 ns | 1,821.847 ns |   489.38 |   22.79 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 2,084.285 ns | 10,244.1442 ns |   561.5161 ns | 1,815.038 ns |   572.02 |  133.79 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,288.257 ns |  6,961.9880 ns |   381.6101 ns | 2,087.215 ns |   627.99 |   91.28 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,064.755 ns |    136.2773 ns |     7.4698 ns | 2,061.095 ns |   566.66 |    9.33 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.525 ns** |      **0.6678 ns** |     **0.0366 ns** |     **2.515 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.773 ns |      0.2354 ns |     0.0129 ns |     1.778 ns |     0.70 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     6.475 ns |      1.5521 ns |     0.0851 ns |     6.492 ns |     2.56 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    |    27.058 ns |      5.7353 ns |     0.3144 ns |    26.977 ns |    10.72 |    0.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    |    26.867 ns |      5.9595 ns |     0.3267 ns |    26.817 ns |    10.64 |    0.17 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,177.325 ns |  1,200.4998 ns |    65.8034 ns | 4,189.087 ns | 1,654.64 |   30.60 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 5,082.893 ns |    273.6966 ns |    15.0022 ns | 5,081.328 ns | 2,013.33 |   25.66 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 5,044.843 ns |    801.6763 ns |    43.9426 ns | 5,033.784 ns | 1,998.26 |   29.15 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,323.124 ns |  2,041.7193 ns |   111.9135 ns | 6,292.381 ns | 2,504.59 |   49.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 5,080.118 ns |    939.1812 ns |    51.4797 ns | 5,084.615 ns | 2,012.23 |   30.71 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.857 ns** |      **0.8401 ns** |     **0.0460 ns** |     **2.844 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.864 ns |      0.6728 ns |     0.0369 ns |     1.844 ns |     0.65 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     7.316 ns |      1.0652 ns |     0.0584 ns |     7.336 ns |     2.56 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  |    13.605 ns |      3.8288 ns |     0.2099 ns |    13.707 ns |     4.76 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    14.455 ns |      2.7154 ns |     0.1488 ns |    14.450 ns |     5.06 |    0.08 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,172.703 ns |  1,071.0233 ns |    58.7064 ns | 4,183.674 ns | 1,460.53 |   26.96 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,210.729 ns |    330.6489 ns |    18.1240 ns | 2,202.972 ns |   773.80 |   12.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,228.949 ns |    243.1046 ns |    13.3254 ns | 2,232.626 ns |   780.18 |   11.55 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,713.024 ns |    412.7588 ns |    22.6247 ns | 2,709.639 ns |   949.62 |   14.85 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,199.974 ns |    115.4236 ns |     6.3268 ns | 2,199.296 ns |   770.04 |   10.85 |         - |          NA |
