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
    public async Task CreateOwned_Dispose_InstanceReusedFromPool()
    {
        // Arrange
        LinkedTextUtf16? first;
        {
            var owned = LinkedTextUtf16.CreateOwned("hello");
            first = owned.Data;
            owned.Dispose();
        }

        // Act — second CreateOwned should reuse the pooled instance
        using var owned2 = LinkedTextUtf16.CreateOwned("world");

        // Assert
        await Assert.That(owned2.Data).IsSameReferenceAs(first);
        await Assert.That(owned2.AsSpan().ToString()).IsEqualTo("world");
    }
}
