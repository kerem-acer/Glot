```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                         | N     | Locale | Mean         | Error       | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|------------:|------------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **2.545 ns** |   **0.0383 ns** |   **0.0359 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     1.945 ns |   0.0215 ns |   0.0201 ns |     0.76 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Ascii  |    14.454 ns |   0.1093 ns |   0.1022 ns |     5.68 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Ascii  |     3.346 ns |   0.0213 ns |   0.0189 ns |     1.31 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Ascii  |    15.441 ns |   0.2337 ns |   0.2071 ns |     6.07 |    0.11 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     4.302 ns |   0.0728 ns |   0.0608 ns |     1.69 |    0.03 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.679 ns |   0.0707 ns |   0.0661 ns |     1.05 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |    16.669 ns |   0.4126 ns |   0.3860 ns |     6.55 |    0.17 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     7.610 ns |   0.1641 ns |   0.1455 ns |     2.99 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |    18.128 ns |   0.2359 ns |   0.2206 ns |     7.12 |    0.13 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **1.279 ns** |   **0.0148 ns** |   **0.0139 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     1.636 ns |   0.0220 ns |   0.0206 ns |     1.28 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Cjk    |    23.636 ns |   0.3438 ns |   0.2871 ns |    18.49 |    0.29 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Cjk    |     2.935 ns |   0.0414 ns |   0.0387 ns |     2.30 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Cjk    |    32.176 ns |   0.8283 ns |   0.7342 ns |    25.17 |    0.61 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     4.428 ns |   0.1268 ns |   0.0990 ns |     3.46 |    0.08 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     7.279 ns |   0.1160 ns |   0.1085 ns |     5.69 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |    28.607 ns |   0.3625 ns |   0.3391 ns |    22.38 |    0.35 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |     6.786 ns |   0.1535 ns |   0.1436 ns |     5.31 |    0.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    34.361 ns |   0.3903 ns |   0.3651 ns |    26.88 |    0.40 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Emoji**  |     **1.822 ns** |   **0.0284 ns** |   **0.0266 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Emoji  |     1.698 ns |   0.0327 ns |   0.0306 ns |     0.93 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Emoji  |    24.808 ns |   0.3288 ns |   0.2915 ns |    13.62 |    0.25 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Emoji  |     2.962 ns |   0.0275 ns |   0.0229 ns |     1.63 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Emoji  |    32.453 ns |   0.3062 ns |   0.2864 ns |    17.81 |    0.29 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Emoji  |     4.449 ns |   0.0465 ns |   0.0435 ns |     2.44 |    0.04 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Emoji  |     6.761 ns |   0.1847 ns |   0.1638 ns |     3.71 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |    27.699 ns |   0.7409 ns |   0.6568 ns |    15.20 |    0.41 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |     6.718 ns |   0.1134 ns |   0.1005 ns |     3.69 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |    35.200 ns |   0.5098 ns |   0.4519 ns |    19.32 |    0.36 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **2.147 ns** |   **0.0285 ns** |   **0.0266 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     1.980 ns |   0.0303 ns |   0.0283 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Mixed  |    15.054 ns |   0.3197 ns |   0.2990 ns |     7.01 |    0.16 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Mixed  |     4.222 ns |   0.0660 ns |   0.0618 ns |     1.97 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Mixed  |    16.087 ns |   0.0623 ns |   0.0520 ns |     7.49 |    0.09 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     4.427 ns |   0.0403 ns |   0.0336 ns |     2.06 |    0.03 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     3.221 ns |   0.0765 ns |   0.0678 ns |     1.50 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |    26.452 ns |   0.2939 ns |   0.2454 ns |    12.32 |    0.18 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |     6.578 ns |   0.2107 ns |   0.1971 ns |     3.06 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    33.350 ns |   0.3211 ns |   0.3003 ns |    15.54 |    0.23 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **2.609 ns** |   **0.0521 ns** |   **0.0487 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     2.083 ns |   0.0663 ns |   0.0620 ns |     0.80 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Ascii  |    15.320 ns |   0.2332 ns |   0.2067 ns |     5.87 |    0.13 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     3.441 ns |   0.0464 ns |   0.0434 ns |     1.32 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Ascii  |    16.406 ns |   0.1776 ns |   0.1483 ns |     6.29 |    0.13 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   253.140 ns |   4.7309 ns |   4.1938 ns |    97.06 |    2.33 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   134.520 ns |   2.8275 ns |   2.6449 ns |    51.58 |    1.35 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   270.194 ns |   4.6381 ns |   4.3385 ns |   103.60 |    2.46 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   427.581 ns |   6.5892 ns |   5.5023 ns |   163.94 |    3.58 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   267.075 ns |   6.3292 ns |   5.9204 ns |   102.40 |    2.87 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **1.301 ns** |   **0.0138 ns** |   **0.0129 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     1.598 ns |   0.0286 ns |   0.0267 ns |     1.23 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Cjk    |    23.576 ns |   0.2734 ns |   0.2557 ns |    18.12 |    0.26 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Cjk    |     2.939 ns |   0.0608 ns |   0.0569 ns |     2.26 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    30.975 ns |   0.4038 ns |   0.3372 ns |    23.81 |    0.34 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   256.678 ns |   6.6680 ns |   6.2373 ns |   197.30 |    5.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   370.422 ns |   2.5419 ns |   2.3777 ns |   284.73 |    3.24 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   277.231 ns |   4.3489 ns |   4.0680 ns |   213.10 |    3.65 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   263.480 ns |  10.9886 ns |  10.2788 ns |   202.53 |    7.89 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   293.443 ns |   6.5798 ns |   6.1548 ns |   225.56 |    5.06 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Emoji**  |     **1.809 ns** |   **0.0160 ns** |   **0.0134 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Emoji  |     1.668 ns |   0.0420 ns |   0.0393 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Emoji  |    25.290 ns |   0.7071 ns |   0.6614 ns |    13.98 |    0.37 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Emoji  |     2.980 ns |   0.0421 ns |   0.0394 ns |     1.65 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Emoji  |    32.676 ns |   0.6092 ns |   0.5698 ns |    18.06 |    0.33 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Emoji  |   262.353 ns |   6.7101 ns |   6.2767 ns |   145.02 |    3.52 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   410.815 ns |   9.9396 ns |   9.2975 ns |   227.09 |    5.23 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   289.462 ns |  11.0928 ns |   9.8335 ns |   160.01 |    5.37 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   269.114 ns |   3.7841 ns |   3.5397 ns |   148.76 |    2.17 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   297.716 ns |   6.1452 ns |   5.7482 ns |   164.57 |    3.29 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **2.184 ns** |   **0.0426 ns** |   **0.0398 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     1.965 ns |   0.0379 ns |   0.0354 ns |     0.90 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Mixed  |    15.006 ns |   0.2586 ns |   0.2419 ns |     6.87 |    0.16 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     4.302 ns |   0.0802 ns |   0.0750 ns |     1.97 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Mixed  |    17.034 ns |   0.3401 ns |   0.3181 ns |     7.80 |    0.20 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   259.265 ns |   5.4009 ns |   5.0520 ns |   118.77 |    3.06 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   178.976 ns |   4.8371 ns |   4.2879 ns |    81.99 |    2.38 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   287.967 ns |   6.4321 ns |   6.0166 ns |   131.92 |    3.53 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   264.811 ns |   7.8567 ns |   7.3492 ns |   121.31 |    3.89 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   297.237 ns |   4.9498 ns |   4.6300 ns |   136.16 |    3.15 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **2.668 ns** |   **0.0451 ns** |   **0.0422 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     1.951 ns |   0.0504 ns |   0.0447 ns |     0.73 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Ascii  |    15.993 ns |   0.2910 ns |   0.2722 ns |     6.00 |    0.13 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     3.498 ns |   0.0571 ns |   0.0534 ns |     1.31 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Ascii  |    16.719 ns |   0.3530 ns |   0.3302 ns |     6.27 |    0.15 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,045.013 ns |  86.2299 ns |  72.0059 ns | 1,516.45 |   34.76 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 2,058.888 ns |  45.1153 ns |  42.2009 ns |   771.87 |   19.30 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 4,201.472 ns |  59.3661 ns |  52.6265 ns | 1,575.11 |   30.61 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 7,211.217 ns | 184.9118 ns | 172.9667 ns | 2,703.45 |   75.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 4,136.568 ns |  78.8648 ns |  73.7702 ns | 1,550.78 |   35.68 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **1.254 ns** |   **0.0217 ns** |   **0.0192 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     1.624 ns |   0.0289 ns |   0.0270 ns |     1.30 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Cjk    |    25.354 ns |   0.4970 ns |   0.4649 ns |    20.22 |    0.47 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Cjk    |     2.996 ns |   0.0533 ns |   0.0499 ns |     2.39 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    32.934 ns |   0.8804 ns |   0.7352 ns |    26.27 |    0.68 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,166.022 ns |  88.7647 ns |  83.0305 ns | 3,323.18 |   80.63 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 6,169.289 ns | 114.1902 ns | 101.2267 ns | 4,921.16 |  106.40 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,204.588 ns |  62.0261 ns |  58.0193 ns | 3,353.94 |   66.63 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,067.450 ns |  62.9234 ns |  55.7799 ns | 3,244.55 |   64.21 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,178.986 ns |  59.8849 ns |  56.0164 ns | 3,333.52 |   65.37 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Emoji**  |     **1.832 ns** |   **0.0209 ns** |   **0.0174 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Emoji  |     1.681 ns |   0.0284 ns |   0.0266 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Emoji  |    25.354 ns |   0.3484 ns |   0.3089 ns |    13.84 |    0.21 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Emoji  |     3.031 ns |   0.0388 ns |   0.0324 ns |     1.65 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Emoji  |    33.412 ns |   0.3863 ns |   0.3614 ns |    18.24 |    0.25 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Emoji  | 4,087.143 ns |  56.8905 ns |  53.2154 ns | 2,231.56 |   34.80 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 6,624.765 ns | 111.3490 ns | 104.1559 ns | 3,617.09 |   64.31 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 4,138.393 ns |  61.3034 ns |  57.3432 ns | 2,259.55 |   36.73 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 4,112.182 ns |  69.1071 ns |  64.6429 ns | 2,245.23 |   39.91 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 4,171.739 ns |  69.6163 ns |  65.1191 ns | 2,277.75 |   40.28 |         - |          NA |
|                                |       |        |              |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **2.188 ns** |   **0.0309 ns** |   **0.0289 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     2.003 ns |   0.0275 ns |   0.0244 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Mixed  |    15.194 ns |   0.1807 ns |   0.1602 ns |     6.95 |    0.11 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     4.334 ns |   0.0453 ns |   0.0423 ns |     1.98 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Mixed  |    17.193 ns |   0.2492 ns |   0.2331 ns |     7.86 |    0.14 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 4,189.828 ns |  67.3746 ns |  63.0222 ns | 1,915.23 |   36.99 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,653.856 ns |  56.3841 ns |  52.7417 ns | 1,213.12 |   27.96 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 4,122.592 ns |  79.4796 ns |  74.3453 ns | 1,884.49 |   40.67 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 4,128.224 ns |  79.7096 ns |  74.5604 ns | 1,887.07 |   40.77 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 4,165.477 ns |  76.6745 ns |  71.7214 ns | 1,904.10 |   39.89 |         - |          NA |
