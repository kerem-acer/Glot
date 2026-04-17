```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean         | Error         | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|--------------:|------------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.347 ns** |     **1.3292 ns** |   **0.0729 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.299 ns |     0.3548 ns |   0.0194 ns |     0.53 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Ascii  |     2.467 ns |     0.3639 ns |   0.0199 ns |     0.57 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     5.466 ns |     1.5647 ns |   0.0858 ns |     1.26 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |    11.592 ns |     0.8748 ns |   0.0479 ns |     2.67 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |    11.671 ns |     2.1495 ns |   0.1178 ns |     2.69 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     4.904 ns |     0.6399 ns |   0.0351 ns |     1.13 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.082 ns |     0.1791 ns |   0.0098 ns |     0.48 |    0.01 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Ascii  |     2.319 ns |     0.7770 ns |   0.0426 ns |     0.53 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.000 ns |     1.3423 ns |   0.0736 ns |     0.69 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.645 ns |     2.1816 ns |   0.1196 ns |     1.99 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.565 ns |     0.8932 ns |   0.0490 ns |     1.97 |    0.03 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.176 ns** |     **0.7801 ns** |   **0.0428 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.462 ns |     0.5509 ns |   0.0302 ns |     0.67 |    0.02 |         - |          NA |
| U8String.LastIndexOf               | 64    | Cjk    |     1.647 ns |     0.7223 ns |   0.0396 ns |     0.76 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |     6.427 ns |     1.4919 ns |   0.0818 ns |     2.95 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |    27.595 ns |     5.6815 ns |   0.3114 ns |    12.69 |    0.25 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |    24.906 ns |     3.4939 ns |   0.1915 ns |    11.45 |    0.21 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     4.894 ns |     1.3842 ns |   0.0759 ns |     2.25 |    0.05 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.811 ns |     0.6356 ns |   0.0348 ns |     2.67 |    0.05 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Cjk    |     6.152 ns |     0.6611 ns |   0.0362 ns |     2.83 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.788 ns |     1.0403 ns |   0.0570 ns |     3.12 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.969 ns |     4.0378 ns |   0.2213 ns |     7.80 |    0.16 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    22.408 ns |     1.3855 ns |   0.0759 ns |    10.30 |    0.18 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.382 ns** |     **0.1297 ns** |   **0.0071 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.588 ns |     0.7139 ns |   0.0391 ns |     0.59 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Mixed  |     2.804 ns |     0.5793 ns |   0.0318 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     6.772 ns |     0.6664 ns |   0.0365 ns |     1.55 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |    12.818 ns |     1.1076 ns |   0.0607 ns |     2.93 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |    12.821 ns |     2.7576 ns |   0.1512 ns |     2.93 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     5.108 ns |     1.8791 ns |   0.1030 ns |     1.17 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.883 ns |     1.2399 ns |   0.0680 ns |     0.66 |    0.01 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Mixed  |     2.709 ns |     0.9011 ns |   0.0494 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.415 ns |     1.3638 ns |   0.0748 ns |     0.78 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |    13.344 ns |     2.1706 ns |   0.1190 ns |     3.05 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    19.670 ns |     3.3805 ns |   0.1853 ns |     4.49 |    0.04 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.340 ns** |     **0.2435 ns** |   **0.0133 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.567 ns |     0.4460 ns |   0.0244 ns |     0.67 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Ascii  |     1.709 ns |     1.1101 ns |   0.0608 ns |     0.73 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     7.003 ns |     0.3225 ns |   0.0177 ns |     2.99 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |    13.723 ns |     2.4864 ns |   0.1363 ns |     5.86 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |    13.466 ns |     3.1225 ns |   0.1712 ns |     5.75 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   270.070 ns |    80.2847 ns |   4.4007 ns |   115.40 |    1.72 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   136.884 ns |    38.9942 ns |   2.1374 ns |    58.49 |    0.84 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Ascii  |   137.608 ns |    33.4415 ns |   1.8330 ns |    58.80 |    0.74 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   135.820 ns |    21.7779 ns |   1.1937 ns |    58.04 |    0.53 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   142.085 ns |    38.9671 ns |   2.1359 ns |    60.71 |    0.85 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   144.920 ns |   143.8853 ns |   7.8868 ns |    61.92 |    2.93 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.089 ns** |     **0.9150 ns** |   **0.0502 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.849 ns |     0.2454 ns |   0.0134 ns |     0.89 |    0.02 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Cjk    |     2.063 ns |     0.3970 ns |   0.0218 ns |     0.99 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     9.280 ns |     0.5515 ns |   0.0302 ns |     4.44 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |    28.151 ns |     6.7363 ns |   0.3692 ns |    13.48 |    0.32 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |    26.239 ns |     1.6898 ns |   0.0926 ns |    12.56 |    0.27 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   261.222 ns |    54.5318 ns |   2.9891 ns |   125.09 |    2.90 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   387.324 ns |    31.6513 ns |   1.7349 ns |   185.47 |    3.96 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Cjk    |   390.530 ns |    51.3510 ns |   2.8147 ns |   187.00 |    4.10 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   383.275 ns |    30.4422 ns |   1.6686 ns |   183.53 |    3.92 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   395.253 ns |    42.4668 ns |   2.3277 ns |   189.27 |    4.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   411.572 ns |    53.4260 ns |   2.9285 ns |   197.08 |    4.31 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.612 ns** |     **1.5490 ns** |   **0.0849 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.269 ns |     0.3210 ns |   0.0176 ns |     0.63 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Mixed  |     2.447 ns |     0.3767 ns |   0.0206 ns |     0.68 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |    10.255 ns |     3.1258 ns |   0.1713 ns |     2.84 |    0.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |    16.779 ns |     4.7322 ns |   0.2594 ns |     4.65 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |    16.397 ns |     0.6265 ns |   0.0343 ns |     4.54 |    0.09 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   260.104 ns |     6.9481 ns |   0.3808 ns |    72.04 |    1.49 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   173.171 ns |    30.5387 ns |   1.6739 ns |    47.96 |    1.07 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Mixed  |   174.298 ns |    20.7708 ns |   1.1385 ns |    48.27 |    1.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   182.259 ns |   174.8452 ns |   9.5839 ns |    50.48 |    2.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   185.497 ns |     7.5230 ns |   0.4124 ns |    51.37 |    1.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   194.848 ns |    44.8535 ns |   2.4586 ns |    53.96 |    1.26 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.607 ns** |     **0.3329 ns** |   **0.0182 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.910 ns |     0.3609 ns |   0.0198 ns |     0.53 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Ascii  |     2.056 ns |     0.8565 ns |   0.0469 ns |     0.57 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  |    10.323 ns |     0.6831 ns |   0.0374 ns |     2.86 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  |    16.857 ns |     3.9626 ns |   0.2172 ns |     4.67 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  |    16.610 ns |     0.6745 ns |   0.0370 ns |     4.61 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,053.968 ns |   580.2006 ns |  31.8028 ns | 1,123.98 |    9.09 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,664.184 ns |   313.2239 ns |  17.1689 ns |   461.40 |    4.59 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Ascii  | 1,647.167 ns |   155.6510 ns |   8.5318 ns |   456.69 |    2.86 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,666.859 ns |   168.6716 ns |   9.2455 ns |   462.15 |    3.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,039.251 ns |   552.6444 ns |  30.2923 ns |   565.39 |    7.68 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,045.620 ns |   129.9945 ns |   7.1254 ns |   567.16 |    3.02 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.583 ns** |     **0.2452 ns** |   **0.0134 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.782 ns |     0.4214 ns |   0.0231 ns |     0.69 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Cjk    |     2.031 ns |     0.2164 ns |   0.0119 ns |     0.79 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     6.379 ns |     1.3143 ns |   0.0720 ns |     2.47 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    |    26.999 ns |     1.3100 ns |   0.0718 ns |    10.45 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    |    26.393 ns |     2.5455 ns |   0.1395 ns |    10.22 |    0.07 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,124.792 ns |   382.1193 ns |  20.9452 ns | 1,596.72 |   10.06 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 5,031.132 ns |   416.3144 ns |  22.8196 ns | 1,947.57 |   11.65 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Cjk    | 4,960.436 ns |   614.7394 ns |  33.6959 ns | 1,920.21 |   14.24 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 5,219.847 ns | 5,467.0654 ns | 299.6683 ns | 2,020.62 |  100.88 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,165.068 ns | 1,031.0593 ns |  56.5158 ns | 2,386.52 |   21.80 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,979.026 ns | 1,463.6799 ns |  80.2292 ns | 1,927.40 |   28.27 |         - |          NA |
|                                    |       |        |              |               |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.793 ns** |     **0.4413 ns** |   **0.0242 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.795 ns |     0.3857 ns |   0.0211 ns |     0.64 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Mixed  |     2.042 ns |     0.2843 ns |   0.0156 ns |     0.73 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     7.408 ns |     0.7884 ns |   0.0432 ns |     2.65 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  |    14.765 ns |     4.0246 ns |   0.2206 ns |     5.29 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    14.082 ns |     1.5222 ns |   0.0834 ns |     5.04 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 4,079.459 ns |   318.4869 ns |  17.4573 ns | 1,460.52 |   12.25 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,196.975 ns |   302.8913 ns |  16.6025 ns |   786.56 |    7.85 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Mixed  | 2,192.720 ns |   689.3106 ns |  37.7834 ns |   785.04 |   13.12 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,206.858 ns |    56.2734 ns |   3.0845 ns |   790.10 |    6.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,680.157 ns |   493.3417 ns |  27.0417 ns |   959.55 |   11.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,184.816 ns |    41.6281 ns |   2.2818 ns |   782.21 |    5.93 |         - |          NA |
