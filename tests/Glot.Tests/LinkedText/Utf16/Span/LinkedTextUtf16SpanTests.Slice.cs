namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // Slice(offset)

    [Test]
    public Task Slice_Offset_Zero_ReturnsSame()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(0);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Slice_Offset_Full_ReturnsDefault()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act
        var sliced = span.Slice(5);
        var isEmpty = sliced.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public Task Slice_Offset_WithinFirstSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(2);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Slice_Offset_AtSegmentBoundary()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(5);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Slice_Offset_IntoMiddleSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(6);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Slice_Offset_IntoLastSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(10);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Slice_Offset_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act & Assert
        await Assert.That(() => span.Slice(6)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => span.Slice(-1)).Throws<ArgumentOutOfRangeException>();
    }

    // Slice(offset, count)

    [Test]
    public Task Slice_OffsetCount_FullSpan_ReturnsSame()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(0, 13);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Slice_OffsetCount_ZeroLength_ReturnsDefault()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act
        var sliced = span.Slice(2, 0);
        var isEmpty = sliced.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public Task Slice_OffsetCount_WithinSingleSegment()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(1, 3);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Slice_OffsetCount_AcrossSegments()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span.Slice(3, 7);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Slice_OffsetCount_AtSegmentBoundaries()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act — slice exactly the middle segment
        var sliced = span.Slice(5, 3);
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Slice_OffsetCount_CountExceedsBounds_Throws()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act & Assert
        await Assert.That(() => span.Slice(0, 6)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => span.Slice(3, 3)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Slice_OffsetCount_OffsetExceedsBounds_Throws()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act & Assert
        await Assert.That(() => span.Slice(6, 1)).Throws<ArgumentOutOfRangeException>();
    }

    // Chained slicing

    [Test]
    public Task Slice_Chained_ProducesCorrectResult()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act — slice then slice again
        var first = span.Slice(2, 9);    // "llo - wor"
        var second = first.Slice(2, 5);  // "o - w"
        var length = second.Length;
        var result = second.ToString();

        // Assert
        return Verify(new { length, result });
    }

    // Range indexer

    [Test]
    public Task RangeIndexer_SlicesCorrectly()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var sliced = span[3..10];
        var length = sliced.Length;
        var result = sliced.ToString();

        // Assert
        return Verify(new { length, result });
    }
}
