using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore.Tests;

public class GlotResultExtensionsTests
{
    [Test]
    public async Task Text_WithText_ReturnsResult()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);

        // Act
        var result = Results.Text(text);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Text_WithOwnedText_ReturnsResult()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("hello"u8)!;

        // Act
        var result = Results.Text(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithText_ReturnsResult()
    {
        // Arrange
        var text = Text.FromUtf8("{}"u8);

        // Act
        var result = Results.Json(text);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithOwnedText_ReturnsResult()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("{}"u8)!;

        // Act
        var result = Results.Json(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Text_WithLinkedTextUtf8_ReturnsResult()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("hello"u8));

        // Act
        var result = Results.Text(linked);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithLinkedTextUtf8_ReturnsResult()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("{}"u8));

        // Act
        var result = Results.Json(linked);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Text_WithOwnedLinkedTextUtf8_ReturnsResult()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("test"u8));

        // Act
        var result = Results.Text(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithOwnedLinkedTextUtf8_ReturnsResult()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("{}"u8));

        // Act
        var result = Results.Json(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Text_WithLinkedTextUtf16_ReturnsResult()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello");

        // Act
        var result = Results.Text(linked);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithLinkedTextUtf16_ReturnsResult()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("{}");

        // Act
        var result = Results.Json(linked);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Text_WithOwnedLinkedTextUtf16_ReturnsResult()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf16.Create("test");

        // Act
        var result = Results.Text(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task Json_WithOwnedLinkedTextUtf16_ReturnsResult()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf16.Create("{}");

        // Act
        var result = Results.Json(owned);

        // Assert
        await Assert.That(result).IsNotNull();
    }
}
