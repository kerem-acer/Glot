```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                      | N     | Locale | Mean         | Error      | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|------------:|
| **string.Contains**             | **64**    | **Ascii**  |     **4.046 ns** |  **0.0673 ns** |  **0.0629 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Ascii  |     3.467 ns |  0.0272 ns |  0.0241 ns |     0.86 |    0.01 |         - |          NA |
| U8String.Contains           | 64    | Ascii  |     3.628 ns |  0.0358 ns |  0.0335 ns |     0.90 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Ascii  |     4.482 ns |  0.0442 ns |  0.0413 ns |     1.11 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Ascii  |    16.830 ns |  0.0810 ns |  0.0676 ns |     4.16 |    0.07 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Ascii  |    17.941 ns |  0.1156 ns |  0.0966 ns |     4.44 |    0.07 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Ascii  |     5.933 ns |  0.0478 ns |  0.0399 ns |     1.47 |    0.02 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Ascii  |     4.408 ns |  0.0551 ns |  0.0489 ns |     1.09 |    0.02 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 64    | Ascii  |     4.417 ns |  0.0515 ns |  0.0457 ns |     1.09 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Ascii  |     4.907 ns |  0.0322 ns |  0.0301 ns |     1.21 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Ascii  |    17.794 ns |  0.0935 ns |  0.0829 ns |     4.40 |    0.07 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Ascii  |    19.155 ns |  0.0678 ns |  0.0529 ns |     4.74 |    0.07 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **64**    | **Cjk**    |     **2.573 ns** |  **0.0163 ns** |  **0.0136 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Cjk    |     3.019 ns |  0.0071 ns |  0.0059 ns |     1.17 |    0.01 |         - |          NA |
| U8String.Contains           | 64    | Cjk    |     3.212 ns |  0.0286 ns |  0.0268 ns |     1.25 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Cjk    |     4.339 ns |  0.0309 ns |  0.0289 ns |     1.69 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Cjk    |    36.437 ns |  0.1114 ns |  0.0930 ns |    14.16 |    0.08 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Cjk    |    33.174 ns |  0.3846 ns |  0.3598 ns |    12.90 |    0.15 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Cjk    |     5.894 ns |  0.0263 ns |  0.0246 ns |     2.29 |    0.01 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Cjk    |     9.668 ns |  0.3992 ns |  0.3539 ns |     3.76 |    0.13 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 64    | Cjk    |     9.757 ns |  0.2392 ns |  0.2238 ns |     3.79 |    0.09 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Cjk    |    10.126 ns |  0.1837 ns |  0.1628 ns |     3.94 |    0.06 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Cjk    |    33.396 ns |  0.3708 ns |  0.3468 ns |    12.98 |    0.15 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Cjk    |    39.351 ns |  0.4320 ns |  0.4041 ns |    15.30 |    0.17 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **64**    | **Emoji**  |     **3.165 ns** |  **0.0230 ns** |  **0.0215 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Emoji  |     3.047 ns |  0.0159 ns |  0.0148 ns |     0.96 |    0.01 |         - |          NA |
| U8String.Contains           | 64    | Emoji  |     3.215 ns |  0.0071 ns |  0.0066 ns |     1.02 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Emoji  |     4.410 ns |  0.0301 ns |  0.0281 ns |     1.39 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Emoji  |    28.574 ns |  0.3209 ns |  0.3002 ns |     9.03 |    0.11 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Emoji  |    33.412 ns |  0.3160 ns |  0.2801 ns |    10.56 |    0.11 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Emoji  |     5.883 ns |  0.0989 ns |  0.0925 ns |     1.86 |    0.03 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Emoji  |     8.631 ns |  0.2463 ns |  0.2304 ns |     2.73 |    0.07 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 64    | Emoji  |     8.599 ns |  0.2306 ns |  0.2157 ns |     2.72 |    0.07 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Emoji  |     8.907 ns |  0.2963 ns |  0.2627 ns |     2.81 |    0.08 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Emoji  |    34.426 ns |  0.5450 ns |  0.5098 ns |    10.88 |    0.17 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Emoji  |    39.627 ns |  0.6478 ns |  0.6060 ns |    12.52 |    0.20 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **64**    | **Mixed**  |     **3.613 ns** |  **0.0293 ns** |  **0.0274 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Mixed  |     3.379 ns |  0.0145 ns |  0.0135 ns |     0.94 |    0.01 |         - |          NA |
| U8String.Contains           | 64    | Mixed  |     3.616 ns |  0.0229 ns |  0.0214 ns |     1.00 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Mixed  |     4.646 ns |  0.0575 ns |  0.0538 ns |     1.29 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Mixed  |    17.373 ns |  0.1719 ns |  0.1608 ns |     4.81 |    0.06 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Mixed  |    18.406 ns |  0.2176 ns |  0.2036 ns |     5.09 |    0.07 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Mixed  |     5.934 ns |  0.0541 ns |  0.0506 ns |     1.64 |    0.02 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Mixed  |     5.070 ns |  0.1361 ns |  0.1136 ns |     1.40 |    0.03 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 64    | Mixed  |     5.384 ns |  0.2634 ns |  0.2464 ns |     1.49 |    0.07 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Mixed  |     5.488 ns |  0.0582 ns |  0.0544 ns |     1.52 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Mixed  |    29.677 ns |  0.4606 ns |  0.4309 ns |     8.21 |    0.13 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Mixed  |    34.904 ns |  0.4671 ns |  0.4370 ns |     9.66 |    0.14 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **4096**  | **Ascii**  |     **4.076 ns** |  **0.0502 ns** |  **0.0469 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Ascii  |     3.479 ns |  0.0460 ns |  0.0408 ns |     0.85 |    0.01 |         - |          NA |
| U8String.Contains           | 4096  | Ascii  |     3.620 ns |  0.0326 ns |  0.0305 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Ascii  |     4.510 ns |  0.0437 ns |  0.0409 ns |     1.11 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Ascii  |    16.901 ns |  0.2021 ns |  0.1891 ns |     4.15 |    0.06 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Ascii  |    18.068 ns |  0.1610 ns |  0.1506 ns |     4.43 |    0.06 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Ascii  |   275.157 ns |  3.0028 ns |  2.6619 ns |    67.52 |    0.99 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Ascii  |   141.513 ns |  0.7567 ns |  0.7078 ns |    34.72 |    0.43 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 4096  | Ascii  |   141.229 ns |  0.7878 ns |  0.6984 ns |    34.65 |    0.42 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Ascii  |   143.497 ns |  1.0650 ns |  0.9962 ns |    35.21 |    0.46 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Ascii  |   156.907 ns |  1.6343 ns |  1.5288 ns |    38.50 |    0.57 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Ascii  |   157.628 ns |  1.3398 ns |  1.2533 ns |    38.68 |    0.53 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **4096**  | **Cjk**    |     **2.569 ns** |  **0.0102 ns** |  **0.0095 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Cjk    |     3.021 ns |  0.0218 ns |  0.0203 ns |     1.18 |    0.01 |         - |          NA |
| U8String.Contains           | 4096  | Cjk    |     3.228 ns |  0.0284 ns |  0.0265 ns |     1.26 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Cjk    |     4.342 ns |  0.0350 ns |  0.0327 ns |     1.69 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Cjk    |    36.587 ns |  0.3034 ns |  0.2838 ns |    14.24 |    0.12 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Cjk    |    33.388 ns |  0.5260 ns |  0.4920 ns |    12.99 |    0.19 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Cjk    |   279.417 ns |  2.0083 ns |  1.7803 ns |   108.75 |    0.77 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   408.485 ns |  3.2064 ns |  2.9993 ns |   158.99 |    1.27 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 4096  | Cjk    |   401.580 ns |  1.7920 ns |  1.4964 ns |   156.30 |    0.79 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   404.705 ns |  3.0028 ns |  2.8088 ns |   157.51 |    1.20 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Cjk    |   429.919 ns |  4.1654 ns |  3.8963 ns |   167.33 |    1.59 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Cjk    |   434.586 ns |  3.1378 ns |  2.9351 ns |   169.14 |    1.26 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **4096**  | **Emoji**  |     **3.196 ns** |  **0.0411 ns** |  **0.0364 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Emoji  |     3.098 ns |  0.0263 ns |  0.0246 ns |     0.97 |    0.01 |         - |          NA |
| U8String.Contains           | 4096  | Emoji  |     3.326 ns |  0.0485 ns |  0.0453 ns |     1.04 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Emoji  |     4.426 ns |  0.0436 ns |  0.0408 ns |     1.38 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Emoji  |    29.174 ns |  0.2964 ns |  0.2773 ns |     9.13 |    0.13 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Emoji  |    33.807 ns |  0.1495 ns |  0.1399 ns |    10.58 |    0.12 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Emoji  |   279.376 ns |  2.3584 ns |  2.2061 ns |    87.43 |    1.17 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Emoji  |   430.478 ns |  5.2323 ns |  4.8943 ns |   134.71 |    2.09 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 4096  | Emoji  |   425.672 ns |  3.2257 ns |  3.0173 ns |   133.21 |    1.72 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Emoji  |   429.411 ns |  3.7944 ns |  3.5493 ns |   134.38 |    1.82 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Emoji  |   456.052 ns |  3.9610 ns |  3.3076 ns |   142.72 |    1.85 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Emoji  |   464.063 ns |  7.8273 ns |  6.9387 ns |   145.22 |    2.63 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **4096**  | **Mixed**  |     **3.623 ns** |  **0.0375 ns** |  **0.0351 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Mixed  |     3.352 ns |  0.0322 ns |  0.0301 ns |     0.93 |    0.01 |         - |          NA |
| U8String.Contains           | 4096  | Mixed  |     3.524 ns |  0.0207 ns |  0.0194 ns |     0.97 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Mixed  |     4.584 ns |  0.0478 ns |  0.0447 ns |     1.27 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Mixed  |    17.162 ns |  0.1244 ns |  0.1164 ns |     4.74 |    0.05 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Mixed  |    18.278 ns |  0.1429 ns |  0.1336 ns |     5.05 |    0.06 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Mixed  |   272.174 ns |  2.1194 ns |  1.8788 ns |    75.13 |    0.87 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   184.713 ns |  2.1896 ns |  2.0481 ns |    50.98 |    0.73 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 4096  | Mixed  |   183.470 ns |  1.6619 ns |  1.5545 ns |    50.64 |    0.63 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   183.414 ns |  1.8681 ns |  1.7474 ns |    50.63 |    0.67 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Mixed  |   205.978 ns |  2.0308 ns |  1.8996 ns |    56.85 |    0.74 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Mixed  |   213.839 ns |  1.5922 ns |  1.4893 ns |    59.02 |    0.68 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **65536** | **Ascii**  |     **4.012 ns** |  **0.0359 ns** |  **0.0336 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Ascii  |     3.462 ns |  0.0296 ns |  0.0262 ns |     0.86 |    0.01 |         - |          NA |
| U8String.Contains           | 65536 | Ascii  |     3.613 ns |  0.0322 ns |  0.0286 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Ascii  |     4.571 ns |  0.0432 ns |  0.0404 ns |     1.14 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Ascii  |    16.874 ns |  0.0564 ns |  0.0440 ns |     4.21 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Ascii  |    18.033 ns |  0.1490 ns |  0.1394 ns |     4.50 |    0.05 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Ascii  | 4,193.850 ns | 17.4132 ns | 16.2883 ns | 1,045.38 |    9.35 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 2,091.322 ns | 10.4817 ns |  9.8046 ns |   521.29 |    4.85 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 65536 | Ascii  | 2,092.973 ns | 16.9983 ns | 15.9003 ns |   521.71 |    5.72 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 2,095.146 ns | 10.7070 ns | 10.0153 ns |   522.25 |    4.88 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Ascii  | 2,118.608 ns | 22.5657 ns | 21.1080 ns |   528.10 |    6.66 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Ascii  | 2,116.326 ns | 22.4997 ns | 21.0463 ns |   527.53 |    6.64 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **65536** | **Cjk**    |     **2.579 ns** |  **0.0192 ns** |  **0.0180 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Cjk    |     3.032 ns |  0.0207 ns |  0.0193 ns |     1.18 |    0.01 |         - |          NA |
| U8String.Contains           | 65536 | Cjk    |     3.228 ns |  0.0274 ns |  0.0257 ns |     1.25 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Cjk    |     4.348 ns |  0.0365 ns |  0.0341 ns |     1.69 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Cjk    |    37.211 ns |  0.3121 ns |  0.2919 ns |    14.43 |    0.15 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Cjk    |    33.308 ns |  0.5083 ns |  0.4755 ns |    12.92 |    0.20 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Cjk    | 4,192.512 ns | 19.3728 ns | 17.1734 ns | 1,625.71 |   12.72 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 6,288.579 ns | 33.1132 ns | 29.3540 ns | 2,438.49 |   19.80 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 65536 | Cjk    | 6,288.107 ns | 48.0663 ns | 44.9613 ns | 2,438.31 |   23.58 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 6,349.109 ns | 62.2994 ns | 58.2749 ns | 2,461.96 |   27.48 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Cjk    | 6,353.044 ns | 67.6994 ns | 63.3261 ns | 2,463.49 |   29.02 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Cjk    | 6,285.866 ns | 32.5436 ns | 27.1754 ns | 2,437.44 |   19.34 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **65536** | **Emoji**  |     **3.181 ns** |  **0.0138 ns** |  **0.0122 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Emoji  |     3.115 ns |  0.0179 ns |  0.0168 ns |     0.98 |    0.01 |         - |          NA |
| U8String.Contains           | 65536 | Emoji  |     3.249 ns |  0.0109 ns |  0.0102 ns |     1.02 |    0.00 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Emoji  |     4.461 ns |  0.0358 ns |  0.0318 ns |     1.40 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Emoji  |    29.085 ns |  0.2757 ns |  0.2579 ns |     9.14 |    0.09 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Emoji  |    33.826 ns |  0.1775 ns |  0.1660 ns |    10.63 |    0.06 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Emoji  | 4,224.404 ns | 43.4596 ns | 40.6521 ns | 1,327.83 |   13.31 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Emoji  | 6,767.501 ns | 96.7732 ns | 90.5217 ns | 2,127.19 |   28.66 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 65536 | Emoji  | 6,860.387 ns | 67.6933 ns | 63.3204 ns | 2,156.38 |   20.86 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Emoji  | 6,767.434 ns | 98.2233 ns | 87.0724 ns | 2,127.17 |   27.59 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Emoji  | 6,868.234 ns | 68.1640 ns | 63.7607 ns | 2,158.85 |   20.99 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Emoji  | 6,704.884 ns | 56.9848 ns | 53.3036 ns | 2,107.50 |   18.00 |         - |          NA |
|                             |       |        |              |            |            |          |         |           |             |
| **string.Contains**             | **65536** | **Mixed**  |     **3.546 ns** |  **0.0102 ns** |  **0.0080 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Mixed  |     3.314 ns |  0.0139 ns |  0.0116 ns |     0.93 |    0.00 |         - |          NA |
| U8String.Contains           | 65536 | Mixed  |     3.470 ns |  0.0074 ns |  0.0058 ns |     0.98 |    0.00 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Mixed  |     4.536 ns |  0.0229 ns |  0.0203 ns |     1.28 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Mixed  |    16.991 ns |  0.0632 ns |  0.0528 ns |     4.79 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Mixed  |    18.102 ns |  0.1464 ns |  0.1370 ns |     5.10 |    0.04 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Mixed  | 4,158.233 ns | 25.4676 ns | 23.8224 ns | 1,172.67 |    6.99 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 2,707.799 ns | 25.9489 ns | 24.2726 ns |   763.63 |    6.83 |         - |          NA |
| &#39;U8String.Contains miss&#39;    | 65536 | Mixed  | 2,680.902 ns | 10.6776 ns |  8.3364 ns |   756.05 |    2.79 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 2,700.555 ns | 18.4308 ns | 16.3384 ns |   761.59 |    4.75 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Mixed  | 2,721.871 ns | 23.9982 ns | 22.4480 ns |   767.60 |    6.35 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Mixed  | 2,732.855 ns | 23.1805 ns | 19.3567 ns |   770.70 |    5.52 |         - |          NA |
