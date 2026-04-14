# Glot Performance Report

**Date:** 2026-04-13 14:19 UTC
**Runtime:** .NET 10.0.201
**OS:** Darwin arm64
**Job:** ShortRun (3 iterations)
**Params:** N={256,4096}, Locale={Ascii,Mixed}, PartSize={8,64}, Parts={4,16}

## TextCreationBenchmarks

| Method                   | N    | Locale | Mean        | Error        | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------------- |----- |------- |------------:|-------------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| Encoding.GetString       | 256  | Ascii  |    28.68 ns |     3.222 ns |   0.177 ns |  1.00 |    0.01 | 0.0641 |      - |     536 B |        1.00 |
| 'Text.FromUtf8(byte[])'  | 256  | Ascii  |   481.85 ns |   192.696 ns |  10.562 ns | 16.80 |    0.33 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 256  | Ascii  |   485.17 ns |    54.165 ns |   2.969 ns | 16.92 |    0.13 | 0.0334 |      - |     280 B |        0.52 |
| 'new U8String(span)'     | 256  | Ascii  |    17.29 ns |     2.627 ns |   0.144 ns |  0.60 |    0.01 | 0.0344 | 0.0000 |     288 B |        0.54 |
| OwnedText.FromUtf8(span) | 256  | Ascii  |    19.38 ns |     1.536 ns |   0.084 ns |  0.68 |    0.00 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| Encoding.GetString       | 256  | Mixed  |   252.38 ns |    59.648 ns |   3.269 ns |  1.00 |    0.02 | 0.0639 |      - |     536 B |        1.00 |
| 'Text.FromUtf8(byte[])'  | 256  | Mixed  |   601.93 ns |    18.347 ns |   1.006 ns |  2.39 |    0.03 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 256  | Mixed  |   589.90 ns |   169.147 ns |   9.272 ns |  2.34 |    0.04 | 0.0448 |      - |     376 B |        0.70 |
| 'new U8String(span)'     | 256  | Mixed  |   164.02 ns |    25.169 ns |   1.380 ns |  0.65 |    0.01 | 0.0458 |      - |     384 B |        0.72 |
| OwnedText.FromUtf8(span) | 256  | Mixed  |    23.39 ns |     2.251 ns |   0.123 ns |  0.09 |    0.00 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| Encoding.GetString       | 4096 | Ascii  |   378.10 ns |    61.212 ns |   3.355 ns |  1.00 |    0.01 | 0.9813 |      - |    8216 B |        1.00 |
| 'Text.FromUtf8(byte[])'  | 4096 | Ascii  | 7,776.99 ns | 2,971.372 ns | 162.871 ns | 20.57 |    0.40 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 4096 | Ascii  | 7,737.00 ns |   675.958 ns |  37.052 ns | 20.46 |    0.18 | 0.4883 |      - |    4120 B |        0.50 |
| 'new U8String(span)'     | 4096 | Ascii  |   215.98 ns |     4.890 ns |   0.268 ns |  0.57 |    0.00 | 0.4933 | 0.0074 |    4128 B |        0.50 |
| OwnedText.FromUtf8(span) | 4096 | Ascii  |   181.47 ns |    35.722 ns |   1.958 ns |  0.48 |    0.01 |      - |      - |         - |        0.00 |
|                          |      |        |             |              |            |       |         |        |        |           |             |
| Encoding.GetString       | 4096 | Mixed  | 3,930.13 ns |   730.440 ns |  40.038 ns |  1.00 |    0.01 | 0.9766 |      - |    8216 B |        1.00 |
| 'Text.FromUtf8(byte[])'  | 4096 | Mixed  | 9,583.22 ns |   933.210 ns |  51.152 ns |  2.44 |    0.02 |      - |      - |         - |        0.00 |
| Text.FromUtf8(span)      | 4096 | Mixed  | 9,199.08 ns |   522.968 ns |  28.666 ns |  2.34 |    0.02 | 0.6714 |      - |    5664 B |        0.69 |
| 'new U8String(span)'     | 4096 | Mixed  | 2,753.91 ns |   109.910 ns |   6.025 ns |  0.70 |    0.01 | 0.6752 | 0.0114 |    5672 B |        0.69 |
| OwnedText.FromUtf8(span) | 4096 | Mixed  |   254.38 ns |    18.887 ns |   1.035 ns |  0.06 |    0.00 |      - |      - |         - |        0.00 |

## TextSearchBenchmarks

| Method                       | Categories | N    | Locale | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------------- |----------- |----- |------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| string.Contains              | Contains   | 256  | Ascii  |  2.7391 ns | 0.3046 ns | 0.0167 ns |  1.00 |    0.01 |         - |          NA |
| 'Text.Contains same-enc'     | Contains   | 256  | Ascii  |  7.4417 ns | 0.6961 ns | 0.0382 ns |  2.72 |    0.02 |         - |          NA |
| 'Text.Contains cross-enc'    | Contains   | 256  | Ascii  | 14.3460 ns | 2.0334 ns | 0.1115 ns |  5.24 |    0.04 |         - |          NA |
| U8String.Contains            | Contains   | 256  | Ascii  |  2.3490 ns | 0.6639 ns | 0.0364 ns |  0.86 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.Contains              | Contains   | 256  | Mixed  |  2.7002 ns | 0.4019 ns | 0.0220 ns |  1.00 |    0.01 |         - |          NA |
| 'Text.Contains same-enc'     | Contains   | 256  | Mixed  |  7.5578 ns | 0.8087 ns | 0.0443 ns |  2.80 |    0.02 |         - |          NA |
| 'Text.Contains cross-enc'    | Contains   | 256  | Mixed  | 14.7416 ns | 1.9323 ns | 0.1059 ns |  5.46 |    0.05 |         - |          NA |
| U8String.Contains            | Contains   | 256  | Mixed  |  2.5864 ns | 0.4610 ns | 0.0253 ns |  0.96 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.Contains              | Contains   | 4096 | Ascii  |  2.7681 ns | 0.3882 ns | 0.0213 ns |  1.00 |    0.01 |         - |          NA |
| 'Text.Contains same-enc'     | Contains   | 4096 | Ascii  |  7.4301 ns | 0.7545 ns | 0.0414 ns |  2.68 |    0.02 |         - |          NA |
| 'Text.Contains cross-enc'    | Contains   | 4096 | Ascii  | 13.6048 ns | 0.7287 ns | 0.0399 ns |  4.92 |    0.03 |         - |          NA |
| U8String.Contains            | Contains   | 4096 | Ascii  |  2.3156 ns | 0.1682 ns | 0.0092 ns |  0.84 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.Contains              | Contains   | 4096 | Mixed  |  2.6852 ns | 0.3162 ns | 0.0173 ns |  1.00 |    0.01 |         - |          NA |
| 'Text.Contains same-enc'     | Contains   | 4096 | Mixed  |  7.8112 ns | 0.1233 ns | 0.0068 ns |  2.91 |    0.02 |         - |          NA |
| 'Text.Contains cross-enc'    | Contains   | 4096 | Mixed  | 13.8106 ns | 2.0688 ns | 0.1134 ns |  5.14 |    0.05 |         - |          NA |
| U8String.Contains            | Contains   | 4096 | Mixed  |  2.5758 ns | 0.4048 ns | 0.0222 ns |  0.96 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.IndexOf               | IndexOf    | 256  | Ascii  |  2.6637 ns | 0.8145 ns | 0.0446 ns |  1.00 |    0.02 |         - |          NA |
| 'Text.RuneIndexOf same-enc'  | IndexOf    | 256  | Ascii  |  8.8835 ns | 0.1757 ns | 0.0096 ns |  3.34 |    0.05 |         - |          NA |
| 'Text.RuneIndexOf cross-enc' | IndexOf    | 256  | Ascii  | 15.6161 ns | 1.5909 ns | 0.0872 ns |  5.86 |    0.09 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 256  | Ascii  |  2.2739 ns | 0.1634 ns | 0.0090 ns |  0.85 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.IndexOf               | IndexOf    | 256  | Mixed  |  2.6703 ns | 0.8380 ns | 0.0459 ns |  1.00 |    0.02 |         - |          NA |
| 'Text.RuneIndexOf same-enc'  | IndexOf    | 256  | Mixed  |  9.5805 ns | 1.6870 ns | 0.0925 ns |  3.59 |    0.06 |         - |          NA |
| 'Text.RuneIndexOf cross-enc' | IndexOf    | 256  | Mixed  | 16.9620 ns | 2.6007 ns | 0.1426 ns |  6.35 |    0.11 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 256  | Mixed  |  2.5288 ns | 0.2772 ns | 0.0152 ns |  0.95 |    0.02 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.IndexOf               | IndexOf    | 4096 | Ascii  |  2.6891 ns | 0.0958 ns | 0.0052 ns |  1.00 |    0.00 |         - |          NA |
| 'Text.RuneIndexOf same-enc'  | IndexOf    | 4096 | Ascii  |  8.8887 ns | 1.4228 ns | 0.0780 ns |  3.31 |    0.03 |         - |          NA |
| 'Text.RuneIndexOf cross-enc' | IndexOf    | 4096 | Ascii  | 15.8458 ns | 1.4059 ns | 0.0771 ns |  5.89 |    0.03 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 4096 | Ascii  |  2.3084 ns | 0.0375 ns | 0.0021 ns |  0.86 |    0.00 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.IndexOf               | IndexOf    | 4096 | Mixed  |  2.6491 ns | 0.3486 ns | 0.0191 ns |  1.00 |    0.01 |         - |          NA |
| 'Text.RuneIndexOf same-enc'  | IndexOf    | 4096 | Mixed  |  9.6037 ns | 2.7412 ns | 0.1503 ns |  3.63 |    0.05 |         - |          NA |
| 'Text.RuneIndexOf cross-enc' | IndexOf    | 4096 | Mixed  | 16.0664 ns | 1.9800 ns | 0.1085 ns |  6.07 |    0.05 |         - |          NA |
| U8String.IndexOf             | IndexOf    | 4096 | Mixed  |  2.5662 ns | 0.1873 ns | 0.0103 ns |  0.97 |    0.01 |         - |          NA |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.StartsWith            | StartsWith | 256  | Ascii  |  0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| 'Text.StartsWith same-enc'   | StartsWith | 256  | Ascii  |  5.8056 ns | 0.2542 ns | 0.0139 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 256  | Ascii  |  0.1654 ns | 0.0382 ns | 0.0021 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.StartsWith            | StartsWith | 256  | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| 'Text.StartsWith same-enc'   | StartsWith | 256  | Mixed  |  6.0277 ns | 0.1843 ns | 0.0101 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 256  | Mixed  |  0.1184 ns | 0.1267 ns | 0.0069 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.StartsWith            | StartsWith | 4096 | Ascii  |  0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| 'Text.StartsWith same-enc'   | StartsWith | 4096 | Ascii  |  5.7504 ns | 0.2600 ns | 0.0143 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 4096 | Ascii  |  0.1669 ns | 0.0559 ns | 0.0031 ns |     ? |       ? |         - |           ? |
|                              |            |      |        |            |           |           |       |         |           |             |
| string.StartsWith            | StartsWith | 4096 | Mixed  |  0.0000 ns | 0.0000 ns | 0.0000 ns |     ? |       ? |         - |           ? |
| 'Text.StartsWith same-enc'   | StartsWith | 4096 | Mixed  |  5.9942 ns | 0.9923 ns | 0.0544 ns |     ? |       ? |         - |           ? |
| U8String.StartsWith          | StartsWith | 4096 | Mixed  |  0.1455 ns | 0.0929 ns | 0.0051 ns |     ? |       ? |         - |           ? |

