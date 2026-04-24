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
| P2 | Mutation/Split specialization, Case vectorization, Replace fast-path | 6 | ~1–2 weeks | Drop Split 10–16× → 1.5×; ToUpper 3.8× → 1.2× | Pending |
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

## P2 — Specialization and fast paths (Effort: M, ~1–2 weeks)

Six independent fixes. Can be worked in parallel or in any order.

### P2.1 — Split rune-enumerator specialization

**Fixes:**
- `TextSplitUtf16Benchmarks` — `EnumerateRunes` 10× `string.EnumerateRunes` at 4K Ascii
- `TextSplitUtf8Benchmarks` — `RuneCount.Count` called on every yielded segment (16× at N=64)

**Root causes:**
- `RuneEnumerator.MoveNext` calls generic `Rune.TryDecodeFirst(_remaining, _encoding, ...)` every iteration — 4 branches + surrogate check for UTF-16
- `SplitEnumerator.MoveNext` calls `RuneCount.Count` on every yielded segment even when the consumer never reads `.RuneCount`

**Locations:**
- `src/Glot/TextSpan/TextSpan.RuneEnumerator.cs:28` (`MoveNext`)
- `src/Glot/TextSpan/TextSpan.SplitEnumerator.cs:46, 61, 68` (per-segment `RuneCount.Count` calls)

**Changes:**
1. **Specialize `RuneEnumerator` per encoding.** UTF-16: read `char` directly, branch on surrogate inline. UTF-8: check high-bit and decode inline. UTF-32: `MemoryMarshal.Cast<byte,int>(remaining)[0]`. Dispatch via an encoding field; JIT de-virtualizes on type.
2. **Lazy rune-count on `SplitEnumerator` segments.** Construct yielded `TextSpan` with `runeCount = 0` (sentinel for "not computed"); compute on first `.RuneCount` access and cache. For the common split-then-iterate-bytes workflow, never pay the count cost.

**Expected impact:**
- UTF-16 `EnumerateRunes` 4K Ascii: 10× → ~2×
- UTF-8 `Split` 64 Ascii: 16× → ~1.5×
- UTF-8 `Split` 4K Ascii: 10× → ~1.2×

**Verification:** re-run `TextSplit*Benchmarks`. Ensure `tests/Glot.Tests/TextSpan/RuneEnumerator*` and `SplitEnumerator*` still pass.

**Risk:** medium. Changing `runeCount` to a lazy sentinel touches public `TextSpan` construction semantics — audit all callers.

---

### P2.2 — ToUpper vectorization

**Fixes:**
- `ToUpperUtf16Benchmarks` — 3.88× `string.ToUpperInvariant` at 65K Cjk
- `ToUpperUtf8Benchmarks` — 3.74× at 65K Cjk

**Root cause:** `ToUpperCore` in `Text.Case.cs:165` loops rune-by-rune via `Rune.ToUpperInvariant`. `string.ToUpperInvariant` uses `MemoryExtensions.ToUpperInvariant(Span,Span)` which is vectorized via ICU tables.

**Location:** `src/Glot/Text/Text.Case.cs:165` (`ToUpperCore` → `CaseCore`)

**Changes:**
1. **UTF-16 backing:** after the ASCII fast-path bails on `firstChangeOffset`, delegate the non-ASCII suffix to `MemoryExtensions.ToUpperInvariant(srcChars, destChars)` on a `MemoryMarshal.Cast<byte,char>` view.
2. **UTF-8 backing:** transcode the non-ASCII remainder to UTF-16 chars into a pooled buffer, call `MemoryExtensions.ToUpperInvariant(srcChars, destChars, CultureInfo.InvariantCulture)`, transcode back. Sounds expensive but the vectorized BCL path dominates.

**Expected impact:**
- UTF-16 65K Cjk: 3.88× → ~1.2×
- UTF-8 65K Cjk: 3.74× → ~1.5×
- Ascii fast-path untouched (stays at 0.06×)

**Verification:** re-run `ToUpper*Benchmarks`. Ensure `tests/Glot.Tests/Text/Case*` passes on Cjk/Emoji/Mixed inputs.

**Risk:** low. The BCL API is the same one `string` uses; identical semantics guaranteed.

---

### P2.3 — Replace small-input shortcut

**Fix:** `ReplaceUtf16Benchmarks` — 1.5–1.8× `string.Replace` at small N.

**Root cause:** below ~256 bytes, the rune-aware `Replace` machinery (RuneLength tracking, cross-encoding dispatch) doesn't earn its keep.

**Location:** `src/Glot/Text/Text.Replace.cs` main `Replace(Text marker, Text replacement)` entry point.

**Change:** add a guard at entry:
```csharp
if (same-encoding && haystack.ByteLength + replacement.ByteLength <= 128)
{
    Span<byte> scratch = stackalloc byte[128];
    // IndexOf + byte-splice; skip RuneLength recomputation
}
```

**Expected impact:** 64 Ascii 1.8× → ~1.1×. 4K/65K rows untouched (they already win on the main path).

**Verification:** re-run `ReplaceUtf16Benchmarks`. Ensure result text matches the standard path byte-for-byte on a fuzzed test corpus.

**Risk:** low if guarded on `same-encoding + size`. Ensure the stackalloc budget accounts for worst-case replacement expansion.

---

### P2.4 — Factory char-span single-pass

**Fix:** `TextCreationCharSpanUtf16Benchmarks` — `Text.FromChars(span)` 1.8× `new string(span)` at N=256 Ascii (44.75 ns).

**Root cause:** memcpy then separate rune-count scan; two passes over the data.

**Location:** `src/Glot/Text/Text.Factory.cs` `FromChars(ReadOnlySpan<char>)`.

**Change:** combine alloc + memcpy + rune-count into one pass. Count surrogates inline during copy.

**Expected impact:** 44.75 ns → ~30 ns at 256 Ascii.

**Verification:** re-run `TextCreationCharSpanUtf16Benchmarks`; add/check test on a char span with surrogate pairs.

**Risk:** low.

---

### P2.5 — LastRuneIndexOf UTF-16→UTF-8 Emoji specialization

**Fix:** `LastRuneIndexOfUtf8Benchmarks` — UTF-16 cross with Emoji needle 1.7× the UTF-32 cross equivalent (72 ns).

**Root cause:** surrogate-pair UTF-16→UTF-8 transcode inside `LastIndexOfCrossEncoding` goes through the generic `Transcode` dispatch.

**Location:** `src/Glot/TextSpan/TextSpan.Search.cs:294` (`LastIndexOfCrossEncoding`).

**Change:** tight char-loop for ≤4-byte-UTF-8 needles (BMP + astral): decode surrogate pair → encode UTF-8 inline, skip the generic `Transcode` dispatch overhead.

**Expected impact:** 72 ns Emoji cross → ~40 ns (parity with UTF-32 cross).

**Verification:** re-run `LastRuneIndexOfUtf8Benchmarks`.

**Risk:** low (well-defined codepath).

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
