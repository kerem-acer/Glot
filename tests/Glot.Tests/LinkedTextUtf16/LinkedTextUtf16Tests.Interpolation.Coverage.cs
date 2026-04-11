namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Interpolation with non-string formattable values exercises AppendFormattedCore path

    [Test]
    public async Task Interpolation_WithUtf8TextHole_Transcodes()
    {
        // Arrange
        var utf8Text = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create($"hello {utf8Text}");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello world");
    }

    // ISpanFormattable that exceeds 256 chars exercises buffer retry path

    [Test]
    public async Task Interpolation_LargeFormattedValue_FallsThrough()
    {
        // Arrange — create a string long enough to potentially exceed small ISpanFormattable buffer
        var largeVal = new string('x', 300);

        // Act
        var linked = LinkedTextUtf16.Create($"prefix{largeVal}suffix");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo($"prefix{largeVal}suffix");
    }

    // Memory<char> hole

    [Test]
    public async Task Interpolation_WithMemoryHole_ZeroCopy()
    {
        // Arrange
        var memory = "hello".AsMemory();

        // Act
        var linked = LinkedTextUtf16.Create($"say: {memory}");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("say: hello");
    }

    // Owned interpolation with format buffer

    [Test]
    public async Task InterpolationOwned_WithIntHole_FormatsCorrectly()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned($"count={42}");
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("count=42");
    }
}
