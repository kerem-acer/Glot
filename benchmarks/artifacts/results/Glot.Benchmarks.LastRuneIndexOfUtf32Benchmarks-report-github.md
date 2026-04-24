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
| **string.LastIndexOf**                 | **64**    | **Ascii**  |      **4.255 ns** |   **0.0242 ns** |   **0.0189 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |      2.369 ns |   0.0478 ns |   0.0424 ns |     0.56 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     22.548 ns |   0.3939 ns |   0.3685 ns |     5.30 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |     21.746 ns |   0.2410 ns |   0.2012 ns |     5.11 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |      8.611 ns |   0.0867 ns |   0.0769 ns |     2.02 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |      4.797 ns |   0.0544 ns |   0.0482 ns |     1.13 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |      2.308 ns |   0.0242 ns |   0.0215 ns |     0.54 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     25.172 ns |   0.2511 ns |   0.2097 ns |     5.92 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |     24.677 ns |   0.4097 ns |   0.3832 ns |     5.80 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |     10.820 ns |   0.0749 ns |   0.0584 ns |     2.54 |    0.02 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |      **1.819 ns** |   **0.0395 ns** |   **0.0350 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |      1.380 ns |   0.0209 ns |   0.0196 ns |     0.76 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |     35.985 ns |   0.4773 ns |   0.4231 ns |    19.79 |    0.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |     37.764 ns |   0.1959 ns |   0.1636 ns |    20.77 |    0.39 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |      4.542 ns |   0.0251 ns |   0.0209 ns |     2.50 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |      4.850 ns |   0.0736 ns |   0.0688 ns |     2.67 |    0.06 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |      6.208 ns |   0.1166 ns |   0.1090 ns |     3.41 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     34.420 ns |   0.3629 ns |   0.3031 ns |    18.93 |    0.38 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |     35.916 ns |   0.6096 ns |   0.5702 ns |    19.75 |    0.47 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |     10.042 ns |   0.1576 ns |   0.1474 ns |     5.52 |    0.13 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |      **3.077 ns** |   **0.0209 ns** |   **0.0175 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |      2.370 ns |   0.0387 ns |   0.0362 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Emoji  |     32.031 ns |   0.4099 ns |   0.3633 ns |    10.41 |    0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Emoji  |     35.226 ns |   0.2880 ns |   0.2248 ns |    11.45 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Emoji  |      4.693 ns |   0.0176 ns |   0.0137 ns |     1.53 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |      4.809 ns |   0.1154 ns |   0.1080 ns |     1.56 |    0.04 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |      5.501 ns |   0.0727 ns |   0.0680 ns |     1.79 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     34.174 ns |   0.6882 ns |   0.6437 ns |    11.11 |    0.21 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Emoji  |     36.997 ns |   0.2220 ns |   0.2077 ns |    12.03 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Emoji  |      5.735 ns |   0.0282 ns |   0.0235 ns |     1.86 |    0.01 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |      **4.276 ns** |   **0.0198 ns** |   **0.0155 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |      2.510 ns |   0.0187 ns |   0.0156 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     29.014 ns |   0.4339 ns |   0.3623 ns |     6.78 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |     28.399 ns |   0.6452 ns |   0.6035 ns |     6.64 |    0.14 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |     11.768 ns |   0.2814 ns |   0.2632 ns |     2.75 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |      4.813 ns |   0.0604 ns |   0.0504 ns |     1.13 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |      2.756 ns |   0.0335 ns |   0.0280 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     33.805 ns |   0.2807 ns |   0.2344 ns |     7.91 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |     35.404 ns |   0.4085 ns |   0.3622 ns |     8.28 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |     10.034 ns |   0.1670 ns |   0.1562 ns |     2.35 |    0.04 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |      **2.328 ns** |   **0.0345 ns** |   **0.0323 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |      1.501 ns |   0.0165 ns |   0.0138 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |     18.873 ns |   0.3233 ns |   0.3024 ns |     8.11 |    0.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |     18.091 ns |   0.2505 ns |   0.2092 ns |     7.77 |    0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |      4.544 ns |   0.0249 ns |   0.0195 ns |     1.95 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |    265.047 ns |   3.5793 ns |   3.1730 ns |   113.86 |    2.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |    133.454 ns |   0.8865 ns |   0.7403 ns |    57.33 |    0.82 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |    668.465 ns |  10.0664 ns |   8.9236 ns |   287.17 |    5.32 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |    666.686 ns |   8.0880 ns |   6.7538 ns |   286.41 |    4.73 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |    704.761 ns |  10.9425 ns |   9.1375 ns |   302.76 |    5.52 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |      **1.876 ns** |   **0.0463 ns** |   **0.0433 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |      1.734 ns |   0.0102 ns |   0.0085 ns |     0.92 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |     36.023 ns |   0.1792 ns |   0.1399 ns |    19.22 |    0.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |     38.123 ns |   0.7136 ns |   0.6675 ns |    20.34 |    0.56 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |      4.575 ns |   0.0823 ns |   0.0688 ns |     2.44 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |    260.510 ns |   1.9652 ns |   1.6410 ns |   138.97 |    3.17 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |    384.551 ns |   4.4508 ns |   3.7166 ns |   205.13 |    4.89 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |    455.264 ns |   6.8399 ns |   6.3980 ns |   242.85 |    6.27 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |    448.512 ns |   4.0398 ns |   3.1540 ns |   239.25 |    5.50 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |    506.975 ns |   4.1264 ns |   3.4458 ns |   270.44 |    6.20 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |      **2.766 ns** |   **0.0214 ns** |   **0.0179 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |      1.794 ns |   0.0216 ns |   0.0202 ns |     0.65 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Emoji  |     31.101 ns |   0.3189 ns |   0.2663 ns |    11.24 |    0.12 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Emoji  |     34.531 ns |   0.2757 ns |   0.2302 ns |    12.48 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Emoji  |      4.575 ns |   0.0495 ns |   0.0463 ns |     1.65 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |    260.402 ns |   5.4133 ns |   5.0636 ns |    94.14 |    1.87 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |    380.001 ns |   3.8454 ns |   3.2111 ns |   137.37 |    1.41 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |    245.618 ns |   3.7785 ns |   3.5344 ns |    88.79 |    1.36 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Emoji  |    249.923 ns |   3.6313 ns |   3.3967 ns |    90.35 |    1.32 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Emoji  |    258.760 ns |   1.4194 ns |   1.1082 ns |    93.54 |    0.70 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |      **3.517 ns** |   **0.0270 ns** |   **0.0225 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |      2.205 ns |   0.0136 ns |   0.0114 ns |     0.63 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |     27.136 ns |   0.9748 ns |   0.9118 ns |     7.72 |    0.26 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |     26.695 ns |   0.6231 ns |   0.5828 ns |     7.59 |    0.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |      8.097 ns |   0.1049 ns |   0.0981 ns |     2.30 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |    258.088 ns |   2.5098 ns |   2.0958 ns |    73.39 |    0.73 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |    173.122 ns |   1.5360 ns |   1.2826 ns |    49.23 |    0.47 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |    431.367 ns |   6.8640 ns |   6.4206 ns |   122.66 |    1.92 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |    430.084 ns |   3.6285 ns |   3.2166 ns |   122.30 |    1.16 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |    485.931 ns |   3.1852 ns |   2.4868 ns |   138.18 |    1.09 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |      **3.541 ns** |   **0.0455 ns** |   **0.0425 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |      1.840 ns |   0.0458 ns |   0.0406 ns |     0.52 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  |     21.244 ns |   0.2098 ns |   0.1752 ns |     6.00 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  |     20.498 ns |   0.4096 ns |   0.3831 ns |     5.79 |    0.12 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  |      6.129 ns |   0.0526 ns |   0.0466 ns |     1.73 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  |  4,030.243 ns |  71.8813 ns |  67.2378 ns | 1,138.39 |   22.64 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  |  1,994.348 ns |  15.7131 ns |  13.1211 ns |   563.33 |    7.45 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 10,622.939 ns | 232.1902 ns | 217.1909 ns | 3,000.57 |   68.85 |       1 B |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 10,635.069 ns | 200.9218 ns | 187.9424 ns | 3,003.99 |   62.11 |       1 B |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 11,534.735 ns | 219.6828 ns | 194.7431 ns | 3,258.11 |   65.21 |       1 B |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |      **2.259 ns** |   **0.0446 ns** |   **0.0396 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |      1.742 ns |   0.0132 ns |   0.0110 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    |     36.105 ns |   0.6642 ns |   0.5888 ns |    15.99 |    0.37 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    |     38.390 ns |   0.4131 ns |   0.3662 ns |    17.00 |    0.33 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    |      4.706 ns |   0.0238 ns |   0.0199 ns |     2.08 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    |  4,000.585 ns |  33.0099 ns |  27.5648 ns | 1,771.69 |   32.06 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    |  6,055.239 ns |  64.5632 ns |  53.9132 ns | 2,681.61 |   50.66 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    |  6,882.250 ns | 142.6451 ns | 126.4511 ns | 3,047.86 |   74.55 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    |  7,546.237 ns | 175.3339 ns | 164.0074 ns | 3,341.91 |   90.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    |  8,017.801 ns |  52.0325 ns |  40.6236 ns | 3,550.75 |   62.22 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |      **2.776 ns** |   **0.0245 ns** |   **0.0204 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |      1.779 ns |   0.0163 ns |   0.0136 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Emoji  |     31.281 ns |   0.1565 ns |   0.1222 ns |    11.27 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Emoji  |     34.592 ns |   0.4471 ns |   0.4182 ns |    12.46 |    0.17 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Emoji  |      4.506 ns |   0.0385 ns |   0.0341 ns |     1.62 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  |  4,010.901 ns |  43.2368 ns |  36.1047 ns | 1,445.14 |   16.14 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  |  6,033.562 ns | 104.3245 ns |  92.4809 ns | 2,173.91 |   35.64 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Emoji  |  3,525.714 ns |  49.2077 ns |  46.0289 ns | 1,270.32 |   18.38 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Emoji  |  3,485.780 ns |  47.8723 ns |  39.9756 ns | 1,255.94 |   16.45 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Emoji  |  3,992.492 ns |  30.2132 ns |  26.7832 ns | 1,438.51 |   13.76 |         - |          NA |
|                                    |       |        |               |             |             |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |      **2.765 ns** |   **0.0168 ns** |   **0.0131 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |      1.745 ns |   0.0054 ns |   0.0042 ns |     0.63 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  |     19.532 ns |   0.1495 ns |   0.1248 ns |     7.07 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  |     19.360 ns |   0.3850 ns |   0.3601 ns |     7.00 |    0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  |      4.899 ns |   0.0500 ns |   0.0468 ns |     1.77 |    0.02 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  |  4,007.383 ns |  25.0645 ns |  20.9300 ns | 1,449.58 |    9.83 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  |  2,612.228 ns |  32.2228 ns |  26.9075 ns |   944.92 |   10.32 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  |  6,644.334 ns | 285.5097 ns | 253.0970 ns | 2,403.44 |   89.16 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  |  7,295.481 ns | 178.6658 ns | 149.1940 ns | 2,638.98 |   53.38 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  |  7,752.669 ns | 113.5668 ns | 106.2305 ns | 2,804.35 |   39.35 |         - |          NA |
