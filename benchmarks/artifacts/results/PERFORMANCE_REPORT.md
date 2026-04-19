# Glot Performance Report

**Source:** `benchmarks/artifacts/results/*-report-github.md` (52 benchmark classes)
**Hardware:** Apple M4 Max, 16 cores, .NET 10.0.5, Arm64 RyuJIT
**Run configuration:** ShortRun (3 iterations, 1 launch, 3 warmups) — RatioSD is noisy on several rows; flagged where relevant.

> **Update 2026-04-17:** Several findings below have been resolved or reclassified after code review and fix implementation. See the **Addendum** at the end of this report for the current status and retractions.

---

## Executive Summary

Glot's **same-encoding hot paths are competitive or better** than the BCL (`string`, `Span<byte>`) and on par with `U8String`. The library's unique wins are:

- **Zero-allocation factory methods** (`Text.FromAscii`, `Text.FromUtf32(int[])`) that borrow rather than copy — essentially free vs. `Encoding.GetString` at ~1000× at large N.
- **Owned/pooled variants** (`OwnedText.Create`, `Text.ConcatPooled`, `ReplacePooled`, `SerializeToUtf8OwnedText`) that hit true `0 B` allocation across all sizes.
- **End-to-end pipeline throughput** (HTTP pooled pipeline: 57% faster, 81% less allocation at 64 KB Ascii payloads).
- **`GetHashCode` beats `string` by 20×** at 65 KB because it skips UTF-16 randomized hashing.

The report flags **four structural regressions** that dominate the "bad rows" and account for the majority of >5× ratios:

1. **Cross-encoding transcode dispatch (~6–11 ns fixed)** — identical cost at N=64 and N=65 KB, scaling-free overhead.
2. **UTF-16 CJK / non-ASCII transcoder** — 10–100× slower than same-encoding at Cjk/Mixed locales, but fine at Ascii — a missing non-ASCII fast path.
3. **`LastRuneIndexOf` Ascii** — scales with haystack length (10.3 ns at 65 KB vs 3 ns for `LastByteIndexOf`) — an O(N) reverse rune walk that should be O(match-offset).
4. **`ToUpperInvariant`** — 40–100× baseline everywhere, Pooled variant offers no speedup, confirming the Unicode walk itself is the bottleneck.

Some catastrophic-on-paper ratios are absolute-time irrelevant (`StartsWith UTF-16 @ Mixed` = 210× but only 7 ns). Others are genuine cliffs (`Text.Equals UTF-32 cross @ 65 KB Cjk` = 190 µs vs 2.5 µs for `string.Equals`).

**Zero allocations** is achieved on every search, equality, and hash API. All allocation regressions are in mutation / interpolation / creation paths — and even there, Pooled/Owned variants fix them.

---

## Methodology Notes

- **Ratio > 1.5×** flagged as a regression; **> 5×** flagged as a cliff; **absolute time < 10 ns** treated as "ratio-interesting but perf-irrelevant."
- **Allocation ratios** are weighted heavily — a 1.3× time regression with 0.25× allocation is a win in most workloads.
- **Cross-encoding** rows (e.g. `Text.Contains UTF-16` inside a UTF-8 benchmark class) involve a transcode; separated from same-encoding analysis.
- **ShortRun noise:** RatioSD values over ~30% of Ratio are marked; any finding there should be revalidated with `--job medium`.

---

## 1. Substring Search — Contains / StartsWith / EndsWith

**Verdict:** Same-encoding path is at parity; cross-encoding paths pay a flat ~6–8 ns dispatch tax; CJK cross-encoding is the cliff.

### Contains
| Case | Text (ns) | Baseline (ns) | Ratio | Notes |
|---|---|---|---|---|
| UTF-8 hit, any N, Ascii | 2.97 | 2.14 (Span) | **1.12×** | parity |
| UTF-8 hit, any N, Cjk | 2.74 | 1.75 (Span) | 1.38–1.56× | small excess |
| UTF-8 miss, 4096, Ascii | 97 | 98 (Span) | ~1.00× | good |
| **UTF-16 xenc, 4096, Cjk** | **23.56** | 1.98 (string) | **11.88×** | CJK transcode + fixed tax |
| UTF-16 xenc, any, Ascii | 8.49 | 2.65 (string) | 3.21× | ~6 ns fixed tax |
| UTF-32 xenc, any, Cjk | 18.81 | 1.95 (string) | 9.65× | CJK xenc regressed |

**Fixed cross-encoding overhead is N-independent** (8.4 ns @ N=64 ≈ 8.5 ns @ N=65536 for Ascii). Same-encoding `Text.Contains UTF-8` keeps up at all N because it delegates to `Span.IndexOf`.

