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

    // CountPrefix — UTF-32 short-circuit

    [Test]
    public async Task CountPrefix_Utf32_AlwaysDividesByFour()
    {
        // Arrange — 10-rune UTF-32 span, prefix of 8 bytes = 2 runes
        var bytes = TestHelpers.Encode("0123456789", TextEncoding.Utf32);
        const int bytePos = 8;
        const int expected = 2;

        // Act — totalRuneLength ignored for UTF-32
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf32, bytePos, totalRuneLength: 0);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // CountPrefix — UTF-8 fast paths

    [Test]
    public async Task CountPrefix_Utf8_AllAscii_ReturnsBytePos()
    {
        // Arrange — "Hello" = 5 bytes, 5 runes, totalRuneLength == bytes.Length hits fast path
        ReadOnlySpan<byte> bytes = "Hello"u8;
        const int bytePos = 3;
        const int expected = 3;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf8, bytePos, totalRuneLength: 5);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task CountPrefix_Utf8_MultiByte_UsesSlowPath()
    {
        // Arrange — "café" = 5 bytes, 4 runes; prefix "caf" = 3 bytes = 3 runes (é starts at byte 3)
        ReadOnlySpan<byte> bytes = "café"u8;
        const int bytePos = 3;
        const int expected = 3;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf8, bytePos, totalRuneLength: 4);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task CountPrefix_Utf8_MultiByteIncludingRune_CountsIt()
    {
        // Arrange — "café" = 5 bytes, 4 runes; prefix "café" = all 5 bytes = 4 runes
        ReadOnlySpan<byte> bytes = "café"u8;
        const int bytePos = 5;
        const int expected = 4;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf8, bytePos, totalRuneLength: 4);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task CountPrefix_Utf8_LazyRuneLength_UsesSlowPath()
    {
        // Arrange — totalRuneLength = 0 (lazy sentinel) forces the slow path even for ASCII
        ReadOnlySpan<byte> bytes = "Hello"u8;
        const int bytePos = 3;
        const int expected = 3;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf8, bytePos, totalRuneLength: 0);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // CountPrefix — UTF-16 fast path (NEW)

    [Test]
    public async Task CountPrefix_Utf16_AllBmp_ReturnsHalfBytePos()
    {
        // Arrange — "Hello" UTF-16 = 10 bytes, 5 runes; totalRuneLength * 2 == bytes.Length hits BMP fast path
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        const int bytePos = 4;
        const int expected = 2;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf16, bytePos, totalRuneLength: 5);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task CountPrefix_Utf16_WithSurrogatePair_UsesSlowPath()
    {
        // Arrange — "A🎉B" UTF-16 = 2 + 4 + 2 = 8 bytes, 3 runes. 3 * 2 != 8 so BMP fast path misses.
        // Prefix of 2 bytes = "A" = 1 rune. Prefix of 6 bytes = "A🎉" = 2 runes.
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);

        // Act
        var prefixOne = RuneCount.CountPrefix(bytes, TextEncoding.Utf16, bytePos: 2, totalRuneLength: 3);
        var prefixTwo = RuneCount.CountPrefix(bytes, TextEncoding.Utf16, bytePos: 6, totalRuneLength: 3);

        // Assert
        await Assert.That(prefixOne).IsEqualTo(1);
        await Assert.That(prefixTwo).IsEqualTo(2);
    }

    [Test]
    public async Task CountPrefix_Utf16_LazyRuneLength_UsesSlowPath()
    {
        // Arrange — totalRuneLength = 0 (lazy sentinel) forces the slow path even for all-BMP input
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        const int bytePos = 4;
        const int expected = 2;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf16, bytePos, totalRuneLength: 0);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // CountPrefix — slow-path suffix optimization

    [Test]
    public async Task CountPrefix_Utf8_PrefixPastHalf_CountsFromTail()
    {
        // Arrange — "日本語" = 9 bytes (3 bytes each), 3 runes; prefix of 6 bytes is past-half, triggers tail-count path
        ReadOnlySpan<byte> bytes = "日本語"u8;
        const int bytePos = 6;
        const int expected = 2;

        // Act
        var result = RuneCount.CountPrefix(bytes, TextEncoding.Utf8, bytePos, totalRuneLength: 3);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}
