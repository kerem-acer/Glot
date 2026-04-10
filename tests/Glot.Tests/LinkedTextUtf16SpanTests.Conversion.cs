using System.Buffers;

namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // ToString

    [Test]
    public async Task ToString_SingleSegment_ReturnsString()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello");
    }

    [Test]
    public async Task ToString_MultiSegment_ReturnsConcatenated()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public async Task ToString_SlicedSpan_ReturnsSlicedContent()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var sliced = span.Slice(3, 7);

        // Act
        var result = sliced.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("lo - wo");
    }

    [Test]
    public async Task ToString_Empty_ReturnsEmptyString()
    {
        // Arrange
        var span = default(LinkedTextUtf16Span);

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    // WriteTo

    [Test]
    public async Task WriteTo_MultiSegment_WritesAllContent()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var writer = new ArrayBufferWriter<char>();

        // Act
        span.WriteTo(writer);

        // Assert
        var written = new string(writer.WrittenSpan);
        await Assert.That(written).IsEqualTo("hello - world");
    }

    [Test]
    public async Task WriteTo_SlicedSpan_WritesSlicedContent()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var sliced = span.Slice(2, 9);
        var writer = new ArrayBufferWriter<char>();

        // Act
        sliced.WriteTo(writer);

        // Assert
        var written = new string(writer.WrittenSpan);
        await Assert.That(written).IsEqualTo("llo - wor");
    }

    [Test]
    public async Task WriteTo_Empty_WritesNothing()
    {
        // Arrange
        var span = default(LinkedTextUtf16Span);
        var writer = new ArrayBufferWriter<char>();

        // Act
        span.WriteTo(writer);

        // Assert
        await Assert.That(writer.WrittenCount).IsEqualTo(0);
    }
}
