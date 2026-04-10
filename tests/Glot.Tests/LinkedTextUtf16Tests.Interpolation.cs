namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Create with interpolation

    [Test]
    public async Task Create_Interpolation_CapturesSegments()
    {
        // Arrange
        var name = "world";

        // Act
        var linked = LinkedTextUtf16.Create($"Hello {name}!");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3); // "Hello ", "world", "!"
        await Assert.That(linked.Length).IsEqualTo(12);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("Hello world!");
    }

    [Test]
    public async Task Create_Interpolation_StringsAreZeroCopy()
    {
        // Arrange
        var name = "world";

        // Act
        var linked = LinkedTextUtf16.Create($"{name}");

        // Assert — single segment, pointing to the original string
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("world");
    }

    [Test]
    public async Task Create_Interpolation_EmptyHoles_Skipped()
    {
        // Arrange
        var empty = "";

        // Act
        var linked = LinkedTextUtf16.Create($"hello{empty}world");

        // Assert — empty hole skipped, but literals are separate
        await Assert.That(linked.SegmentCount).IsEqualTo(2); // "hello", "world"
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("helloworld");
    }

    [Test]
    public async Task Create_Interpolation_NonStringTypes_FormattedToString()
    {
        // Arrange
        const int count = 42;

        // Act
        var linked = LinkedTextUtf16.Create($"count: {count}");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2); // "count: ", "42"
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("count: 42");
    }

    [Test]
    public async Task Create_Interpolation_NoHoles_SingleLiteral()
    {
        // Act
        var linked = LinkedTextUtf16.Create($"hello world");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello world");
    }

    [Test]
    public async Task Create_Interpolation_Empty_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create($"");

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    [Test]
    public async Task Create_Interpolation_MultipleHoles()
    {
        // Arrange
        var a = "hello";
        var sep = " - ";
        var b = "world";

        // Act
        var linked = LinkedTextUtf16.Create($"{a}{sep}{b}");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello - world");
    }

    [Test]
    public async Task Create_Interpolation_WithFormat()
    {
        // Arrange
        var date = new DateTime(2026, 4, 10);

        // Act
        var linked = LinkedTextUtf16.Create($"date: {date:yyyy-MM-dd}");

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("date: 2026-04-10");
    }

    // CreateOwned with interpolation

    [Test]
    public async Task CreateOwned_Interpolation_CapturesSegments()
    {
        // Arrange
        var name = "world";

        // Act
        using var owned = LinkedTextUtf16.CreateOwned($"Hello {name}!");

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("Hello world!");
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(3);
    }

    [Test]
    public async Task CreateOwned_Interpolation_PooledInstanceReused()
    {
        // Arrange — create and dispose to seed pool
        LinkedTextUtf16? first;
        {
            var owned = LinkedTextUtf16.CreateOwned($"hello");
            first = owned.Data;
            owned.Dispose();
        }

        // Act — interpolation owned should reuse pooled instance
        using var owned2 = LinkedTextUtf16.CreateOwned($"world");

        // Assert
        await Assert.That(owned2.Data).IsSameReferenceAs(first);
    }

    [Test]
    public async Task CreateOwned_Interpolation_NullHole_Skipped()
    {
        // Arrange
        string? nullStr = null;

        // Act
        using var owned = LinkedTextUtf16.CreateOwned($"hello{nullStr}world");

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("helloworld");
    }
}