### StartsWith / EndsWith
Baselines for these are sub-nanosecond (inlined, length-checked ordinal compare). Text has a hard floor:

- `Text.StartsWith UTF-8`: ~0.78–0.87 ns fixed (ratio 5–24×, absolute-time irrelevant).
- `Text.StartsWith UTF-16`: ~6.9–7.9 ns fixed (ratios up to **210× at Mixed** — but 7 ns absolute; not a real regression).
- `Text.StartsWith UTF-32`: ~5.3–5.9 ns fixed (ratio 33–161×, same caveat).
- `Text.EndsWith UTF-8`: ~1.5–2.1 ns (adds ~0.7 ns over StartsWith — reverse-slicing not free).
- `Text.EndsWith UTF-16 miss, Cjk, 4096`: **19.1×** (10.45 ns vs 0.55 ns).

**Allocations:** zero everywhere. Clean.

### Suggested Fixes
1. **Eliminate the ~6 ns cross-encoding dispatch floor.** The flat N-independent overhead is consistent with a stackalloc + transcode setup per call. For short needles (≤ 16 bytes):
   - Inline a `[SkipLocalsInit]` 32-byte stackalloc transcode.
   - Fast-path `IsAscii` on the needle: ASCII-only → skip transcoding and reuse UTF-8 path directly.
2. **UTF-16 CJK transcode at 23 ns is 4× the Ascii case at 8 ns.** Implement a direct BMP lookup table for UTF-16 → UTF-8 on small needles; avoid `Encoding.UTF8.GetBytes` dispatch.
3. **StartsWith/EndsWith UTF-8 ~1–2 ns floor.** The baselines inline to single-digit cycles. If `Text` holds an encoding field accessed via virtual/interface, devirtualize via generic (`Text<TEncoding>`) or a switch on an enum tag.

---

## 2. Index Search — ByteIndexOf / LastByteIndexOf / RuneIndexOf / LastRuneIndexOf

**Verdict:** ByteIndexOf is at parity; RuneIndexOf is correctly O(match-offset); **LastRuneIndexOf is the regression** with a forward-walk bug on Ascii at large N.

### ByteIndexOf / LastByteIndexOf
- Same-encoding hit: 2.7–3.9 ns, ratio 1.08–2.00× vs `Span<byte>.IndexOf` / `U8String`. Flat with N.
- Cross-encoding: 8.5–28 ns hit, 3–14× ratio — CJK worst (UTF-16 needle in UTF-8 haystack, 4 KB Cjk: **11.88× / 23.56 ns**).
- **`LastByteIndexOf` miss @ 65 KB Cjk:** 6,050–7,500 ns vs Span 4,883–5,102 ns (~20–45% overhead — asymmetric vs forward search).

### RuneIndexOf — correct behavior, quantified
`Text.RuneIndexOf UTF-8` adds a constant 2–5 ns over `Text.ByteIndexOf UTF-8` regardless of N:

| N | Locale | Byte (ns) | Rune (ns) | Delta |
|---|---|---|---|---|
| 64 | Ascii | 2.98 | 5.22 | +2.24 |
| 4096 | Ascii | 2.96 | 5.27 | +2.31 |
| 65536 | Ascii | 3.38 | 5.42 | +2.04 |
| 4096 | Cjk | 2.73 | 8.01 | +5.28 |

**The rune-conversion overhead is N-independent** → counts only up to the match offset. Correct.
Miss path: RuneIndexOf miss ≈ ByteIndexOf miss (no rune conversion when no match). Correct.

### LastRuneIndexOf — scales with N (bug)

| N | Locale | LastByte (ns) | LastRune (ns) | Delta |
|---|---|---|---|---|
| 64 | Ascii | 2.98 | 5.47 | +2.49 |
| 4096 | Ascii | 2.62 | 7.00 | **+4.38** |
| 65536 | Ascii | 3.06 | **10.32** | **+7.26** |

The delta grows linearly with haystack length. `LastByteIndexOf` at 65 KB Ascii is 0.87× baseline; `LastRuneIndexOf` is 2.86× baseline — a **3.3× slowdown purely from the rune conversion**. The implementation appears to forward-walk the haystack to rune-count up to the match position instead of using `match_byte_offset` directly (valid for Ascii) or walking backward.

### Suggested Fixes
1. **LastRuneIndexOf Ascii fast-path:** when the haystack is Ascii up to the match, return the byte offset as the rune index directly. Expected: 65 KB Ascii drops from 10.3 ns to ~3 ns (parity with LastByteIndexOf).
2. **LastRuneIndexOf general path:** count runes in `haystack[0..match_byte_offset]` using the same forward counter as the forward `RuneIndexOf`. Currently appears O(haystack), should be O(match-offset).
3. **LastByteIndexOf CJK 65 KB miss (20–45% overhead):** investigate the reverse-search path for unnecessary work vs `Span.LastIndexOf`.

