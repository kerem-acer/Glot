using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    // Factory

    [Test]
    public Task Create_SingleSegment_StoresOneSegment()
    {
        // Arrange & Act
        var linked = LinkedTextUtf8.Create(Utf8("hello"));

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, linked.IsEmpty });
    }

    [Test]
    public async Task Create_EmptyMemory_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf8.Create(ReadOnlyMemory<byte>.Empty);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf8.Empty);
    }

    [Test]
    public Task Create_TwoSegments_StoresBoth()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" world"));

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
    }

    [Test]
    public Task Create_ThreeSegments_StoresAll()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Utf8("a"), Utf8(" - "), Utf8("b"));

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length });
    }

    // AsSpan + ToString

    [Test]
    public async Task AsSpan_ToString_DecodesUtf8()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello - world");
    }

    [Test]
    public Task AsSpan_ToString_MultibyteChars()
    {
        // Arrange — "café" is 5 bytes in UTF-8 (é = 2 bytes)
        var linked = LinkedTextUtf8.Create(Utf8("caf"), Utf8("é"));

        // Act
        var result = linked.AsSpan().ToString();

        // Assert
        return Verify(new { result, linked.Length });
    }

    // Slicing

    [Test]
    public Task Slice_AcrossSegments_WorksCorrectly()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));
        var span = linked.AsSpan();

        // Act — byte-level slice
        var sliced = span.Slice(3, 7);
        var length = sliced.Length;
        var text = sliced.ToString();

        // Assert
        return Verify(new { length, text });
    }

    // Indexer

    [Test]
    public Task Indexer_ReturnsCorrectByte()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("AB"), Utf8("CD"));
        var span = linked.AsSpan();

        // Act
        var byte0 = span[0];
        var byte1 = span[1];
        var byte2 = span[2];
        var byte3 = span[3];

        // Assert
        return Verify(new { byte0, byte1, byte2, byte3 });
    }

    // EnumerateSegments

    [Test]
    public Task EnumerateSegments_YieldsAllSegments()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));
        var span = linked.AsSpan();
        var segments = new List<string>();

        // Act
        foreach (var seg in span.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        return Verify(segments);
    }

    // Four-arg factory

    [Test]
    public Task Create_FourSegments_StoresAll()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Utf8("a"), Utf8("b"), Utf8("c"), Utf8("d"));

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, text = linked.AsSpan().ToString() });
    }

    // Direct UTF-8 bytes — non-transcode copy path

    [Test]
    public async Task Create_FromUtf8Bytes_DirectCopy()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);

        // Act
        var linked = LinkedTextUtf8.Create(new ReadOnlyMemory<byte>(bytes));
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    // CreateOwned — Memory segments

    [Test]
    public async Task CreateOwned_FromMemorySegments_Works()
    {
        // Arrange
        var bytes1 = "hello"u8.ToArray().AsMemory();
        var bytes2 = " world"u8.ToArray().AsMemory();

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(bytes1, bytes2);
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello world");
    }

    // Empty segment filtering

    [Test]
    public async Task Create_EmptySegment_Filtered()
    {
        // Act
        var linked = LinkedTextUtf8.Create(ReadOnlyMemory<byte>.Empty);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf8.Empty);
    }

    // Overflow — factory with >8 Memory segments triggers overflow array

    [Test]
    public Task Create_NineSegments_UsesOverflow()
    {
        // Arrange
        var segments = Enumerable.Range(0, 9)
            .Select(i => Utf8(((char)('a' + i)).ToString()))
            .ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(segments.AsSpan());

        // Assert
        return Verify(new { linked.SegmentCount, text = linked.AsSpan().ToString() });
    }

    // EnumerateSegments on LinkedTextUtf8

    [Test]
    public Task EnumerateSegments_ViaLinkedText_YieldsAll()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" "), Utf8("world"));
        var segments = new List<string>();

        // Act
        foreach (var seg in linked.EnumerateSegments())
        {
            segments.Add(Encoding.UTF8.GetString(seg.Span));
        }

        // Assert
        return Verify(segments);
    }

    // LinkedTextUtf8Owned IsEmpty

    [Test]
    public async Task Owned_IsEmpty_WhenEmpty()
    {
        // Act
        using var owned = LinkedTextUtf8.CreateOwned(ReadOnlyMemory<byte>.Empty);

        // Assert
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    // CreateOwned with overflow segments

    [Test]
    public Task CreateOwned_NineSegments_UsesOverflow()
    {
        // Arrange
        var segments = Enumerable.Range(0, 9)
            .Select(i => Utf8(((char)('a' + i)).ToString()))
            .ToArray();

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(segments.AsSpan());

        // Assert
        return Verify(new { SegmentCount = owned.Data!.SegmentCount, text = owned.AsSpan().ToString() });
    }

    // All-empty segments via factory

    [Test]
    public async Task Create_AllEmptySegments_ReturnsSingleton()
    {
        // Arrange
        var segments = new[] { ReadOnlyMemory<byte>.Empty, ReadOnlyMemory<byte>.Empty };

        // Act
        var linked = LinkedTextUtf8.Create(segments.AsSpan());

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf8.Empty);
    }

    // AsSpan on empty LinkedTextUtf8

    [Test]
    public async Task AsSpan_Empty_ReturnsDefault()
    {
        // Act
        var span = LinkedTextUtf8.Empty.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }

    // Thread-safe AsSequence — concurrent access

    [Test]
    public async Task AsSequence_ConcurrentAccess_BothSucceed()
    {
        // Arrange — need >1 segment to trigger the CompareExchange path
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" "), Utf8("world"));
        var results = new System.Collections.Concurrent.ConcurrentBag<string>();
        var barrier = new Barrier(2);

        // Act — two threads race to build the sequence
        var t1 = Task.Run(() =>
        {
            barrier.SignalAndWait();
            _ = linked.AsSequence(); // trigger race
            results.Add(linked.AsSpan().ToString());
        });
        var t2 = Task.Run(() =>
        {
            barrier.SignalAndWait();
            _ = linked.AsSequence(); // trigger race
            results.Add(linked.AsSpan().ToString());
        });
        await Task.WhenAll(t1, t2);

        // Assert — both threads got the correct result
        await Verify(results.OrderBy(r => r).ToList());
    }
}
