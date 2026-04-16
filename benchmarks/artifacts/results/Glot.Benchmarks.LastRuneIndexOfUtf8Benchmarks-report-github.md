```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean         | Error         | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|--------------:|-----------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.288 ns** |     **0.2446 ns** |  **0.0134 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.240 ns |     0.0334 ns |  0.0018 ns |     0.52 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 64    | Ascii  |     2.404 ns |     0.0098 ns |  0.0005 ns |     0.56 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Ascii  |     7.923 ns |     1.5882 ns |  0.0871 ns |     1.85 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Ascii  |    14.629 ns |     0.2687 ns |  0.0147 ns |     3.41 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Ascii  |    15.147 ns |     0.3395 ns |  0.0186 ns |     3.53 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     5.086 ns |     1.2235 ns |  0.0671 ns |     1.19 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.031 ns |     0.1252 ns |  0.0069 ns |     0.47 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Ascii  |     2.294 ns |     0.7161 ns |  0.0393 ns |     0.53 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.824 ns |     0.1400 ns |  0.0077 ns |     0.89 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Ascii  |    11.614 ns |     1.1488 ns |  0.0630 ns |     2.71 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Ascii  |    11.475 ns |     1.1395 ns |  0.0625 ns |     2.68 |    0.01 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Latin**  |     **5.072 ns** |     **0.3518 ns** |  **0.0193 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Latin  |     2.640 ns |     1.4293 ns |  0.0783 ns |     0.52 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Latin  |     2.651 ns |     0.3864 ns |  0.0212 ns |     0.52 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Latin  |     4.240 ns |     0.0346 ns |  0.0019 ns |     0.84 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Latin  |    16.979 ns |     0.3721 ns |  0.0204 ns |     3.35 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Latin  |    31.607 ns |     3.7366 ns |  0.2048 ns |     6.23 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Latin  |     5.208 ns |     0.3218 ns |  0.0176 ns |     1.03 |    0.00 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Latin  |     2.595 ns |     0.0284 ns |  0.0016 ns |     0.51 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Latin  |     2.681 ns |     0.8804 ns |  0.0483 ns |     0.53 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Latin  |     4.252 ns |     0.0812 ns |  0.0045 ns |     0.84 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Latin  |    16.441 ns |     0.4856 ns |  0.0266 ns |     3.24 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Latin  |    30.313 ns |     0.6118 ns |  0.0335 ns |     5.98 |    0.02 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.103 ns** |     **0.0909 ns** |  **0.0050 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.409 ns |     0.0499 ns |  0.0027 ns |     0.67 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 64    | Cjk    |     1.655 ns |     0.6232 ns |  0.0342 ns |     0.79 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Cjk    |    14.291 ns |     1.8611 ns |  0.1020 ns |     6.80 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Cjk    |    34.586 ns |     5.8317 ns |  0.3197 ns |    16.45 |    0.14 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Cjk    |    31.147 ns |     3.6722 ns |  0.2013 ns |    14.81 |    0.09 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     5.336 ns |     3.0070 ns |  0.1648 ns |     2.54 |    0.07 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.897 ns |     0.9832 ns |  0.0539 ns |     2.80 |    0.02 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Cjk    |     5.985 ns |     0.4742 ns |  0.0260 ns |     2.85 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     7.014 ns |     0.6166 ns |  0.0338 ns |     3.34 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Cjk    |    19.399 ns |     2.0940 ns |  0.1148 ns |     9.22 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Cjk    |    25.762 ns |     1.9545 ns |  0.1071 ns |    12.25 |    0.05 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Emoji**  |     **3.099 ns** |     **0.9730 ns** |  **0.0533 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Emoji  |     2.276 ns |     0.0320 ns |  0.0018 ns |     0.73 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 64    | Emoji  |     2.440 ns |     0.6525 ns |  0.0358 ns |     0.79 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Emoji  |    11.084 ns |     1.0072 ns |  0.0552 ns |     3.58 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Emoji  |    21.562 ns |     0.8570 ns |  0.0470 ns |     6.96 |    0.10 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Emoji  |    27.385 ns |     2.5446 ns |  0.1395 ns |     8.84 |    0.14 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Emoji  |     5.043 ns |     1.4103 ns |  0.0773 ns |     1.63 |    0.03 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Emoji  |     4.577 ns |     0.9256 ns |  0.0507 ns |     1.48 |    0.03 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Emoji  |     4.637 ns |     0.1573 ns |  0.0086 ns |     1.50 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     6.961 ns |     0.2031 ns |  0.0111 ns |     2.25 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Emoji  |    19.059 ns |     0.6175 ns |  0.0338 ns |     6.15 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Emoji  |    24.931 ns |     1.4497 ns |  0.0795 ns |     8.05 |    0.12 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.294 ns** |     **0.0889 ns** |  **0.0049 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.541 ns |     0.1146 ns |  0.0063 ns |     0.59 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 64    | Mixed  |     2.780 ns |     0.0485 ns |  0.0027 ns |     0.65 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 64    | Mixed  |     9.393 ns |     1.9337 ns |  0.1060 ns |     2.19 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 64    | Mixed  |    15.877 ns |     0.4525 ns |  0.0248 ns |     3.70 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 64    | Mixed  |    15.785 ns |     0.7512 ns |  0.0412 ns |     3.68 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     5.017 ns |     0.3525 ns |  0.0193 ns |     1.17 |    0.00 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.740 ns |     1.3653 ns |  0.0748 ns |     0.64 |    0.02 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 64    | Mixed  |     2.636 ns |     0.0605 ns |  0.0033 ns |     0.61 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     4.224 ns |     0.0230 ns |  0.0013 ns |     0.98 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 64    | Mixed  |    16.848 ns |     1.4384 ns |  0.0788 ns |     3.92 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 64    | Mixed  |    23.678 ns |     1.4820 ns |  0.0812 ns |     5.51 |    0.02 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **256**   | **Ascii**  |     **2.708 ns** |     **0.1602 ns** |  **0.0088 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 256   | Ascii  |     1.540 ns |     0.0928 ns |  0.0051 ns |     0.57 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 256   | Ascii  |     1.695 ns |     0.4400 ns |  0.0241 ns |     0.63 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 256   | Ascii  |    17.284 ns |     6.4069 ns |  0.3512 ns |     6.38 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 256   | Ascii  |    24.755 ns |     1.4740 ns |  0.0808 ns |     9.14 |    0.04 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 256   | Ascii  |    26.595 ns |     2.5922 ns |  0.1421 ns |     9.82 |    0.05 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 256   | Ascii  |    16.858 ns |     0.3261 ns |  0.0179 ns |     6.23 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 256   | Ascii  |     8.258 ns |     0.7745 ns |  0.0425 ns |     3.05 |    0.02 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 256   | Ascii  |     8.262 ns |     0.8654 ns |  0.0474 ns |     3.05 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 256   | Ascii  |     9.510 ns |     0.6038 ns |  0.0331 ns |     3.51 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 256   | Ascii  |    16.702 ns |     0.6113 ns |  0.0335 ns |     6.17 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 256   | Ascii  |    17.178 ns |     1.5442 ns |  0.0846 ns |     6.34 |    0.03 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **256**   | **Latin**  |    **16.819 ns** |     **1.0940 ns** |  **0.0600 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 256   | Latin  |     9.590 ns |     0.0491 ns |  0.0027 ns |     0.57 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 256   | Latin  |     9.634 ns |     0.1500 ns |  0.0082 ns |     0.57 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 256   | Latin  |    10.903 ns |     1.6013 ns |  0.0878 ns |     0.65 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 256   | Latin  |    20.980 ns |     6.0996 ns |  0.3343 ns |     1.25 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 256   | Latin  |    37.268 ns |     1.9494 ns |  0.1069 ns |     2.22 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 256   | Latin  |    16.821 ns |     2.2574 ns |  0.1237 ns |     1.00 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 256   | Latin  |     9.599 ns |     0.4438 ns |  0.0243 ns |     0.57 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 256   | Latin  |     9.628 ns |     0.4312 ns |  0.0236 ns |     0.57 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 256   | Latin  |    10.885 ns |     0.4374 ns |  0.0240 ns |     0.65 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 256   | Latin  |    22.456 ns |     3.6604 ns |  0.2006 ns |     1.34 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 256   | Latin  |    36.572 ns |     4.2012 ns |  0.2303 ns |     2.17 |    0.01 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **256**   | **Cjk**    |     **2.490 ns** |     **0.1492 ns** |  **0.0082 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 256   | Cjk    |     2.215 ns |     0.4250 ns |  0.0233 ns |     0.89 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 256   | Cjk    |     2.406 ns |     0.2993 ns |  0.0164 ns |     0.97 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 256   | Cjk    |    32.822 ns |     2.5823 ns |  0.1415 ns |    13.18 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 256   | Cjk    |    52.384 ns |     6.2030 ns |  0.3400 ns |    21.04 |    0.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 256   | Cjk    |    49.780 ns |     1.2211 ns |  0.0669 ns |    20.00 |    0.06 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 256   | Cjk    |    16.759 ns |     0.8226 ns |  0.0451 ns |     6.73 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 256   | Cjk    |    23.878 ns |     1.6750 ns |  0.0918 ns |     9.59 |    0.04 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 256   | Cjk    |    23.940 ns |     2.8123 ns |  0.1542 ns |     9.62 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 256   | Cjk    |    25.142 ns |     0.2977 ns |  0.0163 ns |    10.10 |    0.03 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 256   | Cjk    |    36.746 ns |     3.6818 ns |  0.2018 ns |    14.76 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 256   | Cjk    |    42.228 ns |     0.8679 ns |  0.0476 ns |    16.96 |    0.05 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **256**   | **Emoji**  |     **3.046 ns** |     **0.1151 ns** |  **0.0063 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 256   | Emoji  |     1.884 ns |     0.0706 ns |  0.0039 ns |     0.62 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 256   | Emoji  |     2.042 ns |     0.1227 ns |  0.0067 ns |     0.67 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 256   | Emoji  |    24.764 ns |     0.8135 ns |  0.0446 ns |     8.13 |    0.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 256   | Emoji  |    34.423 ns |     6.7490 ns |  0.3699 ns |    11.30 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 256   | Emoji  |    40.974 ns |     6.2593 ns |  0.3431 ns |    13.45 |    0.10 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 256   | Emoji  |    16.777 ns |     0.6598 ns |  0.0362 ns |     5.51 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 256   | Emoji  |    23.265 ns |     0.5470 ns |  0.0300 ns |     7.64 |    0.02 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 256   | Emoji  |    18.912 ns |     0.3807 ns |  0.0209 ns |     6.21 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 256   | Emoji  |    25.241 ns |     5.5010 ns |  0.3015 ns |     8.29 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 256   | Emoji  |    36.423 ns |     6.6725 ns |  0.3657 ns |    11.96 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 256   | Emoji  |    39.054 ns |     1.9128 ns |  0.1048 ns |    12.82 |    0.04 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **256**   | **Mixed**  |     **2.360 ns** |     **0.4980 ns** |  **0.0273 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 256   | Mixed  |     1.424 ns |     0.0379 ns |  0.0021 ns |     0.60 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 256   | Mixed  |     1.640 ns |     0.1629 ns |  0.0089 ns |     0.70 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 256   | Mixed  |    17.937 ns |     0.4744 ns |  0.0260 ns |     7.60 |    0.08 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 256   | Mixed  |    23.404 ns |     1.8458 ns |  0.1012 ns |     9.92 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 256   | Mixed  |    23.506 ns |     4.0080 ns |  0.2197 ns |     9.96 |    0.13 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 256   | Mixed  |    17.199 ns |     0.8763 ns |  0.0480 ns |     7.29 |    0.07 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 256   | Mixed  |    10.617 ns |     0.7159 ns |  0.0392 ns |     4.50 |    0.05 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 256   | Mixed  |    10.623 ns |     1.1112 ns |  0.0609 ns |     4.50 |    0.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 256   | Mixed  |    11.894 ns |     1.1748 ns |  0.0644 ns |     5.04 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 256   | Mixed  |    23.858 ns |     2.6720 ns |  0.1465 ns |    10.11 |    0.11 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 256   | Mixed  |    29.003 ns |     2.1160 ns |  0.1160 ns |    12.29 |    0.13 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.338 ns** |     **0.0385 ns** |  **0.0021 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.531 ns |     0.1071 ns |  0.0059 ns |     0.65 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Ascii  |     1.713 ns |     0.2131 ns |  0.0117 ns |     0.73 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Ascii  |   143.763 ns |    23.0863 ns |  1.2654 ns |    61.50 |    0.47 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Ascii  |   150.929 ns |    14.5942 ns |  0.8000 ns |    64.56 |    0.30 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Ascii  |   150.616 ns |     8.4996 ns |  0.4659 ns |    64.43 |    0.18 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   255.796 ns |    21.1293 ns |  1.1582 ns |   109.42 |    0.44 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   134.254 ns |    32.6933 ns |  1.7920 ns |    57.43 |    0.67 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Ascii  |   130.854 ns |     1.8005 ns |  0.0987 ns |    55.98 |    0.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   132.725 ns |    15.9701 ns |  0.8754 ns |    56.78 |    0.33 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   139.421 ns |     3.8952 ns |  0.2135 ns |    59.64 |    0.09 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   139.524 ns |     6.3724 ns |  0.3493 ns |    59.68 |    0.14 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Latin**  |   **254.917 ns** |    **17.1527 ns** |  **0.9402 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Latin  |   154.935 ns |    11.4677 ns |  0.6286 ns |     0.61 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Latin  |   156.252 ns |     7.2517 ns |  0.3975 ns |     0.61 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Latin  |   157.865 ns |    17.4337 ns |  0.9556 ns |     0.62 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Latin  |   172.951 ns |     8.1398 ns |  0.4462 ns |     0.68 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Latin  |   187.216 ns |    22.6539 ns |  1.2417 ns |     0.73 |    0.00 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Latin  |   255.611 ns |    19.8181 ns |  1.0863 ns |     1.00 |    0.00 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Latin  |   155.218 ns |     2.6479 ns |  0.1451 ns |     0.61 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Latin  |   157.634 ns |    38.0534 ns |  2.0858 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Latin  |   158.512 ns |    54.3447 ns |  2.9788 ns |     0.62 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Latin  |   138.859 ns |    20.0542 ns |  1.0992 ns |     0.54 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Latin  |   186.670 ns |     8.3703 ns |  0.4588 ns |     0.73 |    0.00 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.103 ns** |     **0.1097 ns** |  **0.0060 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.815 ns |     0.1712 ns |  0.0094 ns |     0.86 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Cjk    |     2.049 ns |     0.0430 ns |  0.0024 ns |     0.97 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Cjk    |   433.039 ns |    54.4683 ns |  2.9856 ns |   205.92 |    1.33 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Cjk    |   456.006 ns |    51.4072 ns |  2.8178 ns |   216.84 |    1.28 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Cjk    |   440.324 ns |    41.9853 ns |  2.3014 ns |   209.38 |    1.08 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   258.798 ns |    80.9177 ns |  4.4354 ns |   123.06 |    1.85 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   378.848 ns |    66.0446 ns |  3.6201 ns |   180.15 |    1.56 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Cjk    |   378.989 ns |    88.6615 ns |  4.8598 ns |   180.22 |    2.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   381.088 ns |    19.2452 ns |  1.0549 ns |   181.21 |    0.62 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   394.872 ns |   106.4811 ns |  5.8366 ns |   187.77 |    2.45 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   410.840 ns |     6.6975 ns |  0.3671 ns |   195.36 |    0.51 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Emoji**  |     **2.799 ns** |     **0.2150 ns** |  **0.0118 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Emoji  |     1.903 ns |     0.6816 ns |  0.0374 ns |     0.68 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Emoji  |     2.094 ns |     0.4203 ns |  0.0230 ns |     0.75 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Emoji  |   293.531 ns |    92.7179 ns |  5.0822 ns |   104.87 |    1.62 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Emoji  |   293.594 ns |    41.4802 ns |  2.2737 ns |   104.89 |    0.80 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Emoji  |   310.956 ns |    25.7842 ns |  1.4133 ns |   111.10 |    0.60 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Emoji  |   254.605 ns |    26.9521 ns |  1.4773 ns |    90.96 |    0.56 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   353.987 ns |    21.5724 ns |  1.1825 ns |   126.47 |    0.59 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Emoji  |   300.174 ns |    11.6294 ns |  0.6374 ns |   107.25 |    0.44 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   360.932 ns |    79.5036 ns |  4.3579 ns |   128.95 |    1.43 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   366.337 ns |    56.1709 ns |  3.0789 ns |   130.88 |    1.07 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   315.876 ns |    47.2464 ns |  2.5897 ns |   112.86 |    0.90 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.593 ns** |     **0.9151 ns** |  **0.0502 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.252 ns |     0.3381 ns |  0.0185 ns |     0.63 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 4096  | Mixed  |     2.422 ns |     0.1038 ns |  0.0057 ns |     0.67 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 4096  | Mixed  |   185.188 ns |     4.9226 ns |  0.2698 ns |    51.55 |    0.63 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 4096  | Mixed  |   191.766 ns |    16.2043 ns |  0.8882 ns |    53.38 |    0.68 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 4096  | Mixed  |   190.049 ns |     2.3519 ns |  0.1289 ns |    52.91 |    0.64 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   259.095 ns |   106.5321 ns |  5.8394 ns |    72.13 |    1.66 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   170.306 ns |    53.4678 ns |  2.9308 ns |    47.41 |    0.91 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 4096  | Mixed  |   170.578 ns |    37.7025 ns |  2.0666 ns |    47.49 |    0.76 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   171.656 ns |    28.3856 ns |  1.5559 ns |    47.79 |    0.69 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   181.630 ns |    11.0605 ns |  0.6063 ns |    50.56 |    0.63 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   191.371 ns |    10.7847 ns |  0.5911 ns |    53.27 |    0.66 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.518 ns** |     **0.2253 ns** |  **0.0123 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     1.893 ns |     0.6868 ns |  0.0376 ns |     0.54 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Ascii  |     2.061 ns |     0.0440 ns |  0.0024 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Ascii  | 2,219.444 ns |   345.8255 ns | 18.9559 ns |   630.87 |    5.05 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Ascii  | 2,262.401 ns |    32.7284 ns |  1.7940 ns |   643.08 |    2.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Ascii  | 2,237.926 ns |   117.9748 ns |  6.4666 ns |   636.13 |    2.51 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,015.907 ns | 1,346.5903 ns | 73.8112 ns | 1,141.52 |   18.50 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,609.290 ns |    72.1882 ns |  3.9569 ns |   457.44 |    1.70 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Ascii  | 1,601.371 ns |   112.7628 ns |  6.1809 ns |   455.19 |    2.06 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,979.203 ns |   161.6580 ns |  8.8610 ns |   562.59 |    2.77 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 1,977.203 ns |    68.0567 ns |  3.7304 ns |   562.02 |    1.94 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 1,986.151 ns |   164.4192 ns |  9.0124 ns |   564.56 |    2.81 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Latin**  | **4,027.964 ns** |   **188.3622 ns** | **10.3248 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Latin  | 1,952.843 ns |    90.0932 ns |  4.9383 ns |     0.48 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Latin  | 1,952.370 ns |     5.5979 ns |  0.3068 ns |     0.48 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Latin  | 2,378.936 ns |   166.0474 ns |  9.1016 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Latin  | 1,957.388 ns |   108.3840 ns |  5.9409 ns |     0.49 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Latin  | 2,007.486 ns |   649.2176 ns | 35.5858 ns |     0.50 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Latin  | 4,023.270 ns |   731.8027 ns | 40.1126 ns |     1.00 |    0.01 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Latin  | 1,945.835 ns |    28.8535 ns |  1.5816 ns |     0.48 |    0.00 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Latin  | 1,929.573 ns |    91.1771 ns |  4.9977 ns |     0.48 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Latin  | 2,371.874 ns |   149.8441 ns |  8.2135 ns |     0.59 |    0.00 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Latin  | 1,965.956 ns |   565.0393 ns | 30.9717 ns |     0.49 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Latin  | 1,984.602 ns |   552.9510 ns | 30.3091 ns |     0.49 |    0.01 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.480 ns** |     **0.2771 ns** |  **0.0152 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.807 ns |     0.0172 ns |  0.0009 ns |     0.73 |    0.00 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Cjk    |     2.040 ns |     0.1603 ns |  0.0088 ns |     0.82 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Cjk    | 6,673.421 ns |   611.6825 ns | 33.5284 ns | 2,691.49 |   18.51 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Cjk    | 6,894.900 ns | 1,330.7564 ns | 72.9432 ns | 2,780.82 |   29.47 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Cjk    | 6,748.157 ns |   533.3890 ns | 29.2368 ns | 2,721.63 |   17.73 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 3,974.987 ns |   331.1828 ns | 18.1532 ns | 1,603.17 |   10.63 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,856.294 ns |   351.1010 ns | 19.2450 ns | 1,958.62 |   12.41 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Cjk    | 4,944.572 ns | 1,573.8487 ns | 86.2680 ns | 1,994.22 |   31.95 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 5,966.154 ns |   420.3160 ns | 23.0389 ns | 2,406.24 |   15.13 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 5,999.256 ns |   540.1064 ns | 29.6051 ns | 2,419.59 |   16.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,883.404 ns |   344.6158 ns | 18.8896 ns | 1,969.55 |   12.39 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Emoji**  |     **2.818 ns** |     **0.9565 ns** |  **0.0524 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Emoji  |     1.885 ns |     0.1730 ns |  0.0095 ns |     0.67 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Emoji  |     2.119 ns |     0.0988 ns |  0.0054 ns |     0.75 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Emoji  | 4,626.973 ns |   869.2900 ns | 47.6487 ns | 1,642.31 |   30.02 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Emoji  | 4,509.022 ns |   606.8250 ns | 33.2621 ns | 1,600.44 |   27.51 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Emoji  | 4,551.976 ns |   293.4644 ns | 16.0858 ns | 1,615.69 |   26.25 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Emoji  | 4,028.898 ns | 1,514.5972 ns | 83.0202 ns | 1,430.03 |   34.23 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 5,612.911 ns |   772.1582 ns | 42.3246 ns | 1,992.26 |   34.35 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Emoji  | 5,692.500 ns |   659.0207 ns | 36.1231 ns | 2,020.51 |   34.10 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 5,498.465 ns |   604.9475 ns | 33.1592 ns | 1,951.64 |   32.77 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 4,857.588 ns |    43.6128 ns |  2.3906 ns | 1,724.16 |   27.52 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 5,661.274 ns |   354.0393 ns | 19.4061 ns | 2,009.43 |   32.61 |         - |          NA |
|                                    |       |        |              |               |            |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **2.794 ns** |     **0.2774 ns** |  **0.0152 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.815 ns |     0.2894 ns |  0.0159 ns |     0.65 |    0.01 |         - |          NA |
| U8String.LastIndexOf               | 65536 | Mixed  |     2.055 ns |     0.3903 ns |  0.0214 ns |     0.74 |    0.01 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8&#39;       | 65536 | Mixed  | 2,920.603 ns |   379.8375 ns | 20.8202 ns | 1,045.48 |    8.12 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16&#39;      | 65536 | Mixed  | 2,913.717 ns |   183.7190 ns | 10.0703 ns | 1,043.01 |    5.82 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32&#39;      | 65536 | Mixed  | 2,960.277 ns |   423.3219 ns | 23.2037 ns | 1,059.68 |    8.75 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 3,938.143 ns |   199.5146 ns | 10.9361 ns | 1,409.72 |    7.45 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,123.778 ns |   120.4506 ns |  6.6023 ns |   760.24 |    4.12 |         - |          NA |
| &#39;U8String.LastIndexOf miss&#39;        | 65536 | Mixed  | 2,105.497 ns |   154.1823 ns |  8.4513 ns |   753.70 |    4.41 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,593.112 ns |   135.4038 ns |  7.4219 ns |   928.25 |    4.94 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,602.813 ns |   543.0333 ns | 29.7655 ns |   931.72 |   10.22 |         - |          NA |
| &#39;Text.LastRuneIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,146.212 ns |   281.0504 ns | 15.4053 ns |   768.27 |    5.99 |         - |          NA |
