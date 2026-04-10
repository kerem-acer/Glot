# Glot

A text type for .NET that stores UTF-8, UTF-16, or UTF-32 data behind a single API. The encoding is an implementation detail — you work with `Text`, not bytes or chars.

## Why

`System.String` is always UTF-16. When your data arrives as UTF-8 (JSON, HTTP, files), you pay for transcoding just to call `.Contains()`. Glot lets you operate on text in its native encoding.

## Types

**`Text`** — `readonly struct`. The main type. Stores backing data as-is (`byte[]`, `char[]`, `string`, or `int[]`). Storable, passable, comparable. Has an `Encoding` property for the encoding.

**`TextSpan`** — `ref readonly struct`. A stack-only view over text. Always holds `ReadOnlySpan<byte>` — normalizes from `char[]`/`string` via `MemoryMarshal.AsBytes` at zero cost. Zero-alloc slicing and searching. Cannot escape the stack.

**`OwnedText`** — `readonly struct`, `IDisposable`. Owns a pool-backed buffer. Returns the buffer to `ArrayPool` on dispose. Produces `Text` values for reading.

**`TextBuilder`** — `struct`, `IDisposable`. A mutable, pooled builder for constructing `Text` or `OwnedText` values. Accumulates encoded bytes in a target encoding with automatic transcoding.

### Typed views

`Text` can be converted to encoding-specific readonly structs:

**`Utf8Text`**, **`Utf16Text`**, **`Utf32Text`** — typed views where span access is a property (guaranteed zero-alloc, no transcoding).

```csharp
Utf8Text utf8 = text.AsUtf8();       // zero-cost if already UTF-8
ReadOnlySpan<byte> span = utf8.Span; // property, always cheap

Utf16Text utf16 = text.AsUtf16();
ReadOnlySpan<char> span = utf16.Span;

Utf32Text utf32 = text.AsUtf32();
ReadOnlySpan<int> span = utf32.Span;
```

### Relationship

```
TextBuilder  →  OwnedText  →  Text  →  TextSpan
 (builds)        (owns)      (holds)    (views as ReadOnlySpan<byte>)
                                ↓
                           Utf8Text / Utf16Text / Utf32Text
                            (typed views, encoding-specific span)
```

- `TextBuilder` accumulates encoded bytes in a pooled buffer, produces `Text` or `OwnedText`
- `OwnedText` manages the pooled buffer lifetime
- `Text` stores the backing data (`byte[]`, `char[]`, `string`, `int[]`) as-is
- `TextSpan` normalizes everything to `ReadOnlySpan<byte>` for uniform stack-only operations
- Typed views provide encoding-specific span access as cheap properties

### Internal representation

| Type | Internal storage | Notes |
|------|-----------------|-------|
| `Text` | `object` (`byte[]`, `char[]`, `string`, `int[]`) | Stores data in original form |
| `TextSpan` | `ReadOnlySpan<byte>` | `char[]`/`string`/`int[]` cast via `MemoryMarshal.AsBytes` |
| `OwnedText` | `byte[]`, `char[]`, or `int[]` from `ArrayPool` | Returned to pool on dispose |
| `TextBuilder` | `byte[]` from `ArrayPool<byte>` | Doubles on growth, returns on dispose |

## Encodings

| Encoding | `Text` backing | `TextSpan` representation |
|----------|---------------|--------------------------|
| UTF-8    | `byte[]`      | `ReadOnlySpan<byte>` (as-is) |
| UTF-16   | `char[]`, `string` | `ReadOnlySpan<byte>` via `MemoryMarshal.AsBytes` |
| UTF-32   | `int[]`       | `ReadOnlySpan<byte>` via `MemoryMarshal.AsBytes` |

## Construction

