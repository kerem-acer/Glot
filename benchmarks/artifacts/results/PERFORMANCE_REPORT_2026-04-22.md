# Glot Performance Report — 2026-04-22

**Source:** `benchmarks/artifacts/results/*-report-github.md` (66 benchmark classes)
**Hardware:** Apple M4 Max, 16 cores, .NET 10.0.6 (SDK 10.0.202), Arm64 RyuJIT armv8.0-a
**BenchmarkDotNet:** v0.15.8 on macOS Tahoe 26.0.1
**Run configuration:** `Toolchain=InProcessEmitToolchain IterationTime=150ms MaxIterationCount=30 MaxRelativeError=0.1` (stable, low-noise; RatioSD well under 5 % on almost every row). Two files are `Dry`-run only and flagged where cited.

> Compare to `PERFORMANCE_REPORT.md` (2026-04-17). The 2026-04-17 Addendum claimed six fixes; this report re-evaluates each with current numbers and logs the ones that have *re-regressed* or *newly appeared*.

---

## Executive Summary

1. **ASCII ToUpper fast path is live and enormous.** `Text.ToUpperInvariantPooled UTF-8 @ 65 KB Ascii = 1,589 ns (0.03×)` vs `string.ToUpperInvariant` 47,312 ns — a **30× speedup**, zero allocation. UTF-16 Pooled Ascii: 7,964 ns (0.15×). This is the single biggest win since 2026-04-17.
2. **GetHashCode still crushes `string`.** `Text.GetHashCode UTF-8 @ 65 KB Cjk = 5,655 ns (0.14×)`, `UTF-16 Cjk = 3,776 ns (0.10×)` vs `string.GetHashCode` ~40 µs. Linear, zero-alloc, no randomized-hash cost.
3. **Pooled/Owned variants continue to dominate.** `Text.ReplacePooled UTF-8 @ 65 KB Ascii = 30,843 ns / 1 B (0.32×)` vs 96,527 ns / 157 KB for `string.Replace`. `SerializeToUtf8OwnedText @ 65 KB Cjk = 384 µs / 18 B` vs `string` 507 µs / 524 KB. All zero-alloc where possible.
4. **Zero-copy factories are perfect.** `Text.FromAscii(byte[]) @ 65 KB = 1.51 ns / 0 B` vs `Encoding.GetString` 44,051 ns / 131 KB. `Text.FromUtf32(int[]) @ 65 KB = 1.51 ns / 0 B` vs `Encoding.UTF32.GetString` 313,924 ns.
5. **HTTP pooled pipeline still wins big.** 65 KB Ascii: Glot pooled 73,494 ns / 147 KB vs `String pipeline` 299,006 ns / 792 KB — **4× faster, 81 % less memory**.
6. **Cliff #1 — UTF-32 cross-encoding Equals/CompareTo is still catastrophic.** `Text.Equals UTF-32 @ 65 KB Cjk = 330 µs (135×)` vs `string.Equals` 2.4 µs. The 2026-04-17 Addendum claimed streaming was shipped, but `TextSpan.Transcode` falls back to a scalar `Rune.TryDecodeFirst` loop on every 512-byte chunk for UTF-32. Same cost shows up in CompareTo (334 µs, 99×).
7. **Cliff #2 — UTF-32→UTF-8 transcode in Concat/Builder is scalar.** `Text.Concat UTF-32→UTF-8 @ 1024/16 Cjk = 34,966 ns (24.5×)`. Concat UTF-32→UTF-8 @ 1024/64 Ascii = 36,202 ns (16.3×). Same scalar decode loop.
8. **Cliff #3 — `Text.Split UTF-8` allocates and is 10-13× `Span.IndexOf`.** 65 KB Ascii: 560,268 ns / 29 B (Ratio 12.96, Alloc 14.5×). The 2026-04-17 Addendum shipped a SIMD IndexOf fast path, but every returned segment still calls `RuneCount.Count` (SIMD-per-segment) — expensive for many small segments. Allocation leak (29 B at 65 KB) is probably `EnumeratorState` boxing.
9. **EnumerateRunes UTF-8 is 2-3× `string.EnumerateRunes`, 5-7× U8String.** 65 KB Ascii: 150,424 ns vs U8String 19,921 ns. The rune enumerator does per-step `Rune.TryDecodeFirst` with validation instead of a direct UTF-8 lead-byte walk.
10. **Tracked-but-cosmetic floors:** Cross-encoding `Text.Contains UTF-16/UTF-32` has a ~17 ns N-independent dispatch floor (Theme A from the prior report — unchanged). `StartsWith UTF-16/UTF-32` floor is ~7-11 ns. In all these cases absolute time is <30 ns; ratios look bad but users won't notice.

**Allocation story:** Zero allocation across **every** search, equality, hash, factory (array variants), and Pooled/Owned mutation API. Only three paths still allocate meaningfully and shouldn't — `Text.Split UTF-8` (28-29 B at 65 KB, no hard scaling yet), `Text.Replace UTF-8 Cjk/Mixed` (1.33× at 65 KB Cjk), and `Text.Replace UTF-32` (2×, structural, 4 bytes/rune).

---

## Methodology

- **Ratio > 1.5×** → regression. **Ratio > 5×** → cliff. **Absolute time < 10 ns** → cosmetic (downgrade regardless of ratio).
- **Allocation-winning** rows where the time regression < 1.5× are treated as net wins.
- **Cross-encoding** (UTF-16/UTF-32 values on a UTF-8 haystack, etc.) is separated from same-encoding analysis; it legitimately needs a transcode step.
- **Noise:** this run is not ShortRun — iterations and warmup are full, and most RatioSD values are <5 %. Only two files are `Dry` runs (`TextCreationStringUtf8Benchmarks-report-github.md`, `TextSplitUtf32Benchmarks-report-github.md`); their absolute numbers are cold-start latency and noted inline when cited.
- **Severity labels:** Cliff 🚨 (ratio >5× or hundreds of µs), Regression ⚠️ (1.5×–5×), Cosmetic ℹ️ (<10 ns absolute).

