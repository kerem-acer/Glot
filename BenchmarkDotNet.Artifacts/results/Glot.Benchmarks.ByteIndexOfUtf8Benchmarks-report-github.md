```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N    | Locale | Mean       | Error       | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |----- |------- |-----------:|------------:|----------:|------:|--------:|----------:|------------:|
| **U8String.IndexOf**               | **256**  | **Ascii**  |   **2.165 ns** |   **0.4804 ns** | **0.0263 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 256  | Ascii  |   8.019 ns |   1.6766 ns | 0.0919 ns |  3.70 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 256  | Ascii  |  15.196 ns |   0.3474 ns | 0.0190 ns |  7.02 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 256  | Ascii  |  25.731 ns |  13.2152 ns | 0.7244 ns | 11.89 |    0.32 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 256  | Ascii  |   7.165 ns |   2.7562 ns | 0.1511 ns |  3.31 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 256  | Ascii  |  10.737 ns |   3.4591 ns | 0.1896 ns |  4.96 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 256  | Ascii  |  18.827 ns |   1.4298 ns | 0.0784 ns |  8.70 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 256  | Ascii  |  31.794 ns |   2.1552 ns | 0.1181 ns | 14.69 |    0.16 |         - |          NA |
|                                |      |        |            |             |           |       |         |           |             |
| **U8String.IndexOf**               | **256**  | **Mixed**  |   **2.148 ns** |   **0.5643 ns** | **0.0309 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 256  | Mixed  |   8.111 ns |   0.2702 ns | 0.0148 ns |  3.78 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 256  | Mixed  |  15.873 ns |   7.8471 ns | 0.4301 ns |  7.39 |    0.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 256  | Mixed  |  31.698 ns |   1.8683 ns | 0.1024 ns | 14.76 |    0.19 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 256  | Mixed  |   8.186 ns |   0.1098 ns | 0.0060 ns |  3.81 |    0.05 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 256  | Mixed  |  12.413 ns |   2.9955 ns | 0.1642 ns |  5.78 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 256  | Mixed  |  19.313 ns |   3.4323 ns | 0.1881 ns |  8.99 |    0.13 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 256  | Mixed  |  25.722 ns |   5.1674 ns | 0.2832 ns | 11.98 |    0.19 |         - |          NA |
|                                |      |        |            |             |           |       |         |           |             |
| **U8String.IndexOf**               | **4096** | **Ascii**  |   **2.227 ns** |   **0.6626 ns** | **0.0363 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096 | Ascii  |   8.246 ns |   2.4257 ns | 0.1330 ns |  3.70 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096 | Ascii  |  14.791 ns |   0.1065 ns | 0.0058 ns |  6.64 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096 | Ascii  |  26.374 ns |   5.3384 ns | 0.2926 ns | 11.84 |    0.20 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096 | Ascii  | 102.718 ns | 123.8562 ns | 6.7890 ns | 46.13 |    2.72 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096 | Ascii  | 107.697 ns |  27.7296 ns | 1.5200 ns | 48.37 |    0.90 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096 | Ascii  | 110.738 ns |  33.1293 ns | 1.8159 ns | 49.73 |    0.99 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096 | Ascii  | 122.512 ns |   6.5509 ns | 0.3591 ns | 55.02 |    0.78 |         - |          NA |
|                                |      |        |            |             |           |       |         |           |             |
| **U8String.IndexOf**               | **4096** | **Mixed**  |   **2.141 ns** |   **0.8160 ns** | **0.0447 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096 | Mixed  |   8.378 ns |   1.7208 ns | 0.0943 ns |  3.91 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096 | Mixed  |  15.668 ns |   2.6425 ns | 0.1448 ns |  7.32 |    0.14 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096 | Mixed  |  31.840 ns |   8.4916 ns | 0.4655 ns | 14.88 |    0.33 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096 | Mixed  | 123.833 ns |  24.6520 ns | 1.3513 ns | 57.86 |    1.18 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096 | Mixed  | 134.591 ns |  38.0913 ns | 2.0879 ns | 62.88 |    1.42 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096 | Mixed  | 136.316 ns |  14.1544 ns | 0.7758 ns | 63.69 |    1.19 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096 | Mixed  | 148.602 ns |  80.6916 ns | 4.4230 ns | 69.43 |    2.19 |         - |          NA |
