namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public async Task TrimStart_LeadingWhitespace_RemovesWhitespace()
    {
        // Arrange & Act
        var span = new TextSpan("  \t Hello"u8, TextEncoding.Utf8);
        var result = span.TrimStart().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimStart_NoWhitespace_Unchanged()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.TrimStart().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimStart_AllWhitespace_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan("   \t\n"u8, TextEncoding.Utf8);
        var result = span.TrimStart().IsEmpty;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimStart_Utf16_RemovesWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("  Hi", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.TrimStart().Equals("Hi".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimEnd_TrailingWhitespace_RemovesWhitespace()
    {
        // Arrange & Act
        var span = new TextSpan("Hello  \t "u8, TextEncoding.Utf8);
        var result = span.TrimEnd().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimEnd_NoWhitespace_Unchanged()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.TrimEnd().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TrimEnd_AllWhitespace_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan("   \t\n"u8, TextEncoding.Utf8);
        var result = span.TrimEnd().IsEmpty;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_BothSides_RemovesWhitespace()
    {
        // Arrange & Act
        var span = new TextSpan("  Hello  "u8, TextEncoding.Utf8);
        var result = span.Trim().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_TabsAndNewlines_RemovesAll()
    {
        // Arrange & Act
        var span = new TextSpan("\t\n Hello \n\t"u8, TextEncoding.Utf8);
        var result = span.Trim().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_NoWhitespace_Unchanged()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.Trim().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_EmptySpan_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan([], TextEncoding.Utf8);
        var result = span.Trim().IsEmpty;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_MultiByteContent_PreservesContent()
    {
        // Arrange & Act
        var span = new TextSpan(" café "u8, TextEncoding.Utf8);
        var result = span.Trim().Equals("café".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Trim_Utf32_RemovesWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("  Hello  ", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var result = span.Trim().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }
}