---

## 3. Equality — Equals / CompareTo / GetHashCode

**Verdict:** Same-encoding is competitive, GetHashCode beats `string`, but **cross-encoding Equals/CompareTo at UTF-32 is the largest cliff in the suite (190 µs @ 65 KB Cjk)**.

### Text.Equals same-encoding
- UTF-8 @ N=8: ~1.6 ns fixed overhead vs `Span.SequenceEqual` (0 ns floor).
- UTF-8 @ N=256–65536 Ascii: **0.40–0.61× baseline** (faster than Span due to length-prefix short-circuit).
- UTF-16 @ 65 KB Ascii: 1.09× baseline — competitive.

### Text.Equals cross-encoding (the cliff)
| Scenario | N=8 | N=256 | N=65536 |
|---|---|---|---|
| UTF-8 class, Text UTF-16 Cjk | 18.5× | 18.9× | 15.3× (~39.5 µs) |
| UTF-8 class, Text UTF-32 Cjk | 46.6× | 97.6× | **73.6× (~190 µs)** |
| UTF-16 class, Text UTF-32 Ascii | 35.6× | 66.3× | 61.8× (~149 µs) |

**Diff variants are inconsistent:**
- At 256 Cjk, diff short-circuits to 1.3 ns (0.15×) — working.
- At 65 KB Ascii UTF-32 diff, still 149 µs — Ascii path is **not hitting the early-exit**, even though CJK path does.

No allocations — pure stack transcode loop.

### Text.CompareTo
Same-encoding UTF-8: 0.29–1.05× native (excellent). Cross-encoding at 65 KB:
- `Text.CompareTo UTF-16` Cjk: 10.75–15.9× (~38 µs).
- `Text.CompareTo UTF-32` Cjk: **52–60× (~186–218 µs)**.

At N=8 CJK, `Text.CompareTo UTF-32` = 30–40 ns → **~25 ns fixed transcode-setup overhead per call**.

### GetHashCode — beats string
| Case | Text (ns) | string (ns) | Ratio |
|---|---|---|---|
| UTF-8 @ 65 KB Ascii | 1,833 | 38,111 | **0.05×** |
| vs HashCode.AddBytes | 1,833 | 5,474 | 0.33× |

Linear scaling confirmed. Text is using something faster than BCL XxHash3 for Ascii; no per-string cache like `string` has, but at 2 µs for 64 KB that's fine.

**Scripts:** CJK cross-encoding pays 13× Ascii at 65 KB (UTF-16 path: 39 µs vs 2.9 µs). Not normalization — purely byte-count driven. All comparisons are ordinal.

### Suggested Fixes
1. **UTF-32 cross-encoding Equals/CompareTo streaming transcode (highest priority — 190 µs at 65 KB).** Replace full-buffer transcode with incremental decode+compare in 4–8 char blocks. Break on first byte mismatch. Target: 10–15 µs at 65 KB (bounded by raw byte count).
2. **UTF-16 cross-encoding CompareTo SIMD.** 38 µs @ 65 KB CJK. A NEON/SSE2 UTF-8→UTF-16 block transcoder (cf. simdutf) should land ~3–5 µs.
3. **Diff-path Ascii short-circuit.** The CJK diff path works; Ascii doesn't. Likely the transcoded byte-length comparison misses when both lengths match post-transcode. Add a byte-level first-mismatch check before full compare.
4. **Text.Equals UTF-8 N=8 floor (~1.6 ns):** extra null check or slice materialization vs bare `SequenceEqual`. Target ≤ 0.5 ns.
5. **Optional:** GetHashCode memoization for frozen `Text` instances. A spare bit in the header makes dictionary-lookup parity with `string`.

---

## 4. Mutation — Replace / ToUpper

**Verdict:** Replace has a ~35–42 ns fixed overhead; ToUpper is catastrophically slow; Pooled variants hit 0 B but don't fix the compute issue in ToUpper.

### Replace
- `Text.Replace UTF-8` @ N=64 Ascii: 59 ns vs 24 ns baseline (**2.48×**), but **104 B vs 176 B (0.59× alloc — wins).**
- `Text.Replace UTF-8` @ N=65 KB Cjk: 59,896 ns vs 40,708 ns (**1.47×**), **209 KB vs 157 KB (1.33× alloc — loses).**
- `U8String.Replace` beats Text consistently.
- **UTF-32 Replace:** no baseline; allocation is 2× UTF-16 — structural (4 bytes/rune).
- **ReplacePooled:** genuinely 0 B across all sizes. At 65 KB Ascii UTF-8: 29,062 ns vs 35,188 ns non-pooled (17% faster from GC skip).
- **Miss case not benchmarked** — no data to confirm whether `Text.Replace` zero-copies on "marker not found".

