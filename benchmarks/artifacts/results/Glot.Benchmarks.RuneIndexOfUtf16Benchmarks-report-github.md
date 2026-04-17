```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean         | Error         | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|--------------:|------------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **2.711 ns** |     **0.7086 ns** |   **0.0388 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     2.053 ns |     0.0441 ns |   0.0024 ns |     0.76 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Ascii  |     5.383 ns |     1.0699 ns |   0.0586 ns |     1.99 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Ascii  |    11.395 ns |     2.5281 ns |   0.1386 ns |     4.20 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Ascii  |    11.452 ns |     0.3595 ns |   0.0197 ns |     4.23 |    0.05 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     4.650 ns |     0.5348 ns |   0.0293 ns |     1.72 |    0.02 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.412 ns |     0.3976 ns |   0.0218 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.517 ns |     0.4277 ns |   0.0234 ns |     1.30 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.783 ns |     2.3136 ns |   0.1268 ns |     3.24 |    0.06 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.909 ns |     1.7964 ns |   0.0985 ns |     3.29 |    0.05 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **1.952 ns** |     **1.1579 ns** |   **0.0635 ns** |     **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     1.630 ns |     0.1840 ns |   0.0101 ns |     0.84 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Cjk    |     7.984 ns |     0.7942 ns |   0.0435 ns |     4.09 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Cjk    |    28.443 ns |     2.5045 ns |   0.1373 ns |    14.58 |    0.41 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Cjk    |    23.783 ns |     1.0548 ns |   0.0578 ns |    12.19 |    0.34 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     4.497 ns |     0.0932 ns |   0.0051 ns |     2.31 |    0.06 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.783 ns |     0.7809 ns |   0.0428 ns |     2.96 |    0.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.388 ns |     2.7435 ns |   0.1504 ns |     3.27 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |    15.297 ns |     0.5255 ns |   0.0288 ns |     7.84 |    0.22 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    20.925 ns |     0.9997 ns |   0.0548 ns |    10.73 |    0.30 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **2.185 ns** |     **0.1834 ns** |   **0.0101 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     2.003 ns |     0.0452 ns |   0.0025 ns |     0.92 |    0.00 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 64    | Mixed  |     6.519 ns |     0.5663 ns |   0.0310 ns |     2.98 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 64    | Mixed  |    11.886 ns |     0.0852 ns |   0.0047 ns |     5.44 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 64    | Mixed  |    11.994 ns |     0.3948 ns |   0.0216 ns |     5.49 |    0.02 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     4.647 ns |     0.8293 ns |   0.0455 ns |     2.13 |    0.02 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.822 ns |     1.3193 ns |   0.0723 ns |     1.29 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.760 ns |     0.0460 ns |   0.0025 ns |     1.72 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |    13.637 ns |     2.1823 ns |   0.1196 ns |     6.24 |    0.05 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    19.664 ns |     1.1157 ns |   0.0612 ns |     9.00 |    0.04 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **2.803 ns** |     **0.0944 ns** |   **0.0052 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     2.018 ns |     0.4968 ns |   0.0272 ns |     0.72 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     5.399 ns |     1.3499 ns |   0.0740 ns |     1.93 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Ascii  |    11.343 ns |     1.0079 ns |   0.0552 ns |     4.05 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Ascii  |    11.245 ns |     0.9231 ns |   0.0506 ns |     4.01 |    0.02 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   259.556 ns |    26.8198 ns |   1.4701 ns |    92.60 |    0.48 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |    94.816 ns |    10.2788 ns |   0.5634 ns |    33.83 |    0.18 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |    96.848 ns |    27.5067 ns |   1.5077 ns |    34.55 |    0.47 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   108.440 ns |     9.7789 ns |   0.5360 ns |    38.69 |    0.18 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   104.045 ns |    11.3187 ns |   0.6204 ns |    37.12 |    0.20 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **1.966 ns** |     **0.5234 ns** |   **0.0287 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     1.658 ns |     0.8660 ns |   0.0475 ns |     0.84 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     8.580 ns |     6.3631 ns |   0.3488 ns |     4.36 |    0.16 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Cjk    |    28.353 ns |     0.5908 ns |   0.0324 ns |    14.42 |    0.18 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Cjk    |    24.428 ns |     5.1519 ns |   0.2824 ns |    12.43 |    0.20 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   257.250 ns |    15.2488 ns |   0.8358 ns |   130.87 |    1.71 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   274.697 ns |    42.6683 ns |   2.3388 ns |   139.74 |    2.06 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   273.959 ns |    52.6734 ns |   2.8872 ns |   139.37 |    2.18 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   286.737 ns |    10.7791 ns |   0.5908 ns |   145.87 |    1.88 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   303.057 ns |    18.0927 ns |   0.9917 ns |   154.17 |    2.01 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **2.253 ns** |     **0.0626 ns** |   **0.0034 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     1.929 ns |     0.2173 ns |   0.0119 ns |     0.86 |    0.00 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 4096  | Mixed  |     6.402 ns |     0.7515 ns |   0.0412 ns |     2.84 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 4096  | Mixed  |    12.197 ns |     1.5585 ns |   0.0854 ns |     5.41 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 4096  | Mixed  |    12.180 ns |     2.8494 ns |   0.1562 ns |     5.41 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   262.620 ns |    63.4838 ns |   3.4798 ns |   116.56 |    1.35 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   122.234 ns |     2.0143 ns |   0.1104 ns |    54.25 |    0.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   124.068 ns |    52.7300 ns |   2.8903 ns |    55.07 |    1.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   132.410 ns |    15.3709 ns |   0.8425 ns |    58.77 |    0.33 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   143.364 ns |     2.2722 ns |   0.1245 ns |    63.63 |    0.10 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **2.660 ns** |     **0.8601 ns** |   **0.0471 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     2.048 ns |     0.5005 ns |   0.0274 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Ascii  |     5.329 ns |     0.8847 ns |   0.0485 ns |     2.00 |    0.03 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Ascii  |    11.144 ns |     0.9228 ns |   0.0506 ns |     4.19 |    0.07 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Ascii  |    11.213 ns |     1.3395 ns |   0.0734 ns |     4.22 |    0.07 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,045.520 ns | 1,902.2612 ns | 104.2694 ns | 1,520.99 |   41.14 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,481.081 ns |   136.5482 ns |   7.4847 ns |   556.84 |    8.85 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,454.894 ns |   138.2350 ns |   7.5771 ns |   547.00 |    8.71 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 1,458.311 ns |   157.1006 ns |   8.6112 ns |   548.28 |    8.83 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 1,440.047 ns |   310.9973 ns |  17.0468 ns |   541.41 |    9.96 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **1.911 ns** |     **0.1478 ns** |   **0.0081 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     1.707 ns |     0.3620 ns |   0.0198 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     7.912 ns |     0.6399 ns |   0.0351 ns |     4.14 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Cjk    |    28.222 ns |     3.7659 ns |   0.2064 ns |    14.77 |    0.11 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Cjk    |    23.860 ns |     3.5539 ns |   0.1948 ns |    12.49 |    0.10 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,024.722 ns | 1,037.4539 ns |  56.8663 ns | 2,106.59 |   26.91 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,347.442 ns | 1,059.5508 ns |  58.0776 ns | 2,275.50 |   27.62 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,369.438 ns |   305.5952 ns |  16.7507 ns | 2,287.02 |   11.32 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,429.001 ns |   970.7478 ns |  53.2100 ns | 2,318.19 |   25.58 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,452.771 ns |   842.2764 ns |  46.1680 ns | 2,330.63 |   22.61 |         - |          NA |
|                                |       |        |              |               |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **2.189 ns** |     **0.6330 ns** |   **0.0347 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     2.007 ns |     0.5108 ns |   0.0280 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     6.210 ns |     0.3216 ns |   0.0176 ns |     2.84 |    0.04 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16&#39;      | 65536 | Mixed  |    12.313 ns |     1.4626 ns |   0.0802 ns |     5.63 |    0.08 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32&#39;      | 65536 | Mixed  |    12.070 ns |     1.4604 ns |   0.0800 ns |     5.51 |    0.08 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 4,040.840 ns | 1,109.0003 ns |  60.7881 ns | 1,846.10 |   35.09 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 1,916.539 ns |   129.3442 ns |   7.0898 ns |   875.59 |   12.44 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 1,915.798 ns |   220.6147 ns |  12.0926 ns |   875.25 |   13.02 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 1,998.831 ns |   238.9321 ns |  13.0967 ns |   913.19 |   13.66 |         - |          NA |
| &#39;Text.RuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 1,949.880 ns |   950.9554 ns |  52.1251 ns |   890.82 |   24.03 |         - |          NA |