```csharp
// From string (UTF-16, zero-copy — stores string reference)
var t = Text.From("hello");

// From UTF-8 bytes (copies into new byte[])
var t = Text.FromUtf8(utf8Span);

// From UTF-8 literal (copies into new byte[])
var t = Text.FromUtf8("hello"u8);

// From char span (UTF-16, copies into new char[])
var t = Text.FromChars(charSpan);

// From UTF-32 code points (copies into new int[])
var t = Text.FromUtf32(codePoints);

// From raw bytes with explicit encoding
var t = Text.FromBytes(rawBytes, TextEncoding.Utf8);

// Implicit from string
Text t = "hello";

// Pooled — from UTF-8 bytes (copies into pooled buffer)
using var t = OwnedText.FromUtf8(utf8Span);

// Pooled — from char span (copies into pooled buffer)
using var t = OwnedText.FromChars(charSpan);

// Pooled — from UTF-32 code points (copies into pooled buffer)
using var t = OwnedText.FromUtf32(codePoints);

// Pooled — take ownership of existing pooled buffer (zero-copy)
var owned = OwnedText.Create(pooledByteArray, byteLength, TextEncoding.Utf8);
var owned = OwnedText.Create(pooledCharArray, charLength);
var owned = OwnedText.Create(pooledIntArray, intLength);
```

## Operations

```csharp
// Encoding
TextEncoding encoding = text.Encoding; // Utf8, Utf16, Utf32

// Length
int runes = text.RuneLength;  // Unicode scalar count, O(1)
int bytes = text.ByteLength;  // byte count
bool empty = text.IsEmpty;

// Raw access
ReadOnlySpan<byte> bytes = text.Bytes; // raw bytes
ReadOnlySpan<char> chars = text.Chars; // reinterpret as char
ReadOnlySpan<int>  ints  = text.Ints;  // reinterpret as int

// Equality (encoding-independent, rune-by-rune)
text1 == text2
text.Equals("hello")
text.Equals("hello"u8)

// Comparison (lexicographic by rune value)
text.CompareTo(other)

// Search
text.Contains("world")
text.Contains("world"u8)
text.StartsWith("he")
text.EndsWith("lo"u8)

// Index — rune position (O(n) — counts runes to match)
text.RuneIndexOf("ll")
text.LastRuneIndexOf("ll")

// Index — byte offset (O(1) result lookup)
text.ByteIndexOf("ll")
text.LastByteIndexOf("ll")

// Slicing (returns Text, non-copying sub-view)
Text sub = text.RuneSlice(2);       // from rune 2 to end
Text sub = text.RuneSlice(2, 3);    // 3 runes starting at rune 2
Text sub = text.ByteSlice(4);       // from byte 4 to end
Text sub = text.ByteSlice(4, 6);    // 6 bytes starting at byte 4

// TextSpan (stack-only view, zero-alloc)
TextSpan span = text.AsSpan();
TextSpan sub = span.RuneSlice(2, 3);
TextSpan sub = span.ByteSlice(4, 6);
TextSpan sub = span[2..5]; // rune-based range indexer

// Trim
text.Trim()
text.TrimStart()
text.TrimEnd()

// Split (allocation-free enumeration)
foreach (TextSpan segment in text.Split(","))
{
    // each segment is a zero-alloc slice
}

// Rune enumeration
foreach (Rune rune in text.EnumerateRunes())
{
    // iterate Unicode scalar values
}
```

## Mutation

All mutation methods return new values — `Text` is immutable. Each method has a `Pooled` variant that returns `OwnedText` backed by `ArrayPool`.

```csharp
// Replace
Text result = text.Replace("old", "new");
using OwnedText result = text.ReplacePooled("old", "new");

// Insert (at rune index)
Text result = text.Insert(3, "inserted");
using OwnedText result = text.InsertPooled(3, "inserted");

// Remove (at rune index, count)
Text result = text.Remove(2, 4);
using OwnedText result = text.RemovePooled(2, 4);

// Case conversion (invariant)
Text upper = text.ToUpperInvariant();
Text lower = text.ToLowerInvariant();
using OwnedText upper = text.ToUpperInvariantPooled();
using OwnedText lower = text.ToLowerInvariantPooled();

// Concatenation
Text result = Text.Concat(a, b, c);
using OwnedText result = Text.ConcatPooled(a, b, c);
Text result = a + b;
```

## TextBuilder

A mutable, pooled builder for constructing text. Transcodes automatically when appending from different encodings.

```csharp
using var builder = new TextBuilder(TextEncoding.Utf8);

builder.Append("hello ");
builder.Append(someUtf16Text);     // transcoded to UTF-8
builder.AppendRune(new Rune('!')); // single rune
builder.AppendLine();

Text result = builder.ToText();           // copies to exact-size array
using OwnedText pooled = builder.ToOwnedText(); // transfers buffer ownership
```

## Interpolation

