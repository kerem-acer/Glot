```

BenchmarkDotNet v0.14.0, macOS 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.201
  [Host]   : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 10.0.5 (10.0.526.15411), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Categories  | N    | Locale | Mean        | Error       | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------------- |------------ |----- |------- |------------:|------------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **&#39;Deserialize to string&#39;**     | **Deserialize** | **256**  | **Ascii**  |    **243.5 ns** |    **61.46 ns** |   **3.37 ns** |  **1.00** |    **0.02** | **0.1173** |      **-** |     **984 B** |        **1.00** |
| &#39;Deserialize to Text&#39;       | Deserialize | 256  | Ascii  |  1,003.6 ns |    38.67 ns |   2.12 ns |  4.12 |    0.05 | 0.0877 |      - |     744 B |        0.76 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Deserialize to string&#39;**     | **Deserialize** | **256**  | **Mixed**  |  **1,712.2 ns** |    **64.75 ns** |   **3.55 ns** |  **1.00** |    **0.00** | **0.1163** |      **-** |     **984 B** |        **1.00** |
| &#39;Deserialize to Text&#39;       | Deserialize | 256  | Mixed  |  2,304.4 ns |    79.64 ns |   4.37 ns |  1.35 |    0.00 | 0.1068 |      - |     904 B |        0.92 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Deserialize to string&#39;**     | **Deserialize** | **4096** | **Ascii**  |    **999.5 ns** |    **98.25 ns** |   **5.39 ns** |  **1.00** |    **0.01** | **1.4362** | **0.0610** |   **12024 B** |        **1.00** |
| &#39;Deserialize to Text&#39;       | Deserialize | 4096 | Ascii  | 12,682.4 ns | 1,362.59 ns |  74.69 ns | 12.69 |    0.09 | 0.7477 | 0.0153 |    6264 B |        0.52 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Deserialize to string&#39;**     | **Deserialize** | **4096** | **Mixed**  | **24,228.7 ns** |   **884.08 ns** |  **48.46 ns** |  **1.00** |    **0.00** | **1.4343** | **0.0610** |   **12024 B** |        **1.00** |
| &#39;Deserialize to Text&#39;       | Deserialize | 4096 | Mixed  | 36,072.0 ns | 3,257.25 ns | 178.54 ns |  1.49 |    0.01 | 0.9766 |      - |    8680 B |        0.72 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Round trip: string&#39;**        | **RoundTrip**   | **256**  | **Ascii**  |    **372.5 ns** |    **28.93 ns** |   **1.59 ns** |  **1.00** |    **0.01** | **0.1702** | **0.0005** |    **1424 B** |        **1.00** |
| &#39;Round trip: Text&#39;          | RoundTrip   | 256  | Ascii  |  1,126.7 ns |   273.31 ns |  14.98 ns |  3.02 |    0.04 | 0.1411 |      - |    1184 B |        0.83 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Round trip: string&#39;**        | **RoundTrip**   | **256**  | **Mixed**  |  **2,624.1 ns** |   **202.14 ns** |  **11.08 ns** |  **1.00** |    **0.01** | **0.2289** |      **-** |    **2024 B** |        **1.00** |
| &#39;Round trip: Text&#39;          | RoundTrip   | 256  | Mixed  |  3,470.2 ns |   172.08 ns |   9.43 ns |  1.32 |    0.01 | 0.2289 |      - |    1944 B |        0.96 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Round trip: string&#39;**        | **RoundTrip**   | **4096** | **Ascii**  |  **1,657.3 ns** |   **103.39 ns** |   **5.67 ns** |  **1.00** |    **0.00** | **2.1477** | **0.0877** |   **17984 B** |        **1.00** |
| &#39;Round trip: Text&#39;          | RoundTrip   | 4096 | Ascii  | 13,255.8 ns | 1,187.08 ns |  65.07 ns |  8.00 |    0.04 | 1.4496 | 0.0305 |   12224 B |        0.68 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Round trip: string&#39;**        | **RoundTrip**   | **4096** | **Mixed**  | **35,616.1 ns** | **3,552.03 ns** | **194.70 ns** |  **1.00** |    **0.01** | **3.2959** | **0.1221** |   **27624 B** |        **1.00** |
| &#39;Round trip: Text&#39;          | RoundTrip   | 4096 | Mixed  | 49,808.8 ns | 1,934.08 ns | 106.01 ns |  1.40 |    0.01 | 2.8687 | 0.0610 |   24280 B |        0.88 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Serialize string to bytes&#39;** | **Serialize**   | **256**  | **Ascii**  |    **126.5 ns** |    **15.54 ns** |   **0.85 ns** |  **1.00** |    **0.01** | **0.0525** |      **-** |     **440 B** |        **1.00** |
| &#39;Serialize Text to bytes&#39;   | Serialize   | 256  | Ascii  |    106.6 ns |     5.86 ns |   0.32 ns |  0.84 |    0.01 | 0.0526 |      - |     440 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 256  | Ascii  |    103.7 ns |     4.68 ns |   0.26 ns |  0.82 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Serialize string to bytes&#39;** | **Serialize**   | **256**  | **Mixed**  |    **790.2 ns** |    **92.06 ns** |   **5.05 ns** |  **1.00** |    **0.01** | **0.1240** |      **-** |    **1040 B** |        **1.00** |
| &#39;Serialize Text to bytes&#39;   | Serialize   | 256  | Mixed  |    910.3 ns |   108.80 ns |   5.96 ns |  1.15 |    0.01 | 0.1240 |      - |    1040 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 256  | Mixed  |    928.8 ns |    42.23 ns |   2.31 ns |  1.18 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Serialize string to bytes&#39;** | **Serialize**   | **4096** | **Ascii**  |    **679.7 ns** |    **19.67 ns** |   **1.08 ns** |  **1.00** |    **0.00** | **0.7114** |      **-** |    **5960 B** |        **1.00** |
| &#39;Serialize Text to bytes&#39;   | Serialize   | 4096 | Ascii  |    543.8 ns |    47.27 ns |   2.59 ns |  0.80 |    0.00 | 0.7114 |      - |    5960 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 4096 | Ascii  |    543.4 ns |    74.50 ns |   4.08 ns |  0.80 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| **&#39;Serialize string to bytes&#39;** | **Serialize**   | **4096** | **Mixed**  | **10,230.8 ns** |   **344.45 ns** |  **18.88 ns** |  **1.00** |    **0.00** | **1.8616** |      **-** |   **15600 B** |        **1.00** |
| &#39;Serialize Text to bytes&#39;   | Serialize   | 4096 | Mixed  | 13,177.9 ns | 2,878.41 ns | 157.78 ns |  1.29 |    0.01 | 1.8616 |      - |   15600 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 4096 | Mixed  | 12,931.8 ns | 1,204.49 ns |  66.02 ns |  1.26 |    0.01 |      - |      - |         - |        0.00 |
