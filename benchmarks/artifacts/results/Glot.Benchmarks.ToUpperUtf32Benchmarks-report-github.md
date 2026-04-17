```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean         | Error         | StdDev      | Gen0    | Gen1    | Gen2    | Allocated |
|------------------------------------- |------ |------- |-------------:|--------------:|------------:|--------:|--------:|--------:|----------:|
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Ascii**  |     **405.5 ns** |      **71.75 ns** |     **3.93 ns** |  **0.0334** |       **-** |       **-** |     **280 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Ascii  |     407.8 ns |      68.70 ns |     3.77 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Cjk**    |     **658.0 ns** |       **8.49 ns** |     **0.47 ns** |       **-** |       **-** |       **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Cjk    |     672.3 ns |      38.71 ns |     2.12 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Mixed**  |     **450.9 ns** |     **113.64 ns** |     **6.23 ns** |  **0.0324** |       **-** |       **-** |     **272 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Mixed  |     436.4 ns |      12.80 ns |     0.70 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Ascii**  |  **26,803.7 ns** |   **5,438.46 ns** |   **298.10 ns** |  **1.9531** |  **0.0916** |       **-** |   **16408 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Ascii  |  26,915.5 ns |   2,574.80 ns |   141.13 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Cjk**    |  **42,064.6 ns** |   **3,236.41 ns** |   **177.40 ns** |       **-** |       **-** |       **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Cjk    |  42,159.9 ns |   1,556.21 ns |    85.30 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Mixed**  |  **27,647.3 ns** |   **6,191.74 ns** |   **339.39 ns** |  **1.8616** |  **0.0916** |       **-** |   **15696 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Mixed  |  26,967.0 ns |   1,595.46 ns |    87.45 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Ascii**  | **440,205.1 ns** | **144,320.12 ns** | **7,910.67 ns** | **76.6602** | **76.6602** | **76.6602** |  **262218 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Ascii  | 436,085.1 ns |  17,138.36 ns |   939.41 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Cjk**    | **690,924.5 ns** | **171,607.46 ns** | **9,406.38 ns** |       **-** |       **-** |       **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Cjk    | 692,594.5 ns | 134,538.71 ns | 7,374.52 ns |       - |       - |       - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Mixed**  | **420,375.5 ns** |  **30,012.51 ns** | **1,645.09 ns** | **71.2891** | **71.2891** | **71.2891** |  **250814 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Mixed  | 418,853.4 ns |  15,956.74 ns |   874.64 ns |       - |       - |       - |         - |