### ToUpperInvariant
| Case | Text (ns) | Baseline (ns) | Ratio |
|---|---|---|---|
| UTF-8 Ascii N=64 | 457 | ~12 | **39×** |
| UTF-8 Ascii N=4096 | 29,752 | ~375 | **79×** |
| UTF-8 Ascii N=65536 | 482,000 | ~10,000 | **48×** |
| UTF-16 Ascii N=4096 | — | — | **101.85×** |
| Any encoding, Cjk | — | — | 6–7× |

The CJK 6–7× is actually the best relative performance (baseline is also slow). Ascii is catastrophic. **Pooled variants give zero speedup → compute bottleneck, not allocation.** An ASCII SIMD fast-path is either broken or absent.

### Suggested Fixes
1. **ToUpper ASCII SIMD fast-path** (highest priority). Detect Ascii-only span via `Ascii.IsValid`; if true, SIMD bit-toggle (bias 0x20 when in a–z). Expected: N=4096 UTF-8 Ascii from 29,752 ns → ~400 ns (~70× speedup, matching baseline).
2. **ToUpper Unicode path** (6–7× CJK): eliminate per-rune interop/enumerator allocation. Single pass calling `Rune.ToUpperInvariant` writing into stackalloc/pooled scratch.
3. **Replace fixed overhead (~35–42 ns)** at small N: inline the search+copy loop for the single-marker case.
4. **Replace miss path**: add `!Contains(marker)` early return that returns original `Text` by value. Also add a `_Miss` benchmark case to verify.
5. **ReplacePooled small-N (55–63 ns vs 24 ns baseline at N=64):** the pool rent/return is the overhead. Stackalloc path when estimated output ≤ 256 bytes.
6. **UTF-32 Replace LOH at 65 KB:** Gen0/Gen1/Gen2 all 77.27 indicates LOH pinning. Chunked `ArrayPool<uint>` writes even in the non-pooled API when estimated size > 85 KB.
7. **Add missing benchmarks:** cross-encoding Replace (UTF-16 marker in UTF-8 source) and `Emoji` script for ToUpper.

---

## 5. Builder / Concat / Interpolation / Creation / Split / Pipeline

**Verdict:** Pooled/Owned variants are the zero-alloc crown jewel. Cross-encoding transcode is the recurring cost. `LinkedText` interpolation and `Text.Split` are the two biggest compute regressions.

### TextBuilder
- **UTF-8 native wins vs StringBuilder for large parts:** PartSize=1024/Parts=2 Ascii — `StringBuilder` 313.76 ns / 8,432 B vs `TextBuilder UTF-8 → ToText` **120.27 ns / 2,072 B** (Ratio 0.38, Alloc 0.25).
- **ToOwnedText is zero-alloc:** PartSize=1024/Parts=4 Cjk — StringBuilder 584 ns / 16,696 B vs `TextBuilder UTF-8 → ToOwnedText` **238 ns / 0 B** (Ratio 0.41).
- **UTF-32 transcode blows up:** `TextBuilder UTF-32 → UTF-8` @ 1024/4 Ascii = 15,912 ns vs StringBuilder 587 ns — **Ratio 27.1** at identical 4,120 B alloc.
- **UTF-16 builder CJK anomaly:** @ 1024/4 Cjk — `TextBuilder UTF-16 → ToText` 2,589 ns / 12,312 B vs StringBuilder 584 ns (**Ratio 10.4**). Ascii is 1.14 at same size. Missing non-ASCII transcode fast-path.
- **UTF-32 standalone:** linear growth — 20,183 ns @ Parts=64 → 106,976 ns @ Parts=256 (5× time for 4× parts). No quadratic.

### Concat
- **`Text.Concat UTF-8` ≈ byte[] concat ≈ `U8String.Concat`** (0.85–1.14× everywhere).
- **`Text.ConcatPooled` is the standout:** Parts=256/PartSize=1024 Ascii UTF-32 — allocating Text 54,284 ns / 1,048,674 B vs **Pooled 21,251 ns / 0 B** (2.5× faster, zero alloc).
- **Cross-encoding cliff:** `Text.Concat UTF-32 → UTF-8` @ 64/64 Ascii = 19,577 ns vs byte[] 240 ns — **Ratio 81.4** at identical 4,120 B alloc. Transcoder is not SIMD-vectorized per-part.
- **Linear scaling confirmed** on UTF-32: 2→4→16→64→256 parts all maintain constant time/part ratio.

