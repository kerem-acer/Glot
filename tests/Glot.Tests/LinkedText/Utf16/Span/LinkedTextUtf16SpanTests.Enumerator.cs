namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // EnumerateSegments — full span

    [Test]
    public Task EnumerateSegments_FullSpan_YieldsAllSegments()
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
        return Verify(segments);
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
        const string expected = "hello";
        await Assert.That(segments.Count).IsEqualTo(1);
        await Assert.That(segments[0]).IsEqualTo(expected);
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
    public Task EnumerateSegments_SlicedWithinFirstSegment_YieldsSlicedSegment()
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
        return Verify(segments);
    }

    [Test]
    public Task EnumerateSegments_SlicedAcrossSegments_YieldsSlicedFirstAndLast()
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
        return Verify(segments);
    }

    [Test]
    public Task EnumerateSegments_SlicedAtBoundary_YieldsFullMiddleSegment()
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
        return Verify(segments);
    }
}
