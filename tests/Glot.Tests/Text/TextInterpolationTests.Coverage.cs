namespace Glot.Tests;

public partial class TextInterpolationTests
{
    // IFormattable fallback path (non-ISpanFormattable)

    [Test]
    public async Task Format_IFormattable_Fallback()
    {
        // Arrange — DateTimeOffset implements IFormattable
        var dto = new DateTimeOffset(2026, 4, 10, 0, 0, 0, TimeSpan.Zero);

        // Act
        var result = Text.Create($"Date: {dto:yyyy-MM-dd}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Date: 2026-04-10");
    }

    // TextSpan hole

    [Test]
    public async Task Format_TextSpanHole_Works()
    {
        // Arrange
        var text = Text.From("hello");
        var span = text.AsSpan();

        // Act
        var result = Text.Create($"Say: {span}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Say: hello");
    }

    // Alignment with IUtf8SpanFormattable path (UTF-8 encoding)

    [Test]
    public async Task Format_Utf8_RightAlignedInt()
    {
        // Act
        var result = Text.Create($"{42,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("        42");
    }

    [Test]
    public async Task Format_Utf8_LeftAlignedInt()
    {
        // Act
        var result = Text.Create($"{42,-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("42        ");
    }

    [Test]
    public async Task Format_Utf8_AlignmentSmallerThanValue_NoExtraPadding()
    {
        // Act
        var result = Text.Create($"{12345,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("12345");
    }

    // Alignment through ISpanFormattable path (non-UTF8 encoding)

    [Test]
    public async Task Format_Utf16_RightAlignedInt()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{42,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("        42");
    }

    [Test]
    public async Task Format_Utf16_LeftAlignedInt()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{42,-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("42        ");
    }

    [Test]
    public async Task Format_Utf16_AlignmentSmallerThanValue_NoExtraPadding()
    {
        // Act
        var result = Text.Create(TextEncoding.Utf16, $"{12345,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("12345");
    }

    // AppendAligned with ReadOnlySpan<char> directly

    [Test]
    public async Task Format_CharSpanAlignment_RightAligned()
    {
        // Arrange
        ReadOnlySpan<char> val = "hi";

        // Act
        var result = Text.Create($"{val,8}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("      hi");
    }

    [Test]
    public async Task Format_CharSpanAlignment_LeftAligned()
    {
        // Arrange
        ReadOnlySpan<char> val = "hi";

        // Act
        var result = Text.Create($"{val,-8}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hi      ");
    }

    [Test]
    public async Task Format_CharSpanAlignment_NoNeedToPad()
    {
        // Arrange
        ReadOnlySpan<char> val = "hello";

        // Act
        var result = Text.Create($"{val,3}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hello");
    }

    // IFormattable fallback with alignment (toString path with alignment)

    [Test]
    public async Task Format_ObjectWithAlignment_UsesAppendAligned()
    {
        // Arrange — object only has ToString()
        object val = "hi";

        // Act
        var result = Text.Create($"{val,10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("        hi");
    }

    // Dispose explicitly

    [Test]
    public async Task FormatPooled_Dispose_IsIdempotent()
    {
        // Act — verify double dispose doesn't throw
        var result = Text.CreatePooled($"Hello {42}");
        var textBefore = result.Text.ToString();
        result.Dispose();
        result.Dispose(); // second dispose should not throw

        // Assert
        await Assert.That(textBefore).IsEqualTo("Hello 42");
    }
}
