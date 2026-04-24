# Glot Performance Report (Detailed)

**Date:** 2026-04-23
**Hardware:** Apple M4 Max, 16 physical cores, .NET 10.0.6, BenchmarkDotNet v0.15.8 (Arm64 RyuJIT)

---

## ByteIndexOfUtf16Benchmarks

**File:** `Glot.Benchmarks.ByteIndexOfUtf16Benchmarks-report-github.md`
**Measures:** Byte-offset substring search with a UTF-16 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.IndexOf`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.IndexOf | 2.55 ns | 1.00 |
| 64 Ascii | Text.ByteIndexOf UTF-16 (same) | 3.35 ns | 1.31 |
| 64 Ascii | Text.ByteIndexOf UTF-8 (cross) | 14.45 ns | 5.68 |
| 64 Ascii | Text.ByteIndexOf UTF-32 (cross) | 15.44 ns | 6.07 |
| 64 Cjk | Text.ByteIndexOf UTF-16 (same) | 2.94 ns | 2.30 |
| 64 Cjk | Text.ByteIndexOf UTF-8 (cross) | 23.64 ns | 18.49 |
| 64 Cjk | Text.ByteIndexOf UTF-32 (cross) | 32.18 ns | 25.17 |
| 4096 Cjk miss | string.IndexOf miss | 257 ns | 1.00 |
| 4096 Cjk miss | Text.ByteIndexOf UTF-16 miss (same) | 263 ns | 1.03 |
| 65K Ascii miss | string.IndexOf miss | 4,045 ns | 1.00 |
| 65K Ascii miss | Text.ByteIndexOf UTF-16 miss (same) | 7,211 ns | 1.78 |
| 65K Cjk miss | Text.ByteIndexOf UTF-16 miss (same) | 4,067 ns | 0.98 |
| 65K Cjk miss | Text.ByteIndexOf UTF-8 miss (cross) | 4,204 ns | 1.01 |

### Analysis
- Zero allocations across the board.
- Same-encoding UTF-16: ~0.8–1.7 ns wrapper overhead on hits (1.3× string at Ascii, 2.3× at Cjk where the string baseline is ~1.3 ns), parity on most large misses. Lone outlier is 65K Ascii miss at 1.78× — `string.IndexOf` has a specialized vectorized path Text doesn't hit here.
- Cross-encoding (UTF-8 or UTF-32 needle): ~15–35 ns fixed per-call cost from the rune-by-rune scan. Looks bad as a ratio at small N, but on large miss paths the SIMD scan dominates and Text converges to parity with `string.IndexOf` (65K Cjk miss: 4,204 ns Text vs 4,166 ns string).

### Perf Fix
Not warranted. Same-encoding is already a thin wrapper at parity on large misses; cross-encoding has a ~15–35 ns setup cost that amortizes to parity by large N. The absolute cost is sub-microsecond and users who care can transcode the needle to the haystack encoding before calling.

---

## ByteIndexOfUtf32Benchmarks

**File:** `Glot.Benchmarks.ByteIndexOfUtf32Benchmarks-report-github.md`
**Measures:** Byte-offset substring search with a UTF-32 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.IndexOf` (UTF-16; no native UTF-32 peer in .NET).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.IndexOf | 2.67 ns | 1.00 |
| 64 Ascii | Text.ByteIndexOf UTF-32 (same) | 4.98 ns | 1.87 |
| 64 Ascii | Text.ByteIndexOf UTF-8 (cross) | 19.73 ns | 7.40 |
| 64 Ascii | Text.ByteIndexOf UTF-16 (cross) | 18.83 ns | 7.07 |
| 64 Cjk | Text.ByteIndexOf UTF-32 (same) | 3.17 ns | 2.50 |
| 64 Cjk | Text.ByteIndexOf UTF-8 (cross) | 36.04 ns | 28.43 |
| 64 Cjk | Text.ByteIndexOf UTF-16 (cross) | 37.22 ns | 29.36 |
| 65K Ascii miss | string.IndexOf miss | 4,107 ns | 1.00 |
| 65K Ascii miss | Text.ByteIndexOf UTF-32 miss (same) | 11,407 ns | 2.78 |
| 65K Ascii miss | Text.ByteIndexOf UTF-16 miss (cross) | 10,779 ns | 2.62 |
| 65K Cjk miss | Text.ByteIndexOf UTF-32 miss (same) | 8,139 ns | 1.98 |
| 65K Emoji miss | Text.ByteIndexOf UTF-32 miss (same) | 4,180 ns | 1.00 |

### Analysis
- Zero allocations except a 1 B blip on all three Text variants at 65K Ascii miss — BDN per-op rounding artifact, not a real heap alloc.
- Same-encoding UTF-32 sits at ~1.9–2.8× `string.IndexOf` on miss. This is structural: UTF-32 stores every rune in 4 bytes, so a 65K-char haystack is 256K bytes to scan vs `string`'s 128K (Cjk) or 2K (Ascii). Can't be fixed without changing the representation.
- Cross-encoding (UTF-8/UTF-16 needle) adds ~15–35 ns fixed setup cost per call. Looks bad at small N (7–29×) but at 65K Ascii miss cross-encoding actually edges out same-encoding (10,779 ns vs 11,407 ns) because both are dominated by the UTF-32 byte scan.

### Perf Fix
Not warranted. The 2× same-encoding overhead is the cost of UTF-32 — if a user holds data as UTF-32, they're opting into constant-width access and paying 2× the bytes-to-scan. Cross-encoding adds a small fixed setup cost that disappears at scale. No cheap structural win available here.

---

## ByteIndexOfUtf8Benchmarks

**File:** `Glot.Benchmarks.ByteIndexOfUtf8Benchmarks-report-github.md`
**Measures:** Byte-offset substring search with a UTF-8 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.IndexOf`. Direct peers: `Span<byte>.IndexOf`, `U8String.IndexOf`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.IndexOf | 3.94 ns | 1.00 |
| 64 Ascii | Span.IndexOf UTF-8 | 3.33 ns | 0.84 |
| 64 Ascii | U8String.IndexOf | 3.52 ns | 0.89 |
| 64 Ascii | Text.ByteIndexOf UTF-8 (same) | 4.43 ns | 1.12 |
| 64 Ascii | Text.ByteIndexOf UTF-16 (cross) | 16.88 ns | 4.28 |
| 64 Ascii | Text.ByteIndexOf UTF-32 (cross) | 17.80 ns | 4.52 |
| 64 Cjk | Text.ByteIndexOf UTF-8 (same) | 4.29 ns | 1.67 |
| 64 Cjk | Text.ByteIndexOf UTF-16 (cross) | 36.55 ns | 14.28 |
| 65K Ascii miss | Span.IndexOf UTF-8 miss | 2,098 ns | 524.5 |
| 65K Ascii miss | U8String.IndexOf miss | 2,101 ns | 525.2 |
| 65K Ascii miss | Text.ByteIndexOf UTF-8 miss (same) | 2,100 ns | 525.0 |
| 65K Ascii miss | Text.ByteIndexOf UTF-16 miss (cross) | 2,111 ns | 527.7 |
| 65K Cjk miss | U8String.IndexOf miss | 6,279 ns | 2,430 |
| 65K Cjk miss | Text.ByteIndexOf UTF-8 miss (same) | 6,284 ns | 2,433 |

### Analysis
- Zero allocations across the board.
- Same-encoding Text sits ~1 ns behind `Span.IndexOf` and `U8String.IndexOf` on small hits (pure wrapper overhead) and at parity with both on every large-miss scenario — within measurement noise. This is as close to the UTF-8-native reference as a typed wrapper can realistically get.
- The `string.IndexOf` baseline is sometimes faster than Text on Cjk/Emoji miss (4.2 µs vs 6.3 µs) because UTF-16 stores those scripts in 2 bytes vs UTF-8's 3 — less data to scan. That's a representation choice, not a Text bug.
- Cross-encoding (UTF-16/UTF-32 needle) costs ~15–35 ns fixed per call at small N; at 65K Ascii miss cross-encoding lands within 0.5% of same-encoding (2,111 vs 2,100 ns) and beats `string.IndexOf` 2×.

### Perf Fix
Not warranted. Text.ByteIndexOf UTF-8 is at parity with `U8String.IndexOf` on real scan workloads — can't meaningfully improve. Cross-encoding's small-N setup cost is the same story as the UTF-16/UTF-32 haystack classes; converges at scale.

---

## CompareToUtf16Benchmarks

**File:** `Glot.Benchmarks.CompareToUtf16Benchmarks-report-github.md`
**Measures:** Ordinal compare of two near-equal texts (Diff mutates the last char of A). UTF-16 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Compare`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.Compare | 1.98 ns | 1.00 | - |
| 8 Ascii | Text.CompareTo UTF-16 (same) | 6.30 ns | 3.19 | - |
| 8 Ascii | Text.CompareTo UTF-8 (cross stream) | 18.64 ns | 9.44 | - |
| 8 Ascii | Text.CompareTo UTF-32 (cross full) | 29.97 ns | 15.17 | - |
| 256 Ascii | Text.CompareTo UTF-16 (same) | 14.78 ns | 1.11 | - |
| 256 Cjk | Text.CompareTo UTF-16 (same) | 14.89 ns | 1.11 | - |
| 65K Ascii | string.Compare | 3,391 ns | 1.00 | - |
| 65K Ascii | Text.CompareTo UTF-16 (same) | 2,434 ns | **0.72** | - |
| 65K Cjk | Text.CompareTo UTF-16 (same) | 2,483 ns | 0.77 | - |
| 65K Ascii | Text.CompareTo UTF-8 (cross stream) | 4,080 ns | 1.20 | - |
| 65K Cjk | Text.CompareTo UTF-8 (cross stream) | 48,094 ns | 14.96 | **2 B** |
| 65K Emoji | Text.CompareTo UTF-8 (cross stream) | 68,436 ns | 20.45 | **3 B** |
| 65K Ascii | Text.CompareTo UTF-32 (cross full) | 153,322 ns | 45.21 | **6 B** |
| 65K Cjk | Text.CompareTo UTF-32 (cross full) | 237,124 ns | 73.76 | **12 B** |
| 65K Emoji | Text.CompareTo UTF-32 (cross full) | 173,255 ns | 51.76 | **9 B** |
| 65K Mixed | Text.CompareTo UTF-32 (cross full) | 193,276 ns | 57.31 | **10 B** |

### Analysis
- Same-encoding UTF-16 beats `string.Compare` at 65K (0.72–0.77×), sits at 1.11× at N=256, ~4 ns fixed overhead at N=8. Excellent.
- Cross-encoding UTF-8 target takes the streaming short-circuit path in `CompareUtf8WithTranscoded`: at parity on Ascii (1.20× at 65K) but 9–20× on non-Ascii because the worst-case walks all 65K chars before the final-char mismatch, and the chunk-transcode dominates. 2–3 B residual allocation per op at 65K.
- Cross-encoding UTF-32 target takes `CompareBothTranscoded`: allocates **two** full UTF-8 transcoded buffers (~192 KB each for 65K Cjk) via `ArrayPool`, compares the whole thing at once, no short-circuit. 45–74× `string.Compare` with 6–12 B alloc per call.
- Correctness side-note: `Text.CompareTo` takes the `Bytes.SequenceCompareTo` shortcut for any same encoding including UTF-16, where byte order does not preserve rune order across surrogate pairs. Flagging for a separate correctness review; not in scope here.

### Perf Fix

**Problem:** `CompareBothTranscoded` in `TextSpan.Equality.cs:227` allocates two full UTF-8 buffers (up to ~192 KB each at 65K) and compares them all at once — no streaming, no short-circuit. Responsible for the 45–74× UTF-32 ratio and the 6–12 B ArrayPool noise.
**Location:** `src/Glot/TextSpan/TextSpan.Equality.cs:227`
**Proposed fix:** Extend the streaming approach from `CompareUtf8WithTranscoded` to the both-non-UTF-8 case: transcode a chunk of each side to UTF-8 (stackalloc, ~256 B), compare, short-circuit on mismatch, advance. Eliminates the double full-buffer alloc and enables early termination (most compares find a mismatch well before the end).
**Expected impact:** UTF-32 cross drops from 45–74× to ~15–25× (parity with UTF-8 cross path). Kills the 6–12 B ArrayPool overhead.
**Effort:** M

### Perf Fix

**Problem:** Cross-encoding `Text.CompareTo` (UTF-8 or UTF-32 against UTF-16 haystack) rune-decodes both sides and allocates 2–16 B per call at 65K — likely a boxed state holder or rental path in the cross-encoding comparer.
**Location:** `Text.Equality.cs` `CompareTo` overloads and the `TextSpan.CompareTo` implementations in `src/Glot/TextSpan/TextSpan.Equality.cs`.
**Proposed fix:** Transcode the shorter side into a stackalloc in the haystack's encoding and call `Bytes.SequenceCompareTo`. Eliminate the per-call heap allocation in the rune-decode path.
**Expected impact:** Drop the 65K UTF-32 ratio from 73× to ~10–15×; eliminate the 2–16 B alloc.
**Effort:** M

---

## CompareToUtf32Benchmarks

