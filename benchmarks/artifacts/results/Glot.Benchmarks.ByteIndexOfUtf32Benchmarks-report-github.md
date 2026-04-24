```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                         | N     | Locale | Mean          | Error       | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |--------------:|------------:|------------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |      **2.668 ns** |   **0.1026 ns** |   **0.0910 ns** |     **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |      2.046 ns |   0.0509 ns |   0.0451 ns |     0.77 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Ascii  |     19.729 ns |   0.4143 ns |   0.3876 ns |     7.40 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Ascii  |     18.834 ns |   0.5001 ns |   0.4678 ns |     7.07 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Ascii  |      4.981 ns |   0.0619 ns |   0.0579 ns |     1.87 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |      4.586 ns |   0.0730 ns |   0.0682 ns |     1.72 |    0.06 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |      2.889 ns |   0.0624 ns |   0.0583 ns |     1.08 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     26.953 ns |   0.5774 ns |   0.5401 ns |    10.11 |    0.38 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     25.858 ns |   0.4234 ns |   0.3960 ns |     9.70 |    0.34 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     12.706 ns |   0.4490 ns |   0.3980 ns |     4.77 |    0.21 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |      **1.268 ns** |   **0.0135 ns** |   **0.0120 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |      1.618 ns |   0.0262 ns |   0.0233 ns |     1.28 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Cjk    |     36.035 ns |   0.3881 ns |   0.3441 ns |    28.43 |    0.37 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Cjk    |     37.218 ns |   0.5388 ns |   0.5040 ns |    29.36 |    0.47 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Cjk    |      3.171 ns |   0.0351 ns |   0.0329 ns |     2.50 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |      4.531 ns |   0.0761 ns |   0.0712 ns |     3.57 |    0.06 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |      7.930 ns |   0.1679 ns |   0.1571 ns |     6.26 |    0.13 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     36.373 ns |   0.5041 ns |   0.4715 ns |    28.70 |    0.45 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |     37.827 ns |   0.9981 ns |   0.8848 ns |    29.84 |    0.73 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |     11.344 ns |   0.2536 ns |   0.2372 ns |     8.95 |    0.20 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Emoji**  |      **1.864 ns** |   **0.0287 ns** |   **0.0268 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Emoji  |      1.688 ns |   0.0451 ns |   0.0376 ns |     0.91 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Emoji  |     31.375 ns |   0.6878 ns |   0.6098 ns |    16.84 |    0.39 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Emoji  |     35.463 ns |   0.5464 ns |   0.5111 ns |    19.03 |    0.37 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Emoji  |      3.217 ns |   0.0446 ns |   0.0417 ns |     1.73 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Emoji  |      4.619 ns |   0.1010 ns |   0.0944 ns |     2.48 |    0.06 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Emoji  |      7.136 ns |   0.1451 ns |   0.1357 ns |     3.83 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     36.349 ns |   0.6969 ns |   0.5820 ns |    19.50 |    0.40 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |     39.369 ns |   0.6521 ns |   0.6100 ns |    21.13 |    0.43 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |      6.953 ns |   0.1675 ns |   0.1567 ns |     3.73 |    0.10 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |      **2.181 ns** |   **0.0301 ns** |   **0.0267 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |      1.975 ns |   0.0447 ns |   0.0373 ns |     0.91 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Mixed  |     23.114 ns |   0.4997 ns |   0.4674 ns |    10.60 |    0.24 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Mixed  |     22.926 ns |   0.3761 ns |   0.3518 ns |    10.51 |    0.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Mixed  |      5.758 ns |   0.0858 ns |   0.0803 ns |     2.64 |    0.05 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |      4.568 ns |   0.0679 ns |   0.0635 ns |     2.10 |    0.04 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |      3.350 ns |   0.0582 ns |   0.0544 ns |     1.54 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     36.767 ns |   0.4606 ns |   0.4309 ns |    16.86 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |     37.964 ns |   0.3908 ns |   0.3656 ns |    17.41 |    0.26 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |     11.384 ns |   0.1233 ns |   0.1093 ns |     5.22 |    0.08 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |      **2.635 ns** |   **0.0497 ns** |   **0.0465 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |      2.020 ns |   0.0229 ns |   0.0191 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     19.464 ns |   0.3133 ns |   0.2931 ns |     7.39 |    0.17 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     18.963 ns |   0.3034 ns |   0.2838 ns |     7.20 |    0.16 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Ascii  |      4.913 ns |   0.0683 ns |   0.0605 ns |     1.86 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |    271.551 ns |   4.7730 ns |   4.4647 ns |   103.08 |    2.41 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |    136.634 ns |   2.6921 ns |   2.5182 ns |    51.86 |    1.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |    695.523 ns |  12.4971 ns |  11.6898 ns |   264.01 |    6.23 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |    709.351 ns |  12.8999 ns |  12.0666 ns |   269.26 |    6.39 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |    730.648 ns |  16.2453 ns |  15.1959 ns |   277.34 |    7.32 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |      **1.248 ns** |   **0.0171 ns** |   **0.0160 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |      1.591 ns |   0.0462 ns |   0.0386 ns |     1.28 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     36.351 ns |   0.3852 ns |   0.3603 ns |    29.14 |    0.46 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Cjk    |     37.935 ns |   0.4969 ns |   0.4648 ns |    30.41 |    0.52 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Cjk    |      3.104 ns |   0.0212 ns |   0.0198 ns |     2.49 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |    263.515 ns |   1.5851 ns |   1.2375 ns |   211.24 |    2.77 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |    394.791 ns |   5.9830 ns |   5.5965 ns |   316.48 |    5.84 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |    510.559 ns |   9.6309 ns |   9.0087 ns |   409.28 |    8.62 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |    506.247 ns |   7.0321 ns |   6.5778 ns |   405.83 |    7.15 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |    520.209 ns |   9.1771 ns |   8.5842 ns |   417.02 |    8.41 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Emoji**  |      **1.865 ns** |   **0.0275 ns** |   **0.0243 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Emoji  |      1.698 ns |   0.0216 ns |   0.0181 ns |     0.91 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Emoji  |     31.896 ns |   0.3857 ns |   0.3608 ns |    17.10 |    0.28 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Emoji  |     35.592 ns |   0.4648 ns |   0.4347 ns |    19.08 |    0.33 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Emoji  |      2.970 ns |   0.0337 ns |   0.0315 ns |     1.59 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Emoji  |    260.613 ns |   1.7305 ns |   1.4450 ns |   139.74 |    1.90 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Emoji  |    415.289 ns |   8.4383 ns |   7.8932 ns |   222.68 |    4.96 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |    271.117 ns |   3.5275 ns |   3.1270 ns |   145.38 |    2.44 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |    275.657 ns |   4.4276 ns |   3.9250 ns |   147.81 |    2.75 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |    261.719 ns |   2.5089 ns |   2.0951 ns |   140.34 |    2.06 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |      **2.197 ns** |   **0.0254 ns** |   **0.0237 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |      2.006 ns |   0.0971 ns |   0.0908 ns |     0.91 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     22.775 ns |   0.5478 ns |   0.5124 ns |    10.37 |    0.25 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     22.239 ns |   0.3567 ns |   0.3336 ns |    10.12 |    0.18 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Mixed  |      5.688 ns |   0.0728 ns |   0.0681 ns |     2.59 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |    264.858 ns |   3.1573 ns |   2.9533 ns |   120.57 |    1.80 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |    179.757 ns |   3.3761 ns |   3.1580 ns |    81.83 |    1.63 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |    491.576 ns |   8.0467 ns |   7.1332 ns |   223.78 |    3.90 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |    490.032 ns |  10.1971 ns |   9.0395 ns |   223.07 |    4.60 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |    497.344 ns |  10.3281 ns |   9.6609 ns |   226.40 |    4.86 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |      **2.664 ns** |   **0.0501 ns** |   **0.0418 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |      2.132 ns |   0.1099 ns |   0.1028 ns |     0.80 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     19.578 ns |   0.3065 ns |   0.2560 ns |     7.35 |    0.14 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     18.541 ns |   0.1951 ns |   0.1629 ns |     6.96 |    0.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Ascii  |      4.948 ns |   0.0716 ns |   0.0670 ns |     1.86 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  |  4,107.197 ns |  65.6964 ns |  61.4524 ns | 1,542.21 |   32.21 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  |  2,035.647 ns |  33.2065 ns |  31.0613 ns |   764.37 |   16.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 10,916.981 ns | 174.5024 ns | 163.2297 ns | 4,099.22 |   85.58 |       1 B |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 10,779.330 ns |  79.8370 ns |  66.6675 ns | 4,047.53 |   65.49 |       1 B |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 11,406.542 ns |  75.0100 ns |  58.5629 ns | 4,283.04 |   67.82 |       1 B |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |      **1.257 ns** |   **0.0279 ns** |   **0.0261 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |      1.681 ns |   0.0684 ns |   0.0640 ns |     1.34 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     36.219 ns |   0.4463 ns |   0.4174 ns |    28.83 |    0.66 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Cjk    |     37.680 ns |   0.3953 ns |   0.3698 ns |    30.00 |    0.66 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Cjk    |      3.166 ns |   0.0468 ns |   0.0438 ns |     2.52 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    |  4,103.135 ns |  65.3562 ns |  61.1343 ns | 3,266.32 |   80.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    |  6,104.978 ns |  54.2192 ns |  45.2755 ns | 4,859.89 |  102.30 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    |  7,841.923 ns | 144.3118 ns | 134.9893 ns | 6,242.60 |  161.55 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    |  7,948.553 ns | 233.8868 ns | 207.3346 ns | 6,327.48 |  202.78 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    |  8,139.436 ns |  70.7728 ns |  59.0985 ns | 6,479.43 |  136.07 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Emoji**  |      **1.821 ns** |   **0.0264 ns** |   **0.0221 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Emoji  |      1.725 ns |   0.0626 ns |   0.0555 ns |     0.95 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Emoji  |     31.245 ns |   0.4800 ns |   0.4490 ns |    17.16 |    0.31 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Emoji  |     35.436 ns |   0.7544 ns |   0.7057 ns |    19.46 |    0.44 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Emoji  |      2.979 ns |   0.0563 ns |   0.0527 ns |     1.64 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Emoji  |  4,181.772 ns |  72.6970 ns |  68.0008 ns | 2,296.86 |   45.10 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Emoji  |  6,777.816 ns |  73.9691 ns |  69.1907 ns | 3,722.74 |   57.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  |  4,004.541 ns | 107.4233 ns |  95.2280 ns | 2,199.51 |   56.74 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  |  4,019.558 ns |  53.7849 ns |  47.6789 ns | 2,207.76 |   36.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  |  4,179.805 ns |  61.9732 ns |  57.9697 ns | 2,295.78 |   40.93 |         - |          NA |
|                                |       |        |               |             |             |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |      **2.232 ns** |   **0.0321 ns** |   **0.0300 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |      2.038 ns |   0.0844 ns |   0.0790 ns |     0.91 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     23.061 ns |   0.4507 ns |   0.4216 ns |    10.33 |    0.23 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     22.246 ns |   0.4251 ns |   0.3976 ns |     9.97 |    0.22 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Mixed  |      5.707 ns |   0.0825 ns |   0.0772 ns |     2.56 |    0.05 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  |  4,171.270 ns |  65.5997 ns |  61.3620 ns | 1,869.28 |   36.13 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  |  2,722.328 ns |  39.4337 ns |  36.8863 ns | 1,219.96 |   22.58 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  |  7,579.175 ns | 148.9300 ns | 139.3092 ns | 3,396.48 |   74.98 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  |  8,482.646 ns | 165.1991 ns | 154.5273 ns | 3,801.35 |   83.43 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  |  7,928.011 ns | 145.2488 ns | 135.8658 ns | 3,552.80 |   75.03 |         - |          NA |
