using System.Text;

namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    // Factory

    [Test]
    public async Task Create_SingleSegment_StoresOneSegment()
    {
        // Arrange & Act
        var linked = LinkedTextUtf8.Create(Utf8("hello"));

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5);
        await Assert.That(linked.IsEmpty).IsFalse();
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
    public async Task Create_TwoSegments_StoresBoth()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" world"));

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
        await Assert.That(linked.Length).IsEqualTo(11);
    }

    [Test]
    public async Task Create_ThreeSegments_StoresAll()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Utf8("a"), Utf8(" - "), Utf8("b"));

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3);
        await Assert.That(linked.Length).IsEqualTo(5);
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
    public async Task AsSpan_ToString_MultibyteChars()
    {
        // Arrange — "café" is 5 bytes in UTF-8 (é = 2 bytes)
        var linked = LinkedTextUtf8.Create(Utf8("caf"), Utf8("é"));

        // Act
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café");
        await Assert.That(linked.Length).IsEqualTo(5);
    }

    // Slicing

    [Test]
    public async Task Slice_AcrossSegments_WorksCorrectly()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));
        var span = linked.AsSpan();

        // Act — byte-level slice
        var sliced = span.Slice(3, 7);

        // Assert
        await Assert.That(sliced.Length).IsEqualTo(7);
        await Assert.That(sliced.ToString()).IsEqualTo("lo - wo");
    }

    // Indexer

    [Test]
    public async Task Indexer_ReturnsCorrectByte()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Utf8("AB"), Utf8("CD"));
        var span = linked.AsSpan();

        // Act & Assert
        await Assert.That(span[0]).IsEqualTo((byte)'A');
        await Assert.That(span[1]).IsEqualTo((byte)'B');
        await Assert.That(span[2]).IsEqualTo((byte)'C');
        await Assert.That(span[3]).IsEqualTo((byte)'D');
    }

    // EnumerateSegments

    [Test]
    public async Task EnumerateSegments_YieldsAllSegments()
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
        await Assert.That(segments.Count).IsEqualTo(3);
        await Assert.That(segments[0]).IsEqualTo("hello");
        await Assert.That(segments[1]).IsEqualTo(" - ");
        await Assert.That(segments[2]).IsEqualTo("world");
    }
}
