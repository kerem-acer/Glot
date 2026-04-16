```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean            | Error          | StdDev        | Ratio   | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |----------------:|---------------:|--------------:|--------:|--------:|----------:|------------:|
| **string.Equals**                        | **8**     | **Ascii**  |       **0.5193 ns** |      **0.2630 ns** |     **0.0144 ns** |    **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Ascii  |       0.0492 ns |      0.0412 ns |     0.0023 ns |    0.09 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Ascii  |       1.8568 ns |      0.3200 ns |     0.0175 ns |    3.58 |    0.09 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Ascii  |      16.9672 ns |      2.3446 ns |     0.1285 ns |   32.69 |    0.80 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Ascii  |      16.7603 ns |      1.3151 ns |     0.0721 ns |   32.29 |    0.77 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Ascii  |       0.5452 ns |      0.2623 ns |     0.0144 ns |    1.05 |    0.03 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Ascii  |       0.0427 ns |      0.0774 ns |     0.0042 ns |    0.08 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Ascii  |       1.8672 ns |      0.1590 ns |     0.0087 ns |    3.60 |    0.09 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Ascii  |      16.6178 ns |      1.4072 ns |     0.0771 ns |   32.01 |    0.77 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Ascii  |      16.2179 ns |      3.4486 ns |     0.1890 ns |   31.24 |    0.80 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **8**     | **Latin**  |       **0.5410 ns** |      **0.6771 ns** |     **0.0371 ns** |    **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Latin  |       0.0469 ns |      0.0526 ns |     0.0029 ns |    0.09 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Latin  |       1.8687 ns |      0.6395 ns |     0.0351 ns |    3.46 |    0.21 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Latin  |      25.9285 ns |      3.4955 ns |     0.1916 ns |   48.07 |    2.77 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Latin  |      22.5049 ns |      1.5663 ns |     0.0859 ns |   41.72 |    2.39 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Latin  |       0.5477 ns |      0.7295 ns |     0.0400 ns |    1.02 |    0.09 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Latin  |       0.0545 ns |      0.1638 ns |     0.0090 ns |    0.10 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Latin  |       1.8573 ns |      0.1634 ns |     0.0090 ns |    3.44 |    0.20 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Latin  |      25.6384 ns |      0.7659 ns |     0.0420 ns |   47.53 |    2.72 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Latin  |      21.7850 ns |      1.7510 ns |     0.0960 ns |   40.39 |    2.31 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **8**     | **Cjk**    |       **0.5656 ns** |      **0.7828 ns** |     **0.0429 ns** |   **1.004** |    **0.09** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Cjk    |       0.1977 ns |      0.0496 ns |     0.0027 ns |   0.351 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Cjk    |       2.0730 ns |      0.7437 ns |     0.0408 ns |   3.679 |    0.24 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Cjk    |      64.0128 ns |      4.0472 ns |     0.2218 ns | 113.599 |    7.26 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Cjk    |      46.8024 ns |      6.9624 ns |     0.3816 ns |  83.057 |    5.34 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Cjk    |       0.5290 ns |      0.0369 ns |     0.0020 ns |   0.939 |    0.06 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Cjk    |       1.7446 ns |      0.8264 ns |     0.0453 ns |   3.096 |    0.21 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Cjk    |      62.5517 ns |     10.8921 ns |     0.5970 ns | 111.006 |    7.15 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Cjk    |      48.5429 ns |      3.6245 ns |     0.1987 ns |  86.146 |    5.51 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **8**     | **Emoji**  |       **0.5396 ns** |      **0.3897 ns** |     **0.0214 ns** |   **1.001** |    **0.05** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Emoji  |       0.0522 ns |      0.2657 ns |     0.0146 ns |   0.097 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Emoji  |       2.0397 ns |      0.6103 ns |     0.0335 ns |   3.784 |    0.14 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Emoji  |      37.2407 ns |      0.8697 ns |     0.0477 ns |  69.095 |    2.42 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Emoji  |      27.1450 ns |      1.5767 ns |     0.0864 ns |  50.364 |    1.77 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Emoji  |       0.5365 ns |      0.2197 ns |     0.0120 ns |   0.995 |    0.04 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Emoji  |       1.6252 ns |      0.3655 ns |     0.0200 ns |   3.015 |    0.11 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Emoji  |      34.7285 ns |      1.9135 ns |     0.1049 ns |  64.434 |    2.27 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Emoji  |      26.8850 ns |      5.1978 ns |     0.2849 ns |  49.881 |    1.81 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **8**     | **Mixed**  |       **0.5360 ns** |      **0.2182 ns** |     **0.0120 ns** |    **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Mixed  |       0.0505 ns |      0.0481 ns |     0.0026 ns |    0.09 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Mixed  |       1.8640 ns |      0.2716 ns |     0.0149 ns |    3.48 |    0.07 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Mixed  |      16.3861 ns |      2.4159 ns |     0.1324 ns |   30.58 |    0.63 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Mixed  |      17.7203 ns |      0.4082 ns |     0.0224 ns |   33.07 |    0.64 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Mixed  |       0.5609 ns |      0.9643 ns |     0.0529 ns |    1.05 |    0.09 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Mixed  |       0.0464 ns |      0.0881 ns |     0.0048 ns |    0.09 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Mixed  |       1.8887 ns |      0.1909 ns |     0.0105 ns |    3.52 |    0.07 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Mixed  |      15.7859 ns |      2.0435 ns |     0.1120 ns |   29.46 |    0.60 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Mixed  |      16.3965 ns |      1.1642 ns |     0.0638 ns |   30.60 |    0.60 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **64**    | **Ascii**  |       **2.3088 ns** |      **0.7805 ns** |     **0.0428 ns** |    **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Ascii  |       0.8253 ns |      0.9208 ns |     0.0505 ns |    0.36 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Ascii  |       2.4395 ns |      0.1223 ns |     0.0067 ns |    1.06 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Ascii  |      99.7803 ns |      9.0415 ns |     0.4956 ns |   43.23 |    0.72 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Ascii  |     100.8832 ns |     15.8905 ns |     0.8710 ns |   43.71 |    0.77 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Ascii  |       2.2822 ns |      0.0890 ns |     0.0049 ns |    0.99 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Ascii  |       0.8286 ns |      0.8541 ns |     0.0468 ns |    0.36 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Ascii  |       2.4216 ns |      0.1679 ns |     0.0092 ns |    1.05 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Ascii  |     102.3120 ns |     28.7130 ns |     1.5739 ns |   44.32 |    0.92 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Ascii  |     106.5449 ns |      3.6789 ns |     0.2017 ns |   46.16 |    0.74 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **64**    | **Latin**  |       **2.2826 ns** |      **0.2427 ns** |     **0.0133 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Latin  |       1.1233 ns |      0.0708 ns |     0.0039 ns |    0.49 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Latin  |       2.7275 ns |      0.2775 ns |     0.0152 ns |    1.19 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Latin  |     157.8419 ns |     21.6733 ns |     1.1880 ns |   69.15 |    0.57 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Latin  |     136.8319 ns |      6.4224 ns |     0.3520 ns |   59.95 |    0.33 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Latin  |       2.3052 ns |      0.8639 ns |     0.0474 ns |    1.01 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Latin  |       1.1511 ns |      0.6968 ns |     0.0382 ns |    0.50 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Latin  |       2.7254 ns |      0.5347 ns |     0.0293 ns |    1.19 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Latin  |     155.1305 ns |      7.2783 ns |     0.3990 ns |   67.97 |    0.38 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Latin  |     134.3818 ns |      2.1759 ns |     0.1193 ns |   58.87 |    0.30 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **64**    | **Cjk**    |       **2.3359 ns** |      **1.4315 ns** |     **0.0785 ns** |   **1.001** |    **0.04** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Cjk    |       4.6018 ns |      0.2586 ns |     0.0142 ns |   1.972 |    0.06 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Cjk    |       5.1613 ns |      3.4690 ns |     0.1901 ns |   2.211 |    0.09 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Cjk    |     461.0513 ns |     42.2654 ns |     2.3167 ns | 197.526 |    5.71 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Cjk    |     340.5250 ns |     60.6133 ns |     3.3224 ns | 145.890 |    4.35 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Cjk    |       2.3377 ns |      0.8020 ns |     0.0440 ns |   1.002 |    0.03 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Cjk    |       1.6371 ns |      0.4799 ns |     0.0263 ns |   0.701 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Cjk    |     459.3537 ns |     16.6585 ns |     0.9131 ns | 196.799 |    5.64 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Cjk    |     333.1615 ns |     10.6784 ns |     0.5853 ns | 142.735 |    4.09 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **64**    | **Emoji**  |       **2.3575 ns** |      **0.9374 ns** |     **0.0514 ns** |   **1.000** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Emoji  |       1.9106 ns |      0.5160 ns |     0.0283 ns |   0.811 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Emoji  |       3.5284 ns |      0.0206 ns |     0.0011 ns |   1.497 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Emoji  |     252.0355 ns |     23.7537 ns |     1.3020 ns | 106.940 |    2.09 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Emoji  |     181.7145 ns |     17.8603 ns |     0.9790 ns |  77.103 |    1.51 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Emoji  |       2.3073 ns |      0.5857 ns |     0.0321 ns |   0.979 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Emoji  |       1.6579 ns |      0.2331 ns |     0.0128 ns |   0.703 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Emoji  |     251.1550 ns |     15.2362 ns |     0.8351 ns | 106.567 |    2.05 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Emoji  |     181.4621 ns |      3.1024 ns |     0.1701 ns |  76.995 |    1.46 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **64**    | **Mixed**  |       **2.3103 ns** |      **1.1895 ns** |     **0.0652 ns** |    **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Mixed  |       1.3070 ns |      0.3556 ns |     0.0195 ns |    0.57 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Mixed  |       2.9996 ns |      0.3876 ns |     0.0212 ns |    1.30 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Mixed  |     161.2164 ns |      8.6025 ns |     0.4715 ns |   69.82 |    1.69 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Mixed  |     139.9365 ns |     26.6869 ns |     1.4628 ns |   60.60 |    1.56 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Mixed  |       2.2882 ns |      0.3259 ns |     0.0179 ns |    0.99 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Mixed  |       1.3588 ns |      1.5735 ns |     0.0862 ns |    0.59 |    0.04 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Mixed  |       2.9977 ns |      0.1539 ns |     0.0084 ns |    1.30 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Mixed  |     173.7763 ns |     54.6763 ns |     2.9970 ns |   75.26 |    2.13 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Mixed  |     135.4002 ns |     39.6521 ns |     2.1735 ns |   58.64 |    1.63 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **256**   | **Ascii**  |       **8.7985 ns** |      **0.2149 ns** |     **0.0118 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Ascii  |       4.7909 ns |      0.3361 ns |     0.0184 ns |    0.54 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Ascii  |       6.8934 ns |      1.3162 ns |     0.0721 ns |    0.78 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Ascii  |     374.3375 ns |     50.8454 ns |     2.7870 ns |   42.55 |    0.28 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Ascii  |     403.6977 ns |      3.9054 ns |     0.2141 ns |   45.88 |    0.06 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Ascii  |       8.7831 ns |      1.8016 ns |     0.0988 ns |    1.00 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Ascii  |       4.4856 ns |      2.6335 ns |     0.1444 ns |    0.51 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Ascii  |       5.7941 ns |      0.3066 ns |     0.0168 ns |    0.66 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Ascii  |     378.0747 ns |     36.0647 ns |     1.9768 ns |   42.97 |    0.20 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Ascii  |     399.9849 ns |     38.3034 ns |     2.0995 ns |   45.46 |    0.21 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **256**   | **Latin**  |       **8.7642 ns** |      **0.2524 ns** |     **0.0138 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Latin  |       5.1912 ns |      1.3333 ns |     0.0731 ns |    0.59 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Latin  |       6.7042 ns |      0.6771 ns |     0.0371 ns |    0.76 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Latin  |     614.9044 ns |     71.2944 ns |     3.9079 ns |   70.16 |    0.40 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Latin  |     520.5767 ns |     31.5712 ns |     1.7305 ns |   59.40 |    0.19 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Latin  |       8.8077 ns |      1.3202 ns |     0.0724 ns |    1.00 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Latin  |       5.3212 ns |      1.3966 ns |     0.0766 ns |    0.61 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Latin  |       6.6807 ns |      0.2511 ns |     0.0138 ns |    0.76 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Latin  |     610.9348 ns |     42.9999 ns |     2.3570 ns |   69.71 |    0.25 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Latin  |     521.5314 ns |     32.0536 ns |     1.7570 ns |   59.51 |    0.19 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **256**   | **Cjk**    |       **8.8593 ns** |      **1.4267 ns** |     **0.0782 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Cjk    |      13.3697 ns |      0.4147 ns |     0.0227 ns |   1.509 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Cjk    |      15.0920 ns |      2.0575 ns |     0.1128 ns |   1.704 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Cjk    |   1,857.7561 ns |    224.5028 ns |    12.3058 ns | 209.707 |    2.00 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Cjk    |   1,311.6261 ns |    118.3099 ns |     6.4850 ns | 148.058 |    1.30 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Cjk    |       8.8510 ns |      0.6850 ns |     0.0375 ns |   0.999 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Cjk    |       1.6095 ns |      0.2311 ns |     0.0127 ns |   0.182 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Cjk    |   1,863.2698 ns |    164.5422 ns |     9.0191 ns | 210.329 |    1.83 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Cjk    |   1,314.6624 ns |    192.9391 ns |    10.5756 ns | 148.401 |    1.53 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **256**   | **Emoji**  |       **8.9235 ns** |      **1.7543 ns** |     **0.0962 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Emoji  |       8.3073 ns |      0.2772 ns |     0.0152 ns |   0.931 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Emoji  |       9.9026 ns |      1.7441 ns |     0.0956 ns |   1.110 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Emoji  |     971.9934 ns |     76.7196 ns |     4.2053 ns | 108.933 |    1.10 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Emoji  |     698.6631 ns |     41.5279 ns |     2.2763 ns |  78.300 |    0.77 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Emoji  |       8.8292 ns |      0.4436 ns |     0.0243 ns |   0.990 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Emoji  |       1.7041 ns |      0.7834 ns |     0.0429 ns |   0.191 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Emoji  |     969.0803 ns |     32.1068 ns |     1.7599 ns | 108.607 |    1.03 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Emoji  |     698.6655 ns |     60.1953 ns |     3.2995 ns |  78.301 |    0.80 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **256**   | **Mixed**  |       **9.0940 ns** |      **1.4442 ns** |     **0.0792 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Mixed  |       5.4018 ns |      0.3693 ns |     0.0202 ns |   0.594 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Mixed  |       7.0481 ns |      0.1172 ns |     0.0064 ns |   0.775 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Mixed  |     700.5825 ns |     86.5411 ns |     4.7436 ns |  77.041 |    0.73 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Mixed  |     522.5799 ns |    128.4131 ns |     7.0388 ns |  57.467 |    0.80 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Mixed  |       8.7552 ns |      1.1127 ns |     0.0610 ns |   0.963 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Mixed  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Mixed  |       1.7089 ns |      0.9081 ns |     0.0498 ns |   0.188 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Mixed  |     632.7217 ns |     42.1057 ns |     2.3080 ns |  69.579 |    0.57 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Mixed  |     545.4706 ns |     65.0386 ns |     3.5650 ns |  59.984 |    0.56 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **4096**  | **Ascii**  |     **152.4180 ns** |      **5.7860 ns** |     **0.3171 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Ascii  |      79.7197 ns |      7.3980 ns |     0.4055 ns |    0.52 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Ascii  |      82.4330 ns |     13.6632 ns |     0.7489 ns |    0.54 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Ascii  |   5,860.9114 ns |    560.3052 ns |    30.7122 ns |   38.45 |    0.19 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Ascii  |   5,855.3141 ns |    576.4582 ns |    31.5976 ns |   38.42 |    0.19 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Ascii  |     148.9907 ns |      4.8666 ns |     0.2668 ns |    0.98 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Ascii  |      80.0530 ns |      5.0724 ns |     0.2780 ns |    0.53 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Ascii  |      82.7054 ns |     35.9172 ns |     1.9687 ns |    0.54 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Ascii  |   5,819.3880 ns |    124.0999 ns |     6.8023 ns |   38.18 |    0.08 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Ascii  |   5,851.5982 ns |    136.3714 ns |     7.4750 ns |   38.39 |    0.08 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **4096**  | **Latin**  |     **166.6800 ns** |     **49.5193 ns** |     **2.7143 ns** |    **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Latin  |      93.5781 ns |     10.2680 ns |     0.5628 ns |    0.56 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Latin  |      95.9958 ns |      6.5380 ns |     0.3584 ns |    0.58 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Latin  |   9,879.2693 ns |  1,150.4121 ns |    63.0580 ns |   59.28 |    0.89 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Latin  |   8,229.5439 ns |  2,527.0830 ns |   138.5179 ns |   49.38 |    1.00 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Latin  |     148.3098 ns |      2.0772 ns |     0.1139 ns |    0.89 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Latin  |      94.4565 ns |     21.5049 ns |     1.1788 ns |    0.57 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Latin  |      96.1214 ns |      2.3857 ns |     0.1308 ns |    0.58 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Latin  |  10,304.5434 ns |    856.6992 ns |    46.9586 ns |   61.83 |    0.90 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Latin  |   8,517.8621 ns |  2,458.7638 ns |   134.7731 ns |   51.11 |    1.00 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **4096**  | **Cjk**    |     **152.3273 ns** |     **10.0648 ns** |     **0.5517 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Cjk    |     221.0295 ns |     14.3821 ns |     0.7883 ns |   1.451 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Cjk    |     220.1288 ns |     18.3093 ns |     1.0036 ns |   1.445 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Cjk    |  29,200.6984 ns |  4,247.0798 ns |   232.7968 ns | 191.699 |    1.45 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Cjk    |  20,689.0535 ns |  2,895.6494 ns |   158.7203 ns | 135.821 |    1.00 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Cjk    |     150.0475 ns |      4.7346 ns |     0.2595 ns |   0.985 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Cjk    |       1.6742 ns |      0.8074 ns |     0.0443 ns |   0.011 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Cjk    |  28,978.9882 ns |  2,280.6934 ns |   125.0125 ns | 190.243 |    0.93 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Cjk    |  20,622.1436 ns |    597.2290 ns |    32.7361 ns | 135.382 |    0.46 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **4096**  | **Emoji**  |     **151.7604 ns** |     **29.8286 ns** |     **1.6350 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Emoji  |     148.6425 ns |      5.5088 ns |     0.3020 ns |   0.980 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Emoji  |     150.1236 ns |      7.7246 ns |     0.4234 ns |   0.989 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Emoji  |  15,459.2501 ns |    885.7553 ns |    48.5512 ns | 101.874 |    0.99 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Emoji  |  11,130.7570 ns |    796.9586 ns |    43.6840 ns |  73.350 |    0.72 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Emoji  |     148.8431 ns |     10.8824 ns |     0.5965 ns |   0.981 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Emoji  |       1.7157 ns |      0.3828 ns |     0.0210 ns |   0.011 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Emoji  |  15,330.2405 ns |    399.9758 ns |    21.9240 ns | 101.024 |    0.95 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Emoji  |  10,844.8919 ns |    676.4678 ns |    37.0795 ns |  71.466 |    0.70 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **4096**  | **Mixed**  |     **165.3600 ns** |     **25.0261 ns** |     **1.3718 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Mixed  |     101.3828 ns |     20.8084 ns |     1.1406 ns |    0.61 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Mixed  |     104.1043 ns |      5.3780 ns |     0.2948 ns |    0.63 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Mixed  |  10,806.5565 ns |  1,209.6034 ns |    66.3024 ns |   65.35 |    0.58 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Mixed  |   8,286.7898 ns |  2,532.7341 ns |   138.8277 ns |   50.12 |    0.81 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Mixed  |     150.0799 ns |     18.1200 ns |     0.9932 ns |    0.91 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Mixed  |     102.0190 ns |      8.8486 ns |     0.4850 ns |    0.62 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Mixed  |     103.6450 ns |     22.7649 ns |     1.2478 ns |    0.63 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Mixed  |  10,147.8265 ns |    738.2665 ns |    40.4669 ns |   61.37 |    0.49 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Mixed  |   8,286.5819 ns |  1,564.6862 ns |    85.7657 ns |   50.11 |    0.58 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Ascii**  |   **2,364.5210 ns** |     **65.1903 ns** |     **3.5733 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Ascii  |   1,037.2389 ns |    230.9673 ns |    12.6601 ns |    0.44 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Ascii  |   1,003.8422 ns |     81.8940 ns |     4.4889 ns |    0.42 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Ascii  |  93,856.2877 ns | 10,612.3516 ns |   581.6988 ns |   39.69 |    0.22 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Ascii  |  93,619.8459 ns |  6,534.4747 ns |   358.1766 ns |   39.59 |    0.14 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Ascii  |   2,379.8939 ns |     82.1974 ns |     4.5055 ns |    1.01 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Ascii  |   1,094.4598 ns |    192.7921 ns |    10.5676 ns |    0.46 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Ascii  |   1,052.7977 ns |    439.4300 ns |    24.0866 ns |    0.45 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Ascii  |  93,540.7969 ns |  5,952.0167 ns |   326.2501 ns |   39.56 |    0.13 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Ascii  |  93,645.1093 ns |  3,183.9933 ns |   174.5254 ns |   39.60 |    0.08 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Latin**  |   **2,393.5900 ns** |    **301.6917 ns** |    **16.5367 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Latin  |   1,324.8289 ns |    163.3651 ns |     8.9546 ns |    0.55 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Latin  |   1,472.7567 ns |     88.7924 ns |     4.8670 ns |    0.62 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Latin  | 157,830.7766 ns | 16,129.3110 ns |   884.1020 ns |   65.94 |    0.51 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Latin  | 130,405.7769 ns |  9,145.1088 ns |   501.2743 ns |   54.48 |    0.37 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Latin  |   2,627.6218 ns |     94.2010 ns |     5.1635 ns |    1.10 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Latin  |   1,471.2432 ns |     51.3589 ns |     2.8152 ns |    0.61 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Latin  |   1,382.2904 ns |    146.6995 ns |     8.0411 ns |    0.58 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Latin  | 156,587.8532 ns | 33,244.9154 ns | 1,822.2660 ns |   65.42 |    0.77 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Latin  | 132,363.8271 ns | 12,218.3380 ns |   669.7283 ns |   55.30 |    0.41 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Cjk**    |   **2,401.6176 ns** |    **125.7830 ns** |     **6.8946 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Cjk    |   3,442.2201 ns |    208.0240 ns |    11.4025 ns |   1.433 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Cjk    |   3,473.5703 ns |    240.9227 ns |    13.2058 ns |   1.446 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Cjk    | 465,070.2446 ns | 12,104.5428 ns |   663.4908 ns | 193.650 |    0.54 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Cjk    | 324,739.7056 ns |  2,614.2170 ns |   143.2941 ns | 135.218 |    0.34 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Cjk    |   2,384.0070 ns |    178.7334 ns |     9.7970 ns |   0.993 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Cjk    |       1.8356 ns |      0.1700 ns |     0.0093 ns |   0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Cjk    | 463,269.6942 ns | 23,300.3189 ns | 1,277.1691 ns | 192.900 |    0.67 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Cjk    | 329,255.4935 ns | 60,501.6549 ns | 3,316.2999 ns | 137.098 |    1.24 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Emoji**  |   **2,398.2734 ns** |    **228.4083 ns** |    **12.5198 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Emoji  |   2,311.5223 ns |    171.7854 ns |     9.4161 ns |   0.964 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Emoji  |   2,311.1041 ns |     92.1466 ns |     5.0509 ns |   0.964 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Emoji  | 249,618.8223 ns | 46,815.7030 ns | 2,566.1267 ns | 104.085 |    1.04 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Emoji  | 173,423.0313 ns |  9,899.1825 ns |   542.6076 ns |  72.313 |    0.38 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Emoji  |   2,377.8231 ns |    567.1964 ns |    31.0899 ns |   0.991 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Emoji  |       1.8212 ns |      0.4301 ns |     0.0236 ns |   0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Emoji  | 247,427.0363 ns |  1,525.0566 ns |    83.5935 ns | 103.171 |    0.47 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Emoji  | 174,366.5296 ns | 11,194.2062 ns |   613.5922 ns |  72.706 |    0.40 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Mixed**  |   **2,372.2578 ns** |    **618.0855 ns** |    **33.8794 ns** |    **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Mixed  |   1,568.5846 ns |     21.6042 ns |     1.1842 ns |    0.66 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Mixed  |   1,584.5552 ns |     93.3391 ns |     5.1162 ns |    0.67 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Mixed  | 164,360.7551 ns | 13,230.8034 ns |   725.2250 ns |   69.29 |    0.89 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Mixed  | 136,328.6709 ns | 11,449.2968 ns |   627.5746 ns |   57.48 |    0.74 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Mixed  |   2,384.7592 ns |     56.9228 ns |     3.1201 ns |    1.01 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Mixed  |   1,612.6066 ns |    170.7211 ns |     9.3578 ns |    0.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Mixed  |   1,618.2114 ns |    190.4853 ns |    10.4411 ns |    0.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Mixed  | 164,466.4509 ns | 13,772.6176 ns |   754.9236 ns |   69.34 |    0.89 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Mixed  | 140,693.7035 ns | 58,310.1596 ns | 3,196.1766 ns |   59.32 |    1.38 |         - |          NA |
