```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | N     | Locale | Mean      | Error     | StdDev    | Median    | Ratio   | RatioSD | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |----------:|----------:|----------:|----------:|--------:|--------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  | **0.1093 ns** | **0.5718 ns** | **0.0313 ns** | **0.1164 ns** |    **1.07** |    **0.41** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  | 0.0261 ns | 0.4186 ns | 0.0229 ns | 0.0351 ns |    0.25 |    0.21 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  | 0.7857 ns | 0.2453 ns | 0.0134 ns | 0.7887 ns |    7.66 |    2.15 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  | 6.9755 ns | 0.4003 ns | 0.0219 ns | 6.9784 ns |   67.98 |   19.10 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  | 5.3388 ns | 0.1054 ns | 0.0058 ns | 5.3417 ns |   52.03 |   14.62 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  | 0.1924 ns | 0.0411 ns | 0.0023 ns | 0.1912 ns |    1.88 |    0.53 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 0.2715 ns | 0.2215 ns | 0.0121 ns | 0.2661 ns |    2.65 |    0.75 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 0.8299 ns | 0.0367 ns | 0.0020 ns | 0.8306 ns |    8.09 |    2.27 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  | 6.9976 ns | 0.7196 ns | 0.0394 ns | 6.9994 ns |   68.20 |   19.16 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  | 5.3849 ns | 0.2679 ns | 0.0147 ns | 5.3821 ns |   52.48 |   14.74 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    | **0.1811 ns** | **0.2877 ns** | **0.0158 ns** | **0.1735 ns** |   **1.005** |    **0.10** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    | 0.0046 ns | 0.1445 ns | 0.0079 ns | 0.0000 ns |   0.025 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    | 0.8818 ns | 0.1532 ns | 0.0084 ns | 0.8804 ns |   4.893 |    0.35 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    | 7.9431 ns | 2.9729 ns | 0.1630 ns | 7.9038 ns |  44.079 |    3.27 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    | 5.8931 ns | 1.1355 ns | 0.0622 ns | 5.8894 ns |  32.702 |    2.37 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    | 0.0850 ns | 0.2289 ns | 0.0125 ns | 0.0854 ns |   0.472 |    0.07 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 0.8324 ns | 0.4113 ns | 0.0225 ns | 0.8195 ns |   4.619 |    0.35 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    | 7.7978 ns | 0.2764 ns | 0.0152 ns | 7.7893 ns |  43.272 |    3.11 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    | 5.9302 ns | 0.6247 ns | 0.0342 ns | 5.9188 ns |  32.909 |    2.37 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  | **0.1182 ns** | **0.8500 ns** | **0.0466 ns** | **0.1221 ns** |   **1.129** |    **0.60** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  | 0.8891 ns | 0.4338 ns | 0.0238 ns | 0.9015 ns |   8.491 |    3.28 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  | 7.0791 ns | 1.8366 ns | 0.1007 ns | 7.0955 ns |  67.610 |   26.10 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  | 5.3673 ns | 1.0600 ns | 0.0581 ns | 5.3677 ns |  51.261 |   19.79 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  | 0.1571 ns | 0.4789 ns | 0.0263 ns | 0.1449 ns |   1.501 |    0.62 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 0.8773 ns | 0.7481 ns | 0.0410 ns | 0.8921 ns |   8.379 |    3.25 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  | 7.0355 ns | 0.9430 ns | 0.0517 ns | 7.0517 ns |  67.193 |   25.93 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  | 5.3107 ns | 0.3321 ns | 0.0182 ns | 5.3210 ns |  50.720 |   19.57 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  | **0.1444 ns** | **0.6507 ns** | **0.0357 ns** | **0.1549 ns** |    **1.05** |    **0.34** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  | 0.0278 ns | 0.1080 ns | 0.0059 ns | 0.0248 ns |    0.20 |    0.06 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  | 0.7866 ns | 0.1562 ns | 0.0086 ns | 0.7902 ns |    5.71 |    1.38 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  | 6.9461 ns | 0.2484 ns | 0.0136 ns | 6.9410 ns |   50.39 |   12.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  | 5.3404 ns | 1.1947 ns | 0.0655 ns | 5.3070 ns |   38.74 |    9.35 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  | 0.1607 ns | 0.7971 ns | 0.0437 ns | 0.1629 ns |    1.17 |    0.40 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 0.3154 ns | 0.8304 ns | 0.0455 ns | 0.2956 ns |    2.29 |    0.62 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 0.8215 ns | 0.0163 ns | 0.0009 ns | 0.8216 ns |    5.96 |    1.44 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  | 6.9884 ns | 0.7194 ns | 0.0394 ns | 6.9700 ns |   50.70 |   12.23 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  | 5.3649 ns | 0.7210 ns | 0.0395 ns | 5.3460 ns |   38.92 |    9.39 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    | **0.1857 ns** | **0.1249 ns** | **0.0068 ns** | **0.1848 ns** |   **1.001** |    **0.05** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    | 0.8352 ns | 0.0379 ns | 0.0021 ns | 0.8352 ns |   4.501 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    | 7.8554 ns | 0.5204 ns | 0.0285 ns | 7.8406 ns |  42.333 |    1.35 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    | 5.8407 ns | 0.0369 ns | 0.0020 ns | 5.8402 ns |  31.476 |    1.00 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    | 0.1351 ns | 0.4796 ns | 0.0263 ns | 0.1343 ns |   0.728 |    0.12 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 0.8254 ns | 0.1622 ns | 0.0089 ns | 0.8208 ns |   4.448 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    | 7.8527 ns | 0.3420 ns | 0.0187 ns | 7.8549 ns |  42.318 |    1.34 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    | 5.8544 ns | 0.1024 ns | 0.0056 ns | 5.8516 ns |  31.550 |    1.00 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  | **0.0715 ns** | **0.9373 ns** | **0.0514 ns** | **0.0862 ns** |   **2.147** |    **2.81** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  | 0.7981 ns | 0.0304 ns | 0.0017 ns | 0.7985 ns |  23.967 |   23.77 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  | 7.0153 ns | 0.5047 ns | 0.0277 ns | 7.0183 ns | 210.671 |  208.96 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  | 5.3776 ns | 0.2748 ns | 0.0151 ns | 5.3784 ns | 161.489 |  160.17 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  | 0.1692 ns | 0.2216 ns | 0.0121 ns | 0.1683 ns |   5.082 |    5.06 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 0.8178 ns | 0.0983 ns | 0.0054 ns | 0.8148 ns |  24.560 |   24.36 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  | 6.9701 ns | 0.3149 ns | 0.0173 ns | 6.9659 ns | 209.313 |  207.61 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  | 5.3842 ns | 0.3885 ns | 0.0213 ns | 5.3816 ns | 161.687 |  160.37 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  | **0.1546 ns** | **0.4249 ns** | **0.0233 ns** | **0.1507 ns** |    **1.01** |    **0.19** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  | 0.0730 ns | 0.6727 ns | 0.0369 ns | 0.0665 ns |    0.48 |    0.22 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  | 0.7949 ns | 0.0754 ns | 0.0041 ns | 0.7961 ns |    5.22 |    0.66 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  | 7.0485 ns | 0.3105 ns | 0.0170 ns | 7.0425 ns |   46.29 |    5.89 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  | 5.5927 ns | 0.1562 ns | 0.0086 ns | 5.5934 ns |   36.73 |    4.67 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  | 0.1489 ns | 0.5841 ns | 0.0320 ns | 0.1673 ns |    0.98 |    0.22 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 0.2751 ns | 0.0626 ns | 0.0034 ns | 0.2737 ns |    1.81 |    0.23 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 0.8192 ns | 0.2920 ns | 0.0160 ns | 0.8177 ns |    5.38 |    0.69 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  | 7.0784 ns | 0.2201 ns | 0.0121 ns | 7.0802 ns |   46.48 |    5.91 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  | 5.4996 ns | 0.3355 ns | 0.0184 ns | 5.5059 ns |   36.11 |    4.59 |         - |          NA |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    | **0.0027 ns** | **0.0442 ns** | **0.0024 ns** | **0.0035 ns** |       **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    | 0.8656 ns | 0.1510 ns | 0.0083 ns | 0.8618 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    | 7.9367 ns | 2.3336 ns | 0.1279 ns | 7.9279 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    | 5.8327 ns | 0.4331 ns | 0.0237 ns | 5.8235 ns |       ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    | 0.0063 ns | 0.1012 ns | 0.0055 ns | 0.0037 ns |       ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 0.8598 ns | 0.0432 ns | 0.0024 ns | 0.8595 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 7.7723 ns | 0.4183 ns | 0.0229 ns | 7.7744 ns |       ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    | 5.8791 ns | 0.4270 ns | 0.0234 ns | 5.8709 ns |       ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |         |         |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  | **0.1683 ns** | **0.9812 ns** | **0.0538 ns** | **0.1878 ns** |   **1.088** |    **0.48** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  | 0.8251 ns | 0.3192 ns | 0.0175 ns | 0.8162 ns |   5.337 |    1.77 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  | 6.9314 ns | 0.4248 ns | 0.0233 ns | 6.9187 ns |  44.836 |   14.85 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  | 5.3890 ns | 1.5044 ns | 0.0825 ns | 5.3699 ns |  34.859 |   11.56 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  | 0.1474 ns | 0.3329 ns | 0.0182 ns | 0.1525 ns |   0.953 |    0.33 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 0.8571 ns | 0.1443 ns | 0.0079 ns | 0.8553 ns |   5.544 |    1.84 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  | 6.9431 ns | 0.3420 ns | 0.0187 ns | 6.9344 ns |  44.911 |   14.88 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  | 5.3809 ns | 0.2583 ns | 0.0142 ns | 5.3732 ns |  34.806 |   11.53 |         - |          NA |
