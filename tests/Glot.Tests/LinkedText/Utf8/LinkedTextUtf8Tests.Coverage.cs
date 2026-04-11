namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Create from Text — exercises AppendTextSpan transcoding paths

    [Test]
    public async Task Create_FromUtf16Text_TranscodesToUtf8()
    {
        // Arrange
        var utf16 = Text.From("Hello World");

        // Act
        var linked = LinkedTextUtf8.Create(utf16);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromMultipleTexts_TranscodesAll()
    {
        // Arrange
        var a = Text.From("Hello");
        var b = Text.FromUtf8(" World"u8);

        // Act
        var linked = LinkedTextUtf8.Create(a, b);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromUtf16Text_MultiByte_TranscodesCorrectly()
    {
        // Arrange
        var utf16 = Text.From("café");

        // Act
        var linked = LinkedTextUtf8.Create(utf16);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café");
    }

    // Empty text in Create

    [Test]
    public async Task Create_EmptyText_ReturnsEmpty()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Text.Empty);

        // Assert
        await Assert.That(linked.IsEmpty).IsTrue();
    }

    // Format buffer growth — add enough segments to overflow initial buffer

    [Test]
    public async Task Create_ManyTexts_GrowsBuffer()
    {
        // Arrange — each transcoded segment accumulates in format buffer
        var texts = Enumerable.Range(0, 50)
            .Select(i => Text.From($"item{i} "))
            .ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked.SegmentCount).IsGreaterThan(0);
        await Assert.That(linked.Length).IsGreaterThan(0);
    }

    // Direct UTF-8 bytes — exercises non-transcode copy path

    [Test]
    public async Task Create_FromUtf8Bytes_DirectCopy()
    {
        // Arrange
        var bytes = "hello"u8.ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(new ReadOnlyMemory<byte>(bytes));
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello");
    }
}
