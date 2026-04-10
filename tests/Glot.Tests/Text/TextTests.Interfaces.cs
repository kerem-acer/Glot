namespace Glot.Tests;

public partial class TextTests
{
    // IParsable<Text>

    [Test]
    public async Task Parse_String_ReturnsText()
    {
        // Act
        var result = Text.Parse("Hello", null);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryParse_String_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("Hello", null, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryParse_NullString_ReturnsEmpty()
    {
        // Act
        var success = Text.TryParse((string?)null, null, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.IsEmpty).IsTrue();
    }

    // ISpanParsable<Text>

    [Test]
    public async Task Parse_CharSpan_ReturnsUtf16Text()
    {
        // Act
        var result = Text.Parse("Hello".AsSpan(), null);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task TryParse_CharSpan_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("café".AsSpan(), null, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.ToString()).IsEqualTo("café");
    }

    // IUtf8SpanParsable<Text>

    [Test]
    public async Task Parse_Utf8Span_ReturnsUtf8Text()
    {
        // Act
        var result = Text.Parse("Hello"u8, null);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task TryParse_Utf8Span_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("café"u8, null, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.ToString()).IsEqualTo("café");
    }

    // ISpanFormattable

    [Test]
    public async Task ISpanFormattable_TryFormat_WritesChars()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new char[10];

        // Act
        var success = ((ISpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
    }

    // IUtf8SpanFormattable

    [Test]
    public async Task IUtf8SpanFormattable_TryFormat_WritesUtf8()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new byte[10];

        // Act
        var success = ((IUtf8SpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
    }

    // Generic parsing — works with generic constraints

    [Test]
    public async Task GenericParse_WorksWithConstraint()
    {
        // Act
        var result = ParseHelper<Text>("Hello World");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    static T ParseHelper<T>(string value) where T : IParsable<T>
        => T.Parse(value, null);
}
