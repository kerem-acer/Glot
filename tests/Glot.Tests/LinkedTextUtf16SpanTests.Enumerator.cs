namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // EnumerateSegments — full span

    [Test]
    public async Task EnumerateSegments_FullSpan_YieldsAllSegments()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var segments = new List<string>();

        // Act
        foreach (var seg in span.EnumerateSegments())
        {
            segments.Add(new string(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(3);
        await Assert.That(segments[0]).IsEqualTo("hello");
        await Assert.That(segments[1]).IsEqualTo(" - ");
        await Assert.That(segments[2]).IsEqualTo("world");
    }

    [Test]
    public async Task EnumerateSegments_SingleSegment_YieldsOne()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();
        var segments = new List<string>();

        // Act
        foreach (var seg in span.EnumerateSegments())
        {
            segments.Add(new string(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(1);
        await Assert.That(segments[0]).IsEqualTo("hello");
    }

    [Test]
    public async Task EnumerateSegments_Default_YieldsNothing()
    {
        // Arrange
        var span = default(LinkedTextUtf16Span);
        var count = 0;

        // Act
        foreach (var _ in span.EnumerateSegments())
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    // EnumerateSegments — sliced span

    [Test]
    public async Task EnumerateSegments_SlicedWithinFirstSegment_YieldsSlicedSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var sliced = span.Slice(1, 3); // "ell"
        var segments = new List<string>();

        // Act
        foreach (var seg in sliced.EnumerateSegments())
        {
            segments.Add(new string(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(1);
        await Assert.That(segments[0]).IsEqualTo("ell");
    }

    [Test]
    public async Task EnumerateSegments_SlicedAcrossSegments_YieldsSlicedFirstAndLast()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var sliced = span.Slice(3, 7); // "lo - wo"
        var segments = new List<string>();

        // Act
        foreach (var seg in sliced.EnumerateSegments())
        {
            segments.Add(new string(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(3);
        await Assert.That(segments[0]).IsEqualTo("lo");
        await Assert.That(segments[1]).IsEqualTo(" - ");
        await Assert.That(segments[2]).IsEqualTo("wo");
    }

    [Test]
    public async Task EnumerateSegments_SlicedAtBoundary_YieldsFullMiddleSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();
        var sliced = span.Slice(5, 3); // " - "
        var segments = new List<string>();

        // Act
        foreach (var seg in sliced.EnumerateSegments())
        {
            segments.Add(new string(seg.Span));
        }

        // Assert
        await Assert.That(segments.Count).IsEqualTo(1);
        await Assert.That(segments[0]).IsEqualTo(" - ");
    }
}
