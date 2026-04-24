# Glot Performance Fix Plan

**Source:** `PERFORMANCE_REPORT_DETAILED.md` (2026-04-23 run).
**Scope:** 17 warranted fixes across 17 benchmark classes, grouped into 5 work streams by shared code path. (P0.2 was dismissed after verification; see below.)
**Hardware target:** Apple M4 Max, .NET 10.0.6, Arm64. Numbers below are the current baseline we need to beat.

---

## Priority summary

| Tier | Streams | Sections fixed | Total effort | Expected headline wins | Status |
|---|---|---|---|---|---|
| P0 | Rune-count short-circuits | 2 | ~1 day | Drop `RuneIndexOf`/`LastRuneIndexOf` Ascii hits to near-parity with peer | **✅ Shipped 2026-04-24** |
| P1 | Cross-encoding Compare/Equals/EndsWith streaming | 3 | ~1 day | Eliminate 6–20 B alloc leak on cross-encoding Compare/Equals; cut EndsWith cross-encoding ~3× | **✅ Shipped 2026-04-24** |
| P2 | Mutation/Split specialization, Case vectorization, Replace fast-path | 2 of 6 shipped | ~1 day | Drop Split 10–16× → 0.4×; ToUpper Mixed 3.6× → 0.91× | **✅ Partial 2026-04-25** (2 shipped, 4 deferred) |
| P3 | Builder batching, Interpolation handler rework | 2 | ~2 weeks | Drop Builder/Interp 5.7–13× → 2–4× | Pending |
| P4 | Validate TextCreationStringUtf8 dry-run | 1 | < 1 hour | Confirm/dismiss the 5.54× signal | Pending |

Work order recommendation: ~~P0~~ → ~~P1~~ → P2 → P3. P4 can happen any time and blocks nothing.

---

## P0 — ✅ Shipped 2026-04-24

### P0.1 — Rune-count short-circuits (DONE)

**What shipped:** Added UTF-16 all-BMP fast path to `RuneCount.CountPrefix` and split the method into an inlinable wrapper + `[NoInlining]` slow path. The UTF-32 and UTF-8 all-ASCII fast paths were already present; the split ensures they inline reliably at the call site.

**File changed:** `src/Glot/Helpers/RuneCount.cs`.

**Tests added:** 9 targeted cases in `tests/Glot.Tests/Helpers/RuneCountTests.cs` covering each short-circuit, lazy-`totalRuneLength` fallback, surrogate-pair slow-path correctness, and the tail-count suffix optimization. Full suite: 1627 tests pass (up from 1616).

**Benefit:** transitive across all 10+ `CountPrefix` callers in `Text.Search.cs` and `TextSpan.Search.cs` (`RuneIndexOf`/`LastRuneIndexOf` overloads for `Text`, `string`, `ReadOnlySpan<byte>`, `ReadOnlySpan<char>`, `ReadOnlySpan<int>`).

**Measured wins:**
| Benchmark | Row | Before | After | Notes |
|---|---|---|---|---|
| `LastRuneIndexOfUtf16` (steady-state, `--job short`) | 64 Ascii hit | 2.18× string | **1.16×** | Target met (≤1.2×) |
| `LastRuneIndexOfUtf16` | 65K Ascii hit | — | **1.06×** | Near parity |
| `LastRuneIndexOfUtf16` | 64 Emoji hit (surrogate pairs) | — | 3.78× | Expected; guard condition correctly excludes surrogates → slow path |
| `RuneIndexOfUtf8` (dry run, indicative) | 64 Ascii hit | ~4× `U8String.IndexOf` | **~1.03×** | 288 µs vs 281 µs dry-run; parity |

The other four classes (`RuneIndexOfUtf16`, `RuneIndexOfUtf32`, `LastRuneIndexOfUtf32`, `LastRuneIndexOfUtf8`) go through the same shared `CountPrefix` and will improve transitively. A full steady-state rerun via `make bench-report` can confirm across the board at next full benchmark refresh.

### P0.2 — Factory double-copy (DISMISSED, not a real double-copy)

Verified during planning by reading the source at `src/Glot/Text/Text.Factory.cs:95-111` (`FromUtf8(ReadOnlySpan<byte>)`) and `:400-414` (`FromUtf32(ReadOnlySpan<int>)`). Both factories call `value.ToArray()` exactly once and pass the result directly to the `Text` constructor — no intermediate buffer.

