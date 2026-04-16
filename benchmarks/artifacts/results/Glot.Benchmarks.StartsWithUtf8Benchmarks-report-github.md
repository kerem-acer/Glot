```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | N     | Locale | Mean       | Error      | StdDev    | Median     | Ratio     | RatioSD   | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |-----------:|-----------:|----------:|-----------:|----------:|----------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  |  **0.0707 ns** |  **0.4541 ns** | **0.0249 ns** |  **0.0820 ns** |      **1.11** |      **0.56** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  |  0.0495 ns |  0.0607 ns | 0.0033 ns |  0.0478 ns |      0.78 |      0.30 |         - |          NA |
| U8String.StartsWith           | 64    | Ascii  |  0.1699 ns |  0.0265 ns | 0.0014 ns |  0.1693 ns |      2.68 |      1.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  |  1.8538 ns |  0.1115 ns | 0.0061 ns |  1.8528 ns |     29.23 |     11.09 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  | 12.3316 ns |  3.7383 ns | 0.2049 ns | 12.2171 ns |    194.45 |     73.86 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  | 11.0362 ns |  2.7297 ns | 0.1496 ns | 10.9560 ns |    174.02 |     66.08 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  |  0.0745 ns |  0.4520 ns | 0.0248 ns |  0.0743 ns |      1.17 |      0.57 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.2548 ns |  0.0664 ns | 0.0036 ns |  0.2535 ns |      4.02 |      1.53 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Ascii  |  0.2976 ns |  0.3862 ns | 0.0212 ns |  0.2939 ns |      4.69 |      1.81 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  1.8759 ns |  0.1150 ns | 0.0063 ns |  1.8725 ns |     29.58 |     11.23 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  | 12.4881 ns |  1.4649 ns | 0.0803 ns | 12.4671 ns |    196.91 |     74.75 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  | 10.2187 ns |  0.4491 ns | 0.0246 ns | 10.2325 ns |    161.13 |     61.16 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **64**    | **Latin**  |  **0.0549 ns** |  **0.6649 ns** | **0.0364 ns** |  **0.0511 ns** |      **1.45** |      **1.36** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Latin  |  0.0643 ns |  0.0666 ns | 0.0037 ns |  0.0649 ns |      1.69 |      1.11 |         - |          NA |
| U8String.StartsWith           | 64    | Latin  |  0.0859 ns |  0.0141 ns | 0.0008 ns |  0.0862 ns |      2.26 |      1.48 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Latin  |  1.9358 ns |  0.1029 ns | 0.0056 ns |  1.9338 ns |     50.95 |     33.24 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Latin  | 12.3805 ns |  0.5061 ns | 0.0277 ns | 12.3685 ns |    325.83 |    212.59 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Latin  | 10.2663 ns |  0.3345 ns | 0.0183 ns | 10.2563 ns |    270.19 |    176.28 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Latin  |  0.0792 ns |  0.8223 ns | 0.0451 ns |  0.0710 ns |      2.08 |      1.82 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Latin  |  0.0295 ns |  0.0781 ns | 0.0043 ns |  0.0275 ns |      0.78 |      0.52 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Latin  |  0.0605 ns |  0.0189 ns | 0.0010 ns |  0.0604 ns |      1.59 |      1.04 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Latin  |  1.8916 ns |  0.1670 ns | 0.0092 ns |  1.8909 ns |     49.78 |     32.48 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Latin  | 11.8781 ns |  5.6990 ns | 0.3124 ns | 11.7082 ns |    312.61 |    204.13 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Latin  | 11.0068 ns |  2.2070 ns | 0.1210 ns | 10.9413 ns |    289.67 |    189.02 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    |  **0.1361 ns** |  **1.4256 ns** | **0.0781 ns** |  **0.1600 ns** |     **1.441** |      **1.33** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    |  0.0294 ns |  0.1273 ns | 0.0070 ns |  0.0267 ns |     0.311 |      0.23 |         - |          NA |
| U8String.StartsWith           | 64    | Cjk    |  0.0703 ns |  0.1193 ns | 0.0065 ns |  0.0708 ns |     0.745 |      0.53 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    |  1.9127 ns |  0.0509 ns | 0.0028 ns |  1.9143 ns |    20.261 |     14.27 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    | 12.6602 ns |  1.1753 ns | 0.0644 ns | 12.6899 ns |   134.106 |     94.45 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    | 10.9311 ns |  0.3736 ns | 0.0205 ns | 10.9352 ns |   115.790 |     81.54 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    |  0.0522 ns |  0.3385 ns | 0.0186 ns |  0.0557 ns |     0.553 |      0.44 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Cjk    |  0.0556 ns |  0.0230 ns | 0.0013 ns |  0.0551 ns |     0.589 |      0.42 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  1.9361 ns |  0.6055 ns | 0.0332 ns |  1.9181 ns |    20.509 |     14.45 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    | 11.9946 ns |  0.9148 ns | 0.0501 ns | 11.9693 ns |   127.055 |     89.48 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    | 11.0742 ns |  0.1070 ns | 0.0059 ns | 11.0724 ns |   117.306 |     82.61 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **64**    | **Emoji**  |  **0.0764 ns** |  **0.5091 ns** | **0.0279 ns** |  **0.0674 ns** |      **1.09** |      **0.47** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Emoji  |  0.0567 ns |  0.1833 ns | 0.0100 ns |  0.0559 ns |      0.81 |      0.26 |         - |          NA |
| U8String.StartsWith           | 64    | Emoji  |  0.1673 ns |  0.0583 ns | 0.0032 ns |  0.1673 ns |      2.38 |      0.67 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Emoji  |  1.8748 ns |  0.1052 ns | 0.0058 ns |  1.8763 ns |     26.64 |      7.54 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Emoji  | 12.6422 ns |  0.5278 ns | 0.0289 ns | 12.6315 ns |    179.64 |     50.83 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Emoji  | 10.3966 ns |  1.5040 ns | 0.0824 ns | 10.3884 ns |    147.73 |     41.81 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Emoji  |  0.0507 ns |  0.5687 ns | 0.0312 ns |  0.0440 ns |      0.72 |      0.45 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  0.0548 ns |  0.1066 ns | 0.0058 ns |  0.0515 ns |      0.78 |      0.23 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Emoji  |  0.1712 ns |  0.1973 ns | 0.0108 ns |  0.1679 ns |      2.43 |      0.70 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  1.8187 ns |  0.1596 ns | 0.0087 ns |  1.8163 ns |     25.84 |      7.31 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Emoji  | 13.0594 ns |  2.3209 ns | 0.1272 ns | 13.0008 ns |    185.57 |     52.53 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Emoji  | 10.4139 ns |  0.2655 ns | 0.0146 ns | 10.4159 ns |    147.98 |     41.87 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  |  **0.0338 ns** |  **0.4898 ns** | **0.0268 ns** |  **0.0325 ns** |     **2.005** |      **2.58** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  |  0.0469 ns |  0.0538 ns | 0.0029 ns |  0.0468 ns |     2.780 |      2.54 |         - |          NA |
| U8String.StartsWith           | 64    | Mixed  |  0.1030 ns |  0.0937 ns | 0.0051 ns |  0.1009 ns |     6.107 |      5.57 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  |  1.9283 ns |  0.1108 ns | 0.0061 ns |  1.9308 ns |   114.326 |    104.13 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  | 11.5882 ns |  0.4105 ns | 0.0225 ns | 11.5885 ns |   687.055 |    625.76 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  | 10.2313 ns |  0.4423 ns | 0.0242 ns | 10.2425 ns |   606.603 |    552.49 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  |  0.1105 ns |  1.0160 ns | 0.0557 ns |  0.1093 ns |     6.550 |      7.06 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Mixed  |  0.0623 ns |  0.0713 ns | 0.0039 ns |  0.0605 ns |     3.694 |      3.38 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  1.9132 ns |  0.1169 ns | 0.0064 ns |  1.9108 ns |   113.430 |    103.31 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  | 11.8151 ns |  5.8282 ns | 0.3195 ns | 11.7407 ns |   700.504 |    638.37 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  | 10.8176 ns |  0.5088 ns | 0.0279 ns | 10.8091 ns |   641.364 |    584.15 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **256**   | **Ascii**  |  **0.0913 ns** |  **0.1365 ns** | **0.0075 ns** |  **0.0925 ns** |      **1.00** |      **0.10** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Ascii  |  0.0791 ns |  0.1060 ns | 0.0058 ns |  0.0800 ns |      0.87 |      0.08 |         - |          NA |
| U8String.StartsWith           | 256   | Ascii  |  0.1663 ns |  0.0517 ns | 0.0028 ns |  0.1677 ns |      1.83 |      0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Ascii  |  1.8467 ns |  0.1138 ns | 0.0062 ns |  1.8446 ns |     20.33 |      1.48 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Ascii  | 11.6661 ns |  1.2587 ns | 0.0690 ns | 11.6577 ns |    128.42 |      9.35 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Ascii  | 10.2624 ns |  0.6477 ns | 0.0355 ns | 10.2718 ns |    112.97 |      8.21 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Ascii  |  0.1077 ns |  1.2369 ns | 0.0678 ns |  0.1363 ns |      1.19 |      0.65 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Ascii  |  0.2535 ns |  0.2157 ns | 0.0118 ns |  0.2511 ns |      2.79 |      0.23 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 256   | Ascii  |  0.2900 ns |  0.1027 ns | 0.0056 ns |  0.2893 ns |      3.19 |      0.24 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Ascii  |  1.8911 ns |  0.3018 ns | 0.0165 ns |  1.8872 ns |     20.82 |      1.52 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Ascii  | 11.6157 ns |  0.5650 ns | 0.0310 ns | 11.6289 ns |    127.86 |      9.29 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Ascii  | 10.9910 ns |  0.4426 ns | 0.0243 ns | 10.9771 ns |    120.99 |      8.79 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **256**   | **Latin**  |  **0.0560 ns** |  **0.4415 ns** | **0.0242 ns** |  **0.0542 ns** |     **1.145** |      **0.64** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 256   | Latin  |  0.1154 ns |  0.1820 ns | 0.0100 ns |  0.1137 ns |     2.359 |      0.94 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Latin  |  1.8989 ns |  0.0755 ns | 0.0041 ns |  1.8965 ns |    38.818 |     15.23 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Latin  | 12.3795 ns |  0.2745 ns | 0.0150 ns | 12.3862 ns |   253.066 |     99.30 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Latin  | 10.9444 ns |  2.5307 ns | 0.1387 ns | 10.9609 ns |   223.729 |     87.83 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Latin  |  0.1010 ns |  1.6673 ns | 0.0914 ns |  0.0654 ns |     2.064 |      1.91 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 256   | Latin  |  0.1341 ns |  0.0204 ns | 0.0011 ns |  0.1341 ns |     2.741 |      1.08 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Latin  |  1.8969 ns |  0.0880 ns | 0.0048 ns |  1.8987 ns |    38.776 |     15.22 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Latin  | 11.6131 ns |  0.2836 ns | 0.0155 ns | 11.6168 ns |   237.399 |     93.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Latin  | 10.9464 ns |  0.2509 ns | 0.0138 ns | 10.9410 ns |   223.770 |     87.80 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **256**   | **Cjk**    |  **0.1051 ns** |  **0.9090 ns** | **0.0498 ns** |  **0.1232 ns** |     **1.248** |      **0.90** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Cjk    |  0.0576 ns |  0.0050 ns | 0.0003 ns |  0.0575 ns |     0.684 |      0.38 |         - |          NA |
| U8String.StartsWith           | 256   | Cjk    |  0.1102 ns |  0.0141 ns | 0.0008 ns |  0.1105 ns |     1.309 |      0.72 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Cjk    |  1.9147 ns |  0.3298 ns | 0.0181 ns |  1.9111 ns |    22.743 |     12.47 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Cjk    | 12.5476 ns |  0.4789 ns | 0.0262 ns | 12.5426 ns |   149.036 |     81.73 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Cjk    | 10.4165 ns |  0.5112 ns | 0.0280 ns | 10.4289 ns |   123.724 |     67.85 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Cjk    |  0.1126 ns |  0.4980 ns | 0.0273 ns |  0.1207 ns |     1.338 |      0.80 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Cjk    |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 256   | Cjk    |  0.0758 ns |  0.0188 ns | 0.0010 ns |  0.0763 ns |     0.901 |      0.49 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Cjk    |  1.8766 ns |  0.0359 ns | 0.0020 ns |  1.8763 ns |    22.289 |     12.22 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Cjk    | 11.9297 ns |  0.2498 ns | 0.0137 ns | 11.9229 ns |   141.696 |     77.70 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Cjk    | 11.0077 ns |  3.1969 ns | 0.1752 ns | 10.9238 ns |   130.745 |     71.72 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **256**   | **Emoji**  |  **0.0663 ns** |  **0.6582 ns** | **0.0361 ns** |  **0.0492 ns** |      **1.18** |      **0.73** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Emoji  |  0.0449 ns |  0.0380 ns | 0.0021 ns |  0.0439 ns |      0.80 |      0.30 |         - |          NA |
| U8String.StartsWith           | 256   | Emoji  |  0.1679 ns |  0.0346 ns | 0.0019 ns |  0.1690 ns |      2.99 |      1.11 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Emoji  |  1.8463 ns |  0.2236 ns | 0.0123 ns |  1.8490 ns |     32.92 |     12.17 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Emoji  | 12.5238 ns |  0.3785 ns | 0.0207 ns | 12.5302 ns |    223.29 |     82.53 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Emoji  | 10.3354 ns |  0.1980 ns | 0.0109 ns | 10.3308 ns |    184.27 |     68.10 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Emoji  |  0.0944 ns |  0.8591 ns | 0.0471 ns |  0.0900 ns |      1.68 |      0.99 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Emoji  |  0.0527 ns |  0.0368 ns | 0.0020 ns |  0.0527 ns |      0.94 |      0.35 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 256   | Emoji  |  0.1676 ns |  0.0258 ns | 0.0014 ns |  0.1669 ns |      2.99 |      1.10 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Emoji  |  1.8327 ns |  0.1592 ns | 0.0087 ns |  1.8351 ns |     32.68 |     12.08 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Emoji  | 12.3515 ns |  0.3857 ns | 0.0211 ns | 12.3624 ns |    220.22 |     81.39 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Emoji  | 11.1638 ns |  0.5367 ns | 0.0294 ns | 11.1562 ns |    199.04 |     73.56 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **256**   | **Mixed**  |  **0.1049 ns** |  **0.7665 ns** | **0.0420 ns** |  **0.1275 ns** |     **1.161** |      **0.68** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 256   | Mixed  |  0.1384 ns |  0.0796 ns | 0.0044 ns |  0.1366 ns |     1.532 |      0.69 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Mixed  |  1.9027 ns |  0.2209 ns | 0.0121 ns |  1.9034 ns |    21.071 |      9.50 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Mixed  | 12.2380 ns |  0.6953 ns | 0.0381 ns | 12.2201 ns |   135.528 |     61.11 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Mixed  | 10.2284 ns |  0.3753 ns | 0.0206 ns | 10.2392 ns |   113.274 |     51.07 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Mixed  |  0.0588 ns |  0.5359 ns | 0.0294 ns |  0.0696 ns |     0.652 |      0.42 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Mixed  |  0.0236 ns |  0.0950 ns | 0.0052 ns |  0.0258 ns |     0.262 |      0.13 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 256   | Mixed  |  0.0816 ns |  0.0137 ns | 0.0008 ns |  0.0814 ns |     0.904 |      0.41 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Mixed  |  1.9110 ns |  0.0243 ns | 0.0013 ns |  1.9115 ns |    21.163 |      9.54 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Mixed  | 11.5895 ns |  0.6723 ns | 0.0368 ns | 11.5870 ns |   128.347 |     57.87 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Mixed  | 10.1869 ns |  0.5013 ns | 0.0275 ns | 10.1954 ns |   112.814 |     50.87 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  |  **0.0196 ns** |  **0.1631 ns** | **0.0089 ns** |  **0.0231 ns** |      **1.22** |      **0.84** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  |  0.0416 ns |  0.0585 ns | 0.0032 ns |  0.0417 ns |      2.59 |      1.37 |         - |          NA |
| U8String.StartsWith           | 4096  | Ascii  |  0.1639 ns |  0.0231 ns | 0.0013 ns |  0.1633 ns |     10.21 |      5.34 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  |  1.8611 ns |  0.7849 ns | 0.0430 ns |  1.8369 ns |    115.88 |     60.61 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  | 12.3427 ns |  0.2691 ns | 0.0147 ns | 12.3458 ns |    768.51 |    401.63 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  | 10.9428 ns |  0.0950 ns | 0.0052 ns | 10.9431 ns |    681.35 |    356.08 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  |  0.0138 ns |  0.1515 ns | 0.0083 ns |  0.0116 ns |      0.86 |      0.67 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  0.2630 ns |  0.1339 ns | 0.0073 ns |  0.2654 ns |     16.38 |      8.57 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Ascii  |  0.2860 ns |  0.1783 ns | 0.0098 ns |  0.2825 ns |     17.81 |      9.33 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  1.9155 ns |  0.2681 ns | 0.0147 ns |  1.9133 ns |    119.26 |     62.33 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  | 11.5579 ns |  0.1603 ns | 0.0088 ns | 11.5536 ns |    719.64 |    376.09 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  | 11.2244 ns |  1.0194 ns | 0.0559 ns | 11.1933 ns |    698.88 |    365.26 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **4096**  | **Latin**  |  **0.0174 ns** |  **0.0389 ns** | **0.0021 ns** |  **0.0172 ns** |     **1.010** |      **0.15** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 4096  | Latin  |  0.0833 ns |  0.0303 ns | 0.0017 ns |  0.0825 ns |     4.846 |      0.52 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Latin  |  1.9152 ns |  0.0907 ns | 0.0050 ns |  1.9171 ns |   111.468 |     11.82 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Latin  | 12.2391 ns |  1.9332 ns | 0.1060 ns | 12.2958 ns |   712.337 |     75.73 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Latin  | 10.8817 ns |  0.5497 ns | 0.0301 ns | 10.8871 ns |   633.333 |     67.18 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Latin  |  0.0191 ns |  0.1399 ns | 0.0077 ns |  0.0171 ns |     1.114 |      0.41 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Latin  |  0.0592 ns |  0.0317 ns | 0.0017 ns |  0.0593 ns |     3.446 |      0.38 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Latin  |  1.9031 ns |  0.0631 ns | 0.0035 ns |  1.9049 ns |   110.765 |     11.75 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Latin  | 11.7104 ns |  1.0465 ns | 0.0574 ns | 11.7132 ns |   681.565 |     72.33 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Latin  | 11.2321 ns |  0.2465 ns | 0.0135 ns | 11.2367 ns |   653.728 |     69.33 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    |  **0.0212 ns** |  **0.0911 ns** | **0.0050 ns** |  **0.0195 ns** |      **1.03** |      **0.29** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    |  0.0412 ns |  0.0074 ns | 0.0004 ns |  0.0415 ns |      2.01 |      0.37 |         - |          NA |
| U8String.StartsWith           | 4096  | Cjk    |  0.1103 ns |  0.0211 ns | 0.0012 ns |  0.1097 ns |      5.37 |      1.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    |  1.9169 ns |  0.1087 ns | 0.0060 ns |  1.9158 ns |     93.36 |     17.35 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    | 12.7131 ns |  0.4385 ns | 0.0240 ns | 12.7158 ns |    619.17 |    115.07 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    | 10.6927 ns |  2.4781 ns | 0.1358 ns | 10.6666 ns |    520.77 |     96.95 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    |  0.0323 ns |  0.3904 ns | 0.0214 ns |  0.0211 ns |      1.57 |      0.96 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.0048 ns |  0.0838 ns | 0.0046 ns |  0.0051 ns |      0.23 |      0.20 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Cjk    |  0.0615 ns |  0.0941 ns | 0.0052 ns |  0.0598 ns |      3.00 |      0.60 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  1.8836 ns |  0.0386 ns | 0.0021 ns |  1.8848 ns |     91.74 |     17.05 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    | 12.0491 ns |  5.3040 ns | 0.2907 ns | 11.8855 ns |    586.83 |    109.76 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    | 13.5435 ns | 13.2284 ns | 0.7251 ns | 13.3574 ns |    659.62 |    126.45 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **4096**  | **Emoji**  |  **0.0000 ns** |  **0.0000 ns** | **0.0000 ns** |  **0.0000 ns** |         **?** |         **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Emoji  |  0.0015 ns |  0.0478 ns | 0.0026 ns |  0.0000 ns |         ? |         ? |         - |           ? |
| U8String.StartsWith           | 4096  | Emoji  |  0.1136 ns |  0.0328 ns | 0.0018 ns |  0.1141 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Emoji  |  1.7963 ns |  0.1784 ns | 0.0098 ns |  1.8008 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Emoji  | 12.9699 ns |  0.4696 ns | 0.0257 ns | 12.9744 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Emoji  | 10.9445 ns |  0.3919 ns | 0.0215 ns | 10.9541 ns |         ? |         ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Emoji  |  0.0011 ns |  0.0337 ns | 0.0018 ns |  0.0000 ns |         ? |         ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  0.0240 ns |  0.0397 ns | 0.0022 ns |  0.0252 ns |         ? |         ? |         - |           ? |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Emoji  |  0.1565 ns |  0.2848 ns | 0.0156 ns |  0.1589 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  1.7866 ns |  0.1274 ns | 0.0070 ns |  1.7896 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Emoji  | 12.4303 ns |  1.2064 ns | 0.0661 ns | 12.3956 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Emoji  | 10.6057 ns |  1.9631 ns | 0.1076 ns | 10.5720 ns |         ? |         ? |         - |           ? |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  |  **0.0362 ns** |  **0.6358 ns** | **0.0349 ns** |  **0.0310 ns** |     **3.429** |      **5.74** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 4096  | Mixed  |  0.0873 ns |  0.2222 ns | 0.0122 ns |  0.0820 ns |     8.274 |      9.55 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  |  1.8821 ns |  0.0516 ns | 0.0028 ns |  1.8830 ns |   178.334 |    203.34 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  | 11.6468 ns |  1.0868 ns | 0.0596 ns | 11.6249 ns | 1,103.567 |  1,258.31 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  | 11.1374 ns |  0.6183 ns | 0.0339 ns | 11.1374 ns | 1,055.294 |  1,203.26 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  |  0.0022 ns |  0.0550 ns | 0.0030 ns |  0.0011 ns |     0.212 |      0.44 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Mixed  |  0.0667 ns |  0.5280 ns | 0.0289 ns |  0.0503 ns |     6.317 |      8.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  1.8553 ns |  0.1757 ns | 0.0096 ns |  1.8531 ns |   175.797 |    200.45 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  | 12.5570 ns |  2.9405 ns | 0.1612 ns | 12.5239 ns | 1,189.805 |  1,356.76 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  | 10.7644 ns |  6.8945 ns | 0.3779 ns | 10.5530 ns | 1,019.957 |  1,163.85 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  |  **0.0310 ns** |  **0.4501 ns** | **0.0247 ns** |  **0.0205 ns** |      **1.45** |      **1.37** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  |  0.0086 ns |  0.0311 ns | 0.0017 ns |  0.0093 ns |      0.40 |      0.23 |         - |          NA |
| U8String.StartsWith           | 65536 | Ascii  |  0.1237 ns |  0.0915 ns | 0.0050 ns |  0.1221 ns |      5.81 |      3.13 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  |  1.8740 ns |  0.1165 ns | 0.0064 ns |  1.8728 ns |     87.93 |     47.24 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  | 12.1154 ns |  1.4033 ns | 0.0769 ns | 12.1044 ns |    568.49 |    305.39 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  | 10.8639 ns |  0.7395 ns | 0.0405 ns | 10.8586 ns |    509.77 |    273.83 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  |  0.0190 ns |  0.0966 ns | 0.0053 ns |  0.0214 ns |      0.89 |      0.54 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.2864 ns |  1.0921 ns | 0.0599 ns |  0.2559 ns |     13.44 |      7.72 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Ascii  |  0.2531 ns |  0.0303 ns | 0.0017 ns |  0.2535 ns |     11.88 |      6.38 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  1.9351 ns |  0.1125 ns | 0.0062 ns |  1.9317 ns |     90.80 |     48.78 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  | 12.1998 ns |  0.1595 ns | 0.0087 ns | 12.1988 ns |    572.45 |    307.49 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  | 11.2529 ns |  3.4930 ns | 0.1915 ns | 11.1593 ns |    528.02 |    283.76 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **65536** | **Latin**  |  **0.0101 ns** |  **0.1545 ns** | **0.0085 ns** |  **0.0133 ns** |     **7.902** |     **14.34** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 65536 | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Latin  |  2.0062 ns |  0.0306 ns | 0.0017 ns |  2.0061 ns | 1,574.664 |  2,157.56 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Latin  | 12.2370 ns |  0.4360 ns | 0.0239 ns | 12.2374 ns | 9,605.001 | 13,160.55 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Latin  | 11.4746 ns |  0.5303 ns | 0.0291 ns | 11.4834 ns | 9,006.588 | 12,340.63 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Latin  |  0.0083 ns |  0.0833 ns | 0.0046 ns |  0.0060 ns |     6.516 |     10.27 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Latin  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Latin  |  1.9577 ns |  0.0547 ns | 0.0030 ns |  1.9586 ns | 1,536.605 |  2,105.42 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Latin  | 12.1905 ns |  1.4541 ns | 0.0797 ns | 12.1527 ns | 9,568.470 | 13,110.76 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Latin  | 11.6450 ns |  0.8496 ns | 0.0466 ns | 11.6606 ns | 9,140.293 | 12,523.90 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    |  **0.0150 ns** |  **0.4750 ns** | **0.0260 ns** |  **0.0000 ns** |         **?** |         **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |         ? |         ? |         - |           ? |
| U8String.StartsWith           | 65536 | Cjk    |  0.0292 ns |  0.2829 ns | 0.0155 ns |  0.0210 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    |  1.9758 ns |  0.2994 ns | 0.0164 ns |  1.9714 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    | 12.6027 ns |  1.1014 ns | 0.0604 ns | 12.6209 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    | 11.0731 ns |  0.6091 ns | 0.0334 ns | 11.0902 ns |         ? |         ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    |  0.0298 ns |  0.0992 ns | 0.0054 ns |  0.0305 ns |         ? |         ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |         ? |         ? |         - |           ? |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Cjk    |  0.0028 ns |  0.0335 ns | 0.0018 ns |  0.0038 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  2.0025 ns |  0.2571 ns | 0.0141 ns |  2.0052 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 13.2683 ns |  0.5653 ns | 0.0310 ns | 13.2797 ns |         ? |         ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    | 11.1630 ns |  0.4390 ns | 0.0241 ns | 11.1636 ns |         ? |         ? |         - |           ? |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **65536** | **Emoji**  |  **0.0206 ns** |  **0.1502 ns** | **0.0082 ns** |  **0.0235 ns** |      **1.15** |      **0.66** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Emoji  |  0.0003 ns |  0.0080 ns | 0.0004 ns |  0.0000 ns |      0.01 |      0.02 |         - |          NA |
| U8String.StartsWith           | 65536 | Emoji  |  0.1106 ns |  0.0931 ns | 0.0051 ns |  0.1130 ns |      6.20 |      2.72 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Emoji  |  1.9358 ns |  0.1325 ns | 0.0073 ns |  1.9381 ns |    108.44 |     47.34 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Emoji  | 13.6479 ns |  2.1848 ns | 0.1198 ns | 13.6844 ns |    764.55 |    333.81 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Emoji  | 10.9984 ns |  0.7253 ns | 0.0398 ns | 10.9870 ns |    616.13 |    268.97 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Emoji  |  0.0352 ns |  0.4198 ns | 0.0230 ns |  0.0227 ns |      1.97 |      1.48 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  0.0042 ns |  0.1329 ns | 0.0073 ns |  0.0000 ns |      0.24 |      0.40 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Emoji  |  0.1439 ns |  0.1652 ns | 0.0091 ns |  0.1420 ns |      8.06 |      3.55 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  1.8867 ns |  0.0903 ns | 0.0050 ns |  1.8874 ns |    105.69 |     46.14 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Emoji  | 13.2154 ns |  1.4560 ns | 0.0798 ns | 13.2296 ns |    740.32 |    323.20 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Emoji  | 11.3216 ns |  1.2683 ns | 0.0695 ns | 11.3014 ns |    634.23 |    276.89 |         - |          NA |
|                               |       |        |            |            |           |            |           |           |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  |  **0.0839 ns** |  **0.4831 ns** | **0.0265 ns** |  **0.0934 ns** |     **1.086** |      **0.47** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| U8String.StartsWith           | 65536 | Mixed  |  0.0591 ns |  0.0351 ns | 0.0019 ns |  0.0582 ns |     0.766 |      0.25 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  |  1.9685 ns |  0.2185 ns | 0.0120 ns |  1.9716 ns |    25.481 |      8.31 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  | 12.5227 ns |  3.3317 ns | 0.1826 ns | 12.4248 ns |   162.101 |     52.93 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  | 10.5239 ns |  0.7380 ns | 0.0405 ns | 10.5084 ns |   136.228 |     44.45 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  |  0.0356 ns |  0.7254 ns | 0.0398 ns |  0.0193 ns |     0.461 |      0.49 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.0000 ns |  0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |      0.00 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Mixed  |  0.0060 ns |  0.1896 ns | 0.0104 ns |  0.0000 ns |     0.078 |      0.12 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  1.8482 ns |  0.6237 ns | 0.0342 ns |  1.8289 ns |    23.924 |      7.82 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  | 11.6777 ns |  0.2045 ns | 0.0112 ns | 11.6745 ns |   151.164 |     49.32 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  | 10.4205 ns |  2.6962 ns | 0.1478 ns | 10.3619 ns |   134.890 |     44.04 |         - |          NA |