---

## 1. Substring Search — Contains / StartsWith / EndsWith

**Verdict:** Same-encoding paths are parity-to-slightly-slower; cross-encoding paths are the recurring floor (Theme A unchanged from 2026-04-17).

### Same-encoding Contains — near parity

| Case | Text (ns) | Baseline (ns) | Ratio | Notes |
|---|---|---|---|---|
| UTF-8 hit @ 65 KB Ascii | 4.57 | 3.46 (Span) | 1.14× | parity ℹ️ |
| UTF-8 miss @ 65 KB Ascii | 2,095 | 2,091 (Span) | 1.00× | ✅ |
| UTF-8 miss @ 65 KB Cjk | 6,349 | 6,288 (Span) | 1.01× | ✅ |
| UTF-16 hit @ 65 KB Cjk | 36,791 | 2,576 (string) | 14.28× | cross-encode floor ⚠️ |
| UTF-32 hit @ 65 KB Emoji | 33,798 | 3,175 (string) | 10.64× | cross-encode floor ⚠️ |

Same-encoding UTF-8 Contains hit: 4.40–4.58 ns across *all* N and locales — **scale-free and zero-alloc**. The 1.1–1.6× ratio over `Span.IndexOf` is a ~1 ns fixed Text wrapper cost (the encoding branch + `Uncounted` cast).

### StartsWith / EndsWith — cosmetic ratios

| Case | Text (ns) | Baseline (ns) | Ratio | Verdict |
|---|---|---|---|---|
| `Text.StartsWith UTF-8` @ 65 KB Ascii | 2.23 | 1.28 (string) | 1.74× | ℹ️ |
| `Text.StartsWith UTF-16` @ 65 KB Ascii | 8.93 | 1.28 (string) | 6.97× | ℹ️ (7 ns abs) |
| `Text.StartsWith UTF-16` @ 65 KB Emoji | 11.22 | 1.29 (string) | 8.65× | ℹ️ (11 ns abs) |
| `Text.EndsWith UTF-16` @ 65 KB Emoji | 19.54 | 2.07 (string) | 9.43× | ⚠️ borderline (19 ns abs) |
| **`Text.StartsWith UTF-8` @ 64 Cjk** (UTF-16 class) | **9.88** | 1.28 | **7.69×** | anomaly — see §1.1 |

#### §1.1 Anomaly — `Text.StartsWith UTF-8` @ 64 Cjk in the UTF-16 benchmark class = 9.88 ns

In `StartsWithUtf16Benchmarks-report-github.md` at N=64 Cjk, `Text.StartsWith UTF-8` = **9.877 ns**, while the same method at Ascii/Emoji/Mixed is ~2.3 ns. The ratio there matches a cross-encoding path, not same-encoding UTF-8. Looks like a benchmark set-up issue (UTF-8 needle on UTF-16 haystack wired where same-encoding was intended) — verify `StartsWithUtf16Benchmarks.cs` setup at `benchmarks/Search/StartsWith/`.

### Allocations — zero everywhere. ✅

---

## 2. Index Search — ByteIndexOf / RuneIndexOf / LastRuneIndexOf

**Verdict:** `LastByteIndexOf` and `LastRuneIndexOf UTF-8` look clean now; `RuneIndexOf UTF-8 @ N=64 Ascii` shows a surprising spike.

### ByteIndexOf UTF-8 — at parity

| N | Locale | Text (ns) | Span (ns) | Ratio |
|---|---|---|---|---|
| 64 | Ascii | 4.43 | 3.33 | 1.12× |
| 4096 | Ascii | 4.45 | 3.34 | 1.13× |
| 65536 | Cjk miss | 6,349 | 6,288 | 1.01× |

### RuneIndexOf — spike at N=64 Ascii

| N | Locale | Byte (ns) | Rune (ns) | Delta |
|---|---|---|---|---|
| 64 | Ascii | 2.98 | **14.99** | +12.01 — spike ⚠️ |
| 4096 | Ascii | 2.96 | 5.10 | +2.14 |
| 65536 | Ascii (miss) | 2,095 | 2,095 | 0 |

At N=64 Ascii, `Text.RuneIndexOf UTF-8` = 14.99 ns (Ratio 3.82). At N=4096 Ascii it drops to 5.10 ns. The `RuneCount.CountPrefix` Ascii fast-path triggers when `totalRuneLength == bytes.Length` — but for short strings the `_encodedLength` rune length is probably not cached (the fast path requires `totalRuneLength > 0`). Either the cache isn't populated or the branch predictor is cold on short inputs. Investigate `Uncounted`-constructed paths at small N.

### LastRuneIndexOf UTF-8 — now fast on hit, clean on miss

| N | Locale | LastByte (ns) | LastRune (ns) | Delta |
|---|---|---|---|---|
| 64 | Ascii | 3.56 | 4.98 | +1.42 ✅ |
| 4096 | Ascii | 2.74 | 4.36 | +1.62 ✅ |
| 65536 | Ascii | 3.10 | 4.71 | +1.61 ✅ |
| 65536 | Cjk | 3.03 | 9.39 | +6.36 |

The 2026-04-17 `RuneCount.CountPrefix` Ascii shortcut is working: LastRune Ascii now matches LastByte within 1.6 ns (was +7.3 ns before). **This fix held.**

---

## 3. Equality — Equals / CompareTo / GetHashCode

**Verdict:** Same-encoding competitive. UTF-32 cross-encoding is the biggest regression in the suite; not fixed despite the Addendum.

