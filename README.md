# Glot

A text type for .NET that stores UTF-8, UTF-16, or UTF-32 data behind a single API. The encoding is an implementation detail — you work with `Text`, not bytes or chars.

## Install

```
dotnet add package Glot
```

Serialization packages (optional):

```
dotnet add package Glot.SystemTextJson
dotnet add package Glot.NewtonsoftJson
dotnet add package Glot.VYaml
```

## Why

`System.String` is always UTF-16. When your data arrives as UTF-8 (JSON, HTTP, files), you pay for transcoding just to call `.Contains()`. Glot lets you operate on text in its native encoding.

## Quick start

```csharp
using Glot;

// From string (UTF-16, zero-copy — stores string reference)
Text greeting = Text.From("hello");

// From UTF-8 bytes (no transcoding to UTF-16)
Text utf8 = Text.FromUtf8("hello"u8);

// Implicit from string
Text t = "hello";

// Encoding-independent equality
Text a = Text.From("cafe\u0301");
Text b = Text.FromUtf8("café"u8);
bool same = a == b; // rune-by-rune comparison

// Search, slice, mutate — same API regardless of encoding
bool found = utf8.Contains("ell");
Text sub = utf8.RuneSlice(1, 3);
Text upper = utf8.ToUpperInvariant();
```

## Types

| Type | Kind | Purpose |
|------|------|---------|
| `Text` | `readonly struct` | Main type. Stores backing data as-is (`byte[]`, `char[]`, `string`, `int[]`). |
| `TextSpan` | `ref readonly struct` | Stack-only view. Normalizes to `ReadOnlySpan<byte>` via `MemoryMarshal.AsBytes`. Zero-alloc slicing and searching. |
| `OwnedText` | `readonly struct`, `IDisposable` | Owns a pool-backed buffer. Returns to `ArrayPool` on dispose. |
| `TextBuilder` | `struct`, `IDisposable` | Mutable, pooled builder. Accumulates encoded bytes with automatic transcoding. |
| `Utf8Text`, `Utf16Text`, `Utf32Text` | `readonly struct` | Typed views with encoding-specific span access as a property (guaranteed zero-alloc). |
| `LinkedTextUtf16`, `LinkedTextUtf8` | `sealed class` | Segmented text — stores references to original segments without copying. |

### Relationship

```
TextBuilder  -->  OwnedText  -->  Text  -->  TextSpan
 (builds)          (owns)        (holds)     (views as ReadOnlySpan<byte>)
                                    |
                               Utf8Text / Utf16Text / Utf32Text
                                (typed views, encoding-specific span)
```

### Internal representation

| Type | Internal storage | Notes |
|------|-----------------|-------|
| `Text` | `object` (`byte[]`, `char[]`, `string`, `int[]`) | Stores data in original form |
| `TextSpan` | `ReadOnlySpan<byte>` | `char[]`/`string`/`int[]` cast via `MemoryMarshal.AsBytes` |
| `OwnedText` | `byte[]`, `char[]`, or `int[]` from `ArrayPool` | Returned to pool on dispose |
| `TextBuilder` | `byte[]` from `ArrayPool<byte>` | Doubles on growth, returns on dispose |

## Construction

```csharp
// From string (UTF-16, zero-copy — stores string reference)
var t = Text.From("hello");

// From UTF-8 bytes
var t = Text.FromUtf8(utf8Span);
var t = Text.FromUtf8("hello"u8);

// From char span (UTF-16)
var t = Text.FromChars(charSpan);

// From UTF-32 code points
var t = Text.FromUtf32(codePoints);

// From raw bytes with explicit encoding
var t = Text.FromBytes(rawBytes, TextEncoding.Utf8);

// Implicit from string
Text t = "hello";

// Pooled variants — backed by ArrayPool
using var t = OwnedText.FromUtf8(utf8Span);
using var t = OwnedText.FromChars(charSpan);
using var t = OwnedText.FromUtf32(codePoints);

// Take ownership of existing pooled buffer (zero-copy)
var owned = OwnedText.Create(pooledByteArray, byteLength, TextEncoding.Utf8);
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
ReadOnlySpan<byte> bytes = text.Bytes;
ReadOnlySpan<char> chars = text.Chars;
ReadOnlySpan<int>  ints  = text.Ints;

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

// Index — rune position (O(n))
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

## Typed views

Convert `Text` to encoding-specific readonly structs where span access is a property:

```csharp
Utf8Text utf8 = text.AsUtf8();       // zero-cost if already UTF-8
ReadOnlySpan<byte> span = utf8.Span;