**File:** `Glot.Benchmarks.CompareToUtf32Benchmarks-report-github.md`
**Measures:** Ordinal compare of two near-equal texts (Diff mutates the last char of A). UTF-32 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Compare` (UTF-16; no native UTF-32 peer in .NET — reference only).

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.Compare | 2.03 ns | 1.00 | - |
| 8 Ascii | Text.CompareTo UTF-32 (same) | 5.62 ns | 2.78 | - |
| 8 Ascii | Text.CompareTo UTF-16 (cross full) | 30.08 ns | 14.87 | - |
| 8 Ascii | Text.CompareTo UTF-8 (cross stream) | 55.43 ns | 27.40 | - |
| 256 Ascii | Text.CompareTo UTF-32 (same) | 22.29 ns | 1.67 | - |
| 256 Ascii | Text.CompareTo UTF-16 (cross full) | 660.97 ns | 49.66 | - |
| 256 Ascii | Text.CompareTo UTF-8 (cross stream) | 1,290 ns | 96.92 | - |
| 65K Ascii | string.Compare | 3,377 ns | 1.00 | - |
| 65K Ascii | Text.CompareTo UTF-32 (same) | 4,614 ns | 1.37 | - |
| 65K Ascii | Text.CompareTo UTF-16 (cross full) | 154,003 ns | 45.60 | **8 B** |
| 65K Ascii | Text.CompareTo UTF-8 (cross stream) | 324,630 ns | 96.13 | **16 B** |
| 65K Cjk | Text.CompareTo UTF-32 (same) | 4,738 ns | 1.42 | - |
| 65K Cjk | Text.CompareTo UTF-16 (cross full) | 232,954 ns | 69.78 | **12 B** |
| 65K Cjk | Text.CompareTo UTF-8 (cross stream) | 387,616 ns | 116.11 | **19 B** |
| 65K Emoji | Text.CompareTo UTF-32 (same) | 2,413 ns | 0.77 | - |
| 65K Mixed | Text.CompareTo UTF-16 (cross full) | 196,644 ns | 59.85 | **10 B** |

### Analysis
- Same-encoding UTF-32 CompareTo runs 1.37–1.67× `string.Compare` at ≥256 and 0.77× on 65K Emoji. Near parity despite scanning 4× the bytes per rune — the compare short-circuits on the 4-byte int mismatch at the very last rune and exits almost immediately.
- `CompareTo` on a UTF-32 receiver always takes `CompareToCrossEncoding` (the same-UTF-8 byte-ordering shortcut only applies when both sides are UTF-8). For same-encoding UTF-32 this still dispatches into `CompareBothTranscoded` — i.e. the worst path — yet comes in at 1.4× because the single-char mutation forces a full walk anyway and the stackalloc buffers cover it.
- Cross-encoding UTF-8 target takes the streaming `CompareUtf8WithTranscoded` path: 96–116× `string.Compare` at 65K with 10–19 B alloc. The stream transcodes 512 B chunks of UTF-32 → UTF-8 and short-circuits, but the "last-char-diff" workload makes it walk the entire input before finding the mismatch, so the 4:1 expansion from UTF-32 runes to UTF-8 bytes dominates.
- Cross-encoding UTF-16 target takes `CompareBothTranscoded`: full UTF-16 + UTF-32 → UTF-8 transcode with `ArrayPool` rentals (6–12 B overhead). 45–70× at 65K.

### Perf Fix

**Problem:** `CompareBothTranscoded` in `TextSpan.Equality.cs:227` allocates two full UTF-8 buffers via `ArrayPool` and compares them at once, no streaming, no short-circuit. Responsible for the 45–70× UTF-16-cross ratio and the 8–12 B ArrayPool overhead on this benchmark.
**Location:** `src/Glot/TextSpan/TextSpan.Equality.cs:227`
**Proposed fix:** Extend the `CompareUtf8WithTranscoded` streaming pattern to both-non-UTF-8: transcode `a` and `b` in matched chunks (stackalloc), compare per chunk, short-circuit. Eliminates the double full-buffer alloc; enables early termination.
**Expected impact:** 65K UTF-16-cross drops from 45–70× to ~20–30× (parity with UTF-8-cross streaming path); kills the 8–12 B ArrayPool allocs.
**Effort:** M

---

## CompareToUtf8Benchmarks

**File:** `Glot.Benchmarks.CompareToUtf8Benchmarks-report-github.md`
**Measures:** Ordinal compare of two near-equal texts (Diff mutates the last char). UTF-8 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Compare`. Direct peers: `Span<byte>.SequenceCompareTo`, `U8String.CompareTo`.

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.Compare | 3.45 ns | 1.00 | - |
| 8 Ascii | U8String.CompareTo | 3.42 ns | 0.99 | - |
| 8 Ascii | Text.CompareTo UTF-8 (same) | 3.87 ns | 1.12 | - |
| 8 Ascii | Text.CompareTo UTF-16 (cross stream) | 20.25 ns | 5.87 | - |
| 8 Ascii | Text.CompareTo UTF-32 (cross stream) | 54.32 ns | 15.76 | - |
| 256 Mixed | Text.CompareTo UTF-8 (same) | 11.85 ns | 0.82 | - |
| 256 Mixed | Text.CompareTo UTF-16 (cross stream) | 139.55 ns | 9.63 | - |
| 256 Mixed | Text.CompareTo UTF-32 (cross stream) | 1,254 ns | 86.51 | - |
| 65K Ascii | string.Compare | 3,491 ns | 1.00 | - |
| 65K Ascii | U8String.CompareTo | 1,126 ns | 0.32 | - |
| 65K Ascii | Text.CompareTo UTF-8 (same) | 1,141 ns | 0.33 | - |
| 65K Ascii | Text.CompareTo UTF-16 (cross stream) | 3,471 ns | 0.99 | - |
| 65K Ascii | Text.CompareTo UTF-32 (cross stream) | 299,571 ns | 85.81 | **14 B** |
| 65K Cjk | Text.CompareTo UTF-8 (same) | 3,646 ns | 1.08 | - |
| 65K Cjk | Text.CompareTo UTF-16 (cross stream) | 48,807 ns | 14.46 | **2 B** |
| 65K Cjk | Text.CompareTo UTF-32 (cross stream) | 334,413 ns | 99.06 | **15 B** |
| 65K Emoji | Text.CompareTo UTF-8 (same) | 2,289 ns | 0.68 | - |
| 65K Emoji | Text.CompareTo UTF-32 (cross stream) | 173,858 ns | 51.72 | **8 B** |

### Analysis
- Same-encoding Text UTF-8 CompareTo tracks `U8String.CompareTo` and raw `Span<byte>.SequenceCompareTo` within 15 ns at every size — it's the identical `Bytes.SequenceCompareTo` shortcut in `TextSpan.Equality.cs:157`. On 65K Emoji it beats all three peers (0.68×) because the very last 4-byte rune diff short-circuits near the start of the vectorized scan.
- Cross-encoding UTF-16 target takes streaming `CompareUtf8WithTranscoded`: at parity on Ascii (0.99× at 65K because the UTF-16 data transcodes 1:1 back to UTF-8 and the vectorized compare dominates), but 14× on Cjk and 20× on Emoji where the multibyte transcoding forces a full walk before the last-char mismatch. 2–3 B residual alloc at 65K.
- Cross-encoding UTF-32 target takes `CompareBothTranscoded`: full transcode + ArrayPool rentals for both sides. 85–99× at 65K with 8–15 B alloc per call — the worst cross-path in the suite.
- 256 Mixed UTF-32 cross at 1,254 ns (86×) vs 140 ns for UTF-16 cross (9.6×) shows the both-transcode penalty kicking in even at small N.

### Perf Fix
Same root cause as CompareToUtf16Benchmarks / CompareToUtf32Benchmarks: `CompareBothTranscoded` in `TextSpan.Equality.cs:227` is the bottleneck for the UTF-32 cross path. Streaming rewrite there will drop the 85–99× ratio and eliminate the 8–15 B ArrayPool allocs. See CompareToUtf16Benchmarks for the full fix spec.

---

## ContainsUtf16Benchmarks

**File:** `Glot.Benchmarks.ContainsUtf16Benchmarks-report-github.md`
**Measures:** Substring membership test with a UTF-16 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Contains`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.Contains | 2.67 ns | 1.00 |
| 64 Ascii | Span.Contains UTF-8 | 2.14 ns | 0.80 |
| 64 Ascii | Text.Contains UTF-16 (same) | 3.71 ns | 1.39 |
| 64 Ascii | Text.Contains UTF-8 (cross) | 15.35 ns | 5.75 |
| 64 Ascii | Text.Contains UTF-32 (cross) | 16.71 ns | 6.26 |
| 64 Cjk | Text.Contains UTF-16 (same) | 2.97 ns | 2.31 |
| 64 Cjk | Text.Contains UTF-8 (cross) | 25.39 ns | 19.78 |
| 65K Ascii miss | string.Contains miss | 4,034 ns | 1.00 |
| 65K Ascii miss | Text.Contains UTF-16 miss (same) | 7,086 ns | 1.76 |
| 65K Ascii miss | Text.Contains UTF-8 miss (cross) | 4,079 ns | 1.01 |
| 65K Ascii miss | Text.Contains UTF-32 miss (cross) | 4,131 ns | 1.02 |
| 65K Cjk miss | string.Contains miss | 4,066 ns | 1.00 |
| 65K Cjk miss | Text.Contains UTF-16 miss (same) | 4,044 ns | 0.99 |
| 65K Cjk miss | Text.Contains UTF-8 miss (cross) | 4,090 ns | 1.01 |

### Analysis
- Zero allocations across the board.
- Same-encoding `Text.Contains UTF-16` at 65K Cjk/Emoji/Mixed misses is at parity with `string.Contains` (0.99–1.00×). The 1.76× at 65K Ascii miss repeats the ByteIndexOf pattern — `string.Contains` has a specialized vectorized ASCII-single-char path that Text's pure `Bytes.IndexOf` doesn't hit.
- Cross-encoding miss paths (UTF-8 or UTF-32 needle) converge to baseline at 65K (1.01–1.02×) — the ASCII-narrowing fast path in `TextSpan.Search.cs:236` (`IndexOfCrossEncoding`) converts the needle to the haystack encoding via stackalloc and uses the same SIMD `IndexOf`.
- Small-N cross-encoding cost is the familiar ~15–25 ns setup (TryConvertAscii + transcode dispatch); amortizes as N grows.

### Perf Fix
Not warranted. Contains delegates to `Bytes.IndexOf` same-encoding and to the already-optimized `IndexOfCrossEncoding` with ASCII-narrow fast path; 65K miss paths are at parity with `string.Contains`. The sub-2× small-N cross ratios are setup cost that disappears at scale.

---

## ContainsUtf32Benchmarks

**File:** `Glot.Benchmarks.ContainsUtf32Benchmarks-report-github.md`
**Measures:** Substring membership with a UTF-32 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Contains` (UTF-16; no native UTF-32 peer in .NET — reference only).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.Contains | 2.68 ns | 1.00 |
| 64 Ascii | Text.Contains UTF-32 (same) | 4.86 ns | 1.81 |
| 64 Ascii | Text.Contains UTF-8 (cross) | 19.19 ns | 7.14 |
| 64 Ascii | Text.Contains UTF-16 (cross) | 18.49 ns | 6.88 |
| 64 Cjk | Text.Contains UTF-32 (same) | 3.20 ns | 2.47 |
| 64 Cjk | Text.Contains UTF-16 (cross) | 37.30 ns | 28.80 |
| 64 Cjk | Text.Contains UTF-8 (cross) | 35.29 ns | 27.25 |
| 65K Ascii miss | string.Contains miss | 4,100 ns | 1.00 |
| 65K Ascii miss | Text.Contains UTF-32 miss (same) | 11,344 ns | 2.77 |
| 65K Ascii miss | Text.Contains UTF-16 miss (cross) | 10,852 ns | 2.65 |
| 65K Ascii miss | Text.Contains UTF-8 miss (cross) | 10,971 ns | 2.68 |
| 65K Cjk miss | Text.Contains UTF-32 miss (same) | 8,157 ns | 1.99 |
| 65K Emoji miss | Text.Contains UTF-32 miss (same) | 4,080 ns | 1.05 |

### Analysis
- Zero allocations except a 1 B blip on all three cross-encoding Text variants at 65K Ascii miss — BDN per-op rounding, not a real heap alloc.
- Same-encoding UTF-32 sits at 2× the UTF-16 `string.Contains` at 65K miss. Structural: the UTF-32 buffer is 4× the bytes of the Ascii string, so the SIMD scan processes more data. Can't be closed without changing the representation.
- Cross-encoding converges to same-encoding at 65K miss (10,852 vs 11,344 ns) — the cross-encoding ASCII-narrow path hits the same 4×-wide UTF-32 scan. Not a regression over same-encoding, just the UTF-32 byte-count tax.
- Small-N (64) cross-encoding ~15–35 ns setup overhead; amortizes by 65K.

### Perf Fix
Not warranted. 2× vs `string.Contains` is the UTF-32 representation tax, not a code issue. Cross-encoding has no additional penalty at scale.

---

## ContainsUtf8Benchmarks

**File:** `Glot.Benchmarks.ContainsUtf8Benchmarks-report-github.md`
**Measures:** Substring membership with a UTF-8 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Contains`. Direct peers: `Span<byte>.IndexOf`, `U8String.Contains`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.Contains | 4.02 ns | 1.00 |
| 64 Ascii | Span.Contains UTF-8 | 3.46 ns | 0.86 |
| 64 Ascii | U8String.Contains | 3.63 ns | 0.90 |
| 64 Ascii | Text.Contains UTF-8 (same) | 4.48 ns | 1.11 |
| 64 Ascii | Text.Contains UTF-16 (cross) | 16.83 ns | 4.16 |
| 64 Cjk | Text.Contains UTF-16 (cross) | 36.44 ns | 14.16 |
| 65K Ascii miss | Span.Contains UTF-8 miss | 2,091 ns | 521.29 |
| 65K Ascii miss | U8String.Contains miss | 2,093 ns | 521.71 |
| 65K Ascii miss | Text.Contains UTF-8 miss (same) | 2,095 ns | 522.25 |
| 65K Ascii miss | Text.Contains UTF-16 miss (cross) | 2,119 ns | 528.10 |
| 65K Cjk miss | U8String.Contains miss | 6,288 ns | 2,438 |
| 65K Cjk miss | Text.Contains UTF-8 miss (same) | 6,349 ns | 2,462 |
| 65K Cjk miss | Text.Contains UTF-16 miss (cross) | 6,353 ns | 2,463 |

### Analysis
- Zero allocations across the board.
- Same-encoding Text sits within 1 ns of `Span.Contains UTF-8` and `U8String.Contains` across every large-miss row — parity with the UTF-8-native reference.
- `string.Contains` looks "faster" on Cjk miss (4.2 µs vs 6.3 µs) because UTF-16 stores CJK in 2 bytes/char vs UTF-8's 3 — less data to scan. Representation, not a Text regression.
- Cross-encoding (UTF-16 or UTF-32 needle) lands within 1% of same-encoding at 65K miss (2,119 vs 2,095 ns); the ASCII-narrow path in `IndexOfCrossEncoding` converts needle bytes up-front so the scan is the same SIMD `Bytes.IndexOf`.

### Perf Fix
Not warranted. Text.Contains UTF-8 matches the UTF-8-native peers on realistic workloads; cross-encoding's small-N setup cost amortizes to parity at scale.

---

## EndsWithUtf16Benchmarks

**File:** `Glot.Benchmarks.EndsWithUtf16Benchmarks-report-github.md`
**Measures:** Suffix match with a UTF-16 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.EndsWith` (largely constant-folded; ratios noisy, prefer absolute times).

### Results (ratios inflated — `string.EndsWith` is 0.5–0.7 ns after JIT)

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.EndsWith | 0.58 ns | 1.00 |
| 64 Ascii | Span.EndsWith UTF-8 | 0.77 ns | 1.33 |
| 64 Ascii | Text.EndsWith UTF-16 (same) | 1.57 ns | 2.70 |
| 64 Ascii | Text.EndsWith UTF-32 (cross) | 7.69 ns | 13.21 |
| 64 Ascii | Text.EndsWith UTF-8 (cross) | 8.18 ns | 14.07 |
| 64 Cjk | Text.EndsWith UTF-16 (same) | 1.57 ns | 3.31 |
| 64 Cjk | Text.EndsWith UTF-8 (cross) | 8.98 ns | 18.99 |
| 64 Cjk | Text.EndsWith UTF-32 (cross) | 7.56 ns | 16.00 |
| 64 Emoji | Text.EndsWith UTF-8 (cross) | 11.69 ns | 16.19 |
| 64 Emoji | Text.EndsWith UTF-16 (same) | 1.83 ns | 2.53 |
| 64 Emoji | Text.EndsWith UTF-32 (cross) | 9.79 ns | 13.56 |

### Analysis
- Zero allocations across the board.
- `string.EndsWith` and `Span.EndsWith` with a compile-time-known suffix fold to 0.5–0.7 ns after JIT — ratios are unreliable, read absolute times.
- Same-encoding `Text.EndsWith UTF-16` is a flat 1.5–1.8 ns regardless of script/size (dispatches straight to `Bytes.EndsWith`). ~1 ns of Text wrapper cost — reasonable for this hot path.
- Cross-encoding Text.EndsWith (UTF-8 or UTF-32 needle) runs 7–12 ns. The path is `TextSpan.Search.cs:149`: a rune-at-a-time backward walk using `Rune.TryDecodeLast` on both sides — no SIMD, no bulk transcoding.

### Perf Fix

**Problem:** Cross-encoding `EndsWith` (`TextSpan.Search.cs:149`) walks both sides rune-by-rune via `Rune.TryDecodeLast`. For a small, known-length needle, transcode-once-and-compare is faster and would hit SIMD `EndsWith`.
**Location:** `src/Glot/TextSpan/TextSpan.Search.cs:149` `EndsWith(TextSpan)` cross-encoding branch.
**Proposed fix:** When the needle fits in a stackalloc budget (e.g. ≤512 B in haystack encoding), transcode the needle into haystack encoding once and call `Bytes.EndsWith`. Retain the streaming walk as fallback for oversize needles.
**Expected impact:** 7–12 ns cross rows drop to ~2–3 ns (same-encoding territory).
**Effort:** M

