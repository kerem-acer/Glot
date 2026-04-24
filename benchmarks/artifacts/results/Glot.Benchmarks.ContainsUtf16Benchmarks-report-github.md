```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                      | N     | Locale | Mean         | Error       | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-------------:|------------:|------------:|---------:|--------:|----------:|------------:|
| **string.Contains**             | **64**    | **Ascii**  |     **2.669 ns** |   **0.0699 ns** |   **0.0654 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Ascii  |     2.135 ns |   0.0241 ns |   0.0201 ns |     0.80 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Ascii  |    15.346 ns |   0.3088 ns |   0.2888 ns |     5.75 |    0.17 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Ascii  |     3.711 ns |   0.0985 ns |   0.0921 ns |     1.39 |    0.05 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Ascii  |    16.714 ns |   0.3175 ns |   0.2970 ns |     6.26 |    0.18 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Ascii  |     4.472 ns |   0.1061 ns |   0.0941 ns |     1.68 |    0.05 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Ascii  |     2.974 ns |   0.0735 ns |   0.0688 ns |     1.11 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Ascii  |    17.300 ns |   0.0882 ns |   0.0736 ns |     6.48 |    0.15 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Ascii  |     7.894 ns |   0.2473 ns |   0.2314 ns |     2.96 |    0.11 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Ascii  |    18.823 ns |   0.3723 ns |   0.3483 ns |     7.06 |    0.21 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **64**    | **Cjk**    |     **1.284 ns** |   **0.0177 ns** |   **0.0148 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Cjk    |     1.712 ns |   0.0197 ns |   0.0184 ns |     1.33 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Cjk    |    25.389 ns |   0.4655 ns |   0.4354 ns |    19.78 |    0.39 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Cjk    |     2.966 ns |   0.0414 ns |   0.0387 ns |     2.31 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Cjk    |    32.621 ns |   0.3123 ns |   0.2921 ns |    25.42 |    0.36 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Cjk    |     4.587 ns |   0.0482 ns |   0.0450 ns |     3.57 |    0.05 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Cjk    |     8.345 ns |   0.1198 ns |   0.1121 ns |     6.50 |    0.11 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Cjk    |    28.913 ns |   0.3749 ns |   0.3507 ns |    22.53 |    0.36 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Cjk    |     6.940 ns |   0.1518 ns |   0.1420 ns |     5.41 |    0.12 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Cjk    |    35.532 ns |   0.4838 ns |   0.4525 ns |    27.68 |    0.46 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **64**    | **Emoji**  |     **1.903 ns** |   **0.0176 ns** |   **0.0164 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Emoji  |     1.777 ns |   0.0086 ns |   0.0077 ns |     0.93 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Emoji  |    26.215 ns |   0.7309 ns |   0.6479 ns |    13.78 |    0.35 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Emoji  |     2.917 ns |   0.0311 ns |   0.0260 ns |     1.53 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Emoji  |    33.689 ns |   0.3797 ns |   0.3552 ns |    17.71 |    0.23 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Emoji  |     4.578 ns |   0.0901 ns |   0.0843 ns |     2.41 |    0.05 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Emoji  |     7.183 ns |   0.1881 ns |   0.1759 ns |     3.78 |    0.09 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Emoji  |    28.622 ns |   0.2382 ns |   0.1989 ns |    15.04 |    0.16 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Emoji  |     6.920 ns |   0.1522 ns |   0.1423 ns |     3.64 |    0.08 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Emoji  |    36.473 ns |   0.4758 ns |   0.4451 ns |    19.17 |    0.28 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **64**    | **Mixed**  |     **2.272 ns** |   **0.0211 ns** |   **0.0197 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Mixed  |     2.089 ns |   0.0853 ns |   0.0712 ns |     0.92 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Mixed  |    15.433 ns |   0.1803 ns |   0.1598 ns |     6.79 |    0.09 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Mixed  |     4.406 ns |   0.0724 ns |   0.0678 ns |     1.94 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Mixed  |    17.114 ns |   0.1475 ns |   0.1379 ns |     7.53 |    0.09 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Mixed  |     4.596 ns |   0.0423 ns |   0.0396 ns |     2.02 |    0.02 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Mixed  |     3.736 ns |   0.1147 ns |   0.1016 ns |     1.64 |    0.05 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Mixed  |    28.783 ns |   0.1755 ns |   0.1642 ns |    12.67 |    0.13 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Mixed  |     6.941 ns |   0.0819 ns |   0.0726 ns |     3.06 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Mixed  |    35.999 ns |   0.5243 ns |   0.4904 ns |    15.85 |    0.25 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **4096**  | **Ascii**  |     **2.613 ns** |   **0.0461 ns** |   **0.0431 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Ascii  |     2.150 ns |   0.0187 ns |   0.0175 ns |     0.82 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Ascii  |    15.471 ns |   0.2748 ns |   0.2570 ns |     5.92 |    0.13 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Ascii  |     3.637 ns |   0.0746 ns |   0.0698 ns |     1.39 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Ascii  |    16.655 ns |   0.1381 ns |   0.1292 ns |     6.38 |    0.11 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Ascii  |   262.373 ns |   2.4618 ns |   2.3028 ns |   100.43 |    1.82 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Ascii  |   138.962 ns |   1.8689 ns |   1.7482 ns |    53.19 |    1.07 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Ascii  |   277.860 ns |   5.1300 ns |   4.5476 ns |   106.36 |    2.39 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Ascii  |   468.006 ns |  10.6827 ns |   9.9926 ns |   179.15 |    4.69 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Ascii  |   284.494 ns |   3.5197 ns |   3.2923 ns |   108.90 |    2.13 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **4096**  | **Cjk**    |     **1.300 ns** |   **0.0332 ns** |   **0.0311 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Cjk    |     1.659 ns |   0.0288 ns |   0.0270 ns |     1.28 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Cjk    |    24.946 ns |   0.4042 ns |   0.3781 ns |    19.20 |    0.52 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Cjk    |     2.945 ns |   0.0345 ns |   0.0323 ns |     2.27 |    0.06 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Cjk    |    31.719 ns |   0.3956 ns |   0.3507 ns |    24.41 |    0.62 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Cjk    |   271.433 ns |   5.8677 ns |   5.4886 ns |   208.91 |    6.30 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   392.371 ns |   8.6864 ns |   8.1253 ns |   301.99 |    9.19 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   287.832 ns |   6.1627 ns |   5.7646 ns |   221.53 |    6.65 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Cjk    |   274.027 ns |   2.3944 ns |   2.2397 ns |   210.91 |    5.11 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Cjk    |   292.163 ns |   4.5252 ns |   4.0114 ns |   224.86 |    5.95 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **4096**  | **Emoji**  |     **1.894 ns** |   **0.0254 ns** |   **0.0238 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Emoji  |     1.816 ns |   0.0376 ns |   0.0352 ns |     0.96 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Emoji  |    26.358 ns |   0.5398 ns |   0.5049 ns |    13.92 |    0.31 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Emoji  |     3.004 ns |   0.0365 ns |   0.0341 ns |     1.59 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Emoji  |    32.464 ns |   0.1901 ns |   0.1587 ns |    17.14 |    0.22 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Emoji  |   263.626 ns |   4.5207 ns |   4.2287 ns |   139.22 |    2.75 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Emoji  |   410.424 ns |   7.4929 ns |   7.0089 ns |   216.75 |    4.45 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Emoji  |   286.536 ns |   5.5442 ns |   5.1861 ns |   151.32 |    3.23 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Emoji  |   258.499 ns |   1.9061 ns |   1.5917 ns |   136.51 |    1.85 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Emoji  |   294.148 ns |   5.5848 ns |   5.2240 ns |   155.34 |    3.27 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **4096**  | **Mixed**  |     **2.254 ns** |   **0.0299 ns** |   **0.0280 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Mixed  |     2.006 ns |   0.0313 ns |   0.0293 ns |     0.89 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Mixed  |    15.160 ns |   0.2056 ns |   0.1923 ns |     6.73 |    0.11 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Mixed  |     4.397 ns |   0.0714 ns |   0.0668 ns |     1.95 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Mixed  |    16.709 ns |   0.1311 ns |   0.1024 ns |     7.41 |    0.10 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Mixed  |   260.086 ns |   4.0103 ns |   3.5550 ns |   115.41 |    2.05 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   176.545 ns |   3.9945 ns |   3.7365 ns |    78.34 |    1.85 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   284.448 ns |   3.8515 ns |   3.4143 ns |   126.22 |    2.09 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Mixed  |   259.630 ns |   2.1979 ns |   1.8353 ns |   115.21 |    1.57 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Mixed  |   290.169 ns |   2.8318 ns |   2.3647 ns |   128.76 |    1.83 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **65536** | **Ascii**  |     **2.650 ns** |   **0.0407 ns** |   **0.0380 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Ascii  |     2.135 ns |   0.0342 ns |   0.0320 ns |     0.81 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Ascii  |    15.447 ns |   0.2380 ns |   0.2226 ns |     5.83 |    0.11 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Ascii  |     3.715 ns |   0.0596 ns |   0.0557 ns |     1.40 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Ascii  |    16.183 ns |   0.0830 ns |   0.0693 ns |     6.11 |    0.09 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Ascii  | 4,034.025 ns |  39.8000 ns |  33.2348 ns | 1,522.56 |   24.40 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 2,008.578 ns |  30.9257 ns |  27.4149 ns |   758.10 |   14.53 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 4,078.918 ns |  79.9971 ns |  74.8293 ns | 1,539.51 |   34.74 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Ascii  | 7,085.899 ns | 220.3330 ns | 206.0996 ns | 2,674.43 |   84.02 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Ascii  | 4,131.032 ns |  81.2604 ns |  76.0111 ns | 1,559.18 |   35.25 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **65536** | **Cjk**    |     **1.327 ns** |   **0.0259 ns** |   **0.0242 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Cjk    |     1.717 ns |   0.0312 ns |   0.0292 ns |     1.29 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Cjk    |    24.983 ns |   0.5710 ns |   0.5341 ns |    18.83 |    0.51 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Cjk    |     2.984 ns |   0.0362 ns |   0.0338 ns |     2.25 |    0.05 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Cjk    |    31.771 ns |   0.5019 ns |   0.4695 ns |    23.95 |    0.54 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Cjk    | 4,065.509 ns |  76.0305 ns |  71.1190 ns | 3,064.45 |   74.97 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 6,090.645 ns | 116.9722 ns | 109.4159 ns | 4,590.93 |  113.78 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 4,090.214 ns |  81.6358 ns |  76.3622 ns | 3,083.07 |   77.90 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Cjk    | 4,043.827 ns |  91.8440 ns |  85.9109 ns | 3,048.10 |   82.63 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Cjk    | 4,046.934 ns |  18.1599 ns |  14.1781 ns | 3,050.45 |   54.85 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **65536** | **Emoji**  |     **1.903 ns** |   **0.0309 ns** |   **0.0289 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Emoji  |     1.735 ns |   0.0250 ns |   0.0234 ns |     0.91 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Emoji  |    25.592 ns |   0.6556 ns |   0.6133 ns |    13.45 |    0.37 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Emoji  |     3.002 ns |   0.0360 ns |   0.0337 ns |     1.58 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Emoji  |    32.592 ns |   0.4423 ns |   0.4137 ns |    17.13 |    0.33 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Emoji  | 4,017.727 ns |  34.0461 ns |  28.4301 ns | 2,111.45 |   33.99 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Emoji  | 6,505.956 ns | 131.9972 ns | 123.4703 ns | 3,419.10 |   80.20 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Emoji  | 4,043.384 ns |  25.4792 ns |  21.2763 ns | 2,124.93 |   32.81 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Emoji  | 4,001.151 ns |  44.0950 ns |  36.8213 ns | 2,102.74 |   35.89 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Emoji  | 4,109.734 ns |  79.4098 ns |  74.2800 ns | 2,159.80 |   49.20 |         - |          NA |
|                             |       |        |              |             |             |          |         |           |             |
| **string.Contains**             | **65536** | **Mixed**  |     **2.277 ns** |   **0.0321 ns** |   **0.0285 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Mixed  |     1.983 ns |   0.0282 ns |   0.0264 ns |     0.87 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Mixed  |    15.173 ns |   0.2971 ns |   0.2779 ns |     6.67 |    0.14 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Mixed  |     4.370 ns |   0.0569 ns |   0.0475 ns |     1.92 |    0.03 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Mixed  |    16.652 ns |   0.0973 ns |   0.0813 ns |     7.32 |    0.09 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Mixed  | 4,008.366 ns |  28.8166 ns |  24.0631 ns | 1,760.95 |   23.57 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 2,607.346 ns |  23.6455 ns |  19.7451 ns | 1,145.46 |   16.16 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 4,086.306 ns |  86.5776 ns |  80.9848 ns | 1,795.19 |   40.70 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Mixed  | 4,002.774 ns |  30.0869 ns |  25.1240 ns | 1,758.50 |   23.74 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Mixed  | 4,081.018 ns |  65.3048 ns |  57.8910 ns | 1,792.87 |   32.74 |         - |          NA |
