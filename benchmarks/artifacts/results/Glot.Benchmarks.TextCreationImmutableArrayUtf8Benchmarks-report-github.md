```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                                   | N     | Locale | Mean           | Error         | StdDev        | Ratio | RatioSD | Gen0   | Gen1   | Gen2   | Allocated | Alloc Ratio |
|----------------------------------------- |------ |------- |---------------:|--------------:|--------------:|------:|--------:|-------:|-------:|-------:|----------:|------------:|
| **Encoding.GetString**                       | **8**     | **Ascii**  |       **8.664 ns** |     **0.1081 ns** |     **0.0958 ns** |  **1.00** |    **0.02** | **0.0047** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 8     | Ascii  |       3.988 ns |     0.0794 ns |     0.0704 ns |  0.46 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 8     | Ascii  |       1.514 ns |     0.0186 ns |     0.0174 ns |  0.17 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 8     | Ascii  |       3.524 ns |     0.0633 ns |     0.0592 ns |  0.41 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **8**     | **Cjk**    |      **17.991 ns** |     **0.3402 ns** |     **0.3182 ns** |  **1.00** |    **0.02** | **0.0047** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 8     | Cjk    |       7.222 ns |     0.1673 ns |     0.1565 ns |  0.40 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 8     | Cjk    |       1.516 ns |     0.0220 ns |     0.0205 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 8     | Cjk    |       8.796 ns |     0.1374 ns |     0.1147 ns |  0.49 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **8**     | **Emoji**  |      **17.256 ns** |     **0.4323 ns** |     **0.3832 ns** |  **1.00** |    **0.03** | **0.0047** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 8     | Emoji  |       4.007 ns |     0.0356 ns |     0.0333 ns |  0.23 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 8     | Emoji  |       1.512 ns |     0.0236 ns |     0.0209 ns |  0.09 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 8     | Emoji  |      10.052 ns |     0.1341 ns |     0.1255 ns |  0.58 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **8**     | **Mixed**  |       **8.616 ns** |     **0.1303 ns** |     **0.1218 ns** |  **1.00** |    **0.02** | **0.0047** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 8     | Mixed  |       4.030 ns |     0.0991 ns |     0.0927 ns |  0.47 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 8     | Mixed  |       1.519 ns |     0.0142 ns |     0.0133 ns |  0.18 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 8     | Mixed  |       3.540 ns |     0.0712 ns |     0.0666 ns |  0.41 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **256**   | **Ascii**  |      **32.459 ns** |     **0.5815 ns** |     **0.5439 ns** |  **1.00** |    **0.02** | **0.0639** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 256   | Ascii  |      12.289 ns |     0.0693 ns |     0.0578 ns |  0.38 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 256   | Ascii  |       1.509 ns |     0.0129 ns |     0.0107 ns |  0.05 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 256   | Ascii  |       7.512 ns |     0.0876 ns |     0.0820 ns |  0.23 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **256**   | **Cjk**    |     **350.504 ns** |     **4.5202 ns** |     **3.7745 ns** | **1.000** |    **0.01** | **0.0630** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 256   | Cjk    |      30.579 ns |     0.6388 ns |     0.5975 ns | 0.087 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 256   | Cjk    |       1.503 ns |     0.0120 ns |     0.0106 ns | 0.004 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 256   | Cjk    |     123.419 ns |     1.6026 ns |     1.4991 ns | 0.352 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **256**   | **Emoji**  |     **465.514 ns** |    **10.4458 ns** |     **9.7711 ns** | **1.000** |    **0.03** | **0.0620** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 256   | Emoji  |      20.290 ns |     0.0983 ns |     0.0821 ns | 0.044 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 256   | Emoji  |       1.500 ns |     0.0117 ns |     0.0098 ns | 0.003 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 256   | Emoji  |     196.652 ns |     1.8456 ns |     1.4410 ns | 0.423 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **256**   | **Mixed**  |     **317.124 ns** |     **5.6722 ns** |     **5.0283 ns** | **1.000** |    **0.02** | **0.0622** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 256   | Mixed  |      18.133 ns |     0.1578 ns |     0.1476 ns | 0.057 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 256   | Mixed  |       1.546 ns |     0.0274 ns |     0.0243 ns | 0.005 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 256   | Mixed  |     180.443 ns |     1.8095 ns |     1.5110 ns | 0.569 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **65536** | **Ascii**  |  **44,638.996 ns** |   **906.0264 ns** |   **803.1688 ns** | **1.000** |    **0.02** | **0.4562** | **0.4562** | **0.4562** |  **131101 B** |        **1.00** |
| Text.FromUtf8(ImmutableArray)            | 65536 | Ascii  |   2,550.756 ns |    14.9870 ns |    12.5148 ns | 0.057 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 65536 | Ascii  |       1.516 ns |     0.0272 ns |     0.0254 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(ImmutableArray)&#39;           | 65536 | Ascii  |   1,246.923 ns |    10.3049 ns |     9.1350 ns | 0.028 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **65536** | **Cjk**    |  **90,103.005 ns** | **1,687.8399 ns** | **1,578.8064 ns** | **1.000** |    **0.02** |      **-** |      **-** |      **-** |  **131101 B** |       **1.000** |
| Text.FromUtf8(ImmutableArray)            | 65536 | Cjk    |   7,650.582 ns |    52.6176 ns |    49.2186 ns | 0.085 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 65536 | Cjk    |       1.503 ns |     0.0177 ns |     0.0166 ns | 0.000 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;new U8String(ImmutableArray)&#39;           | 65536 | Cjk    |  27,515.713 ns |   127.9830 ns |    99.9207 ns | 0.305 |    0.01 |      - |      - |      - |       1 B |       0.000 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **65536** | **Emoji**  | **118,961.461 ns** | **2,115.3191 ns** | **1,978.6708 ns** | **1.000** |    **0.02** |      **-** |      **-** |      **-** |  **131102 B** |       **1.000** |
| Text.FromUtf8(ImmutableArray)            | 65536 | Emoji  |   5,095.207 ns |    42.6040 ns |    39.8518 ns | 0.043 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 65536 | Emoji  |       1.510 ns |     0.0163 ns |     0.0152 ns | 0.000 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;new U8String(ImmutableArray)&#39;           | 65536 | Emoji  |  47,901.773 ns |   558.5811 ns |   466.4405 ns | 0.403 |    0.01 |      - |      - |      - |       3 B |       0.000 |
|                                          |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.GetString**                       | **65536** | **Mixed**  | **128,350.193 ns** | **1,889.2589 ns** | **1,767.2139 ns** | **1.000** |    **0.02** | **0.5734** | **0.5734** | **0.5734** |  **131105 B** |       **1.000** |
| Text.FromUtf8(ImmutableArray)            | 65536 | Mixed  |   3,327.575 ns |    30.9951 ns |    27.4764 ns | 0.026 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;Text.FromUtf8(ImmutableArray) no-count&#39; | 65536 | Mixed  |       1.549 ns |     0.0316 ns |     0.0280 ns | 0.000 |    0.00 |      - |      - |      - |         - |       0.000 |
| &#39;new U8String(ImmutableArray)&#39;           | 65536 | Mixed  |  44,726.308 ns | 1,243.3993 ns | 1,102.2411 ns | 0.349 |    0.01 |      - |      - |      - |       2 B |       0.000 |
