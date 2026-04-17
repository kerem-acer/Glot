```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | N     | Locale | Mean      | Error      | StdDev    | Median    | Ratio      | RatioSD   | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |----------:|-----------:|----------:|----------:|-----------:|----------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  | **0.1622 ns** |  **0.4125 ns** | **0.0226 ns** | **0.1569 ns** |       **1.01** |      **0.17** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  | 0.0416 ns |  0.3210 ns | 0.0176 ns | 0.0359 ns |       0.26 |      0.10 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  | 0.7737 ns |  0.1234 ns | 0.0068 ns | 0.7723 ns |       4.83 |      0.56 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  | 6.9113 ns |  0.2491 ns | 0.0137 ns | 6.9074 ns |      43.14 |      5.01 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  | 5.3395 ns |  0.2213 ns | 0.0121 ns | 5.3326 ns |      33.33 |      3.87 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  | 0.1861 ns |  0.4005 ns | 0.0220 ns | 0.1851 ns |       1.16 |      0.18 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 0.2701 ns |  0.2828 ns | 0.0155 ns | 0.2644 ns |       1.69 |      0.21 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 0.8398 ns |  0.0305 ns | 0.0017 ns | 0.8398 ns |       5.24 |      0.61 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  | 6.9708 ns |  0.5511 ns | 0.0302 ns | 6.9695 ns |      43.52 |      5.06 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  | 5.3383 ns |  0.0882 ns | 0.0048 ns | 5.3356 ns |      33.33 |      3.87 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    | **0.1330 ns** |  **0.6801 ns** | **0.0373 ns** | **0.1177 ns** |      **1.048** |      **0.34** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    | 0.5585 ns |  8.9098 ns | 0.4884 ns | 0.3585 ns |      4.401 |      3.53 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    | 1.1184 ns | 10.0192 ns | 0.5492 ns | 1.2304 ns |      8.813 |      4.26 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    | 8.2892 ns |  8.0599 ns | 0.4418 ns | 8.0445 ns |     65.314 |     14.33 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    | 5.9539 ns |  1.9109 ns | 0.1047 ns | 5.9193 ns |     46.914 |     10.08 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    | 0.0444 ns |  0.1172 ns | 0.0064 ns | 0.0456 ns |      0.350 |      0.09 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 0.8629 ns |  1.1337 ns | 0.0621 ns | 0.8482 ns |      6.799 |      1.52 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    | 7.9139 ns |  2.6742 ns | 0.1466 ns | 7.9450 ns |     62.357 |     13.40 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    | 5.8637 ns |  0.1222 ns | 0.0067 ns | 5.8621 ns |     46.203 |      9.90 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  | **0.0489 ns** |  **0.3189 ns** | **0.0175 ns** | **0.0457 ns** |      **1.087** |      **0.48** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  | 0.8134 ns |  0.0483 ns | 0.0026 ns | 0.8147 ns |     18.070 |      5.38 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  | 7.0063 ns |  0.2592 ns | 0.0142 ns | 7.0131 ns |    155.642 |     46.37 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  | 5.3560 ns |  0.1116 ns | 0.0061 ns | 5.3554 ns |    118.981 |     35.45 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  | 0.0638 ns |  0.2445 ns | 0.0134 ns | 0.0708 ns |      1.417 |      0.50 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 0.8216 ns |  0.6460 ns | 0.0354 ns | 0.8074 ns |     18.252 |      5.48 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  | 6.9412 ns |  0.6864 ns | 0.0376 ns | 6.9198 ns |    154.194 |     45.95 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  | 5.3502 ns |  0.3643 ns | 0.0200 ns | 5.3413 ns |    118.853 |     35.41 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  | **0.1510 ns** |  **0.7012 ns** | **0.0384 ns** | **0.1305 ns** |       **1.04** |      **0.31** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  | 0.0372 ns |  0.7044 ns | 0.0386 ns | 0.0284 ns |       0.26 |      0.24 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  | 0.7767 ns |  0.1464 ns | 0.0080 ns | 0.7796 ns |       5.34 |      1.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  | 6.9724 ns |  0.4115 ns | 0.0226 ns | 6.9844 ns |      47.98 |      9.24 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  | 5.3176 ns |  0.5065 ns | 0.0278 ns | 5.3023 ns |      36.59 |      7.05 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  | 0.1435 ns |  0.5005 ns | 0.0274 ns | 0.1355 ns |       0.99 |      0.25 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 0.2598 ns |  0.2502 ns | 0.0137 ns | 0.2604 ns |       1.79 |      0.35 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 0.8433 ns |  1.4464 ns | 0.0793 ns | 0.7992 ns |       5.80 |      1.22 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  | 7.1665 ns |  5.6397 ns | 0.3091 ns | 7.0417 ns |      49.31 |      9.67 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  | 5.3036 ns |  0.3187 ns | 0.0175 ns | 5.3001 ns |      36.49 |      7.03 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    | **0.0844 ns** |  **0.4369 ns** | **0.0239 ns** | **0.0912 ns** |      **1.065** |      **0.40** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    | 0.8538 ns |  0.2333 ns | 0.0128 ns | 0.8505 ns |     10.769 |      3.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    | 7.8957 ns |  0.8071 ns | 0.0442 ns | 7.8767 ns |     99.590 |     28.07 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    | 5.7742 ns |  0.3218 ns | 0.0176 ns | 5.7681 ns |     72.831 |     20.53 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    | 0.1415 ns |  0.4381 ns | 0.0240 ns | 0.1510 ns |      1.785 |      0.57 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 0.8330 ns |  0.1565 ns | 0.0086 ns | 0.8306 ns |     10.507 |      2.96 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    | 7.6301 ns |  0.7691 ns | 0.0422 ns | 7.6354 ns |     96.240 |     27.13 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    | 5.8139 ns |  0.6296 ns | 0.0345 ns | 5.8249 ns |     73.331 |     20.67 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  | **0.1275 ns** |  **0.2710 ns** | **0.0149 ns** | **0.1270 ns** |      **1.009** |      **0.14** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  | 0.7952 ns |  0.1201 ns | 0.0066 ns | 0.7946 ns |      6.293 |      0.64 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  | 6.9361 ns |  0.3060 ns | 0.0168 ns | 6.9282 ns |     54.888 |      5.54 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  | 5.2777 ns |  0.2019 ns | 0.0111 ns | 5.2775 ns |     41.765 |      4.22 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  | 0.1166 ns |  0.7087 ns | 0.0388 ns | 0.1383 ns |      0.923 |      0.28 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 0.8452 ns |  0.5090 ns | 0.0279 ns | 0.8587 ns |      6.688 |      0.70 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  | 7.0921 ns |  1.4353 ns | 0.0787 ns | 7.1227 ns |     56.123 |      5.69 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  | 5.3807 ns |  0.6267 ns | 0.0343 ns | 5.3709 ns |     42.580 |      4.30 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  | **0.1173 ns** |  **0.5575 ns** | **0.0306 ns** | **0.1340 ns** |       **1.06** |      **0.37** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  | 0.0518 ns |  0.3325 ns | 0.0182 ns | 0.0468 ns |       0.47 |      0.19 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  | 0.7763 ns |  0.1790 ns | 0.0098 ns | 0.7815 ns |       6.99 |      1.86 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  | 7.2003 ns |  3.3453 ns | 0.1834 ns | 7.1034 ns |      64.84 |     17.28 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  | 5.5683 ns |  0.4145 ns | 0.0227 ns | 5.5586 ns |      50.14 |     13.31 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  | 0.1341 ns |  0.3153 ns | 0.0173 ns | 0.1300 ns |       1.21 |      0.35 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 0.2780 ns |  0.1463 ns | 0.0080 ns | 0.2757 ns |       2.50 |      0.67 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 0.8129 ns |  0.1255 ns | 0.0069 ns | 0.8136 ns |       7.32 |      1.94 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  | 7.1449 ns |  2.7476 ns | 0.1506 ns | 7.0637 ns |      64.34 |     17.12 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  | 5.4790 ns |  0.2277 ns | 0.0125 ns | 5.4816 ns |      49.34 |     13.10 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    | **0.0117 ns** |  **0.3173 ns** | **0.0174 ns** | **0.0031 ns** |     **16.245** |     **39.54** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    | 0.8768 ns |  0.1165 ns | 0.0064 ns | 0.8738 ns |  1,220.323 |  1,599.18 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    | 7.7626 ns |  0.2770 ns | 0.0152 ns | 7.7633 ns | 10,804.405 | 14,158.28 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    | 5.8383 ns |  0.1389 ns | 0.0076 ns | 5.8359 ns |  8,125.995 | 10,648.43 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    | 0.0014 ns |  0.0307 ns | 0.0017 ns | 0.0009 ns |      1.953 |      4.12 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 0.8685 ns |  0.0239 ns | 0.0013 ns | 0.8688 ns |  1,208.864 |  1,584.11 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 7.9061 ns |  1.1170 ns | 0.0612 ns | 7.8790 ns | 11,004.093 | 14,420.40 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    | 5.8777 ns |  1.6627 ns | 0.0911 ns | 5.8274 ns |  8,180.850 | 10,721.73 |         - |          NA |
|                               |       |        |           |            |           |           |            |           |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  | **0.1353 ns** |  **0.4704 ns** | **0.0258 ns** | **0.1464 ns** |      **1.028** |      **0.26** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  | 0.8659 ns |  0.2026 ns | 0.0111 ns | 0.8644 ns |      6.579 |      1.21 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  | 6.9283 ns |  0.5386 ns | 0.0295 ns | 6.9213 ns |     52.634 |      9.68 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  | 5.3161 ns |  0.4298 ns | 0.0236 ns | 5.3187 ns |     40.386 |      7.43 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  | 0.1390 ns |  0.3803 ns | 0.0208 ns | 0.1504 ns |      1.056 |      0.24 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 0.0000 ns |  0.0000 ns | 0.0000 ns | 0.0000 ns |      0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 0.8558 ns |  0.1595 ns | 0.0087 ns | 0.8560 ns |      6.502 |      1.20 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  | 6.9016 ns |  0.3010 ns | 0.0165 ns | 6.8983 ns |     52.432 |      9.65 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  | 5.3401 ns |  0.0713 ns | 0.0039 ns | 5.3386 ns |     40.569 |      7.46 |         - |          NA |
