using System.Text;

namespace Glot.Tests;

public partial class TextTests
{
    // Factory — From(string)

    [Test]
    public async Task From_String_StoresStringBacking()
    {
        // Arrange & Act
        var text = Text.From("Hello");

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.ByteLength).IsEqualTo(10);
        await Assert.That(text.IsEmpty).IsFalse();
    }

    [Test]
    public async Task From_EmptyString_ReturnsEmpty()
    {
        // Act
        var text = Text.From("");

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
        await Assert.That(text.RuneLength).IsEqualTo(0);
        await Assert.That(text.ByteLength).IsEqualTo(0);
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
    public async Task FromUtf8_Bytes_StoresUtf8()
    {
        // Arrange & Act
        var text = Text.FromUtf8("café"u8);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(4);
        await Assert.That(text.ByteLength).IsEqualTo(5); // é is 2 bytes
    }

    [Test]
    public async Task FromUtf8_Empty_ReturnsEmpty()
    {
        // Act
        var text = Text.FromUtf8([]);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromChars

    [Test]
    public async Task FromChars_Span_StoresUtf16()
    {
        // Arrange & Act
        var text = Text.FromChars("Hello".AsSpan());

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // Factory — FromUtf32

    [Test]
    public async Task FromUtf32_IntSpan_StoresUtf32()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C]; // H, e, l

        // Act
        var text = Text.FromUtf32(codePoints);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(3);
        await Assert.That(text.ByteLength).IsEqualTo(12);
    }

    // Factory — FromBytes

    [Test]
    public async Task FromBytes_Utf16Bytes_StoresCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var text = Text.FromBytes(bytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // Factory — implicit from string

    [Test]
    public async Task ImplicitFromString_CreatesText()
    {
        // Act
        Text text = "Hello";

        // Assert
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // AsSpan

    [Test]
    public async Task AsSpan_StringBacked_ReturnsCorrectSpan()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var span = text.AsSpan();
        var eq = span.Equals("Hello".AsSpan());

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
        var text = Text.FromUtf8("café🎉"u8);

        // Act
        var result = text.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café🎉");
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
    public async Task Equals_NullString_EmptyText_ReturnsTrue()
    {
        // Act & Assert
        await Assert.That(Text.Empty.Equals((string?)null)).IsTrue();
    }

    [Test]
    public async Task GetHashCode_SameContentDifferentEncoding_SameHash()
    {
        // Arrange
        var utf8 = Text.FromUtf8("Hello"u8);
        var utf16 = Text.From("Hello");

        // Act
        var hash1 = utf8.GetHashCode();
        var hash2 = utf16.GetHashCode();

        // Assert
        await Assert.That(hash1).IsEqualTo(hash2);
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
    public async Task TrimStart_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.TrimStart();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("Hello  ");
        await Assert.That(trimmed.RuneLength).IsEqualTo(7);
    }

    [Test]
    public async Task TrimEnd_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.TrimEnd();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("  Hello");
        await Assert.That(trimmed.RuneLength).IsEqualTo(7);
    }

    [Test]
    public async Task Trim_ReturnsTextNotCopy()
    {
        // Arrange
        var text = Text.From("  Hello  ");

        // Act
        var trimmed = text.Trim();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("Hello");
        await Assert.That(trimmed.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task Trim_Utf8_ReturnsCorrectText()
    {
        // Arrange
        var text = Text.FromUtf8(" café "u8);

        // Act
        var trimmed = text.Trim();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("café");
        await Assert.That(trimmed.RuneLength).IsEqualTo(4);
    }

    // Slice (non-copying)

    [Test]
    public async Task RuneSlice_Offset_ReturnsTextView()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var sliced = text.RuneSlice(6);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo("World");
        await Assert.That(sliced.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task RuneSlice_OffsetAndCount_ReturnsTextView()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var sliced = text.RuneSlice(6, 5);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo("World");
        await Assert.That(sliced.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task ByteSlice_ReturnsTextView()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);

        // Act
        var sliced = text.ByteSlice(3);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo("lo");
    }

    [Test]
    public async Task ByteSlice_OffsetAndCount_ReturnsTextView()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var sliced = text.ByteSlice(6, 5);

        // Assert
        await Assert.That(sliced.ToString()).IsEqualTo("World");
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
        var text = Text.From("Hello");
        var dest = new char[10];

        // Act
        var success = text.TryFormat(dest.AsSpan(), out var written);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
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

        // Act & Assert
        await Assert.That(utf8.Equals(utf32)).IsTrue();
        await Assert.That(utf8.GetHashCode()).IsEqualTo(utf32.GetHashCode());
    }

    // ToString on trimmed string-backed Text

    [Test]
    public async Task ToString_TrimmedStringBacked_AllocatesNewString()
    {
        // Arrange
        const string original = "  Hello  ";
        var text = Text.From(original);
        var trimmed = text.Trim();

        // Act
        var result = trimmed.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
        await Assert.That(ReferenceEquals(result, original)).IsFalse();
    }
}
