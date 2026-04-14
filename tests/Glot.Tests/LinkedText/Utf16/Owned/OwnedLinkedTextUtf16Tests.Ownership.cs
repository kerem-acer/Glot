namespace Glot.Tests;

public partial class OwnedLinkedTextUtf16Tests
{
    [Test]
    public async Task Interpolation_OwnedText_TakesOwnership_ContentCorrect()
    {
        // Arrange
        var original = Text.From("hello world");
        var replaced = original.ReplacePooled("world", "glot")!;

        // Act — default interpolation takes ownership of replaced's buffer
        var result = OwnedLinkedTextUtf16.Create($"prefix:{replaced}:suffix");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("prefix:hello glot:suffix");

        result.Dispose();
    }

    [Test]
    public async Task Interpolation_OwnedText_MixedWithString_ContentCorrect()
    {
        // Arrange
        const string name = "café";
        var sanitized = Text.From("user@example.com").ReplacePooled("@", "[at]")!;

        // Act
        var result = OwnedLinkedTextUtf16.Create($"Name: {name}, Email: {sanitized}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("Name: café, Email: user[at]example.com");

        result.Dispose();
    }

    [Test]
    public async Task CreateWithBorrow_OwnedText_DoesNotTakeOwnership()
    {
        // Arrange
        var replaced = Text.From("hello").ReplacePooled("hello", "world")!;

        // Act — Create with Borrow does NOT take ownership
        using var result = OwnedLinkedTextUtf16.Create(OwnedTextHandling.Borrow, $"value:{replaced.Text}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("value:world");

        // Caller must dispose since ownership was not transferred
        replaced.Dispose();
    }

    [Test]
    public async Task Interpolation_NullOwnedText_Skipped()
    {
        // Arrange
        OwnedText? empty = OwnedText.FromChars([]);

        // Act
        var result = OwnedLinkedTextUtf16.Create($"before{empty}after");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("beforeafter");

        result.Dispose();
    }

    [Test]
    public async Task Interpolation_OwnedText_Utf8Backed_TranscodesAndDisposes()
    {
        // Arrange — create a UTF-8 backed OwnedText
        var utf8Owned = OwnedText.FromUtf8("hello utf8"u8)!;

        // Act — interpolation into UTF-16 linked text should transcode and dispose
        var result = OwnedLinkedTextUtf16.Create($"content:{utf8Owned}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("content:hello utf8");

        result.Dispose();
    }

    [Test]
    public async Task Dispose_WithOwnedSegments_ReturnsBuffersToPool()
    {
        // Arrange
        var replaced = Text.From("test").ReplacePooled("test", "replaced")!;
        var result = OwnedLinkedTextUtf16.Create($"value:{replaced}");

        // Act
        var content = result.AsSpan().ToString();
        result.Dispose();

        // Assert
        await Assert.That(content).IsEqualTo("value:replaced");
    }
}
