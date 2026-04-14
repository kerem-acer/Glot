```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  Categories=CompareTo  

```
| Method                     | N    | Locale | Mean          | Error         | StdDev        | Median        | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------------- |----- |------- |--------------:|--------------:|--------------:|--------------:|------:|--------:|----------:|------------:|
| **string.Compare**             | **256**  | **Ascii**  |     **11.772 ns** |      **2.752 ns** |     **0.1509 ns** |     **11.838 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.CompareTo same-enc&#39;  | 256  | Ascii  |     10.958 ns |      8.777 ns |     0.4811 ns |     10.982 ns |  0.93 |    0.04 |         - |          NA |
| &#39;Text.CompareTo cross-enc&#39; | 256  | Ascii  |    385.184 ns |     35.157 ns |     1.9271 ns |    385.366 ns | 32.72 |    0.39 |         - |          NA |
| U8String.CompareTo         | 256  | Ascii  |      8.855 ns |      3.819 ns |     0.2094 ns |      8.753 ns |  0.75 |    0.02 |         - |          NA |
|                            |      |        |               |               |               |               |       |         |           |             |
| **string.Compare**             | **256**  | **Mixed**  |     **11.848 ns** |      **3.329 ns** |     **0.1825 ns** |     **11.793 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.CompareTo same-enc&#39;  | 256  | Mixed  |     12.605 ns |      3.142 ns |     0.1722 ns |     12.510 ns |  1.06 |    0.02 |         - |          NA |
| &#39;Text.CompareTo cross-enc&#39; | 256  | Mixed  |    768.206 ns |     10.949 ns |     0.6002 ns |    768.150 ns | 64.85 |    0.86 |         - |          NA |
| U8String.CompareTo         | 256  | Mixed  |     11.480 ns |      1.744 ns |     0.0956 ns |     11.484 ns |  0.97 |    0.01 |         - |          NA |
|                            |      |        |               |               |               |               |       |         |           |             |
| **string.Compare**             | **4096** | **Ascii**  |    **184.095 ns** |     **43.767 ns** |     **2.3990 ns** |    **184.801 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.CompareTo same-enc&#39;  | 4096 | Ascii  |     86.921 ns |     28.202 ns |     1.5459 ns |     87.341 ns |  0.47 |    0.01 |         - |          NA |
| &#39;Text.CompareTo cross-enc&#39; | 4096 | Ascii  |  8,199.106 ns | 61,803.433 ns | 3,387.6547 ns |  6,392.459 ns | 44.54 |   15.95 |         - |          NA |
| U8String.CompareTo         | 4096 | Ascii  |     90.497 ns |     67.892 ns |     3.7214 ns |     92.125 ns |  0.49 |    0.02 |         - |          NA |
|                            |      |        |               |               |               |               |       |         |           |             |
| **string.Compare**             | **4096** | **Mixed**  |    **187.476 ns** |    **100.435 ns** |     **5.5052 ns** |    **186.442 ns** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Text.CompareTo same-enc&#39;  | 4096 | Mixed  |    108.385 ns |      7.752 ns |     0.4249 ns |    108.144 ns |  0.58 |    0.01 |         - |          NA |
| &#39;Text.CompareTo cross-enc&#39; | 4096 | Mixed  | 12,427.918 ns | 10,046.325 ns |   550.6730 ns | 12,212.995 ns | 66.33 |    3.05 |         - |          NA |
| U8String.CompareTo         | 4096 | Mixed  |    106.557 ns |     13.897 ns |     0.7618 ns |    106.351 ns |  0.57 |    0.01 |         - |          NA |
