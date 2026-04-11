namespace Glot.Tests;

public class OwnedLinkedTextUtf16Tests
{
    [Test]
    public async Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = LinkedTextUtf16.CreateOwned("hello", " - ", "world");

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
        var owned = LinkedTextUtf16.CreateOwned("hello");

        // Act
        owned.Dispose();

        // Assert
        await Assert.That(owned.IsDisposed).IsTrue();
        await Assert.That(owned.Length).IsEqualTo(0);
        await Assert.That(owned.IsEmpty).IsTrue();
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

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Length_ReturnsDataLength()
    {
        // Arrange
        using var owned = LinkedTextUtf16.CreateOwned("hello", " world");

        // Assert
        await Assert.That(owned.Length).IsEqualTo(11);
        await Assert.That(owned.IsEmpty).IsFalse();
    }

    [Test]
    public async Task Data_ReturnsUnderlyingInstance()
    {
        // Arrange
        using var owned = LinkedTextUtf16.CreateOwned("hello");

        // Assert
        await Assert.That(owned.Data).IsNotNull();
        await Assert.That(owned.Data!.Length).IsEqualTo(5);
    }
}