---

## EndsWithUtf32Benchmarks

**File:** `Glot.Benchmarks.EndsWithUtf32Benchmarks-report-github.md`
**Measures:** Suffix match with a UTF-32 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.EndsWith` (UTF-16; no native UTF-32 peer — reference only, and constant-folded).

### Results (ratios noisy — `string.EndsWith` constant-folded to 0.5 ns)

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.EndsWith | 0.51 ns | 1.00 |
| 64 Ascii | Text.EndsWith UTF-32 (same) | 1.78 ns | 3.48 |
| 64 Ascii | Text.EndsWith UTF-8 (cross) | 6.47 ns | 12.63 |
| 64 Ascii | Text.EndsWith UTF-16 (cross) | 7.39 ns | 14.42 |
| 64 Cjk | Text.EndsWith UTF-32 (same) | 1.76 ns | 3.35 |
| 64 Cjk | Text.EndsWith UTF-8 (cross) | 7.22 ns | 13.80 |
| 64 Cjk | Text.EndsWith UTF-16 (cross) | 7.43 ns | 14.20 |
| 64 Emoji | Text.EndsWith UTF-32 (same) | 1.78 ns | 2.39 |
| 64 Emoji | Text.EndsWith UTF-16 (cross) | 9.95 ns | 13.39 |
| 64 Mixed | Text.EndsWith UTF-32 (same) | 1.99 ns | 2.65 |

### Analysis
- Zero allocations.
- Same-encoding UTF-32 sits flat at 1.76–1.99 ns across locales — just `Bytes.EndsWith` on the 4-byte-per-rune span.
- Cross-encoding lands at 6.5–10 ns, dominated by `Rune.TryDecodeLast` on both sides in `TextSpan.Search.cs:149`.
- UTF-32 is ~2× the same-encoding UTF-16 class (1.5 ns) because `Bytes.EndsWith` scans 4 bytes per rune vs 2. Structural.

### Perf Fix
Same as EndsWithUtf16Benchmarks — transcode-the-needle-once would eliminate the rune-by-rune cross walk.

---

## EndsWithUtf8Benchmarks

**File:** `Glot.Benchmarks.EndsWithUtf8Benchmarks-report-github.md`
**Measures:** Suffix match with a UTF-8 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.EndsWith`. Direct peers: `Span<byte>.EndsWith`, `U8String.EndsWith`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.EndsWith | 1.84 ns | 1.00 |
| 64 Ascii | Span.EndsWith UTF-8 | 2.04 ns | 1.11 |
| 64 Ascii | U8String.EndsWith | 2.05 ns | 1.12 |
| 64 Ascii | Text.EndsWith UTF-8 (same) | 2.99 ns | 1.63 |
| 64 Ascii | Text.EndsWith UTF-16 (cross) | 10.97 ns | 5.98 |
| 64 Ascii | Text.EndsWith UTF-32 (cross) | 8.97 ns | 4.89 |
| 64 Cjk | Text.EndsWith UTF-8 (same) | 3.14 ns | 1.72 |
| 64 Cjk | Text.EndsWith UTF-16 (cross) | 11.34 ns | 6.23 |
| 64 Emoji | Text.EndsWith UTF-16 (cross) | 17.49 ns | 8.66 |
| 64 Emoji | Text.EndsWith UTF-32 (cross) | 9.97 ns | 4.93 |

### Analysis
- Zero allocations.
- Same-encoding Text.EndsWith UTF-8 is ~1 ns slower than `U8String.EndsWith` and `Span.EndsWith` — pure dispatch wrapper cost on this near-zero operation.
- Cross-encoding (UTF-16 or UTF-32 needle) is 9–17 ns absolute, 4.9–8.7× baseline. The Emoji UTF-16 path (17 ns) decodes surrogate pairs in `Rune.TryDecodeLast` twice per rune — slower than the UTF-32 cross (10 ns) which decodes in one int read.
- `string.EndsWith` is a real 1.8 ns here (not folded to zero like UTF-16), so ratios are meaningful.

### Perf Fix
Same as EndsWithUtf16Benchmarks — transcode-the-needle-once would close the cross-encoding gap.

---

## EqualsUtf16Benchmarks

**File:** `Glot.Benchmarks.EqualsUtf16Benchmarks-report-github.md`
**Measures:** Ordinal equality of two equal texts (and same-length mutated pair for `different`). UTF-16 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Equals`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | Text.Equals UTF-16 (same) | 1.75 ns | 1.78 | - |
| 8 Ascii | Text.Equals UTF-8 (cross stream) | 16.11 ns | 16.34 | - |
| 8 Ascii | Text.Equals UTF-32 (cross stream) | 61.40 ns | 62.28 | - |
| 256 Ascii | Text.Equals UTF-16 (same) | 9.83 ns | 1.08 | - |
| 256 Ascii | Text.Equals UTF-32 (cross stream) | 1,428 ns | 156.42 | - |
| 65K Ascii | string.Equals | 2,427 ns | 1.00 | - |
| 65K Ascii | Text.Equals UTF-16 (same) | 2,448 ns | 1.01 | - |
| 65K Ascii | Text.Equals UTF-8 (cross stream) | 5,254 ns | 2.17 | - |
| 65K Ascii | Text.Equals UTF-32 (cross stream) | 370,748 ns | 152.77 | **19 B** |
| 65K Cjk | Text.Equals UTF-8 (cross stream) | 57,226 ns | 23.20 | **3 B** |
| 65K Cjk | Text.Equals UTF-32 (cross stream) | 369,475 ns | 149.80 | **18 B** |
| 65K Emoji | Text.Equals UTF-8 (cross stream) | 64,752 ns | 26.92 | **3 B** |
| 65K Emoji | Text.Equals UTF-32 (cross stream) | 193,398 ns | 80.39 | **10 B** |
| 65K Cjk `different` | Text.Equals UTF-16 different (same) | 2,434 ns | 0.99 | - |
| 65K Cjk `different` | Text.Equals UTF-8 different (cross) | 57,049 ns | 23.13 | **3 B** |
| 65K Emoji `different` | Text.Equals UTF-8 different (cross) | 1.17 ns | ~0 | - |
| 65K Emoji `different` | Text.Equals UTF-32 different (cross) | 1.23 ns | ~0 | - |

### Analysis
- Same-encoding `Text.Equals UTF-16` matches `string.Equals` at 65K (1.01×) — dispatches to `Bytes.SequenceEqual` (SIMD).
- Rune-length short-circuit works: `different` with a different rune length across encodings terminates in ~1 ns (65K Emoji cross rows).
- Cross-encoding (non-Emoji) takes `EqualsCrossEncoding` in `TextSpan.Equality.cs:50`: transcodes `other` in 512 B chunks, compares to the equivalent slice of `Bytes`. When both sides are equal, it walks the entire input. Cjk/Mixed UTF-8 cross costs 23× at 65K with 2–3 B alloc — the chunk transcoder holds a small state object that escapes.
- UTF-32 cross is the worst: 150× at 65K with 18–19 B alloc. UTF-32 → UTF-16 transcoding (2× expansion reversed per rune) plus the chunk-state allocs.
- Cjk/Mixed-`different` cross has same rune length on both sides so the short-circuit misses and it walks the full input — still 23× at 65K.

### Perf Fix

**Problem:** `EqualsCrossEncoding` (`TextSpan.Equality.cs:50`) streams via repeatedly-constructed `TextSpan` with encoded state and allocates 2–19 B per call at 65K — the chunk-source or transcoder state appears to escape.
**Location:** `src/Glot/TextSpan/TextSpan.Equality.cs:50` `EqualsCrossEncoding`.
**Proposed fix:** (1) Inspect the transcode inner loop — the `Transcode` call may be boxing encoder state. (2) For bounded-length inputs (≤ ~32 KB), transcode-the-shorter-side into an `ArrayPool`-rented buffer once and call `Bytes.SequenceEqual`. For unbounded, keep the streaming approach but eliminate the per-chunk allocation (use a struct-based incremental transcoder).
**Expected impact:** Drop 150× UTF-32-cross to ~10–20× (parity with same-encoding for equal-rune-count inputs would require more work); eliminate 2–19 B allocs.
**Effort:** M

---

## EqualsUtf32Benchmarks

**File:** `Glot.Benchmarks.EqualsUtf32Benchmarks-report-github.md`
**Measures:** Ordinal equality with a UTF-32 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Equals` (UTF-16; no native UTF-32 peer — reference only).

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 65K Ascii | string.Equals | 2,469 ns | 1.00 | - |
| 65K Ascii | Text.Equals UTF-32 (same) | 4,770 ns | 1.93 | - |
| 65K Ascii | Text.Equals UTF-8 (cross stream) | 310,961 ns | 125.94 | **16 B** |
| 65K Ascii | Text.Equals UTF-16 (cross stream) | 393,537 ns | 159.38 | **20 B** |
| 65K Cjk | Text.Equals UTF-32 (same) | 4,799 ns | 1.93 | - |
| 65K Cjk | Text.Equals UTF-8 (cross stream) | 347,435 ns | 139.85 | **18 B** |
| 65K Cjk | Text.Equals UTF-16 (cross stream) | 396,101 ns | 159.44 | **20 B** |
| 65K Emoji | Text.Equals UTF-32 (same) | 2,404 ns | 0.98 | - |
| 65K Emoji | Text.Equals UTF-8 (cross stream) | 180,414 ns | 73.51 | **9 B** |
| 65K Emoji | Text.Equals UTF-16 (cross stream) | 233,779 ns | 95.26 | **11 B** |
| 65K Cjk `different` | Text.Equals UTF-8 different | 349,700 ns | 140.77 | **17 B** |
| 65K Emoji `different` | Text.Equals UTF-8 different | 1.27 ns | ~0 | - |
| 65K Emoji `different` | Text.Equals UTF-16 different | 0.99 ns | ~0 | - |

### Analysis
- Same-encoding UTF-32 Equals runs at 1.93× `string.Equals` at 65K (SIMD `SequenceEqual` on 4-byte spans vs 2-byte — 2× data so ~2× ratio). Structural.
- Cross-encoding is 125–160× at 65K with 16–20 B allocations — same `EqualsCrossEncoding` streaming path as EqualsUtf16. UTF-32 direction is worse because the haystack is 4 bytes/rune so `TextSpan` construction and chunk comparison walks 4× the bytes.
- Length-mismatch short-circuit works: Emoji `different` rows finish in ~1 ns because UTF-8 and UTF-32 have different rune-length-to-byte ratios; on Cjk/Ascii the byte lengths differ too so short-circuit fires same-size.
- `Cjk different` doesn't short-circuit (UTF-8 3 bytes/rune + UTF-32 4 bytes/rune, same rune count, different byte lengths — but the `_encodedLength.RuneLength` test uses runes so both match; code then walks).

### Perf Fix
Same root cause as EqualsUtf16Benchmarks (`TextSpan.Equality.cs:50`). Same fix applies — bigger absolute impact here because UTF-32 is on both sides of the chunk transcode.

---

## EqualsUtf8Benchmarks

