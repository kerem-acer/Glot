```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                               | N     | Locale | Mean          | Error         | StdDev        | Gen0   | Gen1   | Gen2   | Allocated |
|------------------------------------- |------ |------- |--------------:|--------------:|--------------:|-------:|-------:|-------:|----------:|
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Ascii**  |      **52.30 ns** |      **1.195 ns** |      **1.118 ns** | **0.0332** |      **-** |      **-** |     **280 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Ascii  |      52.48 ns |      1.100 ns |      1.029 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Cjk**    |     **349.05 ns** |      **4.255 ns** |      **3.772 ns** |      **-** |      **-** |      **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Cjk    |     361.98 ns |      3.592 ns |      3.360 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Emoji**  |      **69.75 ns** |      **1.641 ns** |      **1.535 ns** |      **-** |      **-** |      **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Emoji  |      83.41 ns |      1.854 ns |      1.734 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **64**    | **Mixed**  |     **387.40 ns** |      **4.380 ns** |      **4.097 ns** | **0.0311** |      **-** |      **-** |     **272 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 64    | Mixed  |     393.36 ns |      3.749 ns |      3.323 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Ascii**  |   **2,618.48 ns** |     **63.160 ns** |     **59.080 ns** | **1.9437** | **0.1060** |      **-** |   **16408 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Ascii  |   2,210.15 ns |     54.420 ns |     50.904 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Cjk**    |  **21,443.84 ns** |    **190.213 ns** |    **158.836 ns** |      **-** |      **-** |      **-** |       **1 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Cjk    |  21,763.31 ns |    243.400 ns |    227.676 ns |      - |      - |      - |       1 B |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Emoji**  |   **3,802.16 ns** |    **143.265 ns** |    **134.010 ns** |      **-** |      **-** |      **-** |         **-** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Emoji  |   3,888.66 ns |    123.570 ns |    115.588 ns |      - |      - |      - |         - |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **4096**  | **Mixed**  |  **22,610.88 ns** |    **317.290 ns** |    **296.793 ns** | **1.8029** |      **-** |      **-** |   **15697 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 4096  | Mixed  |  21,381.84 ns |    302.404 ns |    252.521 ns |      - |      - |      - |       1 B |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Ascii**  | **103,685.84 ns** |  **5,193.579 ns** |  **4,054.805 ns** | **0.6944** | **0.6944** | **0.6944** |  **262179 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Ascii  |  34,620.48 ns |    873.755 ns |    774.561 ns |      - |      - |      - |       2 B |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Cjk**    | **344,389.95 ns** |  **2,007.228 ns** |  **1,676.126 ns** |      **-** |      **-** |      **-** |      **13 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Cjk    | 351,846.05 ns |  6,475.277 ns |  6,056.978 ns |      - |      - |      - |      19 B |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Emoji**  |  **59,428.42 ns** |  **1,952.730 ns** |  **1,826.585 ns** |      **-** |      **-** |      **-** |       **2 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Emoji  |  64,382.88 ns |  2,874.676 ns |  2,688.974 ns |      - |      - |      - |       1 B |
| **&#39;Text.ToUpperInvariant UTF-32&#39;**       | **65536** | **Mixed**  | **387,365.57 ns** | **11,449.579 ns** | **10,709.944 ns** |      **-** |      **-** |      **-** |  **250788 B** |
| &#39;Text.ToUpperInvariantPooled UTF-32&#39; | 65536 | Mixed  | 348,683.68 ns |  8,122.997 ns |  7,598.256 ns |      - |      - |      - |      13 B |
