using System.Runtime.InteropServices;

namespace Glot.Tests;

public partial class TextSpanTests
{
    // ByteLength

    [Test]
    public async Task ByteLength_Utf8Ascii_EqualsByteCount()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var length = span.ByteLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public Task ByteLength_Utf8MultiByte_GreaterThanRuneLength()
    {
        // Arrange — "café" = 5 bytes, 4 runes
        var span = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var byteLen = span.ByteLength;
        var runeLen = span.RuneLength;

        // Assert
        return Verify(new { byteLen, runeLen });
    }

    [Test]
    public async Task ByteLength_Utf16_TwoBytesPerBmpChar()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var length = span.ByteLength;

        // Assert
        await Assert.That(length).IsEqualTo(10);
    }

    [Test]
    public async Task ByteLength_Empty_ReturnsZero()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf8);

        // Act
        var length = span.ByteLength;

        // Assert
        await Assert.That(length).IsEqualTo(0);
    }

    // ByteSlice

    [Test]
    public async Task ByteSlice_Offset_ReturnsRemainder()
    {
        // Arrange — "Hello" in UTF-8
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.ByteSlice(3);
        var eq = sliced.Equals("lo".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task ByteSlice_OffsetAndCount_ReturnsSubstring()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.ByteSlice(6, 5);
        var eq = sliced.Equals("World".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public Task ByteSlice_Utf8MultiByte_SlicesAtByteOffset()
    {
        // Arrange — "café" = [63 61 66 C3 A9], byte 3 is start of "é"
        var span = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.ByteSlice(3);
        var eq = sliced.Equals("é".AsSpan());
        var runeLen = sliced.RuneLength;

        // Assert
        return Verify(new { eq, runeLen });
    }

    [Test]
    public async Task ByteSlice_ZeroOffset_ReturnsFullSpan()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.ByteSlice(0);
        var eq = sliced.Equals("Hello".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task ByteSlice_AtEnd_ReturnsEmpty()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.ByteSlice(2);

        // Assert
        await Assert.That(sliced.IsEmpty).IsTrue();
    }

    // ByteIndexOf

    [Test]
    public async Task ByteIndexOf_TextSpan_ReturnsBytePosition()
    {
        // Arrange — "café" = [63 61 66 C3 A9], "é" starts at byte 3
        var span = new TextSpan("café"u8, TextEncoding.Utf8);
        var value = new TextSpan("é"u8, TextEncoding.Utf8);

        // Act
        var pos = span.ByteIndexOf(value);

        // Assert
        await Assert.That(pos).IsEqualTo(3);
    }

    [Test]
    public async Task ByteIndexOf_NotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var pos = span.ByteIndexOf(value);

        // Assert
        await Assert.That(pos).IsEqualTo(-1);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanByte_DefaultUtf8()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var pos = span.ByteIndexOf("World"u8);

        // Assert
        await Assert.That(pos).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanByte_ExplicitEncoding()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var pos = span.ByteIndexOf(searchBytes, TextEncoding.Utf16);

        // Assert — "Hello " = 6 chars * 2 bytes = 12
        await Assert.That(pos).IsEqualTo(12);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanChar_Utf16()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var pos = span.ByteIndexOf("World".AsSpan());

        // Assert
        await Assert.That(pos).IsEqualTo(12);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanInt_Utf32()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act
        var pos = span.ByteIndexOf(searchInts);

        // Assert — "Hello " = 6 runes * 4 bytes = 24
        await Assert.That(pos).IsEqualTo(24);
    }

    // LastByteIndexOf

    [Test]
    public async Task LastByteIndexOf_TextSpan_ReturnsLastBytePosition()
    {
        // Arrange
        var span = new TextSpan("abcabc"u8, TextEncoding.Utf8);
        var value = new TextSpan("abc"u8, TextEncoding.Utf8);

        // Act
        var pos = span.LastByteIndexOf(value);

        // Assert
        await Assert.That(pos).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanByte_DefaultUtf8()
    {
        // Arrange
        var span = new TextSpan("abcabc"u8, TextEncoding.Utf8);

        // Act
        var pos = span.LastByteIndexOf("abc"u8);

        // Assert
        await Assert.That(pos).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanChar_Utf16()
    {
        // Arrange
        var bytes = TestHelpers.Encode("abcabc", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var pos = span.LastByteIndexOf("abc".AsSpan());

        // Assert — second "abc" starts at char 3 * 2 bytes = 6
        await Assert.That(pos).IsEqualTo(6);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanInt_Utf32()
    {
        // Arrange
        var bytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);
        var searchBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act
        var pos = span.LastByteIndexOf(searchInts);

        // Assert — second "abc" starts at rune 3 * 4 bytes = 12
        await Assert.That(pos).IsEqualTo(12);
    }

    [Test]
    public async Task LastByteIndexOf_NotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var pos = span.LastByteIndexOf("xyz"u8);

        // Assert
        await Assert.That(pos).IsEqualTo(-1);
    }
}