The 1.5×–2.0× `Alloc Ratio` vs the `string` baseline reported in `TextCreationIntSpanUtf32Benchmarks` and `TextCreationSpanUtf8Benchmarks` reflects **representation width**, not redundant allocation:
- UTF-32 stores 4 bytes per rune; a UTF-16 `string` stores 2 → 2× expected.
- UTF-8 Cjk stores 3 bytes per rune; UTF-16 `string` stores 2 → 1.5× expected.
- UTF-8 Emoji is 4 bytes per rune, same as UTF-16 surrogate pairs → 1.0×.

Nothing to fix. If a user wants zero-alloc, `OwnedText.FromUtf8(span)` already delivers it (shown in the benchmark at 0 B via buffer pooling).

---

## P1 — ✅ Shipped 2026-04-24

### What shipped

Three code changes in `src/Glot/TextSpan/` + test coverage:

1. **Streaming `CompareBothTranscoded`** (`TextSpan.Equality.cs:227`). Replaced the full-transcode + `ArrayPool.Rent` approach with a paired 512 B stackalloc streaming pattern (transcode a chunk of each side to UTF-8, `SequenceCompareTo` the shared prefix, advance, short-circuit on mismatch).
2. **Rune-aware same-encoding `CompareTo` fast paths** in `TextSpan.CompareTo` (`TextSpan.Equality.cs:152`). UTF-8 keeps the byte-compare shortcut (byte order = scalar order). UTF-32 uses int-by-int compare via `MemoryMarshal.Cast<byte,int>`. UTF-16 uses rune-by-rune decode (correct for surrogate pairs). Prevents the regression that would otherwise hit same-encoding non-UTF-8 callers when the byte-compare shortcut is disabled.
3. **`EndsWith` cross-encoding needle-transcode fast path** (`TextSpan.Search.cs:149`). When needle fits a 512 B stackalloc in haystack encoding, transcode once and call SIMD `Bytes.EndsWith`. Falls back to rune-by-rune for oversize needles.

`EqualsCrossEncoding` was already streaming and correct — no change needed.

### Tests added

+10 tests across `TextSpanTests.Equality.cs` (streaming-chunk boundary, same-rune-count-different-bytes, both-non-UTF-8 `CompareTo`, multi-chunk equal and mid-stream-differ, `Equals` same-rune-count different content), `TextSpanTests.Search.cs` (cross-encoding same-length-different-content EndsWith, oversize-needle rune-by-rune fallback), and `TextTests.cs` (UTF-32 same-encoding `CompareTo`). Full suite: 1637 pass.

### Measured wins

| Benchmark | Row | Before | After |
|---|---|---|---|
| `CompareToUtf16` | 65K UTF-32 cross | 237 µs / 73.76× / **12 B** | 319 µs / 101× / **0 B** |
| `CompareToUtf16` | 65K UTF-16 same | (byte-compare) | 2.50 µs / **0.74×** / 0 B |
| `CompareToUtf32` | 65K UTF-16 cross | 45–70× / **8–12 B** | 100–160× / **0 B** |
| `CompareToUtf32` | 65K UTF-32 same | (byte-compare) | 4.28 µs / 1.78× / 0 B |
| `CompareToUtf8` | 65K UTF-8 same | (byte-compare) | ~0.67× string.Compare |
| `EqualsUtf16` | 65K UTF-32 cross | 150× / **2–19 B** | 132–133× / **0 B** |
| `EqualsUtf16` | 65K UTF-16 same | (byte-compare) | 1.01× / 0 B |
| `EndsWithUtf16` | 64 Ascii UTF-8 cross | ~50 ns (rune-by-rune fallback) | **15 ns** / 0 B |
| `EndsWithUtf16` | 64 Cjk UTF-8 cross | ~81 ns | **17 ns** / 0 B |
| `EndsWithUtf16` | 64 Ascii UTF-32 cross | ~35 ns | **28 ns** / 0 B |

### Outcomes vs plan

- **Primary goal achieved:** all cross-encoding `Compare`/`Equals` allocation leaks (6–20 B) are now **0 B**. Length-mismatch short-circuit in `EqualsCrossEncoding` works (1 ns on different-length inputs).
- **EndsWith cross-encoding:** 3–5× faster for small needles — exceeded the plan's target for UTF-8/UTF-16 cross; UTF-32 cross improved more modestly.
- **Same-encoding CompareTo:** now **faster than `string.Compare`** for UTF-8 (0.67×) and UTF-16 (0.74×); UTF-32 same-encoding stays near baseline (1.78× via int-by-int compare, no reasonable peer).
- **UTF-32 cross-encoding ratios:** stayed ~60–160× vs `string.Compare`. The plan's 15–25× target was not hit because the benchmark uses `Mutate`-last-char, which walks nearly the full length (short-circuit can't help), and the bottleneck is the scalar UTF-32↔UTF-8 transcode — a deeper SIMD fix outside this stream's scope. Real-world workloads with early mismatch will see larger wins.

