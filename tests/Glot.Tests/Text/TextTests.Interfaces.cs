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
    public Task TryParse_String_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("Hello", null, out var result);

        // Assert
        return Verify(new { success, result = result.ToString() });
    }

    [Test]
    public Task TryParse_NullString_ReturnsEmpty()
    {
        // Act
        var success = Text.TryParse((string?)null, null, out var result);

        // Assert
        return Verify(new { success, result.IsEmpty });
    }

    // ISpanParsable<Text>

    [Test]
    public Task Parse_CharSpan_ReturnsUtf16Text()
    {
        // Act
        var result = Text.Parse("Hello".AsSpan(), null);

        // Assert
        return Verify(new { text = result.ToString(), result.Encoding });
    }

    [Test]
    public Task TryParse_CharSpan_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("café".AsSpan(), null, out var result);

        // Assert
        return Verify(new { success, result = result.ToString() });
    }

    // IUtf8SpanParsable<Text>

    [Test]
    public Task Parse_Utf8Span_ReturnsUtf8Text()
    {
        // Act
        var result = Text.Parse("Hello"u8, null);

        // Assert
        return Verify(new { text = result.ToString(), result.Encoding });
    }

    [Test]
    public Task TryParse_Utf8Span_AlwaysSucceeds()
    {
        // Act
        var success = Text.TryParse("café"u8, null, out var result);

        // Assert
        return Verify(new { success, result = result.ToString() });
    }

    // ISpanFormattable

    [Test]
    public Task ISpanFormattable_TryFormat_WritesChars()
    {
        // Arrange
        var text = Text.From("Hello");
        var dest = new char[10];

        // Act
        var success = ((ISpanFormattable)text).TryFormat(dest, out var written, default, null);
        var result = new string(dest, 0, written);

        // Assert
        return Verify(new { success, written, result });
    }

    // IUtf8SpanFormattable

    [Test]
    public Task IUtf8SpanFormattable_TryFormat_WritesUtf8()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);
        var dest = new byte[10];

        // Act
        var success = ((IUtf8SpanFormattable)text).TryFormat(dest, out var written, default, null);

        // Assert
        return Verify(new { success, written });
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