## TextMutationBenchmarks

| Method                      | Categories | N    | Locale | Mean         | Error         | StdDev       | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------------- |----------- |----- |------- |-------------:|--------------:|-------------:|------:|--------:|-------:|-------:|----------:|------------:|
| string.Replace              | Replace    | 256  | Ascii  |    118.23 ns |     11.284 ns |     0.619 ns |  1.00 |    0.01 | 0.0753 |      - |     632 B |        1.00 |
| Text.Replace                | Replace    | 256  | Ascii  |    313.80 ns |      4.971 ns |     0.272 ns |  2.65 |    0.01 | 0.0391 |      - |     328 B |        0.52 |
| Text.ReplacePooled          | Replace    | 256  | Ascii  |    290.99 ns |     31.308 ns |     1.716 ns |  2.46 |    0.02 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 256  | Ascii  |    107.64 ns |      7.673 ns |     0.421 ns |  0.91 |    0.01 | 0.0401 |      - |     336 B |        0.53 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.Replace              | Replace    | 256  | Mixed  |    118.59 ns |     26.011 ns |     1.426 ns |  1.00 |    0.01 | 0.0753 |      - |     632 B |        1.00 |
| Text.Replace                | Replace    | 256  | Mixed  |    373.98 ns |      7.166 ns |     0.393 ns |  3.15 |    0.03 | 0.0505 |      - |     424 B |        0.67 |
| Text.ReplacePooled          | Replace    | 256  | Mixed  |    368.71 ns |     56.011 ns |     3.070 ns |  3.11 |    0.04 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 256  | Mixed  |    115.36 ns |     16.480 ns |     0.903 ns |  0.97 |    0.01 | 0.0515 |      - |     432 B |        0.68 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.Replace              | Replace    | 4096 | Ascii  |  2,133.39 ns |    465.361 ns |    25.508 ns |  1.00 |    0.01 | 1.1749 |      - |    9848 B |        1.00 |
| Text.Replace                | Replace    | 4096 | Ascii  | 19,370.93 ns |  3,070.162 ns |   168.286 ns |  9.08 |    0.12 | 0.5798 |      - |    4936 B |        0.50 |
| Text.ReplacePooled          | Replace    | 4096 | Ascii  | 18,892.55 ns |  1,343.216 ns |    73.626 ns |  8.86 |    0.10 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 4096 | Ascii  |  1,441.04 ns |    503.173 ns |    27.581 ns |  0.68 |    0.01 | 0.5894 | 0.0095 |    4944 B |        0.50 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.Replace              | Replace    | 4096 | Mixed  |  2,099.32 ns |    199.274 ns |    10.923 ns |  1.00 |    0.01 | 1.1749 |      - |    9848 B |        1.00 |
| Text.Replace                | Replace    | 4096 | Mixed  | 26,804.08 ns |  1,615.892 ns |    88.572 ns | 12.77 |    0.07 | 0.7629 |      - |    6496 B |        0.66 |
| Text.ReplacePooled          | Replace    | 4096 | Mixed  | 25,823.80 ns |    703.290 ns |    38.550 ns | 12.30 |    0.06 |      - |      - |         - |        0.00 |
| U8String.Replace            | Replace    | 4096 | Mixed  |  2,097.85 ns |    134.661 ns |     7.381 ns |  1.00 |    0.01 | 0.7744 | 0.0153 |    6496 B |        0.66 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.ToUpperInvariant     | ToUpper    | 256  | Ascii  |     28.77 ns |      4.032 ns |     0.221 ns |  1.00 |    0.01 | 0.0669 |      - |     560 B |        1.00 |
| Text.ToUpperInvariant       | ToUpper    | 256  | Ascii  |  2,005.90 ns |    205.499 ns |    11.264 ns | 69.72 |    0.57 | 0.0343 |      - |     296 B |        0.53 |
| Text.ToUpperInvariantPooled | ToUpper    | 256  | Ascii  |  2,026.15 ns |    334.791 ns |    18.351 ns | 70.43 |    0.72 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.ToUpperInvariant     | ToUpper    | 256  | Mixed  |    535.83 ns |    252.737 ns |    13.853 ns |  1.00 |    0.03 | 0.0668 |      - |     560 B |        1.00 |
| Text.ToUpperInvariant       | ToUpper    | 256  | Mixed  |  2,402.28 ns |     83.182 ns |     4.559 ns |  4.49 |    0.10 | 0.0458 |      - |     392 B |        0.70 |
| Text.ToUpperInvariantPooled | ToUpper    | 256  | Mixed  |  2,353.09 ns |    273.283 ns |    14.980 ns |  4.39 |    0.10 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.ToUpperInvariant     | ToUpper    | 4096 | Ascii  |    391.28 ns |     56.251 ns |     3.083 ns |  1.00 |    0.01 | 1.0295 |      - |    8624 B |        1.00 |
| Text.ToUpperInvariant       | ToUpper    | 4096 | Ascii  | 32,329.14 ns |  6,224.965 ns |   341.211 ns | 82.63 |    0.94 | 0.4883 |      - |    4328 B |        0.50 |
| Text.ToUpperInvariantPooled | ToUpper    | 4096 | Ascii  | 32,988.24 ns | 10,582.307 ns |   580.052 ns | 84.31 |    1.41 |      - |      - |         - |        0.00 |
|                             |            |      |        |              |               |              |       |         |        |        |           |             |
| string.ToUpperInvariant     | ToUpper    | 4096 | Mixed  |  8,583.68 ns |    467.955 ns |    25.650 ns |  1.00 |    0.00 | 1.0223 |      - |    8624 B |        1.00 |
| Text.ToUpperInvariant       | ToUpper    | 4096 | Mixed  | 38,273.33 ns | 19,895.858 ns | 1,090.559 ns |  4.46 |    0.11 | 0.6714 |      - |    5888 B |        0.68 |
| Text.ToUpperInvariantPooled | ToUpper    | 4096 | Mixed  | 38,444.13 ns |  5,284.226 ns |   289.646 ns |  4.48 |    0.03 |      - |      - |         - |        0.00 |