Build `Text` from interpolated strings without intermediate string allocation (.NET 6+).

```csharp
int count = 42;
Text name = Text.From("world");

Text result = Text.Format($"hello {name}, count={count}");
Text utf8 = Text.Format(TextEncoding.Utf8, $"value: {count:N0}");

using OwnedText pooled = Text.FormatPooled($"{name}: {count}");
```

Supports `IUtf8SpanFormattable` (NET 8+), `ISpanFormattable`, and `IFormattable` for formatted holes — no boxing, no intermediate strings when possible.

## Parsing

Parse `Text` directly into primitives and `ISpanParsable<T>` types.

```csharp
Text number = Text.FromUtf8("42"u8);

// Primitives (all target frameworks)
int n = int.Parse(number);
bool ok = double.TryParse(number, out double d);
// Supported: byte, sbyte, short, ushort, int, uint, long, ulong,
//            float, double, decimal, bool, Guid, DateTime, DateTimeOffset

// ISpanParsable<T> (.NET 7+)
var value = MyType.Parse(number);
MyType.TryParse(number, out var result);

// IUtf8SpanParsable<T> (.NET 8+ — zero-alloc for UTF-8 backed text)
var value = int.ParseUtf8(number);
int.TryParseUtf8(number, out var result);
```

## Formatting

```csharp
// ISpanFormattable — write to char destination
text.TryFormat(charDestination, out charsWritten);

// IUtf8SpanFormattable — write to byte destination (.NET 8+)
text.TryFormat(utf8Destination, out bytesWritten);

// ToString — always works, cached when backed by string
string s = text.ToString();
```

## Lifetime

```csharp
// OwnedText is IDisposable — returns pooled buffer
using var owned = OwnedText.FromUtf8(data);
Text text = owned.Text;

// String-backed Text has nothing to dispose
Text text = Text.From("hello");

// TextSpan borrows from Text — Text must outlive the span
TextSpan span = text.AsSpan();
TextSpan slice = span[2..5]; // valid while text is alive

// TextBuilder is IDisposable — returns pooled buffer
using var builder = new TextBuilder(TextEncoding.Utf8);
```

## Equality and Hashing

- `IEquatable<Text>` — encoding-independent rune-by-rune comparison
- `==` / `!=` operators
- `GetHashCode()` produces identical hash regardless of backing encoding
- Works correctly as dictionary key

## Interfaces

```csharp
public readonly struct Text :
    IEquatable<Text>,
    IComparable<Text>,
    ISpanFormattable,          // .NET 6+
    IUtf8SpanFormattable,      // .NET 8+
    ISpanParsable<Text>,       // .NET 7+
    IUtf8SpanParsable<Text>    // .NET 8+

public ref readonly struct TextSpan :
    ISpanFormattable,          // .NET 6+
    IUtf8SpanFormattable,      // .NET 8+
    IEquatable<TextSpan>,      // .NET 9+
    IComparable<TextSpan>      // .NET 9+

public readonly struct OwnedText : IDisposable

public struct TextBuilder : IDisposable

public readonly struct Utf8Text
public readonly struct Utf16Text
public readonly struct Utf32Text
```

## Scope

What Glot provides:
- Core types: `Text`, `TextSpan`, `OwnedText`, `TextBuilder`
- Typed views: `Utf8Text`, `Utf16Text`, `Utf32Text`
- Construction from string, UTF-8 bytes, UTF-16 chars, UTF-32 code points
- Equality, comparison, hashing across encodings
- Search: Contains, StartsWith, EndsWith, RuneIndexOf, ByteIndexOf (and Last variants)
- Slicing: RuneSlice, ByteSlice, range indexer
- Trim, Split, EnumerateRunes
- Mutation: Replace, Insert, Remove, case conversion, Concat
- Interpolated string handler (no intermediate string allocation)
- Parsing: primitives and `ISpanParsable<T>` / `IUtf8SpanParsable<T>`
- Typed and untyped span access
- Pool-backed buffer management

What Glot does not provide:
- Culture-aware operations (ordinal only)
- Regular expressions
- Encoding detection
- File or stream I/O

## Target

- `net10.0;net8.0;net6.0;netstandard2.1;netstandard2.0`
- Zero runtime dependencies (uses `ArrayPool<T>` from the BCL)

## License

MIT
