namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    [Test]
    public async Task CreateOwned_Dispose_ResetsInstance()
    {
        // Arrange
        var owned = LinkedTextUtf16.CreateOwned("hello", " - ", "world");
        var data = owned.Data!;
        _ = data.AsSequence(); // force sequence cache

        // Act
        owned.Dispose();

        // Assert
        await Assert.That(data.SegmentCount).IsEqualTo(0);
        await Assert.That(data.Length).IsEqualTo(0);
        await Assert.That(data.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CreateOwned_Dispose_InstanceCanBeReused()
    {
        // Arrange — dispose returns to pool
        var owned = LinkedTextUtf16.CreateOwned("hello");
        owned.Dispose();

        // Act — next CreateOwned gets a clean instance (may or may not be same reference)
        using var owned2 = LinkedTextUtf16.CreateOwned("world");

        // Assert — instance is clean and functional
        await Assert.That(owned2.Data!.SegmentCount).IsEqualTo(1);
        await Assert.That(owned2.AsSpan().ToString()).IsEqualTo("world");
    }

    [Test]
    public async Task CreateOwned_PoolFull_DiscardsExcessInstances()
    {
        // Arrange — fill pool beyond MaxPoolSize (32)
        var instances = new LinkedTextUtf16Owned[35];
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i] = LinkedTextUtf16.CreateOwned("test");
        }

        // Act — dispose all, some will be discarded
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i].Dispose();
        }

        // Assert — pool works normally after overflow
        using var owned = LinkedTextUtf16.CreateOwned("verify");
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("verify");
    }

    [Test]
    public async Task CreateOwned_PoolFull_ReturnsFormatBuffer()
    {
        // Arrange — create instances with formatted values
        var instances = new LinkedTextUtf16Owned[35];
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i] = LinkedTextUtf16.CreateOwned($"count: {i}");
        }

        // Act — dispose all, excess instances return format buffer to ArrayPool
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i].Dispose();
        }

        // Assert
        using var owned = LinkedTextUtf16.CreateOwned($"verify: {42}");
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("verify: 42");
    }
}
