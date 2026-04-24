```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                           | Categories | PartSize | Locale | Mean         | Error      | StdDev     | Gen0   | Gen1   | Allocated |
|--------------------------------- |----------- |--------- |------- |-------------:|-----------:|-----------:|-------:|-------:|----------:|
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Ascii**  |     **19.04 ns** |   **0.129 ns** |   **0.114 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Ascii  |     19.50 ns |   0.148 ns |   0.132 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Cjk**    |     **19.19 ns** |   **0.123 ns** |   **0.109 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Cjk    |     20.54 ns |   0.135 ns |   0.120 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Emoji**  |     **18.97 ns** |   **0.174 ns** |   **0.154 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Emoji  |     20.20 ns |   0.185 ns |   0.173 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1**        | **Mixed**  |     **19.03 ns** |   **0.173 ns** |   **0.162 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1        | Mixed  |     19.38 ns |   0.139 ns |   0.130 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Ascii**  |     **81.09 ns** |   **0.377 ns** |   **0.315 ns** | **0.0177** |      **-** |     **152 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Ascii  |     77.83 ns |   0.851 ns |   0.711 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Cjk**    |    **286.01 ns** |   **3.065 ns** |   **2.559 ns** | **0.0479** |      **-** |     **408 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Cjk    |    265.66 ns |   2.361 ns |   2.093 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Emoji**  |    **191.38 ns** |   **2.360 ns** |   **2.092 ns** | **0.0328** |      **-** |     **280 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Emoji  |    189.21 ns |   1.415 ns |   1.182 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **64**       | **Mixed**  |    **110.73 ns** |   **1.330 ns** |   **1.244 ns** | **0.0226** |      **-** |     **192 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 64       | Mixed  |    108.74 ns |   0.907 ns |   0.804 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Ascii**  |  **1,120.61 ns** |  **25.999 ns** |  **24.320 ns** | **0.2422** |      **-** |    **2072 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Ascii  |  1,020.90 ns |   9.994 ns |   8.859 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Cjk**    |  **4,284.55 ns** | **125.996 ns** | **105.212 ns** | **0.7320** |      **-** |    **6168 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Cjk    |  3,951.36 ns |  35.016 ns |  31.041 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Emoji**  |  **2,678.76 ns** |  **50.261 ns** |  **44.555 ns** | **0.4923** |      **-** |    **4120 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Emoji  |  2,493.06 ns |  36.876 ns |  32.689 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **2 parts**    | **1024**     | **Mixed**  |  **1,608.09 ns** |  **25.724 ns** |  **24.062 ns** | **0.3186** |      **-** |    **2696 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 2 parts    | 1024     | Mixed  |  1,485.55 ns |  17.039 ns |  15.104 ns |      - |      - |         - |
|                                  |            |          |        |              |            |            |        |        |           |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Ascii**  |     **30.83 ns** |   **0.234 ns** |   **0.208 ns** | **0.0037** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Ascii  |     29.17 ns |   0.254 ns |   0.237 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Cjk**    |     **32.37 ns** |   **0.114 ns** |   **0.096 ns** | **0.0047** |      **-** |      **40 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Cjk    |     31.01 ns |   0.265 ns |   0.248 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Emoji**  |     **32.35 ns** |   **0.147 ns** |   **0.130 ns** | **0.0046** |      **-** |      **40 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Emoji  |     30.87 ns |   0.275 ns |   0.258 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1**        | **Mixed**  |     **30.74 ns** |   **0.261 ns** |   **0.231 ns** | **0.0037** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1        | Mixed  |     29.27 ns |   0.288 ns |   0.269 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Ascii**  |    **149.62 ns** |   **0.768 ns** |   **0.599 ns** | **0.0328** |      **-** |     **280 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Ascii  |    132.10 ns |   1.459 ns |   1.364 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Cjk**    |    **549.41 ns** |   **6.183 ns** |   **5.163 ns** | **0.0943** |      **-** |     **792 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Cjk    |    511.36 ns |   7.166 ns |   6.353 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Emoji**  |    **350.82 ns** |   **4.657 ns** |   **3.889 ns** | **0.0634** |      **-** |     **536 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Emoji  |    325.48 ns |   4.220 ns |   3.741 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **64**       | **Mixed**  |    **209.36 ns** |   **3.330 ns** |   **3.115 ns** | **0.0425** |      **-** |     **360 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 64       | Mixed  |    187.12 ns |   1.892 ns |   1.677 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Ascii**  |  **2,187.48 ns** |  **34.646 ns** |  **30.712 ns** | **0.4874** |      **-** |    **4120 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Ascii  |  2,036.78 ns |  34.123 ns |  31.919 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Cjk**    |  **8,572.15 ns** | **211.816 ns** | **187.770 ns** | **1.4509** | **0.0558** |   **12312 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Cjk    |  8,024.65 ns | 120.195 ns | 106.550 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Emoji**  |  **5,369.51 ns** |  **48.211 ns** |  **40.258 ns** | **0.9737** |      **-** |    **8216 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Emoji  |  4,987.78 ns |  52.952 ns |  46.940 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **4 parts**    | **1024**     | **Mixed**  |  **3,225.96 ns** |  **71.178 ns** |  **66.580 ns** | **0.6358** |      **-** |    **5368 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 4 parts    | 1024     | Mixed  |  2,945.88 ns |  29.362 ns |  24.518 ns |      - |      - |         - |
|                                  |            |          |        |              |            |            |        |        |           |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Ascii**  |     **48.41 ns** |   **0.259 ns** |   **0.229 ns** | **0.0035** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Ascii  |     49.02 ns |   0.352 ns |   0.329 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Cjk**    |     **52.05 ns** |   **0.269 ns** |   **0.224 ns** | **0.0056** |      **-** |      **48 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Cjk    |     52.15 ns |   0.237 ns |   0.198 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Emoji**  |     **52.14 ns** |   **0.356 ns** |   **0.316 ns** | **0.0056** |      **-** |      **48 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Emoji  |     52.41 ns |   0.404 ns |   0.358 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1**        | **Mixed**  |     **48.39 ns** |   **0.459 ns** |   **0.407 ns** | **0.0035** |      **-** |      **32 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1        | Mixed  |     48.95 ns |   0.273 ns |   0.228 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Ascii**  |    **285.18 ns** |   **6.387 ns** |   **5.974 ns** | **0.0639** |      **-** |     **536 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Ascii  |    255.73 ns |   1.833 ns |   1.530 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Cjk**    |  **1,079.61 ns** |  **20.208 ns** |  **18.903 ns** | **0.1855** |      **-** |    **1560 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Cjk    |  1,010.31 ns |  15.417 ns |  14.421 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Emoji**  |    **676.29 ns** |  **12.843 ns** |  **12.013 ns** | **0.1212** |      **-** |    **1048 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Emoji  |    621.93 ns |  10.157 ns |   9.004 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **64**       | **Mixed**  |    **396.07 ns** |   **2.545 ns** |   **1.987 ns** | **0.0820** |      **-** |     **696 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 64       | Mixed  |    360.97 ns |   2.575 ns |   2.150 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Ascii**  |  **4,496.95 ns** | **109.256 ns** | **102.198 ns** | **0.9729** | **0.0295** |    **8216 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Ascii  |  4,141.17 ns |  64.603 ns |  57.269 ns |      - |      - |         - |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Cjk**    | **17,279.19 ns** | **198.091 ns** | **165.415 ns** | **2.8409** | **0.2273** |   **24601 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Cjk    | 15,958.96 ns | 130.197 ns | 101.649 ns |      - |      - |       1 B |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Emoji**  | **10,848.90 ns** | **315.339 ns** | **279.540 ns** | **1.9025** | **0.0705** |   **16409 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Emoji  | 10,052.45 ns | 123.841 ns | 103.413 ns |      - |      - |       1 B |
| **&#39;Text.Create $&quot;...&quot; UTF-32&#39;**      | **8 parts**    | **1024**     | **Mixed**  |  **6,404.70 ns** |  **34.873 ns** |  **27.227 ns** | **1.2755** | **0.0425** |   **10712 B** |
| &#39;OwnedText.Create $&quot;...&quot; UTF-32&#39; | 8 parts    | 1024     | Mixed  |  6,055.49 ns | 128.267 ns | 119.981 ns |      - |      - |         - |
