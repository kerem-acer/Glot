```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean         | Error          | StdDev        | Median       | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|---------------:|--------------:|-------------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **2.639 ns** |      **0.1290 ns** |     **0.0071 ns** |     **2.641 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     2.020 ns |      0.6410 ns |     0.0351 ns |     2.009 ns |     0.77 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Ascii  |     2.943 ns |      0.1726 ns |     0.0095 ns |     2.944 ns |     1.12 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Ascii  |     8.524 ns |      0.8637 ns |     0.0473 ns |     8.536 ns |     3.23 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Ascii  |    10.453 ns |     63.7362 ns |     3.4936 ns |     8.441 ns |     3.96 |    1.15 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     4.491 ns |      0.6039 ns |     0.0331 ns |     4.501 ns |     1.70 |    0.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     2.367 ns |      0.3904 ns |     0.0214 ns |     2.377 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     3.325 ns |      0.3874 ns |     0.0212 ns |     3.321 ns |     1.26 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |     8.136 ns |      0.9045 ns |     0.0496 ns |     8.150 ns |     3.08 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |     8.196 ns |      0.6617 ns |     0.0363 ns |     8.197 ns |     3.11 |    0.01 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **1.866 ns** |      **0.2058 ns** |     **0.0113 ns** |     **1.871 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     1.579 ns |      0.0928 ns |     0.0051 ns |     1.579 ns |     0.85 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Cjk    |     2.736 ns |      0.3920 ns |     0.0215 ns |     2.735 ns |     1.47 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Cjk    |    24.353 ns |      0.3606 ns |     0.0198 ns |    24.356 ns |    13.05 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Cjk    |    19.002 ns |      2.1452 ns |     0.1176 ns |    19.052 ns |    10.18 |    0.08 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     5.416 ns |     29.2104 ns |     1.6011 ns |     4.521 ns |     2.90 |    0.74 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     6.678 ns |     30.5399 ns |     1.6740 ns |     5.733 ns |     3.58 |    0.78 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     7.345 ns |      5.5716 ns |     0.3054 ns |     7.214 ns |     3.94 |    0.14 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    15.821 ns |     33.1569 ns |     1.8174 ns |    14.820 ns |     8.48 |    0.84 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    19.932 ns |      1.4701 ns |     0.0806 ns |    19.927 ns |    10.68 |    0.07 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **2.417 ns** |      **2.0753 ns** |     **0.1138 ns** |     **2.449 ns** |     **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     2.070 ns |      0.3718 ns |     0.0204 ns |     2.060 ns |     0.86 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Mixed  |     2.906 ns |      0.3453 ns |     0.0189 ns |     2.916 ns |     1.20 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Mixed  |    10.766 ns |     60.2225 ns |     3.3010 ns |     8.896 ns |     4.46 |    1.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Mixed  |     9.887 ns |     23.7833 ns |     1.3036 ns |     9.187 ns |     4.10 |    0.50 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     4.541 ns |      1.0486 ns |     0.0575 ns |     4.537 ns |     1.88 |    0.08 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     3.733 ns |     29.9473 ns |     1.6415 ns |     2.808 ns |     1.55 |    0.59 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     3.532 ns |      0.2974 ns |     0.0163 ns |     3.529 ns |     1.46 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    14.022 ns |     25.4199 ns |     1.3933 ns |    14.044 ns |     5.81 |    0.56 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    21.575 ns |    105.5279 ns |     5.7843 ns |    18.296 ns |     8.94 |    2.11 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **2.698 ns** |      **0.3555 ns** |     **0.0195 ns** |     **2.690 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     2.001 ns |      0.4344 ns |     0.0238 ns |     1.997 ns |     0.74 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     2.981 ns |      0.5239 ns |     0.0287 ns |     2.985 ns |     1.10 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Ascii  |    10.776 ns |     68.9745 ns |     3.7807 ns |     8.690 ns |     3.99 |    1.21 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Ascii  |     9.080 ns |      8.5716 ns |     0.4698 ns |     9.159 ns |     3.37 |    0.15 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   331.797 ns |  1,965.0652 ns |   107.7119 ns |   270.496 ns |   122.97 |   34.58 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   102.707 ns |    121.9636 ns |     6.6852 ns |   100.032 ns |    38.07 |    2.16 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   119.936 ns |    618.0007 ns |    33.8747 ns |   100.745 ns |    44.45 |   10.88 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   111.389 ns |     48.4130 ns |     2.6537 ns |   112.308 ns |    41.28 |    0.89 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   106.087 ns |     11.7531 ns |     0.6442 ns |   105.954 ns |    39.32 |    0.32 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **1.928 ns** |      **0.3720 ns** |     **0.0204 ns** |     **1.922 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     1.884 ns |      9.1477 ns |     0.5014 ns |     1.617 ns |     0.98 |    0.23 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     2.744 ns |      0.3419 ns |     0.0187 ns |     2.737 ns |     1.42 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    24.435 ns |     10.7171 ns |     0.5874 ns |    24.407 ns |    12.68 |    0.29 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    19.123 ns |      2.2325 ns |     0.1224 ns |    19.166 ns |     9.92 |    0.11 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   291.443 ns |    667.6002 ns |    36.5934 ns |   272.011 ns |   151.19 |   16.50 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   296.331 ns |     44.8491 ns |     2.4583 ns |   295.123 ns |   153.73 |    1.79 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   292.227 ns |     67.5011 ns |     3.7000 ns |   292.278 ns |   151.60 |    2.16 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   299.314 ns |    284.2342 ns |    15.5798 ns |   291.516 ns |   155.28 |    7.14 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   308.625 ns |     68.0049 ns |     3.7276 ns |   307.365 ns |   160.11 |    2.22 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **2.303 ns** |      **0.3714 ns** |     **0.0204 ns** |     **2.299 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     1.935 ns |      0.4262 ns |     0.0234 ns |     1.937 ns |     0.84 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     2.849 ns |      0.3708 ns |     0.0203 ns |     2.852 ns |     1.24 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Mixed  |     9.091 ns |      2.6866 ns |     0.1473 ns |     9.141 ns |     3.95 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Mixed  |     8.906 ns |      0.0303 ns |     0.0017 ns |     8.907 ns |     3.87 |    0.03 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   267.844 ns |     19.5710 ns |     1.0728 ns |   268.193 ns |   116.32 |    0.98 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   158.083 ns |    969.8715 ns |    53.1619 ns |   127.679 ns |    68.65 |   20.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   127.859 ns |      6.9592 ns |     0.3815 ns |   127.814 ns |    55.53 |    0.45 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   139.628 ns |     99.3152 ns |     5.4438 ns |   136.679 ns |    60.64 |    2.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   174.636 ns |    137.8398 ns |     7.5555 ns |   178.300 ns |    75.84 |    2.90 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **2.659 ns** |      **0.3080 ns** |     **0.0169 ns** |     **2.660 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     2.526 ns |     16.6043 ns |     0.9101 ns |     2.024 ns |     0.95 |    0.30 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     2.995 ns |      0.2715 ns |     0.0149 ns |     2.994 ns |     1.13 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Ascii  |     8.622 ns |      0.9821 ns |     0.0538 ns |     8.638 ns |     3.24 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Ascii  |     8.885 ns |      9.9277 ns |     0.5442 ns |     8.614 ns |     3.34 |    0.18 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,173.859 ns |  1,661.1161 ns |    91.0514 ns | 4,123.553 ns | 1,569.73 |   30.89 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 1,515.119 ns |    137.7470 ns |     7.5504 ns | 1,519.466 ns |   569.81 |    3.98 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 1,466.878 ns |    189.8040 ns |    10.4038 ns | 1,463.345 ns |   551.67 |    4.55 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 1,500.600 ns |    100.5739 ns |     5.5128 ns | 1,501.473 ns |   564.35 |    3.59 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 1,562.561 ns |    161.6203 ns |     8.8590 ns | 1,557.845 ns |   587.66 |    4.33 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **2.003 ns** |      **0.3862 ns** |     **0.0212 ns** |     **1.991 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     1.672 ns |      0.3587 ns |     0.0197 ns |     1.676 ns |     0.83 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     3.074 ns |      4.2237 ns |     0.2315 ns |     3.017 ns |     1.53 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    24.498 ns |      4.0666 ns |     0.2229 ns |    24.579 ns |    12.23 |    0.15 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    22.172 ns |     68.9048 ns |     3.7769 ns |    20.648 ns |    11.07 |    1.64 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,157.499 ns |    296.7580 ns |    16.2663 ns | 4,163.955 ns | 2,075.52 |   20.15 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 4,447.633 ns |    939.0312 ns |    51.4715 ns | 4,467.082 ns | 2,220.36 |   30.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 4,729.489 ns |  3,931.8520 ns |   215.5181 ns | 4,630.035 ns | 2,361.07 |   95.62 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 4,525.423 ns |    854.5022 ns |    46.8381 ns | 4,545.962 ns | 2,259.19 |   28.85 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 4,524.185 ns |  1,126.4451 ns |    61.7443 ns | 4,496.634 ns | 2,258.57 |   33.68 |         - |          NA |
|                                |       |        |              |                |               |              |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **2.222 ns** |      **0.4875 ns** |     **0.0267 ns** |     **2.235 ns** |     **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     1.998 ns |      0.3285 ns |     0.0180 ns |     1.989 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     3.002 ns |      0.6166 ns |     0.0338 ns |     2.986 ns |     1.35 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Mixed  |     9.061 ns |      0.5257 ns |     0.0288 ns |     9.067 ns |     4.08 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Mixed  |     8.986 ns |      0.8276 ns |     0.0454 ns |     8.985 ns |     4.05 |    0.05 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 4,185.126 ns |    230.5417 ns |    12.6368 ns | 4,180.255 ns | 1,884.03 |   20.37 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,547.255 ns | 18,680.0967 ns | 1,023.9191 ns | 1,965.203 ns | 1,146.70 |  399.39 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 1,968.961 ns |    340.4021 ns |    18.6586 ns | 1,959.971 ns |   886.37 |   11.80 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 1,990.597 ns |    308.0705 ns |    16.8864 ns | 1,996.974 ns |   896.11 |   11.48 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,723.463 ns | 20,882.5755 ns | 1,144.6444 ns | 2,067.108 ns | 1,226.03 |  446.46 |         - |          NA |
