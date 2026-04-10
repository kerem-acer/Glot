using System.Buffers;
using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    [Test]
    public async Task AsSequence_Empty_ReturnsEmptySequence()
    {
        // Act
        var seq = LinkedTextUtf8.Empty.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(0);
        await Assert.That(seq.IsEmpty).IsTrue();
    }

    [Test]
    public async Task AsSequence_SingleSegment_IsSingleSegment()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"));

        // Act
        var seq = linked.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(5);
        await Assert.That(seq.IsSingleSegment).IsTrue();
    }

    [Test]
    public async Task AsSequence_MultiSegment_ReturnsCorrectContent()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var seq = linked.AsSequence();

        // Assert
        await Assert.That(seq.Length).IsEqualTo(13);
        var buffer = new byte[13];
        seq.CopyTo(buffer);
        var result = Encoding.UTF8.GetString(buffer);
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public async Task AsSequence_IsCached()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("a"), Utf8("b"));

        // Act
        var seq1 = linked.AsSequence();
        var seq2 = linked.AsSequence();

        // Assert
        await Assert.That(seq1.Start.Equals(seq2.Start)).IsTrue();
        await Assert.That(seq1.End.Equals(seq2.End)).IsTrue();
    }

    [Test]
    public async Task AsSequence_ConcurrentCalls_NoNodeLeak()
    {
        // Arrange
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
            await Assert.That(results[i].Length).IsEqualTo(13);
        }
    }
}