**File:** `Glot.Benchmarks.EqualsUtf8Benchmarks-report-github.md`
**Measures:** Ordinal equality with a UTF-8 haystack; target encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.Equals`. Direct peers: `Span<byte>.SequenceEqual`, `U8String.Equals`.

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | U8String.Equals | 1.52 ns | 0.68 | - |
| 8 Ascii | Text.Equals UTF-8 (same) | 2.84 ns | 1.26 | - |
| 65K Ascii | string.Equals | 2,384 ns | 1.00 | - |
| 65K Ascii | Span.SequenceEqual UTF-8 | 1,038 ns | 0.44 | - |
| 65K Ascii | U8String.Equals | 1,113 ns | 0.47 | - |
| 65K Ascii | Text.Equals UTF-8 (same) | 1,084 ns | 0.45 | - |
| 65K Ascii | Text.Equals UTF-16 (cross stream) | 3,339 ns | 1.40 | - |
| 65K Ascii | Text.Equals UTF-32 (cross stream) | 291,090 ns | 122.11 | **13 B** |
| 65K Cjk | Text.Equals UTF-8 (same) | 3,564 ns | 1.50 | - |
| 65K Cjk | Text.Equals UTF-16 (cross stream) | 45,039 ns | 18.94 | **2 B** |
| 65K Cjk | Text.Equals UTF-32 (cross stream) | 328,994 ns | 138.33 | **15 B** |
| 65K Emoji | Text.Equals UTF-8 (same) | 2,388 ns | 0.99 | - |
| 65K Emoji | Text.Equals UTF-16 (cross stream) | 67,156 ns | 27.80 | **3 B** |
| 65K Mixed | Text.Equals UTF-8 (same) | 1,485 ns | 0.62 | - |
| 65K Cjk `different` | Text.Equals UTF-8 different (same) | 2.54 ns | ~0 | - |
| 65K Emoji `different` | Text.Equals UTF-8 different (same) | 2.51 ns | ~0 | - |

### Analysis
- Same-encoding Text UTF-8 tracks `U8String.Equals` and raw `Span.SequenceEqual` within 30 ns at 65K — all dispatch to the same SIMD sequence-equal.
- `different` with a byte-length mismatch (Cjk/Emoji `different`) short-circuits at ~2.5 ns. `Text.Equals` is 1 ns slower than the raw peers (dispatch + length check).
- Cross-encoding UTF-16 target: 19–28× at 65K Cjk/Emoji with 2–3 B alloc. UTF-32 target is worst: 122–138× with 13–15 B alloc — same `EqualsCrossEncoding` streaming as EqualsUtf16/32.
- 65K Ascii Text Equals UTF-32 cross at 291 µs (122×) — the chunk transcoder is the bottleneck even on all-ASCII (because the destination is UTF-8 but the chunk source is UTF-32, so each 512 B UTF-8 chunk needs to consume 2048 B of UTF-32).

### Perf Fix
Cross-encoding variants: same as EqualsUtf16Benchmarks. Same-encoding: no fix warranted — at parity with `U8String` and `Span.SequenceEqual`.

---

## GetHashCodeUtf16Benchmarks

**File:** `Glot.Benchmarks.GetHashCodeUtf16Benchmarks-report-github.md`
**Measures:** Hash code of a text value (UTF-16 backing). Text's hash is rune-sequence-based (encoding-invariant) via XxHash3.
**Baseline:** `string.GetHashCode` (seeded Marvin hash).

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.GetHashCode | 3.33 ns | 1.00 | - |
| 8 Ascii | HashCode.AddBytes UTF-8 | 2.43 ns | 0.73 | - |
| 8 Ascii | Text.GetHashCode | 1.85 ns | 0.56 | - |
| 256 Cjk | Text.GetHashCode | 21.36 ns | 0.14 | - |
| 65K Ascii | string.GetHashCode | 40,820 ns | 1.00 | **2 B** |
| 65K Ascii | Text.GetHashCode | 3,788 ns | 0.09 | - |
| 65K Cjk | Text.GetHashCode | 3,776 ns | 0.10 | - |

### Analysis
- Text.GetHashCode is 0.09–0.14× `string.GetHashCode` at 65K — an order-of-magnitude win. `string` uses a seeded Marvin hash per-call (hence the 2 B alloc for the seeded state); Text uses XxHash3 over the decoded-rune buffer.
- For UTF-16 Text decodes runes via `Rune.TryDecodeFirst` into a stackalloc `int[]` (up to 256 runes; larger rents from `ArrayPool<int>`). At 65K the rented buffer is freed in-frame so no reported heap alloc.
- Zero allocs across all Text rows.

_(No Perf Fix needed.)_

---

## GetHashCodeUtf32Benchmarks

**File:** `Glot.Benchmarks.GetHashCodeUtf32Benchmarks-report-github.md`
**Measures:** Hash code of a UTF-32-backed text value. Uses direct `XxHash3` over the raw UTF-32 bytes (no rune decode needed).
**Baseline:** `string.GetHashCode` (UTF-16; no native UTF-32 peer — reference only).

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.GetHashCode | 3.31 ns | 1.00 | - |
| 8 Ascii | Text.GetHashCode | 2.78 ns | 0.84 | - |
| 256 Ascii | Text.GetHashCode | 35.48 ns | 0.23 | - |
| 65K Ascii | string.GetHashCode | 40,034 ns | 1.00 | **2 B** |
| 65K Ascii | Text.GetHashCode | 7,703 ns | 0.19 | - |
| 65K Emoji | Text.GetHashCode | 3,798 ns | 0.09 | - |

### Analysis
- UTF-32 hashing skips rune decoding entirely: `ComputeHashCode` in `TextSpan.Equality.cs:104` fast-paths to a direct `XxHash3.HashToUInt64(bytes)`.
- 2–10× faster than `string.GetHashCode` depending on locale; on equal-rune-count inputs, UTF-32 hashes 2× the byte volume of UTF-16 so Ascii lags Emoji/Cjk (where UTF-16 string is also 2 bytes/char).
- Zero allocs.

_(No Perf Fix needed.)_

---

## GetHashCodeUtf8Benchmarks

**File:** `Glot.Benchmarks.GetHashCodeUtf8Benchmarks-report-github.md`
**Measures:** Hash code of a UTF-8-backed text value.
**Baseline:** `string.GetHashCode`. Direct peer: `U8String.GetHashCode`.

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | string.GetHashCode | 3.36 ns | 1.00 | - |
| 8 Ascii | U8String.GetHashCode | 1.51 ns | 0.45 | - |
| 8 Ascii | Text.GetHashCode | 2.02 ns | 0.60 | - |
| 65K Ascii | string.GetHashCode | 40,426 ns | 1.00 | **1 B** |
| 65K Ascii | Text.GetHashCode | 1,858 ns | 0.05 | - |
| 65K Cjk | U8String.GetHashCode | 5,702 ns | 0.14 | - |
| 65K Cjk | Text.GetHashCode | 5,655 ns | 0.14 | - |

### Analysis
- At 65K Text edges `U8String.GetHashCode` by a nose on Cjk (5,655 vs 5,702 ns). Both use XxHash3 over the UTF-8 bytes; Text's rune-decode indirection through `ComputeHashCode` adds negligible cost.
- Half an order of magnitude faster than `string.GetHashCode` with zero alloc.
- On 65K Ascii Text hashes at 1,858 ns (0.05× string) — the rune-decode loop for UTF-8 ASCII is essentially `*p = *src` with an int-array scan, so XxHash3 dominates.

_(No Perf Fix needed.)_

---

## HttpPipelineBenchmarks

**File:** `Glot.Benchmarks.HttpPipelineBenchmarks-report-github.md`
**Measures:** End-to-end HTTP read/deserialize/process pipeline: `String` (HttpClient → string → parse), `Glot` (HttpClient → Text), `Glot pooled` (pool-rented UTF-8 buffer → OwnedText).
**Baseline:** `String pipeline`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 64 Ascii | String pipeline | 385.6 ns | 1.00 | 1,368 B |
| 64 Ascii | Glot pipeline | 493.9 ns | 1.28 | 824 B |
| 64 Ascii | Glot pooled pipeline | 403.2 ns | 1.05 | 568 B |
| 4096 Ascii | String pipeline | 4,098 ns | 1.00 | 49,944 B |
| 4096 Ascii | Glot pipeline | 2,635 ns | 0.64 | 18,936 B |
| 4096 Ascii | Glot pooled pipeline | 2,533 ns | 0.62 | 9,624 B |
| 4096 Cjk | String pipeline | 102,117 ns | 1.00 | 98,269 B |
| 4096 Cjk | Glot pooled pipeline | 100,225 ns | 0.98 | 27,037 B |
| 65K Ascii | String pipeline | 299,006 ns | 1.00 | 792,388 B |
| 65K Ascii | Glot pipeline | 132,562 ns | 0.44 | 295,430 B |
| 65K Ascii | Glot pooled pipeline | 73,494 ns | 0.25 | 147,871 B |
| 65K Cjk | Glot pooled pipeline | 1,581,111 ns | 0.97 | 426,468 B |
| 65K Emoji | Glot pooled pipeline | 1,510,635 ns | 0.91 | 289,930 B |

### Analysis
- At 65K Ascii the pooled Glot pipeline is **4× faster** and allocates **5.4× less** than the String pipeline — the UTF-8-native path skips the String constructor's UTF-16 decode and pools the intermediate buffer.
- Cjk/Emoji are tied on wall time (multi-byte transcoding on the wire dominates) but still allocate 3–5× less. The non-pooled Glot path delivers ~50–75% of the Ascii allocation, pooled delivers 20–30%.
- N=64 non-pooled Glot is 1.28× baseline — the fixed dispatch cost is visible when the total work is tiny.

_(No Perf Fix needed — the pooled variant is the production path and wins on both time and alloc at the sizes that matter.)_

---

## JsonSerializationBenchmarks

**File:** `Glot.Benchmarks.JsonSerializationBenchmarks-report-github.md`
**Measures:** System.Text.Json `Deserialize`, `RoundTrip`, `Serialize` for `string` vs `Text`; also the UTF-8-native `SerializeToUtf8OwnedText`.
**Baseline:** the string-based variant in each category.

### Results

| Category | Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|---|
| Deserialize | 65K Ascii | string | 59,318 ns | 1.00 | 188,671 B |
| Deserialize | 65K Ascii | Text | 9,257 ns | 0.16 | 94,584 B |
| Deserialize | 65K Cjk | string | 689,967 ns | 1.00 | 188,697 B |
| Deserialize | 65K Cjk | Text | 617,407 ns | 0.89 | 266,645 B |
| RoundTrip | 65K Ascii | string | 98,224 ns | 1.00 | 282,955 B |
| RoundTrip | 65K Ascii | Text | 39,366 ns | 0.40 | 188,868 B |
| RoundTrip | 65K Cjk | string | 1,013,782 ns | 1.00 | 713,075 B |
| RoundTrip | 65K Cjk | Text | 1,021,919 ns | 1.01 | 791,020 B |
| Serialize | 65K Ascii | string | 46,072 ns | 1.00 | 94,285 B |
| Serialize | 65K Ascii | Text | 42,751 ns | 0.93 | 94,285 B |
| Serialize | 65K Ascii | SerializeToUtf8OwnedText | 8,168 ns | 0.18 | - |
| Serialize | 65K Cjk | SerializeToUtf8OwnedText | 384,345 ns | 0.76 | **18 B** |

### Analysis
- **Deserialize-to-Text** at 65K Ascii is **6× faster** and allocates half (System.Text.Json hands the UTF-8 bytes directly to Text's factory; the string path has to UTF-16-decode). Cjk/Emoji is closer (0.89–0.93×) because the actual JSON decode work dominates.
- **RoundTrip (Ascii)** is **2.5× faster** and allocates 33% less. For Cjk at 65K it's at parity (within 1%) — full deserialize + serialize is wire-bound on multi-byte.
- **SerializeToUtf8OwnedText** is the standout: **5× faster** than `Serialize string to bytes` on 65K Ascii with ~0 B alloc — the OwnedText renter gives the JSON writer a pooled destination.
- For 65K Cjk `Deserialize to Text` allocates 41% *more* than `to string` (267 KB vs 189 KB) — the UTF-8 byte buffer for Cjk is 3 bytes/rune vs string's 2-byte-char, so Text's UTF-8 buffer is actually larger than the UTF-16 string. Not a bug, representation cost.

_(No Perf Fix — Text is consistently faster or at parity, and `SerializeToUtf8OwnedText` is the intended high-throughput path.)_

---

## LastByteIndexOfUtf16Benchmarks

**File:** `Glot.Benchmarks.LastByteIndexOfUtf16Benchmarks-report-github.md`
**Measures:** Last-occurrence byte-offset search with a UTF-16 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.LastIndexOf`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.LastIndexOf | 4.34 ns | 1.00 |
| 64 Ascii | Span.LastIndexOf UTF-8 | 2.42 ns | 0.56 |
| 64 Ascii | Text.LastByteIndexOf UTF-16 (same) | 4.88 ns | 1.13 |
| 64 Ascii | Text.LastByteIndexOf UTF-8 (cross) | 16.85 ns | 3.89 |
| 64 Ascii | Text.LastByteIndexOf UTF-32 (cross) | 18.21 ns | 4.20 |
| 64 Cjk | Text.LastByteIndexOf UTF-16 (same) | 3.17 ns | 1.71 |
| 64 Cjk | Text.LastByteIndexOf UTF-8 (cross) | 25.12 ns | 13.55 |
| 64 Cjk | Text.LastByteIndexOf UTF-32 (cross) | 32.17 ns | 17.35 |
| 65K Ascii miss | string.LastIndexOf miss | 3,979 ns | 1.00 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-16 miss (same) | 7,423 ns | 1.87 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-8 miss (cross) | 4,028 ns | 1.01 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-32 miss (cross) | 4,031 ns | 1.01 |

### Analysis
- Zero allocations across the board.
- Same-encoding `Text.LastByteIndexOf UTF-16` is 1.13–1.9× baseline on small N hits; 1.87× on 65K Ascii miss (the `string.LastIndexOf` ASCII path is slightly more vectorized than raw `Bytes.LastIndexOf`).
- Cross-encoding (UTF-8 or UTF-32 needle) costs 16–33 ns at small N but **converges to parity at 65K miss** (4,028 ns cross vs 4,031 ns same-encoding) — the ASCII-narrow fast path in `LastIndexOfCrossEncoding` `TextSpan.Search.cs:294` converts the needle once and runs the SIMD `Bytes.LastIndexOf`.

### Perf Fix
Not warranted. Same pattern as ByteIndexOfUtf16Benchmarks — cross-encoding has a small fixed setup cost at tiny N that amortizes to parity by 65K.

---

## LastByteIndexOfUtf32Benchmarks

**File:** `Glot.Benchmarks.LastByteIndexOfUtf32Benchmarks-report-github.md`
**Measures:** Last-occurrence byte-offset search in a UTF-32 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.LastIndexOf` (UTF-16; no native UTF-32 peer — reference only).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Text.LastByteIndexOf UTF-32 (same) | 8.94 ns | 2.07 |
| 64 Ascii | Text.LastByteIndexOf UTF-8 (cross) | 22.47 ns | 5.21 |
| 64 Ascii | Text.LastByteIndexOf UTF-16 (cross) | 21.65 ns | 5.02 |
| 64 Cjk | Text.LastByteIndexOf UTF-32 (same) | 3.45 ns | 1.90 |
| 64 Cjk | Text.LastByteIndexOf UTF-8 (cross) | 35.11 ns | 19.30 |
| 64 Cjk | Text.LastByteIndexOf UTF-16 (cross) | 36.22 ns | 19.91 |
| 65K Ascii miss | string.LastIndexOf miss | 4,147 ns | 1.00 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-32 miss (same) | 11,733 ns | 2.83 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-8 miss (cross) | 11,067 ns | 2.67 |
| 65K Ascii miss | Text.LastByteIndexOf UTF-16 miss (cross) | 10,994 ns | 2.65 |
| 65K Cjk | Text.LastByteIndexOf UTF-32 (same) | — | — |

### Analysis
- Zero allocations except a 1 B blip on all three Text variants at 65K Ascii miss — BDN per-op rounding.
- Same-encoding UTF-32 at 65K Ascii miss is 2.83× `string.LastIndexOf` — structural, UTF-32 scans 4× the bytes per rune.
- Cross-encoding converges to same-encoding at 65K miss (10,994 vs 11,733 ns); the 4×-wide scan dominates, cross-encoding setup is noise.
- At small N Cjk the 35–36 ns cross rows reflect transcode dispatch before the SIMD scan — no ASCII narrow fast path for Cjk needles.

### Perf Fix
Not warranted — same UTF-32 structural cost as ByteIndexOfUtf32Benchmarks.

---

## LastByteIndexOfUtf8Benchmarks

**File:** `Glot.Benchmarks.LastByteIndexOfUtf8Benchmarks-report-github.md`
**Measures:** Last-occurrence byte-offset search in a UTF-8 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.LastIndexOf`. Direct peers: `Span<byte>.LastIndexOf`, `U8String.LastIndexOf`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | string.LastIndexOf | 5.49 ns | 1.00 |
| 64 Ascii | Span.LastIndexOf UTF-8 | 3.58 ns | 0.65 |
| 64 Ascii | U8String.LastIndexOf | 3.66 ns | 0.67 |
| 64 Ascii | Text.LastByteIndexOf UTF-8 (same) | 4.51 ns | 0.82 |
| 64 Ascii | Text.LastByteIndexOf UTF-16 (cross) | 16.41 ns | 2.99 |
| 64 Cjk | Text.LastByteIndexOf UTF-8 (same) | 3.89 ns | 1.27 |
| 64 Cjk | Text.LastByteIndexOf UTF-16 (cross) | 35.40 ns | 11.59 |
| 65K Ascii | Span.LastIndexOf UTF-8 | 3.12 ns | 0.65 |
| 65K Ascii | U8String.LastIndexOf | 3.25 ns | 0.68 |
| 65K Ascii | Text.LastByteIndexOf UTF-8 (same) | 4.21 ns | 0.88 |
| 65K Ascii miss | U8String.LastIndexOf miss | 1,631 ns | 342 |
| 65K Ascii miss | Span.LastIndexOf UTF-8 miss | 2,003 ns | 420 |

### Analysis
- Same-encoding Text sits ~0.85 ns behind `U8String.LastIndexOf` and `Span.LastIndexOf` on 65K Ascii hits — typical wrapper overhead; `U8String.LastIndexOf miss` at 1,631 ns is notably faster than `Span.LastIndexOf miss` at 2,003 ns — U8String has a specialized last-byte SIMD scan that neither Text nor raw Span get.
- Cross-encoding UTF-16 at 64 Cjk (35 ns) is the familiar transcode-dispatch cost; no ASCII narrow path for Cjk needles.
- Zero allocs across the board.

### Perf Fix
Not warranted. Same-encoding Text is within ~1 ns of `Span.LastIndexOf`/`U8String` on hit; the U8String miss advantage reflects its dedicated SIMD implementation, not a Text regression.

---

## LastRuneIndexOfUtf16Benchmarks

**File:** `Glot.Benchmarks.LastRuneIndexOfUtf16Benchmarks-report-github.md`
**Measures:** Last-occurrence **rune**-index search with a UTF-16 haystack. Extra cost vs `LastByteIndexOf` is the `RuneCount.CountPrefix` step after the byte match.
**Baseline:** `string.LastIndexOf` (returns char-index, not rune-index — closest peer).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Text.LastRuneIndexOf UTF-16 (same) | 9.39 ns | 2.18 |
| 64 Ascii | Text.LastRuneIndexOf UTF-8 (cross) | 20.84 ns | 4.83 |
| 64 Ascii | Text.LastRuneIndexOf UTF-32 (cross) | 22.35 ns | 5.18 |
| 64 Cjk | Text.LastRuneIndexOf UTF-16 (same) | 7.03 ns | 3.76 |
| 64 Cjk | Text.LastRuneIndexOf UTF-8 (cross) | 30.03 ns | 16.07 |
| 64 Cjk | Text.LastRuneIndexOf UTF-32 (cross) | 37.09 ns | 19.85 |
| 64 Emoji | Text.LastRuneIndexOf UTF-16 (same) | 13.27 ns | 4.29 |

