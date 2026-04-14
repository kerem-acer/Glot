---
description: Rules for writing and modifying benchmarks
globs: ["**/benchmarks/**", "**/*Benchmarks*.cs"]
---

# Benchmark Authoring Rules

## Framework

- Use **BenchmarkDotNet** — `[Benchmark]`, `[GlobalSetup]`, `[Params]`.
- Always apply `[MemoryDiagnoser]` at class level.
- BenchmarkDotNet and U8String come from central package management — don't pin versions in the benchmark csproj.

## Architecture

- **No base classes.** Every benchmark class is self-contained.
- Shared data generation lives in `TestContent` (static methods).
- Shared encoding conversion lives in `EncodedSet` (record struct with factory).
- Shared parameter values live in custom `ParamsAttribute` subclasses (`BenchmarkParams.cs`).
- One class per file. One file per method per encoding group.

## Naming

- **Class:** `{Method}{Utf8|Utf16|Utf32}Benchmarks` for search/equality, `{Feature}Benchmarks` for others.
- **File:** matches class name, placed in encoding subdirectory for search (`Search/Utf8/`, `Search/Utf16/`, `Search/Utf32/`).
- **Method:** `{Library}{Operation}` for hit, `{Library}{Operation}_Miss` for miss, `{Library}{Operation}_Diff` for different-value.
- **Description attribute:** `"{Library}.{Method}"` or `"{Library}.{Method} {encoding}"` or `"{Library}.{Method} {encoding} miss"`.

```csharp
[Benchmark(Baseline = true, Description = "U8String.IndexOf")]
public int U8IndexOf() => _haystack.U8.IndexOf(_needle.U8);

[Benchmark(Description = "Text.ByteIndexOf UTF-16")]
public int TextByteIndexOf_Utf16() => _haystack.Utf8.ByteIndexOf(_needle.Utf16);

[Benchmark(Description = "Text.ByteIndexOf UTF-32 miss")]
public int TextByteIndexOf_Utf32_Miss() => _haystack.Utf8.ByteIndexOf(_missingNeedle.Utf32);
```

## Code Style

Respect `.editorconfig` and `.globalconfig`. Key rules for benchmarks:

- **Attributes on their own line** — never place `[Params]` or custom param attributes on the same line as the field declaration.
- **Omit default accessibility** — omit `private` on fields (implicit per `resharper_csharp_default_private_modifier = implicit`).
- **LF line endings**, UTF-8 charset, final newline required.
- **File-scoped namespaces** — `namespace Glot.Benchmarks;` (not block-scoped).

## Structure

Class layout order:

1. Custom param attributes on fields (`[SearchSizeParams]`, `[ScriptParams]`) — attribute on its own line
2. Private fields (`EncodedSet` record struct preferred; `null!` acceptable for reference types like `string`, `byte[]`, `Text[]`)
3. `[GlobalSetup]` method named `Setup`
4. Benchmark methods — baseline first, then Text variants, then miss/diff variants

```csharp
[MemoryDiagnoser]
public class ContainsUtf8Benchmarks
{
    [SearchSizeParams]
    public int N;

    [ScriptParams]
    public Script Locale;

    EncodedSet _haystack, _needle, _missingNeedle;

    [GlobalSetup]
    public void Setup()
    {
        _haystack = EncodedSet.From(TestContent.Generate(N, Locale));
        _needle = EncodedSet.From(TestContent.Needle(Locale));
        _missingNeedle = EncodedSet.From(TestContent.MissingNeedle(Locale));
    }

    [Benchmark(Baseline = true, Description = "U8String.Contains")]
    public bool U8Contains() => _haystack.U8.Contains(_needle.U8);

    // ... Text UTF-8, UTF-16, UTF-32 variants, then miss variants
}
```

## Parameters

Use custom attributes from `BenchmarkParams.cs` instead of inline `[Params(...)]`:

| Attribute | Values | Use for |
|---|---|---|
| `[SearchSizeParams]` | 64, 256, 4096, 65536 | Search/contains/index benchmarks |
| `[EqualitySizeParams]` | 8, 64, 256, 4096, 65536 | Equals, CompareTo, GetHashCode |
| `[ScriptParams]` | Ascii, Latin, Cjk, Emoji, Mixed | All locale-parameterized benchmarks |
| `[PartSizeParams]` | 1, 8, 64, 1024 | Builder, concat, interpolation benchmarks |

- Add new custom attributes to `BenchmarkParams.cs` when a parameter set is reused across multiple classes.
- Use inline `[Params(...)]` only for one-off parameters unique to a single class (e.g. `Parts`, `PartSize`).
- **Never modify `[Params]` values to reduce runtime.** Use `--filter` or `--param:` CLI args instead.

## Baselines

Each encoding group has a specific competitor as baseline:

