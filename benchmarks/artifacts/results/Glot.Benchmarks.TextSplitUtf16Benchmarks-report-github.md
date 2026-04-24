```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                       | Categories     | N    | Locale | Mean      | Error      | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------------- |--------------- |----- |------- |----------:|-----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **string.EnumerateRunes**        | **EnumerateRunes** | **4096** | **Ascii**  |  **1.728 μs** |  **1.3650 μs** | **0.0748 μs** |  **1.00** |    **0.05** |      **-** |      **-** |         **-** |          **NA** |
| &#39;Text.EnumerateRunes UTF-16&#39; | EnumerateRunes | 4096 | Ascii  | 15.298 μs |  6.7037 μs | 0.3675 μs |  8.86 |    0.38 |      - |      - |         - |          NA |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **string.EnumerateRunes**        | **EnumerateRunes** | **4096** | **Cjk**    |  **1.764 μs** |  **1.5852 μs** | **0.0869 μs** |  **1.00** |    **0.06** |      **-** |      **-** |         **-** |          **NA** |
| &#39;Text.EnumerateRunes UTF-16&#39; | EnumerateRunes | 4096 | Cjk    | 15.604 μs |  1.0041 μs | 0.0550 μs |  8.86 |    0.38 |      - |      - |         - |          NA |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **string.EnumerateRunes**        | **EnumerateRunes** | **4096** | **Emoji**  |  **5.680 μs** |  **0.9232 μs** | **0.0506 μs** |  **1.00** |    **0.01** |      **-** |      **-** |         **-** |          **NA** |
| &#39;Text.EnumerateRunes UTF-16&#39; | EnumerateRunes | 4096 | Emoji  |  2.164 μs |  0.3810 μs | 0.0209 μs |  0.38 |    0.00 |      - |      - |         - |          NA |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **string.EnumerateRunes**        | **EnumerateRunes** | **4096** | **Mixed**  |  **6.446 μs** |  **0.7527 μs** | **0.0413 μs** |  **1.00** |    **0.01** |      **-** |      **-** |         **-** |          **NA** |
| &#39;Text.EnumerateRunes UTF-16&#39; | EnumerateRunes | 4096 | Mixed  |  2.393 μs |  1.8129 μs | 0.0994 μs |  0.37 |    0.01 |      - |      - |         - |          NA |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**         | **Split**          | **4096** | **Ascii**  |  **4.467 μs** |  **0.3236 μs** | **0.0177 μs** |  **1.00** |    **0.00** | **2.6169** | **0.1907** |   **21896 B** |        **1.00** |
| &#39;Text.Split UTF-16 count&#39;    | Split          | 4096 | Ascii  |  2.445 μs |  1.7476 μs | 0.0958 μs |  0.55 |    0.02 |      - |      - |         - |        0.00 |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**         | **Split**          | **4096** | **Cjk**    | **10.487 μs** |  **2.8046 μs** | **0.1537 μs** |  **1.00** |    **0.02** | **4.8981** | **0.6104** |   **40992 B** |        **1.00** |
| &#39;Text.Split UTF-16 count&#39;    | Split          | 4096 | Cjk    |  4.393 μs |  7.5818 μs | 0.4156 μs |  0.42 |    0.03 |      - |      - |         - |        0.00 |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**         | **Split**          | **4096** | **Emoji**  |  **7.782 μs** | **27.2506 μs** | **1.4937 μs** |  **1.02** |    **0.23** | **3.2654** | **0.2899** |   **27344 B** |        **1.00** |
| &#39;Text.Split UTF-16 count&#39;    | Split          | 4096 | Emoji  |  4.596 μs |  6.0121 μs | 0.3295 μs |  0.60 |    0.10 |      - |      - |         - |        0.00 |
|                              |                |      |        |           |            |           |       |         |        |        |           |             |
| **&#39;string.Split count&#39;**         | **Split**          | **4096** | **Mixed**  |  **4.783 μs** |  **3.9686 μs** | **0.2175 μs** |  **1.00** |    **0.06** | **2.6169** | **0.1907** |   **21896 B** |        **1.00** |
| &#39;Text.Split UTF-16 count&#39;    | Split          | 4096 | Mixed  |  2.726 μs |  2.8125 μs | 0.1542 μs |  0.57 |    0.04 |      - |      - |         - |        0.00 |
