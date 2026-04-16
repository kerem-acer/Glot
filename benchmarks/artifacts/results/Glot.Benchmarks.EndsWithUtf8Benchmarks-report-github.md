```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | N     | Locale | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------ |------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **string.EndsWith**             | **64**    | **Ascii**  |  **0.5386 ns** | **0.1699 ns** | **0.0093 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Ascii  |  0.5609 ns | 0.6566 ns | 0.0360 ns |  1.04 |    0.06 |         - |          NA |
| U8String.EndsWith           | 64    | Ascii  |  0.5768 ns | 0.0674 ns | 0.0037 ns |  1.07 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Ascii  |  2.1641 ns | 0.0452 ns | 0.0025 ns |  4.02 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Ascii  | 12.7971 ns | 0.8865 ns | 0.0486 ns | 23.76 |    0.36 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Ascii  | 11.4804 ns | 0.7420 ns | 0.0407 ns | 21.32 |    0.33 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Ascii  |  0.5757 ns | 0.2612 ns | 0.0143 ns |  1.07 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  0.5393 ns | 0.2307 ns | 0.0126 ns |  1.00 |    0.03 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Ascii  |  0.5702 ns | 0.1120 ns | 0.0061 ns |  1.06 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Ascii  |  2.2383 ns | 0.5195 ns | 0.0285 ns |  4.16 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Ascii  | 12.7099 ns | 0.3696 ns | 0.0203 ns | 23.60 |    0.35 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Ascii  | 11.8668 ns | 0.3583 ns | 0.0196 ns | 22.03 |    0.33 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Latin**  |  **0.5757 ns** | **0.7477 ns** | **0.0410 ns** |  **1.00** |    **0.09** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Latin  |  0.5346 ns | 0.1345 ns | 0.0074 ns |  0.93 |    0.06 |         - |          NA |
| U8String.EndsWith           | 64    | Latin  |  0.5890 ns | 0.4632 ns | 0.0254 ns |  1.03 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Latin  |  2.2341 ns | 0.1523 ns | 0.0083 ns |  3.89 |    0.23 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Latin  | 13.3734 ns | 1.1826 ns | 0.0648 ns | 23.31 |    1.39 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Latin  | 11.3423 ns | 0.3977 ns | 0.0218 ns | 19.77 |    1.17 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Latin  |  0.5592 ns | 0.1422 ns | 0.0078 ns |  0.97 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Latin  |  0.5331 ns | 0.2375 ns | 0.0130 ns |  0.93 |    0.06 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Latin  |  0.5742 ns | 0.1565 ns | 0.0086 ns |  1.00 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Latin  |  2.2121 ns | 0.0846 ns | 0.0046 ns |  3.86 |    0.23 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Latin  | 12.6981 ns | 0.7276 ns | 0.0399 ns | 22.13 |    1.31 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Latin  | 11.4697 ns | 1.9429 ns | 0.1065 ns | 19.99 |    1.20 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Cjk**    |  **0.5412 ns** | **0.0233 ns** | **0.0013 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Cjk    |  0.5269 ns | 0.0753 ns | 0.0041 ns |  0.97 |    0.01 |         - |          NA |
| U8String.EndsWith           | 64    | Cjk    |  0.5684 ns | 0.0897 ns | 0.0049 ns |  1.05 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Cjk    |  2.2656 ns | 0.0826 ns | 0.0045 ns |  4.19 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Cjk    | 14.0896 ns | 0.9916 ns | 0.0544 ns | 26.04 |    0.10 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Cjk    | 12.1735 ns | 0.6543 ns | 0.0359 ns | 22.49 |    0.07 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Cjk    |  0.5463 ns | 0.1956 ns | 0.0107 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  0.5366 ns | 0.0404 ns | 0.0022 ns |  0.99 |    0.00 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Cjk    |  0.5786 ns | 0.1757 ns | 0.0096 ns |  1.07 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Cjk    |  2.2045 ns | 0.0871 ns | 0.0048 ns |  4.07 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Cjk    | 13.8363 ns | 0.7515 ns | 0.0412 ns | 25.57 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Cjk    | 12.6911 ns | 0.8756 ns | 0.0480 ns | 23.45 |    0.09 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Emoji**  |  **0.5520 ns** | **0.0396 ns** | **0.0022 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Emoji  |  0.5313 ns | 0.0521 ns | 0.0029 ns |  0.96 |    0.01 |         - |          NA |
| U8String.EndsWith           | 64    | Emoji  |  0.5857 ns | 0.1283 ns | 0.0070 ns |  1.06 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Emoji  |  2.2214 ns | 0.0460 ns | 0.0025 ns |  4.02 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Emoji  | 16.5751 ns | 1.6331 ns | 0.0895 ns | 30.03 |    0.17 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Emoji  | 12.4513 ns | 1.0770 ns | 0.0590 ns | 22.56 |    0.12 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Emoji  |  0.5697 ns | 0.6546 ns | 0.0359 ns |  1.03 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Emoji  |  0.5442 ns | 0.3018 ns | 0.0165 ns |  0.99 |    0.03 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Emoji  |  0.5698 ns | 0.0205 ns | 0.0011 ns |  1.03 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Emoji  |  2.2263 ns | 0.3089 ns | 0.0169 ns |  4.03 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Emoji  | 16.8549 ns | 3.1681 ns | 0.1737 ns | 30.53 |    0.29 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Emoji  | 12.8438 ns | 0.1135 ns | 0.0062 ns | 23.27 |    0.08 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **64**    | **Mixed**  |  **0.5369 ns** | **0.0964 ns** | **0.0053 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 64    | Mixed  |  0.5189 ns | 0.1792 ns | 0.0098 ns |  0.97 |    0.02 |         - |          NA |
| U8String.EndsWith           | 64    | Mixed  |  0.5792 ns | 0.1548 ns | 0.0085 ns |  1.08 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 64    | Mixed  |  2.2276 ns | 0.1010 ns | 0.0055 ns |  4.15 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 64    | Mixed  | 12.7152 ns | 0.6522 ns | 0.0358 ns | 23.68 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 64    | Mixed  | 11.4959 ns | 0.6070 ns | 0.0333 ns | 21.41 |    0.19 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 64    | Mixed  |  0.5555 ns | 0.1090 ns | 0.0060 ns |  1.03 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  0.5229 ns | 0.0468 ns | 0.0026 ns |  0.97 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 64    | Mixed  |  0.5688 ns | 0.1193 ns | 0.0065 ns |  1.06 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 64    | Mixed  |  2.2410 ns | 0.0176 ns | 0.0010 ns |  4.17 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 64    | Mixed  | 13.4483 ns | 4.9228 ns | 0.2698 ns | 25.05 |    0.49 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 64    | Mixed  | 11.8718 ns | 0.3067 ns | 0.0168 ns | 22.11 |    0.19 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Ascii**  |  **0.5976 ns** | **0.0778 ns** | **0.0043 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Ascii  |  0.5429 ns | 0.1413 ns | 0.0077 ns |  0.91 |    0.01 |         - |          NA |
| U8String.EndsWith           | 256   | Ascii  |  0.5604 ns | 0.0564 ns | 0.0031 ns |  0.94 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Ascii  |  2.1672 ns | 0.5438 ns | 0.0298 ns |  3.63 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Ascii  | 12.7971 ns | 0.7880 ns | 0.0432 ns | 21.41 |    0.15 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Ascii  | 11.8742 ns | 0.8187 ns | 0.0449 ns | 19.87 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Ascii  |  0.5480 ns | 0.0294 ns | 0.0016 ns |  0.92 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Ascii  |  0.5388 ns | 0.1436 ns | 0.0079 ns |  0.90 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 256   | Ascii  |  0.5775 ns | 0.3013 ns | 0.0165 ns |  0.97 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Ascii  |  2.2762 ns | 0.1295 ns | 0.0071 ns |  3.81 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Ascii  | 13.2574 ns | 3.6813 ns | 0.2018 ns | 22.18 |    0.32 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Ascii  | 11.5047 ns | 2.1167 ns | 0.1160 ns | 19.25 |    0.21 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Latin**  |  **0.5784 ns** | **0.6949 ns** | **0.0381 ns** |  **1.00** |    **0.08** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Latin  |  0.5490 ns | 0.2643 ns | 0.0145 ns |  0.95 |    0.06 |         - |          NA |
| U8String.EndsWith           | 256   | Latin  |  0.5695 ns | 0.0175 ns | 0.0010 ns |  0.99 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Latin  |  2.2138 ns | 0.1160 ns | 0.0064 ns |  3.84 |    0.22 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Latin  | 12.7451 ns | 1.2114 ns | 0.0664 ns | 22.10 |    1.25 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Latin  | 11.5069 ns | 0.3056 ns | 0.0168 ns | 19.95 |    1.13 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Latin  |  0.5553 ns | 0.2891 ns | 0.0158 ns |  0.96 |    0.06 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Latin  |  0.5325 ns | 0.0328 ns | 0.0018 ns |  0.92 |    0.05 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 256   | Latin  |  0.5634 ns | 0.0217 ns | 0.0012 ns |  0.98 |    0.06 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Latin  |  2.2875 ns | 0.2193 ns | 0.0120 ns |  3.97 |    0.22 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Latin  | 13.2244 ns | 1.9514 ns | 0.1070 ns | 22.93 |    1.30 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Latin  | 12.0144 ns | 1.3125 ns | 0.0719 ns | 20.83 |    1.18 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Cjk**    |  **0.5672 ns** | **0.0386 ns** | **0.0021 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Cjk    |  0.5447 ns | 0.4022 ns | 0.0220 ns |  0.96 |    0.03 |         - |          NA |
| U8String.EndsWith           | 256   | Cjk    |  0.5657 ns | 0.0854 ns | 0.0047 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Cjk    |  2.2759 ns | 0.4788 ns | 0.0262 ns |  4.01 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Cjk    | 13.9308 ns | 0.8669 ns | 0.0475 ns | 24.56 |    0.11 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Cjk    | 12.5075 ns | 0.1913 ns | 0.0105 ns | 22.05 |    0.07 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Cjk    |  0.5419 ns | 0.0496 ns | 0.0027 ns |  0.96 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Cjk    |  0.5658 ns | 0.5412 ns | 0.0297 ns |  1.00 |    0.05 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 256   | Cjk    |  0.5704 ns | 0.2004 ns | 0.0110 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Cjk    |  2.2120 ns | 0.2441 ns | 0.0134 ns |  3.90 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Cjk    | 14.3225 ns | 1.2731 ns | 0.0698 ns | 25.25 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Cjk    | 12.0946 ns | 0.5331 ns | 0.0292 ns | 21.32 |    0.08 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Emoji**  |  **0.5737 ns** | **0.2207 ns** | **0.0121 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Emoji  |  0.5467 ns | 0.1794 ns | 0.0098 ns |  0.95 |    0.02 |         - |          NA |
| U8String.EndsWith           | 256   | Emoji  |  0.5725 ns | 0.0536 ns | 0.0029 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Emoji  |  2.2265 ns | 0.1789 ns | 0.0098 ns |  3.88 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Emoji  | 17.7212 ns | 0.5863 ns | 0.0321 ns | 30.90 |    0.56 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Emoji  | 12.3163 ns | 0.7978 ns | 0.0437 ns | 21.48 |    0.39 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Emoji  |  0.5644 ns | 0.5371 ns | 0.0294 ns |  0.98 |    0.05 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Emoji  |  0.5380 ns | 0.2241 ns | 0.0123 ns |  0.94 |    0.03 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 256   | Emoji  |  0.5742 ns | 0.1688 ns | 0.0093 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Emoji  |  2.2229 ns | 0.0407 ns | 0.0022 ns |  3.88 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Emoji  | 17.7760 ns | 4.5701 ns | 0.2505 ns | 31.00 |    0.68 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Emoji  | 12.8555 ns | 1.0063 ns | 0.0552 ns | 22.42 |    0.41 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **256**   | **Mixed**  |  **0.5463 ns** | **0.0560 ns** | **0.0031 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 256   | Mixed  |  0.5411 ns | 0.0303 ns | 0.0017 ns |  0.99 |    0.01 |         - |          NA |
| U8String.EndsWith           | 256   | Mixed  |  0.5705 ns | 0.0627 ns | 0.0034 ns |  1.04 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 256   | Mixed  |  2.2105 ns | 0.0604 ns | 0.0033 ns |  4.05 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 256   | Mixed  | 14.7437 ns | 1.8896 ns | 0.1036 ns | 26.99 |    0.21 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 256   | Mixed  | 12.9013 ns | 0.8775 ns | 0.0481 ns | 23.61 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 256   | Mixed  |  0.5419 ns | 0.0641 ns | 0.0035 ns |  0.99 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 256   | Mixed  |  0.5295 ns | 0.2274 ns | 0.0125 ns |  0.97 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 256   | Mixed  |  0.5785 ns | 0.0318 ns | 0.0017 ns |  1.06 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 256   | Mixed  |  2.2380 ns | 0.1057 ns | 0.0058 ns |  4.10 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 256   | Mixed  | 14.6261 ns | 0.5219 ns | 0.0286 ns | 26.77 |    0.14 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 256   | Mixed  | 12.3771 ns | 0.6333 ns | 0.0347 ns | 22.66 |    0.12 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Ascii**  |  **0.8564 ns** | **0.5964 ns** | **0.0327 ns** |  **1.00** |    **0.05** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Ascii  |  0.5402 ns | 0.1447 ns | 0.0079 ns |  0.63 |    0.02 |         - |          NA |
| U8String.EndsWith           | 4096  | Ascii  |  0.5728 ns | 0.1353 ns | 0.0074 ns |  0.67 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Ascii  |  2.1705 ns | 0.1737 ns | 0.0095 ns |  2.54 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Ascii  | 13.3309 ns | 0.8451 ns | 0.0463 ns | 15.58 |    0.51 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Ascii  | 11.3230 ns | 0.6514 ns | 0.0357 ns | 13.23 |    0.43 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Ascii  |  0.5662 ns | 0.1793 ns | 0.0098 ns |  0.66 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  0.5311 ns | 0.1926 ns | 0.0106 ns |  0.62 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Ascii  |  0.5624 ns | 0.0706 ns | 0.0039 ns |  0.66 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Ascii  |  2.2182 ns | 0.0402 ns | 0.0022 ns |  2.59 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Ascii  | 13.1866 ns | 0.3401 ns | 0.0186 ns | 15.41 |    0.50 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Ascii  | 11.9639 ns | 0.3821 ns | 0.0209 ns | 13.98 |    0.46 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Latin**  |  **0.5566 ns** | **0.0612 ns** | **0.0034 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Latin  |  0.5469 ns | 0.0814 ns | 0.0045 ns |  0.98 |    0.01 |         - |          NA |
| U8String.EndsWith           | 4096  | Latin  |  0.5683 ns | 0.1042 ns | 0.0057 ns |  1.02 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Latin  |  2.2243 ns | 0.2825 ns | 0.0155 ns |  4.00 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Latin  | 12.9136 ns | 0.2562 ns | 0.0140 ns | 23.20 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Latin  | 11.9368 ns | 0.3741 ns | 0.0205 ns | 21.45 |    0.12 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Latin  |  0.5612 ns | 0.1298 ns | 0.0071 ns |  1.01 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Latin  |  0.5231 ns | 0.0779 ns | 0.0043 ns |  0.94 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Latin  |  0.5645 ns | 0.0899 ns | 0.0049 ns |  1.01 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Latin  |  2.2227 ns | 0.0518 ns | 0.0028 ns |  3.99 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Latin  | 12.8942 ns | 0.5271 ns | 0.0289 ns | 23.17 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Latin  | 11.8442 ns | 0.5195 ns | 0.0285 ns | 21.28 |    0.12 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Cjk**    |  **0.5572 ns** | **0.1075 ns** | **0.0059 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Cjk    |  0.5278 ns | 0.0112 ns | 0.0006 ns |  0.95 |    0.01 |         - |          NA |
| U8String.EndsWith           | 4096  | Cjk    |  0.5671 ns | 0.0343 ns | 0.0019 ns |  1.02 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Cjk    |  2.2216 ns | 0.0422 ns | 0.0023 ns |  3.99 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Cjk    | 13.9791 ns | 1.6224 ns | 0.0889 ns | 25.09 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Cjk    | 12.4397 ns | 0.7783 ns | 0.0427 ns | 22.33 |    0.21 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Cjk    |  0.8319 ns | 0.1790 ns | 0.0098 ns |  1.49 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  0.5409 ns | 0.0837 ns | 0.0046 ns |  0.97 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Cjk    |  0.5815 ns | 0.0814 ns | 0.0045 ns |  1.04 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Cjk    |  2.2268 ns | 0.0243 ns | 0.0013 ns |  4.00 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Cjk    | 13.9159 ns | 1.7621 ns | 0.0966 ns | 24.98 |    0.27 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Cjk    | 12.5693 ns | 0.6932 ns | 0.0380 ns | 22.56 |    0.21 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Emoji**  |  **0.8374 ns** | **0.1579 ns** | **0.0087 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Emoji  |  0.5470 ns | 0.2281 ns | 0.0125 ns |  0.65 |    0.01 |         - |          NA |
| U8String.EndsWith           | 4096  | Emoji  |  0.5725 ns | 0.1549 ns | 0.0085 ns |  0.68 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Emoji  |  2.2212 ns | 0.0985 ns | 0.0054 ns |  2.65 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Emoji  | 17.0303 ns | 0.2470 ns | 0.0135 ns | 20.34 |    0.18 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Emoji  | 12.3817 ns | 0.5064 ns | 0.0278 ns | 14.79 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Emoji  |  0.6013 ns | 0.7500 ns | 0.0411 ns |  0.72 |    0.04 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Emoji  |  0.5218 ns | 0.0237 ns | 0.0013 ns |  0.62 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Emoji  |  0.5757 ns | 0.1210 ns | 0.0066 ns |  0.69 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Emoji  |  2.2310 ns | 0.3233 ns | 0.0177 ns |  2.66 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Emoji  | 17.6250 ns | 0.5332 ns | 0.0292 ns | 21.05 |    0.19 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Emoji  | 12.8232 ns | 0.3084 ns | 0.0169 ns | 15.31 |    0.14 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **4096**  | **Mixed**  |  **0.5676 ns** | **0.2321 ns** | **0.0127 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 4096  | Mixed  |  0.5645 ns | 0.4242 ns | 0.0232 ns |  0.99 |    0.04 |         - |          NA |
| U8String.EndsWith           | 4096  | Mixed  |  0.5927 ns | 0.5146 ns | 0.0282 ns |  1.04 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 4096  | Mixed  |  2.2174 ns | 0.0835 ns | 0.0046 ns |  3.91 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 4096  | Mixed  | 12.7959 ns | 1.2609 ns | 0.0691 ns | 22.55 |    0.45 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 4096  | Mixed  | 11.5087 ns | 0.2483 ns | 0.0136 ns | 20.28 |    0.40 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 4096  | Mixed  |  0.5610 ns | 0.1919 ns | 0.0105 ns |  0.99 |    0.03 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  0.5311 ns | 0.0255 ns | 0.0014 ns |  0.94 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 4096  | Mixed  |  0.5568 ns | 0.0304 ns | 0.0017 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 4096  | Mixed  |  2.2347 ns | 0.2349 ns | 0.0129 ns |  3.94 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 4096  | Mixed  | 13.3566 ns | 0.6065 ns | 0.0332 ns | 23.54 |    0.46 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 4096  | Mixed  | 11.4946 ns | 0.5259 ns | 0.0288 ns | 20.26 |    0.40 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Ascii**  |  **0.5442 ns** | **0.0717 ns** | **0.0039 ns** |  **1.00** |    **0.01** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Ascii  |  0.5293 ns | 0.0727 ns | 0.0040 ns |  0.97 |    0.01 |         - |          NA |
| U8String.EndsWith           | 65536 | Ascii  |  0.5599 ns | 0.0486 ns | 0.0027 ns |  1.03 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Ascii  |  2.1864 ns | 0.2484 ns | 0.0136 ns |  4.02 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Ascii  | 13.5723 ns | 1.5048 ns | 0.0825 ns | 24.94 |    0.20 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Ascii  | 11.7715 ns | 0.3915 ns | 0.0215 ns | 21.63 |    0.14 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Ascii  |  0.5380 ns | 0.0468 ns | 0.0026 ns |  0.99 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  0.5305 ns | 0.0251 ns | 0.0014 ns |  0.97 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Ascii  |  0.5468 ns | 0.0055 ns | 0.0003 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Ascii  |  2.2810 ns | 0.3918 ns | 0.0215 ns |  4.19 |    0.04 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Ascii  | 13.3761 ns | 0.3591 ns | 0.0197 ns | 24.58 |    0.16 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Ascii  | 11.7274 ns | 1.3977 ns | 0.0766 ns | 21.55 |    0.18 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Latin**  |  **0.5537 ns** | **0.1506 ns** | **0.0083 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Latin  |  0.5491 ns | 0.0326 ns | 0.0018 ns |  0.99 |    0.01 |         - |          NA |
| U8String.EndsWith           | 65536 | Latin  |  0.5455 ns | 0.0415 ns | 0.0023 ns |  0.99 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Latin  |  2.3365 ns | 0.5241 ns | 0.0287 ns |  4.22 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Latin  | 12.9443 ns | 1.5458 ns | 0.0847 ns | 23.38 |    0.33 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Latin  | 11.7099 ns | 0.8246 ns | 0.0452 ns | 21.15 |    0.28 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Latin  |  0.5458 ns | 0.0610 ns | 0.0033 ns |  0.99 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Latin  |  0.5350 ns | 0.2517 ns | 0.0138 ns |  0.97 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Latin  |  0.5535 ns | 0.0329 ns | 0.0018 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Latin  |  2.2575 ns | 0.1257 ns | 0.0069 ns |  4.08 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Latin  | 12.8336 ns | 0.6594 ns | 0.0361 ns | 23.18 |    0.30 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Latin  | 12.1286 ns | 0.7842 ns | 0.0430 ns | 21.91 |    0.29 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Cjk**    |  **0.5384 ns** | **0.1728 ns** | **0.0095 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Cjk    |  0.5431 ns | 0.1426 ns | 0.0078 ns |  1.01 |    0.02 |         - |          NA |
| U8String.EndsWith           | 65536 | Cjk    |  0.5481 ns | 0.0493 ns | 0.0027 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Cjk    |  2.3849 ns | 1.2152 ns | 0.0666 ns |  4.43 |    0.13 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Cjk    | 13.9665 ns | 1.3509 ns | 0.0741 ns | 25.95 |    0.41 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Cjk    | 12.1962 ns | 0.2949 ns | 0.0162 ns | 22.66 |    0.34 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Cjk    |  0.5496 ns | 0.1178 ns | 0.0065 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  0.4869 ns | 0.1135 ns | 0.0062 ns |  0.90 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Cjk    |  0.5471 ns | 0.0089 ns | 0.0005 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Cjk    |  2.2773 ns | 0.1328 ns | 0.0073 ns |  4.23 |    0.07 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Cjk    | 14.2719 ns | 0.6785 ns | 0.0372 ns | 26.51 |    0.41 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Cjk    | 12.3008 ns | 2.2053 ns | 0.1209 ns | 22.85 |    0.40 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Emoji**  |  **0.5692 ns** | **0.2417 ns** | **0.0132 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Emoji  |  0.5505 ns | 0.0915 ns | 0.0050 ns |  0.97 |    0.02 |         - |          NA |
| U8String.EndsWith           | 65536 | Emoji  |  0.5465 ns | 0.1042 ns | 0.0057 ns |  0.96 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Emoji  |  2.2755 ns | 0.2736 ns | 0.0150 ns |  4.00 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Emoji  | 18.1081 ns | 3.2272 ns | 0.1769 ns | 31.82 |    0.69 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Emoji  | 12.4515 ns | 0.6558 ns | 0.0359 ns | 21.88 |    0.44 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Emoji  |  0.5493 ns | 0.1721 ns | 0.0094 ns |  0.97 |    0.02 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Emoji  |  0.5306 ns | 0.0384 ns | 0.0021 ns |  0.93 |    0.02 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Emoji  |  0.5757 ns | 0.2456 ns | 0.0135 ns |  1.01 |    0.03 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Emoji  |  2.2289 ns | 0.1222 ns | 0.0067 ns |  3.92 |    0.08 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Emoji  | 18.1268 ns | 1.6191 ns | 0.0887 ns | 31.86 |    0.65 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Emoji  | 12.8974 ns | 1.0939 ns | 0.0600 ns | 22.67 |    0.46 |         - |          NA |
|                             |       |        |            |           |           |       |         |           |             |
| **string.EndsWith**             | **65536** | **Mixed**  |  **0.5424 ns** | **0.0337 ns** | **0.0018 ns** |  **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.EndsWith UTF-8&#39;       | 65536 | Mixed  |  0.5538 ns | 0.3108 ns | 0.0170 ns |  1.02 |    0.03 |         - |          NA |
| U8String.EndsWith           | 65536 | Mixed  |  0.5542 ns | 0.0368 ns | 0.0020 ns |  1.02 |    0.00 |         - |          NA |
| &#39;Text.EndsWith UTF-8&#39;       | 65536 | Mixed  |  2.3478 ns | 0.4996 ns | 0.0274 ns |  4.33 |    0.05 |         - |          NA |
| &#39;Text.EndsWith UTF-16&#39;      | 65536 | Mixed  | 12.8221 ns | 0.7139 ns | 0.0391 ns | 23.64 |    0.09 |         - |          NA |
| &#39;Text.EndsWith UTF-32&#39;      | 65536 | Mixed  | 12.1449 ns | 0.4792 ns | 0.0263 ns | 22.39 |    0.08 |         - |          NA |
| &#39;string.EndsWith miss&#39;      | 65536 | Mixed  |  0.5403 ns | 0.1408 ns | 0.0077 ns |  1.00 |    0.01 |         - |          NA |
| &#39;Span.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  0.5367 ns | 0.0953 ns | 0.0052 ns |  0.99 |    0.01 |         - |          NA |
| &#39;U8String.EndsWith miss&#39;    | 65536 | Mixed  |  0.5548 ns | 0.1918 ns | 0.0105 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-8 miss&#39;  | 65536 | Mixed  |  2.2773 ns | 0.2297 ns | 0.0126 ns |  4.20 |    0.02 |         - |          NA |
| &#39;Text.EndsWith UTF-16 miss&#39; | 65536 | Mixed  | 13.3009 ns | 1.1548 ns | 0.0633 ns | 24.52 |    0.12 |         - |          NA |
| &#39;Text.EndsWith UTF-32 miss&#39; | 65536 | Mixed  | 11.7231 ns | 0.7720 ns | 0.0423 ns | 21.61 |    0.09 |         - |          NA |
