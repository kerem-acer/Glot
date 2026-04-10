using System.Buffers;

namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // AsSequence — empty

    [Test]
    public async Task AsSequence_Empty_ReturnsEmptySequence()
    {
        // Act
        var seq = LinkedTextUtf16.Empty.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(0);
        await Assert.That(seq.IsEmpty).IsTrue();
    }

    // AsSequence — single segment (no nodes needed)

    [Test]
    public async Task AsSequence_SingleSegment_ReturnsSingleSegmentSequence()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello");

        // Act
        var seq = linked.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(5);
        await Assert.That(seq.IsSingleSegment).IsTrue();
        await Assert.That(new string(seq.FirstSpan)).IsEqualTo("hello");
    }

    // AsSequence — multi segment

    [Test]
    public async Task AsSequence_MultiSegment_ReturnsCorrectSequence()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var seq = linked.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(13);
        await Assert.That(seq.IsSingleSegment).IsFalse();

        // Verify content by copying to array
        var buffer = new char[13];
        seq.CopyTo(buffer);
        var result = new string(buffer);
        await Assert.That(result).IsEqualTo("hello - world");
    }

    // AsSequence — caching

    [Test]
    public async Task AsSequence_CalledTwice_ReturnsCachedInstance()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var seq1 = linked.AsSequence();
        var seq2 = linked.AsSequence();

        // Assert — ReadOnlySequence is a struct, but the backing segments are same
        await Assert.That(seq1.Length).IsEqualTo(seq2.Length);
        await Assert.That(seq1.Start.Equals(seq2.Start)).IsTrue();
        await Assert.That(seq1.End.Equals(seq2.End)).IsTrue();
    }
}
