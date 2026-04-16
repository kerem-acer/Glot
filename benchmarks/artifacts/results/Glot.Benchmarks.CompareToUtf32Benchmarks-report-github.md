```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean            | Error          | StdDev        | Ratio  | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |----------------:|---------------:|--------------:|-------:|--------:|----------:|------------:|
| **string.Compare**                 | **8**     | **Ascii**  |       **1.0143 ns** |      **0.2649 ns** |     **0.0145 ns** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Ascii  |       2.2307 ns |      3.9228 ns |     0.2150 ns |   2.20 |    0.19 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Ascii  |       3.7880 ns |      0.5969 ns |     0.0327 ns |   3.74 |    0.05 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Ascii  |      15.6942 ns |      1.0999 ns |     0.0603 ns |  15.48 |    0.20 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Ascii  |      16.4273 ns |      0.8156 ns |     0.0447 ns |  16.20 |    0.20 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **8**     | **Latin**  |       **1.0623 ns** |      **0.1027 ns** |     **0.0056 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Latin  |       0.5954 ns |      0.3381 ns |     0.0185 ns |   0.56 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Latin  |       1.8917 ns |      0.0382 ns |     0.0021 ns |   1.78 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Latin  |      25.3169 ns |      4.5945 ns |     0.2518 ns |  23.83 |    0.23 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Latin  |      22.8317 ns |      1.8247 ns |     0.1000 ns |  21.49 |    0.13 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **8**     | **Cjk**    |       **1.0401 ns** |      **0.2001 ns** |     **0.0110 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Cjk    |       4.5469 ns |      0.9585 ns |     0.0525 ns |   4.37 |    0.06 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Cjk    |       6.4900 ns |      2.4494 ns |     0.1343 ns |   6.24 |    0.13 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Cjk    |      63.8248 ns |     11.5330 ns |     0.6322 ns |  61.37 |    0.77 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Cjk    |      49.3349 ns |      6.2613 ns |     0.3432 ns |  47.44 |    0.52 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **8**     | **Emoji**  |       **0.7997 ns** |      **0.0219 ns** |     **0.0012 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Emoji  |       1.2820 ns |      0.5376 ns |     0.0295 ns |   1.60 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Emoji  |       2.6196 ns |      0.2798 ns |     0.0153 ns |   3.28 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Emoji  |      35.3345 ns |      2.8329 ns |     0.1553 ns |  44.18 |    0.18 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Emoji  |      26.2471 ns |      2.9130 ns |     0.1597 ns |  32.82 |    0.18 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **8**     | **Mixed**  |       **1.0303 ns** |      **0.2022 ns** |     **0.0111 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Mixed  |       1.9241 ns |      3.8245 ns |     0.2096 ns |   1.87 |    0.18 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Mixed  |       3.7770 ns |      0.2272 ns |     0.0125 ns |   3.67 |    0.04 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Mixed  |      16.7081 ns |      5.5817 ns |     0.3060 ns |  16.22 |    0.30 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Mixed  |      17.5720 ns |      3.3372 ns |     0.1829 ns |  17.06 |    0.22 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **64**    | **Ascii**  |       **6.4900 ns** |      **0.4310 ns** |     **0.0236 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 64    | Ascii  |       5.0123 ns |      0.9582 ns |     0.0525 ns |   0.77 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 64    | Ascii  |       6.1961 ns |      2.6297 ns |     0.1441 ns |   0.95 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 64    | Ascii  |     100.6138 ns |      9.1131 ns |     0.4995 ns |  15.50 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 64    | Ascii  |     109.1554 ns |      3.6609 ns |     0.2007 ns |  16.82 |    0.06 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **64**    | **Latin**  |       **6.7726 ns** |      **0.8725 ns** |     **0.0478 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 64    | Latin  |       5.4368 ns |      1.2370 ns |     0.0678 ns |   0.80 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 64    | Latin  |       6.7881 ns |      0.7276 ns |     0.0399 ns |   1.00 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 64    | Latin  |     157.0198 ns |     35.9310 ns |     1.9695 ns |  23.19 |    0.29 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 64    | Latin  |     138.5208 ns |      6.8212 ns |     0.3739 ns |  20.45 |    0.13 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **64**    | **Cjk**    |       **6.6949 ns** |      **1.3036 ns** |     **0.0715 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 64    | Cjk    |       7.4064 ns |      1.7771 ns |     0.0974 ns |   1.11 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 64    | Cjk    |       8.3772 ns |      1.3929 ns |     0.0763 ns |   1.25 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 64    | Cjk    |     467.4457 ns |     40.7343 ns |     2.2328 ns |  69.83 |    0.71 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 64    | Cjk    |     340.6706 ns |     12.8628 ns |     0.7051 ns |  50.89 |    0.48 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **64**    | **Emoji**  |       **5.2412 ns** |      **1.0391 ns** |     **0.0570 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 64    | Emoji  |       6.1084 ns |      2.6249 ns |     0.1439 ns |   1.17 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 64    | Emoji  |       7.0916 ns |      0.4014 ns |     0.0220 ns |   1.35 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 64    | Emoji  |     254.2641 ns |      1.3368 ns |     0.0733 ns |  48.52 |    0.46 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 64    | Emoji  |     183.7726 ns |     14.2327 ns |     0.7801 ns |  35.07 |    0.36 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **64**    | **Mixed**  |       **6.7708 ns** |      **0.1860 ns** |     **0.0102 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 64    | Mixed  |       5.8300 ns |      0.8346 ns |     0.0457 ns |   0.86 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 64    | Mixed  |       6.9520 ns |      0.3330 ns |     0.0183 ns |   1.03 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 64    | Mixed  |     160.9462 ns |     85.9180 ns |     4.7095 ns |  23.77 |    0.60 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 64    | Mixed  |     136.9629 ns |      6.0908 ns |     0.3339 ns |  20.23 |    0.05 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **256**   | **Ascii**  |      **11.2067 ns** |      **1.1839 ns** |     **0.0649 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Ascii  |       8.4210 ns |      1.8666 ns |     0.1023 ns |   0.75 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Ascii  |       9.4430 ns |      1.7139 ns |     0.0939 ns |   0.84 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Ascii  |     375.6653 ns |     15.6459 ns |     0.8576 ns |  33.52 |    0.18 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Ascii  |     396.8377 ns |     12.7567 ns |     0.6992 ns |  35.41 |    0.19 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **256**   | **Latin**  |      **11.4403 ns** |      **0.8066 ns** |     **0.0442 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Latin  |       9.4392 ns |      0.5276 ns |     0.0289 ns |   0.83 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Latin  |      10.4047 ns |      0.4850 ns |     0.0266 ns |   0.91 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Latin  |     614.9419 ns |     62.9322 ns |     3.4495 ns |  53.75 |    0.32 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Latin  |     526.8758 ns |     35.3677 ns |     1.9386 ns |  46.05 |    0.21 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **256**   | **Cjk**    |      **11.4525 ns** |      **2.3852 ns** |     **0.1307 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Cjk    |      16.5272 ns |      4.9151 ns |     0.2694 ns |   1.44 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Cjk    |      17.7212 ns |      4.6935 ns |     0.2573 ns |   1.55 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Cjk    |   1,867.1306 ns |    500.2971 ns |    27.4230 ns | 163.05 |    2.63 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Cjk    |   1,333.3102 ns |     89.6534 ns |     4.9142 ns | 116.43 |    1.22 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **256**   | **Emoji**  |      **10.8718 ns** |      **2.7942 ns** |     **0.1532 ns** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Emoji  |      12.1539 ns |      2.4382 ns |     0.1336 ns |   1.12 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Emoji  |      13.3221 ns |      1.9104 ns |     0.1047 ns |   1.23 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Emoji  |     983.1752 ns |     94.3372 ns |     5.1709 ns |  90.45 |    1.19 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Emoji  |     697.6039 ns |     44.9740 ns |     2.4652 ns |  64.17 |    0.81 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **256**   | **Mixed**  |      **11.4882 ns** |      **1.4312 ns** |     **0.0785 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Mixed  |       9.6328 ns |      1.2249 ns |     0.0671 ns |   0.84 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Mixed  |      10.5257 ns |      1.8048 ns |     0.0989 ns |   0.92 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Mixed  |     637.2785 ns |    297.4701 ns |    16.3053 ns |  55.47 |    1.27 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Mixed  |     540.6479 ns |     18.3706 ns |     1.0070 ns |  47.06 |    0.29 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **4096**  | **Ascii**  |     **173.5375 ns** |     **10.5803 ns** |     **0.5799 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 4096  | Ascii  |      78.0961 ns |      6.2771 ns |     0.3441 ns |   0.45 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 4096  | Ascii  |      79.5554 ns |      5.8752 ns |     0.3220 ns |   0.46 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 4096  | Ascii  |   6,328.1305 ns |    304.4121 ns |    16.6859 ns |  36.47 |    0.13 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 4096  | Ascii  |   6,376.2321 ns |    581.2560 ns |    31.8606 ns |  36.74 |    0.19 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **4096**  | **Latin**  |     **173.3051 ns** |      **5.8468 ns** |     **0.3205 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 4096  | Latin  |      91.3635 ns |     10.3604 ns |     0.5679 ns |   0.53 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 4096  | Latin  |      93.1922 ns |     17.4447 ns |     0.9562 ns |   0.54 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 4096  | Latin  |   9,638.3908 ns |  1,325.6973 ns |    72.6659 ns |  55.62 |    0.37 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 4096  | Latin  |   8,236.5536 ns |    687.3268 ns |    37.6747 ns |  47.53 |    0.20 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **4096**  | **Cjk**    |     **172.5480 ns** |     **21.3912 ns** |     **1.1725 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 4096  | Cjk    |     202.1092 ns |     21.5243 ns |     1.1798 ns |   1.17 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 4096  | Cjk    |     208.0332 ns |     24.7144 ns |     1.3547 ns |   1.21 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 4096  | Cjk    |  29,468.0345 ns |  1,808.1197 ns |    99.1091 ns | 170.79 |    1.12 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 4096  | Cjk    |  21,055.9960 ns |    899.4730 ns |    49.3031 ns | 122.03 |    0.76 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **4096**  | **Emoji**  |     **173.7112 ns** |     **39.9494 ns** |     **2.1898 ns** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 4096  | Emoji  |     139.1024 ns |     16.3230 ns |     0.8947 ns |   0.80 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 4096  | Emoji  |     144.2932 ns |     22.9882 ns |     1.2601 ns |   0.83 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 4096  | Emoji  |  15,525.2584 ns |  2,948.0599 ns |   161.5931 ns |  89.38 |    1.26 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 4096  | Emoji  |  11,014.7718 ns |    432.1317 ns |    23.6866 ns |  63.42 |    0.70 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **4096**  | **Mixed**  |     **174.6508 ns** |     **27.9455 ns** |     **1.5318 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 4096  | Mixed  |      97.7574 ns |     29.5077 ns |     1.6174 ns |   0.56 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 4096  | Mixed  |     100.2446 ns |      3.1058 ns |     0.1702 ns |   0.57 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 4096  | Mixed  |   9,445.2996 ns |  1,452.9350 ns |    79.6403 ns |  54.08 |    0.57 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 4096  | Mixed  |   8,563.2996 ns |  1,245.0115 ns |    68.2433 ns |  49.03 |    0.50 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **65536** | **Ascii**  |   **3,326.6340 ns** |    **199.8496 ns** |    **10.9544 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1,014.3330 ns |    166.1312 ns |     9.1062 ns |   0.30 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  |   1,014.8400 ns |     70.0749 ns |     3.8410 ns |   0.31 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  |  93,761.9629 ns |  7,387.3872 ns |   404.9276 ns |  28.19 |    0.13 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  |  93,963.0262 ns |  8,686.9597 ns |   476.1616 ns |  28.25 |    0.15 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **65536** | **Latin**  |   **3,333.9900 ns** |     **71.1089 ns** |     **3.8977 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Latin  |   1,458.8267 ns |    111.1199 ns |     6.0909 ns |   0.44 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Latin  |   1,371.1712 ns |    113.5525 ns |     6.2242 ns |   0.41 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Latin  | 154,795.1964 ns |  6,966.7264 ns |   381.8698 ns |  46.43 |    0.11 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Latin  | 140,931.4575 ns | 24,727.2623 ns | 1,355.3847 ns |  42.27 |    0.35 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3,097.2734 ns** |    **175.7722 ns** |     **9.6347 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3,314.2927 ns |    790.5738 ns |    43.3340 ns |   1.07 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    |   3,294.0534 ns |    104.4376 ns |     5.7246 ns |   1.06 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    | 482,964.1317 ns | 31,567.0173 ns | 1,730.2947 ns | 155.93 |    0.64 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    | 327,737.3862 ns | 23,237.1997 ns | 1,273.7093 ns | 105.82 |    0.46 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **65536** | **Emoji**  |   **3,236.3144 ns** |    **755.8383 ns** |    **41.4300 ns** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Emoji  |   2,342.2522 ns |    641.7266 ns |    35.1752 ns |   0.72 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Emoji  |   2,318.6246 ns |     63.5142 ns |     3.4814 ns |   0.72 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Emoji  | 251,308.7091 ns | 10,727.7958 ns |   588.0267 ns |  77.66 |    0.87 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Emoji  | 178,220.4708 ns |  8,198.1450 ns |   449.3680 ns |  55.07 |    0.62 |         - |          NA |
|                                |       |        |                 |                |               |        |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3,215.9283 ns** |    **578.3332 ns** |    **31.7004 ns** |   **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1,576.3570 ns |     44.3921 ns |     2.4333 ns |   0.49 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  |   1,587.9626 ns |     35.0610 ns |     1.9218 ns |   0.49 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  | 164,067.6506 ns | 24,832.6664 ns | 1,361.1622 ns |  51.02 |    0.57 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  | 138,172.0786 ns | 34,001.6181 ns | 1,863.7434 ns |  42.97 |    0.62 |         - |          NA |
