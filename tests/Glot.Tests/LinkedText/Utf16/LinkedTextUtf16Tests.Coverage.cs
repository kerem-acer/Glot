namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Cross-encoding: UTF-8 Text segments force transcode via AppendTextSpan

    [Test]
    public async Task Create_MultipleUtf8Texts_TranscodesAll()
    {
        // Arrange
        var t1 = Text.FromUtf8("hello"u8);
        var t2 = Text.FromUtf8(" "u8);
        var t3 = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create(t1, t2, t3);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Cross-encoding: UTF-32 Text forces transcode via AppendTextSpan

    [Test]
    public async Task Create_Utf32Text_TranscodesFromUtf32ToUtf16()
    {
        // Arrange — 'H', 'i', '!' as UTF-32 code points
        ReadOnlySpan<int> codePoints = ['H', 'i', '!'];
        var utf32 = Text.FromUtf32(codePoints);

        // Act
        var linked = LinkedTextUtf16.Create(utf32);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "Hi!";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Create_Utf32TextWithEmoji_TranscodesCorrectly()
    {
        // Arrange — U+1F389 = 🎉, 2 chars in UTF-16 (surrogate pair)
        var utf32 = Text.FromUtf32([0x1F389]);

        // Act
        var linked = LinkedTextUtf16.Create(utf32);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("\U0001F389");
        await Assert.That(linked.Length).IsEqualTo(2);
    }

    // Many Text segments (>8) to exercise EnsureOverflowCapacity growth through PopulateTexts

    [Test]
    public async Task Create_ManyTextSegments_ExercisesOverflowGrowth()
    {
        // Arrange — 12 UTF-8 Text values to force transcode + overflow
        var texts = Enumerable.Range(0, 12)
            .Select(i => Text.FromUtf8(new[] { (byte)('a' + i) }))
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(texts.AsSpan());
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijkl";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsGreaterThan(8);
    }

    // Interpolation with formatted values — exercises handler paths

    [Test]
    public async Task Interpolation_WithDouble_FormatsCorrectly()
    {
        // Arrange
        const double value = 3.14;

        // Act
        LinkedTextUtf16 linked = $"pi={value}";
        var result = linked.AsSpan().ToString();

        // Assert
        var expected = $"pi={value}";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_WithDateTime_FormatsCorrectly()
    {
        // Arrange
        var date = new DateTime(2026, 4, 19);

        // Act
        LinkedTextUtf16 linked = $"date={date:yyyy-MM-dd}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "date=2026-04-19";
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
        var linked = LinkedTextUtf16.Create(utf32, utf8, utf16);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "ABC";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Large multi-byte UTF-8 content to force format buffer growth

    [Test]
    public async Task Create_LargeUtf8Text_ForcesFormatBufferResize()
    {
        // Arrange — 500 ASCII chars encoded as UTF-8, transcoded to UTF-16
        var largeStr = new string('x', 500);
        var text = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(largeStr));

        // Act
        var linked = LinkedTextUtf16.Create(text);
        var result = linked.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo(largeStr);
        await Assert.That(linked.Length).IsEqualTo(500);
    }

    // Pooling: create, dispose, create again — no crash and correct content

    [Test]
    public async Task Pool_CreateDisposeCreate_ProducesCorrectContent()
    {
        // Arrange — create and dispose via OwnedLinkedTextUtf16
        var owned1 = OwnedLinkedTextUtf16.Create("first", " value");
        _ = owned1.Data!.AsSequence(); // force sequence cache
        owned1.Dispose();

        // Act — next Create should produce correct content regardless of pool state
        using var owned2 = OwnedLinkedTextUtf16.Create("second");
        var result = owned2.AsSpan().ToString();

        // Assert
        const string expected = "second";
        await Assert.That(result).IsEqualTo(expected);
    }

    // PopulateStrings via OwnedLinkedTextUtf16.Create(string...) — exercises internal PopulateStrings path

    [Test]
    public async Task PopulateStrings_ManyStrings_ExercisesOverflowGrowth()
    {
        // Arrange — 12 strings to exercise overflow growth in PopulateStrings
        var strings = Enumerable.Range(0, 12)
            .Select(i => ((char)('a' + i)).ToString())
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(strings.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijkl";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(12);
    }

    // PopulateStrings with empty strings intermixed — exercises skip logic

    [Test]
    public async Task PopulateStrings_EmptyIntermixed_SkipsEmpty()
    {
        // Arrange
        var strings = new[] { "a", "", "b", null!, "c" };

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(strings.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abc";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(3);
    }

    // Populate via OwnedLinkedTextUtf16.Create(ReadOnlyMemory<char>...) — exercises internal Populate path

    [Test]
    public async Task Populate_ManyMemorySegments_ExercisesOverflowGrowth()
    {
        // Arrange — 12 Memory segments to exercise overflow growth in Populate
        var segments = Enumerable.Range(0, 12)
            .Select(i => ((char)('a' + i)).ToString().AsMemory())
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijkl";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(12);
    }

    // Populate with empty memory segments intermixed

    [Test]
    public async Task Populate_EmptyMemoryIntermixed_SkipsEmpty()
    {
        // Arrange
        var segments = new[]
        {
            "a".AsMemory(),
            ReadOnlyMemory<char>.Empty,
            "b".AsMemory(),
            ReadOnlyMemory<char>.Empty,
            "c".AsMemory(),
        };

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abc";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(3);
    }

    // EnsureOverflowCapacity growth — 20 segments via Populate to force re-rent

    [Test]
    public async Task Populate_GrowPastInitialOverflow_DoublesCapacity()
    {
        // Arrange — 20 segments via Populate path
        var segments = Enumerable.Range(0, 20)
            .Select(i => ((char)('a' + (i % 26))).ToString().AsMemory())
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(segments.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijklmnopqrst";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(20);
    }

    // PopulateTexts with UTF-16 memory — zero-copy via TryGetUtf16Memory

    [Test]
    public async Task Create_Utf16TextSegments_ZeroCopyViaMemory()
    {
        // Arrange — UTF-16 backed texts use TryGetUtf16Memory for zero-copy
        var t1 = Text.From("hello");
        var t2 = Text.From(" world");

        // Act
        var linked = LinkedTextUtf16.Create(t1, t2);
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
    }

    // All-empty Text segments — returns Empty

    [Test]
    public async Task Create_AllEmptyTexts_ReturnsSingleton()
    {
        // Arrange
        var texts = new[] { Text.Empty, Text.Empty };

        // Act
        var linked = LinkedTextUtf16.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // Pool reuse after format buffer was used — exercises format buffer return

    [Test]
    public async Task Pool_AfterFormatBuffer_ResetsCleanly()
    {
        // Arrange — create with UTF-8 text to force format buffer usage
        var owned1 = OwnedLinkedTextUtf16.Create(Text.FromUtf8("transcoded"u8), Text.FromUtf8(" value"u8));
        owned1.Dispose();

        // Act — next Create should get a clean pooled instance
        using var owned2 = OwnedLinkedTextUtf16.Create("fresh");
        var result = owned2.AsSpan().ToString();

        // Assert
        const string expected = "fresh";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Constructor path: ReadOnlySpan<string> with many strings (>8) — exercises direct constructor overflow

    [Test]
    public async Task Create_ManyStringsViaFactory_ExercisesOverflowInConstructor()
    {
        // Arrange — 10 non-empty strings to exercise constructor overflow
        var strings = Enumerable.Range(0, 10)
            .Select(i => ((char)('A' + i)).ToString())
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(strings.AsSpan());
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "ABCDEFGHIJ";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsEqualTo(10);
    }

    // Constructor path: ReadOnlySpan<ReadOnlyMemory<char>> with many memories (>8) — exercises direct constructor overflow

    [Test]
    public async Task Create_ManyMemoriesViaFactory_ExercisesOverflowInConstructor()
    {
        // Arrange — 10 non-empty memories to exercise constructor overflow
        var memories = Enumerable.Range(0, 10)
            .Select(i => ((char)('A' + i)).ToString().AsMemory())
            .ToArray();

        // Act
        var linked = LinkedTextUtf16.Create(memories.AsSpan());
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "ABCDEFGHIJ";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(linked.SegmentCount).IsEqualTo(10);
    }

    // PopulateStrings growth — 20 strings via OwnedLinkedTextUtf16 to force re-rent

    [Test]
    public async Task PopulateStrings_GrowPastInitialOverflow_DoublesCapacity()
    {
        // Arrange
        var strings = Enumerable.Range(0, 20)
            .Select(i => ((char)('a' + (i % 26))).ToString())
            .ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create(strings.AsSpan());
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "abcdefghijklmnopqrst";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(owned.Data!.SegmentCount).IsEqualTo(20);
    }
}
