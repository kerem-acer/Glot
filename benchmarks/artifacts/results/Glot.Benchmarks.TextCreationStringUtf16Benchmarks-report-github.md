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
| **&#39;new string(source)&#39;**                 | **8**     | **Ascii**  |       **6.453 ns** |     **0.0548 ns** |     **0.0486 ns** |  **1.00** |    **0.01** | **0.0047** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.From(string)                    | 8     | Ascii  |       4.009 ns |     0.0191 ns |     0.0170 ns |  0.62 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 8     | Ascii  |       1.983 ns |     0.0247 ns |     0.0231 ns |  0.31 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 8     | Ascii  |       1.987 ns |     0.0273 ns |     0.0255 ns |  0.31 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 8     | Ascii  |       7.837 ns |     0.0661 ns |     0.0618 ns |  1.21 |    0.01 | 0.0047 |      - |      - |      40 B |        1.00 |
| OwnedText.FromChars(span)            | 8     | Ascii  |      13.540 ns |     0.1476 ns |     0.1309 ns |  2.10 |    0.02 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 8     | Ascii  |      11.617 ns |     0.1181 ns |     0.1047 ns |  1.80 |    0.02 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **8**     | **Cjk**    |       **6.519 ns** |     **0.1050 ns** |     **0.0931 ns** |  **1.00** |    **0.02** | **0.0048** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.From(string)                    | 8     | Cjk    |       4.008 ns |     0.0202 ns |     0.0179 ns |  0.61 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 8     | Cjk    |       1.982 ns |     0.0218 ns |     0.0204 ns |  0.30 |    0.01 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 8     | Cjk    |       1.982 ns |     0.0266 ns |     0.0249 ns |  0.30 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 8     | Cjk    |      16.268 ns |     0.2821 ns |     0.2638 ns |  2.50 |    0.05 | 0.0066 |      - |      - |      56 B |        1.40 |
| OwnedText.FromChars(span)            | 8     | Cjk    |      13.599 ns |     0.1263 ns |     0.1181 ns |  2.09 |    0.03 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 8     | Cjk    |      11.589 ns |     0.1540 ns |     0.1440 ns |  1.78 |    0.03 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **8**     | **Emoji**  |       **6.465 ns** |     **0.0777 ns** |     **0.0727 ns** |  **1.00** |    **0.02** | **0.0048** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.From(string)                    | 8     | Emoji  |       3.984 ns |     0.0146 ns |     0.0122 ns |  0.62 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 8     | Emoji  |       1.971 ns |     0.0141 ns |     0.0118 ns |  0.30 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 8     | Emoji  |       1.977 ns |     0.0300 ns |     0.0266 ns |  0.31 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 8     | Emoji  |      23.105 ns |     0.5661 ns |     0.5019 ns |  3.57 |    0.08 | 0.0057 |      - |      - |      48 B |        1.20 |
| OwnedText.FromChars(span)            | 8     | Emoji  |      13.585 ns |     0.1675 ns |     0.1566 ns |  2.10 |    0.03 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 8     | Emoji  |      11.538 ns |     0.1586 ns |     0.1483 ns |  1.79 |    0.03 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **8**     | **Mixed**  |       **6.475 ns** |     **0.0577 ns** |     **0.0482 ns** |  **1.00** |    **0.01** | **0.0048** |      **-** |      **-** |      **40 B** |        **1.00** |
| Text.From(string)                    | 8     | Mixed  |       4.014 ns |     0.0389 ns |     0.0364 ns |  0.62 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 8     | Mixed  |       1.969 ns |     0.0141 ns |     0.0125 ns |  0.30 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 8     | Mixed  |       1.969 ns |     0.0179 ns |     0.0159 ns |  0.30 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 8     | Mixed  |       7.804 ns |     0.0385 ns |     0.0322 ns |  1.21 |    0.01 | 0.0048 |      - |      - |      40 B |        1.00 |
| OwnedText.FromChars(span)            | 8     | Mixed  |      13.559 ns |     0.0927 ns |     0.0821 ns |  2.09 |    0.02 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 8     | Mixed  |      11.538 ns |     0.1788 ns |     0.1672 ns |  1.78 |    0.03 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **256**   | **Ascii**  |      **24.134 ns** |     **0.3515 ns** |     **0.3116 ns** |  **1.00** |    **0.02** | **0.0641** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)                    | 256   | Ascii  |      21.309 ns |     0.1255 ns |     0.1174 ns |  0.88 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 256   | Ascii  |       1.976 ns |     0.0255 ns |     0.0239 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 256   | Ascii  |       1.968 ns |     0.0198 ns |     0.0176 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 256   | Ascii  |      24.930 ns |     0.4898 ns |     0.4342 ns |  1.03 |    0.02 | 0.0343 |      - |      - |     288 B |        0.54 |
| OwnedText.FromChars(span)            | 256   | Ascii  |      36.064 ns |     0.3380 ns |     0.3162 ns |  1.49 |    0.02 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 256   | Ascii  |      17.030 ns |     0.1132 ns |     0.1003 ns |  0.71 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **256**   | **Cjk**    |      **24.829 ns** |     **0.7763 ns** |     **0.7261 ns** |  **1.00** |    **0.04** | **0.0639** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)                    | 256   | Cjk    |      21.375 ns |     0.2093 ns |     0.1958 ns |  0.86 |    0.03 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 256   | Cjk    |       1.979 ns |     0.0371 ns |     0.0347 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 256   | Cjk    |       1.986 ns |     0.0277 ns |     0.0259 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 256   | Cjk    |     246.512 ns |     5.2689 ns |     4.9286 ns |  9.94 |    0.34 | 0.0954 |      - |      - |     800 B |        1.49 |
| OwnedText.FromChars(span)            | 256   | Cjk    |      35.499 ns |     0.3924 ns |     0.3671 ns |  1.43 |    0.04 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 256   | Cjk    |      17.057 ns |     0.1933 ns |     0.1808 ns |  0.69 |    0.02 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **256**   | **Emoji**  |      **24.077 ns** |     **0.2009 ns** |     **0.1678 ns** |  **1.00** |    **0.01** | **0.0640** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)                    | 256   | Emoji  |      21.368 ns |     0.1626 ns |     0.1442 ns |  0.89 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 256   | Emoji  |       1.982 ns |     0.0359 ns |     0.0336 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 256   | Emoji  |       1.979 ns |     0.0310 ns |     0.0275 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 256   | Emoji  |     450.746 ns |    11.6274 ns |    10.8763 ns | 18.72 |    0.46 | 0.0640 |      - |      - |     544 B |        1.01 |
| OwnedText.FromChars(span)            | 256   | Emoji  |      35.407 ns |     0.3874 ns |     0.3434 ns |  1.47 |    0.02 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 256   | Emoji  |      17.413 ns |     0.2140 ns |     0.2001 ns |  0.72 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **256**   | **Mixed**  |      **24.171 ns** |     **0.3907 ns** |     **0.3655 ns** |  **1.00** |    **0.02** | **0.0641** |      **-** |      **-** |     **536 B** |        **1.00** |
| Text.From(string)                    | 256   | Mixed  |      21.301 ns |     0.1049 ns |     0.0930 ns |  0.88 |    0.01 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 256   | Mixed  |       1.976 ns |     0.0180 ns |     0.0159 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 256   | Mixed  |       1.970 ns |     0.0267 ns |     0.0236 ns |  0.08 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 256   | Mixed  |     210.875 ns |     2.8751 ns |     2.4009 ns |  8.73 |    0.16 | 0.0421 |      - |      - |     360 B |        0.67 |
| OwnedText.FromChars(span)            | 256   | Mixed  |      35.282 ns |     0.4262 ns |     0.3986 ns |  1.46 |    0.03 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 256   | Mixed  |      16.749 ns |     0.1350 ns |     0.1263 ns |  0.69 |    0.01 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **65536** | **Ascii**  |  **42,733.253 ns** |   **886.6805 ns** |   **829.4015 ns** | **1.000** |    **0.03** | **0.4433** | **0.4433** | **0.4433** |  **131101 B** |        **1.00** |
| Text.From(string)                    | 65536 | Ascii  |   4,723.012 ns |    45.0716 ns |    42.1600 ns | 0.111 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 65536 | Ascii  |       1.977 ns |     0.0373 ns |     0.0349 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 65536 | Ascii  |       1.980 ns |     0.0321 ns |     0.0300 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 65536 | Ascii  |   3,689.384 ns |    45.8071 ns |    40.6068 ns | 0.086 |    0.00 | 7.7880 | 1.5429 |      - |   65568 B |        0.50 |
| OwnedText.FromChars(span)            | 65536 | Ascii  |   5,946.959 ns |    33.0022 ns |    27.5584 ns | 0.139 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 65536 | Ascii  |   1,319.093 ns |    13.7722 ns |    10.7524 ns | 0.031 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **65536** | **Cjk**    |  **42,452.096 ns** |   **853.8646 ns** |   **798.7055 ns** | **1.000** |    **0.03** | **0.4386** | **0.4386** | **0.4386** |  **131101 B** |        **1.00** |
| Text.From(string)                    | 65536 | Cjk    |   4,717.794 ns |    23.8113 ns |    19.8835 ns | 0.111 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 65536 | Cjk    |       1.993 ns |     0.0394 ns |     0.0349 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 65536 | Cjk    |       1.972 ns |     0.0173 ns |     0.0135 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 65536 | Cjk    |  99,229.894 ns | 2,745.9440 ns | 2,292.9876 ns | 2.338 |    0.07 | 0.4371 | 0.4371 | 0.4371 |  196647 B |        1.50 |
| OwnedText.FromChars(span)            | 65536 | Cjk    |   5,996.565 ns |    66.9293 ns |    62.6057 ns | 0.141 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 65536 | Cjk    |   1,353.178 ns |    13.3875 ns |    12.5226 ns | 0.032 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **65536** | **Emoji**  |  **51,477.514 ns** |   **829.5772 ns** |   **735.3986 ns** | **1.000** |    **0.02** | **0.5631** | **0.5631** | **0.5631** |  **131103 B** |        **1.00** |
| Text.From(string)                    | 65536 | Emoji  |   4,763.803 ns |    35.6288 ns |    33.3272 ns | 0.093 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 65536 | Emoji  |       2.021 ns |     0.0551 ns |     0.0515 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 65536 | Emoji  |       1.986 ns |     0.0335 ns |     0.0313 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 65536 | Emoji  | 117,187.680 ns | 1,604.8445 ns | 1,501.1726 ns | 2.277 |    0.04 |      - |      - |      - |  131110 B |        1.00 |
| OwnedText.FromChars(span)            | 65536 | Emoji  |   5,989.127 ns |    60.2404 ns |    56.3489 ns | 0.116 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 65536 | Emoji  |   1,344.301 ns |    26.5421 ns |    23.5289 ns | 0.026 |    0.00 |      - |      - |      - |         - |        0.00 |
|                                      |       |        |                |               |               |       |         |        |        |        |           |             |
| **&#39;new string(source)&#39;**                 | **65536** | **Mixed**  |  **45,109.162 ns** |   **985.2837 ns** |   **921.6350 ns** | **1.000** |    **0.03** | **0.4771** | **0.4771** | **0.4771** |  **131102 B** |        **1.00** |
| Text.From(string)                    | 65536 | Mixed  |   4,742.366 ns |    48.1969 ns |    45.0835 ns | 0.105 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;Text.From(string) no-count&#39;         | 65536 | Mixed  |       1.993 ns |     0.0236 ns |     0.0221 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| Text.FromAscii(string)               | 65536 | Mixed  |       1.989 ns |     0.0168 ns |     0.0131 ns | 0.000 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;new U8String(string)&#39;               | 65536 | Mixed  |  80,124.930 ns | 1,854.7270 ns | 1,734.9128 ns | 1.777 |    0.05 | 0.3472 | 0.3472 | 0.3472 |   85517 B |        0.65 |
| OwnedText.FromChars(span)            | 65536 | Mixed  |   5,991.162 ns |    60.6873 ns |    56.7669 ns | 0.133 |    0.00 |      - |      - |      - |         - |        0.00 |
| &#39;OwnedText.FromChars(span) no-count&#39; | 65536 | Mixed  |   1,417.443 ns |    33.6928 ns |    31.5163 ns | 0.031 |    0.00 |      - |      - |      - |         - |        0.00 |