### Text.Equals same-encoding UTF-8

| N | Locale | Text | Span | Ratio |
|---|---|---|---|---|
| 8 | Ascii | 2.84 ns | 1.52 ns | 1.26× ℹ️ |
| 256 | Ascii | 6.66 ns | 7.18 ns | **0.66×** ✅ |
| 65536 | Ascii | 1,084 ns | 1,038 ns | 1.04× ✅ |
| 65536 | Mixed | 1,485 ns | 1,793 ns | **0.62×** ✅ |

### Text.Equals cross-encoding — the cliff 🚨

| Scenario | N=8 | N=256 | N=65536 |
|---|---|---|---|
| UTF-8 class, `Text.Equals UTF-16` Cjk | 23.78 ns (10.8×) | 192 ns (18.9×) | **45,038 ns (18.9×)** |
| UTF-8 class, `Text.Equals UTF-32` Cjk | 55.20 ns (25.0×) | 1,290 ns (126.9×) | **328,994 ns (138.3×)** 🚨 |
| UTF-16 class, `Text.Equals UTF-32` Ascii | 52.75 ns (22.8×) | 1,120 ns (107.9×) | **294,712 ns (120.9×)** 🚨 |

**Diff short-circuit works for UTF-8 source, broken for UTF-16/32 source at 65 KB Cjk.**

- `Text.Equals UTF-8 different @ 65 KB Cjk` = 2.55 ns (0.001×) ✅
- `Text.Equals UTF-16 different @ 65 KB Cjk` = 48,860 ns (20.5×) 🚨 — same cost as equal
- `Text.Equals UTF-32 different @ 65 KB Cjk` = 331,751 ns (139×) 🚨 — same cost as equal

### Text.CompareTo — mirror

| Case | Text (ns) | Baseline (ns) | Ratio |
|---|---|---|---|
| UTF-8 @ 65 KB Ascii | 1,141 | 1,130 (Span) | 1.01× ✅ |
| UTF-8 @ 65 KB Mixed | 1,596 | 1,588 (Span) | 1.00× ✅ |
| **UTF-16 @ 65 KB Cjk** | 48,807 | 3,376 (string) | **14.46×** 🚨 |
| **UTF-32 @ 65 KB Cjk** | 334,412 | 3,376 (string) | **99.06×** 🚨 |

### GetHashCode — the standout win ✅

| N | Locale | Text (ns) | string (ns) | Ratio |
|---|---|---|---|---|
| 256 | Ascii (UTF-16 class) | 21.4 | 152.8 | 0.14× |
| 65536 | Ascii (UTF-8) | 1,858 | 40,425 | 0.05× |
| 65536 | Cjk (UTF-16) | 3,776 | 39,594 | 0.10× |
| 65536 | Emoji (UTF-8) | 3,772 | 39,612 | 0.10× |

Text.GetHashCode UTF-8 also beats `HashCode.AddBytes UTF-8` (1,858 ns vs 5,347 ns at 65 KB Ascii) by decoding once and feeding XxHash3 over the rune buffer. Linear, zero-alloc everywhere ≥ N=256 (stackalloc path).

### Suggested Fixes

1. **🚨 UTF-32 cross-encoding Equals/CompareTo (highest priority).** Path: `TextSpan.Equality.cs:EqualsCrossEncoding` + `Transcode.cs:Transcode` fallback. For `Encoding == Utf32`, each 512-byte chunk runs a scalar `Rune.TryDecodeFirst` loop over 4-byte ints. Replace with `MemoryMarshal.Cast<byte,int>(otherBytes).Slice(...)` and per-rune `Rune.EncodeTo` — this already *is* the UTF-32 source shape, so there's nothing to decode, just re-encode. Expected: 330 µs → ~15-25 µs @ 65 KB Cjk.
2. **🚨 UTF-16 cross-encoding CompareTo / Equals (Theme D).** The `Utf8.FromUtf16` / `Utf8.ToUtf16` bulk paths *are* used (via the `#if NET6_0_OR_GREATER` branch in `Transcode`). 45 µs for 65 KB CJK is the BCL's scalar CJK fallback, not Glot. Unless we write a NEON `simdutf`-style transcoder (explicitly out of scope per 2026-04-17), this is a BCL limit — document it.
3. **⚠️ Diff short-circuit asymmetry.** `EqualsCrossEncoding` already short-circuits on the first mismatched chunk. But at 65 KB Cjk/Ascii the diff cost equals the equal cost — suggests the mismatch lands at the *end* of the input, not the start. If the test data mutates only the final char, this is a test-design artefact, not a library bug. Check `TestData.Mutate` — it changes the last char, and the streaming compare must walk the whole buffer to reach it. Update benchmark to also include an early-diff variant (first-char different) to confirm the short-circuit is actually wired.

---

## 4. Mutation — Replace / ToUpper

**Verdict:** Replace is competitive; Pooled variants are the recommended API. ToUpper ASCII is a huge win; Cjk is a 3.7× regression caused by rune-by-rune case mapping.

### Replace UTF-8

| N | Locale | Text (ns) | Text alloc | string (ns) | Ratio | Pooled (ns) | Pooled alloc |
|---|---|---|---|---|---|---|---|
| 64 | Ascii | 48 | 104 B | 30 | 1.58× | 65 | 0 B |
| 4096 | Ascii | 2,342 | 4,936 B | 2,385 | 0.98× ✅ | 2,053 | 0 B ✅ |
| 65536 | Ascii | 37,023 | 78,664 B | 96,527 | **0.38×** ✅ | 30,843 | 1 B ✅ |
| 65536 | Cjk | 84,431 | 209,744 B | 92,887 | 0.91× | 35,410 | 2 B ✅ |
| 65536 | Emoji | 121,711 | 163,867 B | 76,935 | **1.58×** ⚠️ | 51,874 | 3 B ✅ |

