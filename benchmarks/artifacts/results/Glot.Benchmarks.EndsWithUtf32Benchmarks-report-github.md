```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N     | Locale | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64**    | **Ascii**  |  **0.5560 ns** | **0.0740 ns** | **0.0041 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Ascii  |  0.5282 ns | 0.5560 ns | 0.0305 ns |  0.95 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Ascii  |  2.1715 ns | 0.0783 ns | 0.0043 ns |  3.91 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Ascii  | 13.5567 ns | 0.7003 ns | 0.0384 ns | 24.38 |    0.17 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Ascii  | 11.4995 ns | 1.6078 ns | 0.0881 ns | 20.68 |    0.19 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Ascii  |  0.5507 ns | 0.0275 ns | 0.0015 ns |  0.99 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.5301 ns | 0.1621 ns | 0.0089 ns |  0.95 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  2.2169 ns | 0.1029 ns | 0.0056 ns |  3.99 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Ascii  | 12.7373 ns | 0.4900 ns | 0.0269 ns | 22.91 |    0.15 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Ascii  | 11.8815 ns | 0.1360 ns | 0.0075 ns | 21.37 |    0.14 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Latin**  |  **0.5937 ns** | **0.7349 ns** | **0.0403 ns** |  **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Latin  |  0.5463 ns | 0.2121 ns | 0.0116 ns |  0.92 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Latin  |  2.2489 ns | 0.2089 ns | 0.0114 ns |  3.80 |    0.22 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Latin  | 13.1743 ns | 2.3969 ns | 0.1314 ns | 22.26 |    1.30 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Latin  | 11.4767 ns | 0.3650 ns | 0.0200 ns | 19.39 |    1.12 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Latin  |  0.5549 ns | 0.2058 ns | 0.0113 ns |  0.94 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Latin  |  0.5364 ns | 0.2899 ns | 0.0159 ns |  0.91 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Latin  |  2.2179 ns | 0.1274 ns | 0.0070 ns |  3.75 |    0.22 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Latin  | 13.3584 ns | 0.8024 ns | 0.0440 ns | 22.57 |    1.30 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Latin  | 11.8463 ns | 0.3401 ns | 0.0186 ns | 20.01 |    1.15 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Cjk**    |  **0.5563 ns** | **0.2893 ns** | **0.0159 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Cjk    |  0.5354 ns | 0.0914 ns | 0.0050 ns |  0.96 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Cjk    |  2.2697 ns | 0.2444 ns | 0.0134 ns |  4.08 |    0.10 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Cjk    | 14.0439 ns | 1.0230 ns | 0.0561 ns | 25.26 |    0.62 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Cjk    | 12.7053 ns | 3.5694 ns | 0.1957 ns | 22.85 |    0.63 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Cjk    |  0.5449 ns | 0.0928 ns | 0.0051 ns |  0.98 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.5452 ns | 0.1610 ns | 0.0088 ns |  0.98 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  2.2391 ns | 0.1591 ns | 0.0087 ns |  4.03 |    0.10 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Cjk    | 13.9948 ns | 0.1099 ns | 0.0060 ns | 25.17 |    0.61 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Cjk    | 12.6302 ns | 0.5715 ns | 0.0313 ns | 22.72 |    0.55 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Emoji**  |  **0.5649 ns** | **0.3274 ns** | **0.0179 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Emoji  |  0.5421 ns | 0.1623 ns | 0.0089 ns |  0.96 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Emoji  |  2.2315 ns | 0.2170 ns | 0.0119 ns |  3.95 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Emoji  | 17.7933 ns | 3.3537 ns | 0.1838 ns | 31.52 |    0.91 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Emoji  | 12.8796 ns | 2.3189 ns | 0.1271 ns | 22.82 |    0.66 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Emoji  |  0.5458 ns | 0.1162 ns | 0.0064 ns |  0.97 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Emoji  |  0.5382 ns | 0.0133 ns | 0.0007 ns |  0.95 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Emoji  |  2.2139 ns | 0.1276 ns | 0.0070 ns |  3.92 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Emoji  | 16.8467 ns | 3.1955 ns | 0.1752 ns | 29.84 |    0.86 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Emoji  | 12.8473 ns | 0.8065 ns | 0.0442 ns | 22.76 |    0.63 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Mixed**  |  **0.5557 ns** | **0.1792 ns** | **0.0098 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Mixed  |  0.5457 ns | 0.0770 ns | 0.0042 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Mixed  |  2.2809 ns | 1.0682 ns | 0.0586 ns |  4.11 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Mixed  | 12.7042 ns | 0.8566 ns | 0.0470 ns | 22.87 |    0.35 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Mixed  | 11.5621 ns | 0.2346 ns | 0.0129 ns | 20.81 |    0.32 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Mixed  |  0.5441 ns | 0.0703 ns | 0.0039 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.5287 ns | 0.0967 ns | 0.0053 ns |  0.95 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  2.2572 ns | 0.2049 ns | 0.0112 ns |  4.06 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Mixed  | 12.7588 ns | 1.1464 ns | 0.0628 ns | 22.97 |    0.36 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Mixed  | 11.4956 ns | 0.2653 ns | 0.0145 ns | 20.69 |    0.31 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Ascii**  |  **0.5921 ns** | **0.0395 ns** | **0.0022 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Ascii  |  0.5396 ns | 0.0362 ns | 0.0020 ns |  0.91 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Ascii  |  2.1656 ns | 0.3936 ns | 0.0216 ns |  3.66 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Ascii  | 12.7653 ns | 1.5839 ns | 0.0868 ns | 21.56 |    0.14 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Ascii  | 11.8488 ns | 0.3317 ns | 0.0182 ns | 20.01 |    0.07 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Ascii  |  0.5560 ns | 0.1906 ns | 0.0104 ns |  0.94 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Ascii  |  0.5297 ns | 0.2457 ns | 0.0135 ns |  0.89 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Ascii  |  2.2194 ns | 0.1722 ns | 0.0094 ns |  3.75 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Ascii  | 13.1419 ns | 0.6907 ns | 0.0379 ns | 22.20 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Ascii  | 12.0073 ns | 1.6302 ns | 0.0894 ns | 20.28 |    0.15 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Latin**  |  **0.5487 ns** | **0.0967 ns** | **0.0053 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Latin  |  0.5530 ns | 0.1833 ns | 0.0100 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Latin  |  2.2185 ns | 0.1714 ns | 0.0094 ns |  4.04 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Latin  | 13.2939 ns | 1.5473 ns | 0.0848 ns | 24.23 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Latin  | 11.9196 ns | 0.3620 ns | 0.0198 ns | 21.73 |    0.18 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Latin  |  0.5578 ns | 0.3543 ns | 0.0194 ns |  1.02 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Latin  |  0.5540 ns | 0.4439 ns | 0.0243 ns |  1.01 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Latin  |  2.4912 ns | 0.0722 ns | 0.0040 ns |  4.54 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Latin  | 12.7637 ns | 0.4473 ns | 0.0245 ns | 23.26 |    0.20 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Latin  | 11.5921 ns | 6.1053 ns | 0.3347 ns | 21.13 |    0.56 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Cjk**    |  **0.5716 ns** | **0.3634 ns** | **0.0199 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Cjk    |  0.5324 ns | 0.0104 ns | 0.0006 ns |  0.93 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Cjk    |  2.2530 ns | 0.8972 ns | 0.0492 ns |  3.94 |    0.14 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Cjk    | 14.3713 ns | 0.1353 ns | 0.0074 ns | 25.16 |    0.74 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Cjk    | 12.0900 ns | 0.3343 ns | 0.0183 ns | 21.17 |    0.63 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Cjk    |  0.5562 ns | 0.1793 ns | 0.0098 ns |  0.97 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Cjk    |  0.5335 ns | 0.2617 ns | 0.0143 ns |  0.93 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Cjk    |  2.2282 ns | 0.1399 ns | 0.0077 ns |  3.90 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Cjk    | 14.3588 ns | 2.1006 ns | 0.1151 ns | 25.14 |    0.76 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Cjk    | 12.5042 ns | 0.6542 ns | 0.0359 ns | 21.89 |    0.65 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Emoji**  |  **0.5705 ns** | **0.0424 ns** | **0.0023 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Emoji  |  0.5427 ns | 0.0282 ns | 0.0015 ns |  0.95 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Emoji  |  2.2250 ns | 0.1293 ns | 0.0071 ns |  3.90 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Emoji  | 16.8809 ns | 1.2515 ns | 0.0686 ns | 29.59 |    0.15 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Emoji  | 12.8202 ns | 0.2587 ns | 0.0142 ns | 22.47 |    0.08 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Emoji  |  0.5629 ns | 0.3644 ns | 0.0200 ns |  0.99 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Emoji  |  0.5355 ns | 0.0175 ns | 0.0010 ns |  0.94 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Emoji  |  2.2660 ns | 0.3846 ns | 0.0211 ns |  3.97 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Emoji  | 16.7310 ns | 0.9763 ns | 0.0535 ns | 29.33 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Emoji  | 12.4206 ns | 0.1190 ns | 0.0065 ns | 21.77 |    0.08 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Mixed**  |  **0.5425 ns** | **0.0788 ns** | **0.0043 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Mixed  |  0.5451 ns | 0.2210 ns | 0.0121 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Mixed  |  2.2319 ns | 0.5139 ns | 0.0282 ns |  4.11 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Mixed  | 14.7074 ns | 3.9570 ns | 0.2169 ns | 27.11 |    0.39 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Mixed  | 12.3213 ns | 0.5906 ns | 0.0324 ns | 22.71 |    0.16 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Mixed  |  0.5675 ns | 0.1612 ns | 0.0088 ns |  1.05 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Mixed  |  0.5349 ns | 0.2475 ns | 0.0136 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Mixed  |  2.2331 ns | 0.1527 ns | 0.0084 ns |  4.12 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Mixed  | 14.4394 ns | 1.1044 ns | 0.0605 ns | 26.62 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Mixed  | 12.4685 ns | 1.2651 ns | 0.0693 ns | 22.99 |    0.19 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Ascii**  |  **0.8098 ns** | **0.2317 ns** | **0.0127 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Ascii  |  0.5477 ns | 0.2174 ns | 0.0119 ns |  0.68 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Ascii  |  2.1534 ns | 0.0451 ns | 0.0025 ns |  2.66 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Ascii  | 12.8365 ns | 0.2951 ns | 0.0162 ns | 15.85 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Ascii  | 11.5328 ns | 0.5984 ns | 0.0328 ns | 14.24 |    0.19 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Ascii  |  0.5755 ns | 0.2115 ns | 0.0116 ns |  0.71 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  0.5243 ns | 0.0780 ns | 0.0043 ns |  0.65 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  2.2265 ns | 0.6350 ns | 0.0348 ns |  2.75 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Ascii  | 12.8404 ns | 1.9088 ns | 0.1046 ns | 15.86 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Ascii  | 11.3924 ns | 0.9644 ns | 0.0529 ns | 14.07 |    0.20 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Latin**  |  **0.5657 ns** | **0.2565 ns** | **0.0141 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Latin  |  0.5400 ns | 0.0911 ns | 0.0050 ns |  0.96 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Latin  |  2.2295 ns | 0.0603 ns | 0.0033 ns |  3.94 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Latin  | 13.4246 ns | 0.3460 ns | 0.0190 ns | 23.74 |    0.52 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Latin  | 11.8623 ns | 0.3263 ns | 0.0179 ns | 20.98 |    0.46 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Latin  |  0.6516 ns | 1.3590 ns | 0.0745 ns |  1.15 |    0.12 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Latin  |  0.5607 ns | 0.6438 ns | 0.0353 ns |  0.99 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Latin  |  2.2175 ns | 0.0230 ns | 0.0013 ns |  3.92 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Latin  | 12.8490 ns | 1.1980 ns | 0.0657 ns | 22.72 |    0.50 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Latin  | 11.7088 ns | 0.8015 ns | 0.0439 ns | 20.71 |    0.45 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Cjk**    |  **0.6073 ns** | **0.4162 ns** | **0.0228 ns** |  **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Cjk    |  0.5380 ns | 0.0270 ns | 0.0015 ns |  0.89 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Cjk    |  2.2379 ns | 0.1224 ns | 0.0067 ns |  3.69 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Cjk    | 13.9098 ns | 1.2026 ns | 0.0659 ns | 22.93 |    0.74 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Cjk    | 12.0782 ns | 0.1058 ns | 0.0058 ns | 19.91 |    0.64 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Cjk    |  0.5638 ns | 0.2042 ns | 0.0112 ns |  0.93 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.6140 ns | 2.1232 ns | 0.1164 ns |  1.01 |    0.17 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  2.2298 ns | 0.2038 ns | 0.0112 ns |  3.67 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Cjk    | 13.6877 ns | 1.1428 ns | 0.0626 ns | 22.56 |    0.73 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Cjk    | 12.5911 ns | 2.7859 ns | 0.1527 ns | 20.75 |    0.70 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Emoji**  |  **0.8329 ns** | **0.4414 ns** | **0.0242 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Emoji  |  0.5471 ns | 0.1377 ns | 0.0075 ns |  0.66 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Emoji  |  2.2286 ns | 0.1460 ns | 0.0080 ns |  2.68 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Emoji  | 16.8523 ns | 0.9379 ns | 0.0514 ns | 20.25 |    0.52 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Emoji  | 12.8728 ns | 0.7654 ns | 0.0420 ns | 15.46 |    0.40 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Emoji  |  0.5678 ns | 0.1779 ns | 0.0098 ns |  0.68 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Emoji  |  0.5281 ns | 0.0219 ns | 0.0012 ns |  0.63 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Emoji  |  2.2050 ns | 0.2953 ns | 0.0162 ns |  2.65 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Emoji  | 17.3482 ns | 6.8346 ns | 0.3746 ns | 20.84 |    0.66 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Emoji  | 12.9805 ns | 0.2462 ns | 0.0135 ns | 15.59 |    0.40 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Mixed**  |  **0.5839 ns** | **0.7682 ns** | **0.0421 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Mixed  |  0.6070 ns | 1.2004 ns | 0.0658 ns |  1.04 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Mixed  |  2.2301 ns | 0.1185 ns | 0.0065 ns |  3.83 |    0.23 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Mixed  | 12.8077 ns | 0.4259 ns | 0.0233 ns | 22.01 |    1.32 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Mixed  | 12.0465 ns | 1.1287 ns | 0.0619 ns | 20.70 |    1.25 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Mixed  |  3.8223 ns | 2.0498 ns | 0.1124 ns |  6.57 |    0.43 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.5284 ns | 0.0593 ns | 0.0032 ns |  0.91 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  2.2127 ns | 0.2331 ns | 0.0128 ns |  3.80 |    0.23 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Mixed  | 13.3151 ns | 0.0763 ns | 0.0042 ns | 22.88 |    1.37 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Mixed  | 12.0210 ns | 0.1605 ns | 0.0088 ns | 20.66 |    1.24 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Ascii**  |  **0.5576 ns** | **0.1132 ns** | **0.0062 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Ascii  |  0.5420 ns | 0.2210 ns | 0.0121 ns |  0.97 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Ascii  |  2.1965 ns | 0.3484 ns | 0.0191 ns |  3.94 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Ascii  | 12.8788 ns | 1.3987 ns | 0.0767 ns | 23.10 |    0.25 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Ascii  | 11.5475 ns | 0.0647 ns | 0.0035 ns | 20.71 |    0.20 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Ascii  |  0.5478 ns | 0.0413 ns | 0.0023 ns |  0.98 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.5271 ns | 0.0496 ns | 0.0027 ns |  0.95 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  2.2728 ns | 0.2562 ns | 0.0140 ns |  4.08 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Ascii  | 13.8006 ns | 0.5531 ns | 0.0303 ns | 24.75 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Ascii  | 12.2589 ns | 2.2120 ns | 0.1212 ns | 21.99 |    0.28 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Latin**  |  **0.5591 ns** | **0.2789 ns** | **0.0153 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Latin  |  0.5463 ns | 0.0541 ns | 0.0030 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Latin  |  2.2432 ns | 0.0339 ns | 0.0019 ns |  4.01 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Latin  | 13.3423 ns | 0.2648 ns | 0.0145 ns | 23.87 |    0.56 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Latin  | 11.6986 ns | 1.6349 ns | 0.0896 ns | 20.93 |    0.51 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Latin  |  0.5585 ns | 0.3423 ns | 0.0188 ns |  1.00 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Latin  |  0.5398 ns | 0.0973 ns | 0.0053 ns |  0.97 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Latin  |  2.2537 ns | 0.0503 ns | 0.0028 ns |  4.03 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Latin  | 12.8983 ns | 0.5961 ns | 0.0327 ns | 23.08 |    0.54 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Latin  | 11.7759 ns | 0.3280 ns | 0.0180 ns | 21.07 |    0.49 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Cjk**    |  **0.5578 ns** | **0.2959 ns** | **0.0162 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Cjk    |  0.5450 ns | 0.1603 ns | 0.0088 ns |  0.98 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Cjk    |  2.2796 ns | 0.4659 ns | 0.0255 ns |  4.09 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Cjk    | 14.3400 ns | 1.7124 ns | 0.0939 ns | 25.72 |    0.65 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Cjk    | 12.7715 ns | 0.7670 ns | 0.0420 ns | 22.91 |    0.57 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Cjk    |  0.5661 ns | 0.3531 ns | 0.0194 ns |  1.02 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.5290 ns | 0.0643 ns | 0.0035 ns |  0.95 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  2.2680 ns | 0.0141 ns | 0.0008 ns |  4.07 |    0.10 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Cjk    | 14.0392 ns | 1.0189 ns | 0.0558 ns | 25.18 |    0.63 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Cjk    | 12.7147 ns | 0.4912 ns | 0.0269 ns | 22.81 |    0.57 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Emoji**  |  **0.5596 ns** | **0.2536 ns** | **0.0139 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Emoji  |  0.5479 ns | 0.0286 ns | 0.0016 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Emoji  |  2.2846 ns | 0.1807 ns | 0.0099 ns |  4.08 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Emoji  | 17.6319 ns | 1.4160 ns | 0.0776 ns | 31.52 |    0.69 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Emoji  | 12.4477 ns | 0.4332 ns | 0.0237 ns | 22.25 |    0.48 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Emoji  |  0.5721 ns | 0.3564 ns | 0.0195 ns |  1.02 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Emoji  |  0.5553 ns | 0.2000 ns | 0.0110 ns |  0.99 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Emoji  |  2.2558 ns | 0.1489 ns | 0.0082 ns |  4.03 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Emoji  | 17.6272 ns | 1.6334 ns | 0.0895 ns | 31.51 |    0.69 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Emoji  | 12.8252 ns | 4.8223 ns | 0.2643 ns | 22.93 |    0.64 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Mixed**  |  **0.5777 ns** | **0.8146 ns** | **0.0447 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Mixed  |  0.5395 ns | 0.0937 ns | 0.0051 ns |  0.94 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Mixed  |  2.2864 ns | 0.4719 ns | 0.0259 ns |  3.97 |    0.26 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Mixed  | 14.2562 ns | 1.0749 ns | 0.0589 ns | 24.77 |    1.59 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Mixed  | 11.6158 ns | 0.5083 ns | 0.0279 ns | 20.18 |    1.29 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Mixed  |  0.5446 ns | 0.2504 ns | 0.0137 ns |  0.95 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.5351 ns | 0.1207 ns | 0.0066 ns |  0.93 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  2.3641 ns | 0.2020 ns | 0.0111 ns |  4.11 |    0.26 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Mixed  | 12.9321 ns | 1.0943 ns | 0.0600 ns | 22.47 |    1.44 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Mixed  | 12.0281 ns | 0.5135 ns | 0.0281 ns | 20.90 |    1.34 |         - |          NA |
