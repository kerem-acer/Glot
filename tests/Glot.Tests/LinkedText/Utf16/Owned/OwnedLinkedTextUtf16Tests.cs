namespace Glot.Tests;

public class OwnedLinkedTextUtf16Tests
{
    [Test]
    public Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = LinkedTextUtf16.CreateOwned("hello", " - ", "world");

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
        var owned = LinkedTextUtf16.CreateOwned("hello");

        // Act
        owned.Dispose();

        // Assert
        return Verify(new { owned.IsDisposed, owned.Length, owned.IsEmpty });
    }

    [Test]
    public async Task Dispose_CalledTwice_NoError()
    {
        // Arrange
        var owned = LinkedTextUtf16.CreateOwned("hello");

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
        var owned = LinkedTextUtf16.CreateOwned("hello");
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
        using var owned = LinkedTextUtf16.CreateOwned("hello", " world");

        // Assert
        return Verify(new { owned.Length, owned.IsEmpty });
    }

    [Test]
    public Task Data_ReturnsUnderlyingInstance()
    {
        // Arrange
        using var owned = LinkedTextUtf16.CreateOwned("hello");

        // Assert
        return Verify(new { isNotNull = owned.Data is not null, length = owned.Data!.Length });
    }
}
