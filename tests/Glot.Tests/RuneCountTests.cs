namespace Glot.Tests;

public class RuneCountTests
{
    // UTF-8

    [Test]
    public async Task Count_Utf8_AsciiOnly_ReturnsCharCount()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hello"u8;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    [Test]
    public async Task Count_Utf8_Empty_ReturnsZero()
    {
        // Act
        var result = RuneCount.Count([], TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task Count_Utf8_TwoByteRunes_CountsCorrectly()
    {
        // Arrange — "café" = c(1) a(1) f(1) é(2) = 4 runes, 5 bytes
        ReadOnlySpan<byte> bytes = "café"u8;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(4);
    }

    [Test]
    public async Task Count_Utf8_ThreeByteRunes_CountsCorrectly()
    {
        // Arrange — "日本語" = 3 runes, 9 bytes (3 bytes each)
        ReadOnlySpan<byte> bytes = "日本語"u8;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task Count_Utf8_FourByteEmoji_CountsCorrectly()
    {
        // Arrange — "🎉🎊" = 2 runes, 8 bytes (4 bytes each)
        ReadOnlySpan<byte> bytes = "🎉🎊"u8;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task Count_Utf8_MixedByteWidths_CountsCorrectly()
    {
        // Arrange — "A é 日 🎉" = A(1) space(1) é(2) space(1) 日(3) space(1) 🎉(4) = 7 runes
        ReadOnlySpan<byte> bytes = "A é 日 🎉"u8;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(7);
    }

    [Test]
    public async Task Count_Utf8_LargeInput_ExercisesSimd()
    {
        // Arrange — 2000 ASCII + 100 two-byte runes = 2100 runes
        var text = new string('A', 2000) + new string('é', 100);
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf8);
        const int expected = 2100;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // UTF-16

    [Test]
    public async Task Count_Utf16_BmpOnly_ReturnsCharCount()
    {
        // Arrange — "Hello" = 5 runes, 10 bytes
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    [Test]
    public async Task Count_Utf16_Empty_ReturnsZero()
    {
        // Act
        var result = RuneCount.Count([], TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task Count_Utf16_SurrogatePairs_CountsAsOneRune()
    {
        // Arrange — "A🎉B" = 3 runes (🎉 is a surrogate pair = 4 bytes, but 1 rune)
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task Count_Utf16_LargeInput_ExercisesSimd()
    {
        // Arrange — 2000 BMP chars + 50 surrogate pairs = 2050 runes
        var text = new string('A', 2000) + string.Concat(Enumerable.Repeat("🎉", 50));
        var bytes = TestHelpers.Encode(text, TextEncoding.Utf16);
        const int expected = 2050;

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // UTF-32

    [Test]
    public async Task Count_Utf32_AlwaysByteLengthDividedByFour()
    {
        // Arrange — "Hello" = 5 runes, 20 bytes
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf32);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    [Test]
    public async Task Count_Utf32_Empty_ReturnsZero()
    {
        // Act
        var result = RuneCount.Count([], TextEncoding.Utf32);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task Count_Utf32_Emoji_CountsCorrectly()
    {
        // Arrange — "🎉🎊" = 2 runes, 8 bytes
        var bytes = TestHelpers.Encode("🎉🎊", TextEncoding.Utf32);

        // Act
        var result = RuneCount.Count(bytes, TextEncoding.Utf32);

        // Assert
        await Assert.That(result).IsEqualTo(2);
    }

    // Invalid encoding

    [Test]
    public async Task Count_InvalidEncoding_ThrowsInvalidEncodingException()
    {
        // Arrange
        ReadOnlySpan<byte> bytes = "Hi"u8;
        const TextEncoding invalid = (TextEncoding)99;
        Exception? caught = null;

        // Act
        try
        {
            _ = RuneCount.Count(bytes, invalid);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<InvalidEncodingException>();
    }
}
