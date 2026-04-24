```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                             | N     | Locale | Mean         | Error      | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **5.492 ns** |  **0.0368 ns** |  **0.0326 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     3.583 ns |  0.0334 ns |  0.0279 ns |     0.65 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Ascii  |     3.661 ns |  0.0325 ns |  0.0288 ns |     0.67 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |     4.510 ns |  0.0462 ns |  0.0410 ns |     0.82 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |    16.410 ns |  0.1737 ns |  0.1540 ns |     2.99 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |    17.452 ns |  0.2143 ns |  0.2004 ns |     3.18 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     6.019 ns |  0.0597 ns |  0.0529 ns |     1.10 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     3.572 ns |  0.0452 ns |  0.0422 ns |     0.65 |    0.01 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Ascii  |     3.728 ns |  0.0358 ns |  0.0335 ns |     0.68 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     4.253 ns |  0.0305 ns |  0.0286 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |    16.257 ns |  0.1513 ns |  0.1342 ns |     2.96 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |    17.364 ns |  0.1849 ns |  0.1730 ns |     3.16 |    0.04 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **3.054 ns** |  **0.0173 ns** |  **0.0153 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     2.616 ns |  0.0074 ns |  0.0070 ns |     0.86 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 64    | Cjk    |     2.836 ns |  0.0091 ns |  0.0085 ns |     0.93 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |     3.891 ns |  0.0156 ns |  0.0130 ns |     1.27 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |    35.398 ns |  0.3585 ns |  0.3178 ns |    11.59 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |    31.137 ns |  0.2513 ns |  0.2098 ns |    10.20 |    0.08 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     6.003 ns |  0.0848 ns |  0.0793 ns |     1.97 |    0.03 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     7.490 ns |  0.1391 ns |  0.1301 ns |     2.45 |    0.04 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Cjk    |     6.732 ns |  0.0412 ns |  0.0385 ns |     2.20 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     8.140 ns |  0.0488 ns |  0.0407 ns |     2.67 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    28.768 ns |  0.1380 ns |  0.1153 ns |     9.42 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    34.560 ns |  0.5984 ns |  0.5304 ns |    11.32 |    0.18 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |     **4.279 ns** |  **0.0158 ns** |  **0.0140 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |     3.536 ns |  0.0302 ns |  0.0268 ns |     0.83 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Emoji  |     3.679 ns |  0.0154 ns |  0.0136 ns |     0.86 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Emoji  |     4.458 ns |  0.0139 ns |  0.0130 ns |     1.04 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Emoji  |    27.907 ns |  0.5814 ns |  0.5438 ns |     6.52 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Emoji  |    32.627 ns |  0.1574 ns |  0.1229 ns |     7.62 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |     5.916 ns |  0.0277 ns |  0.0217 ns |     1.38 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |     6.626 ns |  0.0590 ns |  0.0493 ns |     1.55 |    0.01 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Emoji  |     6.472 ns |  0.0482 ns |  0.0428 ns |     1.51 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     7.370 ns |  0.0511 ns |  0.0426 ns |     1.72 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |    30.843 ns |  0.6349 ns |  0.5628 ns |     7.21 |    0.13 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |    34.928 ns |  0.3713 ns |  0.3100 ns |     8.16 |    0.07 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **5.503 ns** |  **0.0613 ns** |  **0.0573 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |    17.953 ns |  0.1691 ns |  0.1321 ns |     3.26 |    0.04 |         - |          NA |
| U8String.LastIndexOf               | 64    | Mixed  |     3.994 ns |  0.0316 ns |  0.0281 ns |     0.73 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |     4.710 ns |  0.0240 ns |  0.0213 ns |     0.86 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |    16.819 ns |  0.0985 ns |  0.0873 ns |     3.06 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |    18.275 ns |  0.0762 ns |  0.0676 ns |     3.32 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     6.135 ns |  0.1285 ns |  0.1202 ns |     1.12 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     3.977 ns |  0.0212 ns |  0.0198 ns |     0.72 |    0.01 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Mixed  |     4.006 ns |  0.0316 ns |  0.0296 ns |     0.73 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     4.741 ns |  0.0210 ns |  0.0186 ns |     0.86 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    26.509 ns |  0.4045 ns |  0.3586 ns |     4.82 |    0.08 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    31.682 ns |  0.3198 ns |  0.2835 ns |     5.76 |    0.08 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **3.532 ns** |  **0.0212 ns** |  **0.0188 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     2.736 ns |  0.0074 ns |  0.0069 ns |     0.77 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Ascii  |     2.891 ns |  0.0191 ns |  0.0179 ns |     0.82 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     3.965 ns |  0.0179 ns |  0.0168 ns |     1.12 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |    15.248 ns |  0.0877 ns |  0.0684 ns |     4.32 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |    16.955 ns |  0.1678 ns |  0.1310 ns |     4.80 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   254.916 ns |  3.1787 ns |  2.8178 ns |    72.19 |    0.86 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   135.017 ns |  2.3860 ns |  2.2319 ns |    38.23 |    0.64 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Ascii  |   106.608 ns |  1.3240 ns |  1.1737 ns |    30.19 |    0.36 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   136.415 ns |  2.3411 ns |  2.1899 ns |    38.63 |    0.63 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   153.739 ns |  1.2556 ns |  1.0484 ns |    43.53 |    0.36 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   149.430 ns |  2.6650 ns |  2.4929 ns |    42.31 |    0.72 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **3.065 ns** |  **0.0250 ns** |  **0.0222 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     2.988 ns |  0.0088 ns |  0.0082 ns |     0.97 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Cjk    |     3.244 ns |  0.0155 ns |  0.0145 ns |     1.06 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     4.153 ns |  0.0213 ns |  0.0199 ns |     1.36 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    35.379 ns |  0.4767 ns |  0.4459 ns |    11.54 |    0.16 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    31.891 ns |  0.5871 ns |  0.5491 ns |    10.41 |    0.19 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   257.317 ns |  2.6102 ns |  2.4416 ns |    83.96 |    0.97 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   379.454 ns |  4.2889 ns |  3.8020 ns |   123.81 |    1.47 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Cjk    |   306.096 ns |  2.5799 ns |  2.4132 ns |    99.88 |    1.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   379.632 ns |  3.6766 ns |  3.2592 ns |   123.87 |    1.34 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   329.570 ns |  2.4922 ns |  1.9457 ns |   107.53 |    0.96 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   335.731 ns |  5.7943 ns |  5.4200 ns |   109.55 |    1.87 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |     **3.989 ns** |  **0.0146 ns** |  **0.0130 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |     3.012 ns |  0.0160 ns |  0.0150 ns |     0.76 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Emoji  |     3.252 ns |  0.0105 ns |  0.0093 ns |     0.82 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Emoji  |     4.205 ns |  0.0188 ns |  0.0176 ns |     1.05 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Emoji  |    27.504 ns |  0.5107 ns |  0.4777 ns |     6.89 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Emoji  |    32.043 ns |  0.2935 ns |  0.2450 ns |     8.03 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |   261.636 ns |  4.7839 ns |  3.9948 ns |    65.59 |    0.99 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   375.474 ns |  1.3896 ns |  1.1604 ns |    94.13 |    0.41 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Emoji  |   317.161 ns |  2.1506 ns |  1.6790 ns |    79.51 |    0.47 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   380.923 ns |  8.8483 ns |  8.2767 ns |    95.49 |    2.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   336.749 ns |  3.6725 ns |  3.4353 ns |    84.42 |    0.87 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   339.746 ns |  2.2419 ns |  2.0970 ns |    85.17 |    0.57 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **4.732 ns** |  **0.0183 ns** |  **0.0162 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     3.394 ns |  0.0147 ns |  0.0123 ns |     0.72 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Mixed  |     3.607 ns |  0.0123 ns |  0.0109 ns |     0.76 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     4.367 ns |  0.0185 ns |  0.0164 ns |     0.92 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |    16.520 ns |  0.2748 ns |  0.2571 ns |     3.49 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |    24.234 ns |  0.3814 ns |  0.3381 ns |     5.12 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   265.118 ns |  7.5247 ns |  6.6704 ns |    56.02 |    1.37 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   176.590 ns |  3.5702 ns |  3.3396 ns |    37.32 |    0.69 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Mixed  |   138.740 ns |  2.8949 ns |  2.7079 ns |    29.32 |    0.56 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   176.074 ns |  4.0693 ns |  3.8064 ns |    37.21 |    0.79 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   165.209 ns |  2.7672 ns |  2.5885 ns |    34.91 |    0.54 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   165.769 ns |  1.8834 ns |  1.7617 ns |    35.03 |    0.38 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **4.768 ns** |  **0.0339 ns** |  **0.0300 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     3.120 ns |  0.0345 ns |  0.0323 ns |     0.65 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Ascii  |     3.254 ns |  0.0124 ns |  0.0110 ns |     0.68 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     4.210 ns |  0.0349 ns |  0.0327 ns |     0.88 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |    16.032 ns |  0.2136 ns |  0.1998 ns |     3.36 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |    17.202 ns |  0.1831 ns |  0.1623 ns |     3.61 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,021.292 ns | 82.3686 ns | 73.0176 ns |   843.39 |   15.66 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 2,002.728 ns | 25.1909 ns | 23.5635 ns |   420.04 |    5.42 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Ascii  | 1,631.228 ns | 19.1214 ns | 17.8862 ns |   342.12 |    4.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,985.996 ns | 21.3523 ns | 19.9730 ns |   416.53 |    4.78 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,067.429 ns | 32.1356 ns | 30.0597 ns |   433.61 |    6.65 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,106.106 ns | 41.9797 ns | 39.2679 ns |   441.72 |    8.41 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **3.542 ns** |  **0.0311 ns** |  **0.0276 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     3.063 ns |  0.0182 ns |  0.0161 ns |     0.86 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Cjk    |     3.329 ns |  0.0157 ns |  0.0147 ns |     0.94 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     4.218 ns |  0.0196 ns |  0.0183 ns |     1.19 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    36.210 ns |  0.2530 ns |  0.2243 ns |    10.22 |    0.10 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    32.962 ns |  0.4494 ns |  0.4204 ns |     9.31 |    0.13 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,159.611 ns | 47.8142 ns | 44.7254 ns | 1,174.38 |   15.08 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 6,155.848 ns | 98.4004 ns | 92.0438 ns | 1,737.97 |   28.35 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Cjk    | 5,021.906 ns | 63.0915 ns | 59.0159 ns | 1,417.83 |   19.33 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 6,079.967 ns | 61.3384 ns | 51.2203 ns | 1,716.55 |   18.98 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,924.153 ns | 16.0593 ns | 13.4102 ns | 1,390.23 |   11.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,982.169 ns | 36.7135 ns | 34.3419 ns | 1,406.61 |   14.13 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |     **4.016 ns** |  **0.0243 ns** |  **0.0216 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |     3.025 ns |  0.0084 ns |  0.0070 ns |     0.75 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Emoji  |     3.310 ns |  0.0268 ns |  0.0237 ns |     0.82 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Emoji  |     4.195 ns |  0.0460 ns |  0.0430 ns |     1.04 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Emoji  |    27.801 ns |  0.5108 ns |  0.4778 ns |     6.92 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Emoji  |    33.009 ns |  0.2128 ns |  0.1661 ns |     8.22 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  | 4,101.739 ns | 74.0696 ns | 69.2847 ns | 1,021.44 |   17.52 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 6,099.517 ns | 52.0041 ns | 43.4258 ns | 1,518.94 |   13.03 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Emoji  | 4,998.373 ns | 45.6063 ns | 38.0834 ns | 1,244.73 |   11.17 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 6,124.998 ns | 53.2012 ns | 44.4254 ns | 1,525.29 |   13.25 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 5,070.974 ns | 26.5431 ns | 23.5298 ns | 1,262.81 |    8.63 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 5,106.601 ns | 44.6484 ns | 39.5796 ns | 1,271.68 |   11.56 |         - |          NA |
|                                    |       |        |              |            |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **4.026 ns** |  **0.0354 ns** |  **0.0331 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     2.993 ns |  0.0102 ns |  0.0096 ns |     0.74 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Mixed  |     3.250 ns |  0.0202 ns |  0.0189 ns |     0.81 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     4.159 ns |  0.0164 ns |  0.0153 ns |     1.03 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |    16.118 ns |  0.1224 ns |  0.1145 ns |     4.00 |    0.04 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |    17.671 ns |  0.1919 ns |  0.1603 ns |     4.39 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,058.184 ns | 80.6297 ns | 75.4210 ns | 1,008.05 |   19.81 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,637.023 ns | 61.0829 ns | 57.1370 ns |   655.03 |   14.68 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Mixed  | 2,159.152 ns | 36.5212 ns | 34.1619 ns |   536.33 |    9.24 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,717.228 ns | 35.4483 ns | 33.1583 ns |   674.96 |    9.59 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,188.019 ns | 13.0959 ns | 12.2499 ns |   543.50 |    5.20 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,219.404 ns | 16.1291 ns | 15.0871 ns |   551.30 |    5.67 |         - |          NA |
