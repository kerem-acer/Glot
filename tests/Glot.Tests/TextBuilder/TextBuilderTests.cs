using System.Text;

namespace Glot.Tests;

public partial class TextBuilderTests
{
    [Test]
    public Task DefaultEncoding_IsUtf8()
    {
        // Arrange & Act
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Assert
        return Verify(new { builder.Encoding, builder.IsEmpty });
    }

    [Test]
    public Task Append_String_TranscodesToUtf8()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength, builder.ByteLength });
    }

    [Test]
    public Task Append_MultipleStrings_Concatenates()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");
        builder.Append(" ");
        builder.Append("World");

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }

    [Test]
    public async Task Append_EmptyString_NoEffect()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("");
        builder.Append(null!);

        // Assert
        await Assert.That(builder.IsEmpty).IsTrue();
    }

    [Test]
    public Task Append_Utf8Bytes_SameEncoding_DirectCopy()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("café"u8);

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength, builder.ByteLength });
    }

    [Test]
    public Task Append_Utf8Bytes_ToUtf16Builder_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("Hello"u8);

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength, builder.ByteLength });
    }

    [Test]
    public async Task Append_TextSpan_CrossEncoding_Transcodes()
    {
        // Arrange
        const string expected = "café";
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var utf16Bytes = TestHelpers.Encode(expected, TextEncoding.Utf16);

        // Act
        builder.Append(new TextSpan(utf16Bytes, TextEncoding.Utf16));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Append_Text_Works()
    {
        // Arrange
        const string expected = "Hello";
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append(Text.From(expected));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public Task AppendRune_Appended()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.AppendRune(new Rune('A'));
        builder.AppendRune(new Rune(0x1F389));

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }

    [Test]
    public async Task AppendLine_AppendsNewline()
    {
        // Arrange
        const string expected = "Hello\nWorld";
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");
        builder.AppendLine();
        builder.Append("World");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Append_CharSpan_Transcodes()
    {
        // Arrange
        const string expected = "café";
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append(expected.AsSpan());

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public Task ToText_ReturnsCorrectText()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello World");

        // Act
        var text = builder.ToText();

        // Assert
        return Verify(new { result = text.ToString(), text.Encoding, text.RuneLength });
    }

    [Test]
    public async Task ToText_Empty_ReturnsTextEmpty()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        var text = builder.ToText();

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public Task ToOwnedText_TransfersBuffer()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello");

        // Act
        using var owned = builder.ToOwnedText();

        // Assert
        return Verify(new { result = owned.Text.ToString(), builderIsEmpty = builder.IsEmpty, builderByteLength = builder.ByteLength });
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

    [Test]
    public Task ToOwnedText_BuilderCanContinue()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("First");
        using var first = builder.ToOwnedText();
        builder.Append("Second");
        using var second = builder.ToOwnedText();

        // Assert
        return Verify(new { first = first.Text.ToString(), second = second.Text.ToString() });
    }

    [Test]
    public async Task AsSpan_ReturnsCurrentContent()
    {
        // Arrange
        const string expected = "Hello";
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append(expected);

        // Act
        var eq = builder.AsSpan().Equals(expected.AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public Task Clear_ResetsContent()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello");

        // Act
        builder.Clear();

        // Assert
        return Verify(new { builder.IsEmpty, builder.ByteLength, builder.RuneLength });
    }

    [Test]
    public async Task Clear_CanAppendAgain()
    {
        // Arrange
        const string expected = "Second";
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("First");

        // Act
        builder.Clear();
        builder.Append(expected);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Growth_LargeAppend_GrowsBuffer()
    {
        // Arrange
        const string expected = "Hello World, this is a string much longer than 4 bytes";
        using var builder = new TextBuilder(4, TextEncoding.Utf8);

        // Act
        builder.Append(expected);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public Task Growth_ManySmallAppends_GrowsCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(4, TextEncoding.Utf8);

        // Act
        for (var i = 0; i < 1000; i++)
        {
            builder.AppendRune(new Rune('A'));
        }

        // Assert
        return Verify(new { builder.RuneLength, builder.ByteLength });
    }

    [Test]
    public Task MixedAppends_AllTranscodeCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello ");
        builder.Append("World"u8);
        builder.AppendRune(new Rune('!'));

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }

    [Test]
    public Task Utf32Builder_AppendsCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf32);

        // Act
        builder.Append("Hi!");

        // Assert
        return Verify(new { result = builder.ToString(), builder.ByteLength, builder.RuneLength });
    }

    // UTF-16 builder — string append tracks RuneLength

    [Test]
    public Task Append_String_ToUtf16Builder_TracksRuneLength()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("Hello");
        var text = builder.ToText();

        // Assert
        return Verify(new { text.RuneLength, result = text.ToString() });
    }

    [Test]
    public Task Append_String_ToUtf16Builder_MultiByte_TracksRuneLength()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("café");
        var text = builder.ToText();

        // Assert
        return Verify(new { text.RuneLength, result = text.ToString() });
    }

    // Cross-encoding appends

    [Test]
    public Task Append_Utf8Bytes_WithExplicitEncoding_ToUtf16Builder_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("hello"u8, TextEncoding.Utf8);

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }

    [Test]
    public Task Append_Utf16Text_ToUtf8Builder_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var text = Text.From("World");

        // Act
        builder.Append(text);

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }

    // Same-encoding TextSpan — fast path

    [Test]
    public Task Append_SameEncodingTextSpan_UsesAppendBytes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var span = new TextSpan("hello"u8, TextEncoding.Utf8);

        // Act
        builder.Append(span);

        // Assert
        return Verify(new { result = builder.ToString(), builder.RuneLength });
    }
}
