using System.Buffers;
using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8SpanTests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    // Slicing

    [Test]
    public async Task Slice_Offset_WithinFirstSegment()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(2);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(11);
        await Assert.That(sliced.ToString()).IsEqualTo("llo - world");
    }

    [Test]
    public async Task Slice_OffsetCount_AcrossSegments()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(3, 7);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(7);
        await Assert.That(sliced.ToString()).IsEqualTo("lo - wo");
    }

    [Test]
    public async Task Slice_Offset_Full_ReturnsDefault()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hi")).AsSpan();

        // Act
        var sliced = span.Slice(2);

        // Assert
        await Assert.That(sliced.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Slice_Chained()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var first = span.Slice(2, 9);
        var second = first.Slice(2, 5);

        // Assert
        await Assert.That(second.ToString()).IsEqualTo("o - w");
    }

    // EnumerateSegments on sliced span

    [Test]
    public async Task EnumerateSegments_Sliced_YieldsSlicedSegments()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();
        var sliced = span.Slice(3, 7);
        var segments = new List<string>();

        // Act
        foreach (var seg in sliced.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(3);
        await Assert.That(segments[0]).IsEqualTo("lo");
        await Assert.That(segments[1]).IsEqualTo(" - ");
        await Assert.That(segments[2]).IsEqualTo("wo");
    }

    // WriteTo

    [Test]
    public async Task WriteTo_WritesAllContent()
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

    // Default span

    [Test]
    public async Task Default_IsEmpty()
    {
        // Arrange
        var span = default(LinkedTextUtf8Span);

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
        await Assert.That(span.Length).IsEqualTo(0);
        await Assert.That(span.ToString()).IsEqualTo(string.Empty);
    }

    // Indexer out of range

    [Test]
    public async Task Indexer_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hi")).AsSpan();

        // Act & Assert
        await Assert.That(() => _ = span[2]).Throws<ArgumentOutOfRangeException>();
    }

    // Slice out of range

    [Test]
    public async Task Slice_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hi")).AsSpan();

        // Act & Assert
        await Assert.That(() => span.Slice(3)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => span.Slice(0, 3)).Throws<ArgumentOutOfRangeException>();
    }
}
