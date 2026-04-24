```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                                     | PartSize | Parts | Locale | Mean          | Error         | StdDev        | Gen0   | Gen1   | Gen2   | Allocated |
|------------------------------------------- |--------- |------ |------- |--------------:|--------------:|--------------:|-------:|-------:|-------:|----------:|
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **2**     | **Ascii**  |      **19.44 ns** |      **0.290 ns** |      **0.271 ns** | **0.0037** |      **-** |      **-** |      **32 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 2     | Ascii  |      38.74 ns |      0.569 ns |      0.475 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 2     | Ascii  |      20.14 ns |      0.177 ns |      0.157 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 2     | Ascii  |      39.07 ns |      0.530 ns |      0.496 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 2     | Ascii  |      19.70 ns |      0.197 ns |      0.184 ns | 0.0037 |      - |      - |      32 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 2     | Ascii  |      40.61 ns |      0.457 ns |      0.427 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **2**     | **Cjk**    |      **19.37 ns** |      **0.158 ns** |      **0.140 ns** | **0.0038** |      **-** |      **-** |      **32 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 2     | Cjk    |      38.36 ns |      0.618 ns |      0.548 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 2     | Cjk    |      19.90 ns |      0.287 ns |      0.268 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 2     | Cjk    |      41.79 ns |      0.725 ns |      0.678 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 2     | Cjk    |      19.55 ns |      0.191 ns |      0.178 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 2     | Cjk    |      39.96 ns |      0.345 ns |      0.306 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **2**     | **Emoji**  |      **19.23 ns** |      **0.214 ns** |      **0.200 ns** | **0.0038** |      **-** |      **-** |      **32 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 2     | Emoji  |      38.51 ns |      0.454 ns |      0.379 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 2     | Emoji  |      19.76 ns |      0.236 ns |      0.221 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 2     | Emoji  |      41.35 ns |      0.421 ns |      0.352 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 2     | Emoji  |      19.69 ns |      0.238 ns |      0.223 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 2     | Emoji  |      40.21 ns |      0.415 ns |      0.388 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **2**     | **Mixed**  |      **19.20 ns** |      **0.169 ns** |      **0.158 ns** | **0.0037** |      **-** |      **-** |      **32 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 2     | Mixed  |      38.43 ns |      0.395 ns |      0.369 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 2     | Mixed  |      20.09 ns |      0.250 ns |      0.234 ns | 0.0038 |      - |      - |      32 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 2     | Mixed  |      39.04 ns |      0.351 ns |      0.312 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 2     | Mixed  |      19.82 ns |      0.305 ns |      0.286 ns | 0.0037 |      - |      - |      32 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 2     | Mixed  |      39.94 ns |      0.618 ns |      0.548 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **4**     | **Ascii**  |      **28.10 ns** |      **0.329 ns** |      **0.292 ns** | **0.0046** |      **-** |      **-** |      **40 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 4     | Ascii  |      45.44 ns |      0.544 ns |      0.483 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 4     | Ascii  |      29.42 ns |      0.570 ns |      0.505 ns | 0.0047 |      - |      - |      40 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 4     | Ascii  |      46.66 ns |      0.599 ns |      0.560 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 4     | Ascii  |      29.22 ns |      0.504 ns |      0.420 ns | 0.0047 |      - |      - |      40 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 4     | Ascii  |      49.63 ns |      0.775 ns |      0.725 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **4**     | **Cjk**    |      **28.39 ns** |      **0.269 ns** |      **0.251 ns** | **0.0047** |      **-** |      **-** |      **40 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 4     | Cjk    |      45.83 ns |      0.765 ns |      0.716 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 4     | Cjk    |      29.93 ns |      0.424 ns |      0.376 ns | 0.0046 |      - |      - |      40 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 4     | Cjk    |      51.76 ns |      0.748 ns |      0.700 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 4     | Cjk    |      28.38 ns |      0.351 ns |      0.311 ns | 0.0047 |      - |      - |      40 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 4     | Cjk    |      48.99 ns |      0.737 ns |      0.689 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **4**     | **Emoji**  |      **28.26 ns** |      **0.492 ns** |      **0.460 ns** | **0.0047** |      **-** |      **-** |      **40 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 4     | Emoji  |      45.73 ns |      0.515 ns |      0.482 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 4     | Emoji  |      29.56 ns |      0.377 ns |      0.334 ns | 0.0047 |      - |      - |      40 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 4     | Emoji  |      50.54 ns |      0.502 ns |      0.470 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 4     | Emoji  |      28.21 ns |      0.250 ns |      0.221 ns | 0.0048 |      - |      - |      40 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 4     | Emoji  |      48.65 ns |      0.410 ns |      0.363 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **4**     | **Mixed**  |      **28.16 ns** |      **0.236 ns** |      **0.221 ns** | **0.0046** |      **-** |      **-** |      **40 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 4     | Mixed  |      45.07 ns |      0.415 ns |      0.389 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 4     | Mixed  |      29.07 ns |      0.128 ns |      0.119 ns | 0.0047 |      - |      - |      40 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 4     | Mixed  |      46.59 ns |      0.323 ns |      0.302 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 4     | Mixed  |      29.54 ns |      0.531 ns |      0.471 ns | 0.0046 |      - |      - |      40 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 4     | Mixed  |      49.39 ns |      0.821 ns |      0.768 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **16**    | **Ascii**  |      **78.79 ns** |      **1.119 ns** |      **1.047 ns** | **0.0100** |      **-** |      **-** |      **88 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 16    | Ascii  |      95.15 ns |      1.289 ns |      1.206 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 16    | Ascii  |      83.75 ns |      0.984 ns |      0.920 ns | 0.0104 |      - |      - |      88 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 16    | Ascii  |     100.16 ns |      2.791 ns |      2.611 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 16    | Ascii  |      83.35 ns |      0.529 ns |      0.469 ns | 0.0104 |      - |      - |      88 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 16    | Ascii  |     103.01 ns |      0.658 ns |      0.615 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **16**    | **Cjk**    |      **77.51 ns** |      **1.176 ns** |      **1.100 ns** | **0.0102** |      **-** |      **-** |      **88 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 16    | Cjk    |      88.00 ns |      0.931 ns |      0.871 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 16    | Cjk    |      90.00 ns |      0.801 ns |      0.710 ns | 0.0102 |      - |      - |      88 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 16    | Cjk    |     111.48 ns |      1.617 ns |      1.512 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 16    | Cjk    |      82.42 ns |      0.434 ns |      0.385 ns | 0.0100 |      - |      - |      88 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 16    | Cjk    |     101.63 ns |      0.540 ns |      0.505 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **16**    | **Emoji**  |      **77.26 ns** |      **1.305 ns** |      **1.220 ns** | **0.0104** |      **-** |      **-** |      **88 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 16    | Emoji  |      88.64 ns |      0.940 ns |      0.879 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 16    | Emoji  |      89.82 ns |      1.111 ns |      1.039 ns | 0.0100 |      - |      - |      88 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 16    | Emoji  |     109.89 ns |      0.968 ns |      0.906 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 16    | Emoji  |      82.45 ns |      0.212 ns |      0.177 ns | 0.0103 |      - |      - |      88 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 16    | Emoji  |     102.56 ns |      0.845 ns |      0.790 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **16**    | **Mixed**  |      **78.13 ns** |      **1.145 ns** |      **1.071 ns** | **0.0105** |      **-** |      **-** |      **88 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 16    | Mixed  |      88.07 ns |      0.567 ns |      0.502 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 16    | Mixed  |      85.48 ns |      0.441 ns |      0.413 ns | 0.0101 |      - |      - |      88 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 16    | Mixed  |     103.45 ns |      0.664 ns |      0.588 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 16    | Mixed  |      83.33 ns |      0.480 ns |      0.449 ns | 0.0104 |      - |      - |      88 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 16    | Mixed  |     103.35 ns |      1.167 ns |      0.974 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **64**    | **Ascii**  |     **261.86 ns** |      **1.593 ns** |      **1.490 ns** | **0.0332** |      **-** |      **-** |     **280 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 64    | Ascii  |     260.79 ns |      4.879 ns |      4.325 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 64    | Ascii  |     296.72 ns |      1.201 ns |      1.003 ns | 0.0333 |      - |      - |     280 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 64    | Ascii  |     310.83 ns |      3.233 ns |      3.024 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 64    | Ascii  |     291.02 ns |      2.782 ns |      2.323 ns | 0.0331 |      - |      - |     280 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 64    | Ascii  |     301.44 ns |      3.117 ns |      2.916 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **64**    | **Cjk**    |     **263.71 ns** |      **2.264 ns** |      **2.007 ns** | **0.0332** |      **-** |      **-** |     **280 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 64    | Cjk    |     259.03 ns |      3.847 ns |      3.598 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 64    | Cjk    |     331.31 ns |      1.391 ns |      1.233 ns | 0.0326 |      - |      - |     280 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 64    | Cjk    |     320.02 ns |      3.289 ns |      3.077 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 64    | Cjk    |     292.13 ns |      1.220 ns |      1.081 ns | 0.0329 |      - |      - |     280 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 64    | Cjk    |     296.36 ns |      2.293 ns |      2.033 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **64**    | **Emoji**  |     **255.12 ns** |      **2.253 ns** |      **2.107 ns** | **0.0320** |      **-** |      **-** |     **280 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 64    | Emoji  |     261.59 ns |      2.109 ns |      1.973 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 64    | Emoji  |     333.08 ns |      4.273 ns |      3.997 ns | 0.0335 |      - |      - |     280 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 64    | Emoji  |     324.19 ns |      4.759 ns |      4.451 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 64    | Emoji  |     301.94 ns |      2.467 ns |      2.308 ns | 0.0321 |      - |      - |     280 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 64    | Emoji  |     303.37 ns |      2.447 ns |      2.289 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **64**    | **Mixed**  |     **271.07 ns** |      **2.722 ns** |      **2.547 ns** | **0.0319** |      **-** |      **-** |     **280 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 64    | Mixed  |     265.63 ns |      2.258 ns |      2.112 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 64    | Mixed  |     311.86 ns |      2.260 ns |      2.114 ns | 0.0314 |      - |      - |     280 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 64    | Mixed  |     316.91 ns |      2.249 ns |      2.104 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 64    | Mixed  |     294.49 ns |      1.345 ns |      1.192 ns | 0.0334 |      - |      - |     280 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 64    | Mixed  |     302.64 ns |      3.520 ns |      3.120 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **256**   | **Ascii**  |   **1,071.02 ns** |     **11.916 ns** |     **10.563 ns** | **0.1205** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 256   | Ascii  |     977.18 ns |     14.377 ns |     12.745 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 256   | Ascii  |   1,207.29 ns |      6.700 ns |      6.267 ns | 0.1205 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 256   | Ascii  |   1,169.08 ns |      8.441 ns |      7.896 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 256   | Ascii  |   1,160.91 ns |      4.858 ns |      4.306 ns | 0.1240 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 256   | Ascii  |   1,108.05 ns |      9.548 ns |      8.931 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **256**   | **Cjk**    |   **1,039.17 ns** |      **6.942 ns** |      **6.494 ns** | **0.1243** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 256   | Cjk    |     982.61 ns |     14.893 ns |     13.931 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 256   | Cjk    |   1,305.41 ns |     16.793 ns |     15.708 ns | 0.1193 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 256   | Cjk    |   1,246.13 ns |     11.899 ns |     11.130 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 256   | Cjk    |   1,183.35 ns |     19.776 ns |     17.531 ns | 0.1245 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 256   | Cjk    |   1,128.33 ns |     14.075 ns |     13.166 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **256**   | **Emoji**  |   **1,046.10 ns** |     **22.809 ns** |     **21.335 ns** | **0.1246** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 256   | Emoji  |     981.95 ns |     10.734 ns |     10.041 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 256   | Emoji  |   1,306.47 ns |     20.001 ns |     18.709 ns | 0.1196 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 256   | Emoji  |   1,262.34 ns |     22.041 ns |     20.617 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 256   | Emoji  |   1,195.30 ns |     11.350 ns |     10.617 ns | 0.1187 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 256   | Emoji  |   1,124.44 ns |      9.898 ns |      9.259 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1**        | **256**   | **Mixed**  |   **1,049.38 ns** |      **8.901 ns** |      **8.326 ns** | **0.1246** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1        | 256   | Mixed  |     995.36 ns |     21.254 ns |     19.881 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1        | 256   | Mixed  |   1,256.25 ns |      7.628 ns |      7.135 ns | 0.1193 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1        | 256   | Mixed  |   1,219.28 ns |      8.868 ns |      8.295 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1        | 256   | Mixed  |   1,179.10 ns |      9.256 ns |      8.658 ns | 0.1177 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1        | 256   | Mixed  |   1,139.75 ns |      5.842 ns |      5.465 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **2**     | **Ascii**  |      **56.13 ns** |      **0.656 ns** |      **0.614 ns** | **0.0637** |      **-** |      **-** |     **536 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 2     | Ascii  |      36.38 ns |      0.244 ns |      0.217 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 2     | Ascii  |     104.80 ns |      3.144 ns |      2.787 ns | 0.0641 |      - |      - |     536 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 2     | Ascii  |      84.85 ns |      3.171 ns |      2.966 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 2     | Ascii  |     163.17 ns |      1.611 ns |      1.507 ns | 0.0635 |      - |      - |     536 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 2     | Ascii  |     142.17 ns |      3.064 ns |      2.866 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **2**     | **Cjk**    |      **59.16 ns** |      **1.448 ns** |      **1.354 ns** | **0.0638** |      **-** |      **-** |     **536 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 2     | Cjk    |      36.97 ns |      0.375 ns |      0.351 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 2     | Cjk    |     220.42 ns |      3.848 ns |      3.600 ns | 0.0635 |      - |      - |     536 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 2     | Cjk    |     205.17 ns |      2.994 ns |      2.800 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 2     | Cjk    |     173.15 ns |      2.416 ns |      2.017 ns | 0.0634 |      - |      - |     536 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 2     | Cjk    |     142.18 ns |      1.214 ns |      1.136 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **2**     | **Emoji**  |      **35.30 ns** |      **0.595 ns** |      **0.557 ns** | **0.0333** |      **-** |      **-** |     **280 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 2     | Emoji  |      41.45 ns |      0.405 ns |      0.338 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 2     | Emoji  |     151.78 ns |      3.700 ns |      3.461 ns | 0.0332 |      - |      - |     280 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 2     | Emoji  |     144.12 ns |      3.280 ns |      2.908 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 2     | Emoji  |      83.98 ns |      0.546 ns |      0.511 ns | 0.0329 |      - |      - |     280 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 2     | Emoji  |      75.68 ns |      0.667 ns |      0.591 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **2**     | **Mixed**  |      **53.42 ns** |      **0.943 ns** |      **0.882 ns** | **0.0609** |      **-** |      **-** |     **512 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 2     | Mixed  |      34.53 ns |      0.522 ns |      0.488 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 2     | Mixed  |     114.96 ns |      1.080 ns |      1.011 ns | 0.0611 |      - |      - |     512 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 2     | Mixed  |     100.35 ns |      1.641 ns |      1.535 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 2     | Mixed  |     154.24 ns |      3.163 ns |      2.804 ns | 0.0612 |      - |      - |     512 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 2     | Mixed  |     131.17 ns |      1.540 ns |      1.440 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **4**     | **Ascii**  |     **108.12 ns** |      **1.100 ns** |      **1.029 ns** | **0.1248** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 4     | Ascii  |      67.98 ns |      0.569 ns |      0.475 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 4     | Ascii  |     202.27 ns |      3.781 ns |      3.537 ns | 0.1246 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 4     | Ascii  |     160.21 ns |      2.818 ns |      2.636 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 4     | Ascii  |     312.46 ns |      3.840 ns |      3.207 ns | 0.1248 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 4     | Ascii  |     268.94 ns |      2.315 ns |      2.165 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **4**     | **Cjk**    |     **108.76 ns** |      **1.585 ns** |      **1.483 ns** | **0.1251** |      **-** |      **-** |    **1048 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 4     | Cjk    |      65.30 ns |      0.291 ns |      0.258 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 4     | Cjk    |     419.18 ns |      2.339 ns |      1.953 ns | 0.1251 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 4     | Cjk    |     391.11 ns |      5.366 ns |      5.019 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 4     | Cjk    |     306.03 ns |      1.917 ns |      1.793 ns | 0.1253 |      - |      - |    1048 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 4     | Cjk    |     264.54 ns |      2.668 ns |      2.496 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **4**     | **Emoji**  |      **62.37 ns** |      **0.551 ns** |      **0.460 ns** | **0.0637** |      **-** |      **-** |     **536 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 4     | Emoji  |      41.92 ns |      0.258 ns |      0.229 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 4     | Emoji  |     269.50 ns |      7.098 ns |      6.292 ns | 0.0636 |      - |      - |     536 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 4     | Emoji  |     248.02 ns |      8.735 ns |      7.294 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 4     | Emoji  |     159.61 ns |      2.072 ns |      1.938 ns | 0.0635 |      - |      - |     536 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 4     | Emoji  |     140.39 ns |      1.535 ns |      1.436 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **4**     | **Mixed**  |     **105.78 ns** |      **1.277 ns** |      **1.132 ns** | **0.1203** |      **-** |      **-** |    **1008 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 4     | Mixed  |      62.38 ns |      0.360 ns |      0.319 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 4     | Mixed  |     229.59 ns |      3.940 ns |      3.685 ns | 0.1205 |      - |      - |    1008 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 4     | Mixed  |     188.74 ns |      3.163 ns |      2.804 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 4     | Mixed  |     303.82 ns |      6.631 ns |      5.879 ns | 0.1187 |      - |      - |    1008 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 4     | Mixed  |     257.74 ns |      2.873 ns |      2.547 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **16**    | **Ascii**  |     **360.04 ns** |      **4.491 ns** |      **3.981 ns** | **0.4918** | **0.0074** |      **-** |    **4120 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 16    | Ascii  |     203.01 ns |      1.900 ns |      1.685 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 16    | Ascii  |     753.30 ns |      5.235 ns |      4.372 ns | 0.4890 | 0.0050 |      - |    4120 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 16    | Ascii  |     634.33 ns |     26.843 ns |     25.109 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 16    | Ascii  |   1,196.86 ns |     23.316 ns |     20.669 ns | 0.4874 |      - |      - |    4120 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 16    | Ascii  |   1,014.96 ns |      8.832 ns |      8.261 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **16**    | **Cjk**    |     **357.01 ns** |      **3.200 ns** |      **2.836 ns** | **0.4918** | **0.0073** |      **-** |    **4120 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 16    | Cjk    |     202.08 ns |      1.754 ns |      1.640 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 16    | Cjk    |   1,668.96 ns |     14.952 ns |     13.255 ns | 0.4869 |      - |      - |    4120 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 16    | Cjk    |   1,509.77 ns |     18.406 ns |     17.217 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 16    | Cjk    |   1,191.88 ns |     14.722 ns |     12.294 ns | 0.4870 |      - |      - |    4120 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 16    | Cjk    |   1,015.32 ns |      8.946 ns |      8.368 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **16**    | **Emoji**  |     **227.36 ns** |      **3.940 ns** |      **3.290 ns** | **0.2465** | **0.0015** |      **-** |    **2072 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 16    | Emoji  |     143.08 ns |      1.498 ns |      1.251 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 16    | Emoji  |   1,079.65 ns |     50.574 ns |     47.307 ns | 0.2459 |      - |      - |    2072 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 16    | Emoji  |     982.76 ns |      8.880 ns |      8.307 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 16    | Emoji  |     601.73 ns |      5.754 ns |      5.101 ns | 0.2474 |      - |      - |    2072 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 16    | Emoji  |     536.53 ns |      7.223 ns |      6.757 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **16**    | **Mixed**  |     **372.02 ns** |      **9.607 ns** |      **8.022 ns** | **0.4716** | **0.0052** |      **-** |    **3952 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 16    | Mixed  |     189.94 ns |      1.349 ns |      1.261 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 16    | Mixed  |     804.03 ns |     19.130 ns |     16.958 ns | 0.4680 | 0.0053 |      - |    3952 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 16    | Mixed  |     647.25 ns |     10.856 ns |     10.155 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 16    | Mixed  |   1,095.56 ns |     11.571 ns |     10.258 ns | 0.4675 |      - |      - |    3952 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 16    | Mixed  |     922.72 ns |      5.489 ns |      4.866 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **64**    | **Ascii**  |   **1,302.16 ns** |     **24.585 ns** |     **21.794 ns** | **1.9510** | **0.1108** |      **-** |   **16408 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 64    | Ascii  |     748.46 ns |     12.316 ns |     11.520 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 64    | Ascii  |   2,797.49 ns |     46.633 ns |     43.620 ns | 1.9463 | 0.1091 |      - |   16408 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 64    | Ascii  |   2,322.67 ns |     68.415 ns |     63.995 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 64    | Ascii  |   4,485.73 ns |     39.096 ns |     34.657 ns | 1.9421 | 0.0883 |      - |   16408 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 64    | Ascii  |   3,948.53 ns |     92.039 ns |     81.590 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **64**    | **Cjk**    |   **1,381.10 ns** |    **124.547 ns** |    **116.502 ns** | **1.9550** | **0.1095** |      **-** |   **16408 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 64    | Cjk    |     736.78 ns |     18.358 ns |     17.172 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 64    | Cjk    |   6,612.60 ns |     77.271 ns |     64.525 ns | 1.9491 | 0.0866 |      - |   16408 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 64    | Cjk    |   5,917.30 ns |     51.532 ns |     48.203 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 64    | Cjk    |   4,484.44 ns |     97.034 ns |     86.018 ns | 1.9504 | 0.0887 |      - |   16408 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 64    | Cjk    |   3,828.98 ns |     28.406 ns |     25.182 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **64**    | **Emoji**  |     **837.72 ns** |     **17.781 ns** |     **14.848 ns** | **0.9781** | **0.0275** |      **-** |    **8216 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 64    | Emoji  |     503.15 ns |      5.254 ns |      4.914 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 64    | Emoji  |   3,983.54 ns |    127.572 ns |    119.331 ns | 0.9570 | 0.0266 |      - |    8216 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 64    | Emoji  |   3,743.15 ns |     25.877 ns |     24.206 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 64    | Emoji  |   2,267.47 ns |      5.207 ns |      4.065 ns | 0.9782 | 0.0150 |      - |    8216 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 64    | Emoji  |   1,956.31 ns |     11.058 ns |      9.803 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **64**    | **Mixed**  |   **1,214.29 ns** |     **29.984 ns** |     **28.047 ns** | **1.8705** | **0.0998** |      **-** |   **15720 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 64    | Mixed  |     698.39 ns |      9.870 ns |      9.232 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 64    | Mixed  |   3,066.87 ns |     78.346 ns |     69.452 ns | 1.8740 | 0.0997 |      - |   15720 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 64    | Mixed  |   2,581.81 ns |     72.954 ns |     68.241 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 64    | Mixed  |   4,298.80 ns |    138.919 ns |    123.148 ns | 1.8619 | 0.0834 |      - |   15720 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 64    | Mixed  |   3,616.45 ns |     54.066 ns |     50.573 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **256**   | **Ascii**  |   **5,615.33 ns** |    **158.591 ns** |    **148.346 ns** | **7.7855** | **1.5499** |      **-** |   **65560 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 256   | Ascii  |   3,407.62 ns |     68.343 ns |     63.928 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 256   | Ascii  |  11,591.78 ns |    211.420 ns |    197.763 ns | 7.8029 | 1.5300 |      - |   65560 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 256   | Ascii  |   8,737.36 ns |     81.382 ns |     67.957 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 256   | Ascii  |  17,749.17 ns |    292.292 ns |    259.109 ns | 7.7684 | 1.5301 |      - |   65561 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 256   | Ascii  |  15,499.86 ns |    314.368 ns |    294.060 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **256**   | **Cjk**    |   **5,637.79 ns** |    **143.149 ns** |    **126.898 ns** | **7.7802** | **1.5487** |      **-** |   **65560 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 256   | Cjk    |   3,530.59 ns |     45.551 ns |     42.608 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 256   | Cjk    |  27,458.48 ns |    700.241 ns |    655.006 ns | 7.7006 | 1.4327 |      - |   65560 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 256   | Cjk    |  24,648.59 ns |    410.289 ns |    383.785 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 256   | Cjk    |  18,494.19 ns |    279.380 ns |    247.663 ns | 7.7381 | 1.5476 |      - |   65561 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 256   | Cjk    |  15,418.83 ns |    119.204 ns |    111.503 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **256**   | **Emoji**  |   **3,521.79 ns** |     **46.207 ns** |     **43.222 ns** | **3.9018** | **0.4282** |      **-** |   **32792 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 256   | Emoji  |   2,405.73 ns |     18.681 ns |     16.560 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 256   | Emoji  |  16,463.70 ns |    322.679 ns |    301.834 ns | 3.8660 | 0.4296 |      - |   32793 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 256   | Emoji  |  14,787.22 ns |    250.096 ns |    233.940 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 256   | Emoji  |  10,108.66 ns |    111.668 ns |    104.454 ns | 3.8812 | 0.4015 |      - |   32792 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 256   | Emoji  |   7,652.62 ns |     60.825 ns |     53.919 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **64**       | **256**   | **Mixed**  |   **5,623.32 ns** |    **370.264 ns** |    **328.229 ns** | **7.4405** | **1.2112** |      **-** |   **62800 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 64       | 256   | Mixed  |   3,272.30 ns |     51.225 ns |     47.916 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 64       | 256   | Mixed  |  13,053.11 ns |    175.005 ns |    163.700 ns | 7.4425 | 1.1840 |      - |   62801 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 64       | 256   | Mixed  |  10,352.97 ns |    231.257 ns |    216.318 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 64       | 256   | Mixed  |  16,891.02 ns |    310.905 ns |    290.820 ns | 7.3925 | 1.2321 |      - |   62801 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 64       | 256   | Mixed  |  14,496.29 ns |    287.216 ns |    268.662 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **2**     | **Ascii**  |     **515.25 ns** |     **18.286 ns** |     **14.276 ns** | **0.9804** | **0.0286** |      **-** |    **8216 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 2     | Ascii  |     163.76 ns |      6.182 ns |      5.783 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 2     | Ascii  |   1,441.74 ns |     19.031 ns |     17.801 ns | 0.9811 | 0.0283 |      - |    8216 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 2     | Ascii  |   1,123.76 ns |     16.649 ns |     15.573 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 2     | Ascii  |   2,418.30 ns |     37.477 ns |     35.056 ns | 0.9684 | 0.0159 |      - |    8216 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 2     | Ascii  |   2,120.23 ns |     35.188 ns |     32.915 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **2**     | **Cjk**    |     **481.17 ns** |      **6.877 ns** |      **6.433 ns** | **0.9792** | **0.0280** |      **-** |    **8216 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 2     | Cjk    |     195.00 ns |      7.568 ns |      7.079 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 2     | Cjk    |   3,083.80 ns |    119.644 ns |    106.061 ns | 0.9704 | 0.0206 |      - |    8216 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 2     | Cjk    |   2,610.91 ns |     35.291 ns |     33.011 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 2     | Cjk    |   2,533.75 ns |     52.738 ns |     49.331 ns | 0.9768 | 0.0178 |      - |    8216 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 2     | Cjk    |   2,163.09 ns |     42.578 ns |     39.828 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **2**     | **Emoji**  |     **271.58 ns** |     **24.960 ns** |     **20.842 ns** | **0.4912** | **0.0069** |      **-** |    **4120 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 2     | Emoji  |      93.28 ns |      2.262 ns |      2.116 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 2     | Emoji  |   1,635.37 ns |     76.628 ns |     67.929 ns | 0.4908 |      - |      - |    4120 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 2     | Emoji  |   1,500.89 ns |     31.834 ns |     29.777 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 2     | Emoji  |     971.99 ns |     13.069 ns |     12.225 ns | 0.4889 | 0.0071 |      - |    4120 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 2     | Emoji  |     795.55 ns |      6.252 ns |      5.848 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **2**     | **Mixed**  |     **447.63 ns** |      **9.138 ns** |      **8.100 ns** | **0.9397** | **0.0262** |      **-** |    **7864 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 2     | Mixed  |     159.53 ns |      6.637 ns |      5.884 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 2     | Mixed  |   1,404.34 ns |     51.242 ns |     45.425 ns | 0.9315 | 0.0183 |      - |    7864 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 2     | Mixed  |   1,107.26 ns |     37.707 ns |     35.271 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 2     | Mixed  |   2,218.26 ns |     17.297 ns |     15.334 ns | 0.9295 | 0.0150 |      - |    7864 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 2     | Mixed  |   1,938.16 ns |     27.664 ns |     24.523 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **4**     | **Ascii**  |     **844.25 ns** |     **11.265 ns** |      **9.986 ns** | **1.9561** | **0.1118** |      **-** |   **16408 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 4     | Ascii  |     397.36 ns |     11.362 ns |     10.628 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 4     | Ascii  |   2,706.67 ns |     10.475 ns |      8.747 ns | 1.9503 | 0.1104 |      - |   16408 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 4     | Ascii  |   2,233.37 ns |     24.704 ns |     21.899 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 4     | Ascii  |   4,652.53 ns |     69.903 ns |     58.372 ns | 1.9502 | 0.0929 |      - |   16408 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 4     | Ascii  |   4,076.27 ns |     35.673 ns |     29.788 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **4**     | **Cjk**    |     **829.35 ns** |     **14.288 ns** |     **11.931 ns** | **1.9535** | **0.1101** |      **-** |   **16408 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 4     | Cjk    |     375.78 ns |      7.493 ns |      6.642 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 4     | Cjk    |   5,728.69 ns |    102.260 ns |     90.651 ns | 1.9248 | 0.1132 |      - |   16408 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 4     | Cjk    |   5,147.19 ns |     62.986 ns |     58.917 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 4     | Cjk    |   4,604.15 ns |     28.612 ns |     25.364 ns | 1.9292 | 0.0919 |      - |   16408 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 4     | Cjk    |   4,140.26 ns |     81.368 ns |     76.111 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **4**     | **Emoji**  |     **484.98 ns** |      **9.908 ns** |      **7.735 ns** | **0.9800** | **0.0290** |      **-** |    **8216 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 4     | Emoji  |     188.96 ns |      3.731 ns |      3.490 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 4     | Emoji  |   3,397.74 ns |    119.760 ns |    106.164 ns | 0.9735 | 0.0216 |      - |    8216 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 4     | Emoji  |   2,995.82 ns |     85.620 ns |     80.089 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 4     | Emoji  |   1,908.55 ns |     25.171 ns |     22.313 ns | 0.9780 | 0.0254 |      - |    8216 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 4     | Emoji  |   1,626.40 ns |     22.146 ns |     20.716 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **4**     | **Mixed**  |     **802.54 ns** |     **16.799 ns** |     **14.028 ns** | **1.8706** | **0.1018** |      **-** |   **15696 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 4     | Mixed  |     359.30 ns |     14.228 ns |     13.308 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 4     | Mixed  |   2,676.57 ns |     15.310 ns |     13.572 ns | 1.8633 | 0.0887 |      - |   15696 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 4     | Mixed  |   2,215.28 ns |     51.924 ns |     43.359 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 4     | Mixed  |   4,294.07 ns |     50.755 ns |     47.476 ns | 1.8618 | 0.0859 |      - |   15696 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 4     | Mixed  |   3,793.33 ns |     35.967 ns |     31.884 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **16**    | **Ascii**  |   **4,129.60 ns** |     **94.936 ns** |     **88.803 ns** | **7.7992** | **1.5439** |      **-** |   **65560 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 16    | Ascii  |   1,817.66 ns |     22.054 ns |     18.416 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 16    | Ascii  |  10,919.16 ns |     87.869 ns |     77.894 ns | 7.7485 | 1.5351 |      - |   65561 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 16    | Ascii  |   8,718.15 ns |     82.786 ns |     73.388 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 16    | Ascii  |  20,090.47 ns |  1,593.766 ns |  1,412.832 ns | 7.7320 | 1.5464 |      - |   65561 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 16    | Ascii  |  16,928.13 ns |    529.001 ns |    494.828 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **16**    | **Cjk**    |   **4,563.16 ns** |    **118.311 ns** |    **110.668 ns** | **7.8087** | **1.5617** |      **-** |   **65560 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 16    | Cjk    |   2,198.75 ns |     56.878 ns |     53.204 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 16    | Cjk    |  24,607.88 ns |    521.422 ns |    487.738 ns | 7.6531 | 1.4349 |      - |   65561 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 16    | Cjk    |  21,742.12 ns |    551.710 ns |    516.070 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 16    | Cjk    |  19,558.87 ns |    554.995 ns |    519.143 ns | 7.7191 | 1.4940 |      - |   65561 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 16    | Cjk    |  16,196.17 ns |    150.838 ns |    133.714 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **16**    | **Emoji**  |   **1,668.46 ns** |     **28.135 ns** |     **26.317 ns** | **3.9022** | **0.4324** |      **-** |   **32792 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 16    | Emoji  |     756.40 ns |     11.756 ns |     10.421 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 16    | Emoji  |  13,196.96 ns |    473.970 ns |    443.352 ns | 3.8842 | 0.3531 |      - |   32793 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 16    | Emoji  |  12,213.66 ns |    378.413 ns |    335.453 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 16    | Emoji  |   7,823.58 ns |    122.371 ns |    114.466 ns | 3.8644 | 0.4122 |      - |   32792 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 16    | Emoji  |   6,431.34 ns |     65.267 ns |     57.857 ns |      - |      - |      - |         - |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **16**    | **Mixed**  |   **4,253.33 ns** |    **103.633 ns** |     **96.939 ns** | **7.4569** | **1.2428** |      **-** |   **62720 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 16    | Mixed  |   2,105.50 ns |     34.177 ns |     30.297 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 16    | Mixed  |  10,876.57 ns |    263.360 ns |    246.347 ns | 7.4405 | 1.1905 |      - |   62721 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 16    | Mixed  |   8,596.46 ns |    122.450 ns |    108.548 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 16    | Mixed  |  18,473.98 ns |    671.324 ns |    627.957 ns | 7.3950 | 1.1927 |      - |   62721 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 16    | Mixed  |  15,645.66 ns |    303.290 ns |    268.858 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **64**    | **Ascii**  |  **98,828.12 ns** |  **3,284.271 ns** |  **3,072.109 ns** | **1.0331** | **1.0331** | **1.0331** |  **262179 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 64    | Ascii  |   9,031.19 ns |    263.672 ns |    246.639 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 64    | Ascii  | 110,635.95 ns |  2,665.527 ns |  2,493.336 ns | 0.7353 | 0.7353 | 0.7353 |  262179 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 64    | Ascii  |  37,512.25 ns |    848.406 ns |    793.600 ns |      - |      - |      - |       2 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 64    | Ascii  | 173,424.84 ns |  5,710.406 ns |  5,341.518 ns | 1.1161 | 1.1161 | 1.1161 |  262184 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 64    | Ascii  |  67,341.59 ns |  1,504.896 ns |  1,407.681 ns |      - |      - |      - |       3 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **64**    | **Cjk**    |  **96,900.35 ns** |  **3,139.774 ns** |  **2,936.947 ns** | **1.0000** | **1.0000** | **1.0000** |  **262179 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 64    | Cjk    |   9,021.61 ns |    235.560 ns |    220.343 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 64    | Cjk    | 167,010.78 ns |  6,759.500 ns |  5,992.121 ns | 0.7440 | 0.7440 | 0.7440 |  262179 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 64    | Cjk    |  90,038.73 ns |  1,713.810 ns |  1,603.099 ns |      - |      - |      - |       4 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 64    | Cjk    | 178,081.49 ns |  9,864.423 ns |  8,237.240 ns | 1.1161 | 1.1161 | 1.1161 |  262184 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 64    | Cjk    |  68,737.28 ns |  1,046.101 ns |    978.524 ns |      - |      - |      - |       3 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **64**    | **Emoji**  |  **49,544.41 ns** |    **834.768 ns** |    **697.069 ns** | **0.5342** | **0.5342** | **0.5342** |  **131102 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 64    | Emoji  |   4,500.46 ns |     62.015 ns |     58.009 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 64    | Emoji  |  93,057.01 ns |  3,177.428 ns |  2,816.707 ns | 0.4032 | 0.4032 | 0.4032 |  131102 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 64    | Emoji  |  51,606.98 ns |    732.915 ns |    685.569 ns |      - |      - |      - |       2 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 64    | Emoji  |  72,549.37 ns |  1,145.477 ns |  1,015.436 ns | 0.4735 | 0.4735 | 0.4735 |  131103 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 64    | Emoji  |  27,308.79 ns |    317.395 ns |    296.892 ns |      - |      - |      - |       1 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **64**    | **Mixed**  |  **91,626.82 ns** |  **1,892.736 ns** |  **1,677.861 ns** | **0.9615** | **0.9615** | **0.9615** |  **250803 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 64    | Mixed  |   9,409.61 ns |    243.962 ns |    216.266 ns |      - |      - |      - |         - |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 64    | Mixed  | 120,329.52 ns | 10,546.770 ns |  9,865.455 ns | 0.7622 | 0.7622 | 0.7622 |  250803 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 64    | Mixed  |  37,614.73 ns |    733.049 ns |    685.694 ns |      - |      - |      - |       2 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 64    | Mixed  | 166,578.66 ns | 16,560.026 ns | 16,264.160 ns | 0.8803 | 0.8803 | 0.8803 |  250801 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 64    | Mixed  |  63,841.56 ns |    638.277 ns |    565.816 ns |      - |      - |      - |       3 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **256**   | **Ascii**  | **449,693.98 ns** | **15,486.422 ns** | **14,486.009 ns** | **4.8077** | **4.8077** | **4.8077** | **1048653 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 256   | Ascii  |  38,239.20 ns |    421.823 ns |    394.573 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 256   | Ascii  | 466,545.75 ns | 14,957.192 ns | 13,990.966 ns | 3.1250 | 3.1250 | 3.1250 | 1048646 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 256   | Ascii  | 149,376.60 ns |  1,361.372 ns |  1,136.807 ns |      - |      - |      - |       7 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 256   | Ascii  | 676,019.44 ns | 11,108.014 ns | 10,390.443 ns | 4.4643 | 4.4643 | 4.4643 | 1048666 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 256   | Ascii  | 268,971.74 ns |  2,334.728 ns |  2,069.676 ns |      - |      - |      - |      13 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **256**   | **Cjk**    | **374,309.35 ns** |  **8,357.335 ns** |  **7,817.456 ns** | **4.0323** | **4.0323** | **4.0323** | **1048644 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 256   | Cjk    |  38,767.71 ns |    385.987 ns |    361.053 ns |      - |      - |      - |       2 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 256   | Cjk    | 645,975.25 ns | 14,375.295 ns | 13,446.660 ns | 2.9762 | 2.9762 | 2.9762 | 1048636 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 256   | Cjk    | 355,618.85 ns |  6,540.360 ns |  5,797.859 ns |      - |      - |      - |      17 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 256   | Cjk    | 656,725.09 ns | 22,362.012 ns | 19,823.342 ns | 4.1667 | 4.1667 | 4.1667 | 1048652 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 256   | Cjk    | 268,324.38 ns |  3,077.830 ns |  2,879.004 ns |      - |      - |      - |      13 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **256**   | **Emoji**  | **187,115.12 ns** |  **2,031.820 ns** |  **1,801.155 ns** | **2.0161** | **2.0161** | **2.0161** |  **524334 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 256   | Emoji  |  19,319.46 ns |    334.103 ns |    312.520 ns |      - |      - |      - |       1 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 256   | Emoji  | 364,984.13 ns | 11,226.612 ns | 10,501.380 ns | 1.6026 | 1.6026 | 1.6026 |  524336 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 256   | Emoji  | 206,028.08 ns |  2,231.015 ns |  1,977.736 ns |      - |      - |      - |      10 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 256   | Emoji  | 278,198.47 ns |  7,438.924 ns |  6,958.374 ns | 1.8382 | 1.8382 | 1.8382 |  524339 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 256   | Emoji  | 112,507.82 ns |    865.230 ns |    722.506 ns |      - |      - |      - |       5 B |
| **&#39;TextBuilder UTF-32 -&gt; ToText&#39;**             | **1024**     | **256**   | **Mixed**  | **344,400.49 ns** |  **7,025.240 ns** |  **6,571.414 ns** | **3.6765** | **3.6765** | **3.6765** | **1003136 B** |
| &#39;TextBuilder UTF-32 -&gt; ToOwnedText&#39;        | 1024     | 256   | Mixed  |  37,217.77 ns |    568.185 ns |    531.480 ns |      - |      - |      - |       2 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToText&#39;       | 1024     | 256   | Mixed  | 396,669.77 ns | 11,140.802 ns |  9,303.074 ns | 2.6042 | 2.6042 | 2.6042 | 1003134 B |
| &#39;TextBuilder UTF-8→UTF-32 -&gt; ToOwnedText&#39;  | 1024     | 256   | Mixed  | 149,614.88 ns |  3,143.150 ns |  2,786.321 ns |      - |      - |      - |       7 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToText&#39;      | 1024     | 256   | Mixed  | 590,927.97 ns |  5,645.998 ns |  4,408.024 ns | 3.9063 | 3.9063 | 3.9063 | 1003153 B |
| &#39;TextBuilder UTF-16→UTF-32 -&gt; ToOwnedText&#39; | 1024     | 256   | Mixed  | 247,640.88 ns |  2,835.548 ns |  2,513.639 ns |      - |      - |      - |      12 B |
