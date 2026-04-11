using System.Buffers;
using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8SpanTests
{
    [Test]
    public async Task ToString_MultiSegment_ReturnsConcatenated()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public async Task ToString_SingleSegment_ReturnsString()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello")).AsSpan();

        // Act
        var result = span.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello");
    }

    [Test]
    public async Task ToString_Empty_ReturnsEmptyString()
    {
        // Arrange
        var span = default(LinkedTextUtf8Span);

        // Act & Assert
        await Assert.That(span.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task WriteTo_MultiSegment_WritesAllContent()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();
        var writer = new ArrayBufferWriter<byte>();

        // Act
        span.WriteTo(writer);

        // Assert
        var result = Encoding.UTF8.GetString(writer.WrittenSpan);
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public async Task WriteTo_Empty_WritesNothing()
    {
        // Arrange
        var span = default(LinkedTextUtf8Span);
        var writer = new ArrayBufferWriter<byte>();

        // Act
        span.WriteTo(writer);

        // Assert
        await Assert.That(writer.WrittenCount).IsEqualTo(0);
    }

    [Test]
    public async Task WriteTo_SlicedSpan_WritesSlicedContent()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();
        var sliced = span.Slice(2, 9);
        var writer = new ArrayBufferWriter<byte>();

        // Act
        sliced.WriteTo(writer);

        // Assert
        var result = Encoding.UTF8.GetString(writer.WrittenSpan);
        await Assert.That(result).IsEqualTo("llo - wor");
    }
}