### Analysis
- Same-encoding Text adds 3–5 ns over `LastByteIndexOf UTF-16` (4.9 ns → 9.4 ns at Ascii) — the `RuneCount.CountPrefix` step after the byte hit.
- Cross-encoding still 5–20× baseline; identical to `LastByteIndexOfUtf16` cross cost + the rune-count step.
- Zero alloc.

### Perf Fix
**Problem:** `LastRuneIndexOf` always calls `RuneCount.CountPrefix` after the byte hit, even when `_runeLength == ByteLength` (known-all-ASCII, UTF-8 backing) or `_runeLength == ByteLength / 2` (UTF-16 all-BMP). For those cases the mapping is constant and the scan is wasted.
**Location:** `src/Glot/Text/Text.Search.cs:449` `LastRuneIndexOf(Text)`.
**Proposed fix:** Before calling `CountPrefix`, check the trivial-encoding cases — UTF-32 (bytePos / 4), UTF-16 when `_runeLength * 2 == ByteLength` (bytePos / 2), UTF-8 when `_runeLength == ByteLength` (bytePos).
**Expected impact:** 3–5 ns off Ascii/BMP hits (2.18× at 64 Ascii drops to ~1.1×).
**Effort:** S

---

## LastRuneIndexOfUtf32Benchmarks

**File:** `Glot.Benchmarks.LastRuneIndexOfUtf32Benchmarks-report-github.md`
**Measures:** Last-occurrence rune-index search in a UTF-32 haystack.
**Baseline:** `string.LastIndexOf` (UTF-16; no native UTF-32 peer — reference only).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Text.LastRuneIndexOf UTF-32 (same) | 8.61 ns | 2.02 |
| 64 Ascii | Text.LastRuneIndexOf UTF-8 (cross) | 22.55 ns | 5.30 |
| 64 Ascii | Text.LastRuneIndexOf UTF-16 (cross) | 21.75 ns | 5.11 |
| 64 Cjk | Text.LastRuneIndexOf UTF-32 (same) | 4.54 ns | 2.50 |
| 64 Cjk | Text.LastRuneIndexOf UTF-8 (cross) | 35.99 ns | 19.79 |
| 64 Cjk | Text.LastRuneIndexOf UTF-16 (cross) | 37.76 ns | 20.77 |
| 64 Emoji | Text.LastRuneIndexOf UTF-32 (same) | ~4.7 ns | ~1.5 |

### Analysis
- UTF-32 backing has trivially-computable rune counts (`byteLen / 4`) but `RuneCount.CountPrefix` doesn't know that and scans bytes. Same-encoding runs 4.5–8.6 ns absolute — ~4 ns could be shaved with a UTF-32 fast path.
- Cross-encoding still 5–20× the baseline at small N, same transcode-then-scan dispatch as the byte-offset search.
- Zero alloc.

### Perf Fix
Same S-effort fix as LastRuneIndexOfUtf16Benchmarks, **higher-impact here**: adding a UTF-32 shortcut (`bytePos >> 2`) would drop same-encoding from 2× to near-1×.

---

## LastRuneIndexOfUtf8Benchmarks

**File:** `Glot.Benchmarks.LastRuneIndexOfUtf8Benchmarks-report-github.md`
**Measures:** Last-occurrence rune-index search in a UTF-8 haystack.
**Baseline:** `string.LastIndexOf`. Direct peer: `U8String.LastIndexOf`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | U8String.LastIndexOf | 3.76 ns | 0.68 |
| 64 Ascii | Text.LastRuneIndexOf UTF-8 (same) | 4.98 ns | 0.90 |
| 64 Ascii | Text.LastRuneIndexOf UTF-16 (cross) | 17.15 ns | 3.09 |
| 64 Ascii | Text.LastRuneIndexOf UTF-32 (cross) | 18.57 ns | 3.35 |
| 64 Cjk | Text.LastRuneIndexOf UTF-8 (same) | 8.73 ns | 2.83 |
| 64 Cjk | Text.LastRuneIndexOf UTF-16 (cross) | 40.24 ns | 13.02 |
| 64 Cjk | Text.LastRuneIndexOf UTF-32 (cross) | 37.93 ns | 12.28 |
| 64 Emoji | Text.LastRuneIndexOf UTF-8 (same) | 13.93 ns | 3.22 |
| 64 Emoji | Text.LastRuneIndexOf UTF-16 (cross) | 72.81 ns | 16.83 |
| 64 Emoji | Text.LastRuneIndexOf UTF-32 (cross) | 42.88 ns | 9.91 |

### Analysis
- Same-encoding Text is 0.9–3.2× baseline (5–14 ns absolute), mostly the `CountPrefix` scan for non-Ascii.
- Emoji UTF-16 cross at **72.81 ns is 1.7× the UTF-32 cross (42.88 ns)** — the same pattern as LastRuneIndexOfUtf16's Emoji reverse. UTF-16 surrogate-pair decode in the cross-encoding needle transcode is noticeably slower than a straight UTF-32 int read.
- Zero alloc.

### Perf Fix
**Problem:** UTF-16→UTF-8 cross-encoding `LastRuneIndexOf` with surrogate-pair needles (emoji) runs ~1.7× the UTF-32 cross equivalent.
**Location:** `src/Glot/TextSpan/TextSpan.Search.cs:294` `LastIndexOfCrossEncoding` — for UTF-16 needle decode into the haystack (UTF-8) buffer.
**Proposed fix:** Specialize the UTF-16→UTF-8 transcode for the small-needle ASCII-narrow path: when the needle is ≤4 bytes per rune UTF-8 (guaranteed in BMP+astral), convert in a tight char-loop instead of going through the generic `Transcode` dispatch.
**Expected impact:** Drop 72.81 ns Emoji UTF-16 cross to ~40 ns (parity with UTF-32 cross).
**Effort:** M

---

## ReplaceUtf16Benchmarks

**File:** `Glot.Benchmarks.ReplaceUtf16Benchmarks-report-github.md`
**Measures:** Substring replacement with a UTF-16 haystack. Both eager (`Replace`, allocates) and pooled (`ReplacePooled`, rents from `ArrayPool`) variants.
**Baseline:** `string.Replace`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 64 Ascii | string.Replace | 30.27 ns | 1.00 | 176 B |
| 64 Ascii | Text.Replace UTF-16 | 54.51 ns | 1.80 | 176 B |
| 64 Ascii | Text.ReplacePooled UTF-16 | 69.33 ns | 2.29 | - |
| 4096 Ascii | Text.Replace UTF-16 | 2,800 ns | 1.23 | 9,848 B |
| 4096 Ascii | Text.ReplacePooled UTF-16 | 2,338 ns | 1.02 | - |
| 65K Ascii | string.Replace | 77,785 ns | 1.00 | 157,311 B |
| 65K Ascii | Text.Replace UTF-16 | 50,048 ns | 0.64 | 157,302 B |
| 65K Ascii | Text.ReplacePooled UTF-16 | 37,388 ns | 0.48 | **2 B** |
| 65K Cjk | Text.ReplacePooled UTF-16 | 37,556 ns | 0.49 | **2 B** |
| 65K Emoji | Text.Replace UTF-16 | 107,909 ns | 0.88 | 163,863 B |
| 65K Emoji | Text.ReplacePooled UTF-16 | 61,992 ns | 0.50 | **3 B** |

### Analysis
- At 65K Text.ReplacePooled is **2× faster** than `string.Replace` and allocates ~zero (2–3 B residual is BDN per-op rounding from the `ArrayPool` metadata, not a real heap alloc). Eager `Text.Replace` is 1.6× faster than string with equivalent allocation.
- At N=64 Text.Replace is 1.8× slower than `string.Replace` (dispatch cost) and pooled is 2.3× slower (pool rent/return overhead dominates at tiny sizes).
- Small-N pooled underperforming non-pooled is expected: `ArrayPool.Rent/Return` is ~20 ns of overhead.

### Perf Fix
**Problem:** Small-N Text.Replace is 1.5–1.8× slower than `string.Replace`. Below ~256 bytes the rune-aware machinery doesn't earn its keep.
**Location:** `src/Glot/Text/Text.Replace.cs` (the main `Replace(Text marker, Text replacement)` entry point).
**Proposed fix:** Add a short-input shortcut: when haystack + replacement fit in ~128 bytes and all three are same-encoding, stackalloc + `Bytes.IndexOf` + byte-splice, skip the RuneLength tracking.
**Expected impact:** 64 Ascii 1.8× → ~1.1×; 4K and 65K untouched.
**Effort:** M

---

## ReplaceUtf32Benchmarks

**File:** `Glot.Benchmarks.ReplaceUtf32Benchmarks-report-github.md`
**Measures:** Substring replacement with a UTF-32 haystack.
**Baseline:** none (no UTF-32 `Replace` in the BCL).

### Results

| Param set | Method | Mean | Alloc |
|---|---|---|---|
| 64 Ascii | Text.Replace UTF-32 | 78.79 ns | 328 B |
| 64 Ascii | Text.ReplacePooled UTF-32 | 65.26 ns | - |
| 4096 Ascii | Text.Replace UTF-32 | 3,482 ns | 19,672 B |
| 4096 Ascii | Text.ReplacePooled UTF-32 | 2,637 ns | - |
| 65K Ascii | Text.Replace UTF-32 | 129,076 ns | 314,597 B |
| 65K Ascii | Text.ReplacePooled UTF-32 | 44,469 ns | **2 B** |
| 65K Cjk | Text.ReplacePooled UTF-32 | 45,183 ns | - |
| 65K Emoji | Text.ReplacePooled UTF-32 | 53,615 ns | **3 B** |
| 65K Mixed | Text.ReplacePooled UTF-32 | 50,251 ns | **2 B** |

### Analysis
- Pooled UTF-32 is 2–3× faster than non-pooled at 65K — avoids the ~320 KB array alloc.
- Non-pooled allocates ~4× bytes of the input (UTF-32 = 4 bytes/rune).
- Pooled 65K Ascii (44 µs) is comparable to pooled UTF-16 (37 µs) — the 4×-wide scan is offset by `Bytes.IndexOf` SIMD being effectively the same cost for a 4-byte stride.

_(No Perf Fix — no direct comparison point.)_

---

## ReplaceUtf8Benchmarks

**File:** `Glot.Benchmarks.ReplaceUtf8Benchmarks-report-github.md`
**Measures:** Substring replacement with a UTF-8 haystack.
**Baseline:** `string.Replace`. Direct peer: `U8String.Replace`.

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 64 Ascii | U8String.Replace | 26.71 ns | 0.87 | 104 B |
| 64 Ascii | Text.Replace UTF-8 | 48.20 ns | 1.58 | 104 B |
| 64 Ascii | Text.ReplacePooled UTF-8 | 65.38 ns | 2.14 | - |
| 4096 Ascii | Text.Replace UTF-8 | 2,342 ns | 0.98 | 4,936 B |
| 4096 Ascii | Text.ReplacePooled UTF-8 | 2,053 ns | 0.86 | - |
| 65K Ascii | U8String.Replace | 20,262 ns | 0.21 | 78,673 B |
| 65K Ascii | Text.Replace UTF-8 | 37,024 ns | 0.38 | 78,664 B |
| 65K Ascii | Text.ReplacePooled UTF-8 | 30,844 ns | 0.32 | **1 B** |
| 65K Cjk | Text.Replace UTF-8 | 84,432 ns | 0.91 | 209,744 B |
| 65K Cjk | Text.ReplacePooled UTF-8 | 35,410 ns | 0.38 | **2 B** |
| 65K Emoji | U8String.Replace | 150,262 ns | 1.95 | 163,871 B |
| 65K Emoji | Text.Replace UTF-8 | 121,712 ns | 1.58 | 163,867 B |
| 65K Emoji | Text.ReplacePooled UTF-8 | 51,874 ns | 0.67 | **3 B** |
| 65K Ascii `miss` | string.Replace miss | 4,371 ns | 0.05 | - |
| 65K Ascii `miss` | Text.Replace UTF-8 miss | 2,174 ns | 0.02 | - |

### Analysis
- At 64 Ascii Text.Replace UTF-8 is 1.58× `string.Replace` — dispatch overhead at small N.
- At 65K Emoji pooled Text is **3× faster** than `U8String.Replace` (51 µs vs 150 µs) — U8String's Emoji path is slower (2× vs ~1× for Ascii/Cjk) while Text's stays flat.
- At 65K Ascii `U8String.Replace` edges pooled Text (20 µs vs 31 µs) — U8String specializes for short-marker single-byte replacement; Text pays for the rune-count bookkeeping.
- Miss path (marker not found) is 2× faster than `string.Replace` (2.2 µs vs 4.4 µs) because Text returns the original unchanged without allocating.

### Perf Fix
Same small-N concern as ReplaceUtf16Benchmarks: a ≤128 B same-encoding stackalloc path would close the 1.6× gap at 64 Ascii. No fix for 65K — pooled Text is already the faster path for Cjk/Emoji.

---

## RuneIndexOfUtf16Benchmarks

**File:** `Glot.Benchmarks.RuneIndexOfUtf16Benchmarks-report-github.md`
**Measures:** First-occurrence rune-index search with a UTF-16 haystack. Extra cost vs `ByteIndexOf` is `RuneCount.CountPrefix` after the byte hit.
**Baseline:** `string.IndexOf` (returns char-index; closest peer).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Text.RuneIndexOf UTF-16 (same) | 7.68 ns | 2.88 |
| 64 Ascii | Text.RuneIndexOf UTF-8 (cross) | 19.59 ns | 7.36 |
| 64 Ascii | Text.RuneIndexOf UTF-32 (cross) | 20.51 ns | 7.70 |
| 64 Cjk | Text.RuneIndexOf UTF-16 (same) | 5.96 ns | 4.69 |
| 64 Cjk | Text.RuneIndexOf UTF-8 (cross) | 27.62 ns | 21.73 |
| 64 Cjk | Text.RuneIndexOf UTF-32 (cross) | 34.67 ns | 27.27 |
| 64 Emoji | Text.RuneIndexOf UTF-16 (same) | 5.67 ns | 3.08 |
| 64 Mixed | Text.RuneIndexOf UTF-16 (same) | 9.55 ns | 4.42 |

### Analysis
- Same-encoding Text adds ~3 ns over `ByteIndexOf UTF-16` (4.9 → 7.7 ns Ascii) — the prefix rune-count step after the SIMD byte hit.
- Cross-encoding 7–27× absolute — same as `ByteIndexOf` cross path plus the rune-count.
- Zero alloc.

### Perf Fix
Same S-effort fix as LastRuneIndexOfUtf16Benchmarks: skip `RuneCount.CountPrefix` when the encoding-to-rune mapping is constant (UTF-32 / all-BMP UTF-16 / all-Ascii UTF-8).

---

## RuneIndexOfUtf32Benchmarks

**File:** `Glot.Benchmarks.RuneIndexOfUtf32Benchmarks-report-github.md`
**Measures:** First-occurrence rune-index search with a UTF-32 haystack.
**Baseline:** `string.IndexOf` (UTF-16; no native UTF-32 peer — reference only).

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Text.RuneIndexOf UTF-32 (same) | 5.59 ns | 2.09 |
| 64 Ascii | Text.RuneIndexOf UTF-8 (cross) | 20.33 ns | 7.59 |
| 64 Ascii | Text.RuneIndexOf UTF-16 (cross) | 19.00 ns | 7.09 |
| 64 Cjk | Text.RuneIndexOf UTF-32 (same) | 4.13 ns | 3.28 |
| 64 Cjk | Text.RuneIndexOf UTF-16 (cross) | 38.59 ns | 30.69 |
| 64 Cjk | Text.RuneIndexOf UTF-8 (cross) | 37.00 ns | 29.43 |
| 64 Emoji | Text.RuneIndexOf UTF-32 (same) | 3.96 ns | 2.16 |

