namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // Create with Text — same encoding (UTF-16)

    [Test]
    public Task Create_Utf16Text_CopiesIntoFormatBuffer()
    {
        // Arrange
        var text = Text.From("hello");

        // Act
        var linked = LinkedTextUtf16.Create(text);

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, result = linked.AsSpan().ToString() });
    }

    // Create with Text — different encoding (UTF-8 → UTF-16 transcode)

    [Test]
    public Task Create_Utf8Text_TranscodesToUtf16()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);

        // Act
        var linked = LinkedTextUtf16.Create(text);

        // Assert
        return Verify(new { linked.SegmentCount, linked.Length, result = linked.AsSpan().ToString() });
    }

    // Create with two Text values — different encodings

    [Test]
    public Task Create_MixedEncodings_TranscodesCorrectly()
    {
        // Arrange
        var utf16 = Text.From("hello ");
        var utf8 = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create(utf16, utf8);

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    // Create with three Text values

    [Test]
    public Task Create_ThreeTexts_AllTranscoded()
    {
        // Arrange
        var t1 = Text.FromUtf8("hello"u8);
        var t2 = Text.From(" - ");
        var t3 = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create(t1, t2, t3);

        // Assert
        return Verify(new { linked.SegmentCount, result = linked.AsSpan().ToString() });
    }

    // CreateOwned with Text

    [Test]
    public async Task CreateOwned_Text_Works()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);

        // Act
        using var owned = LinkedTextUtf16.CreateOwned(text);

        // Assert
        const string expected = "café";
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task CreateOwned_MixedTexts_TranscodesCorrectly()
    {
        // Arrange
        var utf8 = Text.FromUtf8("hello"u8);
        var utf16 = Text.From(" world");

        // Act
        using var owned = LinkedTextUtf16.CreateOwned(utf8, utf16);

        // Assert
        const string expected = "hello world";
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
    }

    // Interpolation with Text

    [Test]
    public async Task Interpolation_WithText_TranscodesCorrectly()
    {
        // Arrange
        var name = Text.FromUtf8("world"u8);

        // Act
        var linked = LinkedTextUtf16.Create($"Hello {name}!");

        // Assert
        const string expected = "Hello world!";
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_WithMixedTextEncodings()
    {
        // Arrange
        var utf8Name = Text.FromUtf8("café"u8);
        var utf16Place = Text.From("Paris");

        // Act
        var linked = LinkedTextUtf16.Create($"{utf8Name} in {utf16Place}");

        // Assert
        const string expected = "café in Paris";
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo(expected);
    }

    // Interpolation with TextSpan

    [Test]
    public async Task Interpolation_WithTextSpan_TranscodesCorrectly()
    {
        // Arrange
        var text = Text.FromUtf8("world"u8);
        var span = text.AsSpan();

        // Act — direct assignment uses LinkedTextUtf16 as handler (supports ref struct TextSpan)
        LinkedTextUtf16 linked = $"Hello {span}!";

        // Assert
        const string expected = "Hello world!";
        await Assert.That(linked.AsSpan().ToString()).IsEqualTo(expected);
    }

    // CreateOwned interpolation with Text

    [Test]
    public async Task CreateOwned_Interpolation_WithText()
    {
        // Arrange
        var name = Text.FromUtf8("world"u8);

        // Act
        using var owned = LinkedTextUtf16.CreateOwned($"Hello {name}!");

        // Assert
        const string expected = "Hello world!";
        await Assert.That(owned.AsSpan().ToString()).IsEqualTo(expected);
    }

    // Empty Text

    [Test]
    public async Task Create_EmptyText_ReturnsSingleton()
    {
        // Act
        var linked = LinkedTextUtf16.Create(Text.Empty);

        // Assert
        await Assert.That(linked).IsSameReferenceAs(LinkedTextUtf16.Empty);
    }

    // Unicode — emoji and supplementary characters

    [Test]
    public Task Create_Utf8Text_WithEmoji_TranscodesCorrectly()
    {
        // Arrange — 🎉 is U+1F389, 4 bytes UTF-8, 2 chars UTF-16 (surrogate pair)
        var text = Text.FromUtf8("🎉"u8);

        // Act
        var linked = LinkedTextUtf16.Create(text);

        // Assert
        return Verify(new { result = linked.AsSpan().ToString(), linked.Length });
    }
}
