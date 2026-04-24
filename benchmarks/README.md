# Benchmarks

BenchmarkDotNet benchmarks for the Glot library. Compares Text, OwnedText, LinkedText, string, U8String, and raw `Span<byte>` operations across UTF-8, UTF-16, and UTF-32 encodings.

## Quick Start

```bash
make build                                           # build everything
make bench F=Contains PRESET=dry                     # smoke test
make bench F=Contains I=1 NO=1 LOOSE=1 MAX_ITER=30   # fast dev loop
make bench F=Contains                                # full adaptive run
make bench F=Contains ITERATION_TIME=500             # publishable precision
```

## Make Targets

| Target | Description |
|---|---|
| `make bench` | Run benchmarks (modes via variables below) |
| `make list` | List all benchmark names |

## Variables

Selection:

| Variable | Example | Description |
|---|---|---|
| `F=` | `F=ContainsUtf8` | Filter by glob pattern (auto-wrapped in `*...*`) |
| `P=` | `P='N=256 Locale=Ascii'` | Filter by parameter values (space-separated) |

Mode flags (set to any value to enable):

| Variable | BDN flag | Description |
|---|---|---|
| `I=1` | `-i` | Run in-process (no child process per benchmark) |
| `NO=1` | `--no-overhead` | Skip overhead measurement phase |
| `LOOSE=1` | `--loose` | Raise `MaxRelativeError` 2% → 5% (custom) |

Numeric knobs:

| Variable | BDN flag | Description |
|---|---|---|
| `ITERATION_TIME=N` | `--iterationTime N` | Target ms per iteration (default 150; empty = BDN's 500ms) |
| `MAX_ITER=N` | `--maxIterationCount N` | Cap adaptive iteration count |
| `MIN_ITER=N` | `--minIterationCount N` | Floor adaptive iteration count |
| `WARMUP=N` | `--warmupCount N` | Pin warmup count (disables adaptive warmup) |

Job preset:

| Variable | BDN flag | Description |
|---|---|---|
| `PRESET=dry` | `-j dry` | 1 invocation per benchmark (smoke test) |
| `PRESET=short` | `-j short` | Pinned 3 iter / 3 warmup |
| `PRESET=medium` | `-j medium` | Pinned 15 iter / 10 warmup / 2 launches |
| `PRESET=long` | `-j long` | Pinned 100 iter / 30 warmup / 3 launches |

Escape hatch:

| Variable | Example | Description |
|---|---|---|
| `ARGS=` | `ARGS='--memory --threading'` | Any extra BDN flags not covered above |

### Examples

```bash
# Smoke test — does everything still run?
make bench F=Contains PRESET=dry

# Fast dev loop — ~2 min for 72 cases
make bench F=ToUpper I=1 NO=1 LOOSE=1 MAX_ITER=30

# Short pinned run — predictable, ~3 samples per case
make bench F=Contains PRESET=short

# Publishable numbers — adaptive with default 2% error target
make bench F=Contains ITERATION_TIME=500

# All UTF-8 benchmarks, filtered parameters
make bench F=Utf8 P='N=256 Locale=Ascii'

# Advanced: add threading stats diagnoser
make bench F=Contains I=1 ARGS='--threading'

# List available benchmarks
make list F=Search
```

### Custom flags (Program.cs)

Two flags are custom to this repo and have no BDN CLI equivalent:

- **`--loose`** (via `LOOSE=1`) — sets `MaxRelativeError=0.05` on all jobs as a mutator. Halves run time for adaptive runs at the cost of wider CIs (~5% vs BDN's default 2%).
- **`--no-overhead`** (via `NO=1`) — sets `EvaluateOverhead=false`. Skips the overhead measurement phase entirely (~halves run time). Reported nanosecond numbers are inflated by loop wrapper cost (~1-2 ns), but relative ratios within a class stay correct.

Everything else is BDN-native CLI passthrough.

## Reports

Markdown reports (GitHub-flavored) are generated automatically in `BenchmarkDotNet.Artifacts/results/` after each run. These are `.md` files with benchmark tables ready for pasting into PRs or docs.

## Structure

Benchmarks are organized by **method**, then by **encoding**:

```
benchmarks/
├── Search/
│   ├── Contains/        ContainsUtf8, Utf16, Utf32
│   ├── ByteIndexOf/     ByteIndexOfUtf8, Utf16, Utf32
│   ├── LastByteIndexOf/
│   ├── StartsWith/
│   ├── EndsWith/
│   ├── Equals/
│   ├── CompareTo/
│   ├── GetHashCode/
│   ├── RuneIndexOf/
│   └── LastRuneIndexOf/
├── Mutation/
│   ├── Replace/         ReplaceUtf8, Utf16, Utf32
│   └── ToUpper/         ToUpperUtf8, Utf16, Utf32
├── Concat/              TextConcatUtf8, Utf16, Utf32
├── Interpolation/       TextInterpolationUtf8, Utf16, Utf32
├── Builder/             TextBuilderUtf8, Utf16, Utf32
├── Creation/
│   ├── ByteArray/       TextCreationByteArrayUtf8, Utf16, Utf32
│   ├── Span/            TextCreationSpanUtf8, Utf16, Utf32
│   ├── ImmutableArray/  TextCreationImmutableArrayUtf8
│   ├── String/          TextCreationStringUtf8, Utf16
│   ├── CharArray/       TextCreationCharArrayUtf16
│   ├── CharSpan/        TextCreationCharSpanUtf16
│   ├── IntArray/        TextCreationIntArrayUtf32
│   └── IntSpan/         TextCreationIntSpanUtf32
├── Split/               TextSplitUtf8, Utf16, Utf32
├── Pipeline/            HttpPipeline, JsonSerialization
└── Shared/              EncodedSet, TestData, BenchmarkParams, Script
```

## Baselines

| Encoding | Baseline | Rationale |
|---|---|---|
| UTF-8 | `Span<byte>` or `U8String` | Native UTF-8 byte operations |
| UTF-16 | `string` | Native .NET string type |
| UTF-32 | none | No established competitor |

## Competitors per class

Each benchmark includes:
- **Baseline** — the native-encoding equivalent (see above)
- **U8String** — third-party UTF-8 string (UTF-8 classes only)
- **Text** — same-encoding and cross-encoding variants
- **Text pooled** — zero-allocation pooled variants where applicable
- **LinkedText** — segmented text (concat/interpolation only)
