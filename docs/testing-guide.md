# Testing Guide

This guide covers how to discover testable code and write tests in the Glot codebase.

## Testing Philosophy

| Test Type | What to Test | Tools |
|-----------|--------------|-------|
| **Unit Tests** | Pure logic without I/O dependencies | TUnit, Verify |

### Available Test Packages

| Package | Source | Purpose |
|---------|--------|---------|
| **TUnit** | `Directory.Build.props` (automatic) | Test framework — `[Test]`, `[Arguments]`, `Assert.That()` |
| **Verify.TUnit** | Per-project csproj (when needed) | Snapshot testing — `Verify()`, `VerifyJson()` |
| **Verify.\*** plugins | Per-project csproj (when needed) | Domain-specific verification ([full list](https://github.com/VerifyTests/Verify?tab=readme-ov-file#plugins)) |

---

## Ref Struct Testing

`TextSpan` is a `ref struct` and cannot be preserved across `await` boundaries (CS4007). Always extract results into regular variables before asserting:

```csharp
// BAD — span lives across await
var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
await Assert.That(span.Length).IsEqualTo(5);       // CS4007
await Assert.That(span.IsEmpty).IsFalse();          // CS4007

// GOOD — extract values first
var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
var length = span.Length;
var isEmpty = span.IsEmpty;
await Assert.That(length).IsEqualTo(5);
await Assert.That(isEmpty).IsFalse();
```

For single-use values, chain directly without storing the span:

```csharp
var result = new TextSpan("Hello"u8, TextEncoding.Utf8).Equals("Hello".AsSpan());
await Assert.That(result).IsTrue();
```

---

## Writing Tests

### Test Naming Convention

```
{MethodName}_{Scenario}_{ExpectedResult}
```

Examples:
- `Length_Returns_Rune_Count`
- `DecodeFirstRune_Ascii_Utf8`
- `Equals_Cross_Encoding_Utf8_Utf16`
- `IndexOf_NotFound`

### Test Structure (AAA Pattern)

```csharp
[Test]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange
    var input = CreateTestInput();

    // Act
    var result = SystemUnderTest.Method(input);

    // Assert
    await Assert.That(result).IsEqualTo(expected);
}
```

### Write Explicit Tests

Tests must be self-documenting. A reader should understand what's being tested without looking at helper methods.

**Bad:** Values come from hidden defaults
```csharp
[Test]
public async Task Parse_ValidInput_Succeeds()
{
    var result = TestHelpers.CreateResult();
    await Assert.That(result.Name).IsEqualTo("default-name");
}
```

**Good:** Values are explicit in the test
```csharp
[Test]
public async Task Length_Emoji_Utf8()
{
    // Arrange
    var bytes = TestHelpers.Encode("🎉", TextEncoding.Utf8);

    // Act
    var span = new TextSpan(bytes, TextEncoding.Utf8);
    var length = span.Length;

    // Assert
    await Assert.That(length).IsEqualTo(1);
}
```

**When defaults are OK:** Use helper defaults only when the value is irrelevant to the test.

### When to Use Verify vs Assert

| Use Case | Tool | Example |
|----------|------|---------|
| Single scalar value (string, int, bool) | `Assert.That()` | `await Assert.That(result).IsEqualTo("Hello")` |
| Object with 2+ properties | `Verify()` | `await Verify(result)` |
| Selective property check | `Verify(new { ... })` | `await Verify(new { A = x.A, B = x.B })` |
| Source generator output | `Verify()` | Snapshot the generated `.g.cs` content |

### Parameterized Tests

```csharp
[Test]
[Arguments("Hello", TextEncoding.Utf8, 5)]
[Arguments("Hello", TextEncoding.Utf16, 5)]
[Arguments("Hello", TextEncoding.Utf32, 5)]
public async Task Length_Returns_Rune_Count(string input, TextEncoding encoding, int expected)
{
    var bytes = TestHelpers.Encode(input, encoding);
    var span = new TextSpan(bytes, encoding);
    var length = span.Length;
    await Assert.That(length).IsEqualTo(expected);
}
```

---

## Code Organization

### Test Class Naming

Test class names **must match** the class being tested:

| Class Under Test | Test Class Name |
|------------------|-----------------|
| `TextSpan` | `TextSpanTests` or `TextSpanCoreTests`, `TextSpanSearchTests` |

### Use Partial Classes, Not Regions

**Never use `#region`** to organize tests. Use **partial classes** to split tests across multiple files:

```
TextSpanCoreTests.cs                    # construction, properties, decode, equality
TextSpanCoreTests.Comparison.cs         # partial class — comparison tests
TextSpanSliceTrimSplitTests.cs          # slice, trim, split
TextSpanSearchTests.cs                  # search operations
```

### Recommended Project Structure

```
Glot.Tests/
├── {ClassName}Tests.cs
├── {ClassName}Tests.{MethodGroup}.cs   # partial
├── Helpers/
│   └── TestHelpers.cs                  # shared encoding helpers
├── ModuleInitializer.cs                # UseProjectRelativeDirectory("snapshots") (when using Verify)
├── snapshots/                          # (when using Verify)
└── Glot.Tests.csproj
```

### Project File Template

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="TUnit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="../../src/Glot/Glot.csproj" />
  </ItemGroup>
</Project>
```

Notes:
- `OutputType: Exe` is required by Microsoft Testing Platform.
- TUnit comes from `Directory.Build.props` — don't add a version here.
- Don't duplicate transitive package references from the production project.

---

## Snapshot Testing (Verify)

### Verify Packages

Verify is **not** in `Directory.Build.props` — add packages per test project as needed.

The test-framework adapter is always required:

```xml
<PackageReference Include="Verify.TUnit" />
```

### When to Use Verify vs Assert

| Situation | Use | Why |
|-----------|-----|-----|
| Single scalar value | `Assert.That()` | Simpler, inline |
| Object with 2+ properties | `Verify()` | Catches all properties |
| Source generator output | `Verify()` | Locks down generated code |
| Selective property check | `Verify(new { ... })` | Anonymous object limits scope |

### Snapshot File Location

```csharp
using System.Runtime.CompilerServices;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        UseProjectRelativeDirectory("snapshots");
    }
}
```

### `.UseParameters()` for Parameterized Snapshots

When using `[Arguments]` with `Verify()`, always call `.UseParameters()` so each parameter combination gets its own snapshot file:

```csharp
[Test]
[Arguments("camelCase")]
[Arguments("PascalCase")]
public async Task Convert_InputCase_ProducesExpectedOutput(string input)
{
    var result = Convert(input);
    await Verify(result).UseParameters(input);
}
```

### Managing Snapshot Files

| File | Git Status | Purpose |
|------|------------|---------|
| `*.verified.txt` / `*.verified.cs` | **Committed** | Approved snapshot (source of truth) |
| `*.received.txt` / `*.received.cs` | **Gitignored** | New/changed output for review |

---

## Test Data Management

### TestHelpers Pattern

Use static helper classes for shared test object creation:

```csharp
static class TestHelpers
{
    public static byte[] Encode(string s, TextEncoding encoding) => encoding switch
    {
        TextEncoding.Utf8 => System.Text.Encoding.UTF8.GetBytes(s),
        TextEncoding.Utf16 => System.Text.Encoding.Unicode.GetBytes(s),
        TextEncoding.Utf32 => System.Text.Encoding.UTF32.GetBytes(s),
        _ => throw new ArgumentOutOfRangeException(nameof(encoding)),
    };
}
```

**Rule:** Asserted values must be explicit in the test body. Use helper defaults only for irrelevant setup data.

---

## Running Tests

```bash
# Run all tests
dotnet test --solution Glot.slnx -c Release