## TextSplitBenchmarks

| Method                 | Categories     | N    | Locale | Mean         | Error         | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |--------------- |----- |------- |-------------:|--------------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| string.EnumerateRunes  | EnumerateRunes | 256  | Ascii  |    387.01 ns |    146.636 ns |   8.038 ns |  1.00 |    0.03 |      - |      - |         - |          NA |
| Text.EnumerateRunes    | EnumerateRunes | 256  | Ascii  |    573.89 ns |     13.559 ns |   0.743 ns |  1.48 |    0.03 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 256  | Ascii  |     79.50 ns |      9.182 ns |   0.503 ns |  0.21 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| string.EnumerateRunes  | EnumerateRunes | 256  | Mixed  |    383.75 ns |     25.815 ns |   1.415 ns |  1.00 |    0.00 |      - |      - |         - |          NA |
| Text.EnumerateRunes    | EnumerateRunes | 256  | Mixed  |    584.86 ns |     87.290 ns |   4.785 ns |  1.52 |    0.01 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 256  | Mixed  |     77.82 ns |      4.991 ns |   0.274 ns |  0.20 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| string.EnumerateRunes  | EnumerateRunes | 4096 | Ascii  |  1,661.10 ns |    568.054 ns |  31.137 ns |  1.00 |    0.02 |      - |      - |         - |          NA |
| Text.EnumerateRunes    | EnumerateRunes | 4096 | Ascii  |  9,097.09 ns |  1,878.798 ns | 102.983 ns |  5.48 |    0.10 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 4096 | Ascii  |  1,245.00 ns |    130.323 ns |   7.143 ns |  0.75 |    0.01 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| string.EnumerateRunes  | EnumerateRunes | 4096 | Mixed  |  6,266.30 ns |  2,861.890 ns | 156.870 ns |  1.00 |    0.03 |      - |      - |         - |          NA |
| Text.EnumerateRunes    | EnumerateRunes | 4096 | Mixed  |  9,172.33 ns |    602.219 ns |  33.010 ns |  1.46 |    0.03 |      - |      - |         - |          NA |
| U8String.Runes         | EnumerateRunes | 4096 | Mixed  |  1,188.10 ns |    156.558 ns |   8.581 ns |  0.19 |    0.00 |      - |      - |         - |          NA |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| 'string.Split count'   | Split          | 256  | Ascii  |    278.78 ns |     46.461 ns |   2.547 ns |  1.00 |    0.01 | 0.1683 | 0.0005 |    1408 B |        1.00 |
| 'Text.Split count'     | Split          | 256  | Ascii  |  2,080.46 ns |    126.163 ns |   6.915 ns |  7.46 |    0.06 |      - |      - |         - |        0.00 |
| 'U8String.Split count' | Split          | 256  | Ascii  |    227.43 ns |     68.777 ns |   3.770 ns |  0.82 |    0.01 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| 'string.Split count'   | Split          | 256  | Mixed  |    322.16 ns |     19.408 ns |   1.064 ns |  1.00 |    0.00 | 0.1683 | 0.0005 |    1408 B |        1.00 |
| 'Text.Split count'     | Split          | 256  | Mixed  |  1,955.40 ns |    478.027 ns |  26.202 ns |  6.07 |    0.07 |      - |      - |         - |        0.00 |
| 'U8String.Split count' | Split          | 256  | Mixed  |    233.23 ns |     30.778 ns |   1.687 ns |  0.72 |    0.00 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| 'string.Split count'   | Split          | 4096 | Ascii  |  4,601.54 ns |  1,158.196 ns |  63.485 ns |  1.00 |    0.02 | 2.6169 | 0.1907 |   21896 B |        1.00 |
| 'Text.Split count'     | Split          | 4096 | Ascii  | 33,007.07 ns |  2,217.912 ns | 121.571 ns |  7.17 |    0.09 |      - |      - |         - |        0.00 |
| 'U8String.Split count' | Split          | 4096 | Ascii  |  2,335.20 ns |  5,975.375 ns | 327.530 ns |  0.51 |    0.06 |      - |      - |         - |        0.00 |
|                        |                |      |        |              |               |            |       |         |        |        |           |             |
| 'string.Split count'   | Split          | 4096 | Mixed  |  4,777.24 ns |  1,105.380 ns |  60.590 ns |  1.00 |    0.02 | 2.6169 | 0.1907 |   21896 B |        1.00 |
| 'Text.Split count'     | Split          | 4096 | Mixed  | 31,683.68 ns | 13,615.891 ns | 746.333 ns |  6.63 |    0.15 |      - |      - |         - |        0.00 |
| 'U8String.Split count' | Split          | 4096 | Mixed  |  2,506.68 ns |  1,189.830 ns |  65.219 ns |  0.52 |    0.01 |      - |      - |         - |        0.00 |

## TextEqualityBenchmarks