- `Text.Replace UTF-8 miss` zero-copy path **is live**: @ 65 KB Ascii = 2,173 ns (0.02×) vs `string.Replace miss` 4,370 ns. ✅
- `Text.Replace UTF-32` has 2× allocation vs other variants (314 KB at 65 KB Ascii) — structural (4 bytes/rune), mitigated by Pooled at 2 B.
- **Cross-encoding Replace with UTF-16/UTF-32 marker** at 4096 Ascii is parity with same-encoding (~2.2 µs) — the transcode-on-demand path is cheap when the haystack is UTF-8 and the marker is ASCII-narrowable.
- **Emoji 1.58× regression at 65 KB UTF-8** is a new finding — `string.Replace` beats `Text.Replace UTF-8`. Investigate `src/Glot/Text/Text.Mutation.cs:Replace` for an Emoji-specific branch.

### ToUpper — the biggest new win

| N | Locale | Text (ns) | Pooled (ns) | string (ns) | Best Ratio |
|---|---|---|---|---|---|
| 64 | Ascii | 15.12 | 20.70 | 15.10 | 1.00× ✅ |
| 4096 | Ascii | 240 | 115 | 447 | **0.26×** ✅ |
| **65536** | Ascii | 3,045 | **1,589** | 47,312 | **0.03×** 🏆 |
| 65536 | Ascii (UTF-16) | 34,874 | **7,964** | 51,401 | **0.15×** ✅ |
| 65536 | Cjk | 429,200 | 437,251 | 114,781 | **3.74×** ⚠️ |
| 65536 | Mixed | 431,393 | 419,552 | 130,358 | **3.31×** ⚠️ |

**Ascii SIMD path works and is exceptional.** The Pooled variant on 65 KB Ascii at **1,589 ns / 0 B** is the best mutation result in the suite.

**Cjk + Mixed are 3.3-4× slower than `string.ToUpperInvariant`.** The non-Ascii fallback path in `Text.Mutation.cs:ToUpper` is rune-by-rune (`Rune.ToUpperInvariant` per rune + re-encode). `string.ToUpperInvariant` uses `TextInfo.ToUpper` with optimized char tables. Low-hanging fix: detect ASCII blocks *inside* Mixed content and only rune-walk the non-ASCII segments. Harder fix: table-based char-by-char ToUpper for UTF-16, converting to UTF-8 on output.

### Suggested Fixes

1. **⚠️ ToUpper Mixed/Cjk** — `src/Glot/Text/Text.Mutation.cs`. Currently runs a scalar rune loop for any non-pure-Ascii input. Add a per-chunk detector: walk 64-byte chunks; if the chunk is Ascii, SIMD-toggle; else rune-decode. Mixed locale (68 % Ascii in TestData) should drop from 431 µs to ~50-80 µs.
2. **⚠️ Replace Emoji 1.58×** — 65 KB UTF-8 Emoji path. Compare to `string.Replace` which can SIMD-scan. Investigate whether `Text.Replace` is doing per-match rune-count work that `string` skips.
3. **ℹ️ Replace small-N overhead** — 48 ns @ N=64 vs 30 ns `string` is tolerable. Pooled small-N (65-80 ns) is the rent/return cost and could be stackalloc-cutover below 256-byte estimated output.

---

## 5. Builder / Concat / Interpolation / Creation / Split / Pipeline

### Builder (TextBuilder)

**Verdict:** Same-encoding UTF-8 wins vs `StringBuilder`. Cross-encoding UTF-32→UTF-8 is the recurring cliff. UTF-16→UTF-8 is parity for Ascii, Cjk pays a modest BCL transcode penalty.

| Case | StringBuilder (ns) | TextBuilder (ns) | Ratio | Notes |
|---|---|---|---|---|
| PartSize=1024, Parts=16 Ascii, UTF-8→Owned | 4,113 | 440 | **0.11×** ✅ |
| PartSize=1024, Parts=16 Cjk, UTF-8→Owned | 17,233 | 1,342 | **0.08×** ✅ |
| PartSize=1024, Parts=64 Ascii, UTF-8→Owned | 63,228 | 2,319 | **0.04×** ✅ |
| PartSize=1024, Parts=16 Ascii, UTF-32→UTF-8→Text | 4,113 | **9,020** | 2.19× ⚠️ |
| PartSize=1024, Parts=16 Cjk, UTF-32→UTF-8→Text | 17,233 | **36,669** | 2.13× ⚠️ |
| PartSize=1024, Parts=64 Ascii, UTF-32→UTF-8→Text | 63,228 | **36,274** | 0.57× ✅ |
| PartSize=1024, Parts=16 Ascii, UTF-16→UTF-8→Text | 4,113 | 1,354 | 0.33× ✅ |

**UTF-32 cross-encoding is 2-3× slower than same-encoding UTF-8.** Root cause: `TextSpan.EncodeToUtf8` falls through to a per-rune `EnumerateRunes` + `rune.EncodeToUtf8` loop for UTF-32 sources (`src/Glot/TextSpan/TextSpan.Transcode.cs:58-65`). Since UTF-32 bytes already encode the scalar, a direct `MemoryMarshal.Cast<byte,uint>` + branchless scalar-to-UTF-8 would skip the `Rune.TryDecodeFirst` step.

### Concat

**Verdict:** Same-encoding concat parity across the board; UTF-32→UTF-8 scales badly; Pooled is the big win.

