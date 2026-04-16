```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean            | Error           | StdDev        | Median          | Ratio   | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |----------------:|----------------:|--------------:|----------------:|--------:|--------:|----------:|------------:|
| **string.Equals**                        | **8**     | **Ascii**  |       **0.5142 ns** |       **0.0379 ns** |     **0.0021 ns** |       **0.5136 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Ascii  |       0.0284 ns |       0.0600 ns |     0.0033 ns |       0.0281 ns |    0.06 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Ascii  |       1.8658 ns |       0.3714 ns |     0.0204 ns |       1.8588 ns |    3.63 |    0.04 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Ascii  |      16.5661 ns |       2.2982 ns |     0.1260 ns |      16.5110 ns |   32.22 |    0.24 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Ascii  |      16.5941 ns |       1.0408 ns |     0.0570 ns |      16.5974 ns |   32.27 |    0.15 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Ascii  |       0.5474 ns |       0.1877 ns |     0.0103 ns |       0.5434 ns |    1.06 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Ascii  |       0.0445 ns |       0.0223 ns |     0.0012 ns |       0.0445 ns |    0.09 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Ascii  |       1.8906 ns |       0.0645 ns |     0.0035 ns |       1.8903 ns |    3.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Ascii  |      16.4657 ns |       1.2476 ns |     0.0684 ns |      16.4644 ns |   32.02 |    0.16 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Ascii  |      16.1928 ns |       0.9634 ns |     0.0528 ns |      16.1857 ns |   31.49 |    0.14 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **8**     | **Latin**  |       **0.5341 ns** |       **0.0712 ns** |     **0.0039 ns** |       **0.5339 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Latin  |       0.0495 ns |       0.2299 ns |     0.0126 ns |       0.0429 ns |    0.09 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Latin  |       1.8588 ns |       0.0538 ns |     0.0030 ns |       1.8575 ns |    3.48 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Latin  |      26.0521 ns |       3.5106 ns |     0.1924 ns |      26.0356 ns |   48.78 |    0.44 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Latin  |      23.3329 ns |       1.6346 ns |     0.0896 ns |      23.2946 ns |   43.69 |    0.31 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Latin  |       0.5303 ns |       0.2229 ns |     0.0122 ns |       0.5252 ns |    0.99 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Latin  |       0.0547 ns |       0.1465 ns |     0.0080 ns |       0.0543 ns |    0.10 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Latin  |       1.8640 ns |       0.0837 ns |     0.0046 ns |       1.8634 ns |    3.49 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Latin  |      25.6851 ns |       4.3068 ns |     0.2361 ns |      25.5588 ns |   48.10 |    0.49 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Latin  |      22.0524 ns |       4.8073 ns |     0.2635 ns |      22.1550 ns |   41.29 |    0.50 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **8**     | **Cjk**    |       **0.5487 ns** |       **0.2495 ns** |     **0.0137 ns** |       **0.5559 ns** |   **1.000** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Cjk    |       0.2356 ns |       0.6207 ns |     0.0340 ns |       0.2229 ns |   0.429 |    0.05 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Cjk    |       2.1314 ns |       0.4517 ns |     0.0248 ns |       2.1443 ns |   3.886 |    0.09 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Cjk    |      64.4973 ns |       9.2359 ns |     0.5063 ns |      64.7873 ns | 117.595 |    2.70 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Cjk    |      48.5177 ns |       5.2902 ns |     0.2900 ns |      48.6241 ns |  88.460 |    1.99 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Cjk    |       0.5559 ns |       0.6102 ns |     0.0334 ns |       0.5380 ns |   1.014 |    0.06 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Cjk    |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Cjk    |       1.7816 ns |       0.8179 ns |     0.0448 ns |       1.7839 ns |   3.248 |    0.10 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Cjk    |      61.1566 ns |       4.6262 ns |     0.2536 ns |      61.0591 ns | 111.504 |    2.47 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Cjk    |      48.6023 ns |       9.3401 ns |     0.5120 ns |      48.7383 ns |  88.614 |    2.10 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **8**     | **Emoji**  |       **0.5548 ns** |       **0.6402 ns** |     **0.0351 ns** |       **0.5371 ns** |   **1.003** |    **0.08** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Emoji  |       0.0877 ns |       0.9350 ns |     0.0513 ns |       0.0668 ns |   0.159 |    0.08 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Emoji  |       2.0521 ns |       0.5020 ns |     0.0275 ns |       2.0666 ns |   3.708 |    0.20 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Emoji  |      37.2249 ns |       4.0282 ns |     0.2208 ns |      37.1581 ns |  67.272 |    3.58 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Emoji  |      27.3833 ns |       0.7748 ns |     0.0425 ns |      27.4006 ns |  49.487 |    2.62 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Emoji  |       0.5394 ns |       0.1270 ns |     0.0070 ns |       0.5388 ns |   0.975 |    0.05 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Emoji  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Emoji  |       1.6491 ns |       0.3193 ns |     0.0175 ns |       1.6411 ns |   2.980 |    0.16 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Emoji  |      35.1410 ns |       1.7242 ns |     0.0945 ns |      35.0905 ns |  63.506 |    3.36 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Emoji  |      26.6099 ns |       3.5460 ns |     0.1944 ns |      26.6861 ns |  48.089 |    2.56 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **8**     | **Mixed**  |       **0.5466 ns** |       **0.1026 ns** |     **0.0056 ns** |       **0.5491 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 8     | Mixed  |       0.0466 ns |       0.1097 ns |     0.0060 ns |       0.0467 ns |    0.09 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 8     | Mixed  |       1.8409 ns |       0.0502 ns |     0.0028 ns |       1.8420 ns |    3.37 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 8     | Mixed  |      17.3892 ns |       2.4221 ns |     0.1328 ns |      17.4151 ns |   31.81 |    0.35 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 8     | Mixed  |      17.7314 ns |       0.7685 ns |     0.0421 ns |      17.7231 ns |   32.44 |    0.30 |         - |          NA |
| &#39;string.Equals different&#39;            | 8     | Mixed  |       0.5591 ns |       0.5974 ns |     0.0327 ns |       0.5495 ns |    1.02 |    0.05 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 8     | Mixed  |       0.0498 ns |       0.1237 ns |     0.0068 ns |       0.0486 ns |    0.09 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 8     | Mixed  |       1.8830 ns |       0.4185 ns |     0.0229 ns |       1.8748 ns |    3.44 |    0.05 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 8     | Mixed  |      16.2099 ns |       0.7110 ns |     0.0390 ns |      16.2040 ns |   29.66 |    0.27 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 8     | Mixed  |      16.8465 ns |       1.1850 ns |     0.0650 ns |      16.8433 ns |   30.82 |    0.29 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **64**    | **Ascii**  |       **2.3049 ns** |       **0.2232 ns** |     **0.0122 ns** |       **2.3108 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Ascii  |       0.8024 ns |       0.1360 ns |     0.0075 ns |       0.8060 ns |    0.35 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Ascii  |       2.4642 ns |       0.4698 ns |     0.0257 ns |       2.4537 ns |    1.07 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Ascii  |      99.9491 ns |       4.8421 ns |     0.2654 ns |      99.8700 ns |   43.37 |    0.22 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Ascii  |     106.4199 ns |       6.4160 ns |     0.3517 ns |     106.4270 ns |   46.17 |    0.25 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Ascii  |       2.3692 ns |       0.8707 ns |     0.0477 ns |       2.3655 ns |    1.03 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Ascii  |       0.8247 ns |       0.3320 ns |     0.0182 ns |       0.8202 ns |    0.36 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Ascii  |       2.4259 ns |       0.1994 ns |     0.0109 ns |       2.4199 ns |    1.05 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Ascii  |     101.0226 ns |      13.0640 ns |     0.7161 ns |     101.1083 ns |   43.83 |    0.34 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Ascii  |     102.8185 ns |      12.8677 ns |     0.7053 ns |     102.5165 ns |   44.61 |    0.34 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **64**    | **Latin**  |       **2.3492 ns** |       **0.3564 ns** |     **0.0195 ns** |       **2.3595 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Latin  |       1.2072 ns |       1.4175 ns |     0.0777 ns |       1.2036 ns |    0.51 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Latin  |       2.7644 ns |       1.1016 ns |     0.0604 ns |       2.7464 ns |    1.18 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Latin  |     157.2875 ns |      37.7393 ns |     2.0686 ns |     156.7709 ns |   66.96 |    0.90 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Latin  |     135.4745 ns |      10.7636 ns |     0.5900 ns |     135.3674 ns |   57.67 |    0.47 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Latin  |       2.3340 ns |       0.8409 ns |     0.0461 ns |       2.3560 ns |    0.99 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Latin  |       1.1506 ns |       0.7514 ns |     0.0412 ns |       1.1376 ns |    0.49 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Latin  |       2.7066 ns |       0.3235 ns |     0.0177 ns |       2.7131 ns |    1.15 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Latin  |     157.0177 ns |      23.6834 ns |     1.2982 ns |     157.0610 ns |   66.84 |    0.68 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Latin  |     135.8460 ns |       1.4352 ns |     0.0787 ns |     135.8702 ns |   57.83 |    0.42 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **64**    | **Cjk**    |       **2.3571 ns** |       **0.8241 ns** |     **0.0452 ns** |       **2.3694 ns** |   **1.000** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Cjk    |       5.1404 ns |       1.6007 ns |     0.0877 ns |       5.1892 ns |   2.181 |    0.05 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Cjk    |       5.1535 ns |       2.2830 ns |     0.1251 ns |       5.0980 ns |   2.187 |    0.06 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Cjk    |     458.1918 ns |      50.2068 ns |     2.7520 ns |     458.5577 ns | 194.440 |    3.40 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Cjk    |     331.7989 ns |      19.4871 ns |     1.0682 ns |     331.7689 ns | 140.803 |    2.39 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Cjk    |       2.3280 ns |       0.2375 ns |     0.0130 ns |       2.3345 ns |   0.988 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Cjk    |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Cjk    |       1.6458 ns |       0.4224 ns |     0.0232 ns |       1.6565 ns |   0.698 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Cjk    |     457.5158 ns |      20.6989 ns |     1.1346 ns |     457.5709 ns | 194.153 |    3.27 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Cjk    |     334.7833 ns |      38.4565 ns |     2.1079 ns |     334.9673 ns | 142.070 |    2.50 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **64**    | **Emoji**  |       **2.3865 ns** |       **0.8065 ns** |     **0.0442 ns** |       **2.4045 ns** |   **1.000** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Emoji  |       2.0219 ns |       0.3032 ns |     0.0166 ns |       2.0158 ns |   0.847 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Emoji  |       3.5745 ns |       0.8190 ns |     0.0449 ns |       3.5491 ns |   1.498 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Emoji  |     249.3634 ns |      14.5791 ns |     0.7991 ns |     248.9043 ns | 104.513 |    1.72 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Emoji  |     180.5211 ns |      22.6005 ns |     1.2388 ns |     179.9966 ns |  75.660 |    1.31 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Emoji  |       2.3441 ns |       0.2553 ns |     0.0140 ns |       2.3521 ns |   0.982 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Emoji  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Emoji  |       1.6862 ns |       0.3398 ns |     0.0186 ns |       1.6918 ns |   0.707 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Emoji  |     250.4723 ns |      15.4701 ns |     0.8480 ns |     250.1391 ns | 104.978 |    1.73 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Emoji  |     182.2487 ns |      15.9383 ns |     0.8736 ns |     182.1612 ns |  76.384 |    1.28 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **64**    | **Mixed**  |       **2.3381 ns** |       **0.4338 ns** |     **0.0238 ns** |       **2.3284 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 64    | Mixed  |       1.3159 ns |       0.8137 ns |     0.0446 ns |       1.2924 ns |    0.56 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 64    | Mixed  |       3.0237 ns |       0.2241 ns |     0.0123 ns |       3.0241 ns |    1.29 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 64    | Mixed  |     162.1009 ns |      19.9762 ns |     1.0950 ns |     162.3042 ns |   69.33 |    0.73 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 64    | Mixed  |     150.3195 ns |      24.0008 ns |     1.3156 ns |     149.5628 ns |   64.29 |    0.74 |         - |          NA |
| &#39;string.Equals different&#39;            | 64    | Mixed  |       2.3317 ns |       0.7428 ns |     0.0407 ns |       2.3287 ns |    1.00 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 64    | Mixed  |       1.3215 ns |       0.1317 ns |     0.0072 ns |       1.3189 ns |    0.57 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 64    | Mixed  |       2.9710 ns |       0.4407 ns |     0.0242 ns |       2.9640 ns |    1.27 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 64    | Mixed  |     155.1449 ns |      14.5818 ns |     0.7993 ns |     154.7548 ns |   66.36 |    0.65 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 64    | Mixed  |     150.7800 ns |      18.6215 ns |     1.0207 ns |     150.3397 ns |   64.49 |    0.68 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **256**   | **Ascii**  |       **8.7941 ns** |       **2.3100 ns** |     **0.1266 ns** |       **8.7312 ns** |    **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Ascii  |       4.3668 ns |       0.3938 ns |     0.0216 ns |       4.3571 ns |    0.50 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Ascii  |       6.8945 ns |       2.2061 ns |     0.1209 ns |       6.9467 ns |    0.78 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Ascii  |     377.3905 ns |      52.1503 ns |     2.8585 ns |     378.7344 ns |   42.92 |    0.60 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Ascii  |     407.7883 ns |     108.5888 ns |     5.9521 ns |     410.4712 ns |   46.38 |    0.82 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Ascii  |       8.8734 ns |       0.3565 ns |     0.0195 ns |       8.8700 ns |    1.01 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Ascii  |       4.3197 ns |       1.2427 ns |     0.0681 ns |       4.2948 ns |    0.49 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Ascii  |       5.8621 ns |       0.6580 ns |     0.0361 ns |       5.8589 ns |    0.67 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Ascii  |     374.2612 ns |       8.6595 ns |     0.4747 ns |     374.0272 ns |   42.56 |    0.53 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Ascii  |     401.2553 ns |      42.3258 ns |     2.3200 ns |     401.5903 ns |   45.63 |    0.61 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **256**   | **Latin**  |       **8.7567 ns** |       **1.1349 ns** |     **0.0622 ns** |       **8.7350 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Latin  |       5.2751 ns |       1.1736 ns |     0.0643 ns |       5.3067 ns |    0.60 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Latin  |       6.7150 ns |       0.4725 ns |     0.0259 ns |       6.7057 ns |    0.77 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Latin  |     610.0388 ns |      28.5031 ns |     1.5624 ns |     610.1220 ns |   69.67 |    0.45 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Latin  |     518.1415 ns |      20.6016 ns |     1.1292 ns |     518.5184 ns |   59.17 |    0.38 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Latin  |       8.7555 ns |       0.8654 ns |     0.0474 ns |       8.7491 ns |    1.00 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Latin  |       5.1945 ns |       0.3318 ns |     0.0182 ns |       5.1933 ns |    0.59 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Latin  |       6.6966 ns |       0.4974 ns |     0.0273 ns |       6.6961 ns |    0.76 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Latin  |     608.1611 ns |      26.4389 ns |     1.4492 ns |     607.3636 ns |   69.45 |    0.45 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Latin  |     521.2184 ns |       7.6809 ns |     0.4210 ns |     521.1688 ns |   59.52 |    0.37 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **256**   | **Cjk**    |       **8.8552 ns** |       **2.0913 ns** |     **0.1146 ns** |       **8.8270 ns** |   **1.000** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Cjk    |      13.4259 ns |       0.3093 ns |     0.0170 ns |      13.4279 ns |   1.516 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Cjk    |      15.0661 ns |       2.7557 ns |     0.1511 ns |      15.0509 ns |   1.702 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Cjk    |   1,855.6541 ns |     357.8786 ns |    19.6165 ns |   1,855.4885 ns | 209.580 |    3.03 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Cjk    |   1,329.2056 ns |      83.1995 ns |     4.5604 ns |   1,330.9317 ns | 150.122 |    1.73 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Cjk    |       8.8772 ns |       1.8417 ns |     0.1010 ns |       8.8626 ns |   1.003 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Cjk    |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Cjk    |       1.6059 ns |       0.4198 ns |     0.0230 ns |       1.6168 ns |   0.181 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Cjk    |   1,859.0798 ns |     360.4890 ns |    19.7596 ns |   1,850.9252 ns | 209.967 |    3.04 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Cjk    |   1,315.1157 ns |     125.7909 ns |     6.8950 ns |   1,314.3049 ns | 148.531 |    1.79 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **256**   | **Emoji**  |       **8.7637 ns** |       **0.5566 ns** |     **0.0305 ns** |       **8.7701 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Emoji  |       8.3695 ns |       0.8535 ns |     0.0468 ns |       8.3740 ns |   0.955 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Emoji  |       9.8616 ns |       1.0217 ns |     0.0560 ns |       9.8309 ns |   1.125 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Emoji  |     983.1269 ns |     185.8680 ns |    10.1881 ns |     978.6207 ns | 112.183 |    1.06 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Emoji  |     695.1685 ns |     122.0527 ns |     6.6901 ns |     691.9673 ns |  79.324 |    0.70 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Emoji  |       8.8174 ns |       0.7176 ns |     0.0393 ns |       8.8053 ns |   1.006 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Emoji  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Emoji  |       1.6897 ns |       0.4573 ns |     0.0251 ns |       1.6767 ns |   0.193 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Emoji  |     978.2284 ns |      95.1379 ns |     5.2148 ns |     975.7428 ns | 111.624 |    0.62 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Emoji  |     700.8628 ns |      57.4618 ns |     3.1497 ns |     701.5483 ns |  79.974 |    0.39 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **256**   | **Mixed**  |       **9.1236 ns** |       **1.3960 ns** |     **0.0765 ns** |       **9.1660 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 256   | Mixed  |       5.4953 ns |       0.4667 ns |     0.0256 ns |       5.4814 ns |   0.602 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 256   | Mixed  |       6.9945 ns |       0.9874 ns |     0.0541 ns |       6.9814 ns |   0.767 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 256   | Mixed  |     613.0746 ns |      83.9458 ns |     4.6014 ns |     612.5998 ns |  67.200 |    0.66 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 256   | Mixed  |     587.9348 ns |      41.8830 ns |     2.2958 ns |     588.1565 ns |  64.444 |    0.52 |         - |          NA |
| &#39;string.Equals different&#39;            | 256   | Mixed  |       8.8638 ns |       3.8480 ns |     0.2109 ns |       8.7953 ns |   0.972 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 256   | Mixed  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 256   | Mixed  |       1.6914 ns |       0.1434 ns |     0.0079 ns |       1.6906 ns |   0.185 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 256   | Mixed  |     688.7308 ns |     131.8958 ns |     7.2297 ns |     685.5572 ns |  75.493 |    0.88 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 256   | Mixed  |     521.3804 ns |     123.1535 ns |     6.7505 ns |     522.8813 ns |  57.149 |    0.76 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **4096**  | **Ascii**  |     **151.4182 ns** |       **7.7670 ns** |     **0.4257 ns** |     **151.4482 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Ascii  |      80.3057 ns |      21.7747 ns |     1.1935 ns |      79.9921 ns |    0.53 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Ascii  |      81.7818 ns |       1.1773 ns |     0.0645 ns |      81.8101 ns |    0.54 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Ascii  |   5,878.0972 ns |     557.2312 ns |    30.5437 ns |   5,889.7948 ns |   38.82 |    0.20 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Ascii  |   5,881.0744 ns |   1,085.4141 ns |    59.4952 ns |   5,860.2988 ns |   38.84 |    0.35 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Ascii  |     148.8671 ns |       8.5347 ns |     0.4678 ns |     148.6157 ns |    0.98 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Ascii  |      81.3304 ns |      23.8019 ns |     1.3047 ns |      81.3561 ns |    0.54 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Ascii  |      82.6068 ns |      11.4874 ns |     0.6297 ns |      82.2631 ns |    0.55 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Ascii  |   5,838.3474 ns |     331.6632 ns |    18.1796 ns |   5,830.7342 ns |   38.56 |    0.14 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Ascii  |   5,829.8028 ns |     422.0920 ns |    23.1363 ns |   5,822.1623 ns |   38.50 |    0.16 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **4096**  | **Latin**  |     **166.1209 ns** |      **35.4211 ns** |     **1.9415 ns** |     **165.5744 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Latin  |      94.6728 ns |      10.9836 ns |     0.6020 ns |      95.0051 ns |    0.57 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Latin  |      96.6034 ns |       2.8721 ns |     0.1574 ns |      96.6280 ns |    0.58 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Latin  |   9,674.6832 ns |   1,381.7467 ns |    75.7382 ns |   9,638.8537 ns |   58.24 |    0.71 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Latin  |   8,267.3138 ns |     876.8479 ns |    48.0630 ns |   8,250.8901 ns |   49.77 |    0.56 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Latin  |     149.6139 ns |      17.7491 ns |     0.9729 ns |     149.1834 ns |    0.90 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Latin  |      93.9937 ns |      14.7491 ns |     0.8084 ns |      93.6504 ns |    0.57 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Latin  |      95.5288 ns |       5.6332 ns |     0.3088 ns |      95.6629 ns |    0.58 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Latin  |   9,578.5230 ns |     844.8334 ns |    46.3082 ns |   9,600.3755 ns |   57.67 |    0.63 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Latin  |   8,447.0213 ns |   1,250.6062 ns |    68.5499 ns |   8,426.9924 ns |   50.85 |    0.62 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **4096**  | **Cjk**    |     **151.3229 ns** |       **4.9228 ns** |     **0.2698 ns** |     **151.2405 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Cjk    |     221.9342 ns |      20.6717 ns |     1.1331 ns |     221.3666 ns |   1.467 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Cjk    |     221.5393 ns |      21.7391 ns |     1.1916 ns |     220.9066 ns |   1.464 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Cjk    |  28,894.1375 ns |   2,526.7706 ns |   138.5008 ns |  28,832.6645 ns | 190.944 |    0.85 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Cjk    |  21,004.0008 ns |   3,088.7073 ns |   169.3025 ns |  21,055.5051 ns | 138.803 |    0.99 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Cjk    |     149.0374 ns |      11.4729 ns |     0.6289 ns |     148.8323 ns |   0.985 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Cjk    |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Cjk    |       1.7036 ns |       0.2429 ns |     0.0133 ns |       1.6962 ns |   0.011 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Cjk    |  28,845.3064 ns |     939.2506 ns |    51.4835 ns |  28,851.5549 ns | 190.621 |    0.42 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Cjk    |  20,909.7506 ns |   6,135.6317 ns |   336.3147 ns |  20,850.3380 ns | 138.180 |    1.94 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **4096**  | **Emoji**  |     **153.5810 ns** |      **76.5930 ns** |     **4.1983 ns** |     **151.4780 ns** |   **1.000** |    **0.03** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Emoji  |     148.9697 ns |      26.6109 ns |     1.4586 ns |     148.2620 ns |   0.970 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Emoji  |     150.4954 ns |      11.5227 ns |     0.6316 ns |     150.5824 ns |   0.980 |    0.02 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Emoji  |  15,512.2745 ns |     487.9788 ns |    26.7478 ns |  15,512.4372 ns | 101.053 |    2.36 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Emoji  |  10,945.2201 ns |     924.3924 ns |    50.6691 ns |  10,947.2726 ns |  71.302 |    1.69 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Emoji  |     150.6188 ns |      12.6751 ns |     0.6948 ns |     150.8682 ns |   0.981 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Emoji  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Emoji  |       1.7264 ns |       0.6435 ns |     0.0353 ns |       1.7261 ns |   0.011 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Emoji  |  15,348.8049 ns |     641.3127 ns |    35.1525 ns |  15,351.0297 ns |  99.989 |    2.34 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Emoji  |  11,013.9194 ns |   1,875.4543 ns |   102.8000 ns |  10,990.0691 ns |  71.749 |    1.77 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **4096**  | **Mixed**  |     **165.8090 ns** |      **36.0173 ns** |     **1.9742 ns** |     **164.9254 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 4096  | Mixed  |     101.2099 ns |       4.9471 ns |     0.2712 ns |     101.1215 ns |    0.61 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 4096  | Mixed  |     103.9617 ns |      24.1569 ns |     1.3241 ns |     103.4459 ns |    0.63 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 4096  | Mixed  |  11,035.7708 ns |   2,312.9445 ns |   126.7803 ns |  11,078.8174 ns |   66.56 |    0.95 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 4096  | Mixed  |   9,531.4473 ns |   1,845.8236 ns |   101.1758 ns |   9,554.3168 ns |   57.49 |    0.79 |         - |          NA |
| &#39;string.Equals different&#39;            | 4096  | Mixed  |     148.8416 ns |       9.4231 ns |     0.5165 ns |     149.1306 ns |    0.90 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 4096  | Mixed  |     100.9860 ns |       8.0341 ns |     0.4404 ns |     100.9401 ns |    0.61 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 4096  | Mixed  |     102.4963 ns |       8.4053 ns |     0.4607 ns |     102.6674 ns |    0.62 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 4096  | Mixed  |  11,021.1896 ns |     293.7696 ns |    16.1025 ns |  11,023.0090 ns |   66.48 |    0.69 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 4096  | Mixed  |   9,408.1277 ns |   2,195.8212 ns |   120.3604 ns |   9,399.0924 ns |   56.75 |    0.86 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **65536** | **Ascii**  |   **2,387.3281 ns** |      **57.5674 ns** |     **3.1555 ns** |   **2,386.7698 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Ascii  |   1,003.4746 ns |     110.5058 ns |     6.0572 ns |   1,003.6824 ns |    0.42 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Ascii  |   1,000.6178 ns |      41.1330 ns |     2.2546 ns |   1,000.7643 ns |    0.42 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Ascii  |  94,137.8123 ns |  12,758.6578 ns |   699.3451 ns |  94,498.6165 ns |   39.43 |    0.26 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Ascii  |  94,328.7218 ns |   4,916.5110 ns |   269.4906 ns |  94,250.3256 ns |   39.51 |    0.11 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Ascii  |   2,531.1205 ns |      93.6194 ns |     5.1316 ns |   2,534.0331 ns |    1.06 |    0.00 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Ascii  |   1,038.2056 ns |     139.7318 ns |     7.6592 ns |   1,042.3288 ns |    0.43 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Ascii  |   1,042.7056 ns |     264.5025 ns |    14.4983 ns |   1,045.2554 ns |    0.44 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Ascii  |  93,821.7892 ns |  17,876.1187 ns |   979.8504 ns |  93,338.2213 ns |   39.30 |    0.36 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Ascii  |  94,474.9942 ns |  24,445.3620 ns | 1,339.9328 ns |  93,974.1974 ns |   39.57 |    0.49 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **65536** | **Latin**  |   **2,384.4609 ns** |      **46.9329 ns** |     **2.5725 ns** |   **2,383.8143 ns** |    **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Latin  |   1,370.1517 ns |     107.3279 ns |     5.8830 ns |   1,367.3421 ns |    0.57 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Latin  |   1,365.9869 ns |     128.5351 ns |     7.0454 ns |   1,367.3580 ns |    0.57 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Latin  | 157,529.3375 ns |  13,473.9384 ns |   738.5520 ns | 157,356.7605 ns |   66.07 |    0.28 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Latin  | 126,597.2391 ns |   6,231.9052 ns |   341.5918 ns | 126,691.1111 ns |   53.09 |    0.13 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Latin  |   2,540.7488 ns |     322.0146 ns |    17.6507 ns |   2,531.3438 ns |    1.07 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Latin  |   1,470.5555 ns |      51.5885 ns |     2.8277 ns |   1,469.5041 ns |    0.62 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Latin  |   1,470.7724 ns |     134.0975 ns |     7.3503 ns |   1,473.6948 ns |    0.62 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Latin  | 158,063.5243 ns |  27,793.6705 ns | 1,523.4649 ns | 158,658.1016 ns |   66.29 |    0.56 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Latin  | 130,808.4445 ns |  10,637.7295 ns |   583.0899 ns | 130,838.2163 ns |   54.86 |    0.22 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **65536** | **Cjk**    |   **2,418.9439 ns** |     **320.5848 ns** |    **17.5723 ns** |   **2,413.9252 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Cjk    |   3,443.0766 ns |      30.6298 ns |     1.6789 ns |   3,443.4152 ns |   1.423 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Cjk    |   3,458.3287 ns |     111.2456 ns |     6.0977 ns |   3,457.7799 ns |   1.430 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Cjk    | 463,847.9816 ns |  21,452.4472 ns | 1,175.8810 ns | 463,995.2393 ns | 191.763 |    1.27 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Cjk    | 328,364.5833 ns | 112,249.3409 ns | 6,152.7652 ns | 325,499.9390 ns | 135.752 |    2.36 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Cjk    |   2,379.2650 ns |      67.7673 ns |     3.7146 ns |   2,379.7404 ns |   0.984 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Cjk    |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Cjk    |       1.8089 ns |       0.4047 ns |     0.0222 ns |       1.8182 ns |   0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Cjk    | 465,959.5812 ns |  48,314.2211 ns | 2,648.2655 ns | 466,517.1509 ns | 192.636 |    1.54 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Cjk    | 330,196.3840 ns |  27,888.8396 ns | 1,528.6814 ns | 330,031.0669 ns | 136.509 |    1.02 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **65536** | **Emoji**  |   **2,407.6554 ns** |     **580.7441 ns** |    **31.8325 ns** |   **2,408.1628 ns** |   **1.000** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Emoji  |   2,301.0339 ns |      61.2526 ns |     3.3575 ns |   2,300.6689 ns |   0.956 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Emoji  |   2,322.8182 ns |     375.8389 ns |    20.6010 ns |   2,311.6566 ns |   0.965 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Emoji  | 246,940.8094 ns |  15,629.9732 ns |   856.7316 ns | 247,395.3657 ns | 102.577 |    1.21 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Emoji  | 177,777.0828 ns |  19,956.0325 ns | 1,093.8575 ns | 177,611.8064 ns |  73.847 |    0.93 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Emoji  |   2,384.2838 ns |     653.4463 ns |    35.8176 ns |   2,367.2357 ns |   0.990 |    0.02 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Emoji  |       0.0000 ns |       0.0000 ns |     0.0000 ns |       0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Emoji  |       1.7975 ns |       0.4236 ns |     0.0232 ns |       1.7935 ns |   0.001 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Emoji  | 248,229.2243 ns |  35,301.3974 ns | 1,934.9887 ns | 247,258.8398 ns | 103.112 |    1.37 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Emoji  | 177,839.9455 ns |   7,114.1680 ns |   389.9516 ns | 177,967.6208 ns |  73.873 |    0.86 |         - |          NA |
|                                      |       |        |                 |                 |               |                 |         |         |           |             |
| **string.Equals**                        | **65536** | **Mixed**  |   **2,364.0353 ns** |     **399.7261 ns** |    **21.9103 ns** |   **2,352.2614 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Mixed  |   1,572.0230 ns |     114.9977 ns |     6.3034 ns |   1,569.3630 ns |    0.67 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Mixed  |   1,582.2204 ns |      59.5874 ns |     3.2662 ns |   1,583.7896 ns |    0.67 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Mixed  | 159,880.8290 ns |  14,787.1992 ns |   810.5363 ns | 159,705.2307 ns |   67.63 |    0.62 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Mixed  | 136,442.3151 ns |   3,051.3633 ns |   167.2555 ns | 136,432.0171 ns |   57.72 |    0.46 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Mixed  |   2,393.5460 ns |       9.8592 ns |     0.5404 ns |   2,393.4415 ns |    1.01 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Mixed  |   1,609.0279 ns |     174.4207 ns |     9.5606 ns |   1,609.5498 ns |    0.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Mixed  |   1,611.3391 ns |      58.0295 ns |     3.1808 ns |   1,611.8886 ns |    0.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Mixed  | 176,015.2353 ns |   8,460.7069 ns |   463.7599 ns | 176,275.6655 ns |   74.46 |    0.62 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Mixed  | 139,727.7865 ns |   7,748.5527 ns |   424.7243 ns | 139,943.6543 ns |   59.11 |    0.50 |         - |          NA |