| Method                      | Categories  | N    | Locale | Mean           | Error         | StdDev      | Ratio  | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |------------ |----- |------- |---------------:|--------------:|------------:|-------:|--------:|----------:|------------:|
| string.Compare              | CompareTo   | 256  | Ascii  |     11.2300 ns |     2.7494 ns |   0.1507 ns |   1.00 |    0.02 |         - |          NA |
| 'Text.CompareTo same-enc'   | CompareTo   | 256  | Ascii  |     11.6897 ns |     1.9814 ns |   0.1086 ns |   1.04 |    0.01 |         - |          NA |
| 'Text.CompareTo cross-enc'  | CompareTo   | 256  | Ascii  |    384.0681 ns |    54.3167 ns |   2.9773 ns |  34.20 |    0.46 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Compare              | CompareTo   | 256  | Mixed  |     10.7947 ns |     1.3860 ns |   0.0760 ns |   1.00 |    0.01 |         - |          NA |
| 'Text.CompareTo same-enc'   | CompareTo   | 256  | Mixed  |     13.6479 ns |     5.1264 ns |   0.2810 ns |   1.26 |    0.02 |         - |          NA |
| 'Text.CompareTo cross-enc'  | CompareTo   | 256  | Mixed  |    740.4415 ns |   150.8073 ns |   8.2663 ns |  68.60 |    0.78 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Compare              | CompareTo   | 4096 | Ascii  |    173.9202 ns |     0.5753 ns |   0.0315 ns |   1.00 |    0.00 |         - |          NA |
| 'Text.CompareTo same-enc'   | CompareTo   | 4096 | Ascii  |     83.6971 ns |    31.0519 ns |   1.7021 ns |   0.48 |    0.01 |         - |          NA |
| 'Text.CompareTo cross-enc'  | CompareTo   | 4096 | Ascii  |  6,358.8189 ns | 1,117.4380 ns |  61.2505 ns |  36.56 |    0.31 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Compare              | CompareTo   | 4096 | Mixed  |    176.6204 ns |    28.2527 ns |   1.5486 ns |   1.00 |    0.01 |         - |          NA |
| 'Text.CompareTo same-enc'   | CompareTo   | 4096 | Mixed  |    105.8258 ns |     2.5324 ns |   0.1388 ns |   0.60 |    0.00 |         - |          NA |
| 'Text.CompareTo cross-enc'  | CompareTo   | 4096 | Mixed  | 11,531.8544 ns | 1,341.2406 ns |  73.5179 ns |  65.30 |    0.61 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Equals               | Equals      | 256  | Ascii  |      8.8023 ns |     1.1994 ns |   0.0657 ns |   1.00 |    0.01 |         - |          NA |
| 'Text.Equals same-enc'      | Equals      | 256  | Ascii  |      7.7237 ns |     0.6445 ns |   0.0353 ns |   0.88 |    0.01 |         - |          NA |
| 'Text.Equals cross-enc'     | Equals      | 256  | Ascii  |    373.6390 ns |    36.0489 ns |   1.9760 ns |  42.45 |    0.34 |         - |          NA |
| 'Text.Equals different'     | Equals      | 256  | Ascii  |      7.7044 ns |     0.3891 ns |   0.0213 ns |   0.88 |    0.01 |         - |          NA |
| U8String.Equals             | Equals      | 256  | Ascii  |      9.3434 ns |     4.2478 ns |   0.2328 ns |   1.06 |    0.02 |         - |          NA |
| 'U8String.Equals different' | Equals      | 256  | Ascii  |      5.1071 ns |     2.5552 ns |   0.1401 ns |   0.58 |    0.01 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Equals               | Equals      | 256  | Mixed  |      8.8295 ns |     1.1957 ns |   0.0655 ns |   1.00 |    0.01 |         - |          NA |
| 'Text.Equals same-enc'      | Equals      | 256  | Mixed  |      9.2786 ns |     1.2002 ns |   0.0658 ns |   1.05 |    0.01 |         - |          NA |
| 'Text.Equals cross-enc'     | Equals      | 256  | Mixed  |    750.7923 ns |    71.3701 ns |   3.9120 ns |  85.04 |    0.67 |         - |          NA |
| 'Text.Equals different'     | Equals      | 256  | Mixed  |      9.2184 ns |     0.8354 ns |   0.0458 ns |   1.04 |    0.01 |         - |          NA |
| U8String.Equals             | Equals      | 256  | Mixed  |      9.6722 ns |     1.4869 ns |   0.0815 ns |   1.10 |    0.01 |         - |          NA |
| 'U8String.Equals different' | Equals      | 256  | Mixed  |      5.9832 ns |     1.2916 ns |   0.0708 ns |   0.68 |    0.01 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Equals               | Equals      | 4096 | Ascii  |    148.3928 ns |     6.4311 ns |   0.3525 ns |   1.00 |    0.00 |         - |          NA |
| 'Text.Equals same-enc'      | Equals      | 4096 | Ascii  |     84.7261 ns |     4.0925 ns |   0.2243 ns |   0.57 |    0.00 |         - |          NA |
| 'Text.Equals cross-enc'     | Equals      | 4096 | Ascii  |  5,830.9148 ns |   618.8238 ns |  33.9198 ns |  39.29 |    0.21 |         - |          NA |
| 'Text.Equals different'     | Equals      | 4096 | Ascii  |     85.6428 ns |    11.4228 ns |   0.6261 ns |   0.58 |    0.00 |         - |          NA |
| U8String.Equals             | Equals      | 4096 | Ascii  |     80.0222 ns |     5.9771 ns |   0.3276 ns |   0.54 |    0.00 |         - |          NA |
| 'U8String.Equals different' | Equals      | 4096 | Ascii  |     79.7266 ns |     0.6894 ns |   0.0378 ns |   0.54 |    0.00 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.Equals               | Equals      | 4096 | Mixed  |    148.0150 ns |     9.3802 ns |   0.5142 ns |  1.000 |    0.00 |         - |          NA |
| 'Text.Equals same-enc'      | Equals      | 4096 | Mixed  |    111.7074 ns |    10.6382 ns |   0.5831 ns |  0.755 |    0.00 |         - |          NA |
| 'Text.Equals cross-enc'     | Equals      | 4096 | Mixed  | 11,531.7654 ns | 2,148.6171 ns | 117.7730 ns | 77.910 |    0.73 |         - |          NA |
| 'Text.Equals different'     | Equals      | 4096 | Mixed  |      6.8270 ns |     0.2208 ns |   0.0121 ns |  0.046 |    0.00 |         - |          NA |
| U8String.Equals             | Equals      | 4096 | Mixed  |    106.7863 ns |    11.8341 ns |   0.6487 ns |  0.721 |    0.00 |         - |          NA |
| 'U8String.Equals different' | Equals      | 4096 | Mixed  |      0.0000 ns |     0.0000 ns |   0.0000 ns |  0.000 |    0.00 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.GetHashCode          | GetHashCode | 256  | Ascii  |    138.8887 ns |    21.2404 ns |   1.1643 ns |  1.000 |    0.01 |         - |          NA |
| Text.GetHashCode            | GetHashCode | 256  | Ascii  |      0.0000 ns |     0.0000 ns |   0.0000 ns |  0.000 |    0.00 |         - |          NA |
| U8String.GetHashCode        | GetHashCode | 256  | Ascii  |     13.3223 ns |     0.4791 ns |   0.0263 ns |  0.096 |    0.00 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.GetHashCode          | GetHashCode | 256  | Mixed  |    137.9848 ns |    10.1679 ns |   0.5573 ns |  1.000 |    0.00 |         - |          NA |
| Text.GetHashCode            | GetHashCode | 256  | Mixed  |      0.0000 ns |     0.0000 ns |   0.0000 ns |  0.000 |    0.00 |         - |          NA |
| U8String.GetHashCode        | GetHashCode | 256  | Mixed  |     16.3664 ns |     0.2025 ns |   0.0111 ns |  0.119 |    0.00 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.GetHashCode          | GetHashCode | 4096 | Ascii  |  2,338.1468 ns |   101.7687 ns |   5.5783 ns |  1.000 |    0.00 |         - |          NA |
| Text.GetHashCode            | GetHashCode | 4096 | Ascii  |      0.0000 ns |     0.0000 ns |   0.0000 ns |  0.000 |    0.00 |         - |          NA |
| U8String.GetHashCode        | GetHashCode | 4096 | Ascii  |    117.4210 ns |    12.9150 ns |   0.7079 ns |  0.050 |    0.00 |         - |          NA |
|                             |             |      |        |                |               |             |        |         |           |             |
| string.GetHashCode          | GetHashCode | 4096 | Mixed  |  2,349.3883 ns |   326.1592 ns |  17.8779 ns |  1.000 |    0.01 |         - |          NA |
| Text.GetHashCode            | GetHashCode | 4096 | Mixed  |      0.0000 ns |     0.0000 ns |   0.0000 ns |  0.000 |    0.00 |         - |          NA |
| U8String.GetHashCode        | GetHashCode | 4096 | Mixed  |    163.7905 ns |     5.8196 ns |   0.3190 ns |  0.070 |    0.00 |         - |          NA |

## TextConcatBenchmarks

