using System.Runtime.InteropServices;

namespace Glot.Tests;

public partial class TextSpanTests
{
    // ToString

    [Test]
    public async Task ToString_Utf8_ReturnsString()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
    }

    [Test]
    public async Task ToString_Utf8MultiByte_ReturnsCorrectString()
    {
        // Arrange
        var span = new TextSpan("café🎉"u8, TextEncoding.Utf8);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café🎉");
    }

    [Test]
    public async Task ToString_Utf16_ReturnsString()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task ToString_Utf32_ReturnsString()
    {
        // Arrange
        var bytes = TestHelpers.Encode("café🎉", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café🎉");
    }

    [Test]
    public async Task ToString_Empty_ReturnsEmptyString()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf8);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    // AsChars

    [Test]
    public async Task AsChars_Utf16_ReturnsCastSpan()
    {
        // Arrange
        const string text = "Hello";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var chars = span.Chars;
        var result = chars.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(text);
    }

    [Test]
    public async Task AsChars_Utf16WithSurrogates_ReturnsCastSpan()
    {
        // Arrange
        const string text = "A🎉B";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var chars = span.Chars;
        var result = chars.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(text);
    }

    [Test]
    public async Task AsChars_Utf8Bytes_ReinterpretsCast()
    {
        // Arrange — "AB" in UTF-8 = [0x41, 0x42], reinterpreted as one UTF-16 char
        var span = new TextSpan("AB"u8, TextEncoding.Utf8);

        // Act
        var chars = span.Chars;
        var length = chars.Length;

        // Assert — 2 bytes / 2 = 1 char
        await Assert.That(length).IsEqualTo(1);
    }

    // AsInts

    [Test]
    public Task AsInts_Utf32_ReturnsCastSpan()
    {
        // Arrange
        var bytes = TestHelpers.Encode("ABC", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var ints = span.Ints;
        var values = ints.ToArray();

        // Assert
        return Verify(values);
    }

    [Test]
    public Task AsInts_Utf32Emoji_ReturnsCodePoints()
    {
        // Arrange
        var bytes = TestHelpers.Encode("A🎉", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var ints = span.Ints;
        var values = ints.ToArray();

        // Assert
        return Verify(values);
    }

    [Test]
    public async Task AsInts_Utf8Bytes_ReinterpretsCast()
    {
        // Arrange — 4 UTF-8 bytes reinterpreted as 1 int
        var span = new TextSpan("ABCD"u8, TextEncoding.Utf8);

        // Act
        var ints = span.Ints;
        var length = ints.Length;

        // Assert — 4 bytes / 4 = 1 int
        await Assert.That(length).IsEqualTo(1);
    }

    // Round-trip: construct from typed span, convert back

    [Test]
    public async Task RoundTrip_Utf8_ToStringAndBack()
    {
        // Arrange
        var span = new TextSpan("日本語café🎉"u8, TextEncoding.Utf8);

        // Act
        var str = span.ToString();
        var roundTripped = new TextSpan(
            TestHelpers.Encode(str, TextEncoding.Utf8), TextEncoding.Utf8);
        var eq = roundTripped.Equals(span);

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task RoundTrip_Utf16_AsCharsPreservesContent()
    {
        // Arrange
        const string text = "日本語café🎉";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var chars = span.Chars;
        var backToSpan = new TextSpan(
            MemoryMarshal.AsBytes(chars).ToArray(), TextEncoding.Utf16);
        var eq = backToSpan.Equals(span);

        // Assert
        await Assert.That(eq).IsTrue();
    }
}
