namespace Glot.Tests;

public partial class LinkedTextUtf8SpanTests
{
    [Test]
    public async Task Slice_Offset_Zero_ReturnsSame()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(0);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(13);
        await Assert.That(sliced.ToString()).IsEqualTo("hello - world");
    }

    [Test]
    public async Task Slice_Offset_Full_ReturnsDefault()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello")).AsSpan();

        // Act
        var sliced = span.Slice(5);

        // Assert
        await Assert.That(sliced.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Slice_Offset_AtSegmentBoundary()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(5);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(8);
        await Assert.That(sliced.ToString()).IsEqualTo(" - world");
    }

    [Test]
    public async Task Slice_Offset_IntoLastSegment()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(10);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(3);
        await Assert.That(sliced.ToString()).IsEqualTo("rld");
    }

    [Test]
    public async Task Slice_OffsetCount_FullSpan_ReturnsSame()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(0, 13);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(13);
        await Assert.That(sliced.ToString()).IsEqualTo("hello - world");
    }

    [Test]
    public async Task Slice_OffsetCount_ZeroLength_ReturnsDefault()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello")).AsSpan();

        // Act
        var sliced = span.Slice(2, 0);

        // Assert
        await Assert.That(sliced.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Slice_OffsetCount_WithinSingleSegment()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span.Slice(1, 3);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(3);
        await Assert.That(sliced.ToString()).IsEqualTo("ell");
    }

    [Test]
    public async Task Slice_OffsetCount_AtSegmentBoundaries()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act — exactly the middle segment
        var sliced = span.Slice(5, 3);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(3);
        await Assert.That(sliced.ToString()).IsEqualTo(" - ");
    }

    [Test]
    public async Task Slice_OffsetCount_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello")).AsSpan();

        // Act & Assert
        await Assert.That(() => span.Slice(0, 6)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => span.Slice(3, 3)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task RangeIndexer_SlicesCorrectly()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world")).AsSpan();

        // Act
        var sliced = span[3..10];

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(7);
        await Assert.That(sliced.ToString()).IsEqualTo("lo - wo");
    }
}
