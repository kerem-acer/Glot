namespace Glot.Tests;

public partial class TextTests
{
    // TryFormat char — destination too small

    [Test]
    public async Task TryFormat_Char_DestinationTooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello World");
        var dest = new char[3];

        // Act
        var success = ((ISpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat char — non-string-backed (UTF-8 text)

    [Test]
    public async Task TryFormat_Char_Utf8Backed_Success()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new char[10];

        // Act
        var success = ((ISpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryFormat_Char_Utf8Backed_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new char[2];

        // Act
        var success = ((ISpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat byte — UTF-8 backed, too small

    [Test]
    public async Task TryFormat_Utf8_Utf8Backed_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new byte[2];

        // Act
        var success = ((IUtf8SpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat byte — non-UTF-8 backed (UTF-16 text transcodes to UTF-8)

    [Test]
    public async Task TryFormat_Utf8_Utf16Backed_Success()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new byte[10];

        // Act
        var success = ((IUtf8SpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Utf8_Utf16Backed_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new byte[2];

        // Act
        var success = ((IUtf8SpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // EncodeToUtf16/EncodeToUtf8

    [Test]
    public async Task EncodeToUtf16_Utf8Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);
        var dest = new char[10];

        // Act
        var written = text.EncodeToUtf16(dest);

        // Assert
        await Assert.That(written).IsEqualTo(4);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("café");
    }

    [Test]
    public async Task EncodeToUtf8_Utf16Text_Transcodes()
    {
        // Arrange
        var text = Text.From("café");
        var dest = new byte[10];

        // Act
        var written = text.EncodeToUtf8(dest);

        // Assert
        await Assert.That(written).IsEqualTo(5); // café is 5 UTF-8 bytes
    }

    // String-backed ToString optimization

    [Test]
    public async Task ToString_SubslicedStringBacked_Allocates()
    {
        // Arrange — slice creates a non-zero _start
        var text = Text.From("Hello World").RuneSlice(6);

        // Act
        var result = text.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("World");
    }

    // IFormattable.ToString(format, provider) delegates to ToString()

    [Test]
    public async Task IFormattable_ToString_DelegatesToToString()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.ToString("ignored", null);

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
    }
}
