using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    [Test]
    public async Task CreateOwned_Dispose_ResetsInstance()
    {
        // Arrange
        var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"), Utf8(" - "), Utf8("world"));
        var data = owned.Data!;
        _ = data.AsSequence();

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
        LinkedTextUtf8? first;
        {
            var owned = LinkedTextUtf8.CreateOwned(Utf8("hello"));
            first = owned.Data;
            owned.Dispose();
        }

        // Act
        using var owned2 = LinkedTextUtf8.CreateOwned(Utf8("world"));

        // Assert
        await Assert.That(owned2.Data).IsSameReferenceAs(first);
        await Assert.That(owned2.AsSpan().ToString()).IsEqualTo("world");
    }
}
