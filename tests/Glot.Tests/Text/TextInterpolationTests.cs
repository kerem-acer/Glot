namespace Glot.Tests;

public partial class TextInterpolationTests
{
    [Test]
    public async Task Format_LiteralOnly_ReturnsText()
    {
        // Act
        var result = Text.Create($"Hello");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task Format_StringHole_Interpolates()
    {
        // Arrange
        var name = "World";

        // Act
        var result = Text.Create($"Hello {name}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Format_IntHole_Interpolates()
    {
        // Act
        var result = Text.Create($"Count: {42}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Count: 42");
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
        // Act
        var result = Text.Create($"{"hi",10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("        hi");
    }

    [Test]
    public async Task Format_LeftAlignment_PadsRight()
    {
        // Act
        var result = Text.Create($"{"hi",-10}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hi        ");
    }

    [Test]
    public async Task Format_TextHole_NoToString()
    {
        // Arrange
        var prefix = Text.FromUtf8("Hello"u8);

        // Act
        var result = Text.Create($"{prefix} World!");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World!");
    }

    [Test]
    public async Task Format_MultipleHoles_Interpolates()
    {
        // Arrange
        var a = 1;
        var b = 2;
        var c = 3;

        // Act
        var result = Text.Create($"{a} + {b} = {c}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("1 + 2 = 3");
    }

    [Test]
    public async Task Format_ExplicitEncoding_UsesEncoding()
    {
        // Arrange
        var name = "World";

        // Act
        var result = Text.Create(TextEncoding.Utf16, $"Hello {name}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FormatPooled_ReturnsOwnedText()
    {
        // Arrange
        var name = "World";

        // Act
        using var result = Text.CreatePooled($"Hello {name}");

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Format_NullStringHole_AppendsEmpty()
    {
        // Arrange
        string? value = null;

        // Act
        var result = Text.Create($"Value: {value}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Value: ");
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
        ReadOnlySpan<char> name = "World";

        // Act
        var result = Text.Create($"Hello {name}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Format_BoolHole_Interpolates()
    {
        // Act
        var result = Text.Create($"Active: {true}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Active: True");
    }

    [Test]
    public async Task Format_AlignmentWithFormat_Works()
    {
        // Act
        var result = Text.Create($"{42,10:D5}");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("     00042");
    }

    [Test]
    public async Task FormatPooled_ExplicitEncoding_Works()
    {
        // Act
        using var result = Text.CreatePooled(TextEncoding.Utf32, $"Hi {42}");

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hi 42");
        await Assert.That(result.Text.Encoding).IsEqualTo(TextEncoding.Utf32);
    }
}
