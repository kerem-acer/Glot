namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Cross-encoding: UTF-16 Text forces transcode via AppendTextSpan

    [Test]
    public async Task Create_Utf16TextMultipleSegments_TranscodesAll()
    {
        // Arrange
        var t1 = Text.From("hello");
        var t2 = Text.From(" ");
        var t3 = Text.From("world");

        // Act
        var linked = LinkedTextUtf8.Create(t1, t2, t3);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Cross-encoding: UTF-32 Text forces transcode via AppendTextSpan

    [Test]
    public async Task Create_Utf32Text_TranscodesFromUtf32ToUtf8()
    {
        // Arrange — 'H', 'i', '!' as UTF-32 code points
        ReadOnlySpan<int> codePoints = ['H', 'i', '!'];
        var utf32 = Text.FromUtf32(codePoints);

        // Act
        var linked = LinkedTextUtf8.Create(utf32);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "Hi!";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Create_Utf32TextWithEmoji_TranscodesCorrectly()
    {
        // Arrange — U+1F389 = 🎉, 4 bytes in UTF-8
        var utf32 = Text.FromUtf32([0x1F389]);

        // Act
        var linked = LinkedTextUtf8.Create(utf32);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("\U0001F389");
        await Assert.That(linked.Length).IsEqualTo(4);
    }

    // Many segments (>8) via Text[] to exercise EnsureOverflowCapacity growth through PopulateTexts

    [Test]
    public async Task Create_ManyTextSegments_ExercisesOverflowGrowth()
    {
        // Arrange — 12 Text values, each UTF-16 to force transcode + overflow
        var texts = Enumerable.Range(0, 12)
            .Select(i => Text.From(((char)('a' + i)).ToString()))
            .ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(texts.AsSpan());
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijkl";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsGreaterThan(8);
    }

    // Format buffer growth: large UTF-16 Text forces EnsureFormatBuffer to resize

    [Test]
    public async Task Create_LargeUtf16Text_ForcesFormatBufferResize()
    {
        // Arrange — 500 chars will produce >256 bytes, forcing buffer growth
        var largeStr = new string('x', 500);
        var text = Text.From(largeStr);

        // Act
        var linked = LinkedTextUtf8.Create(text);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo(largeStr);
        await Assert.That(linked.Length).IsEqualTo(500);
    }

    // Pooling: create, dispose, create again — no crash and correct content

    [Test]
    public async Task Pool_CreateDisposeCreate_ProducesCorrectContent()
    {
        // Arrange — create and dispose via OwnedLinkedTextUtf8
        var owned1 = OwnedLinkedTextUtf8.Create(Utf8("first"), Utf8(" value"));
        _ = owned1.Data!.AsSequence(); // force sequence cache allocation
        owned1.Dispose();

        // Act — next Create should produce correct content regardless of pool state
        using var owned2 = OwnedLinkedTextUtf8.Create(Utf8("second"));
        var result = owned2.AsSpan().ToString();

        // Assert
        const string expected = "second";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Mixed UTF-32 + UTF-8 + UTF-16 in a single Create call

    [Test]
    public async Task Create_MixedUtf32Utf8Utf16_TranscodesAll()
    {
        // Arrange
        ReadOnlySpan<int> cp = ['A'];
        var utf32 = Text.FromUtf32(cp);
        var utf8 = Text.FromUtf8("B"u8);
        var utf16 = Text.From("C");

        // Act
        var linked = LinkedTextUtf8.Create(utf32, utf8, utf16);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "ABC";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Large multi-byte UTF-16 content to force format buffer double-growth

    [Test]
    public async Task Create_LargeMultibyteUtf16Text_ForcesMultipleBufferGrowths()
    {
        // Arrange — CJK chars are 3 bytes each in UTF-8, so 200 CJK chars = 600 UTF-8 bytes
        var cjk = new string('\u4e16', 200); // '世' repeated
        var text = Text.From(cjk);

        // Act
        var linked = LinkedTextUtf8.Create(text);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo(cjk);
        await Assert.That(linked.Length).IsEqualTo(600);
    }

    // PopulateTexts with UTF-8 memory — zero-copy via TryGetUtf8Memory

    [Test]
    public async Task Create_Utf8TextSegments_ZeroCopyViaMemory()
    {
        // Arrange — UTF-8 backed texts use TryGetUtf8Memory for zero-copy
        var t1 = Text.FromUtf8("hello"u8);
        var t2 = Text.FromUtf8(" world"u8);

        // Act
        var linked = LinkedTextUtf8.Create(t1, t2);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
    }

    // Populate via OwnedLinkedTextUtf8.Create(ReadOnlyMemory<byte>) — exercises internal Populate path

    [Test]
    public async Task Populate_ManyMemorySegments_ExercisesOverflowGrowth()
    {
        // Arrange — 12 Memory segments to exercise overflow growth in Populate
        var segments = Enumerable.Range(0, 12)
            .Select(i => Utf8(((char)('a' + i)).ToString()))
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf8.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijkl";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(12);
    }

    // Populate with empty segments intermixed — exercises skip logic

    [Test]
    public async Task Populate_EmptySegmentsIntermixed_SkipsEmpty()
    {
        // Arrange — mix empty and non-empty segments
        var segments = new[]
        {
            Utf8("a"),
            ReadOnlyMemory<byte>.Empty,
            Utf8("b"),
            ReadOnlyMemory<byte>.Empty,
            Utf8("c"),
        };

        // Act
        using var owned = OwnedLinkedTextUtf8.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abc";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(3);
    }

    // All-empty Text segments — returns Empty

    [Test]
    public async Task Create_AllEmptyTexts_ReturnsSingleton()
    {
        // Arrange
        var texts = new[] { Text.Empty, Text.Empty };

        // Act
        var linked = LinkedTextUtf8.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf8.Empty);
    }

    // EnsureOverflowCapacity growth — start small, grow past initial rent of 8

    [Test]
    public async Task Populate_GrowPastInitialOverflow_DoublesCapacity()
    {
        // Arrange — 20 segments via Populate path to force re-rent of overflow array
        var segments = Enumerable.Range(0, 20)
            .Select(i => Utf8(((char)('a' + (i % 26))).ToString()))
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf8.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijklmnopqrst";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(20);
    }

    // Pool reuse after format buffer was used — exercises format buffer return

    [Test]
    public async Task Pool_AfterFormatBuffer_ResetsCleanly()
    {
        // Arrange — create with UTF-16 text to force format buffer usage
        var owned1 = OwnedLinkedTextUtf8.Create(Text.From("transcoded"), Text.From(" value"));
        owned1.Dispose();

        // Act — next Create should get a clean pooled instance with no leftover format buffer
        using var owned2 = OwnedLinkedTextUtf8.Create(Utf8("fresh"));
        var result = owned2.AsSpan().ToString();

        // Assert
        const string expected = "fresh";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Create from Text with mixed zero-copy and transcode paths

    [Test]
    public async Task Create_MixedUtf8AndUtf16Texts_UsesCorrectPaths()
    {
        // Arrange — UTF-8 text uses zero-copy, UTF-16 text transcodes
        var utf8Text = Text.FromUtf8("zero-copy"u8);
        var utf16Text = Text.From("-transcode");

        // Act
        var linked = LinkedTextUtf8.Create(utf8Text, utf16Text);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "zero-copy-transcode";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
    }
}
