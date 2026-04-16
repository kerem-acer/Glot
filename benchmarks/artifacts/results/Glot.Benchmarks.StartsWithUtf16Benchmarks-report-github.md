```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | N     | Locale | Mean       | Error     | StdDev    | Median     | Ratio     | RatioSD | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |-----------:|----------:|----------:|-----------:|----------:|--------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  |  **0.0867 ns** | **0.1602 ns** | **0.0088 ns** |  **0.0820 ns** |      **1.01** |    **0.12** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  |  0.0315 ns | 0.0117 ns | 0.0006 ns |  0.0318 ns |      0.37 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  |  1.8727 ns | 0.0969 ns | 0.0053 ns |  1.8711 ns |     21.75 |    1.80 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  | 12.6393 ns | 0.7563 ns | 0.0415 ns | 12.6585 ns |    146.79 |   12.18 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  | 10.3758 ns | 1.3991 ns | 0.0767 ns | 10.4116 ns |    120.50 |   10.03 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  |  0.0988 ns | 0.8947 ns | 0.0490 ns |  0.0719 ns |      1.15 |    0.50 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.2600 ns | 0.0939 ns | 0.0051 ns |  0.2571 ns |      3.02 |    0.26 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  1.8808 ns | 0.1767 ns | 0.0097 ns |  1.8850 ns |     21.84 |    1.81 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  | 12.5117 ns | 0.7655 ns | 0.0420 ns | 12.5074 ns |    145.31 |   12.06 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  | 10.2240 ns | 0.3642 ns | 0.0200 ns | 10.2221 ns |    118.74 |    9.85 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **64**    | **Latin**  |  **0.0562 ns** | **0.9044 ns** | **0.0496 ns** |  **0.0558 ns** |     **3.242** |    **5.20** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Latin  |  1.9538 ns | 0.0390 ns | 0.0021 ns |  1.9541 ns |   112.651 |  129.09 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Latin  | 11.5214 ns | 0.5027 ns | 0.0276 ns | 11.5242 ns |   664.280 |  761.25 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Latin  | 10.9974 ns | 1.5071 ns | 0.0826 ns | 10.9642 ns |   634.069 |  726.65 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Latin  |  0.0682 ns | 0.1125 ns | 0.0062 ns |  0.0698 ns |     3.934 |    4.53 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Latin  |  0.0294 ns | 0.0634 ns | 0.0035 ns |  0.0313 ns |     1.697 |    1.96 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Latin  |  1.9077 ns | 0.5018 ns | 0.0275 ns |  1.8961 ns |   109.991 |  126.06 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Latin  | 12.2143 ns | 0.3228 ns | 0.0177 ns | 12.2157 ns |   704.231 |  807.03 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Latin  | 10.5782 ns | 1.4148 ns | 0.0775 ns | 10.5396 ns |   609.898 |  698.95 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    |  **0.0533 ns** | **0.6072 ns** | **0.0333 ns** |  **0.0715 ns** |     **1.684** |    **1.84** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    |  0.0400 ns | 0.0773 ns | 0.0042 ns |  0.0376 ns |     1.263 |    1.08 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    |  1.9080 ns | 0.0588 ns | 0.0032 ns |  1.9087 ns |    60.289 |   50.95 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    | 12.5163 ns | 0.9209 ns | 0.0505 ns | 12.5031 ns |   395.488 |  334.23 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    | 11.0254 ns | 1.8636 ns | 0.1022 ns | 11.0262 ns |   348.380 |  294.43 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    |  0.0972 ns | 0.4059 ns | 0.0223 ns |  0.0996 ns |     3.072 |    2.71 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  1.8912 ns | 0.2882 ns | 0.0158 ns |  1.8858 ns |    59.758 |   50.50 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    | 11.9277 ns | 0.6219 ns | 0.0341 ns | 11.9354 ns |   376.889 |  318.50 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    | 11.0376 ns | 0.6869 ns | 0.0377 ns | 11.0208 ns |   348.763 |  294.74 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **64**    | **Emoji**  |  **0.0585 ns** | **0.9332 ns** | **0.0512 ns** |  **0.0459 ns** |      **1.91** |    **2.39** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Emoji  |  0.0474 ns | 0.1071 ns | 0.0059 ns |  0.0445 ns |      1.54 |    1.28 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Emoji  |  1.8726 ns | 0.0335 ns | 0.0018 ns |  1.8734 ns |     60.97 |   49.78 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Emoji  | 13.4344 ns | 3.1851 ns | 0.1746 ns | 13.3459 ns |    437.39 |  357.22 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Emoji  | 10.3180 ns | 0.1952 ns | 0.0107 ns | 10.3206 ns |    335.92 |  274.31 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Emoji  |  0.0973 ns | 0.4105 ns | 0.0225 ns |  0.0995 ns |      3.17 |    2.71 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  0.0526 ns | 0.0662 ns | 0.0036 ns |  0.0537 ns |      1.71 |    1.40 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  1.8454 ns | 1.1417 ns | 0.0626 ns |  1.8094 ns |     60.08 |   49.11 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Emoji  | 12.4867 ns | 1.2008 ns | 0.0658 ns | 12.4944 ns |    406.53 |  331.98 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Emoji  | 11.1947 ns | 0.1633 ns | 0.0090 ns | 11.1941 ns |    364.47 |  297.62 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  |  **0.0775 ns** | **0.4909 ns** | **0.0269 ns** |  **0.0895 ns** |     **1.110** |    **0.54** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  |  1.9742 ns | 0.6518 ns | 0.0357 ns |  1.9703 ns |    28.262 |   10.53 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  | 12.1949 ns | 0.3098 ns | 0.0170 ns | 12.2047 ns |   174.584 |   64.96 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  | 10.9342 ns | 0.1449 ns | 0.0079 ns | 10.9325 ns |   156.535 |   58.24 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  |  0.0594 ns | 0.2865 ns | 0.0157 ns |  0.0608 ns |     0.850 |    0.38 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  1.9156 ns | 0.1221 ns | 0.0067 ns |  1.9125 ns |    27.424 |   10.20 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  | 11.6085 ns | 0.4180 ns | 0.0229 ns | 11.5985 ns |   166.189 |   61.83 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  | 10.5521 ns | 1.7812 ns | 0.0976 ns | 10.5896 ns |   151.065 |   56.22 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **256**   | **Ascii**  |  **0.1176 ns** | **0.5022 ns** | **0.0275 ns** |  **0.1106 ns** |      **1.04** |    **0.29** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Ascii  |  0.0458 ns | 0.0385 ns | 0.0021 ns |  0.0466 ns |      0.40 |    0.08 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Ascii  |  1.8707 ns | 0.6139 ns | 0.0336 ns |  1.8540 ns |     16.46 |    3.14 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Ascii  | 12.1562 ns | 0.6389 ns | 0.0350 ns | 12.1539 ns |    106.97 |   20.36 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Ascii  | 11.1778 ns | 0.2011 ns | 0.0110 ns | 11.1759 ns |     98.36 |   18.72 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Ascii  |  0.0733 ns | 0.2549 ns | 0.0140 ns |  0.0719 ns |      0.64 |    0.16 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Ascii  |  0.2661 ns | 0.3733 ns | 0.0205 ns |  0.2567 ns |      2.34 |    0.47 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Ascii  |  1.8820 ns | 0.0473 ns | 0.0026 ns |  1.8834 ns |     16.56 |    3.15 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Ascii  | 11.5383 ns | 2.0359 ns | 0.1116 ns | 11.4863 ns |    101.53 |   19.34 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Ascii  | 10.2040 ns | 0.2979 ns | 0.0163 ns | 10.2101 ns |     89.79 |   17.09 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **256**   | **Latin**  |  **0.0833 ns** | **0.3844 ns** | **0.0211 ns** |  **0.0784 ns** |     **1.042** |    **0.32** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Latin  |  1.8922 ns | 0.1078 ns | 0.0059 ns |  1.8913 ns |    23.647 |    4.89 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Latin  | 12.1839 ns | 0.2846 ns | 0.0156 ns | 12.1896 ns |   152.265 |   31.46 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Latin  | 10.2105 ns | 1.0176 ns | 0.0558 ns | 10.1888 ns |   127.604 |   26.37 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Latin  |  0.0920 ns | 0.9767 ns | 0.0535 ns |  0.0692 ns |     1.150 |    0.64 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Latin  |  1.9038 ns | 0.1221 ns | 0.0067 ns |  1.9061 ns |    23.792 |    4.92 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Latin  | 12.2526 ns | 0.7489 ns | 0.0411 ns | 12.2381 ns |   153.124 |   31.64 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Latin  | 11.2173 ns | 0.3944 ns | 0.0216 ns | 11.2243 ns |   140.185 |   28.97 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **256**   | **Cjk**    |  **0.0745 ns** | **0.7854 ns** | **0.0431 ns** |  **0.0552 ns** |     **1.209** |    **0.80** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Cjk    |  0.0612 ns | 0.3518 ns | 0.0193 ns |  0.0503 ns |     0.994 |    0.49 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Cjk    |  1.9522 ns | 0.1579 ns | 0.0087 ns |  1.9492 ns |    31.703 |   12.52 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Cjk    | 11.9956 ns | 0.6408 ns | 0.0351 ns | 12.0052 ns |   194.802 |   76.91 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Cjk    | 10.4682 ns | 2.9091 ns | 0.1595 ns | 10.3891 ns |   169.999 |   67.16 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Cjk    |  0.0931 ns | 0.6040 ns | 0.0331 ns |  0.1015 ns |     1.512 |    0.78 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Cjk    |  1.9021 ns | 0.1839 ns | 0.0101 ns |  1.9017 ns |    30.889 |   12.20 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Cjk    | 11.9768 ns | 0.1979 ns | 0.0108 ns | 11.9754 ns |   194.498 |   76.79 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Cjk    | 11.1229 ns | 0.9676 ns | 0.0530 ns | 11.1045 ns |   180.630 |   71.32 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **256**   | **Emoji**  |  **0.0437 ns** | **0.4652 ns** | **0.0255 ns** |  **0.0541 ns** |      **1.50** |    **1.45** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Emoji  |  0.0463 ns | 0.0455 ns | 0.0025 ns |  0.0456 ns |      1.59 |    1.19 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Emoji  |  1.8667 ns | 0.4616 ns | 0.0253 ns |  1.8571 ns |     63.93 |   47.62 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Emoji  | 13.0921 ns | 1.2847 ns | 0.0704 ns | 13.0838 ns |    448.40 |  333.96 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Emoji  | 11.2167 ns | 0.5448 ns | 0.0299 ns | 11.2320 ns |    384.17 |  286.11 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Emoji  |  0.1065 ns | 0.4570 ns | 0.0251 ns |  0.1076 ns |      3.65 |    2.86 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Emoji  |  0.0512 ns | 0.1218 ns | 0.0067 ns |  0.0477 ns |      1.75 |    1.33 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Emoji  |  1.8210 ns | 0.2410 ns | 0.0132 ns |  1.8198 ns |     62.37 |   46.45 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Emoji  | 12.4227 ns | 0.4824 ns | 0.0264 ns | 12.4110 ns |    425.48 |  316.87 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Emoji  | 11.0922 ns | 0.4471 ns | 0.0245 ns | 11.0961 ns |    379.91 |  282.93 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **256**   | **Mixed**  |  **0.0680 ns** | **0.3655 ns** | **0.0200 ns** |  **0.0784 ns** |     **1.075** |    **0.44** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 256   | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 256   | Mixed  |  1.9026 ns | 0.0749 ns | 0.0041 ns |  1.9004 ns |    30.079 |    9.23 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 256   | Mixed  | 12.2417 ns | 0.1343 ns | 0.0074 ns | 12.2411 ns |   193.528 |   59.41 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 256   | Mixed  | 10.2353 ns | 0.5673 ns | 0.0311 ns | 10.2252 ns |   161.810 |   49.67 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 256   | Mixed  |  0.1015 ns | 0.1961 ns | 0.0107 ns |  0.0996 ns |     1.605 |    0.52 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 256   | Mixed  |  0.0221 ns | 0.2475 ns | 0.0136 ns |  0.0163 ns |     0.350 |    0.22 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 256   | Mixed  |  1.9258 ns | 0.0686 ns | 0.0038 ns |  1.9262 ns |    30.445 |    9.35 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 256   | Mixed  | 12.2239 ns | 0.3472 ns | 0.0190 ns | 12.2162 ns |   193.247 |   59.32 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 256   | Mixed  | 10.3284 ns | 1.4038 ns | 0.0769 ns | 10.3682 ns |   163.280 |   50.14 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  |  **0.0022 ns** | **0.0369 ns** | **0.0020 ns** |  **0.0028 ns** |         **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  |  0.0755 ns | 0.4198 ns | 0.0230 ns |  0.0853 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  |  1.8432 ns | 0.1047 ns | 0.0057 ns |  1.8436 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  | 12.1769 ns | 0.4685 ns | 0.0257 ns | 12.1854 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  | 11.2198 ns | 0.6101 ns | 0.0334 ns | 11.2230 ns |         ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  |  0.0095 ns | 0.0491 ns | 0.0027 ns |  0.0095 ns |         ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  0.2622 ns | 0.0175 ns | 0.0010 ns |  0.2626 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  1.8967 ns | 0.0543 ns | 0.0030 ns |  1.8965 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  | 11.7094 ns | 1.6847 ns | 0.0923 ns | 11.6613 ns |         ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  | 10.1832 ns | 0.9901 ns | 0.0543 ns | 10.1858 ns |         ? |       ? |         - |           ? |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **4096**  | **Latin**  |  **0.0229 ns** | **0.2038 ns** | **0.0112 ns** |  **0.0281 ns** |     **1.280** |    **0.97** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Latin  |  1.9218 ns | 0.2332 ns | 0.0128 ns |  1.9150 ns |   107.604 |   62.83 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Latin  | 12.5703 ns | 1.3856 ns | 0.0759 ns | 12.5269 ns |   703.808 |  410.96 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Latin  | 10.6069 ns | 0.1661 ns | 0.0091 ns | 10.6033 ns |   593.876 |  346.75 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Latin  |  0.0764 ns | 0.4638 ns | 0.0254 ns |  0.0690 ns |     4.277 |    2.87 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Latin  |  1.8991 ns | 0.1405 ns | 0.0077 ns |  1.9014 ns |   106.332 |   62.09 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Latin  | 11.6310 ns | 0.9251 ns | 0.0507 ns | 11.6294 ns |   651.216 |  380.24 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Latin  | 10.2452 ns | 0.6898 ns | 0.0378 ns | 10.2275 ns |   573.628 |  334.93 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    |  **0.0068 ns** | **0.0291 ns** | **0.0016 ns** |  **0.0074 ns** |     **1.044** |    **0.33** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    |  1.9141 ns | 0.1342 ns | 0.0074 ns |  1.9118 ns |   295.352 |   68.53 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    | 12.6595 ns | 0.9282 ns | 0.0509 ns | 12.6306 ns | 1,953.439 |  453.23 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    | 10.2420 ns | 0.2080 ns | 0.0114 ns | 10.2391 ns | 1,580.398 |  366.64 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    |  0.0251 ns | 0.0955 ns | 0.0052 ns |  0.0222 ns |     3.867 |    1.15 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  1.8925 ns | 0.1662 ns | 0.0091 ns |  1.8974 ns |   292.018 |   67.76 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    | 12.5000 ns | 0.3374 ns | 0.0185 ns | 12.4949 ns | 1,928.824 |  447.48 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    | 10.4765 ns | 3.5391 ns | 0.1940 ns | 10.3934 ns | 1,616.589 |  375.97 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **4096**  | **Emoji**  |  **0.0174 ns** | **0.0708 ns** | **0.0039 ns** |  **0.0185 ns** |      **1.04** |    **0.30** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Emoji  |  0.0517 ns | 0.1902 ns | 0.0104 ns |  0.0476 ns |      3.09 |    0.86 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Emoji  |  1.8762 ns | 0.7768 ns | 0.0426 ns |  1.8524 ns |    112.06 |   24.16 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Emoji  | 12.4112 ns | 0.1863 ns | 0.0102 ns | 12.4110 ns |    741.30 |  159.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Emoji  | 11.1244 ns | 1.1403 ns | 0.0625 ns | 11.1116 ns |    664.44 |  142.68 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Emoji  |  0.0160 ns | 0.1244 ns | 0.0068 ns |  0.0123 ns |      0.96 |    0.41 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  0.0459 ns | 0.0783 ns | 0.0043 ns |  0.0437 ns |      2.74 |    0.63 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  1.8114 ns | 0.1972 ns | 0.0108 ns |  1.8059 ns |    108.19 |   23.23 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Emoji  | 12.5112 ns | 0.1458 ns | 0.0080 ns | 12.5118 ns |    747.27 |  160.42 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Emoji  | 11.2825 ns | 0.8402 ns | 0.0461 ns | 11.2982 ns |    673.88 |  144.69 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  |  **0.0153 ns** | **0.0453 ns** | **0.0025 ns** |  **0.0149 ns** |     **1.017** |    **0.20** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  |  1.9217 ns | 0.1075 ns | 0.0059 ns |  1.9227 ns |   127.761 |   17.55 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  | 11.6225 ns | 0.2935 ns | 0.0161 ns | 11.6240 ns |   772.698 |  106.11 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  | 10.2372 ns | 0.1052 ns | 0.0058 ns | 10.2342 ns |   680.597 |   93.46 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  |  0.0059 ns | 0.0327 ns | 0.0018 ns |  0.0059 ns |     0.390 |    0.12 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  1.8848 ns | 0.0940 ns | 0.0052 ns |  1.8875 ns |   125.305 |   17.21 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  | 11.6757 ns | 1.5488 ns | 0.0849 ns | 11.6418 ns |   776.229 |  106.71 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  | 10.2458 ns | 0.2909 ns | 0.0159 ns | 10.2526 ns |   681.165 |   93.54 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  |  **0.1664 ns** | **0.0955 ns** | **0.0052 ns** |  **0.1662 ns** |      **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  |  0.1007 ns | 1.0205 ns | 0.0559 ns |  0.1061 ns |      0.61 |    0.29 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  |  1.8592 ns | 0.0686 ns | 0.0038 ns |  1.8596 ns |     11.18 |    0.31 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  | 11.6709 ns | 0.4615 ns | 0.0253 ns | 11.6566 ns |     70.20 |    1.92 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  | 11.1746 ns | 0.1971 ns | 0.0108 ns | 11.1726 ns |     67.21 |    1.83 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  |  0.1550 ns | 0.3332 ns | 0.0183 ns |  0.1638 ns |      0.93 |    0.10 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.2754 ns | 0.0808 ns | 0.0044 ns |  0.2777 ns |      1.66 |    0.05 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  1.9346 ns | 0.0600 ns | 0.0033 ns |  1.9338 ns |     11.64 |    0.32 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  | 12.6994 ns | 2.7951 ns | 0.1532 ns | 12.6490 ns |     76.38 |    2.23 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  | 11.1032 ns | 0.1661 ns | 0.0091 ns | 11.1013 ns |     66.78 |    1.82 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **65536** | **Latin**  |  **0.1664 ns** | **0.2987 ns** | **0.0164 ns** |  **0.1617 ns** |     **1.006** |    **0.12** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Latin  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Latin  |  1.9708 ns | 0.0813 ns | 0.0045 ns |  1.9689 ns |    11.915 |    0.98 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Latin  | 11.9407 ns | 0.5665 ns | 0.0311 ns | 11.9569 ns |    72.190 |    5.94 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Latin  | 11.5603 ns | 2.3982 ns | 0.1315 ns | 11.4989 ns |    69.890 |    5.79 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Latin  |  0.1140 ns | 0.3897 ns | 0.0214 ns |  0.1208 ns |     0.689 |    0.13 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Latin  |  0.0093 ns | 0.2561 ns | 0.0140 ns |  0.0025 ns |     0.056 |    0.07 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Latin  |  1.9443 ns | 0.2090 ns | 0.0115 ns |  1.9392 ns |    11.755 |    0.97 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Latin  | 11.8883 ns | 0.4088 ns | 0.0224 ns | 11.8847 ns |    71.873 |    5.91 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Latin  | 11.4314 ns | 0.2527 ns | 0.0139 ns | 11.4251 ns |    69.111 |    5.68 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    |  **0.1632 ns** | **1.1194 ns** | **0.0614 ns** |  **0.1954 ns** |     **1.136** |    **0.62** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    |  1.9638 ns | 0.0573 ns | 0.0031 ns |  1.9640 ns |    13.675 |    5.68 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    | 12.8179 ns | 2.5924 ns | 0.1421 ns | 12.7521 ns |    89.259 |   37.06 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    | 11.1463 ns | 1.6524 ns | 0.0906 ns | 11.1690 ns |    77.618 |   32.23 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    |  0.2006 ns | 0.2543 ns | 0.0139 ns |  0.1937 ns |     1.397 |    0.59 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  1.9619 ns | 0.9626 ns | 0.0528 ns |  1.9351 ns |    13.662 |    5.68 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 12.7229 ns | 0.7005 ns | 0.0384 ns | 12.7038 ns |    88.597 |   36.78 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    | 10.5855 ns | 0.3259 ns | 0.0179 ns | 10.5784 ns |    73.713 |   30.60 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **65536** | **Emoji**  |  **0.1539 ns** | **0.4978 ns** | **0.0273 ns** |  **0.1624 ns** |      **1.02** |    **0.23** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Emoji  |  0.0424 ns | 0.0246 ns | 0.0013 ns |  0.0418 ns |      0.28 |    0.05 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Emoji  |  1.8889 ns | 0.1067 ns | 0.0058 ns |  1.8890 ns |     12.56 |    2.10 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Emoji  | 13.0089 ns | 1.8422 ns | 0.1010 ns | 12.9656 ns |     86.52 |   14.48 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Emoji  | 11.0993 ns | 0.4394 ns | 0.0241 ns | 11.1007 ns |     73.82 |   12.34 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Emoji  |  0.1690 ns | 0.3322 ns | 0.0182 ns |  0.1744 ns |      1.12 |    0.22 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  0.0541 ns | 0.1610 ns | 0.0088 ns |  0.0498 ns |      0.36 |    0.08 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  1.9125 ns | 0.0073 ns | 0.0004 ns |  1.9123 ns |     12.72 |    2.13 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Emoji  | 13.1638 ns | 1.0138 ns | 0.0556 ns | 13.1439 ns |     87.55 |   14.64 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Emoji  | 11.3750 ns | 1.2279 ns | 0.0673 ns | 11.3753 ns |     75.65 |   12.66 |         - |          NA |
|                               |       |        |            |           |           |            |           |         |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  |  **0.1600 ns** | **0.7363 ns** | **0.0404 ns** |  **0.1750 ns** |     **1.051** |    **0.35** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  |  1.9770 ns | 0.2174 ns | 0.0119 ns |  1.9757 ns |    12.991 |    3.26 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  | 12.6897 ns | 0.4738 ns | 0.0260 ns | 12.6905 ns |    83.382 |   20.93 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  | 10.5085 ns | 1.2520 ns | 0.0686 ns | 10.4772 ns |    69.050 |   17.34 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  |  0.1515 ns | 0.7362 ns | 0.0404 ns |  0.1729 ns |     0.995 |    0.34 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     0.000 |    0.00 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  1.9981 ns | 2.9746 ns | 0.1630 ns |  1.9182 ns |    13.129 |    3.43 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  | 11.7745 ns | 0.2662 ns | 0.0146 ns | 11.7665 ns |    77.369 |   19.42 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  | 10.6257 ns | 3.0946 ns | 0.1696 ns | 10.7059 ns |    69.820 |   17.55 |         - |          NA |