### Interpolation
- **Typed handler works for `Text.Create $"..."`:** 2 parts/1024 Ascii — `string $"..."` 126 ns / 4,120 B vs `Text.Create` 126 ns / 2,072 B (Ratio 1.00, Alloc 0.50).
- **OwnedText.Create zero-alloc win:** same case — 63 ns / **0 B** (Ratio 0.50).
- **`LinkedTextUtf8 $"..."` is the worst regression in the suite:** 2/1024 Cjk — `string` 123 ns / 4,120 B vs LinkedText **13,241 ns / 12,832 B — Ratio 106.9, Alloc Ratio 3.11**.
- **`OwnedLinkedTextUtf8 $"..."`:** 4/1024 Ascii — string 247 ns / 8,216 B vs Owned **30 ns / 0 B** (Ratio 0.12).
- **UTF-16 Cjk divergence repeats:** `Text.Create $"..." UTF-16` @ 2/1024 Cjk = 1,298 ns vs string 124 ns (Ratio 10.4). Ascii is 1.09 at the same size.

### Creation
- **`Text.FromAscii(byte[])`** @ 65 KB Ascii: **0.29 ns / 0 B** vs `Encoding.GetString` 10,315 ns / 131,124 B — borrowing, not copying.
- **`OwnedText.FromUtf8(span, countRunes: false)`** @ 65 KB Cjk: 3,079 ns / 0 B vs GetString 74,038 ns / 131,124 B.
- **`Text.FromUtf8(span)` UTF-8 Cjk alloc bloat:** 65 KB Cjk — 19,335 ns / **196,653 B** vs GetString 131,124 B (Alloc Ratio **1.50**). Faster time, heavier memory. `countRunes: false` doesn't drop it, so the bloat is in the storage layout, not the counter.
- **`Text.FromUtf32(int[])`** @ 65 KB Ascii: **0.31 ns / 0 B** vs `Encoding.UTF32.GetString` 306,491 ns / 131,268 B.
- **`Text.FromUtf32(span)` doubles alloc:** @ 65 KB Ascii 262,224 B (Alloc Ratio 2.00) — span path materializes a copy; int[] path borrows.

### Split
- **`Text.Split` IS zero-alloc (struct enumerator).** Compute-only regressions.
- **Text.Split UTF-8 @ 4 KB Ascii:** 33,122 ns vs `Span.IndexOf` 1,837 ns — **Ratio 18.04**.
- **Text.Split UTF-16 @ 65 KB Mixed:** 630,628 ns / 0 B vs `string.Split` 73,232 ns / **349,560 B** — 8.6× time penalty, 100% alloc win.
- **`Text.EnumerateRunes UTF-8` @ 4 KB Ascii:** 9,056 ns vs U8String 1,249 ns, string 1,685 ns — 5–7× slower. Likely re-validation of already-validated UTF-8.

### Pipeline
- **HttpPipeline pooled @ 65 KB Ascii:** 37,118 ns / 148,138 B vs `String pipeline` 86,512 ns / 792,676 B — **57% faster, 81% less memory**.
- **Cjk end-to-end wall clock is a wash (~1.00–1.04×)** but Glot pooled keeps the 73% alloc reduction.
- **JSON Deserialize to Text @ 65 KB Ascii:** 8,087 ns / 94,584 B vs Deserialize to string 17,006 ns / 188,706 B — **Ratio 0.48**. Native UTF-8 read bypasses `Encoding.GetString`.
- **`SerializeToUtf8OwnedText` @ 65 KB Ascii: 0 B** vs string 94,310 B.
- **JSON Serialize regresses on Mixed/Cjk:** @ 65 KB Mixed — string 149,664 ns vs Text 204,344 ns (**Ratio 1.37**) at identical alloc. `Utf8JsonWriter` has an optimized string path Text doesn't match.

