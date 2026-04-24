```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                    | N     | Locale | Mean          | Error         | StdDev        | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |--------------:|--------------:|--------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **3.325 ns** |     **0.0574 ns** |     **0.0536 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      2.425 ns |     0.0274 ns |     0.0256 ns |  0.73 |    0.01 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      1.845 ns |     0.0149 ns |     0.0132 ns |  0.56 |    0.01 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **3.310 ns** |     **0.0390 ns** |     **0.0365 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      4.455 ns |     0.0628 ns |     0.0588 ns |  1.35 |    0.02 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      1.870 ns |     0.0187 ns |     0.0175 ns |  0.57 |    0.01 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Emoji**  |      **3.355 ns** |     **0.0315 ns** |     **0.0294 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Emoji  |      3.581 ns |     0.0436 ns |     0.0408 ns |  1.07 |    0.01 |         - |          NA |
| Text.GetHashCode          | 8     | Emoji  |      1.849 ns |     0.0215 ns |     0.0191 ns |  0.55 |    0.01 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **3.333 ns** |     **0.0478 ns** |     **0.0448 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      2.441 ns |     0.0339 ns |     0.0317 ns |  0.73 |    0.01 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      1.865 ns |     0.0247 ns |     0.0231 ns |  0.56 |    0.01 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **152.830 ns** |     **3.4142 ns** |     **3.1937 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     22.230 ns |     0.2711 ns |     0.2536 ns |  0.15 |    0.00 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     21.411 ns |     0.2649 ns |     0.2478 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **151.770 ns** |     **4.2698 ns** |     **3.9940 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     64.360 ns |     0.8400 ns |     0.7446 ns |  0.42 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     21.362 ns |     0.3177 ns |     0.2972 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Emoji**  |    **152.432 ns** |     **4.1395 ns** |     **3.8721 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Emoji  |     43.075 ns |     0.5719 ns |     0.5349 ns |  0.28 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Emoji  |     21.379 ns |     0.2122 ns |     0.1985 ns |  0.14 |    0.00 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **149.854 ns** |     **4.1237 ns** |     **3.8573 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     32.632 ns |     0.4246 ns |     0.3972 ns |  0.22 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     23.167 ns |     0.2244 ns |     0.2099 ns |  0.15 |    0.00 |         - |          NA |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **40,819.688 ns** | **1,093.4726 ns** | **1,022.8350 ns** |  **1.00** |    **0.03** |       **2 B** |        **1.00** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,408.873 ns |    60.2743 ns |    56.3806 ns |  0.13 |    0.00 |         - |        0.00 |
| Text.GetHashCode          | 65536 | Ascii  |  3,787.682 ns |    53.8472 ns |    50.3687 ns |  0.09 |    0.00 |         - |        0.00 |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **39,594.573 ns** |   **588.8400 ns** |   **491.7081 ns** |  **1.00** |    **0.02** |       **2 B** |        **1.00** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 16,300.255 ns |   211.2310 ns |   197.5857 ns |  0.41 |    0.01 |       1 B |        0.50 |
| Text.GetHashCode          | 65536 | Cjk    |  3,776.184 ns |    33.6238 ns |    29.8066 ns |  0.10 |    0.00 |         - |        0.00 |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Emoji**  | **40,234.472 ns** |   **644.1053 ns** |   **570.9826 ns** |  **1.00** |    **0.02** |       **2 B** |        **1.00** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Emoji  | 10,937.246 ns |   158.2769 ns |   148.0523 ns |  0.27 |    0.01 |       1 B |        0.50 |
| Text.GetHashCode          | 65536 | Emoji  |  3,777.792 ns |    40.1232 ns |    37.5312 ns |  0.09 |    0.00 |         - |        0.00 |
|                           |       |        |               |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **39,767.688 ns** |   **425.5899 ns** |   **355.3868 ns** |  **1.00** |    **0.01** |       **2 B** |        **1.00** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  7,065.380 ns |    85.1475 ns |    79.6471 ns |  0.18 |    0.00 |         - |        0.00 |
| Text.GetHashCode          | 65536 | Mixed  |  3,782.118 ns |    35.3810 ns |    33.0954 ns |  0.10 |    0.00 |         - |        0.00 |