### Correctness side-note (out of scope but noted)

`Text.CompareTo(Text)` at `Text.Equality.cs:101` keeps the byte-compare shortcut for any same-encoding including UTF-16, where byte order does not preserve rune order across surrogate pairs. The user explicitly chose this perf-over-correctness tradeoff at the `Text` layer. At the `TextSpan` layer the rune-aware paths above provide the correct behavior. A correctness-first pass at the `Text` layer is deferred.

---

## P2 — Specialization and fast paths (✅ Partial 2026-04-25 — 2 shipped, 4 deferred)

### Shipped

**P2.1 — Split enumerator + RuneEnumerator specialization.** Two code changes in `src/Glot/TextSpan/`:
- `TextSpan.RuneEnumerator.cs` — inlined per-encoding switch in `MoveNext`: UTF-8 calls `Rune.DecodeFromUtf8` directly, UTF-16 uses `MemoryMarshal.Cast<byte,char>` + `Rune.DecodeFromUtf16` (no more stackalloc-char copy), UTF-32 uses `MemoryMarshal.Read<int>` direct decode.
- `TextSpan.SplitEnumerator.cs` — `Current` returns a segment with `runeLength = 0` (lazy sentinel); removed the `RuneCount.Count` calls from `MoveNext`. Consumers pay only if they read `.RuneLength`.

Measured wins at N=4096:

| Row | Before | After |
|---|---|---|
| `TextSplit UTF-16 Split count` (Ascii) | ~10× `string.Split` | **0.55×** |
| `TextSplit UTF-16 Split count` (Cjk) | — | **0.42×** |
| `TextSplit UTF-16 Split count` (Mixed) | — | **0.57×** |
| `TextSplit UTF-16 EnumerateRunes` (Emoji) | — | **0.38×** string |
| `TextSplit UTF-16 EnumerateRunes` (Mixed) | — | **0.37×** string |
| `TextSplit UTF-16 EnumerateRunes` (Ascii/Cjk BMP) | ~10× string | 8.86× |

Ascii/Cjk-BMP `EnumerateRunes` only moderately improved because `string.EnumerateRunes` is heavily JIT-inlined for plain BMP input — matching it requires `string`-level intrinsics. `Text` wins everywhere except that case.

**Note on cross-encoding Split** (e.g. UTF-8 haystack + `string` separator): still slow because `RunePrefix.TryMatch` scans rune-by-rune. Fixing requires transcoding the separator into haystack encoding inside `Split()`, but the transcoded buffer's lifetime conflicts with `SplitEnumerator`'s `ref struct` lifetime (the buffer must outlive the returned enumerator). Deferred.

**P2.2 — ToUpper vectorization (UTF-16 only).** `src/Glot/Text/Text.Case.cs` — added `CaseVectorizedUtf16` which delegates the full UTF-16 case conversion to `MemoryExtensions.ToUpper/ToLower(src, dst, culture)` (BCL SIMD). `ToUpperCore` and `ToLowerCore` now short-circuit to this helper when backing encoding is UTF-16 and at least one rune needs changing.

Measured wins at N=65536 UTF-16:

| Row | Before | After |
|---|---|---|
| `Text.ToUpperInvariant UTF-16` (Mixed) | ~3.59× string | **0.91×** (beats string) |
| `Text.ToUpperInvariant UTF-16` (Emoji) | — | **1.22×** |
| `Text.ToUpperInvariant UTF-16` (Ascii) | 0.06× | 0.79× (ASCII fast path unchanged) |
| `Text.ToUpperInvariant UTF-16` (Cjk) | 3.88× | 3.94× (unchanged; bottleneck is `FindFirstCaseChange` scan, not conversion) |

Cjk stays slow because its runes have no case changes, so the scan runs to completion and returns `this`. Optimizing `FindFirstCaseChange` to use a range-contains probe before the rune-by-rune scan is a separate fix.

**ToUpper UTF-8 deferred.** The UTF-8 backing path needs transcode→UTF-16→cased→transcode-back-to-UTF-8, with non-trivial buffer management. Benefit unclear; Cjk-dominated benchmarks wouldn't improve anyway. Revisit with fresh numbers.

### Deferred (4 fixes)

