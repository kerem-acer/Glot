using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8SpanTests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    // Properties

    [Test]
    public async Task Default_IsEmpty()
    {
        // Arrange
        var span = default(LinkedTextUtf8Span);

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
        await Assert.That(span.Length).IsEqualTo(0);
        await Assert.That(span.ToString()).IsEqualTo(string.Empty);
    }

    // Indexer

    [Test]
    public async Task Indexer_ReturnsCorrectByte()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("AB"), Utf8("CD")).AsSpan();

        // Act & Assert
        await Assert.That(span[0]).IsEqualTo((byte)'A');
        await Assert.That(span[1]).IsEqualTo((byte)'B');
        await Assert.That(span[2]).IsEqualTo((byte)'C');
        await Assert.That(span[3]).IsEqualTo((byte)'D');
    }

    [Test]
    public async Task Indexer_OutOfRange_Throws()
    {
        // Arrange
        var span = LinkedTextUtf8.Create(Utf8("hi")).AsSpan();

        // Act & Assert
        await Assert.That(() => _ = span[2]).Throws<ArgumentOutOfRangeException>();
    }
}
