```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N     | Locale | Mean       | Error      | StdDev    | Median     | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-----------:|-----------:|----------:|-----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64**    | **Ascii**  |  **0.8287 ns** |  **8.2713 ns** | **0.4534 ns** |  **0.5683 ns** |  **1.18** |    **0.73** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Ascii  |  0.8252 ns |  8.3474 ns | 0.4575 ns |  0.5615 ns |  1.17 |    0.73 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Ascii  |  1.6667 ns |  4.5774 ns | 0.2509 ns |  1.5327 ns |  2.37 |    0.91 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Ascii  |  8.2426 ns |  0.5869 ns | 0.0322 ns |  8.2366 ns | 11.73 |    4.22 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Ascii  |  6.5944 ns |  0.4649 ns | 0.0255 ns |  6.5895 ns |  9.38 |    3.38 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Ascii  |  0.5674 ns |  0.5229 ns | 0.0287 ns |  0.5527 ns |  0.81 |    0.29 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.5648 ns |  0.2682 ns | 0.0147 ns |  0.5657 ns |  0.80 |    0.29 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  1.5589 ns |  0.3454 ns | 0.0189 ns |  1.5580 ns |  2.22 |    0.80 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Ascii  |  8.2465 ns |  0.1024 ns | 0.0056 ns |  8.2488 ns | 11.73 |    4.22 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Ascii  |  6.6704 ns |  1.9025 ns | 0.1043 ns |  6.6302 ns |  9.49 |    3.42 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **64**    | **Cjk**    |  **0.6164 ns** |  **1.0757 ns** | **0.0590 ns** |  **0.6353 ns** |  **1.01** |    **0.12** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Cjk    |  0.5515 ns |  0.2495 ns | 0.0137 ns |  0.5436 ns |  0.90 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Cjk    |  1.6675 ns |  0.1857 ns | 0.0102 ns |  1.6635 ns |  2.72 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Cjk    |  9.0856 ns |  0.4690 ns | 0.0257 ns |  9.0992 ns | 14.84 |    1.29 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Cjk    |  7.1936 ns |  0.1496 ns | 0.0082 ns |  7.1949 ns | 11.75 |    1.02 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Cjk    |  0.7797 ns |  7.4234 ns | 0.4069 ns |  0.5502 ns |  1.27 |    0.59 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.8320 ns |  4.4773 ns | 0.2454 ns |  0.9570 ns |  1.36 |    0.37 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  1.6486 ns |  2.4818 ns | 0.1360 ns |  1.5723 ns |  2.69 |    0.30 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Cjk    |  8.9859 ns |  0.0902 ns | 0.0049 ns |  8.9852 ns | 14.67 |    1.27 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Cjk    |  7.1306 ns |  0.7842 ns | 0.0430 ns |  7.1171 ns | 11.64 |    1.01 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **64**    | **Mixed**  |  **0.5191 ns** |  **0.1902 ns** | **0.0104 ns** |  **0.5136 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Mixed  |  0.5746 ns |  0.2239 ns | 0.0123 ns |  0.5791 ns |  1.11 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Mixed  |  1.5836 ns |  0.1020 ns | 0.0056 ns |  1.5842 ns |  3.05 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Mixed  |  8.2630 ns |  0.0945 ns | 0.0052 ns |  8.2641 ns | 15.92 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Mixed  |  6.6085 ns |  0.6510 ns | 0.0357 ns |  6.5887 ns | 12.73 |    0.23 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Mixed  |  0.5466 ns |  0.0675 ns | 0.0037 ns |  0.5481 ns |  1.05 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.7783 ns |  7.2224 ns | 0.3959 ns |  0.5500 ns |  1.50 |    0.66 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  2.1234 ns | 17.6729 ns | 0.9687 ns |  1.5652 ns |  4.09 |    1.62 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Mixed  |  9.1017 ns | 26.2022 ns | 1.4362 ns |  8.2868 ns | 17.54 |    2.42 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Mixed  |  6.6245 ns |  0.0787 ns | 0.0043 ns |  6.6227 ns | 12.76 |    0.22 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Ascii**  |  **0.6929 ns** |  **4.2663 ns** | **0.2339 ns** |  **0.5660 ns** |  **1.07** |    **0.41** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Ascii  |  0.5596 ns |  0.0568 ns | 0.0031 ns |  0.5581 ns |  0.86 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Ascii  |  1.5298 ns |  0.0309 ns | 0.0017 ns |  1.5294 ns |  2.36 |    0.58 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Ascii  |  8.2471 ns |  0.5499 ns | 0.0301 ns |  8.2368 ns | 12.71 |    3.11 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Ascii  |  6.5854 ns |  0.1499 ns | 0.0082 ns |  6.5849 ns | 10.15 |    2.49 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Ascii  |  0.5463 ns |  0.0373 ns | 0.0020 ns |  0.5454 ns |  0.84 |    0.21 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  0.5574 ns |  0.1958 ns | 0.0107 ns |  0.5517 ns |  0.86 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  1.5451 ns |  0.1969 ns | 0.0108 ns |  1.5419 ns |  2.38 |    0.58 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Ascii  |  8.1676 ns |  0.0206 ns | 0.0011 ns |  8.1672 ns | 12.59 |    3.08 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Ascii  |  6.6330 ns |  1.2762 ns | 0.0700 ns |  6.5997 ns | 10.22 |    2.51 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Cjk**    |  **0.5471 ns** |  **0.0306 ns** | **0.0017 ns** |  **0.5472 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Cjk    |  0.5659 ns |  0.2336 ns | 0.0128 ns |  0.5586 ns |  1.03 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Cjk    |  1.5806 ns |  0.1023 ns | 0.0056 ns |  1.5806 ns |  2.89 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Cjk    |  9.0751 ns |  1.1608 ns | 0.0636 ns |  9.0628 ns | 16.59 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Cjk    |  7.2622 ns |  2.0842 ns | 0.1142 ns |  7.2308 ns | 13.27 |    0.18 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Cjk    |  0.4954 ns |  0.4084 ns | 0.0224 ns |  0.4864 ns |  0.91 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.5133 ns |  0.2271 ns | 0.0124 ns |  0.5154 ns |  0.94 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  2.0350 ns | 14.8563 ns | 0.8143 ns |  1.5913 ns |  3.72 |    1.29 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Cjk    | 10.4478 ns | 24.2709 ns | 1.3304 ns | 10.5027 ns | 19.10 |    2.11 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Cjk    |  7.0925 ns |  0.1770 ns | 0.0097 ns |  7.0896 ns | 12.96 |    0.04 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Mixed**  |  **0.5445 ns** |  **0.1023 ns** | **0.0056 ns** |  **0.5447 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Mixed  |  0.5811 ns |  0.7325 ns | 0.0402 ns |  0.5656 ns |  1.07 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Mixed  |  1.6435 ns |  1.4840 ns | 0.0813 ns |  1.6828 ns |  3.02 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Mixed  |  8.2813 ns |  0.4967 ns | 0.0272 ns |  8.2940 ns | 15.21 |    0.14 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Mixed  |  6.6687 ns |  1.0658 ns | 0.0584 ns |  6.6903 ns | 12.25 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Mixed  |  0.5066 ns |  0.1352 ns | 0.0074 ns |  0.5063 ns |  0.93 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.5024 ns |  0.1714 ns | 0.0094 ns |  0.4976 ns |  0.92 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  2.0012 ns | 16.3650 ns | 0.8970 ns |  1.4890 ns |  3.68 |    1.43 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Mixed  |  8.6134 ns | 10.0644 ns | 0.5517 ns |  8.3200 ns | 15.82 |    0.89 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Mixed  |  8.6971 ns | 61.4049 ns | 3.3658 ns |  6.7715 ns | 15.98 |    5.36 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Ascii**  |  **0.5719 ns** |  **2.7016 ns** | **0.1481 ns** |  **0.4886 ns** |  **1.04** |    **0.31** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Ascii  |  0.5370 ns |  1.0571 ns | 0.0579 ns |  0.5205 ns |  0.98 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Ascii  |  1.6106 ns |  2.5013 ns | 0.1371 ns |  1.5600 ns |  2.93 |    0.61 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Ascii  |  8.5910 ns |  0.4383 ns | 0.0240 ns |  8.5965 ns | 15.63 |    3.05 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Ascii  |  6.9340 ns |  1.6969 ns | 0.0930 ns |  6.9483 ns | 12.61 |    2.47 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Ascii  |  0.5575 ns |  0.0637 ns | 0.0035 ns |  0.5560 ns |  1.01 |    0.20 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.7459 ns |  7.1791 ns | 0.3935 ns |  0.5211 ns |  1.36 |    0.68 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  1.5604 ns |  0.3374 ns | 0.0185 ns |  1.5678 ns |  2.84 |    0.55 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Ascii  |  8.6420 ns |  0.2552 ns | 0.0140 ns |  8.6488 ns | 15.72 |    3.07 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Ascii  |  6.9881 ns |  0.2771 ns | 0.0152 ns |  6.9868 ns | 12.71 |    2.48 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Cjk**    |  **0.7668 ns** |  **7.6543 ns** | **0.4196 ns** |  **0.5355 ns** |  **1.18** |    **0.73** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Cjk    |  0.4793 ns |  0.1197 ns | 0.0066 ns |  0.4768 ns |  0.74 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Cjk    |  2.0383 ns | 15.9182 ns | 0.8725 ns |  1.5387 ns |  3.13 |    1.67 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Cjk    |  9.6187 ns |  6.6647 ns | 0.3653 ns |  9.7157 ns | 14.79 |    5.36 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Cjk    |  7.7602 ns |  0.6208 ns | 0.0340 ns |  7.7563 ns | 11.93 |    4.31 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Cjk    |  0.5125 ns |  0.2125 ns | 0.0116 ns |  0.5135 ns |  0.79 |    0.28 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.5352 ns |  1.8580 ns | 0.1018 ns |  0.4807 ns |  0.82 |    0.33 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  1.5749 ns |  0.8040 ns | 0.0441 ns |  1.5773 ns |  2.42 |    0.88 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Cjk    |  9.5772 ns |  6.1681 ns | 0.3381 ns |  9.4828 ns | 14.73 |    5.34 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Cjk    |  7.4241 ns |  0.9391 ns | 0.0515 ns |  7.4446 ns | 11.42 |    4.12 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Mixed**  |  **0.5076 ns** |  **0.1245 ns** | **0.0068 ns** |  **0.5085 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Mixed  |  0.5804 ns |  2.9976 ns | 0.1643 ns |  0.4866 ns |  1.14 |    0.28 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Mixed  |  1.5539 ns |  0.1585 ns | 0.0087 ns |  1.5557 ns |  3.06 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Mixed  |  8.6592 ns |  2.1831 ns | 0.1197 ns |  8.6424 ns | 17.06 |    0.29 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Mixed  |  6.8104 ns |  0.4574 ns | 0.0251 ns |  6.8123 ns | 13.42 |    0.16 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Mixed  |  0.4894 ns |  0.3568 ns | 0.0196 ns |  0.4924 ns |  0.96 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.5148 ns |  0.1046 ns | 0.0057 ns |  0.5143 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  2.0366 ns | 14.5367 ns | 0.7968 ns |  1.6141 ns |  4.01 |    1.36 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Mixed  |  9.2459 ns | 21.3168 ns | 1.1684 ns |  8.5875 ns | 18.22 |    2.01 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Mixed  |  6.7479 ns |  0.7799 ns | 0.0427 ns |  6.7304 ns | 13.30 |    0.17 |         - |          NA |
