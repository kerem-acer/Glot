```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                               | N     | Locale | Mean            | Error          | StdDev        | Ratio   | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |------ |------- |----------------:|---------------:|--------------:|--------:|--------:|----------:|------------:|
| **string.Equals**                        | **65536** | **Ascii**  |   **2,387.4606 ns** |    **189.2748 ns** |    **10.3748 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Ascii  |   1,044.3198 ns |    267.7866 ns |    14.6783 ns |    0.44 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Ascii  |   5,127.4472 ns |    210.9714 ns |    11.5641 ns |    2.15 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Ascii  |   2,407.5638 ns |    354.5381 ns |    19.4334 ns |    1.01 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Ascii  | 311,235.5955 ns | 42,079.2971 ns | 2,306.5083 ns |  130.36 |    0.97 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Ascii  |   2,537.6566 ns |    180.2533 ns |     9.8803 ns |    1.06 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Ascii  |   1,083.3342 ns |    109.2272 ns |     5.9871 ns |    0.45 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Ascii  |   5,126.3986 ns |     92.5041 ns |     5.0705 ns |    2.15 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Ascii  |   2,571.0793 ns |     82.8544 ns |     4.5415 ns |    1.08 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Ascii  | 318,785.9906 ns | 26,654.9810 ns | 1,461.0495 ns |  133.53 |    0.73 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Cjk**    |   **2,409.8979 ns** |     **30.3942 ns** |     **1.6660 ns** |   **1.000** |    **0.00** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Cjk    |   3,455.1562 ns |    423.7809 ns |    23.2289 ns |   1.434 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Cjk    |  50,157.5563 ns |  3,017.8470 ns |   165.4184 ns |  20.813 |    0.06 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Cjk    |   2,422.7350 ns |    159.1336 ns |     8.7227 ns |   1.005 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Cjk    | 312,345.7164 ns | 66,544.5537 ns | 3,647.5315 ns | 129.610 |    1.31 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Cjk    |   2,432.6437 ns |    324.8363 ns |    17.8054 ns |   1.009 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Cjk    |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Cjk    |  49,885.2136 ns |  6,505.4217 ns |   356.5841 ns |  20.700 |    0.13 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Cjk    |   2,436.1666 ns |    673.3914 ns |    36.9109 ns |   1.011 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Cjk    | 314,373.5352 ns | 26,111.6906 ns | 1,431.2699 ns | 130.451 |    0.52 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Emoji**  |   **2,408.5016 ns** |    **328.5768 ns** |    **18.0104 ns** |   **1.000** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Emoji  |   2,356.6831 ns |    375.8499 ns |    20.6016 ns |   0.979 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Emoji  |  59,277.9414 ns |  5,097.9624 ns |   279.4365 ns |  24.613 |    0.19 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Emoji  |   2,435.7580 ns |     92.0910 ns |     5.0478 ns |   1.011 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Emoji  | 167,325.2089 ns | 24,721.0345 ns | 1,355.0433 ns |  69.475 |    0.66 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Emoji  |   2,381.9256 ns |    202.4091 ns |    11.0947 ns |   0.989 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Emoji  |       0.0000 ns |      0.0000 ns |     0.0000 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Emoji  |       1.0092 ns |      0.0919 ns |     0.0050 ns |   0.000 |    0.00 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Emoji  |   2,401.3047 ns |    647.9171 ns |    35.5145 ns |   0.997 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Emoji  |       1.0250 ns |      0.3660 ns |     0.0201 ns |   0.000 |    0.00 |         - |          NA |
|                                      |       |        |                 |                |               |         |         |           |             |
| **string.Equals**                        | **65536** | **Mixed**  |   **2,398.2853 ns** |    **481.2896 ns** |    **26.3811 ns** |    **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.SequenceEqual UTF-8&#39;           | 65536 | Mixed  |   1,590.8121 ns |    149.5793 ns |     8.1989 ns |    0.66 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8&#39;                  | 65536 | Mixed  |  25,280.0259 ns |  1,107.8699 ns |    60.7261 ns |   10.54 |    0.10 |         - |          NA |
| &#39;Text.Equals UTF-16&#39;                 | 65536 | Mixed  |   2,425.9757 ns |  1,402.7714 ns |    76.8906 ns |    1.01 |    0.03 |         - |          NA |
| &#39;Text.Equals UTF-32&#39;                 | 65536 | Mixed  | 317,267.5783 ns | 30,856.7986 ns | 1,691.3653 ns |  132.30 |    1.41 |         - |          NA |
| &#39;string.Equals different&#39;            | 65536 | Mixed  |   2,428.5483 ns |    192.0430 ns |    10.5265 ns |    1.01 |    0.01 |         - |          NA |
| &#39;Span.SequenceEqual UTF-8 different&#39; | 65536 | Mixed  |   1,640.3260 ns |    436.4734 ns |    23.9246 ns |    0.68 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-8 different&#39;        | 65536 | Mixed  |  27,894.0463 ns | 21,569.3191 ns | 1,182.2872 ns |   11.63 |    0.44 |         - |          NA |
| &#39;Text.Equals UTF-16 different&#39;       | 65536 | Mixed  |   2,462.3863 ns |    115.1991 ns |     6.3145 ns |    1.03 |    0.01 |         - |          NA |
| &#39;Text.Equals UTF-32 different&#39;       | 65536 | Mixed  | 320,260.9456 ns | 40,355.0299 ns | 2,211.9954 ns |  133.55 |    1.51 |         - |          NA |
