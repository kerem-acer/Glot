namespace Glot.Tests;

public partial class TextInterpolationTests
{
    [Test]
    public Task Format_LiteralOnly_ReturnsText()
    {
        // Act
        var result = Text.Create($"Hello");

        // Assert
        return Verify(new { result = result.ToString(), result.Encoding });
    }

    [Test]
    public async Task Format_StringHole_Interpolates()
    {
        // Arrange
        const string expected = "Hello World";

        // Act
        var result = Text.Create($"Hello {"World"}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_IntHole_Interpolates()
    {
        // Arrange
        const string expected = "Count: 42";

        // Act
        var result = Text.Create($"Count: {42}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_FormatSpecifier_Formats()
    {
        // Arrange
        var expected = $"Price: {1.5:F2}"; // use runtime's formatting as reference

        // Act
        var result = Text.Create($"Price: {1.5:F2}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_RightAlignment_PadsLeft()
    {
        // Arrange
        const string expected = "        hi";

        // Act
        var result = Text.Create($"{"hi",10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_LeftAlignment_PadsRight()
    {
        // Arrange
        const string expected = "hi        ";

        // Act
        var result = Text.Create($"{"hi",-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_TextHole_NoToString()
    {
        // Arrange
        const string expected = "Hello World!";
        var prefix = Text.FromUtf8("Hello"u8);

        // Act
        var result = Text.Create($"{prefix} World!");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_MultipleHoles_Interpolates()
    {
        // Arrange
        const string expected = "1 + 2 = 3";

        // Act
        var result = Text.Create($"{1} + {2} = {3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public Task Format_ExplicitEncoding_UsesEncoding()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf16, $"Hello {"World"}");

        // Assert
        return Verify(new { result = result.ToString(), result.Encoding });
    }

    [Test]
    public async Task FormatPooled_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "Hello World";

        // Act
        using var result = Text.CreatePooled($"Hello {"World"}");

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_NullStringHole_AppendsEmpty()
    {
        // Arrange
        const string expected = "Value: ";
        string? value = null;

        // Act
        var result = Text.Create($"Value: {value}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_EmptyInterpolation_ReturnsEmpty()
    {
        // Act
        var result = Text.Create($"");

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Format_CharSpanHole_Appends()
    {
        // Arrange
        const string expected = "Hello World";
        ReadOnlySpan<char> name = "World";

        // Act
        var result = Text.Create($"Hello {name}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_BoolHole_Interpolates()
    {
        // Arrange
        const string expected = "Active: True";

        // Act
        var result = Text.Create($"Active: {true}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_AlignmentWithFormat_Works()
    {
        // Arrange
        const string expected = "     00042";

        // Act
        var result = Text.Create($"{42,10:D5}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public Task FormatPooled_ExplicitEncoding_Works()
    {
        // Act
        using var result = Text.CreatePooled(TextEncoding.Utf32, $"Hi {42}");

        // Assert
        return Verify(new { result = result.Text.ToString(), encoding = result.Text.Encoding });
    }

    // IFormattable fallback path (non-ISpanFormattable)

    [Test]
    public async Task Format_IFormattable_Fallback()
    {
        // Arrange — DateTimeOffset implements IFormattable
        const string expected = "Date: 2026-04-10";
        var dto = new DateTimeOffset(2026, 4, 10, 0, 0, 0, TimeSpan.Zero);

        // Act
        var result = Text.Create($"Date: {dto:yyyy-MM-dd}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // TextSpan hole

    [Test]
    public async Task Format_TextSpanHole_Works()
    {
        // Arrange
        const string expected = "Say: hello";
        var text = Text.From("hello");
        var span = text.AsSpan();

        // Act
        var result = Text.Create($"Say: {span}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // Alignment with IUtf8SpanFormattable path (UTF-8 encoding)

    [Test]
    public async Task Format_Utf8_RightAlignedInt()
    {
        // Arrange
        const string expected = "        42";

        // Act
        var result = Text.Create($"{42,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_Utf8_LeftAlignedInt()
    {
        // Arrange
        const string expected = "42        ";

        // Act
        var result = Text.Create($"{42,-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_Utf8_AlignmentSmallerThanValue_NoExtraPadding()
    {
        // Arrange
        const string expected = "12345";

        // Act
        var result = Text.Create($"{12345,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // Alignment through ISpanFormattable path (non-UTF8 encoding)

    [Test]
    public async Task Format_Utf16_RightAlignedInt()
    {
        // Arrange
        const string expected = "        42";

        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{42,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_Utf16_LeftAlignedInt()
    {
        // Arrange
        const string expected = "42        ";

        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{42,-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_Utf16_AlignmentSmallerThanValue_NoExtraPadding()
    {
        // Arrange
        const string expected = "12345";

        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{12345,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // AppendAligned with ReadOnlySpan<char> directly

    [Test]
    public async Task Format_CharSpanAlignment_RightAligned()
    {
        // Arrange
        const string expected = "      hi";
        ReadOnlySpan<char> val = "hi";

        // Act
        var result = Text.Create($"{val,8}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_CharSpanAlignment_LeftAligned()
    {
        // Arrange
        const string expected = "hi      ";
        ReadOnlySpan<char> val = "hi";

        // Act
        var result = Text.Create($"{val,-8}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Format_CharSpanAlignment_NoNeedToPad()
    {
        // Arrange
        const string expected = "hello";
        ReadOnlySpan<char> val = "hello";

        // Act
        var result = Text.Create($"{val,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // IFormattable fallback with alignment (toString path with alignment)

    [Test]
    public async Task Format_ObjectWithAlignment_UsesAppendAligned()
    {
        // Arrange — object only has ToString()
        const string expected = "        hi";
        object val = "hi";

        // Act
        var result = Text.Create($"{val,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // Dispose explicitly

    [Test]
    public async Task FormatPooled_Dispose_IsIdempotent()
    {
        // Arrange
        const string expected = "Hello 42";

        // Act — verify double dispose doesn't throw
        var result = Text.CreatePooled($"Hello {42}");
        var textBefore = result.Text.ToString();
        result.Dispose();
        result.Dispose(); // second dispose should not throw

        // Assert
        await Assert.That(textBefore).IsEqualTo(expected);
    }

    // UTF-32 alignment — exercises AppendSpaces UTF-32 path

    [Test]
    public Task Format_Utf32_RightAlignedInt()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf32, $"{42,10}");

        // Assert
        return Verify(new { result = result.ToString(), result.Encoding });
    }

    [Test]
    public Task Format_Utf32_LeftAlignedInt()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf32, $"{42,-10}");

        // Assert
        return Verify(new { result = result.ToString(), result.Encoding });
    }
}