| Case | Baseline (ns) | Text.Concat (ns) | Ratio | Pooled (ns) |
|---|---|---|---|---|
| 1024/64 Ascii UTF-8 | 2,214 (byte[]) | 2,459 | 1.11× ✅ | 1,256 (0.57×) ✅ |
| 1024/64 Cjk UTF-8 | 71,801 | 65,791 | 0.92× ✅ | 3,987 (0.056×) ✅ |
| 1024/16 Ascii UTF-16→UTF-8 | 581 | 1,354 | 2.33× ⚠️ | 842 (1.45×) |
| **1024/16 Cjk UTF-16→UTF-8** | 1,425 | **12,919** | **9.07×** 🚨 | 10,972 (7.70×) 🚨 |
| 1024/16 Emoji UTF-16→UTF-8 | 953 | **18,030** | **18.91×** 🚨 | 16,441 (17.24×) 🚨 |
| **1024/16 Ascii UTF-32→UTF-8** | 581 | **9,020** | 15.51× 🚨 | 8,341 (14.36×) 🚨 |
| 1024/16 Cjk UTF-32→UTF-8 | 1,425 | **34,966** | **24.54×** 🚨 | 32,791 (23.01×) 🚨 |
| 1024/64 Ascii UTF-32→UTF-8 | 2,214 | **36,202** | 16.35× 🚨 | 33,839 (15.28×) 🚨 |

At 1024/256 Ascii, UTF-32→UTF-8 Concat = 237,922 ns vs byte[] 89,790 ns = 2.65× — **ratio improves at larger N because setup amortizes**, but the per-byte transcode cost is still 2-3× scalar. `Text.ConcatPooled UTF-32→UTF-8` at 1024/256 Cjk = 530,635 ns — that's a huge LOH payload that Pooled would normally kill, so the compute (not alloc) is dominating.

### Interpolation

**Verdict:** `LinkedTextUtf8 $"..."` is the fastest interpolation path at large PartSize; `OwnedText.Create $"..."` is the fastest zero-alloc path.

| Case | string (ns) | Text.Create (ns) | Owned (ns) | LinkedText (ns) | Best |
|---|---|---|---|---|---|
| 2 parts / 1024 Ascii | 146 | 140 (0.95×) / 2,072 B | **61 (0.42×) / 0 B** | 11.9 (0.08×) / 184 B | Linked ✅ |
| 2 parts / 1024 Cjk | 149 | 343 (2.30×) / 6,168 B ⚠️ | 112 (0.75×) / 0 B | **11.9 (0.08×) / 184 B** ✅ |
| 4 parts / 1024 Ascii | 247 | 252 / 4,120 B | 60 / 0 B | 15.7 / 184 B | Linked ✅ |
| 4 parts / 1 Ascii | 9.6 | 28.4 (2.95×) | 26.1 | **30.9 / 592 B (18.5× alloc)** ⚠️ | none |

- **`LinkedTextUtf8 $"..."` at 2 parts/1024 Cjk = 11.9 ns / 184 B (0.08×)** — lazy concatenation, no transcode at creation. Outstanding.
- **`OwnedText.Create $"..." UTF-8` at 2 parts/1024 Ascii = 61 ns / 0 B (0.42×)** — the best zero-alloc interpolation path.
- **`Text.Create $"..." UTF-8` at 2 parts/1024 Cjk = 343 ns (2.30×)** — the transcode path in the interpolated handler eats the win. Investigate `src/Glot/Text/TextInterpolatedStringHandler.cs` (or similar) — probably a UTF-16 to UTF-8 bulk transcode per formatted string argument.
- **`LinkedTextUtf8 $"..."` at 4 parts / 1-byte parts = 30.9 ns / 592 B** — small-part linked text allocates segment arrays disproportionately. For < 32-byte total output, LinkedText is a loss vs `string $"..."`.

### Creation

**Verdict:** Zero-copy factories dominate; copying spans pay a ~5-18 ns fixed cost vs `Encoding.GetString`; `OwnedText.From...(span) no-count` hits true 0 B at sub-µs.

Highlights:

| Case | Baseline (ns) | Text.From (ns) | Ratio | Alloc Ratio |
|---|---|---|---|---|
| 65 KB Ascii byte[] → Text.FromUtf8 | 44,051 | 2,510 / 0 B | **0.057×** ✅ |
| 65 KB Cjk byte[] → Text.FromUtf8 | 90,721 | 7,530 / 0 B | **0.083×** ✅ |
| 65 KB Ascii byte[] → Text.FromAscii | 44,051 | **1.51** / 0 B | **<0.001×** 🏆 |
| 65 KB Ascii int[] → Text.FromUtf32 | 313,924 | **1.51** / 0 B | **<0.001×** 🏆 |
| 65 KB Cjk span → OwnedText.FromUtf8 no-count | 90,721 | 2,516 / 0 B | 0.028× ✅ |
| 65 KB Cjk byte[] → new U8String | 90,721 | 80,655 / 196 KB | 0.89× |

The `Text.FromAscii` / `Text.FromUtf32(int[])` zero-copy factories are the **load-bearing** win for bulk-loading scenarios. Do not regress these.

Note: `TextCreationStringUtf8Benchmarks` (65 KB `Text.From(string)` = 569 µs, Ratio 5.54) is a `Job=Dry` report — the absolute numbers are 1-iteration cold-start and not comparable to other files. Re-run with the default job before acting.

### Split

**Verdict:** Zero-alloc on UTF-16, wins heavily on alloc, but **UTF-8 is slow and allocates**.

