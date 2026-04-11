namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Direct assignment — implicit interpolated string handler

    [Test]
    public async Task Interpolation_Direct_CapturesSegments()
    {
        // Arrange
        var name = "world";

        // Act
        LinkedTextUtf16 linked = $"Hello {name}!";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3); // "Hello ", "world", "!"
        await Assert.That(linked.Length).IsEqualTo(12);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("Hello world!");
    }

    [Test]
    public async Task Interpolation_Direct_StringsAreZeroCopy()
    {
        // Arrange
        var name = "world";

        // Act
        LinkedTextUtf16 linked = $"{name}";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("world");
    }

    [Test]
    public async Task Interpolation_Direct_EmptyHoles_Skipped()
    {
        // Arrange
        var empty = "";

        // Act
        LinkedTextUtf16 linked = $"hello{empty}world";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2); // "hello", "world"
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("helloworld");
    }

    [Test]
    public async Task Interpolation_Direct_NonStringTypes_FormattedIntoBuffer()
    {
        // Arrange
        const int count = 42;

        // Act
        LinkedTextUtf16 linked = $"count: {count}";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2); // "count: ", "42"
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("count: 42");
    }

    [Test]
    public async Task Interpolation_Direct_NoHoles_SingleLiteral()
    {
        // Act
        LinkedTextUtf16 linked = $"hello world";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello world");
    }

    [Test]
    public async Task Interpolation_Direct_MultipleHoles()
    {
        // Arrange
        var a = "hello";
        var sep = " - ";
        var b = "world";

        // Act
        LinkedTextUtf16 linked = $"{a}{sep}{b}";

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello - world");
    }

    [Test]
    public async Task Interpolation_Direct_WithFormat()
    {
        // Arrange
        var date = new DateTime(2026, 4, 10);

        // Act
        LinkedTextUtf16 linked = $"date: {date:yyyy-MM-dd}";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("date: 2026-04-10");
    }

    // Owned — direct assignment

    [Test]
    public async Task Interpolation_Owned_Direct()
    {
        // Arrange
        var name = "world";

        // Act
        using LinkedTextUtf16Owned owned = $"Hello {name}!";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("Hello world!");
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(3);
    }

    [Test]
    public async Task Interpolation_Owned_PooledInstanceCanBeReused()
    {
        // Arrange — dispose returns to pool
        {
            LinkedTextUtf16Owned owned = $"hello";
            owned.Dispose();
        }

        // Act — next owned gets a clean instance
        using LinkedTextUtf16Owned owned2 = $"world";

        // Assert
        await Assert.That(owned2.Data!.SegmentCount).IsEqualTo(1);
        await Assert.That(owned2.AsSpan().ToString()).IsEqualTo("world");
    }

    [Test]
    public async Task Interpolation_Owned_NullHole_Skipped()
    {
        // Arrange
        string? nullStr = null;

        // Act
        using LinkedTextUtf16Owned owned = $"hello{nullStr}world";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("helloworld");
    }

    // Non-ISpanFormattable fallback (IFormattable / ToString)

    [Test]
    public async Task Interpolation_Direct_NonSpanFormattable_UsesToString()
    {
        // Arrange
        var obj = new ToStringOnly("test");

        // Act
        LinkedTextUtf16 linked = $"value: {obj}";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("value: test");
    }

    [Test]
    public async Task Interpolation_Direct_NullObject_Skipped()
    {
        // Arrange
        object? nullObj = null;

        // Act
        LinkedTextUtf16 linked = $"hello{nullObj}world";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("helloworld");
    }

    // ReadOnlyMemory<char> hole

    [Test]
    public async Task Interpolation_Direct_ReadOnlyMemoryHole()
    {
        // Arrange
        var mem = "hello".AsMemory();

        // Act
        LinkedTextUtf16 linked = $"{mem} world";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // Owned with Text hole

    [Test]
    public async Task Interpolation_Owned_TextHole()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);

        // Act
        LinkedTextUtf16Owned owned = $"at {text}";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("at café");
        owned.Dispose();
    }

    // Owned with ReadOnlyMemory hole

    [Test]
    public async Task Interpolation_Owned_ReadOnlyMemoryHole()
    {
        // Arrange
        var mem = "hello".AsMemory();

        // Act
        LinkedTextUtf16Owned owned = $"{mem}!";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello!");
        owned.Dispose();
    }

    // Interpolation with empty Text hole — exercises empty early return

    [Test]
    public async Task Interpolation_EmptyTextHole_Skipped()
    {
        // Act
        LinkedTextUtf16 linked = $"hello{Text.Empty}world";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("helloworld");
    }

    // Interpolation with UTF-16 Text hole — exercises TryGetUtf16Memory direct add

    [Test]
    public async Task Interpolation_Utf16TextHole_DirectAdd()
    {
        // Arrange
        var utf16 = Text.From("world");

        // Act
        LinkedTextUtf16 linked = $"hello {utf16}";

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // Interpolation with Text hole — exercises AppendFormattedCore transcoding path

    [Test]
    public async Task Interpolation_WithUtf8TextHole_Transcodes()
    {
        // Arrange
        var utf8Text = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create($"hello {utf8Text}");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello world");
    }

    // ISpanFormattable that exceeds 256 chars — exercises buffer retry path

    [Test]
    public async Task Interpolation_LargeFormattedValue_FallsThrough()
    {
        // Arrange — create a string long enough to potentially exceed small ISpanFormattable buffer
        var largeVal = new string('x', 300);

        // Act
        var linked = LinkedTextUtf16.Create($"prefix{largeVal}suffix");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo($"prefix{largeVal}suffix");
    }

    // Memory<char> hole

    [Test]
    public async Task Interpolation_WithMemoryHole_ZeroCopy()
    {
        // Arrange
        var memory = "hello".AsMemory();

        // Act
        var linked = LinkedTextUtf16.Create($"say: {memory}");
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("say: hello");
    }

    // Owned interpolation with format buffer

    [Test]
    public async Task InterpolationOwned_WithIntHole_FormatsCorrectly()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned($"count={42}");
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("count=42");
    }

    sealed class ToStringOnly(string value)
    {
        public override string ToString() => value;
    }
}
