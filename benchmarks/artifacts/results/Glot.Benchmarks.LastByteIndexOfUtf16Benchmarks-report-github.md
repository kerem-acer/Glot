```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                             | N     | Locale | Mean         | Error      | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.335 ns** |  **0.0409 ns** |  **0.0383 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.423 ns |  0.0508 ns |  0.0475 ns |     0.56 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |    16.847 ns |  0.2307 ns |  0.2045 ns |     3.89 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |     4.878 ns |  0.0432 ns |  0.0383 ns |     1.13 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |    18.208 ns |  0.2136 ns |  0.1998 ns |     4.20 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     4.856 ns |  0.0672 ns |  0.0628 ns |     1.12 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.393 ns |  0.0366 ns |  0.0342 ns |     0.55 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |    17.774 ns |  0.1482 ns |  0.1387 ns |     4.10 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     6.900 ns |  0.0680 ns |  0.0636 ns |     1.59 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |    18.913 ns |  0.5062 ns |  0.4735 ns |     4.36 |    0.11 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **1.855 ns** |  **0.0271 ns** |  **0.0253 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.335 ns |  0.0177 ns |  0.0166 ns |     0.72 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |    25.121 ns |  0.2871 ns |  0.2685 ns |    13.55 |    0.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |     3.172 ns |  0.0321 ns |  0.0300 ns |     1.71 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |    32.167 ns |  0.4218 ns |  0.3739 ns |    17.35 |    0.30 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     4.896 ns |  0.0669 ns |  0.0626 ns |     2.64 |    0.05 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     6.385 ns |  0.1047 ns |  0.0979 ns |     3.44 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |    28.769 ns |  0.5028 ns |  0.4703 ns |    15.51 |    0.32 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |     5.585 ns |  0.0545 ns |  0.0510 ns |     3.01 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    35.784 ns |  0.3394 ns |  0.3175 ns |    19.30 |    0.31 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |     **3.137 ns** |  **0.0402 ns** |  **0.0336 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |     2.349 ns |  0.0631 ns |  0.0590 ns |     0.75 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Emoji  |    26.502 ns |  0.3143 ns |  0.2940 ns |     8.45 |    0.13 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Emoji  |     6.033 ns |  0.0863 ns |  0.0807 ns |     1.92 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Emoji  |    33.814 ns |  0.2890 ns |  0.2413 ns |    10.78 |    0.13 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |     4.828 ns |  0.0473 ns |  0.0442 ns |     1.54 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |     5.606 ns |  0.0550 ns |  0.0515 ns |     1.79 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |    28.073 ns |  0.2400 ns |  0.2245 ns |     8.95 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |     5.568 ns |  0.0276 ns |  0.0245 ns |     1.77 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |    35.621 ns |  0.2221 ns |  0.2078 ns |    11.36 |    0.13 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.339 ns** |  **0.0191 ns** |  **0.0179 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.577 ns |  0.0320 ns |  0.0299 ns |     0.59 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |    17.270 ns |  0.1432 ns |  0.1340 ns |     3.98 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |     8.230 ns |  0.1872 ns |  0.1751 ns |     1.90 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |    18.489 ns |  0.1624 ns |  0.1519 ns |     4.26 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     4.853 ns |  0.0487 ns |  0.0432 ns |     1.12 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.853 ns |  0.0636 ns |  0.0595 ns |     0.66 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |    27.480 ns |  0.3502 ns |  0.3276 ns |     6.33 |    0.08 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |     5.589 ns |  0.0318 ns |  0.0282 ns |     1.29 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    34.690 ns |  0.2537 ns |  0.2373 ns |     8.00 |    0.06 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.324 ns** |  **0.0208 ns** |  **0.0195 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.515 ns |  0.0115 ns |  0.0108 ns |     0.65 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |    14.672 ns |  0.1108 ns |  0.0982 ns |     6.31 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     3.226 ns |  0.0268 ns |  0.0237 ns |     1.39 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |    15.788 ns |  0.1380 ns |  0.1152 ns |     6.79 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   259.529 ns |  2.4403 ns |  2.2827 ns |   111.68 |    1.31 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   140.993 ns |  0.8080 ns |  0.6747 ns |    60.67 |    0.57 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   274.546 ns |  2.7552 ns |  2.4424 ns |   118.14 |    1.39 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   486.132 ns | 11.4223 ns | 10.6844 ns |   209.19 |    4.76 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   277.894 ns |  4.1606 ns |  3.8919 ns |   119.58 |    1.89 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **1.857 ns** |  **0.0129 ns** |  **0.0120 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.822 ns |  0.0287 ns |  0.0269 ns |     0.98 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |    24.669 ns |  0.3546 ns |  0.3317 ns |    13.29 |    0.19 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |     3.238 ns |  0.0354 ns |  0.0313 ns |     1.74 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    32.048 ns |  0.2899 ns |  0.2421 ns |    17.26 |    0.17 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   261.556 ns |  2.5065 ns |  2.3446 ns |   140.87 |    1.51 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   387.757 ns |  6.5067 ns |  6.0864 ns |   208.85 |    3.43 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   294.743 ns |  4.4620 ns |  4.1737 ns |   158.75 |    2.39 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   265.530 ns |  5.5767 ns |  5.2164 ns |   143.01 |    2.86 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   294.210 ns |  5.4360 ns |  5.0848 ns |   158.46 |    2.83 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |     **2.711 ns** |  **0.0375 ns** |  **0.0351 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |     1.803 ns |  0.0221 ns |  0.0207 ns |     0.67 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Emoji  |    25.710 ns |  0.4973 ns |  0.4651 ns |     9.48 |    0.20 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Emoji  |     5.783 ns |  0.1181 ns |  0.1105 ns |     2.13 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Emoji  |    34.031 ns |  0.3311 ns |  0.2936 ns |    12.55 |    0.19 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |   273.449 ns |  1.8280 ns |  1.7099 ns |   100.88 |    1.40 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   394.197 ns |  4.7930 ns |  4.4833 ns |   145.42 |    2.42 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   297.430 ns |  3.6000 ns |  3.3675 ns |   109.72 |    1.82 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   270.760 ns |  3.6480 ns |  3.4124 ns |    99.88 |    1.74 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   301.667 ns |  5.2214 ns |  4.8841 ns |   111.29 |    2.23 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.531 ns** |  **0.0501 ns** |  **0.0469 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.216 ns |  0.0311 ns |  0.0291 ns |     0.63 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |    16.200 ns |  0.2840 ns |  0.2656 ns |     4.59 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     5.561 ns |  0.0674 ns |  0.0598 ns |     1.58 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |    17.580 ns |  0.3368 ns |  0.3151 ns |     4.98 |    0.11 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   261.380 ns |  3.6303 ns |  3.0315 ns |    74.04 |    1.25 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   172.809 ns |  2.0544 ns |  1.9217 ns |    48.95 |    0.82 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   281.735 ns |  2.5892 ns |  2.4219 ns |    79.80 |    1.21 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   264.092 ns |  5.4708 ns |  5.1174 ns |    74.81 |    1.70 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   295.757 ns |  3.0030 ns |  2.8090 ns |    83.78 |    1.31 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.520 ns** |  **0.0241 ns** |  **0.0214 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.875 ns |  0.0350 ns |  0.0328 ns |     0.53 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |    15.692 ns |  0.1850 ns |  0.1545 ns |     4.46 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     4.086 ns |  0.0431 ns |  0.0382 ns |     1.16 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |    16.711 ns |  0.1100 ns |  0.0975 ns |     4.75 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 3,978.657 ns | 28.9011 ns | 22.5641 ns | 1,130.37 |    9.08 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,991.216 ns | 19.3776 ns | 18.1258 ns |   565.72 |    6.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 4,028.126 ns | 31.4333 ns | 29.4027 ns | 1,144.43 |   10.54 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 7,422.568 ns | 81.8509 ns | 76.5634 ns | 2,108.82 |   24.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 4,031.300 ns | 33.2596 ns | 27.7733 ns | 1,145.33 |   10.17 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.257 ns** |  **0.0304 ns** |  **0.0284 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.791 ns |  0.0289 ns |  0.0256 ns |     0.79 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |    24.773 ns |  0.3280 ns |  0.3068 ns |    10.98 |    0.19 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |     3.390 ns |  0.0351 ns |  0.0328 ns |     1.50 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    31.582 ns |  0.2790 ns |  0.2474 ns |    13.99 |    0.20 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 3,994.559 ns | 42.8861 ns | 40.1157 ns | 1,769.82 |   27.60 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 6,016.088 ns | 64.0583 ns | 59.9202 ns | 2,665.48 |   41.43 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,018.068 ns | 33.9283 ns | 31.7365 ns | 1,780.24 |   25.62 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,008.443 ns | 55.2606 ns | 51.6908 ns | 1,775.97 |   30.99 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,020.191 ns | 20.9234 ns | 19.5718 ns | 1,781.18 |   23.28 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |     **2.772 ns** |  **0.0190 ns** |  **0.0178 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |     1.767 ns |  0.0242 ns |  0.0226 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Emoji  |    25.981 ns |  0.2916 ns |  0.2728 ns |     9.37 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Emoji  |     5.777 ns |  0.1415 ns |  0.1323 ns |     2.08 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Emoji  |    33.496 ns |  0.3040 ns |  0.2695 ns |    12.08 |    0.12 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  | 4,115.079 ns | 59.4770 ns | 55.6348 ns | 1,484.57 |   21.53 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 6,212.688 ns | 67.4280 ns | 59.7732 ns | 2,241.31 |   25.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 4,145.632 ns | 27.2527 ns | 24.1588 ns | 1,495.59 |   12.57 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 4,103.125 ns | 55.2044 ns | 51.6382 ns | 1,480.25 |   20.26 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 4,026.136 ns | 31.8052 ns | 28.1944 ns | 1,452.48 |   13.37 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.847 ns** |  **0.0278 ns** |  **0.0260 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.756 ns |  0.0382 ns |  0.0357 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |    15.404 ns |  0.3400 ns |  0.3180 ns |     5.41 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     3.541 ns |  0.0716 ns |  0.0669 ns |     1.24 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |    17.038 ns |  0.4756 ns |  0.4449 ns |     5.98 |    0.16 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,167.499 ns | 52.6340 ns | 49.2339 ns | 1,463.70 |   21.17 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,663.257 ns | 57.6606 ns | 53.9358 ns |   935.39 |   20.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 4,208.198 ns | 49.8381 ns | 46.6186 ns | 1,478.00 |   20.55 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 4,147.351 ns | 99.8846 ns | 88.5451 ns | 1,456.63 |   32.69 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 4,137.192 ns | 89.6874 ns | 83.8936 ns | 1,453.06 |   31.29 |         - |          NA |
