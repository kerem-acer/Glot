namespace Glot.Tests;

public partial class TextBuilderTests
{
    // Append string to UTF-16 builder — exercises the fixed RuneLength bug

    [Test]
    public async Task Append_String_ToUtf16Builder_TracksRuneLength()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("Hello");
        var text = builder.ToText();

        // Assert
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Append_String_ToUtf16Builder_MultiByte_TracksRuneLength()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("café");
        var text = builder.ToText();

        // Assert
        await Assert.That(text.RuneLength).IsEqualTo(4);
        await Assert.That(text.ToString()).IsEqualTo("café");
    }

    // Cross-encoding appends

    [Test]
    public async Task Append_Utf8Bytes_ToUtf16Builder_TranscodesCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("hello"u8, TextEncoding.Utf8);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("hello");
        await Assert.That(builder.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task Append_Utf16Text_ToUtf8Builder_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var text = Text.From("World");

        // Act
        builder.Append(text);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("World");
        await Assert.That(builder.RuneLength).IsEqualTo(5);
    }

    // ToOwnedText transfers buffer

    [Test]
    public async Task ToOwnedText_TransfersBufferAndResets()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello");

        // Act
        using var owned = builder.ToOwnedText();

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("Hello");
        await Assert.That(builder.IsEmpty).IsTrue();
        await Assert.That(builder.ByteLength).IsEqualTo(0);
    }

    [Test]
    public async Task ToOwnedText_Empty_ReturnsDefault()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        using var owned = builder.ToOwnedText();

        // Assert
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    // TextSpan with matching encoding — same-encoding fast path

    [Test]
    public async Task Append_SameEncodingTextSpan_UsesAppendBytes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var span = new TextSpan("hello"u8, TextEncoding.Utf8);

        // Act
        builder.Append(span);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("hello");
        await Assert.That(builder.RuneLength).IsEqualTo(5);
    }
}