# Run with filter
dotnet test --solution Glot.slnx --filter "FullyQualifiedName~TextSpanCoreTests"

# Run with coverage
dotnet test --solution Glot.slnx -c Release -- \
  --coverage --coverage-settings coverage.settings.xml \
  --coverage-output-format cobertura \
  --coverage-output coverage.cobertura.xml

# Generate coverage report
reportgenerator \
  -reports:"tests/*/bin/Release/net10.0/TestResults/coverage.cobertura.xml" \
  -targetdir:TestResults/CoverageReport \
  -reporttypes:"TextSummary"
```

---

## Test Review Checklist

| Check | Rule |
|-------|------|
| Naming | `{MethodName}_{Scenario}_{ExpectedResult}` |
| Structure | AAA pattern with `// Arrange`, `// Act`, `// Assert` comments |
| Asserted values | Explicit in the test body, not hidden in helper defaults |
| Organization | No `#region` — use partial classes |
| Ref structs | All values extracted before first `await` |
| Parameterized | `.UseParameters(param)` with `[Arguments]` when using Verify |
| After writing | Run `dotnet test` on affected project |

---

## Anti-Patterns

| Anti-Pattern | Problem | Correct Approach |
|-------------|---------|-----------------|
| `#region` in test files | Hides test structure | Use partial classes: `{Class}Tests.{Group}.cs` |
| Hidden default values | Test intent unclear | Make asserted values explicit |
| Testing record equality | Tests compiler-generated code | Don't test — records handle this |
| Oversized test classes (500+ lines) | Hard to navigate | Split into partial class files |
| `null!` field initializers | Hides initialization bugs | Use constructors or `[Before(Test)]` |
| Ref struct across `await` | CS4007 compilation error | Extract values before first `await` |
| Multiple `Assert.That()` on same object | Misses properties, verbose | Use `Verify(result)` |
| Duplicating transitive packages in test csproj | Unnecessary, version conflicts | Production project provides them transitively |
| Missing `UseParameters()` | Parameterized snapshots overwrite each other | Always call `.UseParameters(param)` |
