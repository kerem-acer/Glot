namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Create with Text — same encoding (UTF-8)

    [Test]
    public async Task Create_Utf8Text_CopiesIntoFormatBuffer()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello");
    }

    // Create with Text — different encoding (UTF-16 → UTF-8 transcode)

    [Test]
    public async Task Create_Utf16Text_TranscodesToUtf8()
    {
        // Arrange
        var text = Text.From("café");

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(1);
        await Assert.That(linked.Length).IsEqualTo(5); // 5 bytes in UTF-8 (é = 2 bytes)
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("café");
    }

    // Create with two Text values — different encodings

    [Test]
    public async Task Create_MixedEncodings_TranscodesCorrectly()
    {
        // Arrange
        var utf8 = Text.FromUtf8("hello "u8);
        var utf16 = Text.From("world");

        // Act
        var linked = LinkedTextUtf8.Create(utf8, utf16);

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(2);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello world");
    }

    // Create with three Text values

    [Test]
    public async Task Create_ThreeTexts_AllTranscoded()
    {
        // Arrange
        var t1 = Text.From("hello");
        var t2 = Text.FromUtf8(" - "u8);
        var t3 = Text.From("world");

        // Act
        var linked = LinkedTextUtf8.Create(t1, t2, t3);

        // Assert
        await Assert.That(linked.SegmentCount).IsEqualTo(3);
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("hello - world");
    }

    // CreateOwned with Text

    [Test]
    public async Task CreateOwned_Text_Works()
    {
        // Arrange
        var text = Text.From("café");

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(text);

        // Assert
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo("café");
    }

    [Test]
    public async Task CreateOwned_MixedTexts_TranscodesCorrectly()
    {
        // Arrange
        var utf16 = Text.From("hello");
        var utf8 = Text.FromUtf8(" world"u8);

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(utf16, utf8);

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
    public async Task Create_ManyTexts_GrowsBuffer()
    {
        // Arrange — each transcoded segment accumulates in format buffer
        var texts = Enumerable.Range(0, 50)
            .Select(i => Text.From($"item{i} "))
            .ToArray();

        // Act
        var linked = LinkedTextUtf8.Create(texts.AsSpan());

        // Assert
        await Assert.That(linked.SegmentCount).IsGreaterThan(0);
        await Assert.That(linked.Length).IsGreaterThan(0);
    }

    // Unicode — emoji and supplementary characters

    [Test]
    public async Task Create_Utf16Text_WithEmoji_TranscodesCorrectly()
    {
        // Arrange — 🎉 is U+1F389, 2 chars UTF-16 (surrogate pair), 4 bytes UTF-8
        var text = Text.From("🎉");

        // Act
        var linked = LinkedTextUtf8.Create(text);

        // Assert
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo("🎉");
        await Assert.That(linked.Length).IsEqualTo(4); // 4 bytes in UTF-8
    }
}
