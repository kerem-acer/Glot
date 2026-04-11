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

        // Assert — capture eagerly; pooled object may be reused by parallel tests
        var segmentCount = data.SegmentCount;
        var length = data.Length;
        var isEmpty = data.IsEmpty;
        await Assert.That(segmentCount).IsEqualTo(0);
        await Assert.That(length).IsEqualTo(0);
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public Task CreateOwned_Dispose_InstanceCanBeReused()
    {
        // Arrange — dispose returns to pool
        var owned = LinkedTextUtf16.CreateOwned("hello");
        owned.Dispose();

        // Act — next CreateOwned gets a clean instance (may or may not be same reference)
        using var owned2 = LinkedTextUtf16.CreateOwned("world");

        // Assert — instance is clean and functional
        return Verify(new { segmentCount = owned2.Data!.SegmentCount, result = owned2.AsSpan().ToString() });
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
        const string expected = "verify";
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
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
        const string expected = "verify: 42";
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
    }
}
