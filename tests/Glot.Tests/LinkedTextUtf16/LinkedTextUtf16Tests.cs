namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Factory — Create(string)

    [Test]
    public async Task Create_SingleString_StoresOneSegment()
    {
        // Arrange & Act
        var linked = LinkedTextUtf16.Create("hello");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5);
        await Assert.That(linked.IsEmpty).IsFalse();
    }

    [Test]
    public async Task Create_EmptyString_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create("");

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
        await Assert.That(linked.IsEmpty).IsTrue();
        await Assert.That(linked.Length).IsEqualTo(0);
        await Assert.That(linked.SegmentCount).IsEqualTo(0);
    }

    [Test]
    public async Task Create_NullString_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create((string)null!);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // Factory — Create(string, string)

    [Test]
    public async Task Create_TwoStrings_StoresTwoSegments()
    {
        // Arrange & Act
        var linked = LinkedTextUtf16.Create("hello", " world");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
        await Assert.That(linked.Length).IsEqualTo(11);
    }

    [Test]
    public async Task Create_TwoStrings_OneEmpty_StoresOneSegment()
    {
        // Act
        var linked = LinkedTextUtf16.Create("hello", "");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5);
    }

    [Test]
    public async Task Create_TwoStrings_BothEmpty_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create("", "");

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // Factory — Create(string, string, string)

    [Test]
    public async Task Create_ThreeStrings_StoresThreeSegments()
    {
        // Act
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3);
        await Assert.That(linked.Length).IsEqualTo(13);
    }

    // Factory — Create(string, string, string, string)

    [Test]
    public async Task Create_FourStrings_StoresFourSegments()
    {
        // Act
        var linked = LinkedTextUtf16.Create("a", "b", "c", "d");

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(4);
        await Assert.That(linked.Length).IsEqualTo(4);
    }

    // Factory — Create(ReadOnlyMemory<char>)

    [Test]
    public async Task Create_Memory_StoresOneSegment()
    {
        // Arrange
        var memory = "hello".AsMemory();

        // Act
        var linked = LinkedTextUtf16.Create(memory);

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5);
    }

    [Test]
    public async Task Create_EmptyMemory_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create(ReadOnlyMemory<char>.Empty);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // AsSpan

    [Test]
    public async Task AsSpan_ReturnsSpanCoveringAllContent()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var span = linked.AsSpan();

        // Assert
        await Assert.That(span.Length).IsEqualTo(13);
        await Assert.That(span.IsEmpty).IsFalse();
    }

    [Test]
    public async Task AsSpan_Empty_ReturnsDefaultSpan()
    {
        // Act
        var span = LinkedTextUtf16.Empty.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
        await Assert.That(span.Length).IsEqualTo(0);
    }

    // Overflow — more than 8 segments (triggers overflow array on NET8+)

    [Test]
    public async Task Create_NineSegments_UsesOverflow()
    {
        // Arrange — use interpolation to create 9+ segments
        var a = "a";
        var b = "b";
        var c = "c";

        // Act — 9 segments: 4 literals + 5 holes (but adjacent literals merge in handler)
        // Use explicit construction to guarantee segment count
        LinkedTextUtf16 linked = $"{a}{b}{c}{a}{b}{c}{a}{b}{c}";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(9);
        await Assert.That(linked.Length).IsEqualTo(9);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("abcabcabc");
    }

    // CreateOwned — Memory overloads

    [Test]
    public async Task CreateOwned_SingleMemory_Works()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned("hello".AsMemory());

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task CreateOwned_TwoMemory_Works()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned("hello".AsMemory(), " world".AsMemory());

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // CreateOwned — four strings

    [Test]
    public async Task CreateOwned_FourStrings_Works()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned("a", "b", "c", "d");

        // Assert
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(4);
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("abcd");
    }
}
