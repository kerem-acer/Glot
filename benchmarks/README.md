# Benchmarks

BenchmarkDotNet benchmarks for the Glot library. Compares Text, OwnedText, LinkedText, string, U8String, and raw `Span<byte>` operations across UTF-8, UTF-16, and UTF-32 encodings.

## Quick Start

```bash
make build            # build everything
make dry F=Contains   # single-iteration smoke test
make quick F=Contains # short run with reliable numbers
make bench F=Contains # full run
```

## Make Targets

| Target | Description |
|---|---|
| `make bench` | Full benchmark run |
| `make quick` | Short run (3 iterations, 3 warmup) |
| `make dry` | Single iteration (smoke test only) |
| `make list` | List all benchmark names |

### Options

| Option | Example | Description |
|---|---|---|
| `F=` | `F=ContainsUtf8` | Filter by glob pattern (auto-wrapped in `*...*`) |
| `P=` | `P='N=256 Locale=Ascii'` | Filter by parameters (space-separated) |
| `ARGS=` | `ARGS='--exporters HTML'` | Extra BenchmarkDotNet arguments |

### Examples

```bash
make dry F=ContainsUtf8                           # one class, dry run
make quick F=Contains                             # all Contains benchmarks
make quick F=Utf8                                 # all UTF-8 benchmarks
make dry F=ContainsUtf8 P='N=256 Locale=Ascii'    # filtered params
make bench F=TextConcat ARGS='--exporters HTML'   # with HTML export
make list F=Search                                # list search benchmarks
```

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
├── Creation/            TextCreationUtf8, Utf16, Utf32
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