| Method            | PartSize | Parts | Locale | Mean      | Error      | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------ |--------- |------ |------- |----------:|-----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| string.Concat     | 8        | 4     | Ascii  | 10.308 ns |  0.8491 ns | 0.0465 ns |  1.00 |    0.01 | 0.0105 |      - |      88 B |        1.00 |
| Text.Concat       | 8        | 4     | Ascii  | 12.139 ns |  4.2629 ns | 0.2337 ns |  1.18 |    0.02 | 0.0067 |      - |      56 B |        0.64 |
| Text.ConcatPooled | 8        | 4     | Ascii  | 19.771 ns |  6.3266 ns | 0.3468 ns |  1.92 |    0.03 |      - |      - |         - |        0.00 |
| U8String.Concat   | 8        | 4     | Ascii  |  8.951 ns |  1.7047 ns | 0.0934 ns |  0.87 |    0.01 | 0.0076 |      - |      64 B |        0.73 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 8        | 4     | Mixed  | 10.272 ns |  0.7580 ns | 0.0416 ns |  1.00 |    0.00 | 0.0105 |      - |      88 B |        1.00 |
| Text.Concat       | 8        | 4     | Mixed  | 12.321 ns |  1.9489 ns | 0.1068 ns |  1.20 |    0.01 | 0.0086 |      - |      72 B |        0.82 |
| Text.ConcatPooled | 8        | 4     | Mixed  | 17.806 ns |  7.1059 ns | 0.3895 ns |  1.73 |    0.03 |      - |      - |         - |        0.00 |
| U8String.Concat   | 8        | 4     | Mixed  |  9.084 ns |  0.3329 ns | 0.0182 ns |  0.88 |    0.00 | 0.0086 |      - |      72 B |        0.82 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 8        | 16    | Ascii  | 34.154 ns |  1.1653 ns | 0.0639 ns |  1.00 |    0.00 | 0.0334 |      - |     280 B |        1.00 |
| Text.Concat       | 8        | 16    | Ascii  | 38.683 ns |  9.6217 ns | 0.5274 ns |  1.13 |    0.01 | 0.0181 |      - |     152 B |        0.54 |
| Text.ConcatPooled | 8        | 16    | Ascii  | 42.075 ns |  6.3334 ns | 0.3472 ns |  1.23 |    0.01 |      - |      - |         - |        0.00 |
| U8String.Concat   | 8        | 16    | Ascii  | 30.089 ns | 12.5621 ns | 0.6886 ns |  0.88 |    0.02 | 0.0191 |      - |     160 B |        0.57 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 8        | 16    | Mixed  | 35.157 ns |  0.8501 ns | 0.0466 ns |  1.00 |    0.00 | 0.0334 |      - |     280 B |        1.00 |
| Text.Concat       | 8        | 16    | Mixed  | 38.967 ns |  4.9812 ns | 0.2730 ns |  1.11 |    0.01 | 0.0249 |      - |     208 B |        0.74 |
| Text.ConcatPooled | 8        | 16    | Mixed  | 42.535 ns |  3.2339 ns | 0.1773 ns |  1.21 |    0.00 |      - |      - |         - |        0.00 |
| U8String.Concat   | 8        | 16    | Mixed  | 31.097 ns |  3.6137 ns | 0.1981 ns |  0.88 |    0.00 | 0.0249 |      - |     208 B |        0.74 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 64       | 4     | Ascii  | 24.449 ns |  2.9662 ns | 0.1626 ns |  1.00 |    0.01 | 0.0641 |      - |     536 B |        1.00 |
| Text.Concat       | 64       | 4     | Ascii  | 17.582 ns |  0.9784 ns | 0.0536 ns |  0.72 |    0.00 | 0.0335 | 0.0000 |     280 B |        0.52 |
| Text.ConcatPooled | 64       | 4     | Ascii  | 18.853 ns |  3.0160 ns | 0.1653 ns |  0.77 |    0.01 |      - |      - |         - |        0.00 |
| U8String.Concat   | 64       | 4     | Ascii  | 15.304 ns |  7.8279 ns | 0.4291 ns |  0.63 |    0.02 | 0.0344 | 0.0000 |     288 B |        0.54 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 64       | 4     | Mixed  | 25.346 ns | 15.4997 ns | 0.8496 ns |  1.00 |    0.04 | 0.0641 |      - |     536 B |        1.00 |
| Text.Concat       | 64       | 4     | Mixed  | 21.163 ns |  0.6372 ns | 0.0349 ns |  0.84 |    0.02 | 0.0459 | 0.0001 |     384 B |        0.72 |
| Text.ConcatPooled | 64       | 4     | Mixed  | 19.447 ns |  2.5274 ns | 0.1385 ns |  0.77 |    0.02 |      - |      - |         - |        0.00 |
| U8String.Concat   | 64       | 4     | Mixed  | 18.701 ns |  6.1998 ns | 0.3398 ns |  0.74 |    0.02 | 0.0459 | 0.0001 |     384 B |        0.72 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 64       | 16    | Ascii  | 91.260 ns | 10.1676 ns | 0.5573 ns |  1.00 |    0.01 | 0.2476 |      - |    2072 B |        1.00 |
| Text.Concat       | 64       | 16    | Ascii  | 59.707 ns |  2.8829 ns | 0.1580 ns |  0.65 |    0.00 | 0.1253 | 0.0005 |    1048 B |        0.51 |
| Text.ConcatPooled | 64       | 16    | Ascii  | 44.091 ns |  4.8126 ns | 0.2638 ns |  0.48 |    0.00 |      - |      - |         - |        0.00 |
| U8String.Concat   | 64       | 16    | Ascii  | 52.545 ns |  7.6396 ns | 0.4188 ns |  0.58 |    0.01 | 0.1262 | 0.0005 |    1056 B |        0.51 |
|                   |          |       |        |           |            |           |       |         |        |        |           |             |
| string.Concat     | 64       | 16    | Mixed  | 91.956 ns | 22.9617 ns | 1.2586 ns |  1.00 |    0.02 | 0.2476 |      - |    2072 B |        1.00 |
| Text.Concat       | 64       | 16    | Mixed  | 75.412 ns | 10.7371 ns | 0.5885 ns |  0.82 |    0.01 | 0.1721 | 0.0008 |    1440 B |        0.69 |
| Text.ConcatPooled | 64       | 16    | Mixed  | 48.943 ns |  3.7799 ns | 0.2072 ns |  0.53 |    0.01 |      - |      - |         - |        0.00 |
| U8String.Concat   | 64       | 16    | Mixed  | 66.519 ns |  2.8440 ns | 0.1559 ns |  0.72 |    0.01 | 0.1721 | 0.0008 |    1440 B |        0.69 |

## TextInterpolationBenchmarks

