```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                       | Categories     | N     | Locale | Mean         | Error         | StdDev      | Allocated |
|----------------------------- |--------------- |------ |------- |-------------:|--------------:|------------:|----------:|
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Ascii**  |     **128.6 ns** |      **24.92 ns** |     **1.37 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Cjk**    |     **129.5 ns** |      **14.01 ns** |     **0.77 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **64**    | **Mixed**  |     **114.2 ns** |       **1.74 ns** |     **0.10 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Ascii**  |   **8,397.8 ns** |     **233.57 ns** |    **12.80 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Cjk**    |   **8,421.0 ns** |     **211.50 ns** |    **11.59 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **4096**  | **Mixed**  |   **7,492.4 ns** |     **432.84 ns** |    **23.73 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Ascii**  | **136,426.2 ns** |  **23,540.79 ns** | **1,290.35 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Cjk**    | **135,004.7 ns** |   **2,118.27 ns** |   **116.11 ns** |         **-** |
| **&#39;Text.EnumerateRunes UTF-32&#39;** | **EnumerateRunes** | **65536** | **Mixed**  | **120,760.2 ns** |  **24,215.57 ns** | **1,327.34 ns** |         **-** |
|                              |                |       |        |              |               |             |           |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Ascii**  |     **450.9 ns** |     **260.12 ns** |    **14.26 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Cjk**    |     **434.7 ns** |      **82.60 ns** |     **4.53 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **64**    | **Mixed**  |     **401.3 ns** |      **69.61 ns** |     **3.82 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Ascii**  |  **28,798.6 ns** |  **14,568.78 ns** |   **798.56 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Cjk**    |  **27,208.5 ns** |   **1,522.37 ns** |    **83.45 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **4096**  | **Mixed**  |  **24,932.2 ns** |   **1,121.57 ns** |    **61.48 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Ascii**  | **485,192.7 ns** | **139,709.76 ns** | **7,657.96 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Cjk**    | **449,406.9 ns** |  **29,270.65 ns** | **1,604.42 ns** |         **-** |
| **&#39;Text.Split UTF-32 count&#39;**    | **Split**          | **65536** | **Mixed**  | **414,404.1 ns** |  **18,652.40 ns** | **1,022.40 ns** |         **-** |
