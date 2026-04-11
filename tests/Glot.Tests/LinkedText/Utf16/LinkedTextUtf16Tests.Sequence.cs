using System.Buffers;

namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // AsSequence — empty

    [Test]
    public Task AsSequence_Empty_ReturnsEmptySequence()
    {
        // Act
        var seq = LinkedTextUtf16.Empty.AsSequence();

        // Assert
        return Verify(new { seq.Length, seq.IsEmpty });
    }

    // AsSequence — single segment (no nodes needed)

    [Test]
    public Task AsSequence_SingleSegment_ReturnsSingleSegmentSequence()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello");

        // Act
        var seq = linked.AsSequence();

        // Assert
        return Verify(new { seq.Length, seq.IsSingleSegment, firstSpan = new string(seq.FirstSpan) });
    }

    // AsSequence — multi segment

    [Test]
    public async Task AsSequence_MultiSegment_ReturnsCorrectSequence()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var seq = linked.AsSequence();

        // Verify content by copying to array
        var buffer = new char[13];
        seq.CopyTo(buffer);
        var result = new string(buffer);

        // Assert
        const string expected = "hello - world";
        await Assert.That(seq.Length).IsEqualTo(13);
        await Assert.That(seq.IsSingleSegment).IsFalse();
        await Assert.That(result).IsEqualTo(expected);
    }

    // AsSequence — caching

    [Test]
    public Task AsSequence_CalledTwice_ReturnsCachedInstance()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var seq1 = linked.AsSequence();
        var seq2 = linked.AsSequence();

        // Assert — ReadOnlySequence is a struct, but the backing segments are same
        return Verify(new
        {
            lengthMatch = seq1.Length == seq2.Length,
            startMatch = seq1.Start.Equals(seq2.Start),
            endMatch = seq1.End.Equals(seq2.End)
        });
    }

    [Test]
    public async Task AsSequence_ConcurrentCalls_NoNodeLeak()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act — race multiple threads
        var tasks = new Task<ReadOnlySequence<char>>[8];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(linked.AsSequence);
        }

        var results = await Task.WhenAll(tasks);

        // Assert — all results valid
        const long expectedLength = 13;
        for (var i = 0; i < results.Length; i++)
        {
            await Assert.That(results[i].Length).IsEqualTo(expectedLength);
        }
    }

    [Test]
    public async Task SequenceNodePool_CappedAfterManySequences()
    {
        // Arrange — create many sequences to exercise node pool
        for (var i = 0; i < 100; i++)
        {
            var lt = LinkedTextUtf16.Create("a", "b", "c");
            _ = lt.AsSequence();
        }

        // Assert — pool still works
        var lt2 = LinkedTextUtf16.Create("x", "y");
        var seq = lt2.AsSequence();
        const long expectedLength = 2;
        await Assert.That(seq.Length).IsEqualTo(expectedLength);
    }
}