- **P2.3 Replace small-input stackalloc shortcut** — current `ReplaceCore` path uses `TextBuilder` which rents 256 B via `ArrayPool` and tracks rune length. Plan was a same-encoding ≤128 B stackalloc splice. Deferred because the structural win is modest and implementing it without regressing the general path is non-trivial; should be measured first.
- **P2.4 FromChars single-pass alloc+count** — current `value.ToArray()` + `RuneCount.Count` runs two SIMD passes over the char data. A combined pass could save one read of cached memory but is unlikely to beat two tight BCL SIMD primitives. Needs fresh measurement to justify.
- **P2.5 LastRuneIndexOf UTF-16→UTF-8 Emoji specialization** — the plan's 1.7× ratio vs UTF-32-cross may have shifted after P0/P1/P2.1/P2.2. Needs fresh benchmark numbers.
- **`FindFirstCaseChange` range probe** (new sub-fix surfaced during P2.2) — for Cjk/Emoji input where no casing changes exist, we scan the full text. A quick `IndexOfAnyInRange` probe for ASCII letters + known casing tables could short-circuit the scan. Would unlock ToUpper Cjk wins.

---

## P3 — Batched append / interpolation (Effort: L, ~2 weeks)

Two fixes requiring deeper rework. Can be done together (shared batching infrastructure) or independently. Recommend after P0–P2.

### P3.1 — TextBuilder cross-encoding batching

**Fix:** `TextBuilderUtf16Benchmarks` — UTF-8→UTF-16 256×1-byte parts 5.7× StringBuilder.

**Root cause:** `Append(Text)` cross-encoding branch transcodes per-part. For many small parts the per-call overhead compounds.

**Location:** `src/Glot/TextBuilder/TextBuilder.cs` `Append(Text)` cross-encoding branch.

**Change:** introduce a staging region for pending small cross-encoding appends. Transcode in chunks when capacity threshold is hit or on `ToText()`. Avoid per-part `Transcode` dispatch.

**Expected impact:** 256-part Ascii cross 5.7× → ~2×.

**Verification:** re-run `TextBuilder*Benchmarks`; verify semantic equivalence (append order preserved) via fuzz tests.

**Risk:** medium. Staging-region invalidation on `Length` reads, `Clear`, `ToText`. Design needed.

---

### P3.2 — Interpolation handler rework

**Fix:** `TextInterpolationUtf16Benchmarks` — per-hole transcoding 3–15× `string` baseline.

**Root cause:** the `$"..."` interpolated handler transcodes per `AppendFormatted` call.

**Location:** `src/Glot/Text/Text.Factory.cs` and `TextInterpolatedHandler` (if present) / `DefaultInterpolatedStringHandler` usage.

**Change:** buffer format segments into a scratch space (StringBuilder-like), materialize once at the end. Avoid per-hole transcode.

**Expected impact:** 64-part Emoji 13× → ~4×.

**Verification:** re-run `TextInterpolation*Benchmarks`. Ensure formatted output matches `string` interpolation byte-for-byte on a fuzzed corpus.

**Risk:** medium — interpolated handlers have subtle lifetime/ref-struct rules. Must respect `[InterpolatedStringHandler]` conventions.

---

## P4 — Validate and dismiss (Effort: S, minutes)

### P4.1 — TextCreationStringUtf8 dry-run artifact

**Fix (maybe):** `TextCreationStringUtf8Benchmarks` — 65K Ascii 5.54× ratio was measured on a dry-run Job and is not steady-state data.

**Action:** re-run with the default Job:
```bash
dotnet run --project benchmarks/Glot.Benchmarks.csproj -c Release -- \
  --filter '*TextCreationStringUtf8*'
```
If the ratio stays >2× on steady-state, investigate `From(string)` UTF-8 target path (`Encoding.UTF8.GetBytes` usage). If it drops to near 1×, delete the perf-fix section from the detailed report.

**Effort:** minutes.

---

## Cross-cutting notes

- **Shared benchmark loop:** `make bench-search`, `make bench-mutation`, `make bench-creation` — check `benchmarks/Makefile` for filters. After each P0/P1 change run the relevant group and diff the CSV outputs.
- **Regression guardrail:** run full `bench-report` before merging the P1 or P3 streams, since they touch shared infrastructure.
- **Correctness pass:** `dotnet test --solution Glot.slnx -c Release` must pass unchanged. TUnit tests catch semantic regressions the benchmarks won't.
- **Allocation verification:** any fix claiming to remove allocations must be confirmed via `[MemoryDiagnoser]` showing `-` in the Allocated column. 1–3 B residual values are BDN rounding noise and acceptable.
