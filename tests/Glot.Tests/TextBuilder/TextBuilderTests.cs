using System.Text;

namespace Glot.Tests;

public partial class TextBuilderTests
{
    [Test]
    public async Task DefaultEncoding_IsUtf8()
    {
        // Arrange & Act
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Assert
        await Assert.That(builder.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(builder.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Append_String_TranscodesToUtf8()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello");
        await Assert.That(builder.RuneLength).IsEqualTo(5);
        await Assert.That(builder.ByteLength).IsEqualTo(5);
    }

    [Test]
    public async Task Append_MultipleStrings_Concatenates()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");
        builder.Append(" ");
        builder.Append("World");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello World");
        await Assert.That(builder.RuneLength).IsEqualTo(11);
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
    public async Task Append_Utf8Bytes_SameEncoding_DirectCopy()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("café"u8);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("café");
        await Assert.That(builder.RuneLength).IsEqualTo(4);
        await Assert.That(builder.ByteLength).IsEqualTo(5);
    }

    [Test]
    public async Task Append_Utf8Bytes_ToUtf16Builder_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf16);

        // Act
        builder.Append("Hello"u8);

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello");
        await Assert.That(builder.RuneLength).IsEqualTo(5);
        await Assert.That(builder.ByteLength).IsEqualTo(10);
    }

    [Test]
    public async Task Append_TextSpan_CrossEncoding_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        var utf16Bytes = TestHelpers.Encode("café", TextEncoding.Utf16);

        // Act
        builder.Append(new TextSpan(utf16Bytes, TextEncoding.Utf16));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("café");
    }

    [Test]
    public async Task Append_Text_Works()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append(Text.From("Hello"));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task AppendRune_Appended()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.AppendRune(new Rune('A'));
        builder.AppendRune(new Rune(0x1F389));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("A🎉");
        await Assert.That(builder.RuneLength).IsEqualTo(2);
    }

    [Test]
    public async Task AppendLine_AppendsNewline()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello");
        builder.AppendLine();
        builder.Append("World");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello\nWorld");
    }

    [Test]
    public async Task Append_CharSpan_Transcodes()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("café".AsSpan());

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("café");
    }

    [Test]
    public async Task ToText_ReturnsCorrectText()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello World");

        // Act
        var text = builder.ToText();

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello World");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(11);
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
    public async Task ToOwnedText_TransfersBuffer()
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

    [Test]
    public async Task ToOwnedText_BuilderCanContinue()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("First");
        using var first = builder.ToOwnedText();
        builder.Append("Second");
        using var second = builder.ToOwnedText();

        // Assert
        await Assert.That(first.Text.ToString()).IsEqualTo("First");
        await Assert.That(second.Text.ToString()).IsEqualTo("Second");
    }

    [Test]
    public async Task AsSpan_ReturnsCurrentContent()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello");

        // Act
        var eq = builder.AsSpan().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task Clear_ResetsContent()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("Hello");

        // Act
        builder.Clear();

        // Assert
        await Assert.That(builder.IsEmpty).IsTrue();
        await Assert.That(builder.ByteLength).IsEqualTo(0);
        await Assert.That(builder.RuneLength).IsEqualTo(0);
    }

    [Test]
    public async Task Clear_CanAppendAgain()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);
        builder.Append("First");

        // Act
        builder.Clear();
        builder.Append("Second");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Second");
    }

    [Test]
    public async Task Growth_LargeAppend_GrowsBuffer()
    {
        // Arrange
        using var builder = new TextBuilder(4, TextEncoding.Utf8);

        // Act
        builder.Append("Hello World, this is a string much longer than 4 bytes");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello World, this is a string much longer than 4 bytes");
    }

    [Test]
    public async Task Growth_ManySmallAppends_GrowsCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(4, TextEncoding.Utf8);

        // Act
        for (var i = 0; i < 1000; i++)
        {
            builder.AppendRune(new Rune('A'));
        }

        // Assert
        await Assert.That(builder.RuneLength).IsEqualTo(1000);
        await Assert.That(builder.ByteLength).IsEqualTo(1000);
    }

    [Test]
    public async Task MixedAppends_AllTranscodeCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf8);

        // Act
        builder.Append("Hello ");
        builder.Append("World"u8);
        builder.AppendRune(new Rune('!'));

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hello World!");
        await Assert.That(builder.RuneLength).IsEqualTo(12);
    }

    [Test]
    public async Task Utf32Builder_AppendsCorrectly()
    {
        // Arrange
        using var builder = new TextBuilder(TextEncoding.Utf32);

        // Act
        builder.Append("Hi!");

        // Assert
        await Assert.That(builder.ToString()).IsEqualTo("Hi!");
        await Assert.That(builder.ByteLength).IsEqualTo(12);
        await Assert.That(builder.RuneLength).IsEqualTo(3);
    }

    // UTF-16 builder — string append tracks RuneLength

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
    public async Task Append_Utf8Bytes_WithExplicitEncoding_ToUtf16Builder_Transcodes()
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

    // Same-encoding TextSpan — fast path

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