Utf16Text utf16 = text.AsUtf16();
ReadOnlySpan<char> span = utf16.Span;

Utf32Text utf32 = text.AsUtf32();
ReadOnlySpan<int> span = utf32.Span;
```

## TextBuilder

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

## Parsing

```csharp
Text number = Text.FromUtf8("42"u8);

// Primitives
int n = int.Parse(number);
bool ok = double.TryParse(number, out double d);

// ISpanParsable<T> (.NET 7+)
MyType.TryParse(number, out var result);

// IUtf8SpanParsable<T> (.NET 8+ — zero-alloc for UTF-8 backed text)
int.TryParseUtf8(number, out var result);
```

## Formatting

```csharp
// ISpanFormattable
text.TryFormat(charDestination, out charsWritten);

// IUtf8SpanFormattable (.NET 8+)
text.TryFormat(utf8Destination, out bytesWritten);

// ToString — cached when backed by string
string s = text.ToString();
```

## LinkedText

Segmented text that holds references to original string segments without copying. Instead of `$"{a} - {b}"` allocating `a.Length + b.Length + 3` chars, LinkedText stores each piece as a separate `ReadOnlyMemory<T>` segment.

Uses `[InlineArray(8)]` on .NET 8+ for up to 8 segments with zero extra allocation, overflow to `ArrayPool` beyond that.

```csharp
// From strings
var linked = LinkedTextUtf16.Create("hello", " - ", "world");

// String interpolation (.NET 6+) — zero-copy for string holes
var linked = LinkedTextUtf16.Create($"Hello {name}, count={count}");

// Pooled — object and buffers returned on dispose
using var owned = OwnedLinkedTextUtf16.Create($"Hello {name}!");

// Span view, slicing, enumeration
LinkedTextUtf16Span span = linked.AsSpan();
LinkedTextUtf16Span sub = span[3..10];

foreach (ReadOnlyMemory<char> segment in span.EnumerateSegments())
{
    // each segment is a slice of the original data
}

// ReadOnlySequence interop
ReadOnlySequence<char> seq = linked.AsSequence();
```

After warmup, `OwnedLinkedTextUtf16.Create` with string interpolation is **zero-alloc**: the instance is reused from the pool, string holes are zero-copy, and non-string values are formatted directly into a retained buffer via `ISpanFormattable`.

## Lifetime

```csharp
// OwnedText is IDisposable — returns pooled buffer
using var owned = OwnedText.FromUtf8(data);
Text text = owned.Text;

// String-backed Text has nothing to dispose
Text text = Text.From("hello");

// TextSpan borrows from Text — Text must outlive the span
TextSpan span = text.AsSpan();

// TextBuilder is IDisposable — returns pooled buffer
using var builder = new TextBuilder(TextEncoding.Utf8);
```

## Serialization

### System.Text.Json

```
dotnet add package Glot.SystemTextJson
```

```csharp
using Glot.SystemTextJson;

var options = new JsonSerializerOptions();
options.Converters.Add(new TextJsonConverter());
options.Converters.Add(new OwnedTextJsonConverter());

Text text = JsonSerializer.Deserialize<Text>(json, options);
```

Reads UTF-8 JSON bytes directly into `Text` without allocating an intermediate string. Writes UTF-8/UTF-16 backed text without transcoding.

### Newtonsoft.Json

```
dotnet add package Glot.NewtonsoftJson
```

```csharp
using Glot.NewtonsoftJson;

var settings = new JsonSerializerSettings();
settings.Converters.Add(new TextJsonConverter());
settings.Converters.Add(new OwnedTextJsonConverter());
```

### VYaml

```
dotnet add package Glot.VYaml
```

```csharp
using Glot.VYaml;

// Register formatters
var resolver = CompositeResolver.Create(
    new IYamlFormatterResolver[] { /* your resolvers */ },
    new IYamlFormatter[] { TextYamlFormatter.Instance, OwnedTextYamlFormatter.Instance }
);
```

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
public sealed class LinkedTextUtf16
public readonly struct LinkedTextUtf16Span
public struct LinkedTextUtf16.Owned : IDisposable
public sealed class LinkedTextUtf8
public readonly struct LinkedTextUtf8Span
public struct LinkedTextUtf8.Owned : IDisposable
```

## Target frameworks

`net10.0`, `net8.0`, `net6.0`, `netstandard2.1`, `netstandard2.0`

Serialization packages drop `net6.0` where the underlying library requires newer APIs.

## License

MIT