| Group | Baseline | Rationale |
|---|---|---|
| `Utf8` | `U8String` | Both operate on UTF-8 bytes natively |
| `Utf16` | `string` | Both operate on UTF-16 chars natively |
| `Utf32` | none | No established UTF-32 competitor exists |

- Mark exactly one method `Baseline = true` per class (or per category if using `[BenchmarkCategory]`).
- If the class has no competitor, omit `Baseline = true` entirely.

## Encoding Coverage

Every Text benchmark method must test **all three needle encodings** (UTF-8, UTF-16, UTF-32):

```csharp
[Benchmark(Description = "Text.Contains UTF-8")]
public bool TextContains_Utf8() => _haystack.Utf8.Contains(_needle.Utf8);

[Benchmark(Description = "Text.Contains UTF-16")]
public bool TextContains_Utf16() => _haystack.Utf8.Contains(_needle.Utf16);

[Benchmark(Description = "Text.Contains UTF-32")]
public bool TextContains_Utf32() => _haystack.Utf8.Contains(_needle.Utf32);
```

For search benchmarks, also test **miss scenarios** (needle not found) for every encoding:

```csharp
[Benchmark(Description = "Text.Contains UTF-8 miss")]
public bool TextContains_Utf8_Miss() => _haystack.Utf8.Contains(_missingNeedle.Utf8);
```

For equality benchmarks, test **different-value scenarios**:

```csharp
[Benchmark(Description = "Text.Equals UTF-8 different")]
public bool TextEquals_Utf8_Diff() => _a.Utf8.Equals(_diff.Utf8);
```

## EncodedSet

`EncodedSet` is a `record struct` that holds a string in all five representations: `Str`, `Utf8`, `Utf16`, `Utf32`, `U8`.

- Use `EncodedSet.From(string)` whenever a benchmark needs the same value in multiple encodings.
- Access fields directly: `_haystack.Utf8`, `_needle.U8`, `_a.Str`.
- Declare only the `EncodedSet` fields you need — don't create unused ones.

```csharp
// Search: haystack + needle + missing needle
EncodedSet _haystack, _needle, _missingNeedle;

// Equality: two equal values + one different
EncodedSet _a, _b, _diff;

// GetHashCode: single value
EncodedSet _a;
```

## Categories

Use `[BenchmarkCategory]` + `[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]` + `[CategoriesColumn]` when a single class benchmarks **multiple distinct operations** (e.g. Replace + ToUpper in `TextMutationBenchmarks`). Each category gets its own baseline.

Do **not** use categories when a class benchmarks one operation — just use a flat class.

## Pooled / Disposable Results

Benchmarks returning pooled or disposable results use `void` return and `using var`:

```csharp
[Benchmark(Description = "Text.ReplacePooled")]
public void TextReplacePooled()
{
    using var result = _sourceUtf8.ReplacePooled(_markerUtf8, _replacementUtf8);
}
```

## TestContent Helpers

| Method | Returns | Purpose |
|---|---|---|
| `Generate(int, Script)` | `string` | Repeating pattern of target char length |
| `GenerateCsv(int, Script)` | `string` | Comma-separated segments for split benchmarks |
| `Needle(Script)` | `string` | Short substring guaranteed to exist in `Generate` output |
| `MissingNeedle(Script)` | `string` | Short substring guaranteed NOT to exist in `Generate` output |
| `Mutate(string)` | `string` | Copy with last character changed (safe for surrogate pairs) |
| `MarkerPair(Script)` | `(string, string)` | Marker + replacement pair for replace benchmarks |

## Commands

```bash
# Build
dotnet build benchmarks/Glot.Benchmarks.csproj -c Release

# Quick run (short iterations, for validation)
dotnet run --project benchmarks/Glot.Benchmarks.csproj -c Release -- \
  --filter '*ContainsUtf8*' --quick

# Filter by parameter
dotnet run --project benchmarks/Glot.Benchmarks.csproj -c Release -- \
  --filter '*ContainsUtf8*' --param:N=256 --param:Locale=Ascii

# List all benchmarks
dotnet run --project benchmarks/Glot.Benchmarks.csproj -c Release -- --list flat

# Full report
./benchmarks/run-report.sh
```

## Don'ts

- **Don't** use abstract base classes for benchmarks — each class is self-contained.
- **Don't** use `null!` when `EncodedSet` (record struct) can replace individual encoding fields. `null!` is acceptable for reference types (`string`, `byte[]`, `Text[]`) that have no value-type alternative.
- **Don't** modify `[Params]` attribute values to speed up runs — use `--filter` and `--param:` CLI args.
- **Don't** add BenchmarkDotNet or U8String versions to the benchmark csproj — central package management handles this.
- **Don't** create a benchmark without a competitor baseline (except Utf32 group where none exists).
- **Don't** test only one needle encoding — always test UTF-8, UTF-16, and UTF-32.
- **Don't** test only the hit path — always include miss/different scenarios for search and equality benchmarks.
