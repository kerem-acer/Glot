namespace Glot.Tests;

public partial class LinkedTextUtf16SpanTests
{
    // Properties

    [Test]
    public async Task Length_SingleSegment_ReturnsCharCount()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act & Assert
        await Assert.That(span.Length).IsEqualTo(5);
    }

    [Test]
    public async Task Length_MultiSegment_ReturnsTotalCharCount()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act & Assert
        await Assert.That(span.Length).IsEqualTo(13);
    }

    [Test]
    public async Task IsEmpty_NonEmpty_ReturnsFalse()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("x").AsSpan();

        // Act & Assert
        await Assert.That(span.IsEmpty).IsFalse();
    }

    [Test]
    public async Task IsEmpty_Default_ReturnsTrue()
    {
        // Arrange
        var span = default(LinkedTextUtf16Span);

        // Act & Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }

    // Indexer

    [Test]
    public async Task Indexer_SingleSegment_ReturnsCorrectChar()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello").AsSpan();

        // Act & Assert
        await Assert.That(span[0]).IsEqualTo('h');
        await Assert.That(span[4]).IsEqualTo('o');
    }

    [Test]
    public async Task Indexer_MultiSegment_ReturnsCorrectChar()
    {
        // Arrange
        var span = LinkedTextUtf16.Create("hello", " - ", "world").AsSpan();

        // Act & Assert
        await Assert.That(span[0]).IsEqualTo('h');
        await Assert.That(span[4]).IsEqualTo('o');
        await Assert.That(span[5]).IsEqualTo(' ');
        await Assert.That(span[7]).IsEqualTo(' ');
        await Assert.That(span[8]).IsEqualTo('w');
        await Assert.That(span[12]).IsEqualTo('d');
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
