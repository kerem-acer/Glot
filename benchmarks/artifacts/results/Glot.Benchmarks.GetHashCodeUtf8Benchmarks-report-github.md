```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | N     | Locale | Mean           | Error         | StdDev      | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |---------------:|--------------:|------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **2.0288 ns** |     **0.1645 ns** |   **0.0090 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      1.2841 ns |     0.1191 ns |   0.0065 ns |  0.63 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 8     | Ascii  |      0.2869 ns |     0.0868 ns |   0.0048 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      0.9070 ns |     0.0879 ns |   0.0048 ns |  0.45 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Latin**  |      **2.0473 ns** |     **0.2250 ns** |   **0.0123 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Latin  |      2.9166 ns |     0.1225 ns |   0.0067 ns |  1.42 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Latin  |      0.4231 ns |     0.1552 ns |   0.0085 ns |  0.21 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Latin  |      0.9691 ns |     0.3637 ns |   0.0199 ns |  0.47 |    0.01 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **2.0194 ns** |     **0.1255 ns** |   **0.0069 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      3.0859 ns |     0.2338 ns |   0.0128 ns |  1.53 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Cjk    |      1.4988 ns |     0.1820 ns |   0.0100 ns |  0.74 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      1.9280 ns |     0.3304 ns |   0.0181 ns |  0.95 |    0.01 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Emoji**  |      **2.0143 ns** |     **0.0374 ns** |   **0.0020 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Emoji  |      1.9721 ns |     0.2218 ns |   0.0122 ns |  0.98 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Emoji  |      0.4349 ns |     0.0530 ns |   0.0029 ns |  0.22 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Emoji  |      0.9657 ns |     0.0309 ns |   0.0017 ns |  0.48 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **2.0127 ns** |     **0.3163 ns** |   **0.0173 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      1.3044 ns |     0.2724 ns |   0.0149 ns |  0.65 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 8     | Mixed  |      0.2843 ns |     0.1483 ns |   0.0081 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      0.9325 ns |     0.6011 ns |   0.0330 ns |  0.46 |    0.01 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Ascii**  |     **29.5163 ns** |    **20.3359 ns** |   **1.1147 ns** |  **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Ascii  |      5.7465 ns |     0.5139 ns |   0.0282 ns |  0.19 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 64    | Ascii  |      2.9483 ns |     3.7177 ns |   0.2038 ns |  0.10 |    0.01 |         - |          NA |
| Text.GetHashCode          | 64    | Ascii  |      3.0371 ns |     0.8045 ns |   0.0441 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Latin**  |     **29.5796 ns** |    **20.9309 ns** |   **1.1473 ns** |  **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Latin  |      8.2184 ns |     3.8281 ns |   0.2098 ns |  0.28 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 64    | Latin  |      3.6467 ns |     1.4384 ns |   0.0788 ns |  0.12 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Latin  |      4.0614 ns |     0.3607 ns |   0.0198 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Cjk**    |     **29.5468 ns** |     **1.3227 ns** |   **0.0725 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Cjk    |     15.6965 ns |     1.9209 ns |   0.1053 ns |  0.53 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 64    | Cjk    |      7.3553 ns |     0.8945 ns |   0.0490 ns |  0.25 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Cjk    |      7.7860 ns |     0.4898 ns |   0.0268 ns |  0.26 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Emoji**  |     **29.4283 ns** |     **7.7175 ns** |   **0.4230 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Emoji  |     11.1909 ns |     7.3259 ns |   0.4016 ns |  0.38 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 64    | Emoji  |      4.8197 ns |     1.6073 ns |   0.0881 ns |  0.16 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Emoji  |      5.1126 ns |     0.2038 ns |   0.0112 ns |  0.17 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **64**    | **Mixed**  |     **29.6862 ns** |    **11.8786 ns** |   **0.6511 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 64    | Mixed  |      8.8254 ns |     1.2896 ns |   0.0707 ns |  0.30 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 64    | Mixed  |      3.7209 ns |     0.8082 ns |   0.0443 ns |  0.13 |    0.00 |         - |          NA |
| Text.GetHashCode          | 64    | Mixed  |      4.2860 ns |     2.3978 ns |   0.1314 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **143.2110 ns** |    **35.3541 ns** |   **1.9379 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     20.6033 ns |     2.2059 ns |   0.1209 ns |  0.14 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 256   | Ascii  |     13.3805 ns |     0.8650 ns |   0.0474 ns |  0.09 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     10.0688 ns |     0.0822 ns |   0.0045 ns |  0.07 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Latin**  |    **154.0214 ns** |   **197.1682 ns** |  **10.8075 ns** |  **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Latin  |     27.7597 ns |     4.4952 ns |   0.2464 ns |  0.18 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 256   | Latin  |     15.2906 ns |     1.7196 ns |   0.0943 ns |  0.10 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Latin  |     11.3604 ns |     5.2888 ns |   0.2899 ns |  0.07 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **147.4557 ns** |    **45.1941 ns** |   **2.4772 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     67.8804 ns |    14.1404 ns |   0.7751 ns |  0.46 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 256   | Cjk    |     26.8122 ns |     5.1280 ns |   0.2811 ns |  0.18 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     24.1679 ns |     4.0570 ns |   0.2224 ns |  0.16 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Emoji**  |    **150.7006 ns** |   **153.0202 ns** |   **8.3876 ns** |  **1.00** |    **0.07** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Emoji  |     41.7052 ns |    15.2994 ns |   0.8386 ns |  0.28 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 256   | Emoji  |     20.1607 ns |    13.0948 ns |   0.7178 ns |  0.13 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Emoji  |     15.5002 ns |     2.3065 ns |   0.1264 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **141.5692 ns** |    **67.0547 ns** |   **3.6755 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     33.9260 ns |     5.8282 ns |   0.3195 ns |  0.24 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 256   | Mixed  |     16.5378 ns |     1.2348 ns |   0.0677 ns |  0.12 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     13.2438 ns |     1.7957 ns |   0.0984 ns |  0.09 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Ascii**  |  **2,495.2033 ns** |   **968.3899 ns** |  **53.0807 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Ascii  |    337.3250 ns |   112.7307 ns |   6.1792 ns |  0.14 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 4096  | Ascii  |    120.6982 ns |    30.3876 ns |   1.6656 ns |  0.05 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Ascii  |    115.5113 ns |    32.6032 ns |   1.7871 ns |  0.05 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Latin**  |  **2,468.1915 ns** |   **275.1806 ns** |  **15.0836 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Latin  |    395.0312 ns |    66.0524 ns |   3.6206 ns |  0.16 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 4096  | Latin  |    144.4332 ns |    33.1273 ns |   1.8158 ns |  0.06 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Latin  |    140.6706 ns |    18.8081 ns |   1.0309 ns |  0.06 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Cjk**    |  **2,487.8417 ns** |   **586.7841 ns** |  **32.1636 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Cjk    |    987.2067 ns |   127.9726 ns |   7.0146 ns |  0.40 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 4096  | Cjk    |    351.1994 ns |    23.0256 ns |   1.2621 ns |  0.14 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Cjk    |    347.2824 ns |    70.0780 ns |   3.8412 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Emoji**  |  **2,423.2867 ns** | **1,487.7692 ns** |  **81.5496 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Emoji  |    654.2729 ns |   106.9011 ns |   5.8596 ns |  0.27 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 4096  | Emoji  |    235.1642 ns |    17.3567 ns |   0.9514 ns |  0.10 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Emoji  |    232.1528 ns |    31.5056 ns |   1.7269 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **4096**  | **Mixed**  |  **2,403.6255 ns** |   **547.0643 ns** |  **29.9864 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 4096  | Mixed  |    427.5250 ns |    57.6319 ns |   3.1590 ns |  0.18 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 4096  | Mixed  |    161.5401 ns |    17.4352 ns |   0.9557 ns |  0.07 |    0.00 |         - |          NA |
| Text.GetHashCode          | 4096  | Mixed  |    157.1928 ns |    15.9253 ns |   0.8729 ns |  0.07 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **38,134.3214 ns** | **6,839.9111 ns** | **374.9186 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,268.2374 ns |   893.7165 ns |  48.9876 ns |  0.14 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 65536 | Ascii  |  1,860.0237 ns |   172.5178 ns |   9.4563 ns |  0.05 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Ascii  |  1,848.4298 ns |   203.1356 ns |  11.1345 ns |  0.05 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Latin**  | **37,837.6194 ns** | **2,518.1696 ns** | **138.0294 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Latin  |  6,309.3084 ns |   316.8436 ns |  17.3673 ns |  0.17 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 65536 | Latin  |  2,224.8336 ns |   189.1866 ns |  10.3700 ns |  0.06 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Latin  |  2,235.1605 ns |   251.5030 ns |  13.7857 ns |  0.06 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **38,209.2268 ns** | **9,635.6336 ns** | **528.1616 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 15,600.2575 ns | 1,244.5965 ns |  68.2205 ns |  0.41 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 65536 | Cjk    |  5,603.0749 ns |    82.3449 ns |   4.5136 ns |  0.15 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Cjk    |  5,615.9835 ns | 1,004.8812 ns |  55.0809 ns |  0.15 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Emoji**  | **37,899.7845 ns** | **3,291.8360 ns** | **180.4366 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Emoji  | 10,659.9100 ns | 6,940.4715 ns | 380.4307 ns |  0.28 |    0.01 |         - |          NA |
| U8String.GetHashCode      | 65536 | Emoji  |  3,701.4733 ns |   183.1267 ns |  10.0378 ns |  0.10 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Emoji  |  3,750.9671 ns |   748.6005 ns |  41.0333 ns |  0.10 |    0.00 |         - |          NA |
|                           |       |        |                |               |             |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **37,939.8460 ns** | **3,229.1403 ns** | **177.0001 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  6,810.1079 ns |   343.9224 ns |  18.8515 ns |  0.18 |    0.00 |         - |          NA |
| U8String.GetHashCode      | 65536 | Mixed  |  2,412.5678 ns |   321.3416 ns |  17.6138 ns |  0.06 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Mixed  |  2,420.9381 ns |   103.9196 ns |   5.6962 ns |  0.06 |    0.00 |         - |          NA |
