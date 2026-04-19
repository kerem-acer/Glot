using System.Text;

namespace Glot.Tests;

public partial class TextTests
{
    // Factory — From(string)

    [Test]
    public Task From_String_StoresStringBacking()
    {
        // Arrange & Act
        var text = Text.From("Hello");

        // Assert
        return Verify(new { result = text.ToString(), text.Encoding, text.RuneLength, text.ByteLength, text.IsEmpty });
    }

    [Test]
    public Task From_EmptyString_ReturnsEmpty()
    {
        // Act
        var text = Text.From("");

        // Assert
        return Verify(new { text.IsEmpty, text.RuneLength, text.ByteLength });
    }

    [Test]
    public async Task From_NullString_ReturnsEmpty()
    {
        // Act
        var text = Text.From(null!);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromUtf8

    [Test]
    public Task FromUtf8_Bytes_StoresUtf8()
    {
        // Arrange & Act
        var text = Text.FromUtf8("café"u8);

        // Assert
        return Verify(new { text.Encoding, text.RuneLength, text.ByteLength });
    }

    [Test]
    public async Task FromUtf8_Empty_ReturnsEmpty()
    {
        // Act
        var text = Text.FromUtf8(Array.Empty<byte>());

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromChars

    [Test]
    public Task FromChars_Span_StoresUtf16()
    {
        // Arrange & Act
        var text = Text.FromChars("Hello".AsSpan());

        // Assert
        return Verify(new { text.Encoding, text.RuneLength });
    }

    // Factory — FromUtf32

    [Test]
    public Task FromUtf32_IntSpan_StoresUtf32()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C]; // H, e, l

        // Act
        var text = Text.FromUtf32(codePoints);

        // Assert
        return Verify(new { text.Encoding, text.RuneLength, text.ByteLength });
    }

    // Factory — FromBytes

    [Test]
    public Task FromBytes_Utf16Bytes_StoresCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var text = Text.FromBytes(bytes, TextEncoding.Utf16);