### Analysis
- Same-encoding UTF-32 at 4–6 ns absolute. `RuneCount.CountPrefix` still runs on the bytePos even though UTF-32 makes it `bytePos / 4` — ~3 ns could be shaved.
- Cross-encoding 7–30× baseline, tracks the byte-offset search cost.
- Zero alloc.

### Perf Fix
Same as LastRuneIndexOfUtf32Benchmarks — UTF-32 shortcut (`bytePos >> 2`) in `RuneIndexOf` entry. High-impact: drops same-encoding 2× to near-1×.

---

## RuneIndexOfUtf8Benchmarks

**File:** `Glot.Benchmarks.RuneIndexOfUtf8Benchmarks-report-github.md`
**Measures:** First-occurrence rune-index search with a UTF-8 haystack.
**Baseline:** `string.IndexOf`. Direct peer: `U8String.IndexOf`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Span.IndexOf UTF-8 | 3.29 ns | 0.84 |
| 64 Ascii | U8String.IndexOf | 3.57 ns | 0.91 |
| 64 Ascii | Text.RuneIndexOf UTF-8 (same) | 15.00 ns | 3.82 |
| 64 Ascii | Text.RuneIndexOf UTF-16 (cross) | 17.43 ns | 4.44 |
| 64 Ascii | Text.RuneIndexOf UTF-32 (cross) | 18.48 ns | 4.70 |
| 64 Cjk | Text.RuneIndexOf UTF-8 (same) | 11.94 ns | 4.72 |
| 64 Cjk | Text.RuneIndexOf UTF-16 (cross) | 43.31 ns | 17.13 |
| 64 Emoji | Text.RuneIndexOf UTF-8 (same) | 7.38 ns | 2.44 |
| 64 Ascii `miss` | Text.RuneIndexOf UTF-8 miss | 5.50 ns | 1.40 |
| 64 Cjk `miss` | Text.RuneIndexOf UTF-8 miss | 10.46 ns | 4.14 |

### Analysis
- Same-encoding Text Ascii hit at 15 ns — more than 4× `U8String.IndexOf` (3.6 ns) or `Span.IndexOf` (3.3 ns). The `RuneCount.CountPrefix` step for UTF-8 walks the bytes counting continuation bits, which is slow vs a constant for UTF-32 or char-count for UTF-16. Miss path is fast (5.5 ns) because no prefix to count.
- Cjk same-encoding (11.94 ns) is *faster* than Ascii because `RuneCount.CountPrefix` has a 3-bytes-per-rune shortcut — ironic that Ascii is the slow case.
- Cross-encoding 17–43 ns — tracks the UTF-8 haystack byte search plus prefix count plus transcode.
- Zero alloc.

### Perf Fix
**Problem:** UTF-8 same-encoding `RuneIndexOf` runs 4× `U8String.IndexOf` on Ascii hits because `RuneCount.CountPrefix` walks byte-by-byte counting continuation bytes; for all-ASCII (common case) it should be `bytePos`.
**Location:** `src/Glot/Text/Text.Search.cs:297` `RuneIndexOf(Text)` — the `CountPrefix` call at :314.
**Proposed fix:** When `_runeLength == ByteLength` (Text records all-ASCII on construction), short-circuit `CountPrefix` → return `bytePos`.
**Expected impact:** 64 Ascii 15 ns → ~5 ns (parity with U8String.IndexOf).
**Effort:** S

---

## StartsWithUtf16Benchmarks

**File:** `Glot.Benchmarks.StartsWithUtf16Benchmarks-report-github.md`
**Measures:** Prefix match with a UTF-16 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.StartsWith` (constant-folded; ratios unreliable — prefer absolute times).

### Results (ratios noisy — `string.StartsWith` folds to ~0 ns)

| Param set | Method | Mean |
|---|---|---|
| 64 Ascii | string.StartsWith | ~0 ns |
| 64 Ascii | Span.StartsWith UTF-8 | 0.25 ns |
| 64 Ascii | Text.StartsWith UTF-16 (same) | 1.45 ns |
| 64 Ascii | Text.StartsWith UTF-8 (cross) | 7.40 ns |
| 64 Ascii | Text.StartsWith UTF-32 (cross) | 6.75 ns |
| 64 Cjk | Text.StartsWith UTF-16 (same) | 1.49 ns |
| 64 Cjk | Text.StartsWith UTF-8 (cross) | 8.06 ns |
| 64 Emoji | Text.StartsWith UTF-16 (same) | 1.52 ns |
| 64 Emoji | Text.StartsWith UTF-8 (cross) | 9.24 ns |
| 65K Cjk | Text.StartsWith UTF-16 (same) | 1.49 ns |

### Analysis
- Zero allocations.
- `string.StartsWith` and `Span.StartsWith` with a compile-time-known prefix fold to 0–0.3 ns — ratios unreliable.
- Same-encoding `Text.StartsWith UTF-16` is a flat 1.5 ns regardless of N or script (dispatches to `Bytes.StartsWith`).
- Cross-encoding 7–9 ns absolute — cross-path goes through `RunePrefix.TryMatch` (`TextSpan.Search.cs:131`) which walks rune-by-rune from the front.

### Perf Fix
Same as EndsWithUtf16Benchmarks — transcode-the-needle-once into haystack encoding and call `Bytes.StartsWith`. Would halve the 7–9 ns cross cost.

---

## StartsWithUtf32Benchmarks

**File:** `Glot.Benchmarks.StartsWithUtf32Benchmarks-report-github.md`
**Measures:** Prefix match with a UTF-32 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.StartsWith` (UTF-16; constant-folded; reference only).

### Results (baseline near-zero after inlining)

| Param set | Method | Mean |
|---|---|---|
| 64 Ascii | string.StartsWith | 0.09 ns |
| 64 Ascii | Text.StartsWith UTF-32 (same) | 1.56 ns |
| 64 Ascii | Text.StartsWith UTF-8 (cross) | 5.68 ns |
| 64 Ascii | Text.StartsWith UTF-16 (cross) | 6.82 ns |
| 64 Cjk | Text.StartsWith UTF-32 (same) | 1.55 ns |
| 64 Cjk | Text.StartsWith UTF-8 (cross) | 6.21 ns |
| 64 Emoji | Text.StartsWith UTF-32 (same) | 1.55 ns |
| 64 Emoji | Text.StartsWith UTF-16 (cross) | 8.06 ns |

### Analysis
- Zero allocations.
- Same-encoding UTF-32 is a flat 1.5–1.6 ns — just `Bytes.StartsWith` on the 4-byte-wide span.
- Cross-encoding 5.7–8 ns absolute, same `RunePrefix.TryMatch` path.
- UTF-32 roughly 1 ns heavier than UTF-16 on same-encoding due to the 2× byte-count stride — minor structural tax.

### Perf Fix
Same as EndsWithUtf16Benchmarks.

---

## StartsWithUtf8Benchmarks

**File:** `Glot.Benchmarks.StartsWithUtf8Benchmarks-report-github.md`
**Measures:** Prefix match with a UTF-8 haystack; needle encodings UTF-8/UTF-16/UTF-32.
**Baseline:** `string.StartsWith`. Direct peers: `Span<byte>.StartsWith`, `U8String.StartsWith`.

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 64 Ascii | Span.StartsWith UTF-8 | 1.48 ns | 1.14 |
| 64 Ascii | U8String.StartsWith | 1.52 ns | 1.17 |
| 64 Ascii | Text.StartsWith UTF-8 (same) | 2.18 ns | 1.68 |
| 64 Ascii | Text.StartsWith UTF-16 (cross) | 9.1 ns | ~7 |
| 64 Emoji | Text.StartsWith UTF-16 (cross) | 11.09 ns | 8.69 |

### Analysis
- Zero allocations.
- Text.StartsWith UTF-8 is 0.65 ns slower than `U8String.StartsWith` / `Span.StartsWith` — wrapper dispatch cost; on-par baseline is `string.StartsWith` at ~1.3 ns.
- Cross-encoding 7–11 ns absolute — same `RunePrefix.TryMatch` path as the other StartsWith classes.

### Perf Fix
Same as EndsWithUtf16Benchmarks — transcode-once stackalloc then `Bytes.StartsWith`.

---

## TextBuilderUtf16Benchmarks

**File:** `Glot.Benchmarks.TextBuilderUtf16Benchmarks-report-github.md`
**Measures:** Build a UTF-16 Text from N parts (`Parts` ∈ {2,4,16,64,256}, `PartSize` ∈ {1,8,64,1024}), materializing via `ToText` or `ToOwnedText`.
**Baseline:** `StringBuilder → ToString`

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 1×2 Ascii | StringBuilder → ToString | 14.90 ns | 1.00 | 136 B |
| 1×2 Ascii | TextBuilder UTF-16 → ToText | 19.38 ns | 1.30 | 32 B |
| 1×2 Ascii | TextBuilder UTF-16 → ToOwnedText | 40.38 ns | 2.71 | - |
| 1×2 Ascii | TextBuilder UTF-8→UTF-16 → ToText | 29.26 ns | 1.96 | 32 B |
| 1×16 Ascii | TextBuilder UTF-8→UTF-16 → ToText | 141.06 ns | 4.66 | 56 B |
| 1×256 Ascii | TextBuilder UTF-8→UTF-16 → ToText | 2,089 ns | 5.68 | 536 B |
| 64×2 Ascii | TextBuilder UTF-16 → ToText | 34.28 ns | 0.66 | 280 B |
| 64×2 Cjk | TextBuilder UTF-8→UTF-16 → ToText | 166.58 ns | 3.23 | 280 B |

### Analysis
- Same-encoding TextBuilder UTF-16 allocates 24% of StringBuilder at small parts (32 B vs 136 B) and runs within 1.3× time.
- For 64-byte parts with 2 parts, TextBuilder beats StringBuilder 0.66× — bytewise copy wins over the formatter path.
- Cross-encoding (UTF-8 → UTF-16 append) costs per-part: 2× at 2 parts, 4.7× at 16 parts, 5.7× at 256 parts of 1 byte — the per-Append transcode doesn't amortize.

### Perf Fix
**Problem:** UTF-8→UTF-16 Append paths in `TextBuilder` transcode per-part; at 256×1-byte parts the overhead compounds to 5.7× StringBuilder.
**Location:** `src/Glot/TextBuilder/TextBuilder.cs` `Append(Text)` cross-encoding branch.
**Proposed fix:** Batch cross-encoding: buffer small pending UTF-8 appends in an intermediate staging region and transcode in chunks when capacity reaches threshold or on materialization.
**Expected impact:** 256-part Ascii cross 5.7× → ~2×.
**Effort:** L

---

## TextBuilderUtf32Benchmarks

**File:** `Glot.Benchmarks.TextBuilderUtf32Benchmarks-report-github.md`
**Measures:** Incrementally building a UTF-32 text from N parts.
**Baseline:** none (no Utf32 competitor).

### Results (selected)

| Param set | Method | Mean | Alloc |
|---|---|---|---|
| 1×2 Ascii | TextBuilder UTF-32 -> ToText | 19.44 ns | 32 B |
| 1×64 Ascii | TextBuilder UTF-32 -> ToText | 261.86 ns | 280 B |
| 1×64 Ascii | TextBuilder UTF-8→UTF-32 -> ToText | 296.72 ns | 280 B |
| 1×64 Ascii | TextBuilder UTF-16→UTF-32 -> ToText | 291.02 ns | 280 B |
| 64×16 Cjk | TextBuilder UTF-8→UTF-32 -> ToText | 1,669 ns | 4,120 B |
| 64×16 Cjk | TextBuilder UTF-16→UTF-32 -> ToText | 1,192 ns | 4,120 B |

### Analysis
- Same-encoding UTF-32 builder allocates 4× the byte count of the data (UTF-32 = 4 bytes/rune).
- UTF-16→UTF-32 transcoding is slightly *faster* than UTF-8→UTF-32 for Cjk (1,192 ns vs 1,669 ns) — single-char transcode vs multibyte decode.

_(No Perf Fix — no baseline.)_

---

## TextBuilderUtf8Benchmarks

**File:** `Glot.Benchmarks.TextBuilderUtf8Benchmarks-report-github.md`
**Measures:** Incrementally building a UTF-8 text from N parts.
**Baseline:** `StringBuilder -> UTF-8 bytes` (StringBuilder + `Encoding.UTF8.GetBytes`)

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 1×2 Ascii | StringBuilder -> UTF-8 bytes | 20.44 ns | 1.00 | 168 B |
| 1×2 Ascii | TextBuilder UTF-8 -> ToText | 19.31 ns | 0.94 | 32 B |
| 1×2 Cjk | TextBuilder UTF-16→UTF-8 -> ToText | 25.18 ns | 1.09 | 32 B |
| 64×2 Ascii | StringBuilder -> UTF-8 bytes | 71.47 ns | 1.00 | 904 B |
| 64×2 Ascii | TextBuilder UTF-8 -> ToText | 25.53 ns | 0.36 | 152 B |
| 64×2 Cjk | TextBuilder UTF-16→UTF-8 -> ToText | 130.62 ns | 0.73 | 408 B |

### Analysis
- TextBuilder UTF-8 is 3× faster than `StringBuilder→GetBytes` at larger part sizes and allocates 5× less.
- Cross-encoding UTF-16→UTF-8 is still faster than the baseline roundtrip (0.73×).
- This is the cleanest TextBuilder win.

_(No Perf Fix — consistently faster.)_

---

## TextConcatUtf16Benchmarks

**File:** `Glot.Benchmarks.TextConcatUtf16Benchmarks-report-github.md`
**Measures:** Concatenation of N Text parts.
**Baseline:** `string.Concat`

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 1×2 Ascii | string.Concat | 9.19 ns | 1.00 | 32 B |
| 1×2 Ascii | Text.Concat UTF-16 | 9.01 ns | 0.98 | 32 B |
| 1×2 Ascii | Text.Concat UTF-8→UTF-16 | 32.90 ns | 3.58 | 32 B |
| 1×2 Ascii | Text.ConcatPooled UTF-16 | 14.96 ns | 1.63 | - |
| 1×2 Ascii | LinkedTextUtf16.Create | 12.55 ns | 1.37 | **184 B** |
| 1×64 Cjk | Text.Concat UTF-8→UTF-16 | 603.13 ns | 3.82 | 152 B |
| 1×256 Emoji | Text.Concat UTF-8→UTF-16 | 2,444 ns | 3.82 | 536 B |

### Analysis
- Same-encoding Text.Concat UTF-16 is at parity with `string.Concat` and zero overhead.
- Cross-encoding 3.3–3.8× across sizes — transcoding per part.
- `LinkedTextUtf16.Create` allocates 5–12× more bytes than string.Concat (184 B for 32 B payload) because each link carries overhead — the trade-off is that the tree doesn't copy the payloads.

### Perf Fix
Same as TextBuilderUtf16Benchmarks — cross-encoding per-part transcode could be batched. For Concat the input set is known in advance so batching is trivial; a one-pass "compute total target byte length, allocate, transcode directly into destination" pass would close the 3.8× gap.

---

## TextConcatUtf32Benchmarks

**File:** `Glot.Benchmarks.TextConcatUtf32Benchmarks-report-github.md`
**Measures:** Concatenation of N Text parts into a UTF-32 result.
**Baseline:** none.

### Results (selected)

