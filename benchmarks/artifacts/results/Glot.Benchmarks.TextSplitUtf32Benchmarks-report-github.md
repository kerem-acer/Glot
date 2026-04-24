```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                       | Categories     | N     | Locale | Mean          | Error        | StdDev       | Allocated |
|----------------------------- |--------------- |------ |------- |--------------:|-------------:|-------------:|----------:|
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Ascii**  |     **132.65 ns** |     **1.349 ns** |     **1.262 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Cjk**    |     **133.68 ns** |     **2.375 ns** |     **2.222 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Emoji**  |      **88.55 ns** |     **0.683 ns** |     **0.533 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Mixed**  |     **118.38 ns** |     **1.229 ns** |     **1.150 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Ascii**  |   **8,687.07 ns** |    **66.091 ns** |    **61.822 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Cjk**    |   **8,658.77 ns** |    **70.620 ns** |    **66.058 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Emoji**  |   **5,761.70 ns** |    **42.137 ns** |    **39.415 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Mixed**  |   **7,706.72 ns** |    **44.491 ns** |    **37.152 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Ascii**  | **139,063.67 ns** |   **593.559 ns** |   **463.412 ns** |       **7 B** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Cjk**    | **139,624.52 ns** |   **753.918 ns** |   **705.215 ns** |       **7 B** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Emoji**  |  **93,006.41 ns** |   **471.219 ns** |   **417.723 ns** |       **5 B** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Mixed**  | **124,319.59 ns** |   **577.034 ns** |   **511.526 ns** |       **7 B** |
|                              |                |       |        |               |              |              |           |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Ascii**  |     **467.95 ns** |     **9.668 ns** |     **9.044 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Cjk**    |     **443.97 ns** |     **3.158 ns** |     **2.799 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Emoji**  |     **294.28 ns** |     **1.738 ns** |     **1.452 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Mixed**  |     **407.47 ns** |     **1.952 ns** |     **1.524 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Ascii**  |  **29,263.90 ns** |   **627.650 ns** |   **556.395 ns** |       **2 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Cjk**    |  **28,235.36 ns** |   **350.812 ns** |   **328.150 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Emoji**  |  **18,778.82 ns** |   **163.181 ns** |   **144.655 ns** |       **1 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Mixed**  |  **25,523.02 ns** |   **282.133 ns** |   **263.907 ns** |       **1 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Ascii**  | **488,568.45 ns** | **9,839.343 ns** | **9,203.728 ns** |      **18 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Cjk**    | **461,089.51 ns** | **3,169.995 ns** | **2,810.118 ns** |      **24 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Emoji**  | **308,980.31 ns** | **2,977.011 ns** | **2,639.042 ns** |       **3 B** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Mixed**  | **423,161.68 ns** | **1,956.703 ns** | **1,734.566 ns** |      **21 B** |