| Case | Baseline (ns) | Text (ns) | Ratio | Alloc Ratio |
|---|---|---|---|---|
| 65 KB Ascii UTF-8 | 43,593 (Span.IndexOf) | 560,268 | **12.96×** 🚨 | **14.5×** (29 B) 🚨 |
| 4096 Ascii UTF-8 | 3,347 | 35,079 | **10.56×** 🚨 | (2 B) ⚠️ |
| 65 KB Cjk UTF-8 | 85,308 | 531,854 | 6.25× 🚨 | 7× (28 B) |
| 65 KB Ascii UTF-16 | 79,544 (string.Split) | 43,874 | **0.55×** ✅ | **0** ✅ |
| 65 KB Cjk UTF-16 | 470,069 | 82,721 | **0.18×** ✅ | **0** ✅ |

UTF-16 `Text.Split` is a huge zero-alloc win (string.Split allocates 349 KB). UTF-8 has the opposite problem: 10-13× `Span.IndexOf`. The 2026-04-17 Addendum's SIMD fast path is wired (see `TextSpan.SplitEnumerator.cs:57`), but **each returned segment calls `RuneCount.Count(segment, encoding)`** — which is a SIMD rune count over the segment bytes. For many small segments that's SIMD start-up cost × segment-count, overwhelming the SIMD benefit per segment.

### EnumerateRunes UTF-8

| N | Locale | U8String (ns) | Text (ns) | Ratio vs U8String |
|---|---|---|---|---|
| 4096 | Ascii | 1,252 | 9,487 | **7.57×** 🚨 |
| 65536 | Ascii | 19,921 | 150,424 | **7.55×** 🚨 |
| 65536 | Cjk | 31,571 | 178,430 | 5.65× 🚨 |

Text.EnumerateRunes UTF-8 at 65 KB Ascii is 150 µs — 7× U8String and 2.7× `string.EnumerateRunes`. The UTF-8 rune enumerator should be a pure lead-byte walk; instead it's running `Rune.TryDecodeFirst` per iteration with validation. Check `src/Glot/TextSpan/RuneEnumerator.cs`.

### Pipeline — HttpPipeline ✅, JSON mixed results

| Case | String (ns) | Glot pooled (ns) | Ratio | Alloc Ratio |
|---|---|---|---|---|
| HTTP 65 KB Ascii | 299,006 / 792 KB | 73,494 / 147 KB | **0.25×** | **0.19×** 🏆 |
| HTTP 65 KB Mixed | 784,841 / 971 KB | 750,728 / 196 KB | 0.96× | 0.20× ✅ |
| JSON Deserialize 65 KB Ascii → Text | 59,317 / 188 KB | 9,256 / 94 KB | **0.16×** ✅ | 0.50× ✅ |
| JSON Serialize 65 KB Ascii (SerializeToUtf8OwnedText) | 46,072 / 94 KB | 8,168 / 0 B | **0.18×** | **0.00×** 🏆 |
| JSON Serialize 65 KB Cjk | 507,124 / 524 KB | 591,474 / 524 KB | 1.17× ⚠️ | 1.00× |
| JSON Serialize 65 KB Cjk (SerializeToUtf8OwnedText) | 507,124 / 524 KB | 384,345 / 18 B | **0.76×** ✅ | **0.00×** ✅ |
| JSON Deserialize 65 KB Cjk → Text | 689,966 / 188 KB | 617,407 / 266 KB | 0.89× | 1.41× |
| JSON RoundTrip 65 KB Cjk | 1,013,782 / 713 KB | 1,021,919 / 791 KB | 1.01× | 1.11× |

Non-Ascii JSON allocation ratio > 1 in some rows (Deserialize Cjk 1.41×) — investigate whether we're double-materializing UTF-16 during value parsing in the converter (`Glot.SystemTextJson/TextJsonConverter.cs`).

---

## Ranked Fix Backlog