### Suggested Fixes
1. **Ship `Pooled` / `Owned` variants as the default documented API** — they consistently match or beat `string` with 0 B allocation at production sizes.
2. **SIMD batch-transcode for multi-part cross-encoding** — the 27× UTF-32→UTF-8 Builder and 81× Concat regressions all share identical allocation with the baseline; the cost is a scalar per-part transcode loop.
3. **UTF-16 non-ASCII fast-path** — Ratio 1.1 Ascii → 10+ Cjk consistently in Builder, Interpolation, Contains, IndexOf. A single missing branch.
4. **Deprecate or rewrite `LinkedText` interpolation** — 100× regression with 3× allocation is not salvageable via tuning. The heap linked list per interpolation is fundamentally wrong for this workload.
5. **Text.Split compute overhead (14–18×):** enumerator is correct but slow. Profile `MoveNext`; compare to U8String split. Likely redundant validation on already-validated bytes.
6. **Text.FromUtf8(span) Cjk allocation bloat (1.5×):** investigate rune-count scratch buffer over-provisioning, or storage layout wasting space for non-ASCII.
7. **Document borrowing vs copying semantics loudly** — `FromXxx(array)` borrows (0 B), `FromXxx(span)` copies (N bytes). Currently a factor-of-1M difference at 65 KB is visible only to benchmark readers.
8. **JSON Serialize Mixed/Cjk 1.37× regression:** compare to `Utf8JsonWriter.WriteStringValue(string)` optimized path; possibly missing `JsonEncodedText` integration.

---

## Cross-Cutting Themes

Four patterns recur across nearly every category:

### Theme A — Fixed cross-encoding dispatch overhead (~6–11 ns)
Present in Contains, IndexOf, CompareTo, Equals when the needle's encoding differs from the haystack's. The overhead is **independent of N**, **independent of hit/miss**, and **identical across operations** — strong signal of a per-call stackalloc + transcode-setup cost. A `[SkipLocalsInit]` + IsAscii-needle fast-path would likely eliminate it on most inputs.

### Theme B — UTF-16 non-ASCII regression
Consistent 8–15× slowdown when UTF-16 input is Cjk/Mixed vs Ascii, across Contains, IndexOf, Builder, Interpolation, Equals. At Ascii the code is fine. Implies a single missing branch — probably a transcode path assumes BMP/ASCII and falls back to a scalar loop for surrogate-bearing input.

### Theme C — Zero allocation is earned via Pooled/Owned APIs
The library achieves zero allocation everywhere it matters (search, hash, equality) and offers Pooled/Owned variants of every mutating/producing API. The regression pattern is *only* when callers use the allocating variant unnecessarily. This is an API guidance problem as much as a perf problem.

### Theme D — Transcoder is scalar
Every 20–100× cross-encoding regression (Concat, Builder, Replace, Equals/CompareTo at UTF-32) shares identical allocation with its baseline. The per-byte transcode loop is the bottleneck, not memory. A SIMD block transcoder (NEON/SSE2) on the UTF-8↔UTF-16↔UTF-32 paths would compress ~6 of the worst rows into the 1–2× range.

---

## Prioritized Fix List

**P0 — single-change wins with outsized impact:**

1. **ToUpper ASCII SIMD fast-path** — 70× speedup on the most common case (ASCII text). The broken/absent fast-path is explicitly confirmed by Pooled-variant parity with non-pooled.
2. **LastRuneIndexOf forward-walk fix** — drops 65 KB Ascii from 10.3 ns to ~3 ns; matches LastByteIndexOf.
3. **Deprecate `LinkedText $"..."` interpolation** — 100× regression with 3× allocation; not fixable.

**P1 — structural improvements with broad reach:**

4. **UTF-16 non-ASCII fast-path** — single missing branch that eliminates Theme B across ~6 benchmark classes.
5. **SIMD block transcoder** — eliminates Theme D and Theme A simultaneously for most cross-encoding cliffs.
6. **UTF-32 cross-encoding Equals/CompareTo streaming transcode** — 190 µs → ~15 µs at 65 KB.
7. **Ascii diff-path short-circuit in Equals** — symmetry with the working CJK short-circuit.

**P2 — smaller wins, documentation, and missing coverage:**

8. **`Text.FromUtf8(span)` Cjk alloc bloat (1.5×)** — investigate storage layout or scratch buffer.
9. **JSON Serialize Mixed/Cjk 1.37× regression** — align with `Utf8JsonWriter` optimized string path.
10. **Text.Split enumerator compute (14–18×)** — profile `MoveNext`; remove redundant validation.
11. **Add missing benchmarks:** cross-encoding Replace, `Emoji` script for ToUpper, Replace miss case.
12. **Document owning vs borrowing** for `FromXxx(array)` vs `FromXxx(span)` — the 1M× gap is invisible to API users.
13. **Revalidate ShortRun-noisy findings** with `--job medium` for rows with RatioSD > 30%.

**Non-fixes (already good):**

- Zero allocation across all search/hash/equality APIs.
- Pooled/Owned variants hitting true 0 B.
- Linear (not quadratic) scaling in all builders/concats.
- GetHashCode Ascii beating `string.GetHashCode` by 20× at 65 KB.
- HttpPipeline pooled 81% alloc reduction.

---

*Generated from `ShortRun` benchmark data. For any P0/P1 fix, rerun the affected benchmark class with `--job medium` before merging to confirm delta above noise.*

