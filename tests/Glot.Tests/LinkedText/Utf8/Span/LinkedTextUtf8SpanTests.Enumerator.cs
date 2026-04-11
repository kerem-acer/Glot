using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8SpanTests
{
    [Test]
    public Task EnumerateSegments_FullSpan_YieldsAllSegments()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();
        var segments = new List<string>();

        // Act
        foreach (var seg in span.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        return Verify(segments);
    }

    [Test]
    public Task EnumerateSegments_SingleSegment_YieldsOne()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello")).AsSpan();
        var segments = new List<string>();

        // Act
        foreach (var seg in span.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        return Verify(segments);
    }

    [Test]
    public async Task EnumerateSegments_Default_YieldsNothing()
    {
        // Arrange
        var span = default(LinkedTextUtf8Span);
        var count = 0;

        // Act
        foreach (var _ in span.EnumerateSegments())
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public Task EnumerateSegments_SlicedAcrossSegments_YieldsSlicedFirstAndLast()
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
        return Verify(segments);
    }

    [Test]
    public Task EnumerateSegments_SlicedAtBoundary_YieldsFullMiddleSegment()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();
        var sliced = span.Slice(5, 3);
        var segments = new List<string>();

        // Act
        foreach (var seg in sliced.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        return Verify(segments);
    }
}
