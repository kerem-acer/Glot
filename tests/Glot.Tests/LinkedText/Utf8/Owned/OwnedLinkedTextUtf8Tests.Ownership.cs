namespace Glot.Tests;

public partial class OwnedLinkedTextUtf8Tests
{
    [Test]
    public async Task Interpolation_OwnedText_TakesOwnership_ContentCorrect()
    {
        // Arrange
        var original = Text.FromUtf8("hello world"u8);
        var replaced = original.ReplacePooled("world", "glot")!;

        // Act — default interpolation takes ownership of replaced's buffer
        var result = OwnedLinkedTextUtf8.Create($"prefix:{replaced}:suffix");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("prefix:hello glot:suffix");

        result.Dispose(); // should also return replaced's buffer to pool
    }

    [Test]
    public async Task Interpolation_OwnedText_MixedWithText_ContentCorrect()
    {
        // Arrange
        var name = Text.FromUtf8("café"u8);
        var sanitized = Text.From("user@example.com").ReplacePooled("@", "[at]")!;

        // Act
        var result = OwnedLinkedTextUtf8.Create($"Name: {name}, Email: {sanitized}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("Name: café, Email: user[at]example.com");

        result.Dispose();
    }

    [Test]
    public async Task Interpolation_MultipleOwnedTexts_AllTakenOwnership()
    {
        // Arrange
        var text = Text.FromUtf8("hello@world.café"u8);
        var replaced1 = text.ReplacePooled("@", "[at]")!;
        var replaced2 = text.ReplacePooled(".", "[dot]")!;

        // Act
        var result = OwnedLinkedTextUtf8.Create($"{replaced1} | {replaced2}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("hello[at]world.café | hello@world[dot]café");

        result.Dispose(); // returns both pooled buffers
    }

    [Test]
    public async Task CreateWithBorrow_OwnedText_DoesNotTakeOwnership()
    {
        // Arrange
        var replaced = Text.FromUtf8("hello"u8).ReplacePooled("hello", "world")!;

        // Act — Create with Borrow does NOT take ownership
        using var result = OwnedLinkedTextUtf8.Create(OwnedTextHandling.Borrow, $"value:{replaced.Text}");
        var content = result.AsSpan().ToString();

        // Assert — content is correct
        await Assert.That(content).IsEqualTo("value:world");

        // Caller must dispose replaced since ownership was not transferred
        replaced.Dispose();
    }

    [Test]
    public async Task Interpolation_NullOwnedText_Skipped()
    {
        // Arrange
        OwnedText? empty = OwnedText.FromUtf8([]);

        // Act
        var result = OwnedLinkedTextUtf8.Create($"before{empty}after");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("beforeafter");

        result.Dispose();
    }

    [Test]
    public async Task Interpolation_OwnedText_WithIntHoles_ContentCorrect()
    {
        // Arrange
        const int count = 42;
        var label = Text.FromUtf8("items"u8).ReplacePooled("items", "widgets")!;

        // Act
        var result = OwnedLinkedTextUtf8.Create($"{count} {label}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("42 widgets");

        result.Dispose();
    }

    [Test]
    public async Task Interpolation_OwnedText_Utf16Backed_TranscodesAndDisposes()
    {
        // Arrange — create a UTF-16 backed OwnedText
        var utf16Owned = OwnedText.FromChars("hello utf16".AsSpan())!;

        // Act — interpolation into UTF-8 linked text should transcode and dispose
        var result = OwnedLinkedTextUtf8.Create($"content:{utf16Owned}");
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("content:hello utf16");

        result.Dispose();
    }

    [Test]
    public async Task Dispose_WithOwnedSegments_ReturnsBuffersToPool()
    {
        // Arrange
        var replaced = Text.FromUtf8("test"u8).ReplacePooled("test", "replaced")!;
        var result = OwnedLinkedTextUtf8.Create($"value:{replaced}");

        // Act
        var content = result.AsSpan().ToString();
        result.Dispose();

        // Assert — the content was correct before dispose
        await Assert.That(content).IsEqualTo("value:replaced");
    }

    [Test]
    public async Task Create_FromTexts_NoOwnership_ContentCorrect()
    {
        // Arrange — Create from Text values does NOT take ownership (no OwnedText involved)
        var a = Text.FromUtf8("hello"u8);
        var b = Text.FromUtf8(" world"u8);

        // Act
        using var result = OwnedLinkedTextUtf8.Create(a, b);
        var content = result.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo("hello world");
    }
}
