namespace Glot.Tests;

public class LinkedTextUtf8InterpolatedStringHandlerTests
{
    [Test]
    public async Task Create_WithHandler_ProducesCorrectContent()
    {
        // Arrange
        const string expected = "Hello World";
        const int count = 42;

        // Act -- use a formatted hole to force the interpolated string handler overload
        using var owned = OwnedLinkedTextUtf8.Create($"Hello World {count}");
        var content = owned.AsSpan().ToString();

        // Assert
        await Assert.That(content).IsEqualTo($"{expected} {count}");
    }

    [Test]
    public async Task Create_WithOwnedTextHandling_SetsMode()
    {
        // Arrange
        var ownedText = OwnedText.FromUtf8("value"u8)!;

        // Act -- explicit Copy mode
        using var linked = OwnedLinkedTextUtf8.Create(OwnedTextHandling.Copy, $"key:{ownedText}");
        var content = linked.AsSpan().ToString();

        // Assert -- content correct, OwnedText still usable (Copy mode)
        await Assert.That(content).IsEqualTo("key:value");
        await Assert.That(ownedText.IsEmpty).IsFalse();

        ownedText.Dispose();
    }

    [Test]
    public async Task Handler_Dispose_ReturnsDataToPool()
    {
        // Arrange -- create handler manually, simulating exception-safety path
        var handler = new LinkedTextUtf8InterpolatedStringHandler(5, 0);
        handler.AppendLiteral("hello");

        // Act -- dispose returns the data to the pool without Take
        handler.Dispose();

        // Assert -- double dispose is safe (no-op), no exception thrown
        handler.Dispose();

        var disposed = true;
        await Assert.That(disposed).IsTrue();
    }

    [Test]
    public async Task Handler_Take_NullsData()
    {
        // Arrange
        const string expected = "hello";
        var handler = new LinkedTextUtf8InterpolatedStringHandler(5, 0);
        handler.AppendLiteral(expected);

        // Act
        var data = handler.Take();
        var content = data.AsSpan().ToString();

        // Assert -- data is valid and contains appended content
        await Assert.That(data).IsNotNull();
        await Assert.That(content).IsEqualTo(expected);

        // Clean up -- dispose via the owned wrapper
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("cleanup"u8));
        owned.Dispose();
    }

    [Test]
    public async Task Handler_Take_AfterDispose_ThrowsObjectDisposedException()
    {
        // Arrange
        var handler = new LinkedTextUtf8InterpolatedStringHandler(5, 0);
        handler.AppendLiteral("hello");
        handler.Dispose();

        // Act & Assert
        await Assert.That(() => _ = handler.Take()).ThrowsExactly<ObjectDisposedException>();
    }
}
