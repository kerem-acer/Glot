```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                             | N     | Locale | Mean         | Error          | StdDev        | Median       | Ratio    | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |------ |------- |-------------:|---------------:|--------------:|-------------:|---------:|--------:|----------:|------------:|
| **string.LastIndexOf**                 | **64**    | **Ascii**  |     **4.328 ns** |      **0.7318 ns** |     **0.0401 ns** |     **4.330 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Ascii  |     2.251 ns |      0.7484 ns |     0.0410 ns |     2.262 ns |     0.52 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Ascii  |     3.087 ns |      0.8527 ns |     0.0467 ns |     3.112 ns |     0.71 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Ascii  |     8.812 ns |      8.2374 ns |     0.4515 ns |     8.574 ns |     2.04 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Ascii  |     8.759 ns |      1.8149 ns |     0.0995 ns |     8.722 ns |     2.02 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Ascii  |     6.013 ns |     33.4731 ns |     1.8348 ns |     4.968 ns |     1.39 |    0.37 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.018 ns |      1.0944 ns |     0.0600 ns |     1.989 ns |     0.47 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     2.775 ns |      0.2647 ns |     0.0145 ns |     2.774 ns |     0.64 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     7.567 ns |      2.1268 ns |     0.1166 ns |     7.501 ns |     1.75 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.977 ns |     21.4106 ns |     1.1736 ns |     9.381 ns |     2.07 |    0.24 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Cjk**    |     **2.316 ns** |      **7.9828 ns** |     **0.4376 ns** |     **2.096 ns** |     **1.02** |    **0.23** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Cjk    |     1.376 ns |      0.3956 ns |     0.0217 ns |     1.377 ns |     0.61 |    0.09 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Cjk    |     2.530 ns |      0.3381 ns |     0.0185 ns |     2.528 ns |     1.12 |    0.17 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Cjk    |    28.897 ns |    135.6961 ns |     7.4380 ns |    26.037 ns |    12.75 |    3.43 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Cjk    |    19.717 ns |     15.8248 ns |     0.8674 ns |    19.806 ns |     8.70 |    1.33 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Cjk    |     4.977 ns |      1.2896 ns |     0.0707 ns |     4.963 ns |     2.20 |    0.33 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Cjk    |     5.950 ns |      2.1098 ns |     0.1156 ns |     5.961 ns |     2.63 |    0.39 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     6.765 ns |      0.7005 ns |     0.0384 ns |     6.744 ns |     2.98 |    0.44 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    16.331 ns |      2.3676 ns |     0.1298 ns |    16.382 ns |     7.21 |    1.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    20.745 ns |      1.3963 ns |     0.0765 ns |    20.709 ns |     9.15 |    1.36 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **64**    | **Mixed**  |     **4.343 ns** |      **0.9861 ns** |     **0.0541 ns** |     **4.359 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 64    | Mixed  |     2.577 ns |      0.5141 ns |     0.0282 ns |     2.563 ns |     0.59 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 64    | Mixed  |     3.365 ns |      1.3066 ns |     0.0716 ns |     3.360 ns |     0.77 |    0.02 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 64    | Mixed  |    11.349 ns |     54.6493 ns |     2.9955 ns |     9.648 ns |     2.61 |    0.60 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 64    | Mixed  |     9.657 ns |      9.6863 ns |     0.5309 ns |     9.373 ns |     2.22 |    0.11 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 64    | Mixed  |     4.938 ns |      1.1356 ns |     0.0622 ns |     4.966 ns |     1.14 |    0.02 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 64    | Mixed  |     2.857 ns |      0.6236 ns |     0.0342 ns |     2.870 ns |     0.66 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.350 ns |      0.2899 ns |     0.0159 ns |     3.353 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    12.664 ns |      3.6180 ns |     0.1983 ns |    12.576 ns |     2.92 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    18.536 ns |      4.8776 ns |     0.2674 ns |    18.613 ns |     4.27 |    0.07 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Ascii**  |     **2.269 ns** |      **0.1478 ns** |     **0.0081 ns** |     **2.270 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Ascii  |     1.565 ns |      0.1133 ns |     0.0062 ns |     1.567 ns |     0.69 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     2.737 ns |      2.2208 ns |     0.1217 ns |     2.746 ns |     1.21 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Ascii  |     8.366 ns |      1.1889 ns |     0.0652 ns |     8.332 ns |     3.69 |    0.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Ascii  |     8.329 ns |      0.1466 ns |     0.0080 ns |     8.331 ns |     3.67 |    0.01 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Ascii  |   267.564 ns |     32.1799 ns |     1.7639 ns |   267.724 ns |   117.91 |    0.77 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   138.770 ns |     69.4045 ns |     3.8043 ns |   138.075 ns |    61.15 |    1.46 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   136.168 ns |     12.5010 ns |     0.6852 ns |   136.224 ns |    60.01 |    0.32 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   143.550 ns |     30.9048 ns |     1.6940 ns |   142.824 ns |    63.26 |    0.68 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   147.400 ns |     73.9415 ns |     4.0530 ns |   145.904 ns |    64.96 |    1.56 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Cjk**    |     **2.204 ns** |      **2.7293 ns** |     **0.1496 ns** |     **2.133 ns** |     **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Cjk    |     1.748 ns |      0.2103 ns |     0.0115 ns |     1.743 ns |     0.80 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     2.759 ns |      0.3112 ns |     0.0171 ns |     2.764 ns |     1.26 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    22.486 ns |      0.5810 ns |     0.0318 ns |    22.468 ns |    10.23 |    0.58 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    19.200 ns |     12.8873 ns |     0.7064 ns |    18.865 ns |     8.74 |    0.57 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Cjk    |   264.555 ns |    145.8062 ns |     7.9921 ns |   260.710 ns |   120.42 |    7.52 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   390.101 ns |     83.3237 ns |     4.5673 ns |   391.262 ns |   177.56 |   10.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   408.088 ns |    452.1863 ns |    24.7859 ns |   395.208 ns |   185.75 |   14.37 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   395.765 ns |     26.7595 ns |     1.4668 ns |   395.890 ns |   180.14 |   10.23 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   411.286 ns |     62.3763 ns |     3.4191 ns |   409.822 ns |   187.20 |   10.70 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **4096**  | **Mixed**  |     **3.534 ns** |      **0.9164 ns** |     **0.0502 ns** |     **3.511 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 4096  | Mixed  |     2.434 ns |      5.0058 ns |     0.2744 ns |     2.306 ns |     0.69 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     3.057 ns |      0.6769 ns |     0.0371 ns |     3.045 ns |     0.87 |    0.01 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     9.096 ns |      2.6241 ns |     0.1438 ns |     9.170 ns |     2.57 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 4096  | Mixed  |     9.032 ns |      1.2151 ns |     0.0666 ns |     9.001 ns |     2.56 |    0.04 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 4096  | Mixed  |   260.058 ns |     22.5928 ns |     1.2384 ns |   260.273 ns |    73.60 |    0.95 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   175.007 ns |     62.2933 ns |     3.4145 ns |   176.910 ns |    49.53 |    1.03 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   176.490 ns |    125.7324 ns |     6.8918 ns |   172.855 ns |    49.95 |    1.80 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   195.209 ns |    177.1007 ns |     9.7075 ns |   197.902 ns |    55.24 |    2.47 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   194.655 ns |     17.7143 ns |     0.9710 ns |   194.866 ns |    55.09 |    0.71 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Ascii**  |     **3.565 ns** |      **0.7395 ns** |     **0.0405 ns** |     **3.566 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Ascii  |     2.405 ns |     15.8192 ns |     0.8671 ns |     1.961 ns |     0.67 |    0.21 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     3.034 ns |      3.7054 ns |     0.2031 ns |     3.105 ns |     0.85 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     8.486 ns |      2.9014 ns |     0.1590 ns |     8.409 ns |     2.38 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Ascii  |     8.376 ns |      1.1117 ns |     0.0609 ns |     8.379 ns |     2.35 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Ascii  | 4,111.614 ns |    577.9432 ns |    31.6790 ns | 4,113.988 ns | 1,153.40 |   13.72 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,791.449 ns |  1,971.1136 ns |   108.0434 ns | 1,787.494 ns |   502.54 |   26.71 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 2,622.511 ns | 16,716.1721 ns |   916.2698 ns | 2,094.193 ns |   735.68 |  222.73 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,092.656 ns |     39.0605 ns |     2.1410 ns | 2,092.382 ns |   587.04 |    5.81 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,051.295 ns |    234.7405 ns |    12.8669 ns | 2,051.265 ns |   575.44 |    6.47 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Cjk**    |     **2.481 ns** |      **0.0852 ns** |     **0.0047 ns** |     **2.482 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Cjk    |     1.794 ns |      0.1906 ns |     0.0104 ns |     1.792 ns |     0.72 |    0.00 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     3.361 ns |      8.2865 ns |     0.4542 ns |     3.541 ns |     1.35 |    0.16 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    22.999 ns |      2.3422 ns |     0.1284 ns |    23.001 ns |     9.27 |    0.05 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    19.185 ns |      1.2920 ns |     0.0708 ns |    19.147 ns |     7.73 |    0.03 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Cjk    | 4,133.493 ns |    356.4392 ns |    19.5376 ns | 4,124.578 ns | 1,666.28 |    7.34 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 5,050.635 ns |    173.2343 ns |     9.4956 ns | 5,050.051 ns | 2,036.00 |    4.69 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 7,500.986 ns | 40,630.9906 ns | 2,227.1217 ns | 6,237.924 ns | 3,023.78 |  777.53 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,374.704 ns |  4,729.0521 ns |   259.2153 ns | 6,244.954 ns | 2,569.75 |   90.59 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 6,274.812 ns | 38,449.3126 ns | 2,107.5366 ns | 5,083.849 ns | 2,529.48 |  735.77 |         - |          NA |
|                                    |       |        |              |                |               |              |          |         |           |             |
| **string.LastIndexOf**                 | **65536** | **Mixed**  |     **3.142 ns** |      **5.2666 ns** |     **0.2887 ns** |     **3.218 ns** |     **1.01** |    **0.12** |         **-** |          **NA** |
| &#39;Span.LastIndexOf UTF-8&#39;           | 65536 | Mixed  |     1.882 ns |      2.3938 ns |     0.1312 ns |     1.816 ns |     0.60 |    0.06 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     2.798 ns |      0.4445 ns |     0.0244 ns |     2.792 ns |     0.90 |    0.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16&#39;      | 65536 | Mixed  |    11.268 ns |     70.0888 ns |     3.8418 ns |     9.072 ns |     3.61 |    1.11 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32&#39;      | 65536 | Mixed  |     9.322 ns |      5.9675 ns |     0.3271 ns |     9.385 ns |     2.98 |    0.26 |         - |          NA |
| &#39;string.LastIndexOf miss&#39;          | 65536 | Mixed  | 5,403.454 ns | 39,406.6220 ns | 2,160.0099 ns | 4,174.920 ns | 1,729.80 |  617.39 |         - |          NA |
| &#39;Span.LastIndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,220.033 ns |    455.2620 ns |    24.9544 ns | 2,224.088 ns |   710.70 |   59.07 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,700.346 ns |    184.5679 ns |    10.1168 ns | 2,697.831 ns |   864.46 |   71.41 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,705.919 ns |    416.4246 ns |    22.8256 ns | 2,696.540 ns |   866.24 |   71.79 |         - |          NA |
| &#39;Text.LastByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,457.244 ns |  3,670.7175 ns |   201.2044 ns | 2,547.803 ns |   786.64 |   85.71 |         - |          NA |
