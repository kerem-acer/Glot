---
description: Rules for writing and modifying unit tests
globs: ["**/*Tests*/**", "**/*Test*.cs", "**/tests/**"]
---

# Test Authoring Rules

## Framework

- Use **TUnit** — `[Test]`, `[Arguments]`, `await Assert.That()`.
- Do NOT use xUnit (`[Fact]`, `[Theory]`) or NUnit (`[TestCase]`) attributes.
- **Setup:** use constructors with `readonly` fields for sync setup. Use `[Before(Test)]` only when async or special lifecycle behavior is needed. Do NOT use `null!` field initializers.
- TUnit comes from `Directory.Build.props` — don't add it to individual test csproj files.

## Naming

- Test method: `{MethodName}_{Scenario}_{ExpectedResult}`
- Test class: `{ClassName}Tests` matching the class under test
- Partial class files: `{ClassName}Tests.{MethodGroup}.cs`

## Structure

- Always use AAA with comments: `// Arrange`, `// Act`, `// Assert`
- Never use `#region` — split into partial class files instead.
- Asserted values must be explicit in the test body, not hidden in helper defaults.
- **No duplicated literals between Arrange and Assert:** extract shared values as local `const` variables and use them in both places.
- Tests at top of file, utility/helper methods at bottom.

## Ref Struct Considerations

- `TextSpan` is a `ref struct` — it cannot live across `await` boundaries.
- **Extract all values before the first `await`:** call methods on `TextSpan` and store results in regular variables (int, bool, etc.) before asserting.
- Do NOT pass `TextSpan` to async lambdas or capture it in closures.

## Assertions

TUnit built-in assertions:

```csharp
// Value equality
await Assert.That(x).IsEqualTo(y);

// Boolean
await Assert.That(x).IsTrue();
await Assert.That(x).IsFalse();

// Comparison
await Assert.That(x).IsGreaterThan(y);
await Assert.That(x).IsLessThan(y);

// Type checking
await Assert.That(result).IsTypeOf<ExpectedType>();

// Exceptions (async)
await Assert.That(async () => await method()).ThrowsExactly<ArgumentException>();

// Exceptions (sync, value-returning)
await Assert.That(() => _ = method()).Throws<ArgumentException>();
```

### Verify (snapshot testing)

Verify is **not** in `Directory.Build.props` — add packages per test project as needed.

- **`Verify()` vs `Assert.That()`:** use `Verify()` when asserting an object with 2+ properties. Use `Assert.That()` only for single scalar values. If writing 2+ `Assert.That()` calls on the same object's properties, use `Verify()` instead.
- Parameterized tests with Verify must call `.UseParameters(param)`.
- Commit `.verified.txt` / `.verified.cs` files, gitignore `.received.*` files.
- Use fixed values (e.g. `Guid.Parse(...)`) not `Guid.NewGuid()` when the value appears in snapshot output.
- **Initializer:** every test project using Verify needs a `ModuleInitializer` calling `UseProjectRelativeDirectory("snapshots")`.

## Coverage

- 95%+ recommended, 100% aspired
- Generated code excluded via `coverage.settings.xml` (`.g.cs` files + `[ExcludeFromCodeCoverage]`)

### Commands

```bash
dotnet build Glot.slnx -c Release

dotnet test --solution Glot.slnx -c Release -- \
  --coverage --coverage-settings coverage.settings.xml \
  --coverage-output-format cobertura \
  --coverage-output coverage.cobertura.xml

reportgenerator \
  -reports:"tests/*/bin/Release/net10.0/TestResults/coverage.cobertura.xml" \
  -targetdir:TestResults/CoverageReport \
  -reporttypes:"TextSummary"
```

## TDD Workflow (recommended)

1. Write failing test (red)
2. Implement minimum to pass (green)
3. Refactor, keep green
4. Check coverage

## Don'ts

- **Don't** use `--collect:"XPlat Code Coverage"` — TUnit uses `--coverage` flag
- **Don't** use `dotnet test <solution>` — use `dotnet test --solution <solution>`
- **Don't** test record-generated equality/GetHashCode — records handle this
- **Don't** duplicate transitive package references in test csproj — if the production project references a package, it's already available transitively
