namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Create with Text — same encoding (UTF-8)

    [Test]
    public Task Create_Utf8Text_CopiesIntoFormatBuffer()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, result = linked.AsSpan().ToString() });
    }

    // Create with Text — different encoding (UTF-16 → UTF-8 transcode)

    [Test]
    public Task Create_Utf16Text_TranscodesToUtf8()
    {
        // Arrange
        var text = Text.From("café");

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert — 5 bytes in UTF-8 (é = 2 bytes)
        return Verify(new { linked.SegmentCount, linked.Length, result = linked.AsSpan().ToString() });
    }

    // Create with two Text values — different encodings

    [Test]
    public Task Create_MixedEncodings_TranscodesCorrectly()
    {
        // Arrange
        var utf8 = Text.FromUtf8("hello "u8);
        var utf16 = Text.From("world");

        // Act
        var linked = LinkedTextUtf8.Create(utf8, utf16);

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    // Create with three Text values

    [Test]
    public Task Create_ThreeTexts_AllTranscoded()
    {
        // Arrange
        var t1 = Text.From("hello");
        var t2 = Text.FromUtf8(" - "u8);
        var t3 = Text.From("world");

        // Act
        var linked = LinkedTextUtf8.Create(t1, t2, t3);

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    // Create (Owned) with Text

    [Test]
    public async Task Create_Text_Works()
    {
        // Arrange
        var text = Text.From("café");

        // Act
        using var owned = OwnedLinkedTextUtf8.Create(text);

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("café");
    }

    [Test]
    public async Task Create_MixedTexts_TranscodesCorrectly()
    {
        // Arrange
        var utf16 = Text.From("hello");
        var utf8 = Text.FromUtf8(" world"u8);

        // Act
        using var owned = OwnedLinkedTextUtf8.Create(utf16, utf8);

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // Empty Text

    [Test]
    public async Task Create_EmptyText_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf8.Create(Text.Empty);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf8.Empty);
    }

    // Format buffer growth — many Text values overflow initial buffer

    [Test]
    public Task Create_ManyTexts_GrowsBuffer()
    {
        // Arrange — each transcoded segment accumulates in format buffer
        var texts = Enumerable.Range(0, 50)
            .Select(i => Text.From($"item{i} "))
            .ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(texts.AsSpan());

        // Assert
        return Verify(new { segmentCountPositive = linked.SegmentCount > 0, lengthPositive = linked.Length > 0 });
    }

    // Unicode — emoji and supplementary characters

    [Test]
    public Task Create_Utf16Text_WithEmoji_TranscodesCorrectly()
    {
        // Arrange — 🎉 is U+1F389, 2 chars UTF-16 (surrogate pair), 4 bytes UTF-8
        var text = Text.From("🎉");

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert — 4 bytes in UTF-8
        return Verify(new { result = linked.AsSpan().ToString(), linked.Length });
    }
}