        // Assert
        return Verify(new { text.Encoding, text.RuneLength });
    }

    // Factory — implicit from string

    [Test]
    public Task ImplicitFromString_CreatesText()
    {
        // Act
        Text text = "Hello";

        // Assert
        return Verify(new { text.RuneLength, text.Encoding });
    }

    // AsSpan

    [Test]
    public async Task AsSpan_StringBacked_ReturnsCorrectSpan()
    {
        // Arrange
        const string expected = "Hello";
        var text = Text.From(expected);

        // Act
        var span = text.AsSpan();
        var eq = span.Equals(expected.AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task AsSpan_Utf8Backed_ReturnsCorrectSpan()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);

        // Act
        var span = text.AsSpan();
        var eq = span.Equals("café".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task AsSpan_Empty_ReturnsEmptySpan()
    {
        // Arrange
        var text = Text.Empty;

        // Act
        var span = text.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }

    // ToString

    [Test]
    public async Task ToString_StringBacked_ReturnsSameString()
    {
        // Arrange
        const string original = "Hello World";
        var text = Text.From(original);

        // Act
        var result = text.ToString();

        // Assert
        await Assert.That(ReferenceEquals(result, original)).IsTrue();
    }

    [Test]
    public async Task ToString_Utf8Backed_ReturnsCorrectString()
    {
        // Arrange
        const string expected = "café🎉";
        var text = Text.FromUtf8("café🎉"u8);

        // Act
        var result = text.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ToString_Empty_ReturnsEmptyString()
    {
        // Act
        var result = Text.Empty.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    // Equality

    [Test]
    public async Task Equals_SameContent_DifferentEncoding_ReturnsTrue()
    {
        // Arrange
        var utf8 = Text.FromUtf8("Hello"u8);
        var utf16 = Text.From("Hello");

        // Act
        var result = utf8.Equals(utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_DifferentContent_ReturnsFalse()
    {
        // Arrange
        var a = Text.From("Hello");
        var b = Text.From("World");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsFalse();
    }

    [Test]
    public async Task Equals_String_ReturnsTrue()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(text.Equals("Hello")).IsTrue();
    }

    [Test]
    public async Task Equals_NullString_EmptyText_ReturnsFalse()
    {
        // Act & Assert
        await Assert.That(Text.Empty.Equals((string?)null)).IsFalse();
    }

    [Test]
    public async Task GetHashCode_SameContentDifferentEncoding_DifferentHash()
    {
        // Arrange — hash is byte-level, so different encodings produce different hashes
        var utf8 = Text.FromUtf8("Hello"u8);
        var utf16 = Text.From("Hello");

        // Act
        var hash1 = utf8.GetHashCode();
        var hash2 = utf16.GetHashCode();

        // Assert
        await Assert.That(hash1).IsNotEqualTo(hash2);
    }

    [Test]
    public async Task Operators_EqualityWorks()
    {
        // Arrange
        var a = Text.From("Hello");
        var b = Text.From("Hello");

        // Act & Assert
        await Assert.That(a == b).IsTrue();
        await Assert.That(a != b).IsFalse();
    }

    [Test]
    public async Task CompareTo_LessThan_ReturnsNegative()
    {
        // Arrange
        var a = Text.From("A");
        var b = Text.From("B");

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    // Search

    [Test]
    public async Task Contains_String_Found_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(text.Contains("World")).IsTrue();
    }

    [Test]
    public async Task Contains_Utf8_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(text.Contains("World"u8)).IsTrue();
    }

    [Test]
    public async Task StartsWith_String_ReturnsTrue()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act & Assert
        await Assert.That(text.StartsWith("Hello")).IsTrue();
    }

    [Test]
    public async Task EndsWith_String_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(text.EndsWith("World")).IsTrue();
    }

    [Test]
    public async Task RuneIndexOf_String_ReturnsPosition()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var pos = text.RuneIndexOf("World");

        // Assert
        await Assert.That(pos).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_Utf8_ReturnsBytePosition()
    {
        // Arrange — "café" UTF-8 = [63 61 66 C3 A9], "é" at byte 3
        var text = Text.FromUtf8("café"u8);

        // Act
        var pos = text.ByteIndexOf("é"u8);

        // Assert
        await Assert.That(pos).IsEqualTo(3);
    }

    // Trim (non-copying)

    [Test]
    public Task TrimStart_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.TrimStart();

        // Assert
        return Verify(new { result = trimmed.ToString(), trimmed.RuneLength });
    }

    [Test]
    public Task TrimEnd_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.TrimEnd();

        // Assert
        return Verify(new { result = trimmed.ToString(), trimmed.RuneLength });
    }

    [Test]
    public Task Trim_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.Trim();

        // Assert
        return Verify(new { result = trimmed.ToString(), trimmed.RuneLength });
    }

    [Test]
    public Task Trim_Utf8_ReturnsCorrectText()
    {
        // Arrange
        var text = Text.FromUtf8(" café "u8);

        // Act
        var trimmed = text.Trim();

        // Assert
        return Verify(new { result = trimmed.ToString(), trimmed.RuneLength });
    }

    // Slice (non-copying)

    [Test]
    public Task RuneSlice_Offset_ReturnsTextView()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var sliced = text.RuneSlice(6);

        // Assert
        return Verify(new { result = sliced.ToString(), sliced.RuneLength });
    }

    [Test]
    public Task RuneSlice_OffsetAndCount_ReturnsTextView()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var sliced = text.RuneSlice(6, 5);

        // Assert
        return Verify(new { result = sliced.ToString(), sliced.RuneLength });
    }

    [Test]
    public async Task ByteSlice_ReturnsTextView()
    {
        // Arrange
        const string expected = "lo";
        var text = Text.FromUtf8("Hello"u8);

        // Act
        var sliced = text.ByteSlice(3);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ByteSlice_OffsetAndCount_ReturnsTextView()
    {
        // Arrange
        const string expected = "World";
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var sliced = text.ByteSlice(6, 5);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo(expected);
    }

    // Split

    [Test]
    public async Task Split_String_SplitsCorrectly()
    {
        // Arrange
        var text = Text.From("a,b,c");

        // Act
        var count = 0;
        foreach (var _ in text.Split(","))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_Utf8_SplitsCorrectly()
    {
        // Arrange
        var text = Text.FromUtf8("a,b,c"u8);

        // Act
        var count = 0;
        foreach (var _ in text.Split(","u8))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    // EnumerateRunes

    [Test]
    public async Task EnumerateRunes_YieldsAllRunes()
    {
        // Arrange
        var text = Text.FromUtf8("A🎉B"u8);

        // Act
        var runes = new List<Rune>();
        foreach (var rune in text.EnumerateRunes())
        {
            runes.Add(rune);
        }

        // Assert
        await Assert.That(runes.Count).IsEqualTo(3);
        await Assert.That(runes[0].Value).IsEqualTo('A');
        await Assert.That(runes[1].Value).IsEqualTo(0x1F389);
        await Assert.That(runes[2].Value).IsEqualTo('B');
    }

    // TryFormat — ISpanFormattable

    [Test]
    public async Task TryFormat_Chars_StringBacked_WritesToDestination()
    {
        // Arrange
        const string expected = "Hello";
        var text = Text.From(expected);
        var dest = new char[10];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo(expected);
    }

    [Test]
    public async Task TryFormat_Chars_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new char[3];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat — IUtf8SpanFormattable

    [Test]
    public async Task TryFormat_Utf8_Utf8Backed_DirectCopy()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new byte[10];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Utf8_Utf16Backed_Transcodes()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new byte[10];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Utf8_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new byte[3];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // Chars / Ints / Bytes properties

    [Test]
    public async Task Bytes_ReturnsRawBytes()
    {
        // Arrange
        var text = Text.FromUtf8("Hi"u8);

        // Act
        var span = text.AsSpan();
        var len = span.ByteLength;

        // Assert
        await Assert.That(len).IsEqualTo(2);
    }

    // Cross-encoding round-trip

    [Test]
    public async Task CrossEncoding_Utf8TextEqualsUtf32Text()
    {
        // Arrange
        var utf8 = Text.FromUtf8("café"u8);
        var utf32Bytes = TestHelpers.Encode("café", TextEncoding.Utf32);
        var utf32 = Text.FromBytes(utf32Bytes, TextEncoding.Utf32);

        // Act & Assert — Equals works cross-encoding, but hash is byte-level (encoding-specific)
        await Assert.That(utf8.Equals(utf32)).IsTrue();
    }

    // ToString on trimmed string-backed Text

    [Test]
    public async Task ToString_TrimmedStringBacked_AllocatesNewString()
    {
        // Arrange
        const string original = "  Hello  ";
        const string expected = "Hello";
        var text = Text.From(original);
        var trimmed = text.Trim();

        // Act
        var result = trimmed.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(ReferenceEquals(result, original)).IsFalse();
    }

    // Factory — empty inputs

    [Test]
    public async Task FromChars_Empty_ReturnsDefault()
    {
        // Act
        var text = Text.FromChars(Array.Empty<char>());

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task FromUtf32_Empty_ReturnsDefault()
    {
        // Act
        var text = Text.FromUtf32(Array.Empty<int>());

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task FromBytes_Empty_ReturnsDefault()
    {
        // Act
        var text = Text.FromBytes(Array.Empty<byte>(), TextEncoding.Utf8);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromChars creates char[]-backed text

    [Test]
    public Task FromChars_NonEmpty_CreatesCharBacked()
    {
        // Act
        var text = Text.FromChars("Hello".AsSpan());

        // Assert
        return Verify(new { result = text.ToString(), text.Encoding, text.RuneLength });
    }

    // Mutation validation — Replace

    [Test]
    public async Task Replace_EmptyOldValue_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Replace(Text.Empty, Text.From("x"))).Throws<ArgumentException>();
    }

    [Test]
    public async Task Replace_EmptyText_ReturnsThis()
    {
        // Arrange
        var text = Text.Empty;

        // Act
        var result = text.Replace("x", "y");

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Replace_NoMatch_ReturnsThis()
    {
        // Arrange
        const string expected = "Hello";
        var text = Text.From(expected);

        // Act
        var result = text.Replace(Text.From("xyz"), Text.From("abc"));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ReplacePooled_EmptyOldValue_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.ReplacePooled(Text.Empty, Text.From("x"))).Throws<ArgumentException>();
    }

    [Test]
    public async Task ReplacePooled_EmptyText_ReturnsEmpty()
    {
        // Act
        var result = Text.Empty.ReplacePooled(Text.From("x"), Text.From("y"));

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task Replace_EmptyStringOldValue_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Replace("", "x")).Throws<ArgumentException>();
    }

    [Test]
    public async Task Replace_String_EmptyText_ReturnsThis()
    {
        // Act
        var result = Text.Empty.Replace("x", "y");

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ReplacePooled_StringEmptyOldValue_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.ReplacePooled("", "x")).Throws<ArgumentException>();
    }

    [Test]
    public async Task ReplacePooled_StringEmptyText_ReturnsEmpty()
    {
        // Act
        var result = Text.Empty.ReplacePooled("x", "y");

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    // Mutation validation — Insert

    [Test]
    public async Task Insert_NegativeIndex_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Insert(-1, Text.From("x"))).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Insert_IndexBeyondLength_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Insert(100, Text.From("x"))).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task InsertPooled_NegativeIndex_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.InsertPooled(-1, Text.From("x"))).Throws<ArgumentOutOfRangeException>();
    }

    // Mutation validation — Remove

    [Test]
    public async Task Remove_NegativeIndex_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Remove(-1, 1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_CountBeyondLength_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.Remove(0, 100)).Throws<ArgumentOutOfRangeException>();
    }

    // Search validation — RuneSlice/ByteSlice

    [Test]
    public async Task RuneSlice_NegativeOffset_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.RuneSlice(-1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task RuneSlice_OffsetCount_NegativeOffset_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.RuneSlice(-1, 1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task RuneSlice_OffsetCount_CountBeyondLength_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.RuneSlice(0, 100)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ByteSlice_NegativeOffset_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.ByteSlice(-1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ByteSlice_OffsetCount_NegativeOffset_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.ByteSlice(-1, 1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ByteSlice_OffsetCount_CountBeyondLength_Throws()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(() => _ = text.ByteSlice(0, 100)).Throws<ArgumentOutOfRangeException>();
    }

    // ReplacePooled(Text, Text) — match and no-match paths

    [Test]
    public async Task ReplacePooled_Text_WithMatch_Replaces()
    {
        // Arrange
        const string expected = "Hello Glot";
        var text = Text.From("Hello World");

        // Act
        using var result = text.ReplacePooled(Text.From("World"), Text.From("Glot"))!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ReplacePooled_Text_NoMatch_ReturnsCopy()
    {
        // Arrange
        const string expected = "Hello";
        var text = Text.From(expected);

        // Act
        using var result = text.ReplacePooled(Text.From("xyz"), Text.From("abc"))!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    // Replace(Text, Text) — empty self

    [Test]
    public async Task Replace_Text_EmptyText_ReturnsThis()
    {
        // Act
        var result = Text.Empty.Replace(Text.From("x"), Text.From("y"));

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    // ToLowerInvariantPooled — no change

    [Test]
    public async Task ToLowerInvariantPooled_AlreadyLower_ReturnsCopy()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.From(expected);

        // Act
        using var result = text.ToLowerInvariantPooled()!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    // CaseCore — trailing unchanged run after case change

    [Test]
    public async Task ToUpperInvariant_TrailingUnchangedRun_Works()
    {
        // Arrange
        const string expected = "A!!!";

        // Act — '!' is already uppercase, so after 'a' → 'A' there's an unchanged trailing run
        var result = Text.From("a!!!").ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // TryGetUtf16Memory — char[]-backed text

    [Test]
    public async Task FromChars_InterpolationViaTryGetUtf16Memory_Works()
    {
        // Arrange — FromChars creates char[]-backed Text
        const string expected = "hello";
        var text = Text.FromChars("hello".AsSpan());

        // Act — LinkedTextUtf16 interpolation calls TryGetUtf16Memory
        var linked = LinkedTextUtf16.Create(text);

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo(expected);
    }
}