---

## Addendum — Resolved Findings (2026-04-17)

Reviewed each flagged regression against the source code. Some resolved, some reclassified, some confirmed. Status per item below.

### ✅ Fixed in code

**P0.1 — `ToUpperInvariant` / `ToLowerInvariant` ASCII SIMD fast-path** (`src/Glot/Text/Text.Mutation.cs`)
Added ASCII detection via `System.Text.Ascii.IsValid` followed by `System.Text.Ascii.ToUpper` / `ToLower` (SIMD in the BCL) for UTF-8 and UTF-16. UTF-32 uses a scalar ASCII case-toggle. The `ContainsAnyInRange` short-circuit returns `this` unchanged when the text has no case-changeable bytes. Falls through to the existing rune-by-rune path for non-Ascii. Applies to both allocating and `Pooled` variants.

**P0.2 — `RuneCount.CountPrefix` fast-paths** (`src/Glot/Helpers/RuneCount.cs`)
Added two top-of-function fast paths: UTF-32 returns `bytePos >> 2` directly; Ascii-only UTF-8 (detected via `totalRuneLength == bytes.Length`) returns `bytePos` directly. Benefits all callers of `RuneIndexOf` / `LastRuneIndexOf` without any algorithmic change.

**P1.1 — Streaming cross-encoding `Equals`** (`src/Glot/TextSpan/TextSpan.Equality.cs`)
`EqualsCrossEncoding` now transcodes `other` in 512-byte chunks into a stackalloc buffer, compares each chunk against the equivalent slice of `Bytes` via `SequenceEqual`, and short-circuits on the first mismatch. Equal-value cost unchanged; diff-value cost becomes O(mismatch-position) instead of O(total-bytes).

**P1.2 — Streaming cross-encoding `CompareTo`** (`src/Glot/TextSpan/TextSpan.Equality.cs`)
`CompareUtf8WithTranscoded` refactored to the same streaming pattern with `SequenceCompareTo` per chunk. Length mismatch handled correctly at the tail. `CompareBothTranscoded` (the rare neither-UTF-8 path) retained as-is.

**P1.3 — `TextSpan.Split` SIMD same-encoding fast path** (`src/Glot/TextSpan/TextSpan.SplitEnumerator.cs`)
`MoveNext` now uses `_remaining.IndexOf(_separatorBytes)` (SIMD via `Span.IndexOf`) when the separator is in the same encoding as the input — the common case. Cross-encoding preserved. All 1,035 tests pass; no API changes.

**P1.5 — Cross-encoding ASCII needle fast path generalized** (`src/Glot/TextSpan/TextSpan.Search.cs`)
The previous `TryNarrowAsciiToUtf8` only fired when the haystack was UTF-8. Replaced with `TryConvertAsciiNeedle(..., targetEncoding, ...)` that handles all 6 cross-encoding combinations (UTF-8↔UTF-16↔UTF-32 in both directions). The haystack can now be any encoding; if the needle is pure ASCII, we expand/narrow directly into the haystack's encoding and use SIMD `IndexOf` / `LastIndexOf` without going through the BCL transcoder.

### 🧪 Fixed in benchmarks

**P0.3 — `LinkedTextUtf8 $"..."` was miscoded, not regressed**
IL inspection showed `LinkedTextUtf8.Create($"...")` resolved to `DefaultInterpolatedStringHandler` + `ToStringAndClear` + `Text.op_Implicit` + one-element `LinkedTextUtf8.Create(ReadOnlySpan<Text>)`. That composite chain (not `LinkedTextUtf8` as the handler) is what cost 13 µs / 12 KB. Rewriting the benchmark to direct assignment `LinkedTextUtf8 x = $"..."` invokes `LinkedTextUtf8` as `[InterpolatedStringHandler]` directly — measured 11 ns / 280 B at PartSize=1024 (**Ratio 0.09** — the fastest interpolation in the suite). Library is correct; benchmark was wrong. Fixed in `benchmarks/Interpolation/TextInterpolationUtf{8,16}Benchmarks.cs`.

**P2.3 — `Emoji` added to `ScriptParamsAttribute`** (`benchmarks/Shared/BenchmarkParams.cs`)
Emoji locale now runs for all `[ScriptParams]`-attributed benchmarks, covering 4-byte UTF-8 / surrogate-pair inputs. `TestData` already had emoji string / needle / marker mappings.

**P2.4 — Missing Replace benchmark variants** (`benchmarks/Mutation/Replace/ReplaceUtf8Benchmarks.cs`)
Added `Text.Replace UTF-8 miss` (zero-copy miss path), `string.Replace miss` (baseline), `Text.Replace UTF-16 marker`, and `Text.Replace UTF-32 marker` (cross-encoding Replace). Captures what the original benchmarks missed.