| Method                    | Categories | PartSize | Locale | Mean       | Error      | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|-------------------------- |----------- |--------- |------- |-----------:|-----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| 'string $"..."'           | 2 parts    | 8        | Ascii  |   4.627 ns |  0.4408 ns | 0.0242 ns |  1.00 |    0.01 | 0.0067 |      - |      56 B |        1.00 |
| 'Text.Create $"..."'      | 2 parts    | 8        | Ascii  |  21.202 ns |  2.3687 ns | 0.1298 ns |  4.58 |    0.03 | 0.0048 |      - |      40 B |        0.71 |
| 'OwnedText.Create $"..."' | 2 parts    | 8        | Ascii  |  20.973 ns |  4.4370 ns | 0.2432 ns |  4.53 |    0.05 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 2 parts    | 8        | Mixed  |   4.639 ns |  0.4513 ns | 0.0247 ns |  1.00 |    0.01 | 0.0067 |      - |      56 B |        1.00 |
| 'Text.Create $"..."'      | 2 parts    | 8        | Mixed  |  21.173 ns |  0.8416 ns | 0.0461 ns |  4.56 |    0.02 | 0.0057 |      - |      48 B |        0.86 |
| 'OwnedText.Create $"..."' | 2 parts    | 8        | Mixed  |  20.505 ns |  1.7237 ns | 0.0945 ns |  4.42 |    0.03 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 2 parts    | 64       | Ascii  |  11.988 ns |  1.6299 ns | 0.0893 ns |  1.00 |    0.01 | 0.0335 |      - |     280 B |        1.00 |
| 'Text.Create $"..."'      | 2 parts    | 64       | Ascii  |  32.945 ns |  0.8519 ns | 0.0467 ns |  2.75 |    0.02 | 0.0181 |      - |     152 B |        0.54 |
| 'OwnedText.Create $"..."' | 2 parts    | 64       | Ascii  |  27.309 ns |  2.6111 ns | 0.1431 ns |  2.28 |    0.02 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 2 parts    | 64       | Mixed  |  13.032 ns | 11.1299 ns | 0.6101 ns |  1.00 |    0.06 | 0.0335 |      - |     280 B |        1.00 |
| 'Text.Create $"..."'      | 2 parts    | 64       | Mixed  |  45.842 ns | 30.8655 ns | 1.6918 ns |  3.52 |    0.18 | 0.0249 |      - |     208 B |        0.74 |
| 'OwnedText.Create $"..."' | 2 parts    | 64       | Mixed  |  51.854 ns |  6.2147 ns | 0.3406 ns |  3.98 |    0.16 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 4 parts    | 8        | Ascii  |   8.086 ns |  0.4151 ns | 0.0228 ns |  1.00 |    0.00 | 0.0105 |      - |      88 B |        1.00 |
| 'Text.Create $"..."'      | 4 parts    | 8        | Ascii  |  33.303 ns |  5.0959 ns | 0.2793 ns |  4.12 |    0.03 | 0.0067 |      - |      56 B |        0.64 |
| 'OwnedText.Create $"..."' | 4 parts    | 8        | Ascii  |  35.423 ns |  5.0721 ns | 0.2780 ns |  4.38 |    0.03 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 4 parts    | 8        | Mixed  |   8.178 ns |  3.3688 ns | 0.1847 ns |  1.00 |    0.03 | 0.0105 |      - |      88 B |        1.00 |
| 'Text.Create $"..."'      | 4 parts    | 8        | Mixed  |  37.078 ns | 62.9714 ns | 3.4517 ns |  4.54 |    0.38 | 0.0086 |      - |      72 B |        0.82 |
| 'OwnedText.Create $"..."' | 4 parts    | 8        | Mixed  |  35.401 ns |  0.3944 ns | 0.0216 ns |  4.33 |    0.08 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 4 parts    | 64       | Ascii  |  24.151 ns | 18.8674 ns | 1.0342 ns |  1.00 |    0.05 | 0.0641 |      - |     536 B |        1.00 |
| 'Text.Create $"..."'      | 4 parts    | 64       | Ascii  |  64.728 ns |  2.0148 ns | 0.1104 ns |  2.68 |    0.10 | 0.0334 |      - |     280 B |        0.52 |
| 'OwnedText.Create $"..."' | 4 parts    | 64       | Ascii  |  60.557 ns |  4.2287 ns | 0.2318 ns |  2.51 |    0.09 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 4 parts    | 64       | Mixed  |  23.889 ns |  2.7089 ns | 0.1485 ns |  1.00 |    0.01 | 0.0641 |      - |     536 B |        1.00 |
| 'Text.Create $"..."'      | 4 parts    | 64       | Mixed  |  86.304 ns | 13.6681 ns | 0.7492 ns |  3.61 |    0.03 | 0.0459 |      - |     384 B |        0.72 |
| 'OwnedText.Create $"..."' | 4 parts    | 64       | Mixed  |  68.071 ns |  2.1495 ns | 0.1178 ns |  2.85 |    0.02 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 8 parts    | 8        | Ascii  |  27.430 ns |  4.0003 ns | 0.2193 ns |  1.00 |    0.01 | 0.0181 |      - |     152 B |        1.00 |
| 'Text.Create $"..."'      | 8 parts    | 8        | Ascii  |  65.854 ns | 12.0046 ns | 0.6580 ns |  2.40 |    0.03 | 0.0105 |      - |      88 B |        0.58 |
| 'OwnedText.Create $"..."' | 8 parts    | 8        | Ascii  |  62.825 ns |  4.7622 ns | 0.2610 ns |  2.29 |    0.02 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 8 parts    | 8        | Mixed  |  27.353 ns |  4.0559 ns | 0.2223 ns |  1.00 |    0.01 | 0.0181 |      - |     152 B |        1.00 |
| 'Text.Create $"..."'      | 8 parts    | 8        | Mixed  |  67.723 ns |  1.8974 ns | 0.1040 ns |  2.48 |    0.02 | 0.0134 |      - |     112 B |        0.74 |
| 'OwnedText.Create $"..."' | 8 parts    | 8        | Mixed  |  63.084 ns |  6.6859 ns | 0.3665 ns |  2.31 |    0.02 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 8 parts    | 64       | Ascii  |  84.544 ns |  8.6607 ns | 0.4747 ns |  1.00 |    0.01 | 0.1253 |      - |    1048 B |        1.00 |
| 'Text.Create $"..."'      | 8 parts    | 64       | Ascii  | 112.710 ns | 10.5307 ns | 0.5772 ns |  1.33 |    0.01 | 0.0640 | 0.0001 |     536 B |        0.51 |
| 'OwnedText.Create $"..."' | 8 parts    | 64       | Ascii  |  85.222 ns |  6.8249 ns | 0.3741 ns |  1.01 |    0.01 |      - |      - |         - |        0.00 |
|                           |            |          |        |            |            |           |       |         |        |        |           |             |
| 'string $"..."'           | 8 parts    | 64       | Mixed  | 102.811 ns | 17.3330 ns | 0.9501 ns |  1.00 |    0.01 | 0.1253 |      - |    1048 B |        1.00 |
| 'Text.Create $"..."'      | 8 parts    | 64       | Mixed  | 130.317 ns |  1.3245 ns | 0.0726 ns |  1.27 |    0.01 | 0.0880 | 0.0002 |     736 B |        0.70 |
| 'OwnedText.Create $"..."' | 8 parts    | 64       | Mixed  |  99.554 ns |  5.3134 ns | 0.2912 ns |  0.97 |    0.01 |      - |      - |         - |        0.00 |

## TextBuilderBenchmarks

