```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N     | Locale | Mean       | Error      | StdDev    | Median     | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-----------:|-----------:|----------:|-----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64**    | **Ascii**  |  **0.5340 ns** |  **0.2382 ns** | **0.0131 ns** |  **0.5396 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Ascii  |  0.5022 ns |  0.1696 ns | 0.0093 ns |  0.5019 ns |  0.94 |    0.03 |         - |          NA |
| U8String.EndsWith           | 64    | Ascii  |  0.5974 ns |  2.7322 ns | 0.1498 ns |  0.5214 ns |  1.12 |    0.24 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Ascii  |  1.4895 ns |  0.5598 ns | 0.0307 ns |  1.4766 ns |  2.79 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Ascii  |  8.6195 ns |  0.2842 ns | 0.0156 ns |  8.6127 ns | 16.15 |    0.35 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Ascii  |  6.9226 ns |  1.0283 ns | 0.0564 ns |  6.9435 ns | 12.97 |    0.29 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Ascii  |  0.5715 ns |  0.6161 ns | 0.0338 ns |  0.5706 ns |  1.07 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.5086 ns |  0.0835 ns | 0.0046 ns |  0.5098 ns |  0.95 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Ascii  |  0.5050 ns |  0.1060 ns | 0.0058 ns |  0.5053 ns |  0.95 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  1.5215 ns |  0.1622 ns | 0.0089 ns |  1.5208 ns |  2.85 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Ascii  |  8.6242 ns |  2.2543 ns | 0.1236 ns |  8.6731 ns | 16.16 |    0.40 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Ascii  |  6.8391 ns |  1.0514 ns | 0.0576 ns |  6.8164 ns | 12.81 |    0.29 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **64**    | **Cjk**    |  **0.5079 ns** |  **0.0564 ns** | **0.0031 ns** |  **0.5065 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Cjk    |  0.8041 ns |  8.0839 ns | 0.4431 ns |  0.5740 ns |  1.58 |    0.76 |         - |          NA |
| U8String.EndsWith           | 64    | Cjk    |  0.4954 ns |  0.5680 ns | 0.0311 ns |  0.4789 ns |  0.98 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Cjk    |  1.5795 ns |  0.0891 ns | 0.0049 ns |  1.5777 ns |  3.11 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Cjk    | 10.3091 ns | 25.3762 ns | 1.3910 ns |  9.9593 ns | 20.30 |    2.37 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Cjk    |  7.2071 ns |  0.2991 ns | 0.0164 ns |  7.2094 ns | 14.19 |    0.08 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Cjk    |  0.4975 ns |  0.0868 ns | 0.0048 ns |  0.4983 ns |  0.98 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.5022 ns |  0.0687 ns | 0.0038 ns |  0.5012 ns |  0.99 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Cjk    |  0.9096 ns | 10.5818 ns | 0.5800 ns |  0.5848 ns |  1.79 |    0.99 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  2.1945 ns | 21.0308 ns | 1.1528 ns |  1.5399 ns |  4.32 |    1.97 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Cjk    |  9.3319 ns |  3.1925 ns | 0.1750 ns |  9.3043 ns | 18.37 |    0.31 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Cjk    |  7.1709 ns |  0.4633 ns | 0.0254 ns |  7.1631 ns | 14.12 |    0.09 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **64**    | **Mixed**  |  **0.5621 ns** |  **0.7373 ns** | **0.0404 ns** |  **0.5594 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Mixed  |  0.5543 ns |  1.1890 ns | 0.0652 ns |  0.5233 ns |  0.99 |    0.12 |         - |          NA |
| U8String.EndsWith           | 64    | Mixed  |  0.5209 ns |  0.2772 ns | 0.0152 ns |  0.5262 ns |  0.93 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Mixed  |  1.5509 ns |  0.9344 ns | 0.0512 ns |  1.5637 ns |  2.77 |    0.19 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Mixed  |  8.4003 ns |  2.6873 ns | 0.1473 ns |  8.3707 ns | 15.00 |    0.96 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Mixed  |  6.6816 ns |  0.7818 ns | 0.0429 ns |  6.6979 ns | 11.93 |    0.74 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Mixed  |  0.4980 ns |  0.3120 ns | 0.0171 ns |  0.5067 ns |  0.89 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.5035 ns |  0.7188 ns | 0.0394 ns |  0.4972 ns |  0.90 |    0.08 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Mixed  |  0.7518 ns |  8.4687 ns | 0.4642 ns |  0.4995 ns |  1.34 |    0.72 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  1.6041 ns |  0.4375 ns | 0.0240 ns |  1.6156 ns |  2.86 |    0.18 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Mixed  |  8.2980 ns |  1.2746 ns | 0.0699 ns |  8.3045 ns | 14.81 |    0.92 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Mixed  |  6.6638 ns |  0.7586 ns | 0.0416 ns |  6.6533 ns | 11.90 |    0.74 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Ascii**  |  **0.8012 ns** |  **8.4285 ns** | **0.4620 ns** |  **0.5632 ns** |  **1.20** |    **0.78** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Ascii  |  0.5807 ns |  1.1511 ns | 0.0631 ns |  0.5541 ns |  0.87 |    0.34 |         - |          NA |
| U8String.EndsWith           | 4096  | Ascii  |  0.5073 ns |  0.5682 ns | 0.0311 ns |  0.4983 ns |  0.76 |    0.29 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Ascii  |  1.6288 ns |  2.5726 ns | 0.1410 ns |  1.7096 ns |  2.44 |    0.95 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Ascii  |  8.5238 ns |  1.2011 ns | 0.0658 ns |  8.5512 ns | 12.78 |    4.85 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Ascii  |  6.6587 ns |  0.7652 ns | 0.0419 ns |  6.6641 ns |  9.98 |    3.79 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Ascii  |  0.5141 ns |  0.5205 ns | 0.0285 ns |  0.5226 ns |  0.77 |    0.30 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  2.7207 ns |  1.0698 ns | 0.0586 ns |  2.7116 ns |  4.08 |    1.55 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Ascii  |  0.4815 ns |  0.1973 ns | 0.0108 ns |  0.4780 ns |  0.72 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  1.5000 ns |  0.2464 ns | 0.0135 ns |  1.4949 ns |  2.25 |    0.85 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Ascii  |  8.0883 ns |  0.2191 ns | 0.0120 ns |  8.0819 ns | 12.13 |    4.60 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Ascii  |  6.8308 ns |  5.5748 ns | 0.3056 ns |  6.6689 ns | 10.24 |    3.91 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Cjk**    |  **0.5102 ns** |  **0.2123 ns** | **0.0116 ns** |  **0.5142 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Cjk    |  0.5014 ns |  0.0976 ns | 0.0054 ns |  0.4999 ns |  0.98 |    0.02 |         - |          NA |
| U8String.EndsWith           | 4096  | Cjk    |  0.4489 ns |  0.2336 ns | 0.0128 ns |  0.4463 ns |  0.88 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Cjk    |  1.5548 ns |  0.0195 ns | 0.0011 ns |  1.5547 ns |  3.05 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Cjk    |  9.0911 ns |  2.4081 ns | 0.1320 ns |  9.0639 ns | 17.82 |    0.42 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Cjk    |  7.1447 ns |  1.2460 ns | 0.0683 ns |  7.1791 ns | 14.01 |    0.30 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Cjk    |  0.7456 ns |  7.1051 ns | 0.3895 ns |  0.5220 ns |  1.46 |    0.66 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.6252 ns |  2.0219 ns | 0.1108 ns |  0.6315 ns |  1.23 |    0.19 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Cjk    |  0.4949 ns |  0.2505 ns | 0.0137 ns |  0.5003 ns |  0.97 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  1.5537 ns |  0.2546 ns | 0.0140 ns |  1.5541 ns |  3.05 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Cjk    |  9.0800 ns |  2.2633 ns | 0.1241 ns |  9.1115 ns | 17.80 |    0.41 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Cjk    |  7.2395 ns |  0.7096 ns | 0.0389 ns |  7.2320 ns | 14.19 |    0.29 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Mixed**  |  **0.5627 ns** |  **0.1347 ns** | **0.0074 ns** |  **0.5630 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Mixed  |  0.7457 ns |  7.9332 ns | 0.4348 ns |  0.5013 ns |  1.33 |    0.67 |         - |          NA |
| U8String.EndsWith           | 4096  | Mixed  |  0.4891 ns |  0.0975 ns | 0.0053 ns |  0.4885 ns |  0.87 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Mixed  |  1.9757 ns | 14.5652 ns | 0.7984 ns |  1.5190 ns |  3.51 |    1.23 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Mixed  | 10.1376 ns | 32.3950 ns | 1.7757 ns | 10.2958 ns | 18.02 |    2.74 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Mixed  |  6.6978 ns |  0.5469 ns | 0.0300 ns |  6.6994 ns | 11.90 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Mixed  |  0.6502 ns |  3.8372 ns | 0.2103 ns |  0.5521 ns |  1.16 |    0.32 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.5111 ns |  0.1973 ns | 0.0108 ns |  0.5078 ns |  0.91 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Mixed  |  0.4923 ns |  0.1719 ns | 0.0094 ns |  0.4873 ns |  0.87 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  1.5943 ns |  0.1244 ns | 0.0068 ns |  1.5929 ns |  2.83 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Mixed  |  8.5639 ns |  5.1480 ns | 0.2822 ns |  8.6091 ns | 15.22 |    0.47 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Mixed  |  6.6846 ns |  0.8474 ns | 0.0465 ns |  6.6766 ns | 11.88 |    0.15 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Ascii**  |  **0.7365 ns** |  **7.6729 ns** | **0.4206 ns** |  **0.4972 ns** |  **1.20** |    **0.77** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Ascii  |  0.7223 ns |  7.3189 ns | 0.4012 ns |  0.4941 ns |  1.17 |    0.74 |         - |          NA |
| U8String.EndsWith           | 65536 | Ascii  |  0.7263 ns |  7.0415 ns | 0.3860 ns |  0.5103 ns |  1.18 |    0.72 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Ascii  |  1.6038 ns |  2.3659 ns | 0.1297 ns |  1.6672 ns |  2.60 |    0.99 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Ascii  |  8.4243 ns |  0.6173 ns | 0.0338 ns |  8.4313 ns | 13.67 |    5.09 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Ascii  |  6.8996 ns |  3.0890 ns | 0.1693 ns |  6.8492 ns | 11.20 |    4.17 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Ascii  |  0.5115 ns |  0.1741 ns | 0.0095 ns |  0.5092 ns |  0.83 |    0.31 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.7320 ns |  7.1021 ns | 0.3893 ns |  0.5375 ns |  1.19 |    0.73 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Ascii  |  0.6255 ns |  4.4763 ns | 0.2454 ns |  0.4865 ns |  1.02 |    0.53 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  1.9959 ns |  7.9599 ns | 0.4363 ns |  1.9038 ns |  3.24 |    1.37 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Ascii  |  8.6699 ns |  0.8981 ns | 0.0492 ns |  8.6446 ns | 14.07 |    5.24 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Ascii  |  6.8030 ns |  0.1436 ns | 0.0079 ns |  6.7988 ns | 11.04 |    4.11 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Cjk**    |  **0.5531 ns** |  **0.0734 ns** | **0.0040 ns** |  **0.5525 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Cjk    |  0.8004 ns |  9.5284 ns | 0.5223 ns |  0.5007 ns |  1.45 |    0.82 |         - |          NA |
| U8String.EndsWith           | 65536 | Cjk    |  0.5303 ns |  0.2498 ns | 0.0137 ns |  0.5379 ns |  0.96 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Cjk    |  1.5680 ns |  0.2026 ns | 0.0111 ns |  1.5638 ns |  2.83 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Cjk    |  9.1460 ns |  1.4226 ns | 0.0780 ns |  9.1226 ns | 16.54 |    0.16 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Cjk    |  7.7388 ns | 13.4895 ns | 0.7394 ns |  7.3319 ns | 13.99 |    1.16 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Cjk    |  0.5472 ns |  0.4823 ns | 0.0264 ns |  0.5462 ns |  0.99 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.5371 ns |  0.5675 ns | 0.0311 ns |  0.5264 ns |  0.97 |    0.05 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Cjk    |  0.7988 ns |  8.6385 ns | 0.4735 ns |  0.5322 ns |  1.44 |    0.74 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  1.5655 ns |  0.8193 ns | 0.0449 ns |  1.5514 ns |  2.83 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Cjk    |  9.1891 ns |  0.4753 ns | 0.0261 ns |  9.1755 ns | 16.61 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Cjk    |  7.5312 ns |  5.8488 ns | 0.3206 ns |  7.4338 ns | 13.62 |    0.51 |         - |          NA |
|                             |       |        |            |            |           |            |       |         |           |             |
| **string.EndsWith**             | **65536** | **Mixed**  |  **0.8880 ns** | **11.4557 ns** | **0.6279 ns** |  **0.5337 ns** |  **1.31** |    **1.04** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Mixed  |  0.5131 ns |  0.1075 ns | 0.0059 ns |  0.5122 ns |  0.76 |    0.33 |         - |          NA |
| U8String.EndsWith           | 65536 | Mixed  |  0.6681 ns |  2.5321 ns | 0.1388 ns |  0.6449 ns |  0.99 |    0.47 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Mixed  |  1.9907 ns |  3.2595 ns | 0.1787 ns |  2.0712 ns |  2.94 |    1.30 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Mixed  |  8.4153 ns |  1.3283 ns | 0.0728 ns |  8.4162 ns | 12.42 |    5.41 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Mixed  |  6.6376 ns |  0.1382 ns | 0.0076 ns |  6.6358 ns |  9.79 |    4.26 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Mixed  |  0.5422 ns |  0.4396 ns | 0.0241 ns |  0.5498 ns |  0.80 |    0.35 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.4821 ns |  0.3469 ns | 0.0190 ns |  0.4731 ns |  0.71 |    0.31 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Mixed  |  0.5141 ns |  0.1261 ns | 0.0069 ns |  0.5174 ns |  0.76 |    0.33 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  1.5617 ns |  0.2695 ns | 0.0148 ns |  1.5613 ns |  2.30 |    1.00 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Mixed  |  8.6802 ns |  6.8760 ns | 0.3769 ns |  8.4662 ns | 12.81 |    5.60 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Mixed  |  6.6773 ns |  0.8993 ns | 0.0493 ns |  6.6579 ns |  9.85 |    4.29 |         - |          NA |
