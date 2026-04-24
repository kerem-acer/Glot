```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
| Method                 | N     | Locale | Mean      | Error | Ratio | Allocated | Alloc Ratio |
|----------------------- |------ |------- |----------:|------:|------:|----------:|------------:|
| **&#39;new string(source)&#39;**   | **8**     | **Ascii**  | **122.33 μs** |    **NA** |  **1.00** |      **40 B** |        **1.00** |
| Text.From(string)      | 8     | Ascii  | 124.04 μs |    NA |  1.01 |         - |        0.00 |
| Text.FromAscii(string) | 8     | Ascii  | 172.38 μs |    NA |  1.41 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 8     | Ascii  | 219.38 μs |    NA |  1.79 |      40 B |        1.00 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **8**     | **Cjk**    |  **98.62 μs** |    **NA** |  **1.00** |      **40 B** |        **1.00** |
| Text.From(string)      | 8     | Cjk    |  93.29 μs |    NA |  0.95 |         - |        0.00 |
| Text.FromAscii(string) | 8     | Cjk    | 149.38 μs |    NA |  1.51 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 8     | Cjk    | 174.67 μs |    NA |  1.77 |      56 B |        1.40 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **8**     | **Emoji**  | **125.75 μs** |    **NA** |  **1.00** |      **40 B** |        **1.00** |
| Text.From(string)      | 8     | Emoji  | 107.38 μs |    NA |  0.85 |         - |        0.00 |
| Text.FromAscii(string) | 8     | Emoji  | 118.75 μs |    NA |  0.94 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 8     | Emoji  | 207.71 μs |    NA |  1.65 |      48 B |        1.20 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **8**     | **Mixed**  | **101.33 μs** |    **NA** |  **1.00** |      **40 B** |        **1.00** |
| Text.From(string)      | 8     | Mixed  | 406.50 μs |    NA |  4.01 |         - |        0.00 |
| Text.FromAscii(string) | 8     | Mixed  | 150.25 μs |    NA |  1.48 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 8     | Mixed  | 188.25 μs |    NA |  1.86 |      40 B |        1.00 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **256**   | **Ascii**  | **107.62 μs** |    **NA** |  **1.00** |     **536 B** |        **1.00** |
| Text.From(string)      | 256   | Ascii  | 113.50 μs |    NA |  1.05 |         - |        0.00 |
| Text.FromAscii(string) | 256   | Ascii  | 117.96 μs |    NA |  1.10 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256   | Ascii  | 153.67 μs |    NA |  1.43 |     288 B |        0.54 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **256**   | **Cjk**    | **115.38 μs** |    **NA** |  **1.00** |     **536 B** |        **1.00** |
| Text.From(string)      | 256   | Cjk    | 101.62 μs |    NA |  0.88 |         - |        0.00 |
| Text.FromAscii(string) | 256   | Cjk    | 159.21 μs |    NA |  1.38 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256   | Cjk    | 218.00 μs |    NA |  1.89 |     800 B |        1.49 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **256**   | **Emoji**  | **119.67 μs** |    **NA** |  **1.00** |     **536 B** |        **1.00** |
| Text.From(string)      | 256   | Emoji  |  96.50 μs |    NA |  0.81 |         - |        0.00 |
| Text.FromAscii(string) | 256   | Emoji  | 122.21 μs |    NA |  1.02 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256   | Emoji  | 193.21 μs |    NA |  1.61 |     544 B |        1.01 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **256**   | **Mixed**  | **115.71 μs** |    **NA** |  **1.00** |     **536 B** |        **1.00** |
| Text.From(string)      | 256   | Mixed  |  92.79 μs |    NA |  0.80 |         - |        0.00 |
| Text.FromAscii(string) | 256   | Mixed  | 992.83 μs |    NA |  8.58 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 256   | Mixed  | 179.17 μs |    NA |  1.55 |     360 B |        0.67 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **65536** | **Ascii**  | **102.83 μs** |    **NA** |  **1.00** |  **131096 B** |        **1.00** |
| Text.From(string)      | 65536 | Ascii  | 569.58 μs |    NA |  5.54 |         - |        0.00 |
| Text.FromAscii(string) | 65536 | Ascii  | 142.75 μs |    NA |  1.39 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 65536 | Ascii  | 183.38 μs |    NA |  1.78 |   65568 B |        0.50 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **65536** | **Cjk**    |  **97.67 μs** |    **NA** |  **1.00** |  **131096 B** |        **1.00** |
| Text.From(string)      | 65536 | Cjk    | 551.96 μs |    NA |  5.65 |         - |        0.00 |
| Text.FromAscii(string) | 65536 | Cjk    | 139.58 μs |    NA |  1.43 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 65536 | Cjk    | 253.17 μs |    NA |  2.59 |  196640 B |        1.50 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **65536** | **Emoji**  |  **99.29 μs** |    **NA** |  **1.00** |  **131096 B** |        **1.00** |
| Text.From(string)      | 65536 | Emoji  | 580.25 μs |    NA |  5.84 |         - |        0.00 |
| Text.FromAscii(string) | 65536 | Emoji  | 134.33 μs |    NA |  1.35 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 65536 | Emoji  | 262.83 μs |    NA |  2.65 |  131104 B |        1.00 |
|                        |       |        |           |       |       |           |             |
| **&#39;new string(source)&#39;**   | **65536** | **Mixed**  | **103.33 μs** |    **NA** |  **1.00** |  **131096 B** |        **1.00** |
| Text.From(string)      | 65536 | Mixed  | 609.75 μs |    NA |  5.90 |         - |        0.00 |
| Text.FromAscii(string) | 65536 | Mixed  | 144.08 μs |    NA |  1.39 |         - |        0.00 |
| &#39;new U8String(string)&#39; | 65536 | Mixed  | 246.75 μs |    NA |  2.39 |   85512 B |        0.65 |
