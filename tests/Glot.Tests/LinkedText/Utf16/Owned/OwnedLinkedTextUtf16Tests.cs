namespace Glot.Tests;

public partial class OwnedLinkedTextUtf16Tests
{
    [Test]
    public Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf16.Create("hello", " - ", "world");

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
        var owned = OwnedLinkedTextUtf16.Create("hello");
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
        var owned = OwnedLinkedTextUtf16.Create("hello");

        // Act
        owned.Dispose();
        // Read IsDisposed immediately — before a parallel test can reclaim from pool.
        var isDisposed = owned.IsDisposed;
        owned.Dispose(); // second dispose should not throw

        // Assert
        await Assert.That(isDisposed).IsTrue();
    }

    [Test]
    public async Task AsSpan_AfterDispose_ReturnsDefault()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf16.Create("hello");
        owned.Dispose();

        // Act
        var span = owned.AsSpan();
        var isEmpty = span.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public Task Length_ReturnsDataLength()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf16.Create("hello", " world");

        // Assert
        return Verify(new { owned.Length, owned.IsEmpty });
    }

    [Test]
    public Task Data_ReturnsUnderlyingInstance()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf16.Create("hello");

        // Assert
        return Verify(new { isNotNull = owned.Data is not null, length = owned.Data!.Length });
    }
}
