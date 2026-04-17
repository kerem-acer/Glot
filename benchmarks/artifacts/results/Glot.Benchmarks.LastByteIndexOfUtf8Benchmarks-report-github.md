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
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.196 ns** |      **0.0549 ns** |     **0.0030 ns** |     **4.197 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.335 ns |      1.6922 ns |     0.0928 ns |     2.356 ns |     0.56 |    0.02 |         - |          NA |
| U8String.LastIndexOf               | 64    | Ascii  |     2.385 ns |      0.6771 ns |     0.0371 ns |     2.367 ns |     0.57 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |     2.980 ns |      0.1839 ns |     0.0101 ns |     2.982 ns |     0.71 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |    10.409 ns |     64.6454 ns |     3.5434 ns |     8.370 ns |     2.48 |    0.73 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |     8.815 ns |      7.5113 ns |     0.4117 ns |     8.578 ns |     2.10 |    0.08 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     4.717 ns |      0.4077 ns |     0.0223 ns |     4.711 ns |     1.12 |    0.00 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     1.954 ns |      0.0481 ns |     0.0026 ns |     1.953 ns |     0.47 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Ascii  |     2.330 ns |      4.3413 ns |     0.2380 ns |     2.194 ns |     0.56 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     2.733 ns |      0.0399 ns |     0.0022 ns |     2.733 ns |     0.65 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     7.501 ns |      0.1368 ns |     0.0075 ns |     7.501 ns |     1.79 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     7.575 ns |      0.2513 ns |     0.0138 ns |     7.574 ns |     1.81 |    0.00 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.023 ns** |      **0.2315 ns** |     **0.0127 ns** |     **2.019 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.325 ns |      0.0687 ns |     0.0038 ns |     1.325 ns |     0.66 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 64    | Cjk    |     1.612 ns |      1.6615 ns |     0.0911 ns |     1.564 ns |     0.80 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |     2.641 ns |      4.8084 ns |     0.2636 ns |     2.491 ns |     1.31 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |    23.143 ns |      2.1412 ns |     0.1174 ns |    23.112 ns |    11.44 |    0.08 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |    18.825 ns |      1.3168 ns |     0.0722 ns |    18.861 ns |     9.31 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     6.018 ns |     15.4939 ns |     0.8493 ns |     6.395 ns |     2.98 |    0.36 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.813 ns |      0.9676 ns |     0.0530 ns |     5.831 ns |     2.87 |    0.03 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Cjk    |     6.071 ns |      1.0331 ns |     0.0566 ns |     6.090 ns |     3.00 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.907 ns |      2.3317 ns |     0.1278 ns |     6.906 ns |     3.41 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.822 ns |      1.9786 ns |     0.1085 ns |    16.845 ns |     8.32 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    21.585 ns |      7.3804 ns |     0.4045 ns |    21.454 ns |    10.67 |    0.18 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.495 ns** |      **6.0123 ns** |     **0.3296 ns** |     **4.312 ns** |     **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.523 ns |      0.0887 ns |     0.0049 ns |     2.526 ns |     0.56 |    0.03 |         - |          NA |
| U8String.LastIndexOf               | 64    | Mixed  |     3.746 ns |     14.4360 ns |     0.7913 ns |     4.070 ns |     0.84 |    0.16 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |     3.338 ns |      1.2856 ns |     0.0705 ns |     3.322 ns |     0.75 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |     9.335 ns |      1.9572 ns |     0.1073 ns |     9.335 ns |     2.08 |    0.13 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |     9.422 ns |      0.5444 ns |     0.0298 ns |     9.410 ns |     2.10 |    0.13 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     4.929 ns |      0.3885 ns |     0.0213 ns |     4.917 ns |     1.10 |    0.07 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.723 ns |      1.1910 ns |     0.0653 ns |     2.738 ns |     0.61 |    0.04 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Mixed  |     2.797 ns |      0.9039 ns |     0.0495 ns |     2.796 ns |     0.62 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.303 ns |      0.8909 ns |     0.0488 ns |     3.295 ns |     0.74 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    12.583 ns |      5.0965 ns |     0.2794 ns |    12.447 ns |     2.81 |    0.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    22.569 ns |    129.2058 ns |     7.0822 ns |    18.872 ns |     5.04 |    1.41 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.336 ns** |      **0.5690 ns** |     **0.0312 ns** |     **2.330 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.540 ns |      0.3295 ns |     0.0181 ns |     1.545 ns |     0.66 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Ascii  |     1.662 ns |      0.4339 ns |     0.0238 ns |     1.675 ns |     0.71 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     2.624 ns |      0.8197 ns |     0.0449 ns |     2.599 ns |     1.12 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     7.978 ns |      2.4202 ns |     0.1327 ns |     7.989 ns |     3.42 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |     9.226 ns |     16.2788 ns |     0.8923 ns |     9.235 ns |     3.95 |    0.33 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   333.395 ns |  2,024.4670 ns |   110.9679 ns |   270.341 ns |   142.71 |   41.17 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   137.188 ns |     17.2834 ns |     0.9474 ns |   137.683 ns |    58.73 |    0.76 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Ascii  |   138.849 ns |     17.8782 ns |     0.9800 ns |   139.393 ns |    59.44 |    0.77 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   139.014 ns |     49.7078 ns |     2.7247 ns |   138.044 ns |    59.51 |    1.22 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   148.556 ns |    169.9602 ns |     9.3161 ns |   143.861 ns |    63.59 |    3.53 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   142.102 ns |     24.6253 ns |     1.3498 ns |   141.893 ns |    60.83 |    0.86 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.696 ns** |     **19.2331 ns** |     **1.0542 ns** |     **2.098 ns** |     **1.09** |    **0.49** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.835 ns |      0.6517 ns |     0.0357 ns |     1.848 ns |     0.74 |    0.21 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Cjk    |     2.018 ns |      0.4417 ns |     0.0242 ns |     2.018 ns |     0.82 |    0.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     3.919 ns |     35.3903 ns |     1.9399 ns |     2.806 ns |     1.59 |    0.83 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    24.199 ns |     25.1139 ns |     1.3766 ns |    23.405 ns |     9.79 |    2.75 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    19.504 ns |      2.7504 ns |     0.1508 ns |    19.422 ns |     7.89 |    2.18 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   282.610 ns |    657.1805 ns |    36.0223 ns |   262.599 ns |   114.32 |   34.17 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   454.524 ns |  1,938.0413 ns |   106.2306 ns |   393.987 ns |   183.86 |   63.72 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Cjk    |   421.798 ns |    514.0749 ns |    28.1782 ns |   430.483 ns |   170.63 |   48.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   457.739 ns |  1,901.2903 ns |   104.2161 ns |   399.036 ns |   185.16 |   63.57 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   438.908 ns |    623.5155 ns |    34.1770 ns |   447.283 ns |   177.55 |   50.59 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   414.265 ns |     59.2618 ns |     3.2483 ns |   413.115 ns |   167.58 |   46.32 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.546 ns** |      **1.4783 ns** |     **0.0810 ns** |     **3.521 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.720 ns |     15.8072 ns |     0.8664 ns |     2.250 ns |     0.77 |    0.21 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Mixed  |     2.445 ns |      0.8162 ns |     0.0447 ns |     2.457 ns |     0.69 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     4.128 ns |     33.9482 ns |     1.8608 ns |     3.066 ns |     1.16 |    0.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |    10.309 ns |     38.0229 ns |     2.0842 ns |     9.109 ns |     2.91 |    0.51 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |     8.858 ns |      0.8646 ns |     0.0474 ns |     8.839 ns |     2.50 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   275.015 ns |    134.7108 ns |     7.3840 ns |   278.318 ns |    77.59 |    2.36 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   202.644 ns |    851.4122 ns |    46.6688 ns |   176.706 ns |    57.17 |   11.46 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Mixed  |   174.547 ns |     50.7635 ns |     2.7825 ns |   173.374 ns |    49.25 |    1.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   205.673 ns |  1,011.8264 ns |    55.4616 ns |   174.305 ns |    58.03 |   13.60 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   182.843 ns |     15.1148 ns |     0.8285 ns |   182.470 ns |    51.59 |    1.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   221.249 ns |    924.7572 ns |    50.6891 ns |   193.605 ns |    62.42 |   12.45 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.496 ns** |      **0.4797 ns** |     **0.0263 ns** |     **3.486 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.894 ns |      0.1154 ns |     0.0063 ns |     1.895 ns |     0.54 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Ascii  |     2.058 ns |      0.1929 ns |     0.0106 ns |     2.062 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     3.058 ns |      4.9861 ns |     0.2733 ns |     3.216 ns |     0.87 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     8.587 ns |      1.5439 ns |     0.0846 ns |     8.619 ns |     2.46 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |     8.647 ns |      2.0590 ns |     0.1129 ns |     8.670 ns |     2.47 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,138.393 ns |  1,114.9889 ns |    61.1163 ns | 4,155.284 ns | 1,183.89 |   16.98 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,700.024 ns |    838.2577 ns |    45.9477 ns | 1,715.391 ns |   486.33 |   11.81 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Ascii  | 1,661.771 ns |    466.8076 ns |    25.5873 ns | 1,658.513 ns |   475.39 |    7.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 2,344.790 ns |  8,466.9014 ns |   464.0994 ns | 2,091.955 ns |   670.78 |  115.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,072.790 ns |    113.7184 ns |     6.2333 ns | 2,074.730 ns |   592.97 |    4.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,074.199 ns |    316.5304 ns |    17.3501 ns | 2,081.600 ns |   593.37 |    5.77 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.409 ns** |      **0.0787 ns** |     **0.0043 ns** |     **2.411 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.798 ns |      0.2875 ns |     0.0158 ns |     1.804 ns |     0.75 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Cjk    |     2.017 ns |      0.7062 ns |     0.0387 ns |     2.012 ns |     0.84 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     2.752 ns |      0.4329 ns |     0.0237 ns |     2.743 ns |     1.14 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    22.373 ns |      1.7815 ns |     0.0976 ns |    22.327 ns |     9.29 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    18.728 ns |      0.9958 ns |     0.0546 ns |    18.738 ns |     7.77 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,030.002 ns |    308.1088 ns |    16.8885 ns | 4,030.857 ns | 1,672.89 |    6.60 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 5,102.267 ns |  6,529.1517 ns |   357.8848 ns | 4,899.856 ns | 2,117.99 |  128.70 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Cjk    | 4,920.552 ns |    706.4547 ns |    38.7232 ns | 4,898.207 ns | 2,042.56 |   14.28 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 6,727.304 ns | 11,138.4306 ns |   610.5350 ns | 6,989.371 ns | 2,792.56 |  219.53 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 7,050.443 ns | 31,730.7332 ns | 1,739.2686 ns | 6,051.512 ns | 2,926.70 |  625.27 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,911.228 ns |    263.7238 ns |    14.4556 ns | 4,902.913 ns | 2,038.69 |    6.08 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.786 ns** |      **0.1765 ns** |     **0.0097 ns** |     **2.787 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     2.312 ns |     16.1646 ns |     0.8860 ns |     1.817 ns |     0.83 |    0.28 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Mixed  |     2.058 ns |      0.4976 ns |     0.0273 ns |     2.051 ns |     0.74 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     2.787 ns |      0.7422 ns |     0.0407 ns |     2.789 ns |     1.00 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     8.733 ns |      2.4329 ns |     0.1334 ns |     8.668 ns |     3.13 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |     9.727 ns |      9.6538 ns |     0.5292 ns |    10.028 ns |     3.49 |    0.16 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 5,053.157 ns | 30,254.9681 ns | 1,658.3769 ns | 4,098.477 ns | 1,813.67 |  515.51 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,216.176 ns |     63.2726 ns |     3.4682 ns | 2,215.467 ns |   795.42 |    2.63 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Mixed  | 2,136.395 ns |    161.0058 ns |     8.8253 ns | 2,131.545 ns |   766.79 |    3.58 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,659.768 ns |    244.5209 ns |    13.4030 ns | 2,654.187 ns |   954.64 |    5.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,725.624 ns |  2,620.5270 ns |   143.6399 ns | 2,656.645 ns |   978.27 |   44.75 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,690.011 ns | 15,513.7856 ns |   850.3629 ns | 2,213.393 ns |   965.49 |  264.34 |         - |          NA |
