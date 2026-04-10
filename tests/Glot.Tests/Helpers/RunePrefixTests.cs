namespace Glot.Tests;

public class RunePrefixTests
{
    [Test]
    public async Task TryMatch_SameEncoding_Match_ReturnsTrueWithByteLength()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello World"u8;
        ReadOnlySpan<byte> prefix = "Hello"u8;
        const int expectedLen = 5;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_SameEncoding_NoMatch_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello"u8;
        ReadOnlySpan<byte> prefix = "World"u8;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsFalse();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_EmptyPrefix_ReturnsTrueWithZeroBytes()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello"u8;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            [], TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_PrefixLongerThanSource_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hi"u8;
        ReadOnlySpan<byte> prefix = "Hello"u8;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsFalse();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_CrossEncoding_Utf8SourceUtf16Prefix_ReturnsTrueWithSourceBytes()
    {
        // Arrange
        ReadOnlySpan<byte> source = "café"u8;
        var prefixBytes = TestHelpers.Encode("caf", TextEncoding.Utf16);
        const int expectedLen = 3; // "caf" in UTF-8 is 3 bytes

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefixBytes, TextEncoding.Utf16,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_CrossEncoding_Utf16SourceUtf8Prefix_ReturnsTrueWithSourceBytes()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("café", TextEncoding.Utf16);
        ReadOnlySpan<byte> prefix = "caf"u8;
        const int expectedLen = 6; // "caf" in UTF-16 is 3 chars * 2 bytes

        // Act
        var matched = RunePrefix.TryMatch(
            sourceBytes, TextEncoding.Utf16,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_CrossEncoding_NoMatch_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello"u8;
        var prefixBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefixBytes, TextEncoding.Utf16,
            out var consumed);

        // Assert
        await Assert.That(matched).IsFalse();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_MultiByteRunes_ReportsCorrectSourceBytes()
    {
        // Arrange
        ReadOnlySpan<byte> source = "🎉Hello"u8;
        ReadOnlySpan<byte> prefix = "🎉"u8;
        const int expectedLen = 4; // 🎉 is 4 bytes in UTF-8

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_ExactMatch_ReturnsTrueWithFullLength()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello"u8;
        ReadOnlySpan<byte> prefix = "Hello"u8;
        const int expectedLen = 5;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_EmptySource_NonEmptyPrefix_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<byte> prefix = "Hi"u8;

        // Act
        var matched = RunePrefix.TryMatch(
            [], TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsFalse();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_BothEmpty_ReturnsTrue()
    {
        // Act
        var matched = RunePrefix.TryMatch(
            [], TextEncoding.Utf8,
            [], TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(0);
    }

    [Test]
    public async Task TryMatch_Utf32Source_Utf8Prefix_Match()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        ReadOnlySpan<byte> prefix = "Hel"u8;
        const int expectedLen = 12; // 3 runes * 4 bytes in UTF-32

        // Act
        var matched = RunePrefix.TryMatch(
            sourceBytes, TextEncoding.Utf32,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsTrue();
        await Assert.That(consumed).IsEqualTo(expectedLen);
    }

    [Test]
    public async Task TryMatch_PartialRuneMismatch_ReturnsFalseAtDivergence()
    {
        // Arrange — "café" vs "cafo" diverge at rune 3
        ReadOnlySpan<byte> source = "café"u8;
        ReadOnlySpan<byte> prefix = "cafo"u8;

        // Act
        var matched = RunePrefix.TryMatch(
            source, TextEncoding.Utf8,
            prefix, TextEncoding.Utf8,
            out var consumed);

        // Assert
        await Assert.That(matched).IsFalse();
        await Assert.That(consumed).IsEqualTo(0);
    }
}
