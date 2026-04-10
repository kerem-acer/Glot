using System.Text;

namespace Glot.Tests;

public class OwnedLinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    [Test]
    public async Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var span = owned.AsSpan();

        // Assert
        await Assert.That(span.Length).IsEqualTo(13);
        await Assert.That(span.ToString()).IsEqualTo("hello - world");
    }

    [Test]
    public async Task Dispose_SetsIsDisposed()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));

        // Act
        owned.Dispose();

        // Assert
        await Assert.That(owned.IsDisposed).IsTrue();
        await Assert.That(owned.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Dispose_ResetsData()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"), Utf8("world"));
        var data = owned.Data!;

        // Act
        owned.Dispose();

        // Assert
        await Assert.That(data.SegmentCount).IsEqualTo(0);
        await Assert.That(data.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Dispose_CalledTwice_NoError()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));

        // Act
        owned.Dispose();
        owned.Dispose();

        // Assert
        await Assert.That(owned.IsDisposed).IsTrue();
    }

    [Test]
    public async Task AsSpan_AfterDispose_ReturnsDefault()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));
        owned.Dispose();

        // Act
        var span = owned.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }

    [Test]
    public async Task PooledInstance_IsReused()
    {
        // Arrange
        LinkedTextUtf8? firstInstance;
        {
            var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));
            firstInstance = owned.Data;
            owned.Dispose();
        }

        // Act
        using var owned2 = LinkedTextUtf8.CreateOwned(Utf8("world"));

        // Assert
        await Assert.That(owned2.Data).IsSameReferenceAs(firstInstance);
    }
}
