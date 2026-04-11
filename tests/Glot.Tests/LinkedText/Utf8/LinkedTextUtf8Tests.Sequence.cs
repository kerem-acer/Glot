using System.Buffers;
using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    [Test]
    public Task AsSequence_Empty_ReturnsEmptySequence()
    {
        // Act
        var seq = LinkedTextUtf8.Empty.AsSequence();

        // Assert
        return Verify(new { seq.Length, seq.IsEmpty });
    }

    [Test]
    public Task AsSequence_SingleSegment_IsSingleSegment()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"));

        // Act
        var seq = linked.AsSequence();

        // Assert
        return Verify(new { seq.Length, seq.IsSingleSegment });
    }

    [Test]
    public async Task AsSequence_MultiSegment_ReturnsCorrectContent()
    {
        // Arrange
        const int expectedLength = 13;
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var seq = linked.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(expectedLength);
        var buffer = new byte[expectedLength];
        seq.CopyTo(buffer);
        var result = Encoding.UTF8.GetString(buffer);
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public Task AsSequence_IsCached()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("a"), Utf8("b"));

        // Act
        var seq1 = linked.AsSequence();
        var seq2 = linked.AsSequence();

        // Assert
        return Verify(new { startEqual = seq1.Start.Equals(seq2.Start), endEqual = seq1.End.Equals(seq2.End) });
    }

    [Test]
    public async Task AsSequence_ConcurrentCalls_NoNodeLeak()
    {
        // Arrange
        const int expectedLength = 13;
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var tasks = new Task<ReadOnlySequence<byte>>[8];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(linked.AsSequence);
        }

        var results = await Task.WhenAll(tasks);

        // Assert
        for (var i = 0; i < results.Length; i++)
        {
            await Assert.That(results[i].Length).IsEqualTo(expectedLength);
        }
    }
}
