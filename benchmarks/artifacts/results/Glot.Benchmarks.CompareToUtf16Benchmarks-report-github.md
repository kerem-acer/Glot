```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                         | N     | Locale | Mean            | Error             | StdDev         | Median          | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |----------------:|------------------:|---------------:|----------------:|------:|--------:|----------:|------------:|
| **string.Compare**                 | **8**     | **Ascii**  |       **1.6290 ns** |        **20.2193 ns** |      **1.1083 ns** |       **1.0032 ns** |  **1.28** |    **0.98** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Ascii  |       2.2599 ns |         5.8656 ns |      0.3215 ns |       2.1922 ns |  1.78 |    0.79 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Ascii  |       2.5293 ns |         0.2946 ns |      0.0161 ns |       2.5322 ns |  1.99 |    0.84 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Ascii  |       8.6255 ns |         1.0813 ns |      0.0593 ns |       8.6226 ns |  6.80 |    2.88 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Ascii  |      25.4855 ns |         6.7755 ns |      0.3714 ns |      25.5697 ns | 20.10 |    8.51 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **8**     | **Cjk**    |       **0.9808 ns** |         **0.3406 ns** |      **0.0187 ns** |       **0.9818 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Cjk    |       4.5479 ns |         0.4218 ns |      0.0231 ns |       4.5573 ns |  4.64 |    0.08 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Cjk    |       5.2542 ns |         2.7064 ns |      0.1483 ns |       5.2139 ns |  5.36 |    0.16 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Cjk    |      15.3637 ns |         1.3951 ns |      0.0765 ns |      15.3739 ns | 15.67 |    0.27 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Cjk    |      30.9085 ns |         4.6463 ns |      0.2547 ns |      31.0281 ns | 31.52 |    0.57 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **8**     | **Mixed**  |       **0.9444 ns** |         **0.1368 ns** |      **0.0075 ns** |       **0.9403 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 8     | Mixed  |       2.4205 ns |         3.2789 ns |      0.1797 ns |       2.3599 ns |  2.56 |    0.17 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 8     | Mixed  |       2.5279 ns |         0.6114 ns |      0.0335 ns |       2.5184 ns |  2.68 |    0.04 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 8     | Mixed  |       8.5468 ns |         1.6454 ns |      0.0902 ns |       8.5542 ns |  9.05 |    0.10 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 8     | Mixed  |      26.6415 ns |        27.0271 ns |      1.4814 ns |      25.8410 ns | 28.21 |    1.37 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **256**   | **Ascii**  |      **11.6829 ns** |         **0.7933 ns** |      **0.0435 ns** |      **11.6857 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Ascii  |       9.1872 ns |         3.7304 ns |      0.2045 ns |       9.2095 ns |  0.79 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Ascii  |       9.8573 ns |         7.3869 ns |      0.4049 ns |       9.6485 ns |  0.84 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Ascii  |      25.8613 ns |         1.5170 ns |      0.0832 ns |      25.9080 ns |  2.21 |    0.01 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Ascii  |     603.9977 ns |        29.4426 ns |      1.6138 ns |     604.6445 ns | 51.70 |    0.21 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **256**   | **Cjk**    |      **11.5039 ns** |         **2.9134 ns** |      **0.1597 ns** |      **11.5058 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Cjk    |      17.3063 ns |        10.9454 ns |      0.6000 ns |      16.9911 ns |  1.50 |    0.05 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Cjk    |      17.4982 ns |         0.2262 ns |      0.0124 ns |      17.4927 ns |  1.52 |    0.02 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Cjk    |     171.5792 ns |        20.2871 ns |      1.1120 ns |     171.8291 ns | 14.92 |    0.20 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Cjk    |     741.9409 ns |       157.0473 ns |      8.6083 ns |     742.7492 ns | 64.50 |    1.01 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **256**   | **Mixed**  |      **13.4913 ns** |        **58.2254 ns** |      **3.1915 ns** |      **11.7198 ns** |  **1.03** |    **0.28** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 256   | Mixed  |      10.4664 ns |        10.6522 ns |      0.5839 ns |      10.1306 ns |  0.80 |    0.15 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 256   | Mixed  |      10.2288 ns |         0.7890 ns |      0.0432 ns |      10.2152 ns |  0.78 |    0.14 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 256   | Mixed  |      99.6007 ns |         9.8750 ns |      0.5413 ns |      99.4084 ns |  7.63 |    1.38 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 256   | Mixed  |     629.9321 ns |        90.4638 ns |      4.9586 ns |     631.8798 ns | 48.28 |    8.71 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **65536** | **Ascii**  |   **3,408.9985 ns** |       **170.2132 ns** |      **9.3300 ns** |   **3,408.1778 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Ascii  |   1,053.8117 ns |       231.4399 ns |     12.6860 ns |   1,054.2111 ns |  0.31 |    0.00 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Ascii  |   1,113.6190 ns |     1,810.5552 ns |     99.2426 ns |   1,071.1716 ns |  0.33 |    0.03 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Ascii  |   3,427.0900 ns |    15,686.2362 ns |    859.8155 ns |   2,957.8668 ns |  1.01 |    0.22 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Ascii  | 149,356.8692 ns |     8,142.3447 ns |    446.3094 ns | 149,393.5039 ns | 43.81 |    0.15 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **65536** | **Cjk**    |   **3,782.0168 ns** |    **17,461.2935 ns** |    **957.1124 ns** |   **3,238.9503 ns** |  **1.04** |    **0.30** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Cjk    |   3,455.3132 ns |     1,830.3034 ns |    100.3251 ns |   3,466.9290 ns |  0.95 |    0.18 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Cjk    |   3,345.1436 ns |       247.1142 ns |     13.5452 ns |   3,351.7354 ns |  0.92 |    0.18 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Cjk    |  41,888.0925 ns |    56,382.4377 ns |  3,090.5117 ns |  43,350.3202 ns | 11.50 |    2.32 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Cjk    | 218,569.8887 ns | 1,043,484.5149 ns | 57,196.9076 ns | 185,972.2798 ns | 60.03 |   17.97 |         - |          NA |
|                                |       |        |                 |                   |                |                 |       |         |           |             |
| **string.Compare**                 | **65536** | **Mixed**  |   **3,573.4632 ns** |     **6,589.3219 ns** |    **361.1830 ns** |   **3,548.8777 ns** |  **1.01** |    **0.12** |         **-** |          **NA** |
| &#39;Span.SequenceCompareTo UTF-8&#39; | 65536 | Mixed  |   1,853.9093 ns |     7,507.0989 ns |    411.4894 ns |   1,619.2726 ns |  0.52 |    0.11 |         - |          NA |
| &#39;Text.CompareTo UTF-8&#39;         | 65536 | Mixed  |   1,813.5778 ns |     2,928.0368 ns |    160.4956 ns |   1,896.7853 ns |  0.51 |    0.06 |         - |          NA |
| &#39;Text.CompareTo UTF-16&#39;        | 65536 | Mixed  |  21,275.7096 ns |     9,356.8866 ns |    512.8825 ns |  21,508.1717 ns |  5.99 |    0.54 |         - |          NA |
| &#39;Text.CompareTo UTF-32&#39;        | 65536 | Mixed  | 159,173.5974 ns |   165,785.8717 ns |  9,087.2831 ns | 154,631.0220 ns | 44.85 |    4.49 |         - |          NA |
