using System.Text;

namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public async Task DecodeFirstRune_AsciiUtf8_Succeeds()
    {
        // Arrange
        var span = new TextSpan("ABC"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('A');
        await Assert.That(consumed).IsEqualTo(1);
    }

    [Test]
    public async Task DecodeFirstRune_TwoByteUtf8_Succeeds()
    {
        // Arrange
        var span = new TextSpan("é"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('é');
        await Assert.That(consumed).IsEqualTo(2);
    }

    [Test]
    public async Task DecodeFirstRune_ThreeByteUtf8_Succeeds()
    {
        // Arrange
        var span = new TextSpan("日"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('日');
        await Assert.That(consumed).IsEqualTo(3);
    }

    [Test]
    public async Task DecodeFirstRune_FourByteUtf8Emoji_Succeeds()
    {
        // Arrange
        var span = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeFirstRune_AsciiUtf16_Succeeds()
    {
        // Arrange
        var bytes = TestHelpers.Encode("A", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('A');
        await Assert.That(consumed).IsEqualTo(2);
    }

    [Test]
    public async Task DecodeFirstRune_SurrogatePairUtf16_Succeeds()
    {
        // Arrange
        var bytes = TestHelpers.Encode("🎉", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeFirstRune_Utf32_Succeeds()
    {
        // Arrange
        var bytes = TestHelpers.Encode("🎉", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeFirstRune_EmptyUtf8_ReturnsReplacement()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeFirstRune_EmptyUtf16_ReturnsReplacement()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf16);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeFirstRune_TooShortUtf32_ReturnsReplacement()
    {
        // Arrange
        byte[] twoBytes = [0x41, 0x00];
        var span = new TextSpan(twoBytes, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsEqualTo(2);
    }

    [Test]
    public async Task DecodeFirstRune_InvalidUtf8_ReturnsReplacement()
    {
        // Arrange
        byte[] invalid = [0xFF, 0x41];
        var span = new TextSpan(invalid, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsGreaterThanOrEqualTo(1);
    }

    [Test]
    public async Task DecodeFirstRune_InvalidUtf32_ReturnsReplacement()
    {
        // Arrange — 0x110000 is above max Unicode code point
        byte[] invalid = BitConverter.GetBytes(0x110000);
        var span = new TextSpan(invalid, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeLastRune_AsciiUtf8_ReturnsLastChar()
    {
        // Arrange
        var span = new TextSpan("ABC"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('C');
        await Assert.That(consumed).IsEqualTo(1);
    }

    [Test]
    public async Task DecodeLastRune_MultiByteUtf8_ReturnsLastRune()
    {
        // Arrange
        var span = new TextSpan("Aé"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('é');
        await Assert.That(consumed).IsEqualTo(2);
    }

    [Test]
    public async Task DecodeLastRune_EmojiUtf8_ReturnsEmoji()
    {
        // Arrange
        var span = new TextSpan("A🎉"u8, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeLastRune_AsciiUtf16_ReturnsLastChar()
    {
        // Arrange
        var bytes = TestHelpers.Encode("AB", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('B');
        await Assert.That(consumed).IsEqualTo(2);
    }

    [Test]
    public async Task DecodeLastRune_SurrogatePairUtf16_ReturnsEmoji()
    {
        // Arrange
        var bytes = TestHelpers.Encode("A🎉", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeLastRune_Utf32_ReturnsEmoji()
    {
        // Arrange
        var bytes = TestHelpers.Encode("A🎉", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo(0x1F389);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeLastRune_EmptyUtf16_ReturnsReplacement()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeLastRune_TooShortUtf32_ReturnsReplacement()
    {
        // Arrange
        byte[] twoBytes = [0x41, 0x00];
        var span = new TextSpan(twoBytes, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeLastRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeFirstRune_LoneHighSurrogateUtf16_ReturnsReplacement()
    {
        // Arrange — 0xD800 is a lone high surrogate (invalid without a low surrogate)
        byte[] invalid = [0x00, 0xD8];
        var span = new TextSpan(invalid, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsGreaterThanOrEqualTo(2);
    }

    [Test]
    public async Task DecodeFirstRune_OddByteUtf16_ReturnsReplacement()
    {
        // Arrange — single byte can't form a UTF-16 char
        byte[] oddByte = [0x41];
        var span = new TextSpan(oddByte, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeFirstRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeLastRune_InvalidUtf8AtEnd_ReturnsReplacement()
    {
        // Arrange — 0xFE is never valid in UTF-8, treated as a lead byte
        byte[] invalid = [0x41, 0xFE];
        var span = new TextSpan(invalid, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeLastRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeLastRune_TruncatedMultiByteUtf8_ReturnsReplacement()
    {
        // Arrange — 0xC3 is start of a 2-byte sequence, but no continuation byte follows
        byte[] truncated = [0x41, 0xC3];
        var span = new TextSpan(truncated, TextEncoding.Utf8);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsEqualTo(1);
    }

    [Test]
    public async Task DecodeLastRune_LoneLowSurrogateUtf16_ReturnsReplacement()
    {
        // Arrange — 'A' followed by a lone low surrogate (0xDC00)
        byte[] data = [0x41, 0x00, 0x00, 0xDC];
        var span = new TextSpan(data, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeLastRune_LoneHighSurrogateUtf16_ReturnsReplacement()
    {
        // Arrange — 'A' followed by a lone high surrogate (0xD800)
        byte[] data = [0x41, 0x00, 0x00, 0xD8];
        var span = new TextSpan(data, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out _);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
    }

    [Test]
    public async Task DecodeLastRune_InvalidUtf32AtEnd_ReturnsReplacement()
    {
        // Arrange — valid 'A' followed by invalid code point 0x110000
        var validA = BitConverter.GetBytes((int)'A');
        var invalid = BitConverter.GetBytes(0x110000);
        byte[] data = [.. validA, .. invalid];
        var span = new TextSpan(data, TextEncoding.Utf32);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsFalse();
        await Assert.That(rune).IsEqualTo(Rune.ReplacementChar);
        await Assert.That(consumed).IsEqualTo(4);
    }

    [Test]
    public async Task DecodeLastRune_SingleCharUtf16_ReturnsChar()
    {
        // Arrange — single BMP char, exercises i==0 path (no surrogate check)
        var bytes = TestHelpers.Encode("A", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var ok = span.DecodeLastRune(out var rune, out var consumed);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(rune.Value).IsEqualTo('A');
        await Assert.That(consumed).IsEqualTo(2);
    }
}
