```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host]   : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Categories     | N    | Locale | Mean      | Error      | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |--------------- |----- |------- |----------:|-----------:|----------:|------:|--------:|----------:|------------:|
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096** | **Ascii**  |  **1.668 μs** |  **0.9186 μs** | **0.0504 μs** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096 | Ascii  |  2.328 μs |  0.3310 μs | 0.0181 μs |  1.40 |    0.04 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096 | Ascii  |  1.263 μs |  0.8303 μs | 0.0455 μs |  0.76 |    0.03 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096 | Ascii  | 14.978 μs |  7.0592 μs | 0.3869 μs |  8.98 |    0.31 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096** | **Cjk**    |  **1.716 μs** |  **0.9271 μs** | **0.0508 μs** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096 | Cjk    |  4.223 μs |  1.1632 μs | 0.0638 μs |  2.46 |    0.07 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096 | Cjk    |  1.502 μs |  0.3591 μs | 0.0197 μs |  0.88 |    0.02 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096 | Cjk    | 16.334 μs |  2.4389 μs | 0.1337 μs |  9.53 |    0.25 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096** | **Emoji**  |  **5.733 μs** |  **1.7363 μs** | **0.0952 μs** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096 | Emoji  |  2.975 μs |  0.2555 μs | 0.0140 μs |  0.52 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096 | Emoji  |  1.165 μs |  0.6624 μs | 0.0363 μs |  0.20 |    0.01 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096 | Emoji  | 10.977 μs |  2.3821 μs | 0.1306 μs |  1.92 |    0.03 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **string.EnumerateRunes**       | **EnumerateRunes** | **4096** | **Mixed**  |  **6.453 μs** |  **1.4508 μs** | **0.0795 μs** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.DecodeFromUtf8 count&#39; | EnumerateRunes | 4096 | Mixed  |  3.046 μs |  1.7822 μs | 0.0977 μs |  0.47 |    0.01 |         - |          NA |
| U8String.Runes              | EnumerateRunes | 4096 | Mixed  |  1.224 μs |  0.2937 μs | 0.0161 μs |  0.19 |    0.00 |         - |          NA |
| &#39;Text.EnumerateRunes UTF-8&#39; | EnumerateRunes | 4096 | Mixed  | 14.239 μs | 10.3815 μs | 0.5690 μs |  2.21 |    0.08 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096** | **Ascii**  |  **2.071 μs** |  **1.1704 μs** | **0.0642 μs** |  **1.00** |    **0.04** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096 | Ascii  |  2.343 μs |  2.2559 μs | 0.1237 μs |  1.13 |    0.06 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096 | Ascii  | 34.617 μs | 10.1106 μs | 0.5542 μs | 16.73 |    0.51 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096** | **Cjk**    |  **4.809 μs** |  **1.4331 μs** | **0.0786 μs** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096 | Cjk    |  5.324 μs |  1.5313 μs | 0.0839 μs |  1.11 |    0.02 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096 | Cjk    | 33.064 μs |  7.6306 μs | 0.4183 μs |  6.88 |    0.12 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096** | **Emoji**  |  **2.518 μs** |  **0.9362 μs** | **0.0513 μs** |  **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096 | Emoji  |  2.939 μs |  2.3253 μs | 0.1275 μs |  1.17 |    0.05 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096 | Emoji  | 24.168 μs | 37.8619 μs | 2.0753 μs |  9.60 |    0.73 |         - |          NA |
|                             |                |      |        |           |            |           |       |         |           |             |
| **&#39;Span.IndexOf UTF-8&#39;**        | **Split**          | **4096** | **Mixed**  |  **2.098 μs** |  **1.9027 μs** | **0.1043 μs** |  **1.00** |    **0.06** |         **-** |          **NA** |
| &#39;U8String.Split count&#39;      | Split          | 4096 | Mixed  |  2.143 μs |  1.7977 μs | 0.0985 μs |  1.02 |    0.06 |         - |          NA |
| &#39;Text.Split UTF-8 count&#39;    | Split          | 4096 | Mixed  | 30.581 μs |  6.1097 μs | 0.3349 μs | 14.60 |    0.64 |         - |          NA |
