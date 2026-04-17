```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                           | Categories | PartSize | Locale | Mean         | Error        | StdDev     | Gen0   | Gen1   | Allocated |
|--------------------------------- |----------- |--------- |------- |-------------:|-------------:|-----------:|-------:|-------:|----------:|
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Ascii**  |     **20.09 ns** |     **1.599 ns** |   **0.088 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Ascii  |     23.90 ns |     2.056 ns |   0.113 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Cjk**    |     **20.84 ns** |     **0.634 ns** |   **0.035 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Cjk    |     24.47 ns |     2.875 ns |   0.158 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Mixed**  |     **20.00 ns** |     **0.188 ns** |   **0.010 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Mixed  |     23.66 ns |     1.036 ns |   0.057 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Ascii**  |    **528.51 ns** |   **101.181 ns** |   **5.546 ns** | **0.0181** |      **-** |     **152 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Ascii  |    510.75 ns |    44.402 ns |   2.434 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Cjk**    |    **731.84 ns** |   **395.617 ns** |  **21.685 ns** | **0.0486** |      **-** |     **408 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Cjk    |    622.31 ns |    37.564 ns |   2.059 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Mixed**  |    **640.84 ns** |    **10.965 ns** |   **0.601 ns** | **0.0229** |      **-** |     **192 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Mixed  |    597.56 ns |    25.992 ns |   1.425 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Ascii**  |  **8,050.83 ns** |   **800.279 ns** |  **43.866 ns** | **0.2441** |      **-** |    **2072 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Ascii  |  8,340.00 ns | 1,459.046 ns |  79.975 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Cjk**    |  **9,365.92 ns** |   **555.644 ns** |  **30.457 ns** | **0.7324** | **0.0153** |    **6168 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Cjk    |  8,951.64 ns | 1,857.559 ns | 101.819 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Mixed**  | **10,000.45 ns** |   **277.126 ns** |  **15.190 ns** | **0.3204** |      **-** |    **2696 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Mixed  |  8,569.37 ns | 3,283.752 ns | 179.994 ns |      - |      - |         - |
|                                  |            |          |        |              |              |            |        |        |           |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Ascii**  |     **37.46 ns** |     **5.625 ns** |   **0.308 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Ascii  |     45.30 ns |     5.866 ns |   0.322 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Cjk**    |     **38.36 ns** |     **0.831 ns** |   **0.046 ns** | **0.0048** |      **-** |      **40 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Cjk    |     47.30 ns |     2.047 ns |   0.112 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Mixed**  |     **38.44 ns** |     **4.178 ns** |   **0.229 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Mixed  |     43.80 ns |     4.297 ns |   0.236 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Ascii**  |  **1,051.01 ns** |   **201.181 ns** |  **11.027 ns** | **0.0324** |      **-** |     **280 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Ascii  |  1,041.89 ns |    30.128 ns |   1.651 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Cjk**    |  **1,193.14 ns** |    **50.523 ns** |   **2.769 ns** | **0.0935** |      **-** |     **792 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Cjk    |  1,256.98 ns |   178.723 ns |   9.796 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Mixed**  |  **1,046.49 ns** |    **69.540 ns** |   **3.812 ns** | **0.0420** |      **-** |     **360 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Mixed  |  1,201.40 ns |    99.268 ns |   5.441 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Ascii**  | **15,440.94 ns** |    **16.602 ns** |   **0.910 ns** | **0.4883** |      **-** |    **4120 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Ascii  | 16,249.00 ns | 1,952.585 ns | 107.028 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Cjk**    | **17,769.77 ns** |   **887.592 ns** |  **48.652 ns** | **1.4648** | **0.0610** |   **12312 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Cjk    | 17,999.38 ns | 4,472.903 ns | 245.175 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Mixed**  | **16,158.95 ns** | **2,637.527 ns** | **144.572 ns** | **0.6409** |      **-** |    **5368 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Mixed  | 17,774.96 ns | 1,458.726 ns |  79.958 ns |      - |      - |         - |
|                                  |            |          |        |              |              |            |        |        |           |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Ascii**  |     **68.22 ns** |     **8.983 ns** |   **0.492 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Ascii  |     94.34 ns |     2.576 ns |   0.141 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Cjk**    |     **70.17 ns** |     **1.585 ns** |   **0.087 ns** | **0.0057** |      **-** |      **48 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Cjk    |     92.42 ns |    26.966 ns |   1.478 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Mixed**  |     **68.43 ns** |     **2.482 ns** |   **0.136 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Mixed  |     97.85 ns |     1.651 ns |   0.090 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Ascii**  |  **2,125.17 ns** |   **373.723 ns** |  **20.485 ns** | **0.0610** |      **-** |     **536 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Ascii  |  2,096.67 ns |   283.769 ns |  15.554 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Cjk**    |  **2,365.40 ns** |   **560.546 ns** |  **30.725 ns** | **0.1831** |      **-** |    **1560 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Cjk    |  2,347.29 ns |    63.068 ns |   3.457 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Mixed**  |  **2,169.79 ns** |   **332.487 ns** |  **18.225 ns** | **0.0801** |      **-** |     **696 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Mixed  |  2,114.66 ns |   215.028 ns |  11.786 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Ascii**  | **32,516.46 ns** | **3,550.134 ns** | **194.595 ns** | **0.9766** |      **-** |    **8216 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Ascii  | 32,316.30 ns | 3,315.388 ns | 181.728 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Cjk**    | **35,824.95 ns** | **7,752.661 ns** | **424.949 ns** | **2.9297** | **0.2441** |   **24600 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Cjk    | 35,568.71 ns | 4,884.634 ns | 267.743 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Mixed**  | **32,475.00 ns** | **8,927.740 ns** | **489.360 ns** | **1.2207** |      **-** |   **10712 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Mixed  | 34,818.14 ns | 3,320.605 ns | 182.014 ns |      - |      - |         - |
