namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Create from Text — exercises AppendTextSpan transcoding paths

    [Test]
    public async Task Create_FromUtf8Text_TranscodesToUtf16()
    {
        // Arrange
        var utf8 = Text.FromUtf8("Hello World"u8);

        // Act
        var linked = LinkedTextUtf16.Create(utf8);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromMultipleTexts_TranscodesAll()
    {
        // Arrange
        var a = Text.FromUtf8("Hello"u8);
        var b = Text.From(" World");

        // Act
        var linked = LinkedTextUtf16.Create(a, b);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromUtf8Text_MultiByte_TranscodesCorrectly()
    {
        // Arrange
        var utf8 = Text.FromUtf8("café"u8);

        // Act
        var linked = LinkedTextUtf16.Create(utf8);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café");
    }

    // Format buffer growth

    [Test]
    public async Task Create_ManyTexts_GrowsFormatBuffer()
    {
        // Arrange — each transcoded text accumulates in format buffer
        var texts = Enumerable.Range(0, 50)
            .Select(i => Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes($"item{i} ")))
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked.SegmentCount).IsGreaterThan(0);
        await Assert.That(linked.Length).IsGreaterThan(0);
    }

    // UTF-16 direct copy (no transcode)

    [Test]
    public async Task Create_Utf16Text_DirectCopy()
    {
        // Arrange
        var text = Text.From("hello");

        // Act
        var linked = LinkedTextUtf16.Create(text);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello");
    }
}
