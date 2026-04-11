using System.Text;

namespace Glot.Tests;

public class OwnedLinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    [Test]
    public Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var span = owned.AsSpan();
        var length = span.Length;
        var result = span.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public Task Dispose_SetsIsDisposed()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));

        // Act
        owned.Dispose();

        // Assert
        return Verify(new { owned.IsDisposed, owned.Length });
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
}
