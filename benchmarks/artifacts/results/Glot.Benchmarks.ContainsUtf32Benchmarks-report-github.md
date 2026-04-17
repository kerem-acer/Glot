```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N     | Locale | Mean         | Error          | StdDev        | Median       | Ratio    | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-------------:|---------------:|--------------:|-------------:|---------:|--------:|----------:|------------:|
| **string.Contains**             | **64**    | **Ascii**  |     **2.690 ns** |      **0.7103 ns** |     **0.0389 ns** |     **2.677 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Ascii  |     2.684 ns |     17.3019 ns |     0.9484 ns |     2.138 ns |     1.00 |    0.31 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Ascii  |     2.940 ns |      0.0707 ns |     0.0039 ns |     2.937 ns |     1.09 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Ascii  |     8.485 ns |      1.8927 ns |     0.1037 ns |     8.477 ns |     3.15 |    0.05 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Ascii  |     8.428 ns |      0.7312 ns |     0.0401 ns |     8.409 ns |     3.13 |    0.04 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Ascii  |     4.575 ns |      1.3595 ns |     0.0745 ns |     4.537 ns |     1.70 |    0.03 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Ascii  |     2.404 ns |      0.0579 ns |     0.0032 ns |     2.403 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Ascii  |     4.303 ns |     32.1850 ns |     1.7642 ns |     3.286 ns |     1.60 |    0.57 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Ascii  |     9.156 ns |     37.1644 ns |     2.0371 ns |     7.990 ns |     3.40 |    0.66 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Ascii  |     8.087 ns |      0.6266 ns |     0.0343 ns |     8.074 ns |     3.01 |    0.04 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **64**    | **Cjk**    |     **1.979 ns** |      **0.5128 ns** |     **0.0281 ns** |     **1.964 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Cjk    |     1.732 ns |      0.1839 ns |     0.0101 ns |     1.728 ns |     0.88 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Cjk    |     2.789 ns |      0.0376 ns |     0.0021 ns |     2.788 ns |     1.41 |    0.02 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Cjk    |    23.596 ns |      0.2994 ns |     0.0164 ns |    23.602 ns |    11.93 |    0.15 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Cjk    |    19.025 ns |      1.3938 ns |     0.0764 ns |    19.026 ns |     9.62 |    0.12 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Cjk    |     5.004 ns |     15.7651 ns |     0.8641 ns |     4.520 ns |     2.53 |    0.38 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Cjk    |     5.624 ns |     13.3609 ns |     0.7324 ns |     5.205 ns |     2.84 |    0.32 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Cjk    |     6.525 ns |      3.3710 ns |     0.1848 ns |     6.570 ns |     3.30 |    0.09 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Cjk    |    14.735 ns |      1.5836 ns |     0.0868 ns |    14.688 ns |     7.45 |    0.10 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Cjk    |    23.252 ns |    111.2452 ns |     6.0977 ns |    19.770 ns |    11.75 |    2.67 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **64**    | **Mixed**  |     **2.719 ns** |     **13.8360 ns** |     **0.7584 ns** |     **2.293 ns** |     **1.05** |    **0.34** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 64    | Mixed  |     2.093 ns |      0.6609 ns |     0.0362 ns |     2.075 ns |     0.81 |    0.17 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 64    | Mixed  |     2.941 ns |      0.8368 ns |     0.0459 ns |     2.937 ns |     1.13 |    0.24 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 64    | Mixed  |     8.821 ns |      0.2567 ns |     0.0141 ns |     8.817 ns |     3.40 |    0.71 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 64    | Mixed  |    10.657 ns |     56.1808 ns |     3.0795 ns |     8.880 ns |     4.10 |    1.35 |         - |          NA |
| &#39;string.Contains miss&#39;      | 64    | Mixed  |     4.770 ns |      6.2792 ns |     0.3442 ns |     4.595 ns |     1.84 |    0.40 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 64    | Mixed  |     3.009 ns |      0.4562 ns |     0.0250 ns |     3.016 ns |     1.16 |    0.24 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 64    | Mixed  |     4.497 ns |     30.0283 ns |     1.6460 ns |     3.552 ns |     1.73 |    0.67 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 64    | Mixed  |    12.918 ns |     11.9312 ns |     0.6540 ns |    12.697 ns |     4.97 |    1.06 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 64    | Mixed  |    18.022 ns |      2.5753 ns |     0.1412 ns |    17.946 ns |     6.94 |    1.44 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **4096**  | **Ascii**  |     **2.713 ns** |      **0.6790 ns** |     **0.0372 ns** |     **2.703 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Ascii  |     2.613 ns |     14.0871 ns |     0.7722 ns |     2.186 ns |     0.96 |    0.25 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Ascii  |     3.298 ns |      8.8673 ns |     0.4860 ns |     3.062 ns |     1.22 |    0.16 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Ascii  |     8.448 ns |      0.3078 ns |     0.0169 ns |     8.440 ns |     3.11 |    0.04 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Ascii  |     8.411 ns |      0.0788 ns |     0.0043 ns |     8.410 ns |     3.10 |    0.04 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Ascii  |   256.479 ns |     14.2979 ns |     0.7837 ns |   256.175 ns |    94.54 |    1.15 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Ascii  |    99.866 ns |    151.6905 ns |     8.3147 ns |    95.955 ns |    36.81 |    2.69 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Ascii  |    95.288 ns |     11.9881 ns |     0.6571 ns |    95.196 ns |    35.12 |    0.47 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Ascii  |   102.136 ns |      7.4958 ns |     0.4109 ns |   102.130 ns |    37.65 |    0.46 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Ascii  |   104.102 ns |      3.4507 ns |     0.1891 ns |   104.020 ns |    38.37 |    0.46 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **4096**  | **Cjk**    |     **1.991 ns** |      **0.1319 ns** |     **0.0072 ns** |     **1.987 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Cjk    |     1.727 ns |      0.0245 ns |     0.0013 ns |     1.727 ns |     0.87 |    0.00 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Cjk    |     2.746 ns |      0.1219 ns |     0.0067 ns |     2.746 ns |     1.38 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Cjk    |    25.291 ns |     51.5643 ns |     2.8264 ns |    23.680 ns |    12.70 |    1.23 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Cjk    |    19.021 ns |      1.4382 ns |     0.0788 ns |    18.990 ns |     9.56 |    0.05 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Cjk    |   323.019 ns |  2,008.2439 ns |   110.0786 ns |   259.950 ns |   162.26 |   47.89 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   336.423 ns |  1,794.6768 ns |    98.3723 ns |   284.317 ns |   169.00 |   42.80 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Cjk    |   272.541 ns |     39.7010 ns |     2.1761 ns |   271.515 ns |   136.91 |    1.04 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Cjk    |   295.883 ns |     29.3445 ns |     1.6085 ns |   295.143 ns |   148.63 |    0.84 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Cjk    |   298.628 ns |     24.7555 ns |     1.3569 ns |   298.232 ns |   150.01 |    0.76 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **4096**  | **Mixed**  |     **2.269 ns** |      **0.0580 ns** |     **0.0032 ns** |     **2.270 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 4096  | Mixed  |     2.074 ns |      0.0789 ns |     0.0043 ns |     2.072 ns |     0.91 |    0.00 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 4096  | Mixed  |     2.881 ns |      0.2700 ns |     0.0148 ns |     2.881 ns |     1.27 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 4096  | Mixed  |     9.544 ns |     21.3973 ns |     1.1729 ns |     8.874 ns |     4.21 |    0.45 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 4096  | Mixed  |     8.864 ns |      0.3679 ns |     0.0202 ns |     8.853 ns |     3.91 |    0.01 |         - |          NA |
| &#39;string.Contains miss&#39;      | 4096  | Mixed  |   256.538 ns |     13.7910 ns |     0.7559 ns |   256.181 ns |   113.08 |    0.32 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   125.203 ns |     22.5195 ns |     1.2344 ns |   125.553 ns |    55.19 |    0.48 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 4096  | Mixed  |   125.634 ns |     27.3772 ns |     1.5006 ns |   124.787 ns |    55.38 |    0.58 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 4096  | Mixed  |   159.543 ns |    328.9948 ns |    18.0333 ns |   168.073 ns |    70.32 |    6.88 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 4096  | Mixed  |   174.120 ns |  1,175.3991 ns |    64.4276 ns |   137.063 ns |    76.75 |   24.59 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **65536** | **Ascii**  |     **2.828 ns** |      **5.8127 ns** |     **0.3186 ns** |     **2.683 ns** |     **1.01** |    **0.14** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Ascii  |     2.229 ns |      2.1199 ns |     0.1162 ns |     2.166 ns |     0.79 |    0.08 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Ascii  |     2.975 ns |      0.0806 ns |     0.0044 ns |     2.976 ns |     1.06 |    0.10 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Ascii  |     8.509 ns |      0.0571 ns |     0.0031 ns |     8.511 ns |     3.03 |    0.28 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Ascii  |     8.513 ns |      0.0696 ns |     0.0038 ns |     8.512 ns |     3.03 |    0.28 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Ascii  | 4,006.603 ns |    739.0031 ns |    40.5073 ns | 3,987.253 ns | 1,428.24 |  132.05 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 1,435.615 ns |    176.9763 ns |     9.7007 ns | 1,432.943 ns |   511.76 |   47.20 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Ascii  | 1,406.199 ns |     20.7510 ns |     1.1374 ns | 1,406.665 ns |   501.27 |   46.14 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Ascii  | 1,409.769 ns |    203.0579 ns |    11.1303 ns | 1,404.273 ns |   502.54 |   46.38 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Ascii  | 1,476.134 ns |    253.7612 ns |    13.9095 ns | 1,468.427 ns |   526.20 |   48.62 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **65536** | **Cjk**    |     **1.981 ns** |      **0.2542 ns** |     **0.0139 ns** |     **1.975 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Cjk    |     1.761 ns |      0.5001 ns |     0.0274 ns |     1.745 ns |     0.89 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Cjk    |     2.762 ns |      0.1844 ns |     0.0101 ns |     2.756 ns |     1.39 |    0.01 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Cjk    |    23.436 ns |      1.5260 ns |     0.0836 ns |    23.457 ns |    11.83 |    0.08 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Cjk    |    19.065 ns |      0.3718 ns |     0.0204 ns |    19.064 ns |     9.62 |    0.06 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Cjk    | 4,276.365 ns |  5,513.0532 ns |   302.1890 ns | 4,349.628 ns | 2,158.65 |  132.75 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 5,332.138 ns | 30,847.3949 ns | 1,690.8498 ns | 4,371.270 ns | 2,691.59 |  739.36 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Cjk    | 4,262.810 ns |    190.1119 ns |    10.4207 ns | 4,260.146 ns | 2,151.81 |   13.83 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Cjk    | 4,362.059 ns |  1,362.3279 ns |    74.6738 ns | 4,343.899 ns | 2,201.91 |   35.27 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Cjk    | 5,265.179 ns | 28,613.3620 ns | 1,568.3949 ns | 4,410.832 ns | 2,657.79 |  685.84 |         - |          NA |
|                             |       |        |              |                |               |              |          |         |           |             |
| **string.Contains**             | **65536** | **Mixed**  |     **2.694 ns** |     **13.6038 ns** |     **0.7457 ns** |     **2.263 ns** |     **1.05** |    **0.33** |         **-** |          **NA** |
| &#39;Span.Contains UTF-8&#39;       | 65536 | Mixed  |     2.094 ns |      0.0662 ns |     0.0036 ns |     2.094 ns |     0.81 |    0.17 |         - |          NA |
| &#39;Text.Contains UTF-8&#39;       | 65536 | Mixed  |     2.986 ns |      0.9836 ns |     0.0539 ns |     2.991 ns |     1.16 |    0.24 |         - |          NA |
| &#39;Text.Contains UTF-16&#39;      | 65536 | Mixed  |     8.847 ns |      0.1407 ns |     0.0077 ns |     8.843 ns |     3.44 |    0.71 |         - |          NA |
| &#39;Text.Contains UTF-32&#39;      | 65536 | Mixed  |     9.580 ns |      0.1147 ns |     0.0063 ns |     9.579 ns |     3.72 |    0.77 |         - |          NA |
| &#39;string.Contains miss&#39;      | 65536 | Mixed  | 3,961.813 ns |    214.7748 ns |    11.7725 ns | 3,967.897 ns | 1,538.62 |  318.07 |         - |          NA |
| &#39;Span.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 1,924.797 ns |    609.0870 ns |    33.3861 ns | 1,923.917 ns |   747.52 |  154.94 |         - |          NA |
| &#39;Text.Contains UTF-8 miss&#39;  | 65536 | Mixed  | 1,960.090 ns |    543.0406 ns |    29.7659 ns | 1,959.477 ns |   761.23 |  157.68 |         - |          NA |
| &#39;Text.Contains UTF-16 miss&#39; | 65536 | Mixed  | 1,934.194 ns |    189.9469 ns |    10.4116 ns | 1,933.877 ns |   751.17 |  155.31 |         - |          NA |
| &#39;Text.Contains UTF-32 miss&#39; | 65536 | Mixed  | 1,986.424 ns |     15.9435 ns |     0.8739 ns | 1,986.621 ns |   771.45 |  159.46 |         - |          NA |
