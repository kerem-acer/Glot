```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean         | Error         | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|--------------:|-----------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.411 ns** |     **0.1797 ns** |  **0.0098 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.329 ns |     0.1051 ns |  0.0058 ns |     0.53 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     5.496 ns |     1.1698 ns |  0.0641 ns |     1.25 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |    11.705 ns |     2.4232 ns |  0.1328 ns |     2.65 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |    11.588 ns |     1.7517 ns |  0.0960 ns |     2.63 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     4.857 ns |     1.1749 ns |  0.0644 ns |     1.10 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.017 ns |     0.1652 ns |  0.0091 ns |     0.46 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.040 ns |     0.7702 ns |  0.0422 ns |     0.69 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.580 ns |     1.1147 ns |  0.0611 ns |     1.95 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.634 ns |     0.2506 ns |  0.0137 ns |     1.96 |    0.00 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.095 ns** |     **0.1656 ns** |  **0.0091 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.420 ns |     0.3507 ns |  0.0192 ns |     0.68 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |     6.572 ns |     0.5480 ns |  0.0300 ns |     3.14 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |    27.902 ns |     5.5909 ns |  0.3065 ns |    13.32 |    0.14 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |    25.789 ns |     2.3716 ns |  0.1300 ns |    12.31 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     5.218 ns |     2.2338 ns |  0.1224 ns |     2.49 |    0.05 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.958 ns |     0.9479 ns |  0.0520 ns |     2.84 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.914 ns |     0.9521 ns |  0.0522 ns |     3.30 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.744 ns |     3.9173 ns |  0.2147 ns |     7.99 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    23.364 ns |     2.2686 ns |  0.1243 ns |    11.15 |    0.07 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.413 ns** |     **0.6123 ns** |  **0.0336 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.590 ns |     0.5038 ns |  0.0276 ns |     0.59 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     6.722 ns |     1.0885 ns |  0.0597 ns |     1.52 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |    12.719 ns |     0.8589 ns |  0.0471 ns |     2.88 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |    12.857 ns |     1.0169 ns |  0.0557 ns |     2.91 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     4.816 ns |     0.9773 ns |  0.0536 ns |     1.09 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.697 ns |     0.3585 ns |  0.0196 ns |     0.61 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.490 ns |     0.2137 ns |  0.0117 ns |     0.79 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |    13.359 ns |     3.2393 ns |  0.1776 ns |     3.03 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    19.868 ns |     3.7554 ns |  0.2058 ns |     4.50 |    0.05 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.439 ns** |     **0.2434 ns** |  **0.0133 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.552 ns |     0.2392 ns |  0.0131 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     7.168 ns |     2.5981 ns |  0.1424 ns |     2.94 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |    14.057 ns |     5.3696 ns |  0.2943 ns |     5.76 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |    13.686 ns |     4.0142 ns |  0.2200 ns |     5.61 |    0.08 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   262.263 ns |    74.8577 ns |  4.1032 ns |   107.51 |    1.54 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   137.069 ns |     9.6117 ns |  0.5269 ns |    56.19 |    0.33 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   138.302 ns |    27.4460 ns |  1.5044 ns |    56.69 |    0.60 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   142.330 ns |    16.9959 ns |  0.9316 ns |    58.35 |    0.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   141.723 ns |    11.1826 ns |  0.6130 ns |    58.10 |    0.35 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.098 ns** |     **0.2924 ns** |  **0.0160 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.817 ns |     0.3948 ns |  0.0216 ns |     0.87 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     9.508 ns |     2.7272 ns |  0.1495 ns |     4.53 |    0.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |    29.486 ns |     6.0144 ns |  0.3297 ns |    14.05 |    0.16 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |    26.113 ns |     3.6296 ns |  0.1989 ns |    12.45 |    0.12 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   266.607 ns |    69.9390 ns |  3.8336 ns |   127.08 |    1.79 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   393.176 ns |    47.8816 ns |  2.6246 ns |   187.41 |    1.65 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   395.682 ns |   178.8857 ns |  9.8053 ns |   188.61 |    4.24 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   403.644 ns |    40.7861 ns |  2.2356 ns |   192.40 |    1.57 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   416.446 ns |    29.9809 ns |  1.6434 ns |   198.51 |    1.48 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.550 ns** |     **0.7047 ns** |  **0.0386 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.228 ns |     0.4827 ns |  0.0265 ns |     0.63 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |     9.998 ns |     0.9442 ns |  0.0518 ns |     2.82 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |    16.727 ns |     2.8279 ns |  0.1550 ns |     4.71 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |    16.841 ns |     2.2981 ns |  0.1260 ns |     4.74 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   263.333 ns |    40.8816 ns |  2.2409 ns |    74.18 |    0.88 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   173.509 ns |    18.2918 ns |  1.0026 ns |    48.88 |    0.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   178.150 ns |    14.1771 ns |  0.7771 ns |    50.19 |    0.51 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   184.064 ns |    22.0049 ns |  1.2062 ns |    51.85 |    0.57 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   191.738 ns |    44.2534 ns |  2.4257 ns |    54.01 |    0.78 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.597 ns** |     **0.9195 ns** |  **0.0504 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.926 ns |     0.3540 ns |  0.0194 ns |     0.54 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  |    10.001 ns |     2.2307 ns |  0.1223 ns |     2.78 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  |    16.335 ns |     0.5636 ns |  0.0309 ns |     4.54 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  |    17.540 ns |     2.6735 ns |  0.1465 ns |     4.88 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,129.759 ns |   213.8011 ns | 11.7192 ns | 1,148.22 |   14.32 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,664.341 ns |   104.0622 ns |  5.7040 ns |   462.75 |    5.82 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,653.457 ns |   395.0711 ns | 21.6552 ns |   459.72 |    7.67 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,075.519 ns |   106.5725 ns |  5.8416 ns |   577.07 |    7.19 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,090.626 ns |   293.5603 ns | 16.0910 ns |   581.27 |    8.09 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.490 ns** |     **0.7367 ns** |  **0.0404 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.831 ns |     0.4401 ns |  0.0241 ns |     0.74 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     6.327 ns |     2.6676 ns |  0.1462 ns |     2.54 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    |    26.610 ns |     4.3296 ns |  0.2373 ns |    10.69 |    0.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    |    26.409 ns |     4.3630 ns |  0.2392 ns |    10.61 |    0.17 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,100.023 ns |   405.3840 ns | 22.2205 ns | 1,646.86 |   24.56 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,945.555 ns | 1,086.0349 ns | 59.5292 ns | 1,986.48 |   34.92 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,998.051 ns | 1,165.9913 ns | 63.9119 ns | 2,007.57 |   36.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,099.382 ns |   754.2167 ns | 41.3412 ns | 2,449.94 |   37.54 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 5,010.350 ns | 1,168.7648 ns | 64.0639 ns | 2,012.51 |   36.16 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.813 ns** |     **0.2625 ns** |  **0.0144 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.780 ns |     0.2723 ns |  0.0149 ns |     0.63 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     7.187 ns |     0.9404 ns |  0.0515 ns |     2.55 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  |    13.236 ns |     1.2708 ns |  0.0697 ns |     4.71 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    14.015 ns |     1.9233 ns |  0.1054 ns |     4.98 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,113.601 ns | 1,452.8359 ns | 79.6348 ns | 1,462.37 |   25.35 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,181.731 ns |    10.9747 ns |  0.6016 ns |   775.60 |    3.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,186.833 ns |   543.9843 ns | 29.8176 ns |   777.41 |    9.80 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,633.204 ns |   181.8217 ns |  9.9663 ns |   936.09 |    5.15 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,169.425 ns |   333.7820 ns | 18.2957 ns |   771.22 |    6.58 |         - |          NA |
