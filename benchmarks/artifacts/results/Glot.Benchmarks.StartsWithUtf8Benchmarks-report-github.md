```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                        | N     | Locale | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------ |------ |------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **string.StartsWith**             | **64**    | **Ascii**  |  **1.292 ns** | **0.0271 ns** | **0.0254 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Ascii  |  1.510 ns | 0.0192 ns | 0.0170 ns |  1.17 |    0.03 |         - |          NA |
| U8String.StartsWith           | 64    | Ascii  |  1.516 ns | 0.0153 ns | 0.0136 ns |  1.17 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Ascii  |  2.175 ns | 0.0190 ns | 0.0177 ns |  1.68 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Ascii  |  8.853 ns | 0.1137 ns | 0.1063 ns |  6.86 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Ascii  |  7.257 ns | 0.0556 ns | 0.0520 ns |  5.62 |    0.11 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Ascii  |  1.297 ns | 0.0273 ns | 0.0256 ns |  1.00 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  1.187 ns | 0.0195 ns | 0.0182 ns |  0.92 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Ascii  |  1.271 ns | 0.0104 ns | 0.0098 ns |  0.98 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Ascii  |  2.318 ns | 0.0175 ns | 0.0164 ns |  1.80 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Ascii  |  8.871 ns | 0.1029 ns | 0.0963 ns |  6.87 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Ascii  |  7.266 ns | 0.0566 ns | 0.0530 ns |  5.63 |    0.11 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Cjk**    |  **1.285 ns** | **0.0251 ns** | **0.0235 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Cjk    |  1.285 ns | 0.0147 ns | 0.0137 ns |  1.00 |    0.02 |         - |          NA |
| U8String.StartsWith           | 64    | Cjk    |  1.274 ns | 0.0100 ns | 0.0093 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Cjk    |  2.333 ns | 0.0115 ns | 0.0096 ns |  1.82 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Cjk    |  9.886 ns | 0.1003 ns | 0.0938 ns |  7.69 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Cjk    |  7.887 ns | 0.0768 ns | 0.0641 ns |  6.14 |    0.12 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Cjk    |  1.286 ns | 0.0190 ns | 0.0169 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  1.225 ns | 0.0146 ns | 0.0136 ns |  0.95 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Cjk    |  1.272 ns | 0.0087 ns | 0.0081 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Cjk    |  2.383 ns | 0.0263 ns | 0.0220 ns |  1.85 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Cjk    |  9.832 ns | 0.1437 ns | 0.1274 ns |  7.65 |    0.17 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Cjk    |  7.831 ns | 0.0784 ns | 0.0695 ns |  6.09 |    0.12 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Emoji**  |  **1.277 ns** | **0.0280 ns** | **0.0262 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Emoji  |  1.522 ns | 0.0278 ns | 0.0260 ns |  1.19 |    0.03 |         - |          NA |
| U8String.StartsWith           | 64    | Emoji  |  1.512 ns | 0.0183 ns | 0.0171 ns |  1.18 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Emoji  |  2.181 ns | 0.0205 ns | 0.0191 ns |  1.71 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Emoji  | 11.092 ns | 0.1485 ns | 0.1389 ns |  8.69 |    0.20 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Emoji  |  8.052 ns | 0.0873 ns | 0.0816 ns |  6.31 |    0.14 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Emoji  |  1.320 ns | 0.0184 ns | 0.0163 ns |  1.03 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  1.520 ns | 0.0219 ns | 0.0205 ns |  1.19 |    0.03 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Emoji  |  1.511 ns | 0.0116 ns | 0.0103 ns |  1.18 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Emoji  |  2.163 ns | 0.0096 ns | 0.0080 ns |  1.69 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Emoji  | 11.114 ns | 0.1336 ns | 0.1250 ns |  8.71 |    0.20 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Emoji  |  8.061 ns | 0.1056 ns | 0.0987 ns |  6.31 |    0.15 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **64**    | **Mixed**  |  **1.281 ns** | **0.0234 ns** | **0.0219 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 64    | Mixed  |  1.194 ns | 0.0089 ns | 0.0079 ns |  0.93 |    0.02 |         - |          NA |
| U8String.StartsWith           | 64    | Mixed  |  1.296 ns | 0.0212 ns | 0.0188 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 64    | Mixed  |  2.344 ns | 0.0180 ns | 0.0151 ns |  1.83 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 64    | Mixed  |  8.831 ns | 0.1060 ns | 0.0992 ns |  6.90 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 64    | Mixed  |  7.248 ns | 0.0807 ns | 0.0755 ns |  5.66 |    0.11 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 64    | Mixed  |  1.299 ns | 0.0302 ns | 0.0283 ns |  1.01 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  1.210 ns | 0.0100 ns | 0.0089 ns |  0.95 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 64    | Mixed  |  1.296 ns | 0.0114 ns | 0.0107 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 64    | Mixed  |  2.220 ns | 0.0200 ns | 0.0187 ns |  1.73 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 64    | Mixed  |  8.863 ns | 0.0785 ns | 0.0735 ns |  6.92 |    0.13 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 64    | Mixed  |  7.239 ns | 0.0567 ns | 0.0502 ns |  5.65 |    0.10 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Ascii**  |  **1.275 ns** | **0.0253 ns** | **0.0237 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Ascii  |  1.511 ns | 0.0280 ns | 0.0262 ns |  1.19 |    0.03 |         - |          NA |
| U8String.StartsWith           | 4096  | Ascii  |  1.520 ns | 0.0180 ns | 0.0168 ns |  1.19 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Ascii  |  2.165 ns | 0.0167 ns | 0.0148 ns |  1.70 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Ascii  |  8.884 ns | 0.0883 ns | 0.0826 ns |  6.97 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Ascii  |  7.312 ns | 0.0615 ns | 0.0576 ns |  5.74 |    0.11 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Ascii  |  1.303 ns | 0.0220 ns | 0.0206 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  1.170 ns | 0.0153 ns | 0.0143 ns |  0.92 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Ascii  |  1.277 ns | 0.0091 ns | 0.0085 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Ascii  |  2.338 ns | 0.0208 ns | 0.0194 ns |  1.83 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Ascii  |  8.850 ns | 0.0757 ns | 0.0708 ns |  6.94 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Ascii  |  7.259 ns | 0.0731 ns | 0.0683 ns |  5.69 |    0.11 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Cjk**    |  **1.289 ns** | **0.0259 ns** | **0.0242 ns** |  **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Cjk    |  1.268 ns | 0.0113 ns | 0.0106 ns |  0.98 |    0.02 |         - |          NA |
| U8String.StartsWith           | 4096  | Cjk    |  1.274 ns | 0.0055 ns | 0.0046 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Cjk    |  2.392 ns | 0.0218 ns | 0.0204 ns |  1.86 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Cjk    |  9.953 ns | 0.1185 ns | 0.1109 ns |  7.72 |    0.16 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Cjk    |  7.991 ns | 0.1743 ns | 0.1630 ns |  6.20 |    0.17 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Cjk    |  1.304 ns | 0.0195 ns | 0.0173 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  1.173 ns | 0.0175 ns | 0.0164 ns |  0.91 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Cjk    |  1.275 ns | 0.0093 ns | 0.0087 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Cjk    |  2.314 ns | 0.0230 ns | 0.0204 ns |  1.80 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Cjk    |  9.931 ns | 0.1375 ns | 0.1287 ns |  7.71 |    0.17 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Cjk    |  7.977 ns | 0.1420 ns | 0.1328 ns |  6.19 |    0.15 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Emoji**  |  **1.266 ns** | **0.0201 ns** | **0.0178 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Emoji  |  1.511 ns | 0.0257 ns | 0.0240 ns |  1.19 |    0.02 |         - |          NA |
| U8String.StartsWith           | 4096  | Emoji  |  1.519 ns | 0.0175 ns | 0.0164 ns |  1.20 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Emoji  |  2.187 ns | 0.0178 ns | 0.0166 ns |  1.73 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Emoji  | 11.090 ns | 0.1164 ns | 0.1089 ns |  8.76 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Emoji  |  8.052 ns | 0.0865 ns | 0.0767 ns |  6.36 |    0.10 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Emoji  |  1.304 ns | 0.0242 ns | 0.0226 ns |  1.03 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  1.527 ns | 0.0187 ns | 0.0166 ns |  1.21 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Emoji  |  1.522 ns | 0.0138 ns | 0.0129 ns |  1.20 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Emoji  |  2.215 ns | 0.0091 ns | 0.0076 ns |  1.75 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Emoji  | 11.143 ns | 0.1643 ns | 0.1537 ns |  8.80 |    0.17 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Emoji  |  8.045 ns | 0.0994 ns | 0.0881 ns |  6.36 |    0.11 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **4096**  | **Mixed**  |  **1.283 ns** | **0.0208 ns** | **0.0194 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 4096  | Mixed  |  1.164 ns | 0.0111 ns | 0.0103 ns |  0.91 |    0.02 |         - |          NA |
| U8String.StartsWith           | 4096  | Mixed  |  1.302 ns | 0.0089 ns | 0.0083 ns |  1.02 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 4096  | Mixed  |  2.357 ns | 0.0214 ns | 0.0200 ns |  1.84 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 4096  | Mixed  |  8.955 ns | 0.1166 ns | 0.1090 ns |  6.98 |    0.13 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 4096  | Mixed  |  7.241 ns | 0.0476 ns | 0.0422 ns |  5.64 |    0.09 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 4096  | Mixed  |  1.296 ns | 0.0306 ns | 0.0286 ns |  1.01 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  1.250 ns | 0.0122 ns | 0.0114 ns |  0.97 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 4096  | Mixed  |  1.275 ns | 0.0101 ns | 0.0094 ns |  0.99 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 4096  | Mixed  |  2.372 ns | 0.0271 ns | 0.0253 ns |  1.85 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 4096  | Mixed  |  8.960 ns | 0.1318 ns | 0.1169 ns |  6.98 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 4096  | Mixed  |  7.242 ns | 0.0760 ns | 0.0711 ns |  5.64 |    0.10 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Ascii**  |  **1.280 ns** | **0.0219 ns** | **0.0204 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Ascii  |  1.514 ns | 0.0160 ns | 0.0150 ns |  1.18 |    0.02 |         - |          NA |
| U8String.StartsWith           | 65536 | Ascii  |  1.523 ns | 0.0162 ns | 0.0151 ns |  1.19 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Ascii  |  2.231 ns | 0.0226 ns | 0.0211 ns |  1.74 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Ascii  |  8.927 ns | 0.0726 ns | 0.0679 ns |  6.97 |    0.12 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Ascii  |  7.342 ns | 0.0589 ns | 0.0551 ns |  5.74 |    0.10 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Ascii  |  1.301 ns | 0.0403 ns | 0.0377 ns |  1.02 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  1.199 ns | 0.0102 ns | 0.0096 ns |  0.94 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Ascii  |  1.276 ns | 0.0096 ns | 0.0090 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Ascii  |  2.333 ns | 0.0232 ns | 0.0217 ns |  1.82 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Ascii  |  8.927 ns | 0.1322 ns | 0.1237 ns |  6.97 |    0.14 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Ascii  |  7.280 ns | 0.0742 ns | 0.0694 ns |  5.69 |    0.10 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Cjk**    |  **1.273 ns** | **0.0248 ns** | **0.0232 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Cjk    |  1.148 ns | 0.0091 ns | 0.0086 ns |  0.90 |    0.02 |         - |          NA |
| U8String.StartsWith           | 65536 | Cjk    |  1.274 ns | 0.0096 ns | 0.0089 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Cjk    |  2.354 ns | 0.0169 ns | 0.0150 ns |  1.85 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Cjk    |  9.946 ns | 0.1098 ns | 0.0917 ns |  7.82 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Cjk    |  7.923 ns | 0.1328 ns | 0.1242 ns |  6.23 |    0.14 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Cjk    |  1.302 ns | 0.0325 ns | 0.0304 ns |  1.02 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  1.179 ns | 0.0178 ns | 0.0148 ns |  0.93 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Cjk    |  1.276 ns | 0.0098 ns | 0.0092 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Cjk    |  2.325 ns | 0.0208 ns | 0.0195 ns |  1.83 |    0.04 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Cjk    | 10.004 ns | 0.1072 ns | 0.0950 ns |  7.86 |    0.16 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Cjk    |  7.909 ns | 0.0991 ns | 0.0878 ns |  6.22 |    0.13 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Emoji**  |  **1.297 ns** | **0.0222 ns** | **0.0197 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Emoji  |  1.516 ns | 0.0166 ns | 0.0155 ns |  1.17 |    0.02 |         - |          NA |
| U8String.StartsWith           | 65536 | Emoji  |  1.519 ns | 0.0184 ns | 0.0172 ns |  1.17 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Emoji  |  2.183 ns | 0.0187 ns | 0.0175 ns |  1.68 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Emoji  | 11.224 ns | 0.1600 ns | 0.1419 ns |  8.65 |    0.16 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Emoji  |  8.106 ns | 0.0991 ns | 0.0927 ns |  6.25 |    0.11 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Emoji  |  1.300 ns | 0.0330 ns | 0.0292 ns |  1.00 |    0.03 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  1.534 ns | 0.0187 ns | 0.0175 ns |  1.18 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Emoji  |  1.514 ns | 0.0135 ns | 0.0112 ns |  1.17 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Emoji  |  2.168 ns | 0.0159 ns | 0.0148 ns |  1.67 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Emoji  | 11.150 ns | 0.1267 ns | 0.1186 ns |  8.60 |    0.15 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Emoji  |  8.128 ns | 0.0702 ns | 0.0657 ns |  6.27 |    0.10 |         - |          NA |
|                               |       |        |           |           |           |       |         |           |             |
| **string.StartsWith**             | **65536** | **Mixed**  |  **1.277 ns** | **0.0197 ns** | **0.0184 ns** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.StartsWith UTF-8&#39;       | 65536 | Mixed  |  1.149 ns | 0.0095 ns | 0.0089 ns |  0.90 |    0.01 |         - |          NA |
| U8String.StartsWith           | 65536 | Mixed  |  1.277 ns | 0.0113 ns | 0.0106 ns |  1.00 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8&#39;       | 65536 | Mixed  |  2.362 ns | 0.0229 ns | 0.0191 ns |  1.85 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16&#39;      | 65536 | Mixed  |  8.906 ns | 0.0755 ns | 0.0706 ns |  6.97 |    0.11 |         - |          NA |
| &#39;Text.StartsWith UTF-32&#39;      | 65536 | Mixed  |  7.288 ns | 0.0745 ns | 0.0697 ns |  5.71 |    0.10 |         - |          NA |
| &#39;string.StartsWith miss&#39;      | 65536 | Mixed  |  1.291 ns | 0.0266 ns | 0.0249 ns |  1.01 |    0.02 |         - |          NA |
| &#39;Span.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  1.155 ns | 0.0120 ns | 0.0113 ns |  0.90 |    0.02 |         - |          NA |
| &#39;U8String.StartsWith miss&#39;    | 65536 | Mixed  |  1.328 ns | 0.0113 ns | 0.0105 ns |  1.04 |    0.02 |         - |          NA |
| &#39;Text.StartsWith UTF-8 miss&#39;  | 65536 | Mixed  |  2.423 ns | 0.0243 ns | 0.0227 ns |  1.90 |    0.03 |         - |          NA |
| &#39;Text.StartsWith UTF-16 miss&#39; | 65536 | Mixed  |  8.878 ns | 0.0956 ns | 0.0894 ns |  6.95 |    0.12 |         - |          NA |
| &#39;Text.StartsWith UTF-32 miss&#39; | 65536 | Mixed  |  7.248 ns | 0.0828 ns | 0.0774 ns |  5.68 |    0.10 |         - |          NA |
