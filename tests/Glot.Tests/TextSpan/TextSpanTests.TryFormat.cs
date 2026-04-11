namespace Glot.Tests;

public partial class TextSpanTests
{
    // TryFormat char — UTF-16 fast path (exercises the new optimization)

    [Test]
    public async Task TryFormat_Char_Utf16Span_DirectCopy()
    {
        // Arrange
        var text = Text.From("Hello");
        var span = text.AsSpan();
        var dest = new char[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryFormat_Char_Utf16Span_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello");
        var span = text.AsSpan();
        var dest = new char[2];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat char — UTF-8 span (exercises ToString fallback)

    [Test]
    public async Task TryFormat_Char_Utf8Span_TranscodesViaToString()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var span = text.AsSpan();
        var dest = new char[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Char_Utf8Span_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);
        var span = text.AsSpan();
        var dest = new char[3];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat byte — UTF-8 fast path

    [Test]
    public async Task TryFormat_Byte_Utf8Span_DirectCopy()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var span = text.AsSpan();
        var dest = new byte[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Byte_Utf8Span_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var span = text.AsSpan();
        var dest = new byte[2];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat byte — non-UTF-8 (requires transcoding)

    [Test]
    public async Task TryFormat_Byte_Utf16Span_Transcodes()
    {
        // Arrange
        var text = Text.From("Hello");
        var span = text.AsSpan();
        var dest = new byte[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    [Test]
    public async Task TryFormat_Byte_Utf16Span_TooSmall_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello World");
        var span = text.AsSpan();
        var dest = new byte[3];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // Empty span

    [Test]
    public async Task TryFormat_Char_EmptySpan_ReturnsTrue()
    {
        // Arrange
        var span = default(TextSpan);
        var dest = new char[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(0);
    }

    // ToString with format provider

    [Test]
    public async Task ToString_WithFormatProvider_SameAsToString()
    {
        // Arrange
        var text = Text.From("Hello");
        var span = text.AsSpan();

        // Act
        var result = span.ToString(null, null);

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
    }

    // Equals(object?) — always false for ref struct

    [Test]
    public async Task Equals_Object_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        #pragma warning disable CS8604
        var result = span.Equals((object?)null);
        #pragma warning restore CS8604

        // Assert
        await Assert.That(result).IsFalse();
    }

    // EncodeToUtf16 — identity copy when already UTF-16

    [Test]
    public async Task EncodeToUtf16_Utf16Span_IdentityCopy()
    {
        // Arrange
        var text = Text.From("Hello");
        var span = text.AsSpan();
        var dest = new char[10];

        // Act
        var written = span.EncodeToUtf16(dest);

        // Assert
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
    }

    // EncodeToUtf8 — identity copy when already UTF-8

    [Test]
    public async Task EncodeToUtf8_Utf8Span_IdentityCopy()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var dest = new byte[10];

        // Act
        var written = span.EncodeToUtf8(dest);

        // Assert
        await Assert.That(written).IsEqualTo(5);
    }
}
