namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public Task EnumerateRunes_Utf8Ascii_YieldsAllRunes()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var values = new List<int>();
        foreach (var rune in span.EnumerateRunes())
        {
            values.Add(rune.Value);
        }

        // Assert
        return Verify(values);
    }

    [Test]
    public Task EnumerateRunes_Utf8MultiByte_DecodesCorrectly()
    {
        // Arrange — "café" = c, a, f, é
        var span = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var values = new List<int>();
        foreach (var rune in span.EnumerateRunes())
        {
            values.Add(rune.Value);
        }

        // Assert
        return Verify(values);
    }

    [Test]
    public Task EnumerateRunes_Utf8Emoji_DecodesAsSingleRune()
    {
        // Arrange — "A🎉B" = A, 🎉, B
        var span = new TextSpan("A🎉B"u8, TextEncoding.Utf8);

        // Act
        var values = new List<int>();
        foreach (var rune in span.EnumerateRunes())
        {
            values.Add(rune.Value);
        }

        // Assert
        return Verify(values);
    }

    [Test]
    public async Task EnumerateRunes_Empty_YieldsNothing()
    {
        // Arrange
        var span = new TextSpan([], TextEncoding.Utf8);

        // Act
        var count = 0;
        foreach (var _ in span.EnumerateRunes())
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public Task EnumerateRunes_Utf16_DecodesCorrectly()
    {
        // Arrange — "A🎉B" in UTF-16
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var values = new List<int>();
        foreach (var rune in span.EnumerateRunes())
        {
            values.Add(rune.Value);
        }

        // Assert
        return Verify(values);
    }

    [Test]
    public Task EnumerateRunes_Utf32_DecodesCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var values = new List<int>();
        foreach (var rune in span.EnumerateRunes())
        {
            values.Add(rune.Value);
        }

        // Assert
        return Verify(values);
    }

    [Test]
    public async Task EnumerateRunes_CountMatchesRuneLength()
    {
        // Arrange — "日本語café🎉" = 8 runes
        var span = new TextSpan("日本語café🎉"u8, TextEncoding.Utf8);
        var expectedLength = span.RuneLength;

        // Act
        var count = 0;
        foreach (var _ in span.EnumerateRunes())
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(expectedLength);
    }
}
