namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    [Test]
    public Task Create_Dispose_ResetsInstance()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));
        var data = owned.Data!;
        _ = data.AsSequence();

        // Act
        owned.Dispose();

        // Assert
        return Verify(new { data.SegmentCount, data.Length, data.IsEmpty });
    }

    [Test]
    public Task Create_Dispose_InstanceCanBeReused()
    {
        // Arrange — dispose returns to pool
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));
        owned.Dispose();

        // Act — next Create gets a clean instance
        using var owned2 = OwnedLinkedTextUtf8.Create(Utf8("world"));

        // Assert
        return Verify(new { segmentCount = owned2.Data!.SegmentCount, result = owned2.AsSpan().ToString() });
    }

    [Test]
    public async Task Create_PoolFull_DiscardsExcessInstances()
    {
        // Arrange
        var instances = new OwnedLinkedTextUtf8[35];
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i] = OwnedLinkedTextUtf8.Create(Utf8("test"));
        }

        // Act
        for (var i = 0; i < instances.Length; i++)
        {
            instances[i].Dispose();
        }

        // Assert
        using var owned = OwnedLinkedTextUtf8.Create(Utf8("verify"));
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("verify");
    }
}
