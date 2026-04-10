namespace Glot.Tests;

public class RuneIndexTests
{
    // UTF-8

    [Test]
    public async Task ToByteOffset_Utf8_ZeroOffset_ReturnsZero()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hello"u8;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, 0);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task ToByteOffset_Utf8_AsciiOnly_OffsetEqualsBytes()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hello"u8;
        const int runeOffset = 3;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task ToByteOffset_Utf8_MultiByteRunes_AccountsForExtraBytes()
    {
        // Arrange — "café" = c(1) a(1) f(1) é(2) = 5 bytes, 4 runes
        ReadOnlySpan<byte> bytes = "café"u8;
        const int runeOffset = 4; // past the last rune

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    [Test]
    public async Task ToByteOffset_Utf8_OffsetToMultiByteRune_ReturnsCorrectByte()
    {
        // Arrange — "café" = c(1) a(1) f(1) é(2), rune 3 starts at byte 3
        ReadOnlySpan<byte> bytes = "café"u8;
        const int runeOffset = 3;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task ToByteOffset_Utf8_FourByteEmoji_SkipsCorrectly()
    {
        // Arrange — "🎉AB" = 🎉(4) A(1) B(1) = 6 bytes, 3 runes
        ReadOnlySpan<byte> bytes = "🎉AB"u8;
        const int runeOffset = 1; // past the emoji

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(4);
    }

    [Test]
    public async Task ToByteOffset_Utf8_OffsetAtEnd_ReturnsLength()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hi"u8;
        const int runeOffset = 2;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task ToByteOffset_Utf8_OutOfRange_ThrowsArgumentOutOfRange()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hi"u8;
        Exception? caught = null;

        // Act
        try
        {
            _ = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, 10);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToByteOffset_Utf8_LargeInput_ExercisesSimd()
    {
        // Arrange — 2000 ASCII 'A' + "🎉" (4 bytes), rune 2000 → byte 2000
        var text = new string('A', 2000) + "🎉";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf8);
        const int runeOffset = 2000;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(2000);
    }

    [Test]
    public async Task ToByteOffset_Utf8_LargeMultiByte_SimdWithMixedRunes()
    {
        // Arrange — 500 × "é" (2 bytes each) + "X", rune 500 → byte 1000
        var text = new string('é', 500) + "X";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf8);
        const int runeOffset = 500;
        const int expectedByte = 1000;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf8, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(expectedByte);
    }

    // UTF-16

    [Test]
    public async Task ToByteOffset_Utf16_BmpOnly_OffsetTimesTwo()
    {
        // Arrange — "Hello" in UTF-16, each char = 2 bytes
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        const int runeOffset = 3;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ToByteOffset_Utf16_SurrogatePair_AccountsForExtraChar()
    {
        // Arrange — "A🎉B" = A(2) 🎉(4) B(2) = 8 bytes, 3 runes
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);
        const int runeOffset = 2; // past A and 🎉

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(6); // A(2) + 🎉(4) = 6
    }

    [Test]
    public async Task ToByteOffset_Utf16_ZeroOffset_ReturnsZero()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, 0);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task ToByteOffset_Utf16_OffsetAtEnd_ReturnsLength()
    {
        // Arrange — "Hi" = 4 bytes, 2 runes
        var bytes = TestHelpers.Encode("Hi", TextEncoding.Utf16);
        const int runeOffset = 2;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(4);
    }

    [Test]
    public async Task ToByteOffset_Utf16_OutOfRange_ThrowsArgumentOutOfRange()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hi", TextEncoding.Utf16);
        Exception? caught = null;

        // Act
        try
        {
            _ = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, 10);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToByteOffset_Utf16_LargeInput_ExercisesSimd()
    {
        // Arrange — 2000 A's + 🎉, rune 2001 → past emoji
        var text = new string('A', 2000) + "🎉";
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf16);
        const int runeOffset = 2001;
        const int expectedByte = 2000 * 2 + 4; // 2000 BMP chars + 1 surrogate pair

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf16, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(expectedByte);
    }

    // UTF-32

    [Test]
    public async Task ToByteOffset_Utf32_AlwaysRuneTimesFour()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        const int runeOffset = 3;

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf32, runeOffset);

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task ToByteOffset_Utf32_ZeroOffset_ReturnsZero()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);

        // Act
        var result = RuneIndex.ToByteOffset(bytes, TextEncoding.Utf32, 0);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // Invalid encoding

    [Test]
    public async Task ToByteOffset_InvalidEncoding_ThrowsInvalidEncodingException()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hi"u8;
        const TextEncoding invalid = (TextEncoding)99;
        Exception? caught = null;

        // Act
        try
        {
            _ = RuneIndex.ToByteOffset(bytes, invalid, 1);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<InvalidEncodingException>();
    }
}
