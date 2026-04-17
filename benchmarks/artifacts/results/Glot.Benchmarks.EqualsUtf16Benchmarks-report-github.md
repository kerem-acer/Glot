```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean            | Error             | StdDev         | Median          | Ratio  | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |----------------:|------------------:|---------------:|----------------:|-------:|--------:|----------:|------------:|
| **string.Equals**                        | **8**     | **Ascii**  |       **0.7900 ns** |         **7.9763 ns** |      **0.4372 ns** |       **0.5532 ns** |  **1.184** |    **0.74** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Ascii  |       0.0006 ns |         0.0189 ns |      0.0010 ns |       0.0000 ns |  0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Ascii  |       1.8349 ns |         5.8510 ns |      0.3207 ns |       1.6627 ns |  2.749 |    1.09 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Ascii  |       7.5021 ns |         1.1063 ns |      0.0606 ns |       7.4848 ns | 11.240 |    4.10 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Ascii  |      23.7721 ns |         3.9231 ns |      0.2150 ns |      23.8679 ns | 35.616 |   12.99 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Ascii  |       0.5574 ns |         0.0656 ns |      0.0036 ns |       0.5590 ns |  0.835 |    0.30 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Ascii  |       0.0000 ns |         0.0000 ns |      0.0000 ns |       0.0000 ns |  0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Ascii  |       1.7001 ns |         0.2703 ns |      0.0148 ns |       1.7078 ns |  2.547 |    0.93 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Ascii  |       9.5058 ns |        63.8250 ns |      3.4985 ns |       7.4929 ns | 14.242 |    7.07 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Ascii  |      25.0533 ns |        29.5879 ns |      1.6218 ns |      24.7860 ns | 37.536 |   13.87 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **8**     | **Cjk**    |       **0.5345 ns** |         **0.2691 ns** |      **0.0148 ns** |       **0.5369 ns** |  **1.001** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Cjk    |       0.2057 ns |         0.1281 ns |      0.0070 ns |       0.2081 ns |  0.385 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Cjk    |       2.2121 ns |        16.3768 ns |      0.8977 ns |       1.7526 ns |  4.141 |    1.46 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Cjk    |      12.7028 ns |        14.0515 ns |      0.7702 ns |      12.8487 ns | 23.777 |    1.37 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Cjk    |      28.4318 ns |         3.0360 ns |      0.1664 ns |      28.4021 ns | 53.217 |    1.31 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Cjk    |       0.5290 ns |         0.7865 ns |      0.0431 ns |       0.5107 ns |  0.990 |    0.07 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Cjk    |       0.0000 ns |         0.0000 ns |      0.0000 ns |       0.0000 ns |  0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Cjk    |       1.3857 ns |         0.4610 ns |      0.0253 ns |       1.3733 ns |  2.594 |    0.07 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Cjk    |      10.6216 ns |         0.1533 ns |      0.0084 ns |      10.6256 ns | 19.881 |    0.48 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Cjk    |      28.4249 ns |        28.8421 ns |      1.5809 ns |      28.0301 ns | 53.205 |    2.87 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **8**     | **Mixed**  |       **0.5264 ns** |         **0.1754 ns** |      **0.0096 ns** |       **0.5295 ns** |   **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Mixed  |       0.0111 ns |         0.1094 ns |      0.0060 ns |       0.0118 ns |   0.02 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Mixed  |       1.6373 ns |         0.0550 ns |      0.0030 ns |       1.6382 ns |   3.11 |    0.05 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Mixed  |       7.3519 ns |         0.2243 ns |      0.0123 ns |       7.3507 ns |  13.97 |    0.22 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Mixed  |      23.2991 ns |         4.7567 ns |      0.2607 ns |      23.1850 ns |  44.27 |    0.83 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Mixed  |       0.6035 ns |         1.2027 ns |      0.0659 ns |       0.5903 ns |   1.15 |    0.11 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Mixed  |       0.2726 ns |         8.3802 ns |      0.4593 ns |       0.0077 ns |   0.52 |    0.76 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Mixed  |       2.1128 ns |        14.0692 ns |      0.7712 ns |       1.6760 ns |   4.01 |    1.27 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Mixed  |       8.8920 ns |        24.3585 ns |      1.3352 ns |       9.4769 ns |  16.89 |    2.21 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Mixed  |      23.1248 ns |         2.8191 ns |      0.1545 ns |      23.1123 ns |  43.94 |    0.75 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **256**   | **Ascii**  |       **8.9451 ns** |         **0.4455 ns** |      **0.0244 ns** |       **8.9449 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Ascii  |       4.9652 ns |         8.1513 ns |      0.4468 ns |       4.7645 ns |   0.56 |    0.04 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Ascii  |       5.8952 ns |         0.6754 ns |      0.0370 ns |       5.9107 ns |   0.66 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Ascii  |      26.3763 ns |       147.0799 ns |      8.0619 ns |      21.9141 ns |   2.95 |    0.78 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Ascii  |     593.2978 ns |        23.1607 ns |      1.2695 ns |     592.8249 ns |  66.33 |    0.20 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Ascii  |      10.7350 ns |        58.0799 ns |      3.1836 ns |       8.9139 ns |   1.20 |    0.31 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Ascii  |       4.4996 ns |         1.1442 ns |      0.0627 ns |       4.5351 ns |   0.50 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Ascii  |       5.4665 ns |         3.8772 ns |      0.2125 ns |       5.5424 ns |   0.61 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Ascii  |      21.3386 ns |         2.0634 ns |      0.1131 ns |      21.3316 ns |   2.39 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Ascii  |     662.1076 ns |     1,892.8974 ns |    103.7561 ns |     602.3128 ns |  74.02 |   10.05 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **256**   | **Cjk**    |       **8.9794 ns** |         **1.0374 ns** |      **0.0569 ns** |       **9.0120 ns** |  **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Cjk    |      15.0820 ns |        24.0966 ns |      1.3208 ns |      15.0764 ns |  1.680 |    0.13 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Cjk    |      19.6224 ns |       141.0671 ns |      7.7324 ns |      15.2449 ns |  2.185 |    0.75 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Cjk    |     168.2941 ns |        49.9012 ns |      2.7353 ns |     169.7489 ns | 18.743 |    0.28 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Cjk    |     821.8174 ns |       936.9858 ns |     51.3594 ns |     848.3133 ns | 91.525 |    4.98 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Cjk    |      10.0631 ns |        21.1282 ns |      1.1581 ns |       9.7164 ns |  1.121 |    0.11 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Cjk    |       0.0000 ns |         0.0000 ns |      0.0000 ns |       0.0000 ns |  0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Cjk    |       1.3416 ns |         0.1107 ns |      0.0061 ns |       1.3414 ns |  0.149 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Cjk    |     154.0701 ns |        14.2213 ns |      0.7795 ns |     153.8030 ns | 17.159 |    0.12 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Cjk    |     773.8302 ns |         6.3139 ns |      0.3461 ns |     773.8548 ns | 86.181 |    0.48 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **256**   | **Mixed**  |       **9.2291 ns** |         **0.3935 ns** |      **0.0216 ns** |       **9.2182 ns** |  **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Mixed  |       5.8512 ns |         5.2202 ns |      0.2861 ns |       5.9689 ns |  0.634 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Mixed  |       6.8029 ns |         1.2165 ns |      0.0667 ns |       6.8247 ns |  0.737 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Mixed  |      97.5661 ns |       129.8691 ns |      7.1186 ns |      94.7522 ns | 10.572 |    0.67 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Mixed  |     621.3419 ns |        70.3761 ns |      3.8576 ns |     619.2410 ns | 67.324 |    0.39 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Mixed  |       8.8805 ns |         0.2325 ns |      0.0127 ns |       8.8862 ns |  0.962 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Mixed  |       0.0000 ns |         0.0000 ns |      0.0000 ns |       0.0000 ns |  0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Mixed  |       1.4597 ns |         2.1771 ns |      0.1193 ns |       1.3931 ns |  0.158 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Mixed  |       1.3904 ns |         2.4967 ns |      0.1369 ns |       1.3154 ns |  0.151 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Mixed  |       1.4587 ns |         4.1849 ns |      0.2294 ns |       1.3287 ns |  0.158 |    0.02 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **65536** | **Ascii**  |   **2,418.5428 ns** |       **112.1784 ns** |      **6.1489 ns** |   **2,415.4048 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Ascii  |   1,062.5717 ns |       171.1340 ns |      9.3804 ns |   1,061.0445 ns |   0.44 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Ascii  |   1,035.0153 ns |        81.0592 ns |      4.4431 ns |   1,034.8833 ns |   0.43 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Ascii  |   2,865.2718 ns |       515.1177 ns |     28.2353 ns |   2,849.5514 ns |   1.18 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Ascii  | 149,378.4111 ns |    25,517.2935 ns |  1,398.6890 ns | 148,799.9368 ns |  61.76 |    0.52 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Ascii  |   3,089.0965 ns |    16,829.6054 ns |    922.4875 ns |   2,562.1265 ns |   1.28 |    0.33 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Ascii  |   1,095.5023 ns |       277.4606 ns |     15.2086 ns |   1,101.6003 ns |   0.45 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Ascii  |   1,066.4559 ns |       283.3392 ns |     15.5308 ns |   1,067.9799 ns |   0.44 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Ascii  |   2,856.8168 ns |       672.8186 ns |     36.8795 ns |   2,848.1267 ns |   1.18 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Ascii  | 149,231.2758 ns |    15,094.4502 ns |    827.3778 ns | 149,496.3176 ns |  61.70 |    0.33 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **65536** | **Cjk**    |   **2,428.5815 ns** |       **129.0181 ns** |      **7.0719 ns** |   **2,425.8532 ns** |  **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Cjk    |   3,769.4743 ns |     9,684.8713 ns |    530.8605 ns |   3,470.7080 ns |  1.552 |    0.19 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Cjk    |   3,477.7332 ns |       148.8429 ns |      8.1586 ns |   3,481.3968 ns |  1.432 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Cjk    |  38,651.5851 ns |    16,909.4944 ns |    926.8664 ns |  38,261.8892 ns | 15.915 |    0.33 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Cjk    | 184,552.5649 ns |     7,623.1105 ns |    417.8484 ns | 184,567.4949 ns | 75.992 |    0.24 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Cjk    |   2,468.4479 ns |     1,456.2555 ns |     79.8223 ns |   2,443.8354 ns |  1.016 |    0.03 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Cjk    |       0.0000 ns |         0.0000 ns |      0.0000 ns |       0.0000 ns |  0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Cjk    |       1.3936 ns |         0.1750 ns |      0.0096 ns |       1.3904 ns |  0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Cjk    |  36,325.5378 ns |    28,574.3869 ns |  1,566.2586 ns |  35,443.3594 ns | 14.958 |    0.56 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Cjk    | 214,068.8542 ns | 1,038,483.6333 ns | 56,922.7924 ns | 182,874.2776 ns | 88.146 |   20.30 |         - |          NA |
|                                      |       |        |                 |                   |                |                 |        |         |           |             |
| **string.Equals**                        | **65536** | **Mixed**  |   **2,392.9860 ns** |       **166.0724 ns** |      **9.1030 ns** |   **2,397.6045 ns** |   **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Mixed  |   1,601.3924 ns |       237.7282 ns |     13.0307 ns |   1,600.0125 ns |   0.67 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Mixed  |   1,606.2156 ns |        22.3749 ns |      1.2264 ns |   1,606.6332 ns |   0.67 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Mixed  |  22,347.6609 ns |    63,774.6418 ns |  3,495.7033 ns |  20,493.4959 ns |   9.34 |    1.27 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Mixed  | 157,383.3347 ns |    26,271.1318 ns |  1,440.0094 ns | 156,843.5974 ns |  65.77 |    0.56 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Mixed  |   2,418.5446 ns |       132.1874 ns |      7.2456 ns |   2,421.2309 ns |   1.01 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Mixed  |   1,863.9268 ns |     7,297.6649 ns |    400.0096 ns |   1,644.7676 ns |   0.78 |    0.14 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Mixed  |   1,743.1823 ns |     2,877.2330 ns |    157.7108 ns |   1,671.8186 ns |   0.73 |    0.06 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Mixed  |  24,854.4604 ns |   121,272.0731 ns |  6,647.3316 ns |  21,026.2159 ns |  10.39 |    2.41 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Mixed  | 154,403.0083 ns |    29,647.1663 ns |  1,625.0612 ns | 154,776.9978 ns |  64.52 |    0.63 |         - |          NA |