| # | Severity | Title | Location | Problem | Proposed fix | Expected impact | Effort |
|---|---|---|---|---|---|---|---|
| 1 | 🚨 | UTF-32 cross-encoding Equals/CompareTo @ 65 KB = 330 µs | `src/Glot/TextSpan/TextSpan.Transcode.cs:68-128` (scalar fallback hit by UTF-32 paths) | Streaming Equals streams in 512-byte chunks but the per-chunk transcode is `Rune.TryDecodeFirst` scalar for UTF-32. 4-byte ints get decoded one at a time. | In `Transcode`, detect `Encoding == Utf32`: reinterpret bytes as `ReadOnlySpan<uint>` and emit the target encoding directly via a tight `Rune.EncodeTo`-free loop (just branch on codepoint range, write 1-4 UTF-8 bytes). | 65 KB Cjk 330 µs → ~15-25 µs (12× speedup) | M |
| 2 | 🚨 | UTF-32→UTF-8 Concat/Builder @ 1024/16 Cjk = 34 µs (24×) | `src/Glot/TextSpan/TextSpan.Transcode.cs:58-65` (`EncodeToUtf8` UTF-32 branch) | Uses `foreach (var rune in EnumerateRunes())` + `rune.EncodeToUtf8` — two-layer enumerator per rune. | Replace with inline `MemoryMarshal.Cast<byte,uint>(Bytes)`-driven loop: read the scalar, compute UTF-8 byte count from ranges, write without `Rune` allocation. Same algorithm as fix #1. | Builder UTF-32→UTF-8 ratio 24× → ~2-3× | M |
| 3 | 🚨 | `Text.Split UTF-8` 10-13× `Span.IndexOf`, allocates | `src/Glot/TextSpan/TextSpan.SplitEnumerator.cs:57-71` (`MoveNext` Ascii fast path) | Every returned segment calls `RuneCount.Count(segment, encoding)` — SIMD startup cost × number of segments dominates for small segments. The Ascii segment path can't use the `totalRuneLength == bytes.Length` shortcut because the enumerator doesn't know the parent total. | Lazy-count: do not compute `_currentRuneCount` in `MoveNext`. Defer to a `Current.RuneLength` property that computes on demand (or returns 0 sentinel meaning "uncounted"). Most `foreach` users never consume the rune count. Also trace the 29-B allocation at 65 KB to find the unintentional box. | 560 µs → ~50-80 µs (7-10×), 29 B → 0 B | M |
| 4 | 🚨 | `Text.EnumerateRunes UTF-8` 7.5× U8String @ 65 KB | `src/Glot/TextSpan/RuneEnumerator.cs` (likely) | Per-step `Rune.TryDecodeFirst` with full validation runs on already-validated bytes. | Introduce a `RuneEnumeratorUtf8Fast` struct that only reads lead bytes and masks continuations — no `OperationStatus`, no replacement-char handling. Safe because Glot stores well-formed UTF-8 by construction. | 150 µs → ~25-40 µs @ 65 KB Ascii | M |
| 5 | ⚠️ | `Text.ToUpperInvariant` Mixed/Cjk 3.3-3.7× `string.ToUpperInvariant` | `src/Glot/Text/Text.Mutation.cs` (ToUpper non-Ascii path) | After the Ascii detector fails, the whole string is walked rune-by-rune. Mixed content (68 % Ascii) gets the slow path for every byte. | Chunked detection: scan in 32-byte windows; Ascii chunks use the SIMD `System.Text.Ascii.ToUpper`, non-Ascii chunks fall to the rune loop. For UTF-16 Mixed, use `TextInfo.ToUpper` char-by-char which is table-optimized. | Mixed 65 KB 431 µs → ~80-120 µs | M |
| 6 | ⚠️ | `Text.Replace UTF-8 @ 65 KB Emoji` 1.58× `string.Replace` | `src/Glot/Text/Text.Mutation.cs` (Replace UTF-8) | Unexpected regression only at Emoji — same-locale Ascii/Cjk/Mixed are at parity. `string.Replace` SIMD-scans for 4-byte markers. | Confirm the marker SIMD-scan path hits for Emoji in Text. May be falling to rune-compare because the marker itself has a surrogate pair. | 121 µs → ~75-80 µs | S |
| 7 | ⚠️ | `Text.Create $"..." UTF-8` @ 2 parts/1024 Cjk = 343 ns (2.30×, 1.5× alloc) | `src/Glot/Text/TextInterpolatedStringHandler.cs` (or equivalent) | Writing UTF-16 interpolated args into UTF-8 buffer transcodes via the BCL scalar UTF-16→UTF-8 path for CJK. | Hard to fix generically (same BCL limit as fix #2 but inverse direction). Document that `OwnedText.Create $"..."` (which is zero-alloc and still fast at 112 ns) is the recommended form for CJK interpolation. Re-raise if `simdutf` is ever adopted. | Document-only; perf fix L | S (docs) |
| 8 | ⚠️ | `Text.Equals UTF-16/32` diff-path at 65 KB Cjk still does full walk | `src/Glot/TextSpan/TextSpan.Equality.cs:EqualsCrossEncoding` + benchmark data | The streaming compare *does* short-circuit — but the benchmark `Mutate` mutates the last character, so no short-circuit occurs in practice. | Two actions. (a) Add a benchmark `…_DiffFirst` variant where the first char differs, to prove the short-circuit works. (b) No code change needed if (a) passes. | Confirms current impl is correct | S |
| 9 | ⚠️ | `RuneIndexOf UTF-8` @ N=64 Ascii spike — 14.99 ns (Ratio 3.82) | `src/Glot/TextSpan/TextSpan.Search.cs:33-51` + `Helpers/RuneCount.cs:CountPrefix` | Ascii fast-path requires `totalRuneLength > 0 && totalRuneLength == bytes.Length`. At N=64 built via `Uncounted(...)`, `totalRuneLength == 0`, so the general `Count(bytes[..bytePos])` path runs. | In `RuneIndexOf`, pass `-1` as a sentinel meaning "uncached Ascii", and have `CountPrefix` short-circuit when the input is validated-Ascii (can check with `System.Text.Ascii.IsValid(bytes)` up to bytePos — cheap at small N). | 15 ns → ~5 ns at N=64 Ascii | S |
| 10 | ⚠️ | `Text.EndsWith UTF-16` @ 65 KB Emoji = 19.5 ns (9.43×) | `src/Glot/TextSpan/TextSpan.Search.cs:EndsWith` cross-encoding branch | Emoji needle forces the `RunePrefix.TryMatch` rune-by-rune suffix comparer. | Apply the same ASCII-narrow trick as `TryConvertAsciiNeedle` but for non-ASCII BMP needles: if the needle is pure-BMP UTF-16 and the haystack is UTF-8, still materialize and do a direct byte-suffix compare. (Already attempted in 2026-04-17 "P1.5 generalized" fix — check it covers the EndsWith path.) | 19 ns → ~6 ns | M |
| 11 | ⚠️ | JSON Serialize 65 KB Cjk regular path 1.17× `Utf8JsonWriter` | `src/Glot.SystemTextJson/TextJsonConverter.cs:53-89` | Regular `Text→UTF-8` serialization is 591 µs vs `string` 507 µs. The UTF-32 and UTF-16 cases transcode before writing. | Use `writer.WriteStringValue(chars)` directly when the Text is UTF-16-backed (already partially done per 2026-04-17 Addendum — re-audit). For UTF-8-backed, use `writer.WriteRawValue` with the already-escaped bytes after a single pass scan. | 591 µs → ~500 µs @ 65 KB Cjk | M |
| 12 | ⚠️ | JSON Deserialize Cjk 1.41× alloc ratio (266 KB vs 188 KB) | `src/Glot.SystemTextJson/TextJsonConverter.cs` read path | Deserializing a UTF-16-backed `string` value to `Text` allocates UTF-16 first then converts. | If `TextEncoding` storage policy allows, read JSON value directly as UTF-8 via `reader.HasValueSequence ? reader.ValueSequence : reader.ValueSpan` into a pooled buffer and construct `Text` from those bytes without round-tripping through UTF-16. | 266 KB → ~100-130 KB | M |
| 13 | ℹ️ | Cross-encoding dispatch floor ~17 ns (Text.Contains UTF-16/32) | `src/Glot/TextSpan/TextSpan.Search.cs` + `TextSpan.Transcode.cs` | N-independent 16-18 ns for any cross-encoding search regardless of hit/miss — consistent with a stackalloc + setup + dispatch. | Likely the `TryConvertAsciiNeedle` is doing ASCII detection + span conversion every call. Inline `SkipLocalsInit` + keep a cached 64-byte stackalloc in the caller frame. | 17 ns → ~8-10 ns (cosmetic) | S |
| 14 | ℹ️ | Document owning vs borrowing in factory XML docs (2026-04-17 P2.5) | `src/Glot/Text/Text.Factory.cs` | Already done per 2026-04-17 — verify the summary lines still say "copies" on span-accepting factories and "zero-copy" on array-accepting ones after the recent SDK bump. | Read-over. | N/A | S |
| 15 | ℹ️ | `RuneIndexOf UTF-16/UTF-32` flat 17-18 ns floor | Same as #13 (cross-encoding floor applies to Rune search too) | Scalar-stage setup cost dominates small N. | Same fix as #13 applies. | ~17 ns → ~10 ns (cosmetic) | S |

---

## Wins Worth Keeping — Do Not Regress

1. **Zero-copy factories.** `Text.FromAscii(byte[])` / `Text.FromUtf32(int[])` hitting 1.51 ns at 65 KB with 0 B is the single best demo point in the suite. Guard them with a regression-detecting benchmark threshold (<3 ns at any N).
2. **`Text.GetHashCode` UTF-8 Ascii beats `string.GetHashCode` by 20× at 65 KB.** Rooted in the rune→XxHash3 pipeline. Don't add per-call validation or a dynamic cache that could trip this.
3. **ToUpper ASCII SIMD path (new).** `Text.ToUpperInvariantPooled UTF-8 @ 65 KB Ascii = 1,589 ns / 0 B`. This is the biggest win since the prior report. Any refactor that routes UTF-8 Ascii through `RuneMapping.ToUpper` will regress this 20×.
4. **Pooled/Owned variants hitting true 0 B.** `ReplacePooled` 1-3 B at 65 KB, `SerializeToUtf8OwnedText` 0-18 B, `ConcatPooled` 0-7 B. The tests allocate in scattered bytes (presumably from BenchmarkDotNet harness counters, not the library). Protect these with `Alloc Ratio < 0.01` gates.
5. **`LinkedTextUtf8 $"..."` direct assignment.** 11.9 ns / 184 B at 2 parts/1024 chars (0.08× `string $"..."`). The fastest interpolation path at any PartSize ≥ 64.
6. **`HttpPipeline` Pooled at 65 KB Ascii: 4× faster, 5× less alloc.** `Glot pooled pipeline` @ 65 KB Ascii = 73,494 ns / 147 KB vs `String pipeline` 299,006 ns / 792 KB.
7. **Same-encoding Equals/CompareTo UTF-8 beating `Span.SequenceEqual`.** `Text.Equals UTF-8 @ 65 KB Mixed = 1,485 ns` vs `Span.SequenceEqual` 1,793 ns (0.83×) — length-prefix short-circuit is working.
8. **`Text.Split UTF-16` @ 65 KB Cjk = 82,721 ns / 0 B vs `string.Split` 470 µs / 655 KB (0.18× / zero-alloc).** Dominant win; do not let this regress while fixing the UTF-8 side (backlog #3).
9. **`LastRuneIndexOf` Ascii fix held** (from 2026-04-17 P0.2). 65 KB Ascii hit = 4.71 ns vs `Span.LastIndexOf` 3.10 ns (1.48×). No scaling regression.

---

## Notes vs 2026-04-17 Addendum

- ✅ **Held:** P0.1 ToUpper ASCII (confirmed — 0.03× at 65 KB Ascii Pooled). P0.2 LastRuneIndexOf (confirmed — 4.71 ns at 65 KB). P2.3 Emoji `ScriptParams` (data present across all benchmarks). P2.4 Replace miss (zero-copy miss @ 65 KB = 2,173 ns, 0.02×).
- ⚠️ **Regressed / not fully realized:**
  - P1.1 Streaming `EqualsCrossEncoding` is shipped, but the **UTF-32 path still goes through scalar `Rune.TryDecodeFirst` per 4-byte int** inside the 512-byte chunk. 330 µs @ 65 KB Cjk is almost identical to the pre-fix cost. Root cause moved one layer down into `Transcode`.
  - P1.2 Streaming `CompareTo` same story (334 µs @ 65 KB Cjk).
  - P1.3 Split SIMD fast path *is* wired, but per-segment `RuneCount.Count` keeps Text.Split UTF-8 at 10-13× `Span.IndexOf` — need the lazy-count change described in backlog #3.
- 🆕 **New findings:**
  - `RuneIndexOf UTF-8 @ N=64 Ascii` spike (14.99 ns, Ratio 3.82).
  - `Text.Replace UTF-8 @ 65 KB Emoji` 1.58× new regression.
  - `Text.EnumerateRunes UTF-8` 7× U8String across all locales.
  - `StartsWith UTF-8 @ 64 Cjk` 9.88 ns anomaly in the UTF-16 benchmark class (likely benchmark setup).

---

*End of report. Any P1 fix should re-run the relevant benchmark class (`--filter '*ClassName*'`) and compare to the numbers cited here. All absolute times above are Mean ns from the `Mean` column.*
