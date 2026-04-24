```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                               | N     | Locale | Mean           | Error         | StdDev        | Ratio | RatioSD | Gen0   | Gen1   | Gen2   | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |---------------:|--------------:|--------------:|------:|--------:|-------:|-------:|-------:|----------:|------------:|
| **Encoding.UTF32.GetString(span)**       | **8**     | **Ascii**  |      **50.463 ns** |     **0.6649 ns** |     **0.6219 ns** |  **1.00** |    **0.02** | **0.0218** |      **-** |      **-** |     **184 B** |        **1.00** |
| Text.FromBytes(span)                 | 8     | Ascii  |       5.944 ns |     0.1054 ns |     0.0880 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| &#39;Text.FromBytes(span) no-count&#39;      | 8     | Ascii  |       5.874 ns |     0.0652 ns |     0.0578 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| OwnedText.FromBytes(span)            | 8     | Ascii  |      11.086 ns |     0.1759 ns |     0.1646 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 8     | Ascii  |      11.019 ns |     0.1058 ns |     0.0989 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **8**     | **Cjk**    |      **50.925 ns** |     **0.8596 ns** |     **0.6711 ns** |  **1.00** |    **0.02** | **0.0218** |      **-** |      **-** |     **184 B** |        **1.00** |
| Text.FromBytes(span)                 | 8     | Cjk    |       5.916 ns |     0.0835 ns |     0.0781 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| &#39;Text.FromBytes(span) no-count&#39;      | 8     | Cjk    |       5.885 ns |     0.0532 ns |     0.0472 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| OwnedText.FromBytes(span)            | 8     | Cjk    |      11.085 ns |     0.1483 ns |     0.1387 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 8     | Cjk    |      11.159 ns |     0.1257 ns |     0.1176 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **8**     | **Emoji**  |      **41.729 ns** |     **0.4631 ns** |     **0.3867 ns** |  **1.00** |    **0.01** | **0.0220** |      **-** |      **-** |     **184 B** |        **1.00** |
| Text.FromBytes(span)                 | 8     | Emoji  |       5.614 ns |     0.0721 ns |     0.0639 ns |  0.13 |    0.00 | 0.0048 |      - |      - |      40 B |        0.22 |
| &#39;Text.FromBytes(span) no-count&#39;      | 8     | Emoji  |       5.628 ns |     0.0888 ns |     0.0830 ns |  0.13 |    0.00 | 0.0048 |      - |      - |      40 B |        0.22 |
| OwnedText.FromBytes(span)            | 8     | Emoji  |      11.495 ns |     0.2021 ns |     0.1891 ns |  0.28 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 8     | Emoji  |      11.536 ns |     0.2050 ns |     0.1917 ns |  0.28 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **8**     | **Mixed**  |      **50.312 ns** |     **0.6049 ns** |     **0.5658 ns** |  **1.00** |    **0.02** | **0.0219** |      **-** |      **-** |     **184 B** |        **1.00** |
| Text.FromBytes(span)                 | 8     | Mixed  |       5.880 ns |     0.0787 ns |     0.0658 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| &#39;Text.FromBytes(span) no-count&#39;      | 8     | Mixed  |       5.911 ns |     0.0944 ns |     0.0883 ns |  0.12 |    0.00 | 0.0067 |      - |      - |      56 B |        0.30 |
| OwnedText.FromBytes(span)            | 8     | Mixed  |      11.016 ns |     0.1363 ns |     0.1275 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 8     | Mixed  |      11.051 ns |     0.1543 ns |     0.1443 ns |  0.22 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **256**   | **Ascii**  |   **1,219.424 ns** |    **16.1586 ns** |    **15.1147 ns** |  **1.00** |    **0.02** | **0.0809** |      **-** |      **-** |     **680 B** |        **1.00** |
| Text.FromBytes(span)                 | 256   | Ascii  |      43.367 ns |     0.1967 ns |     0.1642 ns |  0.04 |    0.00 | 0.1250 | 0.0003 |      - |    1048 B |        1.54 |
| &#39;Text.FromBytes(span) no-count&#39;      | 256   | Ascii  |      44.257 ns |     1.2993 ns |     1.0849 ns |  0.04 |    0.00 | 0.1252 | 0.0003 |      - |    1048 B |        1.54 |
| OwnedText.FromBytes(span)            | 256   | Ascii  |      19.692 ns |     0.2199 ns |     0.1950 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 256   | Ascii  |      19.691 ns |     0.1908 ns |     0.1785 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **256**   | **Cjk**    |   **1,214.075 ns** |    **10.7402 ns** |     **8.9686 ns** |  **1.00** |    **0.01** | **0.0736** |      **-** |      **-** |     **680 B** |        **1.00** |
| Text.FromBytes(span)                 | 256   | Cjk    |      43.990 ns |     0.6001 ns |     0.5614 ns |  0.04 |    0.00 | 0.1253 | 0.0003 |      - |    1048 B |        1.54 |
| &#39;Text.FromBytes(span) no-count&#39;      | 256   | Cjk    |      44.024 ns |     0.8368 ns |     0.7827 ns |  0.04 |    0.00 | 0.1250 | 0.0003 |      - |    1048 B |        1.54 |
| OwnedText.FromBytes(span)            | 256   | Cjk    |      25.553 ns |     0.1977 ns |     0.1651 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 256   | Cjk    |      19.732 ns |     0.1794 ns |     0.1678 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **256**   | **Emoji**  |     **806.687 ns** |    **10.7143 ns** |    **10.0221 ns** |  **1.00** |    **0.02** | **0.0801** |      **-** |      **-** |     **680 B** |        **1.00** |
| Text.FromBytes(span)                 | 256   | Emoji  |      25.505 ns |     0.2252 ns |     0.1996 ns |  0.03 |    0.00 | 0.0641 |      - |      - |     536 B |        0.79 |
| &#39;Text.FromBytes(span) no-count&#39;      | 256   | Emoji  |      25.634 ns |     0.4763 ns |     0.4222 ns |  0.03 |    0.00 | 0.0640 |      - |      - |     536 B |        0.79 |
| OwnedText.FromBytes(span)            | 256   | Emoji  |      15.931 ns |     0.1749 ns |     0.1636 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 256   | Emoji  |      15.828 ns |     0.1356 ns |     0.1132 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **256**   | **Mixed**  |   **1,221.370 ns** |    **13.4555 ns** |    **11.9279 ns** |  **1.00** |    **0.01** | **0.0808** |      **-** |      **-** |     **680 B** |        **1.00** |
| Text.FromBytes(span)                 | 256   | Mixed  |      41.558 ns |     0.6478 ns |     0.6060 ns |  0.03 |    0.00 | 0.1205 | 0.0003 |      - |    1008 B |        1.48 |
| &#39;Text.FromBytes(span) no-count&#39;      | 256   | Mixed  |      41.849 ns |     0.8117 ns |     0.7196 ns |  0.03 |    0.00 | 0.1204 | 0.0003 |      - |    1008 B |        1.48 |
| OwnedText.FromBytes(span)            | 256   | Mixed  |      19.017 ns |     0.1185 ns |     0.0989 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 256   | Mixed  |      19.432 ns |     0.1434 ns |     0.1341 ns |  0.02 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **65536** | **Ascii**  | **315,255.116 ns** | **3,889.2615 ns** | **3,638.0176 ns** |  **1.00** |    **0.02** |      **-** |      **-** |      **-** |  **131257 B** |        **1.00** |
| Text.FromBytes(span)                 | 65536 | Ascii  |  63,411.914 ns | 1,455.1850 ns | 1,361.1809 ns |  0.20 |    0.00 | 0.7396 | 0.7396 | 0.7396 |  262169 B |        2.00 |
| &#39;Text.FromBytes(span) no-count&#39;      | 65536 | Ascii  |  61,204.153 ns | 1,281.3684 ns | 1,198.5928 ns |  0.19 |    0.00 | 0.7022 | 0.7022 | 0.7022 |  262169 B |        2.00 |
| OwnedText.FromBytes(span)            | 65536 | Ascii  |   3,225.653 ns |   102.4599 ns |    95.8410 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 65536 | Ascii  |   3,336.961 ns |   132.6777 ns |   117.6153 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **65536** | **Cjk**    | **315,563.046 ns** | **3,681.3746 ns** | **3,263.4427 ns** |  **1.00** |    **0.01** |      **-** |      **-** |      **-** |  **131255 B** |        **1.00** |
| Text.FromBytes(span)                 | 65536 | Cjk    |  64,890.127 ns | 1,956.0336 ns | 1,733.9729 ns |  0.21 |    0.01 | 0.7576 | 0.7576 | 0.7576 |  262169 B |        2.00 |
| &#39;Text.FromBytes(span) no-count&#39;      | 65536 | Cjk    |  60,095.013 ns |   760.1046 ns |   593.4396 ns |  0.19 |    0.00 | 0.7102 | 0.7102 | 0.7102 |  262169 B |        2.00 |
| OwnedText.FromBytes(span)            | 65536 | Cjk    |   3,266.645 ns |   113.4407 ns |   106.1125 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 65536 | Cjk    |   3,345.695 ns |   155.2813 ns |   137.6528 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **65536** | **Emoji**  | **197,559.545 ns** | **3,278.7231 ns** | **3,066.9196 ns** | **1.000** |    **0.02** |      **-** |      **-** |      **-** |  **131250 B** |        **1.00** |
| Text.FromBytes(span)                 | 65536 | Emoji  |  27,734.938 ns |   894.0920 ns |   792.5892 ns | 0.140 |    0.00 | 0.3149 | 0.3149 | 0.3149 |  131092 B |        1.00 |
| &#39;Text.FromBytes(span) no-count&#39;      | 65536 | Emoji  |  48,174.992 ns | 1,401.5685 ns | 1,242.4540 ns | 0.244 |    0.01 | 0.5531 | 0.5531 | 0.5531 |  131103 B |        1.00 |
| OwnedText.FromBytes(span)            | 65536 | Emoji  |   1,292.571 ns |    27.4127 ns |    25.6418 ns | 0.007 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 65536 | Emoji  |   1,373.864 ns |    22.7357 ns |    20.1546 ns | 0.007 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **Encoding.UTF32.GetString(span)**       | **65536** | **Mixed**  | **311,655.654 ns** | **2,859.1971 ns** | **2,674.4947 ns** |  **1.00** |    **0.01** |      **-** |      **-** |      **-** |  **131256 B** |        **1.00** |
| Text.FromBytes(span)                 | 65536 | Mixed  |  64,814.187 ns | 3,129.9053 ns | 2,774.5795 ns |  0.21 |    0.01 | 0.7225 | 0.7225 | 0.7225 |  250766 B |        1.91 |
| &#39;Text.FromBytes(span) no-count&#39;      | 65536 | Mixed  |  74,884.516 ns | 1,081.3164 ns |   844.2207 ns |  0.24 |    0.00 | 0.8224 | 0.8224 | 0.8224 |  250778 B |        1.91 |
| OwnedText.FromBytes(span)            | 65536 | Mixed  |   3,152.269 ns |   146.8181 ns |   130.1504 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromBytes(span) no-count&#39; | 65536 | Mixed  |   3,149.254 ns |    96.3592 ns |    80.4643 ns |  0.01 |    0.00 |      - |      - |      - |         - |        0.00 |