| Param set | Method | Mean | Alloc |
|---|---|---|---|
| 1×2 Ascii | Text.Concat UTF-32 | 8.98 ns | 32 B |
| 1×2 Ascii | Text.Concat UTF-8→UTF-32 | 24.34 ns | 32 B |
| 64×2 Cjk | Text.Concat UTF-8→UTF-32 | 223.12 ns | 536 B |
| 64×16 Cjk | Text.Concat UTF-16→UTF-32 | 1,199 ns | 4,120 B |

### Analysis
- UTF-32 concatenation 2.7× slower than UTF-8 → UTF-32 for Ascii at small sizes (expected: expansion to 4 bytes/rune).

_(No Perf Fix — no baseline.)_

---

## TextConcatUtf8Benchmarks

**File:** `Glot.Benchmarks.TextConcatUtf8Benchmarks-report-github.md`
**Measures:** Concatenation of N Text parts into a UTF-8 result. Adds `U8String.Concat`.
**Baseline:** `byte[] concat UTF-8`

### Results (selected)

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 1×2 Ascii | U8String.Concat | 6.76 ns | 0.82 |
| 1×2 Ascii | Text.Concat UTF-8 | 8.70 ns | 1.06 |
| 1×2 Ascii | Text.ConcatPooled UTF-8 | 14.73 ns | 1.79 |
| 1×2 Ascii | Text.Concat UTF-16→UTF-8 | 27.20 ns | 3.31 |
| 1×64 Ascii | Text.Concat UTF-8 | 140.75 ns | 0.91 |

### Analysis
- Text.Concat UTF-8 near-parity with the byte[] baseline and ~25% slower than U8String.Concat.
- Cross-encoding 3× for small parts, tracks transcode cost.
- Pooled variant is slower than non-pooled at tiny sizes (pool rent overhead).

### Perf Fix
Same as TextBuilderUtf16Benchmarks — one-pass pre-compute + direct-write concat across encodings eliminates per-part transcode.

---

## TextCreationByteArrayUtf16Benchmarks

**File:** `Glot.Benchmarks.TextCreationByteArrayUtf16Benchmarks-report-github.md`
**Measures:** Create a Text from a `byte[]` (UTF-16).
**Baseline:** `new string(chars)`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromBytes(byte[]) | 3.70 ns | 0.61 |
| 8 Ascii | Text.FromBytes(byte[]) no-count | 1.50 ns | 0.25 |
| 65536 Ascii | Text.FromBytes(byte[]) | 4,723 ns | 0.09 |
| 65536 Ascii | Text.FromBytes(byte[]) no-count | 1.49 ns | ~0 |

### Analysis
- The no-count variant is effectively O(1) because rune counting is deferred — huge win (0.000003× at 65K, free).
- The counted variant at 65K is 11× faster than `new string(chars)`.
- No allocations.

_(No Perf Fix — strong win.)_

---

## TextCreationByteArrayUtf32Benchmarks

**File:** `Glot.Benchmarks.TextCreationByteArrayUtf32Benchmarks-report-github.md`
**Measures:** Create a Text from a `byte[]` (UTF-32).
**Baseline:** `Encoding.UTF32.GetString`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromBytes(byte[]) | 1.51 ns | 0.03 |
| 65536 Ascii | Text.FromBytes(byte[]) | 1.52 ns | 0.000 |

### Analysis
- Both counted and no-count variants are near-constant 1.5 ns — no rune counting needed for UTF-32 (byteLength/4 = runeLength).
- Three orders of magnitude faster than `Encoding.UTF32.GetString`.

_(No Perf Fix.)_

---

## TextCreationByteArrayUtf8Benchmarks

**File:** `Glot.Benchmarks.TextCreationByteArrayUtf8Benchmarks-report-github.md`
**Measures:** Create a Text from a `byte[]` (UTF-8). Includes `U8String`, `FromAscii`.
**Baseline:** `Encoding.GetString`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromUtf8(byte[]) | 3.93 ns | 0.45 |
| 8 Ascii | Text.FromUtf8(byte[]) no-count | 1.50 ns | 0.17 |
| 8 Ascii | Text.FromAscii(byte[]) | 1.51 ns | 0.17 |
| 65536 Ascii | Text.FromUtf8(byte[]) | 2,510 ns | 0.057 |
| 65536 Cjk | Text.FromUtf8(byte[]) | 7,530 ns | 0.083 |

### Analysis
- Text.FromAscii and Text.FromUtf8 no-count are both ~1.5 ns regardless of N — deferred validation.
- Even counting runes, 10–17× faster than `Encoding.GetString`.

_(No Perf Fix.)_

---

## TextCreationCharArrayUtf16Benchmarks

**File:** `Glot.Benchmarks.TextCreationCharArrayUtf16Benchmarks-report-github.md`
**Measures:** Create Text from `char[]`.
**Baseline:** `new string(char[])`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromChars(char[]) | 3.77 ns | 0.61 |
| 8 Ascii | Text.FromChars(char[]) no-count | 1.58 ns | 0.25 |
| 65536 Ascii | Text.FromChars(char[]) | 4,730 ns | 0.091 |

### Analysis
- Same pattern as byte[] variants: no-count path is O(1), counted path is 11× faster than `new string`.

_(No Perf Fix.)_

---

## TextCreationCharSpanUtf16Benchmarks

**File:** `Glot.Benchmarks.TextCreationCharSpanUtf16Benchmarks-report-github.md`
**Measures:** Create Text from `ReadOnlySpan<char>` (non-array). Must copy.
**Baseline:** `new string(span)`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | Text.FromChars(span) | 7.91 ns | 1.21 | 40 B |
| 8 Ascii | Text.FromChars(span) no-count | 6.03 ns | 0.92 | 40 B |
| 256 Ascii | Text.FromChars(span) | 44.75 ns | 1.82 | 536 B |
| 65536 Ascii | Text.FromChars(span) | 36,329 ns | 0.70 | 131,092 B |
| 65536 Ascii | OwnedText.FromChars(span) | 5,961 ns | 0.11 | - |

### Analysis
- Text.FromChars(span) must allocate and copy — the counted variant is 1.8× `new string(span)` at 256 because it must both copy and count.
- At 65K it's 0.7× (SIMD copy beats `new string` constructor allocation).
- `OwnedText.FromChars(span)` is 9× faster than baseline at 65K and pool-allocated (0 B).

### Perf Fix
**Problem:** `Text.FromChars(span)` 256 Ascii at 44.75 ns (1.8×) — counted variant is noticeably slower than string.
**Location:** `src/Glot/Text/Text.Factory.cs` `FromChars(ReadOnlySpan<char>)`.
**Proposed fix:** Combine the alloc + memcpy + rune-count into one pass — currently looks like memcpy then separate scan.
**Expected impact:** 44.75 → ~30 ns at 256 Ascii.
**Effort:** M

---

## TextCreationImmutableArrayUtf8Benchmarks

**File:** `Glot.Benchmarks.TextCreationImmutableArrayUtf8Benchmarks-report-github.md`
**Measures:** Create Text from `ImmutableArray<byte>`.
**Baseline:** `Encoding.GetString`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromUtf8(ImmutableArray) | 3.99 ns | 0.46 |
| 65536 Ascii | Text.FromUtf8(ImmutableArray) | 2,551 ns | 0.057 |

### Analysis
- Same shape as byte[] variant — ImmutableArray is just a wrapper.
- 17× faster than baseline at 65K.

_(No Perf Fix.)_

---

## TextCreationIntArrayUtf32Benchmarks

**File:** `Glot.Benchmarks.TextCreationIntArrayUtf32Benchmarks-report-github.md`
**Measures:** Create Text from `int[]` (UTF-32 codepoints).
**Baseline:** `Encoding.UTF32.GetString`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromUtf32(int[]) | 1.51 ns | 0.03 |
| 65536 Ascii | Text.FromUtf32(int[]) | 1.51 ns | ~0 |

### Analysis
- Constant 1.5 ns (no-copy wrapping of the int[] as UTF-32 bytes via `MemoryMarshal.AsBytes`).

_(No Perf Fix.)_

---

## TextCreationIntSpanUtf32Benchmarks

**File:** `Glot.Benchmarks.TextCreationIntSpanUtf32Benchmarks-report-github.md`
**Measures:** Create Text from `ReadOnlySpan<int>`. Must copy.
**Baseline:** `Encoding.UTF32.GetString(span)`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | Text.FromUtf32(span) | 5.91 ns | 0.12 | 56 B |
| 65536 Ascii | Text.FromUtf32(span) | 61,552 ns | 0.20 | **262,169 B** |

### Analysis
- Must copy 4× the codepoint count. At 65K Ascii allocates 262 KB (2× the byte count of the UTF-32 data) — likely a double-copy.
- Still 5× faster than Encoding.UTF32.GetString(span).
- `OwnedText.FromUtf32(span)` avoids the alloc entirely (4,384 ns, 0.014×).

### Perf Fix
**Problem:** `Text.FromUtf32(span)` at 65K allocates **2× the expected bytes** (262 KB for 131 KB of data) — double-copy or intermediate buffer.
**Location:** `src/Glot/Text/Text.Factory.cs` `FromUtf32(ReadOnlySpan<int>)`.
**Proposed fix:** Allocate one `byte[byteCount]`, `MemoryMarshal.Cast<int,byte>(span).CopyTo(dest)`. Don't round-trip through an intermediate allocation.
**Expected impact:** Halve the 262 KB alloc to 131 KB; cut the mean ~25–30%.
**Effort:** S

---

## TextCreationSpanUtf16Benchmarks

**File:** `Glot.Benchmarks.TextCreationSpanUtf16Benchmarks-report-github.md`
**Measures:** Create Text from `ReadOnlySpan<char>` (alt path).
**Baseline:** `new string(chars)`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | Text.FromBytes(span) | 7.73 ns | 1.24 | 40 B |
| 8 Ascii | Text.FromBytes(span) no-count | 5.63 ns | 0.90 | 40 B |
| 256 Ascii | Text.FromBytes(span) | 44.54 ns | 1.83 | 536 B |
| 65536 Ascii | Text.FromBytes(span) | 55,872 ns | 1.33 | 131,102 B |

### Analysis
- Same pattern as CharSpanUtf16 — ~1.8× at 256 Ascii, ~1.3× at 65K.
- No-count variant close to parity at small N.

### Perf Fix
Same as TextCreationCharSpanUtf16Benchmarks — one-pass copy+count.

---

## TextCreationSpanUtf32Benchmarks

**File:** `Glot.Benchmarks.TextCreationSpanUtf32Benchmarks-report-github.md`
**Measures:** Create Text from `ReadOnlySpan<byte>` interpreted as UTF-32.
**Baseline:** `Encoding.UTF32.GetString(span)`

### Results

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 8 Ascii | Text.FromBytes(span) | 5.94 ns | 0.12 | 56 B |
| 65536 Ascii | Text.FromBytes(span) | 63,412 ns | 0.20 | **262,169 B** |

### Analysis
- Same double-allocation anomaly as TextCreationIntSpanUtf32 — 262 KB for 131 KB of payload.

### Perf Fix
Same as TextCreationIntSpanUtf32Benchmarks — one-pass copy (no intermediate buffer).

---

## TextCreationSpanUtf8Benchmarks

**File:** `Glot.Benchmarks.TextCreationSpanUtf8Benchmarks-report-github.md`
**Measures:** Create Text from `ReadOnlySpan<byte>` (UTF-8). Adds `U8String`.
**Baseline:** `Encoding.GetString(span)`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.FromUtf8(span) | 7.26 ns | 0.82 |
| 8 Ascii | Text.FromUtf8(span) no-count | 5.29 ns | 0.60 |
| 65536 Cjk | Text.FromUtf8(span) | 69,224 ns | 0.76 |
| 65536 Cjk | OwnedText.FromUtf8(span) | 9,906 ns | 0.11 |

### Analysis
- Text.FromUtf8(span) at 65K Cjk is 0.76× baseline (needs to copy + rune count); OwnedText is 9× faster because it rents from a pool.
- 65K Cjk allocates 196 KB for 131 KB of data — likely same double-copy issue as the UTF-32 span paths.

### Perf Fix
**Problem:** `Text.FromUtf8(span)` allocates 1.5× byte length at 65K Cjk (196 KB vs 131 KB data).
**Location:** `src/Glot/Text/Text.Factory.cs` `FromUtf8(ReadOnlySpan<byte>)`.
**Proposed fix:** Collapse the copy+validate into a single `span.ToArray()` or `new byte[len]` + `CopyTo` then rune count — don't double-buffer.
**Expected impact:** Halve 65K Cjk mean to ~35 µs, alloc to 131 KB.
**Effort:** S

---

## TextCreationStringUtf16Benchmarks

**File:** `Glot.Benchmarks.TextCreationStringUtf16Benchmarks-report-github.md`
**Measures:** Create Text from a `string`.
**Baseline:** `new string(source)`

### Results

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.From(string) | 4.01 ns | 0.62 |
| 8 Ascii | Text.From(string) no-count | 1.98 ns | 0.31 |
| 8 Ascii | Text.FromAscii(string) | 1.99 ns | 0.31 |
| 256 Ascii | Text.From(string) | 21.31 ns | 0.88 |
| 65536 Ascii | Text.From(string) | 4,723 ns | 0.111 |
| 8 Cjk | new U8String(string) | 16.27 ns | 2.50 |
| 256 Emoji | new U8String(string) | 450.75 ns | 18.72 |

### Analysis
- Text.From(string) no-count is 3× faster than `new string` — deferred validation & alias.
- Counted path is 9× faster than baseline at 65K.
- U8String allocates and transcodes which is why it's 18× at 256 Emoji.

_(No Perf Fix.)_

---

## TextCreationStringUtf8Benchmarks

**File:** `Glot.Benchmarks.TextCreationStringUtf8Benchmarks-report-github.md`
**Measures:** Create Text from a `string`, converting to UTF-8 storage. Run with dry job only (1 iter), so numbers are in µs and extremely noisy.
**Baseline:** `new string(source)`

### Results (dry-run, use with caution)

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 8 Ascii | Text.From(string) | 124.04 µs | 1.01 |
| 8 Ascii | Text.FromAscii(string) | 172.38 µs | 1.41 |
| 8 Ascii | new U8String(string) | 219.38 µs | 1.79 |
| 8 Mixed | Text.From(string) | 406.50 µs | 4.01 |
| 65536 Ascii | Text.From(string) | 569.58 µs | 5.54 |
| 65536 Emoji | new U8String(string) | 262.83 µs | 2.65 |

### Analysis
- This report uses `Job=Dry IterationCount=1 LaunchCount=1 RunStrategy=ColdStart` — numbers are *startup-dominated*, not steady-state. Ratios can swing wildly.
- The 4× Mixed/N=8 result (406 µs) is almost certainly the first JIT compilation of the Text.From(string) code path, not a real regression.
- For trustworthy numbers here, re-run with the default BDN job.

### Perf Fix
**Problem:** Can't conclude real regressions from dry-run data. The 65K Ascii 5.54× result might reflect the transcode UTF-16→UTF-8 work; steady-state needed to confirm.
**Location:** `src/Glot/Text/Text.Factory.cs` `From(string)` — for UTF-8 target, goes through `Encoding.UTF8.GetBytes`.
**Proposed fix:** Re-run this benchmark with the default `Job` (not `Dry`) to get steady-state numbers; then decide.
**Expected impact:** Unknown until re-run.
**Effort:** S (re-run + interpret)

---

## TextInterpolationUtf16Benchmarks