| Method                                | PartSize | Parts | Locale | Mean        | Error     | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|-------------------------------------- |--------- |------ |------- |------------:|----------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| 'StringBuilder -> ToString'           | 8        | 4     | Ascii  |    30.14 ns |  1.982 ns | 0.109 ns |  1.00 |    0.00 | 0.0353 |      - |     296 B |        1.00 |
| 'TextBuilder -> ToText'               | 8        | 4     | Ascii  |    32.49 ns |  5.720 ns | 0.314 ns |  1.08 |    0.01 | 0.0067 |      - |      56 B |        0.19 |
| 'TextBuilder -> ToOwnedText'          | 8        | 4     | Ascii  |    42.80 ns |  0.313 ns | 0.017 ns |  1.42 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 8        | 4     | Ascii  |    38.64 ns |  2.935 ns | 0.161 ns |  1.28 |    0.01 | 0.0067 |      - |      56 B |        0.19 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 8        | 4     | Mixed  |    31.11 ns | 39.548 ns | 2.168 ns |  1.00 |    0.08 | 0.0353 |      - |     296 B |        1.00 |
| 'TextBuilder -> ToText'               | 8        | 4     | Mixed  |    34.58 ns | 19.060 ns | 1.045 ns |  1.11 |    0.07 | 0.0086 |      - |      72 B |        0.24 |
| 'TextBuilder -> ToOwnedText'          | 8        | 4     | Mixed  |    43.22 ns |  4.874 ns | 0.267 ns |  1.39 |    0.08 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 8        | 4     | Mixed  |    49.54 ns |  4.636 ns | 0.254 ns |  1.60 |    0.09 | 0.0086 |      - |      72 B |        0.24 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 8        | 16    | Ascii  |    83.02 ns | 13.760 ns | 0.754 ns |  1.00 |    0.01 | 0.0985 | 0.0001 |     824 B |        1.00 |
| 'TextBuilder -> ToText'               | 8        | 16    | Ascii  |    98.78 ns |  9.827 ns | 0.539 ns |  1.19 |    0.01 | 0.0181 |      - |     152 B |        0.18 |
| 'TextBuilder -> ToOwnedText'          | 8        | 16    | Ascii  |   108.07 ns |  3.405 ns | 0.187 ns |  1.30 |    0.01 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 8        | 16    | Ascii  |   131.52 ns | 15.416 ns | 0.845 ns |  1.58 |    0.02 | 0.0181 |      - |     152 B |        0.18 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 8        | 16    | Mixed  |    86.91 ns |  6.474 ns | 0.355 ns |  1.00 |    0.00 | 0.0985 | 0.0001 |     824 B |        1.00 |
| 'TextBuilder -> ToText'               | 8        | 16    | Mixed  |   101.59 ns |  6.555 ns | 0.359 ns |  1.17 |    0.01 | 0.0248 |      - |     208 B |        0.25 |
| 'TextBuilder -> ToOwnedText'          | 8        | 16    | Mixed  |   106.72 ns |  2.121 ns | 0.116 ns |  1.23 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 8        | 16    | Mixed  |   309.61 ns | 62.552 ns | 3.429 ns |  3.56 |    0.04 | 0.0782 |      - |     656 B |        0.80 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 64       | 4     | Ascii  |    87.75 ns | 10.598 ns | 0.581 ns |  1.00 |    0.01 | 0.1596 | 0.0005 |    1336 B |        1.00 |
| 'TextBuilder -> ToText'               | 64       | 4     | Ascii  |    44.87 ns |  2.258 ns | 0.124 ns |  0.51 |    0.00 | 0.0334 |      - |     280 B |        0.21 |
| 'TextBuilder -> ToOwnedText'          | 64       | 4     | Ascii  |    44.15 ns |  2.664 ns | 0.146 ns |  0.50 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 64       | 4     | Ascii  |    72.34 ns |  4.544 ns | 0.249 ns |  0.82 |    0.01 | 0.0334 |      - |     280 B |        0.21 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 64       | 4     | Mixed  |    86.57 ns | 12.096 ns | 0.663 ns |  1.00 |    0.01 | 0.1596 | 0.0005 |    1336 B |        1.00 |
| 'TextBuilder -> ToText'               | 64       | 4     | Mixed  |    58.51 ns |  9.597 ns | 0.526 ns |  0.68 |    0.01 | 0.0459 |      - |     384 B |        0.29 |
| 'TextBuilder -> ToOwnedText'          | 64       | 4     | Mixed  |    45.26 ns |  1.711 ns | 0.094 ns |  0.52 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 64       | 4     | Mixed  |   309.66 ns |  5.755 ns | 0.315 ns |  3.58 |    0.02 | 0.0992 |      - |     832 B |        0.62 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 64       | 16    | Ascii  |   238.95 ns |  6.692 ns | 0.367 ns |  1.00 |    0.00 | 0.5441 | 0.0048 |    4552 B |        1.00 |
| 'TextBuilder -> ToText'               | 64       | 16    | Ascii  |   161.68 ns | 16.606 ns | 0.910 ns |  0.68 |    0.00 | 0.1252 | 0.0005 |    1048 B |        0.23 |
| 'TextBuilder -> ToOwnedText'          | 64       | 16    | Ascii  |   124.70 ns | 14.259 ns | 0.782 ns |  0.52 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 64       | 16    | Ascii  |   297.83 ns | 24.803 ns | 1.360 ns |  1.25 |    0.01 | 0.1249 | 0.0005 |    1048 B |        0.23 |
|                                       |          |       |        |             |           |          |       |         |        |        |           |             |
| 'StringBuilder -> ToString'           | 64       | 16    | Mixed  |   233.99 ns | 19.634 ns | 1.076 ns |  1.00 |    0.01 | 0.5441 | 0.0050 |    4552 B |        1.00 |
| 'TextBuilder -> ToText'               | 64       | 16    | Mixed  |   199.68 ns | 19.114 ns | 1.048 ns |  0.85 |    0.01 | 0.1721 | 0.0007 |    1440 B |        0.32 |
| 'TextBuilder -> ToOwnedText'          | 64       | 16    | Mixed  |   144.49 ns | 11.782 ns | 0.646 ns |  0.62 |    0.00 |      - |      - |         - |        0.00 |
| 'TextBuilder cross-enc UTF-16->UTF-8' | 64       | 16    | Mixed  | 1,019.51 ns | 17.183 ns | 0.942 ns |  4.36 |    0.02 | 0.2785 |      - |    2336 B |        0.51 |

## LinkedTextBenchmarks

| Method                      | PartSize | Parts | Locale | Mean        | Error      | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------------- |--------- |------ |------- |------------:|-----------:|----------:|------:|--------:|-------:|----------:|------------:|
| string.Concat               | 8        | 4     | Ascii  |    11.03 ns |   0.790 ns |  0.043 ns |  1.00 |    0.00 | 0.0105 |      88 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 8        | 4     | Ascii  |    20.92 ns |   4.083 ns |  0.224 ns |  1.90 |    0.02 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 8        | 4     | Ascii  |   142.45 ns |   2.133 ns |  0.117 ns | 12.91 |    0.04 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 8        | 4     | Mixed  |    10.96 ns |   1.325 ns |  0.073 ns |  1.00 |    0.01 | 0.0105 |      88 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 8        | 4     | Mixed  |    20.71 ns |   1.229 ns |  0.067 ns |  1.89 |    0.01 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 8        | 4     | Mixed  |   148.53 ns |   4.674 ns |  0.256 ns | 13.56 |    0.08 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 8        | 16    | Ascii  |    37.46 ns |   0.796 ns |  0.044 ns |  1.00 |    0.00 | 0.0334 |     280 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 8        | 16    | Ascii  |    59.27 ns |  13.776 ns |  0.755 ns |  1.58 |    0.02 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 8        | 16    | Ascii  |   531.90 ns |  11.518 ns |  0.631 ns | 14.20 |    0.02 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 8        | 16    | Mixed  |    37.25 ns |   2.225 ns |  0.122 ns |  1.00 |    0.00 | 0.0334 |     280 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 8        | 16    | Mixed  |    58.80 ns |   3.189 ns |  0.175 ns |  1.58 |    0.01 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 8        | 16    | Mixed  |   567.94 ns |  41.604 ns |  2.280 ns | 15.25 |    0.07 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 64       | 4     | Ascii  |    27.72 ns |  12.015 ns |  0.659 ns |  1.00 |    0.03 | 0.0641 |     536 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 64       | 4     | Ascii  |    20.88 ns |   2.345 ns |  0.129 ns |  0.75 |    0.02 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 64       | 4     | Ascii  |   772.31 ns |  90.846 ns |  4.980 ns | 27.87 |    0.59 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 64       | 4     | Mixed  |    26.44 ns |   2.891 ns |  0.158 ns |  1.00 |    0.01 | 0.0641 |     536 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 64       | 4     | Mixed  |    20.25 ns |   0.710 ns |  0.039 ns |  0.77 |    0.00 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 64       | 4     | Mixed  | 1,246.57 ns | 130.011 ns |  7.126 ns | 47.15 |    0.34 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 64       | 16    | Ascii  |   102.88 ns |  28.059 ns |  1.538 ns |  1.00 |    0.02 | 0.2476 |    2072 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 64       | 16    | Ascii  |    58.45 ns |   1.132 ns |  0.062 ns |  0.57 |    0.01 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 64       | 16    | Ascii  | 4,360.42 ns | 189.822 ns | 10.405 ns | 42.39 |    0.55 |      - |         - |        0.00 |
|                             |          |       |        |             |            |           |       |         |        |           |             |
| string.Concat               | 64       | 16    | Mixed  |   100.86 ns |   6.536 ns |  0.358 ns |  1.00 |    0.00 | 0.2476 |    2072 B |        1.00 |
| OwnedLinkedTextUtf8.Create  | 64       | 16    | Mixed  |    59.36 ns |   3.189 ns |  0.175 ns |  0.59 |    0.00 |      - |         - |        0.00 |
| OwnedLinkedTextUtf16.Create | 64       | 16    | Mixed  | 5,068.43 ns | 377.242 ns | 20.678 ns | 50.25 |    0.24 |      - |         - |        0.00 |

## JsonSerializationBenchmarks

