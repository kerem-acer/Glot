```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | N     | Locale | Mean          | Error          | StdDev        | Median        | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------- |------ |------- |--------------:|---------------:|--------------:|--------------:|------:|--------:|----------:|------------:|
| **string.GetHashCode**        | **8**     | **Ascii**  |      **2.113 ns** |      **0.8757 ns** |     **0.0480 ns** |      **2.123 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Ascii  |      1.306 ns |      1.1244 ns |     0.0616 ns |      1.297 ns |  0.62 |    0.03 |         - |          NA |
| Text.GetHashCode          | 8     | Ascii  |      1.483 ns |      0.2084 ns |     0.0114 ns |      1.481 ns |  0.70 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Cjk**    |      **2.250 ns** |      **7.3993 ns** |     **0.4056 ns** |      **2.056 ns** |  **1.02** |    **0.22** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Cjk    |      3.302 ns |      4.2185 ns |     0.2312 ns |      3.399 ns |  1.50 |    0.23 |         - |          NA |
| Text.GetHashCode          | 8     | Cjk    |      1.803 ns |      4.6455 ns |     0.2546 ns |      1.877 ns |  0.82 |    0.15 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **8**     | **Mixed**  |      **2.076 ns** |      **1.9415 ns** |     **0.1064 ns** |      **2.023 ns** |  **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 8     | Mixed  |      1.306 ns |      2.5752 ns |     0.1412 ns |      1.237 ns |  0.63 |    0.06 |         - |          NA |
| Text.GetHashCode          | 8     | Mixed  |      1.725 ns |      3.3277 ns |     0.1824 ns |      1.760 ns |  0.83 |    0.08 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Ascii**  |    **185.678 ns** |  **1,245.8717 ns** |    **68.2904 ns** |    **152.955 ns** |  **1.08** |    **0.46** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Ascii  |     20.886 ns |     14.4701 ns |     0.7932 ns |     20.525 ns |  0.12 |    0.03 |         - |          NA |
| Text.GetHashCode          | 256   | Ascii  |     42.041 ns |    273.8473 ns |    15.0105 ns |     33.957 ns |  0.24 |    0.10 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Cjk**    |    **139.241 ns** |      **9.6418 ns** |     **0.5285 ns** |    **139.337 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Cjk    |     65.515 ns |     17.3539 ns |     0.9512 ns |     65.753 ns |  0.47 |    0.01 |         - |          NA |
| Text.GetHashCode          | 256   | Cjk    |     32.578 ns |      1.5276 ns |     0.0837 ns |     32.623 ns |  0.23 |    0.00 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **256**   | **Mixed**  |    **157.727 ns** |    **215.5458 ns** |    **11.8148 ns** |    **164.504 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 256   | Mixed  |     37.521 ns |    252.7679 ns |    13.8551 ns |     29.590 ns |  0.24 |    0.08 |         - |          NA |
| Text.GetHashCode          | 256   | Mixed  |     32.768 ns |      1.9130 ns |     0.1049 ns |     32.787 ns |  0.21 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Ascii**  | **38,739.993 ns** | **21,143.0328 ns** | **1,158.9210 ns** | **38,386.009 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Ascii  |  5,156.453 ns |    152.7080 ns |     8.3704 ns |  5,156.303 ns |  0.13 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Ascii  |  7,393.297 ns |  1,513.9989 ns |    82.9874 ns |  7,378.611 ns |  0.19 |    0.01 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Cjk**    | **37,843.246 ns** |    **683.5893 ns** |    **37.4698 ns** | **37,834.788 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Cjk    | 15,482.557 ns |  1,976.8652 ns |   108.3587 ns | 15,528.271 ns |  0.41 |    0.00 |         - |          NA |
| Text.GetHashCode          | 65536 | Cjk    |  8,439.529 ns | 32,435.5352 ns | 1,777.9011 ns |  7,451.856 ns |  0.22 |    0.04 |         - |          NA |
|                           |       |        |               |                |               |               |       |         |           |             |
| **string.GetHashCode**        | **65536** | **Mixed**  | **42,458.433 ns** | **60,932.0645 ns** | **3,339.8921 ns** | **41,035.484 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;HashCode.AddBytes UTF-8&#39; | 65536 | Mixed  |  8,016.675 ns | 36,080.6007 ns | 1,977.6995 ns |  6,932.371 ns |  0.19 |    0.04 |         - |          NA |
| Text.GetHashCode          | 65536 | Mixed  |  7,223.031 ns |  4,003.3400 ns |   219.4366 ns |  7,116.776 ns |  0.17 |    0.01 |         - |          NA |