**File:** `Glot.Benchmarks.TextInterpolationUtf16Benchmarks-report-github.md`
**Measures:** Interpolated string handler `$"..."` producing Text or string.
**Baseline:** `string $"..."`

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 2p / 1 Ascii | string $"..." | 6.72 ns | 1.00 | 32 B |
| 2p / 1 Ascii | Text.Create $"..." UTF-16 | 21.37 ns | 3.18 | 32 B |
| 2p / 1 Ascii | OwnedText.Create $"..." UTF-16 | 24.11 ns | 3.59 | - |
| 2p / 64 Cjk | Text.Create $"..." UTF-16 | 129.52 ns | 8.90 | 408 B |
| 2p / 1024 Emoji | Text.Create $"..." UTF-16 | 2,187 ns | 14.97 | 4,120 B |
| 4p / 64 Emoji | Text.Create $"..." UTF-16 | 337.20 ns | 13.14 | 536 B |

### Analysis
- Small-N Text interpolation 3× baseline — handler overhead (format state machine) is larger for Text.
- Large-N multibyte (Emoji 1024) is 15× slower — interpolation is doing full transcoding per format slot.
- Allocation ratios mostly match payload size (1.0×).

### Perf Fix
**Problem:** `Text.Create $"..."` interpolated handler for UTF-16 target has per-hole transcoding that costs 3–15× the string baseline.
**Location:** investigate around `src/Glot/Text/Text.Factory.cs` and `TextInterpolatedHandler` (if it exists; otherwise the default `DefaultInterpolatedStringHandler` usage).
**Proposed fix:** Buffer format segments into a scratch space first (like StringBuilder), then materialize once. Avoid per-hole transcoding on every `AppendFormatted`.
**Expected impact:** Bring 64-part Emoji from 13× to ~4×.
**Effort:** L

---

## TextInterpolationUtf32Benchmarks

**File:** `Glot.Benchmarks.TextInterpolationUtf32Benchmarks-report-github.md`
**Measures:** Interpolated string handler → UTF-32 Text.
**Baseline:** none.

### Results (selected)

| Param set | Method | Mean | Alloc |
|---|---|---|---|
| 2p / 1 Ascii | Text.Create $"..." UTF-32 | 19.04 ns | 32 B |
| 2p / 64 Cjk | Text.Create $"..." UTF-32 | 286.01 ns | 408 B |
| 8p / 1024 Cjk | Text.Create $"..." UTF-32 | 17,279 ns | 24,601 B |

### Analysis
- UTF-32 interpolation pays the 4-byte-per-rune expansion tax — large values allocate accordingly.
- No baseline — can't rate "regression" in relative terms.

_(No Perf Fix — no baseline.)_

---

## TextInterpolationUtf8Benchmarks

**File:** `Glot.Benchmarks.TextInterpolationUtf8Benchmarks-report-github.md`
**Measures:** Interpolated string handler → UTF-8 Text.
**Baseline:** `string $"..."`

### Results (selected)

| Param set | Method | Mean | Ratio |
|---|---|---|---|
| 2p / 1 Ascii | Text.Create $"..." UTF-8 | 16.76 ns | 2.52 |
| 2p / 64 Cjk | Text.Create $"..." UTF-8 | 51.11 ns | 3.48 |
| 4p / 64 Cjk | Text.Create $"..." UTF-8 | 92.83 ns | 3.67 |
| 4p / 1024 Cjk | Text.Create $"..." UTF-8 | 684.30 ns | 2.33 |

### Analysis
- UTF-8 interpolation 2–3.7× baseline — better than UTF-16 Text interpolation.
- Pooled variant (OwnedText) often ties or beats string for PartSize 1024.

### Perf Fix
Same as TextInterpolationUtf16Benchmarks — buffer-then-materialize on the UTF-8 target would bring the 3× down toward 1.5×.

---

## TextSplitUtf16Benchmarks

**File:** `Glot.Benchmarks.TextSplitUtf16Benchmarks-report-github.md`
**Measures:** Two categories — `EnumerateRunes` (iterate all runes) and `Split` (iterate segments between separators).
**Baseline:** `string.EnumerateRunes` / `string.Split count`

### Results (selected)

| Category | Param set | Method | Mean | Ratio |
|---|---|---|---|---|
| EnumerateRunes | 64 Ascii | Text.EnumerateRunes UTF-16 | 268.4 ns | 3.04 |
| EnumerateRunes | 4096 Ascii | Text.EnumerateRunes UTF-16 | 16,597 ns | 9.99 |
| EnumerateRunes | 65536 Ascii | Text.EnumerateRunes UTF-16 | 267,277 ns | 5.90 |
| Split | 64 Cjk | Text.Split UTF-16 count | 104.4 ns | 0.57 |
| Split | 65536 Cjk | Text.Split UTF-16 count | 82,721 ns | 0.18 |

### Analysis
- **Split is a big win**: 5.5× faster at 65K Cjk with zero alloc vs 655 KB for string.Split.
- **EnumerateRunes is a big loss**: 10× slower at 4K Ascii. The per-rune decode loop (`Rune.TryDecodeFirst` on each iteration) is costing much more than `string.EnumerateRunes`'s natively inlined char-by-char loop.

### Perf Fix
**Problem:** `TextSpan.RuneEnumerator.MoveNext()` calls `Rune.TryDecodeFirst(_remaining, _encoding, out _current, out var consumed)` every iteration, which for UTF-16 is roughly 4 branches + a surrogate check. string.EnumerateRunes is inlined and uses two-char lookahead.
**Location:** `src/Glot/TextSpan/TextSpan.RuneEnumerator.cs:28` `MoveNext`.
**Proposed fix:** Specialize the enumerator per-encoding: for UTF-16, read `char` directly and branch on surrogate; for UTF-8, check high-bit and decode inline; for UTF-32 just `MemoryMarshal.Cast<byte,int>`. Avoid the generic `Rune.TryDecodeFirst` indirection.
**Expected impact:** Drop UTF-16 EnumerateRunes from 10× to ~2× at 4K Ascii.
**Effort:** M

---

## TextSplitUtf32Benchmarks

**File:** `Glot.Benchmarks.TextSplitUtf32Benchmarks-report-github.md`
**Measures:** EnumerateRunes + Split for UTF-32.
**Baseline:** none.

### Results (selected)

| Category | Param set | Method | Mean | Alloc |
|---|---|---|---|---|
| EnumerateRunes | 4096 Ascii | Text.EnumerateRunes UTF-32 | 8,687 ns | - |
| EnumerateRunes | 65536 Ascii | Text.EnumerateRunes UTF-32 | 139,064 ns | 7 B |
| Split | 65536 Ascii | Text.Split UTF-32 count | 488,568 ns | 18 B |

### Analysis
- No baseline. Split UTF-32 at 65K runs 0.5 ms — slower than UTF-16 Split (83 µs) because the UTF-32 backing contains 4× bytes to scan.
- EnumerateRunes UTF-32 is 2× faster than UTF-16 though (no surrogate pair branch).

### Perf Fix
Same as TextSplitUtf16Benchmarks. UTF-32 is particularly cheap to specialize: `MemoryMarshal.Cast<byte,int>(bytes).GetEnumerator()` is a one-liner.

---

## TextSplitUtf8Benchmarks

**File:** `Glot.Benchmarks.TextSplitUtf8Benchmarks-report-github.md`
**Measures:** EnumerateRunes + Split for UTF-8. Adds `Span.DecodeFromUtf8`, `U8String.Runes`, `U8String.Split`.
**Baseline:** per-category — `string.EnumerateRunes` and `Span.IndexOf UTF-8`

### Results (selected)

| Category | Param set | Method | Mean | Ratio |
|---|---|---|---|---|
| EnumerateRunes | 64 Ascii | U8String.Runes | 19.00 ns | 0.22 |
| EnumerateRunes | 64 Ascii | Text.EnumerateRunes UTF-8 | 158.9 ns | 1.82 |
| EnumerateRunes | 65536 Cjk | Text.EnumerateRunes UTF-8 | 178,430 ns | 2.94 |
| Split | 64 Ascii | Text.Split UTF-8 count | 561.0 ns | **16.31** |
| Split | 4096 Ascii | Text.Split UTF-8 count | 35,079 ns | **10.56** |

### Analysis
- **EnumerateRunes UTF-8 is 8–9× slower than U8String.Runes** — U8String specializes the UTF-8 decode while Text uses the generic `Rune.TryDecodeFirst`.
- **Split UTF-8 is 16× `Span.IndexOf UTF-8`** at 64 Ascii and 10× at 4K Ascii — a huge regression. Span.IndexOf is pure SIMD byte search; Text.Split must also count runes per segment.

### Perf Fix
**Problem:** `TextSpan.SplitEnumerator.MoveNext` calls `RuneCount.Count` on every yielded segment (src:46, 61, 68). For N=64 with separator every 8 bytes that's 8 `RuneCount.Count` scans per foreach — ~8× the work of a single IndexOf.
**Location:** `src/Glot/TextSpan/TextSpan.SplitEnumerator.cs:46, 61, 68`.
**Proposed fix:** Make rune-count on the yielded segment lazy: construct `TextSpan` with `runeCount = 0` (deferred), let consumers that need the count request it. For the common Split-then-iterate-bytes pattern, we'd never pay for it.
**Expected impact:** Drop 64 Ascii 16.31× to ~1.5×; drop 4K Ascii 10× to ~1.2×.
**Effort:** M

---

## ToUpperUtf16Benchmarks

**File:** `Glot.Benchmarks.ToUpperUtf16Benchmarks-report-github.md`
**Measures:** `ToUpperInvariant` with UTF-16 backing.
**Baseline:** `string.ToUpperInvariant`

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 64 Ascii | Text.ToUpperInvariant UTF-16 | 17.60 ns | 1.23 | 152 B |
| 64 Ascii | Text.ToUpperInvariantPooled UTF-16 | 37.51 ns | 2.62 | - |
| 64 Cjk | Text.ToUpperInvariant UTF-16 | 469.5 ns | 3.73 | - |
| 4096 Ascii | Text.ToUpperInvariant UTF-16 | 501.3 ns | 1.17 | 8,216 B |
| 4096 Ascii | Text.ToUpperInvariantPooled UTF-16 | 518.0 ns | 1.21 | - |
| 4096 Cjk | Text.ToUpperInvariant UTF-16 | 28,840 ns | 3.98 | 1 B |
| 4096 Mixed | Text.ToUpperInvariant UTF-16 | 33,821 ns | 4.20 | 8,218 B |
| 65536 Ascii | Text.ToUpperInvariant UTF-16 | 34,874 ns | 0.68 | 131,091 B |
| 65536 Cjk | Text.ToUpperInvariant UTF-16 | 463,826 ns | 3.88 | 23 B |
| 65536 Mixed | Text.ToUpperInvariant UTF-16 | 534,403 ns | 3.99 | 131,124 B |

### Analysis
- **Ascii fast path is working**: 65K Ascii is 0.68× baseline — the `TryUpperAscii` vectorized shortcut beats string.
- **Non-Ascii is 4× slower**: Cjk/Mixed 65K runs at 463–534 µs vs 119–133 µs — rune-by-rune `Rune.ToUpperInvariant` in `ToUpperCore` is much slower than `string.ToUpperInvariant` (which uses ICU/system casing tables with bulk chars).
- Pooled variant at 65K Ascii is 6× faster than non-pooled because it avoids the 131 KB alloc.

### Perf Fix
**Problem:** `ToUpperCore` in `src/Glot/Text/Text.Case.cs:165` calls a rune-by-rune loop for the non-ASCII path; `string.ToUpperInvariant` uses Span-based culture-aware bulk casing (~4× faster on Cjk/Mixed).
**Location:** `src/Glot/Text/Text.Case.cs:165` `ToUpperCore` → `CaseCore`.
**Proposed fix:** For UTF-16 backing, delegate the non-ASCII suffix (bytes after `firstChangeOffset`) to `MemoryExtensions.ToUpperInvariant(srcChars, destChars)` (the Span-based BCL API) instead of per-rune. That uses the same ICU tables as `string.ToUpperInvariant`.
**Expected impact:** Drop 65K Cjk from 3.88× to ~1.2× baseline.
**Effort:** M

---

## ToUpperUtf32Benchmarks

**File:** `Glot.Benchmarks.ToUpperUtf32Benchmarks-report-github.md`
**Measures:** `ToUpperInvariant` with UTF-32 backing.
**Baseline:** none.

### Results (selected)

| Param set | Method | Mean | Alloc |
|---|---|---|---|
| 64 Ascii | Text.ToUpperInvariant UTF-32 | 52.30 ns | 280 B |
| 4096 Cjk | Text.ToUpperInvariant UTF-32 | 21,444 ns | 1 B |
| 65536 Ascii | Text.ToUpperInvariantPooled UTF-32 | 34,620 ns | 2 B |
| 65536 Cjk | Text.ToUpperInvariant UTF-32 | 344,390 ns | 13 B |

### Analysis
- Pooled variant at 65K Ascii is 3× faster than non-pooled (34 µs vs 104 µs).
- Cjk/Mixed costs 344 µs / 387 µs — similar to UTF-16 non-pooled.

_(No Perf Fix — no baseline. UTF-32 is niche; rune-by-rune is the default.)_

---

## ToUpperUtf8Benchmarks

**File:** `Glot.Benchmarks.ToUpperUtf8Benchmarks-report-github.md`
**Measures:** `ToUpperInvariant` with UTF-8 backing.
**Baseline:** `string.ToUpperInvariant`

### Results (selected)

| Param set | Method | Mean | Ratio | Alloc |
|---|---|---|---|---|
| 64 Ascii | Text.ToUpperInvariant UTF-8 | 15.12 ns | 1.00 | 88 B |
| 64 Ascii | Text.ToUpperInvariantPooled UTF-8 | 20.70 ns | 1.37 | - |
| 64 Cjk | Text.ToUpperInvariant UTF-8 | 430.3 ns | 3.40 | - |
| 4096 Ascii | Text.ToUpperInvariant UTF-8 | 240.6 ns | 0.54 | 4,120 B |
| 4096 Ascii | Text.ToUpperInvariantPooled UTF-8 | 115.7 ns | 0.26 | - |
| 4096 Cjk | Text.ToUpperInvariant UTF-8 | 26,439 ns | 3.81 | 1 B |
| 65536 Ascii | Text.ToUpperInvariant UTF-8 | 3,046 ns | 0.06 | 65,560 B |
| 65536 Ascii | Text.ToUpperInvariantPooled UTF-8 | 1,590 ns | 0.03 | - |
| 65536 Cjk | Text.ToUpperInvariant UTF-8 | 429,200 ns | 3.74 | - |
| 65536 Mixed | Text.ToUpperInvariant UTF-8 | 431,393 ns | 3.31 | 85,536 B |

### Analysis
- **Ascii is a massive win**: 65K Ascii non-pooled is **16× faster** than `string.ToUpperInvariant` and half the allocation (byte[] vs UTF-16 char[]). Pooled is 30× faster with ~0 alloc.
- **Non-Ascii still lags**: Cjk/Mixed run 3–4× slower than string — same rune-by-rune issue as UTF-16.
- Emoji is ~1.1× (most emoji have no case mapping so the scan is fast).

### Perf Fix
**Problem:** Same non-ASCII hot path as UTF-16 — per-rune `Rune.ToUpperInvariant` dominates non-ASCII.
**Location:** `src/Glot/Text/Text.Case.cs:165` `ToUpperCore` → `CaseCore` for UTF-8 target.
**Proposed fix:** For UTF-8 backing, transcode the non-ASCII remainder to UTF-16 chars in a pooled buffer, call `MemoryExtensions.ToUpperInvariant(srcChars, destChars, CultureInfo.InvariantCulture)`, transcode back. Sounds expensive but beats rune-by-rune: the BCL path is vectorized.
**Expected impact:** Drop 65K Cjk 3.74× to ~1.5×; keep Ascii at its current 0.06× (ASCII shortcut is untouched).
**Effort:** M
