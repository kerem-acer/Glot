namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Factory — Create(string)

    [Test]
    public Task Create_SingleString_StoresOneSegment()
    {
        // Arrange & Act
        var linked = LinkedTextUtf16.Create("hello");

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, linked.IsEmpty });
    }

    [Test]
    public async Task Create_EmptyString_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create("");

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
        await Verify(new { linked.IsEmpty, linked.Length, linked.SegmentCount });
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
    public Task Create_TwoStrings_StoresTwoSegments()
    {
        // Arrange & Act
        var linked = LinkedTextUtf16.Create("hello", " world");

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
    }

    [Test]
    public Task Create_TwoStrings_OneEmpty_StoresOneSegment()
    {
        // Act
        var linked = LinkedTextUtf16.Create("hello", "");

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
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
    public Task Create_ThreeStrings_StoresThreeSegments()
    {
        // Act
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
    }

    // Factory — Create(string, string, string, string)

    [Test]
    public Task Create_FourStrings_StoresFourSegments()
    {
        // Act
        var linked = LinkedTextUtf16.Create("a", "b", "c", "d");

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
    }

    // Factory — Create(ReadOnlyMemory<char>)

    [Test]
    public Task Create_Memory_StoresOneSegment()
    {
        // Arrange
        var memory = "hello".AsMemory();

        // Act
        var linked = LinkedTextUtf16.Create(memory);

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
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
    public Task AsSpan_ReturnsSpanCoveringAllContent()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " - ", "world");

        // Act
        var span = linked.AsSpan();

        // Assert
        return Verify(new { span.Length, span.IsEmpty });
    }

    [Test]
    public Task AsSpan_Empty_ReturnsDefaultSpan()
    {
        // Act
        var span = LinkedTextUtf16.Empty.AsSpan();

        // Assert
        return Verify(new { span.IsEmpty, span.Length });
    }

    // Overflow — more than 8 segments (triggers overflow array on NET8+)

    [Test]
    public Task Create_NineSegments_UsesOverflow()
    {
        // Arrange — use interpolation to create 9+ segments
        var a = "a";
        var b = "b";
        var c = "c";

        // Act — 9 segments: 4 literals + 5 holes (but adjacent literals merge in handler)
        // Use explicit construction to guarantee segment count
        LinkedTextUtf16 linked = $"{a}{b}{c}{a}{b}{c}{a}{b}{c}";

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, result = linked.AsSpan().ToString() });
    }

    // CreateOwned — Memory overloads

    [Test]
    public async Task CreateOwned_SingleMemory_Works()
    {
        // Act
        const string expected = "hello";
        using var owned = LinkedTextUtf16.CreateOwned(expected.AsMemory());

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task CreateOwned_TwoMemory_Works()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned("hello".AsMemory(), " world".AsMemory());

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // Create from Text — exercises AppendTextSpan transcoding paths

    [Test]
    public async Task Create_FromUtf8Text_TranscodesToUtf16()
    {
        // Arrange
        var utf8 = Text.FromUtf8("Hello World"u8);

        // Act
        var linked = LinkedTextUtf16.Create(utf8);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromMultipleTexts_TranscodesAll()
    {
        // Arrange
        var a = Text.FromUtf8("Hello"u8);
        var b = Text.From(" World");

        // Act
        var linked = LinkedTextUtf16.Create(a, b);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Create_FromUtf8Text_MultiByte_TranscodesCorrectly()
    {
        // Arrange
        var utf8 = Text.FromUtf8("café"u8);

        // Act
        var linked = LinkedTextUtf16.Create(utf8);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café");
    }

    // Format buffer growth

    [Test]
    public async Task Create_ManyTexts_GrowsFormatBuffer()
    {
        // Arrange — each transcoded text accumulates in format buffer
        var texts = Enumerable.Range(0, 50)
            .Select(i => Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes($"item{i} ")))
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked.SegmentCount).IsGreaterThan(0);
        await Assert.That(linked.Length).IsGreaterThan(0);
    }

    // UTF-16 direct copy (no transcode)

    [Test]
    public async Task Create_Utf16Text_DirectCopy()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.From(expected);

        // Act
        var linked = LinkedTextUtf16.Create(text);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // CreateOwned — four strings

    [Test]
    public async Task CreateOwned_FourStrings_Works()
    {
        // Act
        using var owned = LinkedTextUtf16.CreateOwned("a", "b", "c", "d");

        // Assert
        await Verify(new { segmentCount = owned.Data!.SegmentCount, result = owned.AsSpan().ToString() });
    }

    // Overflow — factory with >8 Memory segments triggers overflow array

    [Test]
    public Task Create_NineMemorySegments_UsesOverflow()
    {
        // Arrange
        var segments = Enumerable.Range(0, 9)
            .Select(i => ((char)('a' + i)).ToString().AsMemory())
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(segments.AsSpan());

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    [Test]
    public Task Create_NineStrings_UsesOverflow()
    {
        // Arrange
        var strings = Enumerable.Range(0, 9)
            .Select(i => ((char)('a' + i)).ToString())
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(strings.AsSpan());

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    // EnumerateSegments on LinkedTextUtf16

    [Test]
    public Task EnumerateSegments_ViaLinkedText_YieldsAll()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("hello", " ", "world");
        var segments = new List<string>();

        // Act
        foreach (var seg in linked.EnumerateSegments())
        {
            segments.Add(seg.ToString());
        }

        // Assert
        return Verify(segments);
    }

    // Owned — TextSpan hole and generic <T> hole

    [Test]
    public async Task Owned_TextSpanHole_Works()
    {
        // Arrange
        var text = Text.From("world");
        var span = text.AsSpan();

        // Act
        using LinkedTextUtf16Owned owned = $"hello {span}";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello world");
    }

    [Test]
    public async Task Owned_GenericHole_FormatsValue()
    {
        // Act
        using LinkedTextUtf16Owned owned = $"count={42}";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("count=42");
    }

    [Test]
    public async Task Owned_GenericHoleWithFormat_FormatsValue()
    {
        // Act
        using LinkedTextUtf16Owned owned = $"val={42:D5}";

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("val=00042");
    }

    // CreateOwned with overflow Memory segments

    [Test]
    public async Task CreateOwned_NineMemorySegments_UsesOverflow()
    {
        // Arrange
        var segments = Enumerable.Range(0, 9)
            .Select(i => ((char)('a' + i)).ToString().AsMemory())
            .ToArray();

        // Act
        using var owned = LinkedTextUtf16.CreateOwned(segments.AsSpan());

        // Assert
        await Verify(new { segmentCount = owned.Data!.SegmentCount, result = owned.AsSpan().ToString() });
    }

    // All-empty segments via factory

    [Test]
    public async Task Create_AllEmptyStrings_ReturnsSingleton()
    {
        // Arrange
        var strings = new[] { "", "", "" };

        // Act
        var linked = LinkedTextUtf16.Create(strings.AsSpan());

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    [Test]
    public async Task Create_AllEmptyMemory_ReturnsSingleton()
    {
        // Arrange
        var segments = new[] { ReadOnlyMemory<char>.Empty, ReadOnlyMemory<char>.Empty };

        // Act
        var linked = LinkedTextUtf16.Create(segments.AsSpan());

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // AppendTextSpan — UTF-16 same-encoding fast path

    [Test]
    public async Task Create_FromUtf16TextSpan_DirectCopy()
    {
        // Arrange — Text.From creates UTF-16 backed text
        const string expected = "hello";
        var text = Text.From(expected);

        // Act — exercises AppendTextSpan UTF-16 fast path
        var linked = LinkedTextUtf16.Create(text);

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo(expected);
    }

    // Thread-safe AsSequence — concurrent access

    [Test]
    public async Task AsSequence_ConcurrentAccess_BothSucceed()
    {
        // Arrange — need >1 segment to trigger the CompareExchange path
        const string expected = "hello world";
        var linked = LinkedTextUtf16.Create("hello", " ", "world");
        var results = new System.Collections.Concurrent.ConcurrentBag<string>();
        var barrier = new Barrier(2);

        // Act — two threads race to build the sequence
        var t1 = Task.Run(() =>
        {
            barrier.SignalAndWait();
            var seq = linked.AsSequence();
            results.Add(string.Concat(seq));
        });
        var t2 = Task.Run(() =>
        {
            barrier.SignalAndWait();
            var seq = linked.AsSequence();
            results.Add(string.Concat(seq));
        });
        await Task.WhenAll(t1, t2);

        // Assert — both threads got the correct result
        await Assert.That(results.Count).IsEqualTo(2);
        foreach (var r in results)
        {
            await Assert.That(r).IsEqualTo(expected);
        }
    }
}