| Method                      | Categories  | N    | Locale | Mean        | Error       | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------------- |------------ |----- |------- |------------:|------------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| 'Deserialize to string'     | Deserialize | 256  | Ascii  |    243.5 ns |    61.46 ns |   3.37 ns |  1.00 |    0.02 | 0.1173 |      - |     984 B |        1.00 |
| 'Deserialize to Text'       | Deserialize | 256  | Ascii  |  1,003.6 ns |    38.67 ns |   2.12 ns |  4.12 |    0.05 | 0.0877 |      - |     744 B |        0.76 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Deserialize to string'     | Deserialize | 256  | Mixed  |  1,712.2 ns |    64.75 ns |   3.55 ns |  1.00 |    0.00 | 0.1163 |      - |     984 B |        1.00 |
| 'Deserialize to Text'       | Deserialize | 256  | Mixed  |  2,304.4 ns |    79.64 ns |   4.37 ns |  1.35 |    0.00 | 0.1068 |      - |     904 B |        0.92 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Deserialize to string'     | Deserialize | 4096 | Ascii  |    999.5 ns |    98.25 ns |   5.39 ns |  1.00 |    0.01 | 1.4362 | 0.0610 |   12024 B |        1.00 |
| 'Deserialize to Text'       | Deserialize | 4096 | Ascii  | 12,682.4 ns | 1,362.59 ns |  74.69 ns | 12.69 |    0.09 | 0.7477 | 0.0153 |    6264 B |        0.52 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Deserialize to string'     | Deserialize | 4096 | Mixed  | 24,228.7 ns |   884.08 ns |  48.46 ns |  1.00 |    0.00 | 1.4343 | 0.0610 |   12024 B |        1.00 |
| 'Deserialize to Text'       | Deserialize | 4096 | Mixed  | 36,072.0 ns | 3,257.25 ns | 178.54 ns |  1.49 |    0.01 | 0.9766 |      - |    8680 B |        0.72 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Round trip: string'        | RoundTrip   | 256  | Ascii  |    372.5 ns |    28.93 ns |   1.59 ns |  1.00 |    0.01 | 0.1702 | 0.0005 |    1424 B |        1.00 |
| 'Round trip: Text'          | RoundTrip   | 256  | Ascii  |  1,126.7 ns |   273.31 ns |  14.98 ns |  3.02 |    0.04 | 0.1411 |      - |    1184 B |        0.83 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Round trip: string'        | RoundTrip   | 256  | Mixed  |  2,624.1 ns |   202.14 ns |  11.08 ns |  1.00 |    0.01 | 0.2289 |      - |    2024 B |        1.00 |
| 'Round trip: Text'          | RoundTrip   | 256  | Mixed  |  3,470.2 ns |   172.08 ns |   9.43 ns |  1.32 |    0.01 | 0.2289 |      - |    1944 B |        0.96 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Round trip: string'        | RoundTrip   | 4096 | Ascii  |  1,657.3 ns |   103.39 ns |   5.67 ns |  1.00 |    0.00 | 2.1477 | 0.0877 |   17984 B |        1.00 |
| 'Round trip: Text'          | RoundTrip   | 4096 | Ascii  | 13,255.8 ns | 1,187.08 ns |  65.07 ns |  8.00 |    0.04 | 1.4496 | 0.0305 |   12224 B |        0.68 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Round trip: string'        | RoundTrip   | 4096 | Mixed  | 35,616.1 ns | 3,552.03 ns | 194.70 ns |  1.00 |    0.01 | 3.2959 | 0.1221 |   27624 B |        1.00 |
| 'Round trip: Text'          | RoundTrip   | 4096 | Mixed  | 49,808.8 ns | 1,934.08 ns | 106.01 ns |  1.40 |    0.01 | 2.8687 | 0.0610 |   24280 B |        0.88 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Serialize string to bytes' | Serialize   | 256  | Ascii  |    126.5 ns |    15.54 ns |   0.85 ns |  1.00 |    0.01 | 0.0525 |      - |     440 B |        1.00 |
| 'Serialize Text to bytes'   | Serialize   | 256  | Ascii  |    106.6 ns |     5.86 ns |   0.32 ns |  0.84 |    0.01 | 0.0526 |      - |     440 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 256  | Ascii  |    103.7 ns |     4.68 ns |   0.26 ns |  0.82 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Serialize string to bytes' | Serialize   | 256  | Mixed  |    790.2 ns |    92.06 ns |   5.05 ns |  1.00 |    0.01 | 0.1240 |      - |    1040 B |        1.00 |
| 'Serialize Text to bytes'   | Serialize   | 256  | Mixed  |    910.3 ns |   108.80 ns |   5.96 ns |  1.15 |    0.01 | 0.1240 |      - |    1040 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 256  | Mixed  |    928.8 ns |    42.23 ns |   2.31 ns |  1.18 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Serialize string to bytes' | Serialize   | 4096 | Ascii  |    679.7 ns |    19.67 ns |   1.08 ns |  1.00 |    0.00 | 0.7114 |      - |    5960 B |        1.00 |
| 'Serialize Text to bytes'   | Serialize   | 4096 | Ascii  |    543.8 ns |    47.27 ns |   2.59 ns |  0.80 |    0.00 | 0.7114 |      - |    5960 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 4096 | Ascii  |    543.4 ns |    74.50 ns |   4.08 ns |  0.80 |    0.01 |      - |      - |         - |        0.00 |
|                             |             |      |        |             |             |           |       |         |        |        |           |             |
| 'Serialize string to bytes' | Serialize   | 4096 | Mixed  | 10,230.8 ns |   344.45 ns |  18.88 ns |  1.00 |    0.00 | 1.8616 |      - |   15600 B |        1.00 |
| 'Serialize Text to bytes'   | Serialize   | 4096 | Mixed  | 13,177.9 ns | 2,878.41 ns | 157.78 ns |  1.29 |    0.01 | 1.8616 |      - |   15600 B |        1.00 |
| SerializeToUtf8OwnedText    | Serialize   | 4096 | Mixed  | 12,931.8 ns | 1,204.49 ns |  66.02 ns |  1.26 |    0.01 |      - |      - |         - |        0.00 |

## HttpPipelineBenchmarks

| Method                 | N    | Locale | Mean        | Error       | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|----------------------- |----- |------- |------------:|------------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| 'String pipeline'      | 256  | Ascii  |    580.0 ns |   652.93 ns |  35.79 ns |  1.00 |    0.07 | 0.4263 | 0.0019 |    3568 B |        1.00 |
| 'Glot pipeline'        | 256  | Ascii  |  1,868.6 ns |   570.02 ns |  31.24 ns |  3.23 |    0.17 | 0.1965 |      - |    1656 B |        0.46 |
| 'Glot pooled pipeline' | 256  | Ascii  |  1,743.0 ns |   173.49 ns |   9.51 ns |  3.01 |    0.16 | 0.1163 |      - |     984 B |        0.28 |
|                        |      |        |             |             |           |       |         |        |        |           |             |
| 'String pipeline'      | 256  | Mixed  |  3,973.3 ns |   207.65 ns |  11.38 ns |  1.00 |    0.00 | 0.5341 |      - |    4488 B |        1.00 |
| 'Glot pipeline'        | 256  | Mixed  |  6,004.4 ns |   868.18 ns |  47.59 ns |  1.51 |    0.01 | 0.3281 |      - |    2808 B |        0.63 |
| 'Glot pooled pipeline' | 256  | Mixed  |  5,882.5 ns |   321.24 ns |  17.61 ns |  1.48 |    0.01 | 0.1450 |      - |    1216 B |        0.27 |
|                        |      |        |             |             |           |       |         |        |        |           |             |
| 'String pipeline'      | 4096 | Ascii  |  4,139.4 ns |   638.80 ns |  35.02 ns |  1.00 |    0.01 | 5.9509 | 0.3815 |   49944 B |        1.00 |
| 'Glot pipeline'        | 4096 | Ascii  | 21,263.5 ns |   716.18 ns |  39.26 ns |  5.14 |    0.04 | 2.2583 | 0.0610 |   18936 B |        0.38 |
| 'Glot pooled pipeline' | 4096 | Ascii  | 21,285.8 ns |    94.27 ns |   5.17 ns |  5.14 |    0.04 | 1.1292 | 0.0305 |    9624 B |        0.19 |
|                        |      |        |             |             |           |       |         |        |        |           |             |
| 'String pipeline'      | 4096 | Mixed  | 55,810.2 ns | 9,304.49 ns | 510.01 ns |  1.00 |    0.01 | 7.6904 | 0.4272 |   64584 B |        1.00 |
| 'Glot pipeline'        | 4096 | Mixed  | 76,381.1 ns | 6,082.22 ns | 333.39 ns |  1.37 |    0.01 | 4.3945 | 0.1221 |   37240 B |        0.58 |
| 'Glot pooled pipeline' | 4096 | Mixed  | 74,967.4 ns | 6,368.07 ns | 349.06 ns |  1.34 |    0.01 | 1.5869 |      - |   13288 B |        0.21 |

