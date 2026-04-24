```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  IterationTime=150ms  
MaxIterationCount=30  

```
| Method                        | N     | Locale | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  | **0.0005 ns** | **0.0022 ns** | **0.0019 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  | 0.2483 ns | 0.0234 ns | 0.0219 ns | 0.2368 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  | 7.3972 ns | 0.0741 ns | 0.0619 ns | 7.4052 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  | 1.4461 ns | 0.0220 ns | 0.0206 ns | 1.4372 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  | 6.7504 ns | 0.0644 ns | 0.0538 ns | 6.7530 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  | 0.0084 ns | 0.0113 ns | 0.0100 ns | 0.0069 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 0.0028 ns | 0.0117 ns | 0.0103 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  | 7.5769 ns | 0.0986 ns | 0.0923 ns | 7.5528 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  | 1.5771 ns | 0.0298 ns | 0.0265 ns | 1.5723 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  | 6.8232 ns | 0.0503 ns | 0.0446 ns | 6.8140 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    | 8.0599 ns | 0.0577 ns | 0.0482 ns | 8.0514 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    | 1.4941 ns | 0.0116 ns | 0.0097 ns | 1.4915 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    | 6.7965 ns | 0.0673 ns | 0.0629 ns | 6.7572 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    | 8.0360 ns | 0.0504 ns | 0.0393 ns | 8.0432 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    | 1.4715 ns | 0.0259 ns | 0.0243 ns | 1.4670 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    | 6.7644 ns | 0.0702 ns | 0.0656 ns | 6.7396 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Emoji**  | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Emoji  | 0.2383 ns | 0.0188 ns | 0.0175 ns | 0.2328 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Emoji  | 9.2396 ns | 0.0926 ns | 0.0821 ns | 9.2525 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Emoji  | 1.5242 ns | 0.0090 ns | 0.0075 ns | 1.5252 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Emoji  | 8.0394 ns | 0.0935 ns | 0.0875 ns | 8.0261 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 64    | Emoji  | 0.0046 ns | 0.0111 ns | 0.0093 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Emoji  | 0.2448 ns | 0.0099 ns | 0.0082 ns | 0.2441 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Emoji  | 9.3114 ns | 0.1057 ns | 0.0989 ns | 9.3406 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Emoji  | 1.5486 ns | 0.0345 ns | 0.0322 ns | 1.5578 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Emoji  | 8.0052 ns | 0.0981 ns | 0.0766 ns | 8.0250 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  | **0.0041 ns** | **0.0092 ns** | **0.0086 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  | 0.0071 ns | 0.0068 ns | 0.0056 ns | 0.0067 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  | 7.5624 ns | 0.0725 ns | 0.0642 ns | 7.5525 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  | 1.5233 ns | 0.0128 ns | 0.0119 ns | 1.5213 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  | 6.8340 ns | 0.0983 ns | 0.0872 ns | 6.8046 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  | 8.0839 ns | 0.0932 ns | 0.0872 ns | 8.0706 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  | 1.5170 ns | 0.0231 ns | 0.0216 ns | 1.5127 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  | 6.7589 ns | 0.0554 ns | 0.0463 ns | 6.7600 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  | **0.0025 ns** | **0.0083 ns** | **0.0073 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  | 0.2699 ns | 0.0271 ns | 0.0241 ns | 0.2636 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  | 7.4308 ns | 0.0788 ns | 0.0737 ns | 7.4315 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  | 1.4863 ns | 0.0163 ns | 0.0136 ns | 1.4843 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  | 6.7727 ns | 0.0550 ns | 0.0487 ns | 6.7745 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  | 0.0046 ns | 0.0087 ns | 0.0081 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  | 7.5004 ns | 0.0661 ns | 0.0618 ns | 7.4946 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  | 1.5795 ns | 0.0300 ns | 0.0281 ns | 1.5717 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  | 6.7641 ns | 0.0661 ns | 0.0586 ns | 6.7812 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    | **0.0006 ns** | **0.0024 ns** | **0.0020 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    | 8.0429 ns | 0.0811 ns | 0.0719 ns | 8.0341 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    | 1.5055 ns | 0.0392 ns | 0.0366 ns | 1.4849 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    | 6.7292 ns | 0.0315 ns | 0.0263 ns | 6.7335 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    | 8.0757 ns | 0.0608 ns | 0.0508 ns | 8.0474 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    | 1.4575 ns | 0.0146 ns | 0.0114 ns | 1.4602 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    | 6.7948 ns | 0.0750 ns | 0.0702 ns | 6.7675 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Emoji**  | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Emoji  | 0.2290 ns | 0.0075 ns | 0.0059 ns | 0.2314 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Emoji  | 9.2413 ns | 0.0810 ns | 0.0676 ns | 9.2473 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Emoji  | 1.5603 ns | 0.0307 ns | 0.0272 ns | 1.5615 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Emoji  | 8.0898 ns | 0.1046 ns | 0.0979 ns | 8.0670 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Emoji  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  | 0.2442 ns | 0.0246 ns | 0.0230 ns | 0.2396 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  | 9.2389 ns | 0.0858 ns | 0.0716 ns | 9.2661 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Emoji  | 1.6178 ns | 0.0288 ns | 0.0270 ns | 1.6140 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Emoji  | 7.9955 ns | 0.1420 ns | 0.1328 ns | 7.9879 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  | 7.5095 ns | 0.1346 ns | 0.1124 ns | 7.5087 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  | 1.5317 ns | 0.0115 ns | 0.0089 ns | 1.5323 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  | 6.7833 ns | 0.0250 ns | 0.0195 ns | 6.7810 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  | 0.0011 ns | 0.0038 ns | 0.0031 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  | 8.0505 ns | 0.1183 ns | 0.1106 ns | 8.0270 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  | 1.4373 ns | 0.0257 ns | 0.0241 ns | 1.4267 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  | 6.7468 ns | 0.0543 ns | 0.0481 ns | 6.7555 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  | 0.2389 ns | 0.0091 ns | 0.0076 ns | 0.2385 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  | 7.5029 ns | 0.1044 ns | 0.0976 ns | 7.5378 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  | 1.5158 ns | 0.0130 ns | 0.0108 ns | 1.5142 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  | 6.7107 ns | 0.0722 ns | 0.0676 ns | 6.7136 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 0.0010 ns | 0.0044 ns | 0.0039 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  | 7.4909 ns | 0.0559 ns | 0.0496 ns | 7.4800 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  | 1.5868 ns | 0.0336 ns | 0.0314 ns | 1.5916 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  | 6.7322 ns | 0.0758 ns | 0.0709 ns | 6.7248 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    | **0.0007 ns** | **0.0027 ns** | **0.0023 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    | 8.1398 ns | 0.1187 ns | 0.1052 ns | 8.1397 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    | 1.4906 ns | 0.0282 ns | 0.0250 ns | 1.4800 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    | 6.7816 ns | 0.0746 ns | 0.0661 ns | 6.7805 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    | 7.9690 ns | 0.0676 ns | 0.0565 ns | 7.9627 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 1.4895 ns | 0.0171 ns | 0.0143 ns | 1.4900 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    | 6.7371 ns | 0.0809 ns | 0.0718 ns | 6.7234 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Emoji**  | **0.0062 ns** | **0.0125 ns** | **0.0117 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Emoji  | 0.2238 ns | 0.0090 ns | 0.0075 ns | 0.2242 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Emoji  | 9.2423 ns | 0.1306 ns | 0.1222 ns | 9.2302 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Emoji  | 1.5111 ns | 0.0138 ns | 0.0115 ns | 1.5090 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Emoji  | 7.9984 ns | 0.1544 ns | 0.1444 ns | 7.9984 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Emoji  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  | 0.2368 ns | 0.0170 ns | 0.0150 ns | 0.2314 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  | 9.1063 ns | 0.1127 ns | 0.1055 ns | 9.0941 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Emoji  | 1.6000 ns | 0.0324 ns | 0.0303 ns | 1.5984 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Emoji  | 8.0223 ns | 0.0713 ns | 0.0667 ns | 8.0291 ns |     ? |       ? |         - |           ? |
|                               |       |        |           |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** | **0.0000 ns** |     **?** |       **?** |         **-** |           **?** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  | 7.4980 ns | 0.1070 ns | 0.1001 ns | 7.5268 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  | 1.5271 ns | 0.0300 ns | 0.0281 ns | 1.5151 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  | 6.7975 ns | 0.0544 ns | 0.0454 ns | 6.8057 ns |     ? |       ? |         - |           ? |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  | 0.0053 ns | 0.0124 ns | 0.0110 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  | 8.0746 ns | 0.0861 ns | 0.0806 ns | 8.0703 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  | 1.4863 ns | 0.0221 ns | 0.0207 ns | 1.4785 ns |     ? |       ? |         - |           ? |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  | 6.7678 ns | 0.0775 ns | 0.0725 ns | 6.7675 ns |     ? |       ? |         - |           ? |
