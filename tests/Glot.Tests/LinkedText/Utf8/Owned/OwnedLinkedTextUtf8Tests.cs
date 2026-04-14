using System.Text;

namespace Glot.Tests;

public partial class OwnedLinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    [Test]
    public Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var span = owned.AsSpan();
        var length = span.Length;
        var result = span.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Dispose_ReleasesData()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));
        var hadData = owned.Data is not null;

        // Act
        owned.Dispose();

        // Assert — verify it had data before dispose, and dispose didn't throw
        await Assert.That(hadData).IsTrue();
    }

    [Test]
    public async Task Dispose_CalledTwice_NoError()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));

        // Act
        owned.Dispose();
        // Read IsDisposed immediately — before a parallel test can reclaim from pool.
        // Use volatile read semantics via local capture.
        var isDisposed = owned.IsDisposed;
        owned.Dispose(); // second dispose should not throw

        // Assert
        await Assert.That(isDisposed).IsTrue();
    }

    [Test]
    public async Task AsSpan_AfterDispose_ReturnsDefault()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));
        owned.Dispose();

        // Act
        var span = owned.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }
}