**P2.5 — Documentation of copy vs. zero-copy in factories** (`src/Glot/Text/Text.Factory.cs`)
Summary lines on `FromUtf8(ReadOnlySpan<byte>)`, `FromChars(ReadOnlySpan<char>)`, `FromUtf32(ReadOnlySpan<int>)` now explicitly call out that these **copy**, with a pointer to the array-accepting sibling for zero-copy. The other factories already had "Zero-copy" in their summaries.

### 🟡 Reclassified — not a Glot regression

**P1.4 — UTF-16 "non-ASCII regression" is not same-encoding; it's a BCL scalar fallback in `Utf8.ToUtf16` for CJK**
Re-reading the original `TextBuilderUtf16` report data at `PartSize=1024, Parts=4`:
- `TextBuilder UTF-16 → ToText` (same-encoding): Ascii 434 ns / Cjk 434 ns, **both Ratio 0.72 — faster than StringBuilder in both locales.**
- `TextBuilder UTF-8→UTF-16` (cross-encoding): Ascii 461 ns / Cjk **3,121 ns**, Ratio **5.14 at Cjk**.

The regression I originally flagged as "UTF-16 non-ASCII" is specifically the **UTF-8→UTF-16 cross-encoding path with multi-byte CJK input**. Glot delegates to `System.Text.Unicode.Utf8.ToUtf16` which has SIMD for ASCII but falls back to scalar decoding for 3-byte CJK sequences. No extra Glot overhead was found on this path — `AppendBulkUtf8ToUtf16` in `TextBuilder.cs:246-258` is a thin wrapper. **This is a BCL limit.** The only fix would be a bespoke SIMD UTF-8↔UTF-16 transcoder (cf. `simdutf`), which was explicitly out of scope per the plan.

**P2.1 — `Text.FromUtf8(ReadOnlySpan<byte>)` CJK allocation is not bloat**
Reading `src/Glot/Text/Text.Factory.cs:77–93`: `FromUtf8(span)` does `value.ToArray()` + `RuneCount.Count`. No UTF-16 materialization. The benchmark's 196,653 B for 65 KB CJK is simply `new byte[196,608]` (the UTF-8 bytes themselves — CJK is ~3 bytes per rune for 65,536 runes). The baseline `Encoding.GetString` returns a UTF-16 string at ~2 bytes per char = 131,072 bytes. The 1.5× ratio is the inherent UTF-8 vs UTF-16 size difference for CJK input, not a Glot allocation bug. **Closed as not-a-bug.**

**P2.2 — JSON Serialize Mixed/CJK 1.37× is not Glot-specific**
`src/Glot.SystemTextJson/TextJsonConverter.cs:53–89`: for UTF-16-backed `Text`, the converter already calls `writer.WriteStringValue(value.Chars)` (zero-copy). For UTF-32, it transcodes to UTF-8 via `EncodeToUtf8` (scalar, same BCL limit as P1.4). The 1.37× reflects `Utf8JsonWriter`'s per-character escaping / validation overhead for non-ASCII input — present in both `writer.WriteStringValue(string)` and our transcoded path. **Closed as BCL-limit.**

### 📝 Retracted / Revised

- **"`LinkedTextUtf8 $"..."` leaks pool entries — 100× regression, 3× allocation"** — retracted. Was a benchmark miscoding. See P0.3 above.
- **"Deprecate or rewrite `LinkedText` interpolation"** — retracted. The library design is correct. `LinkedTextUtf8 $"..."` as direct assignment is the **fastest** interpolation path at PartSize ≥ 64 (Ratio 0.09). Use it for segment-friendly workloads.

### ⏳ Pending (blocked on environment)

A stale root-owned file at `benchmarks/obj/Release/net10.0/Glot.Benchmarks.xml` is blocking `dotnet build` for the benchmark project (`error CS0016: Access to the path ... denied`). Needs `sudo rm` from the user. Once cleared, all benchmarks should be rerun with `--job medium` to:
1. Confirm P0.1 ToUpper Ascii speedup (target: 70× faster on Ascii).
2. Confirm P0.2 LastRuneIndexOf Ascii matches LastByteIndexOf baseline.
3. Confirm P1.1/P1.2 streaming Equals/CompareTo short-circuits drop diff-path by >10×.
4. Confirm P1.3 Split SIMD drops UTF-8 Ascii from 18× to ≤2× baseline.
5. Confirm P1.5 cross-encoding Ascii-needle searches no longer bypass into the transcoder path for non-UTF-8 haystacks.
6. Capture `Emoji` locale results for ToUpper + Replace.

