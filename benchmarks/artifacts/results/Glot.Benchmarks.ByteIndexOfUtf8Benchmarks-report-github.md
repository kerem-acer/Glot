```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M4 Max, 1 CPU, 16 logical and 16 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), Arm64 RyuJIT armv8.0-a

EvaluateOverhead=False  MaxRelativeError=0.1  Toolchain=InProcessEmitToolchain  
IterationTime=150ms  MaxIterationCount=30  

```
| Method                         | N     | Locale | Mean         | Error      | StdDev     | Ratio    | RatioSD | Allocated | Alloc Ratio |
|------------------------------- |------ |------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|------------:|
| **string.IndexOf**                 | **64**    | **Ascii**  |     **3.944 ns** |  **0.0813 ns** |  **0.0761 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Ascii  |     3.328 ns |  0.0067 ns |  0.0056 ns |     0.84 |    0.02 |         - |          NA |
| U8String.IndexOf               | 64    | Ascii  |     3.518 ns |  0.0056 ns |  0.0043 ns |     0.89 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Ascii  |     4.427 ns |  0.0094 ns |  0.0083 ns |     1.12 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Ascii  |    16.880 ns |  0.0693 ns |  0.0614 ns |     4.28 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Ascii  |    17.804 ns |  0.0881 ns |  0.0781 ns |     4.52 |    0.09 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Ascii  |     5.893 ns |  0.0165 ns |  0.0146 ns |     1.49 |    0.03 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Ascii  |     4.090 ns |  0.0117 ns |  0.0098 ns |     1.04 |    0.02 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 64    | Ascii  |     4.373 ns |  0.0460 ns |  0.0359 ns |     1.11 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Ascii  |     4.864 ns |  0.0216 ns |  0.0202 ns |     1.23 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Ascii  |    17.829 ns |  0.1788 ns |  0.1672 ns |     4.52 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Ascii  |    19.378 ns |  0.4031 ns |  0.3770 ns |     4.91 |    0.13 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Cjk**    |     **2.560 ns** |  **0.0050 ns** |  **0.0047 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Cjk    |     2.932 ns |  0.0122 ns |  0.0102 ns |     1.14 |    0.00 |         - |          NA |
| U8String.IndexOf               | 64    | Cjk    |     3.151 ns |  0.0164 ns |  0.0137 ns |     1.23 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Cjk    |     4.287 ns |  0.0281 ns |  0.0249 ns |     1.67 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Cjk    |    36.551 ns |  0.1603 ns |  0.1421 ns |    14.28 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Cjk    |    33.524 ns |  0.1633 ns |  0.1528 ns |    13.09 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Cjk    |     5.899 ns |  0.0179 ns |  0.0150 ns |     2.30 |    0.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Cjk    |     8.996 ns |  0.2450 ns |  0.2172 ns |     3.51 |    0.08 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 64    | Cjk    |     9.681 ns |  0.3746 ns |  0.3504 ns |     3.78 |    0.13 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Cjk    |     9.541 ns |  0.2201 ns |  0.1951 ns |     3.73 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Cjk    |    33.303 ns |  0.4883 ns |  0.4568 ns |    13.01 |    0.17 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Cjk    |    39.280 ns |  0.2607 ns |  0.2438 ns |    15.34 |    0.10 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Emoji**  |     **3.082 ns** |  **0.0064 ns** |  **0.0057 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Emoji  |     2.986 ns |  0.0106 ns |  0.0089 ns |     0.97 |    0.00 |         - |          NA |
| U8String.IndexOf               | 64    | Emoji  |     3.223 ns |  0.0127 ns |  0.0113 ns |     1.05 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Emoji  |     4.388 ns |  0.0172 ns |  0.0161 ns |     1.42 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Emoji  |    28.934 ns |  0.2831 ns |  0.2648 ns |     9.39 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Emoji  |    33.828 ns |  0.1707 ns |  0.1425 ns |    10.97 |    0.05 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Emoji  |     5.896 ns |  0.0308 ns |  0.0257 ns |     1.91 |    0.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Emoji  |     8.080 ns |  0.2707 ns |  0.2400 ns |     2.62 |    0.08 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 64    | Emoji  |     8.351 ns |  0.2803 ns |  0.2622 ns |     2.71 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Emoji  |     8.438 ns |  0.1585 ns |  0.1324 ns |     2.74 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Emoji  |    34.302 ns |  0.3933 ns |  0.3679 ns |    11.13 |    0.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Emoji  |    38.447 ns |  0.3733 ns |  0.3492 ns |    12.47 |    0.11 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **64**    | **Mixed**  |     **3.493 ns** |  **0.0065 ns** |  **0.0061 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 64    | Mixed  |     3.271 ns |  0.0102 ns |  0.0096 ns |     0.94 |    0.00 |         - |          NA |
| U8String.IndexOf               | 64    | Mixed  |     3.474 ns |  0.0092 ns |  0.0082 ns |     0.99 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 64    | Mixed  |     4.489 ns |  0.0110 ns |  0.0102 ns |     1.29 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 64    | Mixed  |    17.241 ns |  0.0860 ns |  0.0805 ns |     4.94 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 64    | Mixed  |    18.183 ns |  0.0886 ns |  0.0785 ns |     5.21 |    0.02 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 64    | Mixed  |     5.956 ns |  0.0233 ns |  0.0218 ns |     1.71 |    0.01 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 64    | Mixed  |     4.620 ns |  0.0140 ns |  0.0131 ns |     1.32 |    0.00 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 64    | Mixed  |     5.148 ns |  0.2392 ns |  0.2120 ns |     1.47 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 64    | Mixed  |     5.373 ns |  0.0114 ns |  0.0107 ns |     1.54 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 64    | Mixed  |    29.118 ns |  0.1690 ns |  0.1498 ns |     8.34 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 64    | Mixed  |    34.600 ns |  0.1794 ns |  0.1678 ns |     9.91 |    0.05 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Ascii**  |     **3.928 ns** |  **0.0759 ns** |  **0.0634 ns** |     **1.00** |    **0.02** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Ascii  |     3.336 ns |  0.0097 ns |  0.0086 ns |     0.85 |    0.01 |         - |          NA |
| U8String.IndexOf               | 4096  | Ascii  |     3.536 ns |  0.0174 ns |  0.0154 ns |     0.90 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Ascii  |     4.449 ns |  0.0158 ns |  0.0148 ns |     1.13 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Ascii  |    16.876 ns |  0.0603 ns |  0.0534 ns |     4.30 |    0.07 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Ascii  |    17.899 ns |  0.1486 ns |  0.1390 ns |     4.56 |    0.08 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Ascii  |   277.848 ns |  1.1921 ns |  1.0568 ns |    70.75 |    1.12 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Ascii  |   141.465 ns |  0.7989 ns |  0.7082 ns |    36.02 |    0.58 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096  | Ascii  |   142.253 ns |  1.6288 ns |  1.4439 ns |    36.22 |    0.66 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Ascii  |   142.718 ns |  0.8046 ns |  0.7132 ns |    36.34 |    0.58 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Ascii  |   155.932 ns |  0.8027 ns |  0.6703 ns |    39.71 |    0.63 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Ascii  |   157.429 ns |  0.6802 ns |  0.6030 ns |    40.09 |    0.63 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Cjk**    |     **2.586 ns** |  **0.0057 ns** |  **0.0051 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Cjk    |     2.938 ns |  0.0089 ns |  0.0079 ns |     1.14 |    0.00 |         - |          NA |
| U8String.IndexOf               | 4096  | Cjk    |     3.138 ns |  0.0178 ns |  0.0149 ns |     1.21 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Cjk    |     4.276 ns |  0.0188 ns |  0.0176 ns |     1.65 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Cjk    |    36.572 ns |  0.1535 ns |  0.1361 ns |    14.14 |    0.06 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Cjk    |    33.482 ns |  0.2116 ns |  0.1876 ns |    12.95 |    0.07 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Cjk    |   271.352 ns |  1.3077 ns |  1.1592 ns |   104.93 |    0.48 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Cjk    |   403.901 ns |  1.8544 ns |  1.7346 ns |   156.18 |    0.71 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096  | Cjk    |   404.645 ns |  1.7162 ns |  1.6054 ns |   156.47 |    0.67 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Cjk    |   404.157 ns |  1.2964 ns |  1.2127 ns |   156.28 |    0.54 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Cjk    |   428.170 ns |  0.9027 ns |  0.8002 ns |   165.57 |    0.43 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Cjk    |   433.717 ns |  1.0325 ns |  0.9658 ns |   167.71 |    0.48 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Emoji**  |     **3.080 ns** |  **0.0080 ns** |  **0.0075 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Emoji  |     2.983 ns |  0.0067 ns |  0.0063 ns |     0.97 |    0.00 |         - |          NA |
| U8String.IndexOf               | 4096  | Emoji  |     3.259 ns |  0.0155 ns |  0.0137 ns |     1.06 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Emoji  |     4.341 ns |  0.0138 ns |  0.0129 ns |     1.41 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Emoji  |    29.343 ns |  0.2491 ns |  0.2330 ns |     9.53 |    0.08 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Emoji  |    33.959 ns |  0.1924 ns |  0.1800 ns |    11.03 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Emoji  |   278.332 ns |  0.5101 ns |  0.4522 ns |    90.36 |    0.26 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Emoji  |   426.055 ns |  3.0102 ns |  2.6684 ns |   138.33 |    0.90 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096  | Emoji  |   426.433 ns |  3.4851 ns |  2.9102 ns |   138.45 |    0.97 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Emoji  |   425.061 ns |  1.5815 ns |  1.4794 ns |   138.00 |    0.57 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Emoji  |   450.726 ns |  2.0609 ns |  1.6090 ns |   146.34 |    0.61 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Emoji  |   456.288 ns |  3.5534 ns |  3.1500 ns |   148.14 |    1.05 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **4096**  | **Mixed**  |     **3.494 ns** |  **0.0062 ns** |  **0.0058 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 4096  | Mixed  |     3.268 ns |  0.0074 ns |  0.0065 ns |     0.94 |    0.00 |         - |          NA |
| U8String.IndexOf               | 4096  | Mixed  |     3.453 ns |  0.0114 ns |  0.0095 ns |     0.99 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 4096  | Mixed  |     4.493 ns |  0.0094 ns |  0.0084 ns |     1.29 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 4096  | Mixed  |    17.223 ns |  0.1201 ns |  0.1123 ns |     4.93 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 4096  | Mixed  |    18.145 ns |  0.1663 ns |  0.1474 ns |     5.19 |    0.04 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 4096  | Mixed  |   275.681 ns |  1.1231 ns |  1.0505 ns |    78.89 |    0.32 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 4096  | Mixed  |   180.981 ns |  1.0829 ns |  1.0129 ns |    51.79 |    0.29 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 4096  | Mixed  |   180.949 ns |  1.0631 ns |  0.9424 ns |    51.78 |    0.27 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 4096  | Mixed  |   182.142 ns |  0.9696 ns |  0.8097 ns |    52.12 |    0.24 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 4096  | Mixed  |   205.636 ns |  0.8246 ns |  0.7310 ns |    58.85 |    0.22 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 4096  | Mixed  |   211.618 ns |  1.2943 ns |  1.1474 ns |    60.56 |    0.33 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Ascii**  |     **4.002 ns** |  **0.1000 ns** |  **0.0936 ns** |     **1.00** |    **0.03** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Ascii  |     3.335 ns |  0.0107 ns |  0.0095 ns |     0.83 |    0.02 |         - |          NA |
| U8String.IndexOf               | 65536 | Ascii  |     3.542 ns |  0.0221 ns |  0.0196 ns |     0.89 |    0.02 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Ascii  |     4.472 ns |  0.0110 ns |  0.0098 ns |     1.12 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Ascii  |    16.838 ns |  0.0838 ns |  0.0784 ns |     4.21 |    0.10 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Ascii  |    17.884 ns |  0.1081 ns |  0.0902 ns |     4.47 |    0.10 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Ascii  | 4,203.376 ns | 10.6936 ns | 10.0028 ns | 1,050.78 |   24.16 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Ascii  | 2,098.149 ns |  7.0095 ns |  6.5567 ns |   524.50 |   12.10 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 65536 | Ascii  | 2,101.024 ns |  9.3198 ns |  8.7178 ns |   525.22 |   12.20 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Ascii  | 2,099.924 ns |  7.3036 ns |  6.8318 ns |   524.95 |   12.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Ascii  | 2,110.891 ns |  9.0819 ns |  8.4952 ns |   527.69 |   12.24 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Ascii  | 2,113.771 ns |  9.8864 ns |  9.2477 ns |   528.41 |   12.29 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Cjk**    |     **2.583 ns** |  **0.0034 ns** |  **0.0028 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Cjk    |     2.934 ns |  0.0068 ns |  0.0060 ns |     1.14 |    0.00 |         - |          NA |
| U8String.IndexOf               | 65536 | Cjk    |     3.118 ns |  0.0065 ns |  0.0058 ns |     1.21 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Cjk    |     4.278 ns |  0.0211 ns |  0.0187 ns |     1.66 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Cjk    |    36.614 ns |  0.0968 ns |  0.0858 ns |    14.17 |    0.04 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Cjk    |    33.406 ns |  0.1579 ns |  0.1477 ns |    12.93 |    0.06 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Cjk    | 4,195.894 ns | 18.4493 ns | 14.4040 ns | 1,624.22 |    5.62 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Cjk    | 6,271.538 ns | 26.0630 ns | 24.3794 ns | 2,427.70 |    9.49 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 65536 | Cjk    | 6,278.704 ns | 24.7351 ns | 21.9270 ns | 2,430.48 |    8.59 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Cjk    | 6,284.043 ns | 25.8534 ns | 24.1833 ns | 2,432.54 |    9.42 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Cjk    | 6,305.054 ns | 22.2765 ns | 20.8375 ns | 2,440.68 |    8.22 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Cjk    | 6,324.018 ns | 14.9426 ns | 13.9773 ns | 2,448.02 |    5.84 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Emoji**  |     **3.078 ns** |  **0.0066 ns** |  **0.0059 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Emoji  |     2.982 ns |  0.0079 ns |  0.0070 ns |     0.97 |    0.00 |         - |          NA |
| U8String.IndexOf               | 65536 | Emoji  |     3.209 ns |  0.0169 ns |  0.0150 ns |     1.04 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Emoji  |     4.340 ns |  0.0206 ns |  0.0193 ns |     1.41 |    0.01 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Emoji  |    29.147 ns |  0.2886 ns |  0.2699 ns |     9.47 |    0.09 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Emoji  |    33.804 ns |  0.2255 ns |  0.1999 ns |    10.98 |    0.07 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Emoji  | 4,189.702 ns |  9.6548 ns |  8.5587 ns | 1,361.23 |    3.67 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Emoji  | 6,688.553 ns | 35.5252 ns | 27.7357 ns | 2,173.10 |    9.53 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 65536 | Emoji  | 6,715.887 ns | 46.1048 ns | 40.8707 ns | 2,181.98 |   13.44 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Emoji  | 6,681.177 ns | 37.9478 ns | 31.6881 ns | 2,170.71 |   10.69 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Emoji  | 6,743.416 ns | 30.4857 ns | 28.5163 ns | 2,190.93 |    9.83 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Emoji  | 6,727.697 ns | 47.0834 ns | 39.3167 ns | 2,185.82 |   12.95 |         - |          NA |
|                                |       |        |              |            |            |          |         |           |             |
| **string.IndexOf**                 | **65536** | **Mixed**  |     **3.496 ns** |  **0.0083 ns** |  **0.0073 ns** |     **1.00** |    **0.00** |         **-** |          **NA** |
| &#39;Span.IndexOf UTF-8&#39;           | 65536 | Mixed  |     3.272 ns |  0.0099 ns |  0.0087 ns |     0.94 |    0.00 |         - |          NA |
| U8String.IndexOf               | 65536 | Mixed  |     3.448 ns |  0.0098 ns |  0.0087 ns |     0.99 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8&#39;       | 65536 | Mixed  |     4.555 ns |  0.0118 ns |  0.0110 ns |     1.30 |    0.00 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16&#39;      | 65536 | Mixed  |    17.224 ns |  0.0922 ns |  0.0863 ns |     4.93 |    0.03 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32&#39;      | 65536 | Mixed  |    18.133 ns |  0.0692 ns |  0.0614 ns |     5.19 |    0.02 |         - |          NA |
| &#39;string.IndexOf miss&#39;          | 65536 | Mixed  | 4,197.685 ns | 13.9767 ns | 13.0738 ns | 1,200.75 |    4.37 |         - |          NA |
| &#39;Span.IndexOf UTF-8 miss&#39;      | 65536 | Mixed  | 2,735.190 ns | 13.6814 ns | 12.7976 ns |   782.40 |    3.89 |         - |          NA |
| &#39;U8String.IndexOf miss&#39;        | 65536 | Mixed  | 2,732.006 ns | 11.2405 ns | 10.5144 ns |   781.49 |    3.32 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-8 miss&#39;  | 65536 | Mixed  | 2,729.132 ns | 10.9773 ns |  9.7311 ns |   780.67 |    3.12 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-16 miss&#39; | 65536 | Mixed  | 2,757.815 ns | 13.6229 ns | 12.7429 ns |   788.87 |    3.88 |         - |          NA |
| &#39;Text.ByteIndexOf UTF-32 miss&#39; | 65536 | Mixed  | 2,761.253 ns |  8.5337 ns |  7.9824 ns |   789.86 |    2.73 |         - |          NA |
