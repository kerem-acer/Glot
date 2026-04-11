namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // Properties

    [Test]
    public async Task Length_SingleSegment_ReturnsCharCount()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act
        var length = span.Length;

        // Assert
        const int expected = 5;
        await Assert.That(length).IsEqualTo(expected);
    }

    [Test]
    public async Task Length_MultiSegment_ReturnsTotalCharCount()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var length = span.Length;

        // Assert
        const int expected = 13;
        await Assert.That(length).IsEqualTo(expected);
    }

    [Test]
    public async Task IsEmpty_NonEmpty_ReturnsFalse()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("x").AsSpan();

        // Act
        var isEmpty = span.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    public async Task IsEmpty_Default_ReturnsTrue()
    {
        // Arrange
        var span = default(LinkedTextUtf16Span);

        // Act
        var isEmpty = span.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    // Indexer

    [Test]
    public Task Indexer_SingleSegment_ReturnsCorrectChar()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act
        var first = span[0];
        var last = span[4];

        // Assert
        return Verify(new { first, last });
    }

    [Test]
    public Task Indexer_MultiSegment_ReturnsCorrectChar()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act
        var char0 = span[0];
        var char4 = span[4];
        var char5 = span[5];
        var char7 = span[7];
        var char8 = span[8];
        var char12 = span[12];

        // Assert
        return Verify(new { char0, char4, char5, char7, char8, char12 });
    }

    [Test]
    public async Task Indexer_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hi").AsSpan();

        // Act & Assert
        await Assert.That(() => _ = span[2]).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => _ = span[-1]).Throws<ArgumentOutOfRangeException>();
    }
}
