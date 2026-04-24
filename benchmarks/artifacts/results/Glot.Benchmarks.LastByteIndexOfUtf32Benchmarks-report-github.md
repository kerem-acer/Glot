```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                             | N     | Locale | Mean          | Error       | StdDev      | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |--------------:|------------:|------------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |      **4.316 ns** |   **0.0681 ns** |   **0.0637 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |      2.351 ns |   0.0713 ns |   0.0667 ns |     0.54 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |     22.470 ns |   0.5913 ns |   0.5531 ns |     5.21 |    0.14 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |     21.653 ns |   0.4713 ns |   0.4408 ns |     5.02 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |      8.936 ns |   0.1476 ns |   0.1381 ns |     2.07 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |      4.929 ns |   0.1019 ns |   0.0953 ns |     1.14 |    0.03 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |      2.371 ns |   0.0567 ns |   0.0530 ns |     0.55 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     24.915 ns |   0.5442 ns |   0.5090 ns |     5.77 |    0.14 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     23.785 ns |   0.4786 ns |   0.4477 ns |     5.51 |    0.13 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     10.921 ns |   0.1653 ns |   0.1546 ns |     2.53 |    0.05 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |      **1.819 ns** |   **0.0395 ns** |   **0.0369 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |      1.370 ns |   0.0201 ns |   0.0188 ns |     0.75 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |     35.110 ns |   0.5423 ns |   0.5073 ns |    19.30 |    0.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |     36.217 ns |   0.5940 ns |   0.5556 ns |    19.91 |    0.49 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |      3.454 ns |   0.0520 ns |   0.0487 ns |     1.90 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |      4.820 ns |   0.0969 ns |   0.0906 ns |     2.65 |    0.07 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |      6.207 ns |   0.0627 ns |   0.0524 ns |     3.41 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     35.024 ns |   0.5955 ns |   0.5570 ns |    19.26 |    0.48 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |     36.307 ns |   0.4931 ns |   0.4612 ns |    19.96 |    0.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |      9.852 ns |   0.1216 ns |   0.1137 ns |     5.42 |    0.12 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |      **3.094 ns** |   **0.0468 ns** |   **0.0438 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |      2.412 ns |   0.0381 ns |   0.0337 ns |     0.78 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Emoji  |     31.352 ns |   0.2413 ns |   0.2015 ns |    10.13 |    0.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Emoji  |     34.658 ns |   0.3181 ns |   0.2656 ns |    11.20 |    0.17 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Emoji  |      3.770 ns |   0.0190 ns |   0.0159 ns |     1.22 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |      4.836 ns |   0.0830 ns |   0.0776 ns |     1.56 |    0.03 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |      5.548 ns |   0.0636 ns |   0.0564 ns |     1.79 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     33.548 ns |   0.4502 ns |   0.4212 ns |    10.84 |    0.20 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |     37.274 ns |   0.3892 ns |   0.3450 ns |    12.05 |    0.20 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |      5.476 ns |   0.0256 ns |   0.0227 ns |     1.77 |    0.03 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |      **4.319 ns** |   **0.0163 ns** |   **0.0136 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |      2.528 ns |   0.0208 ns |   0.0173 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |     27.779 ns |   0.4194 ns |   0.3718 ns |     6.43 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |     27.546 ns |   0.4781 ns |   0.4238 ns |     6.38 |    0.10 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |     10.835 ns |   0.2833 ns |   0.2650 ns |     2.51 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |      4.780 ns |   0.1028 ns |   0.0911 ns |     1.11 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |      2.757 ns |   0.0673 ns |   0.0629 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     34.218 ns |   0.8822 ns |   0.8253 ns |     7.92 |    0.19 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |     35.793 ns |   0.5708 ns |   0.5339 ns |     8.29 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |     10.007 ns |   0.0969 ns |   0.0906 ns |     2.32 |    0.02 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |      **2.308 ns** |   **0.0236 ns** |   **0.0209 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |      1.475 ns |   0.0269 ns |   0.0239 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     18.440 ns |   0.2799 ns |   0.2481 ns |     7.99 |    0.13 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     17.688 ns |   0.3307 ns |   0.3093 ns |     7.66 |    0.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |      3.439 ns |   0.0203 ns |   0.0169 ns |     1.49 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |    260.290 ns |   3.6922 ns |   3.0832 ns |   112.76 |    1.63 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |    138.795 ns |   2.4279 ns |   2.2710 ns |    60.13 |    1.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |    678.820 ns |  10.8113 ns |  10.1129 ns |   294.08 |    4.97 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |    668.366 ns |   6.2713 ns |   5.5593 ns |   289.55 |    3.45 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |    705.982 ns |   5.8573 ns |   5.4789 ns |   305.85 |    3.54 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |      **1.834 ns** |   **0.0088 ns** |   **0.0078 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |      1.762 ns |   0.0103 ns |   0.0096 ns |     0.96 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     34.777 ns |   0.1584 ns |   0.1404 ns |    18.96 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |     36.109 ns |   0.5176 ns |   0.4842 ns |    19.69 |    0.27 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |      3.447 ns |   0.0103 ns |   0.0092 ns |     1.88 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |    260.682 ns |   3.2021 ns |   2.8386 ns |   142.12 |    1.61 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |    383.170 ns |   3.2532 ns |   2.8839 ns |   208.91 |    1.75 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |    454.318 ns |   7.4386 ns |   6.2115 ns |   247.70 |    3.42 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |    471.920 ns |  16.6421 ns |  15.5670 ns |   257.29 |    8.29 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |    524.023 ns |   4.1795 ns |   3.7050 ns |   285.70 |    2.28 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |      **2.769 ns** |   **0.0395 ns** |   **0.0370 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |     17.748 ns |   0.4112 ns |   0.3846 ns |     6.41 |    0.16 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Emoji  |     31.267 ns |   0.3520 ns |   0.3293 ns |    11.29 |    0.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Emoji  |     34.869 ns |   0.3757 ns |   0.3514 ns |    12.59 |    0.20 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Emoji  |      3.422 ns |   0.0417 ns |   0.0390 ns |     1.24 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |    267.923 ns |   5.4695 ns |   5.1162 ns |    96.76 |    2.18 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |    395.085 ns |   2.8442 ns |   2.2205 ns |   142.68 |    1.98 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |    248.685 ns |   6.7301 ns |   6.2953 ns |    89.81 |    2.48 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |    250.518 ns |   5.3017 ns |   4.9592 ns |    90.47 |    2.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |    264.670 ns |   5.0783 ns |   4.7502 ns |    95.58 |    2.06 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |      **3.516 ns** |   **0.0510 ns** |   **0.0477 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |      2.264 ns |   0.0572 ns |   0.0535 ns |     0.64 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     25.700 ns |   0.5891 ns |   0.5510 ns |     7.31 |    0.18 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     26.588 ns |   0.8228 ns |   0.7696 ns |     7.56 |    0.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |      7.457 ns |   0.1181 ns |   0.1104 ns |     2.12 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |    262.168 ns |   3.5722 ns |   3.3414 ns |    74.58 |    1.34 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |    173.037 ns |   2.1736 ns |   1.9269 ns |    49.22 |    0.83 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |    431.142 ns |   4.3740 ns |   3.6525 ns |   122.64 |    1.89 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |    434.864 ns |   7.7551 ns |   6.8747 ns |   123.70 |    2.48 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |    492.249 ns |   2.5154 ns |   2.3529 ns |   140.02 |    1.94 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |      **3.557 ns** |   **0.0509 ns** |   **0.0476 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |      1.844 ns |   0.0243 ns |   0.0227 ns |     0.52 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     20.777 ns |   0.5033 ns |   0.4708 ns |     5.84 |    0.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     19.922 ns |   0.3945 ns |   0.3690 ns |     5.60 |    0.12 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |      5.649 ns |   0.0494 ns |   0.0462 ns |     1.59 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  |  4,146.986 ns |  63.4826 ns |  59.3816 ns | 1,166.02 |   22.04 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  |  2,076.085 ns |  28.2000 ns |  26.3783 ns |   583.74 |   10.38 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 11,067.315 ns | 263.4484 ns | 246.4298 ns | 3,111.82 |   78.10 |       1 B |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 10,993.945 ns | 185.4111 ns | 173.4337 ns | 3,091.19 |   61.70 |       1 B |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 11,733.021 ns | 253.3380 ns | 224.5775 ns | 3,299.00 |   74.28 |       1 B |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |      **2.196 ns** |   **0.0114 ns** |   **0.0089 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |      1.754 ns |   0.0297 ns |   0.0278 ns |     0.80 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     35.088 ns |   0.2722 ns |   0.2125 ns |    15.98 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |     36.273 ns |   0.5381 ns |   0.5033 ns |    16.52 |    0.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |      3.768 ns |   0.0432 ns |   0.0404 ns |     1.72 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    |  4,086.969 ns |  71.2390 ns |  66.6370 ns | 1,861.49 |   30.27 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    |  6,104.535 ns |  71.0135 ns |  66.4260 ns | 2,780.43 |   31.22 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    |  7,294.929 ns | 184.9092 ns | 172.9642 ns | 3,322.62 |   77.39 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    |  7,626.502 ns | 360.8707 ns | 337.5587 ns | 3,473.64 |  149.55 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    |  8,135.088 ns |  74.5123 ns |  66.0532 ns | 3,705.28 |   32.41 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |      **2.881 ns** |   **0.0261 ns** |   **0.0244 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |      1.804 ns |   0.0263 ns |   0.0246 ns |     0.63 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Emoji  |     31.220 ns |   0.3928 ns |   0.3482 ns |    10.84 |    0.15 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Emoji  |     38.826 ns |   0.3793 ns |   0.3548 ns |    13.48 |    0.16 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Emoji  |      3.402 ns |   0.0318 ns |   0.0297 ns |     1.18 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  |  4,101.057 ns |  33.4733 ns |  31.3110 ns | 1,423.37 |   15.75 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  |  6,076.888 ns |  48.4803 ns |  42.9766 ns | 2,109.13 |   22.57 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  |  3,602.649 ns |  88.1640 ns |  82.4687 ns | 1,250.39 |   29.57 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  |  3,596.113 ns |  90.5050 ns |  80.2303 ns | 1,248.12 |   28.80 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  |  4,131.190 ns |  93.0995 ns |  87.0854 ns | 1,433.83 |   31.56 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |      **2.829 ns** |   **0.0335 ns** |   **0.0313 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |      1.740 ns |   0.0156 ns |   0.0139 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     19.777 ns |   0.2818 ns |   0.2498 ns |     6.99 |    0.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     19.805 ns |   0.2139 ns |   0.2001 ns |     7.00 |    0.10 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |      4.200 ns |   0.0598 ns |   0.0559 ns |     1.48 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  |  4,180.019 ns |  43.3341 ns |  40.5347 ns | 1,477.57 |   21.06 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  |  2,721.638 ns |  37.5877 ns |  35.1596 ns |   962.06 |   15.85 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  |  7,148.057 ns | 280.7476 ns | 262.6115 ns | 2,526.73 |   93.88 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  |  8,063.030 ns | 223.7038 ns | 209.2527 ns | 2,850.16 |   77.87 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  |  7,855.652 ns | 119.6632 ns | 111.9330 ns | 2,776.85 |   48.53 |         - |          NA |
