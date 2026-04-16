using System.Buffers;
using System.Text;

namespace Glot.Tests;

public partial class TextTests
{
    // Factory — FromUtf8(ReadOnlySequence<byte>)

    [Test]
    public async Task FromUtf8_Sequence_SingleSegment_CreatesText()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var seq = new ReadOnlySequence<byte>(bytes);

        // Act
        var text = Text.FromUtf8(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task FromUtf8_Sequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "Hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        var seq = SequenceHelper.CreateMultiSegment(bytes, 3);

        // Act
        var text = Text.FromUtf8(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task FromUtf8_Sequence_MultiSegment_MultibyteChars_PreservesContent()
    {
        // Arrange
        const string expected = "caf\u00e9\U0001F389";
        var bytes = Encoding.UTF8.GetBytes(expected);
        var seq = SequenceHelper.CreateMultiSegment(bytes, 4);

        // Act
        var text = Text.FromUtf8(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task FromUtf8_Sequence_Empty_ReturnsDefault()
    {
        // Arrange
        var seq = ReadOnlySequence<byte>.Empty;

        // Act
        var text = Text.FromUtf8(seq);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromChars(ReadOnlySequence<char>)

    [Test]
    public async Task FromChars_Sequence_SingleSegment_CreatesText()
    {
        // Arrange
        var chars = "Hello".ToCharArray();
        var seq = new ReadOnlySequence<char>(chars);

        // Act
        var text = Text.FromChars(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromChars_Sequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "Hello";
        var chars = expected.ToCharArray();
        var seq = SequenceHelper.CreateMultiSegment(chars, 2);

        // Act
        var text = Text.FromChars(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task FromChars_Sequence_Empty_ReturnsDefault()
    {
        // Arrange
        var seq = ReadOnlySequence<char>.Empty;

        // Act
        var text = Text.FromChars(seq);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromUtf32(ReadOnlySequence<int>)

    [Test]
    public async Task FromUtf32_Sequence_SingleSegment_CreatesText()
    {
        // Arrange
        int[] codePoints = [0x48, 0x65, 0x6C]; // H, e, l
        var seq = new ReadOnlySequence<int>(codePoints);

        // Act
        var text = Text.FromUtf32(seq);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(3);
    }

    [Test]
    public async Task FromUtf32_Sequence_MultiSegment_CreatesText()
    {
        // Arrange
        int[] codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // H, e, l, l, o
        var seq = SequenceHelper.CreateMultiSegment(codePoints, 2);

        // Act
        var text = Text.FromUtf32(seq);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.ByteLength).IsEqualTo(20);
    }

    [Test]
    public async Task FromUtf32_Sequence_Empty_ReturnsDefault()
    {
        // Arrange
        var seq = ReadOnlySequence<int>.Empty;

        // Act
        var text = Text.FromUtf32(seq);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // Factory — FromBytes(ReadOnlySequence<byte>, TextEncoding)

    [Test]
    public async Task FromBytes_Sequence_SingleSegment_CreatesText()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var seq = new ReadOnlySequence<byte>(bytes);

        // Act
        var text = Text.FromBytes(seq, TextEncoding.Utf16);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromBytes_Sequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "Hello";
        var bytes = TestHelpers.Encode(expected, TextEncoding.Utf16);
        var seq = SequenceHelper.CreateMultiSegment(bytes, 4);

        // Act
        var text = Text.FromBytes(seq, TextEncoding.Utf16);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromBytes_Sequence_Empty_ReturnsDefault()
    {
        // Arrange
        var seq = ReadOnlySequence<byte>.Empty;

        // Act
        var text = Text.FromBytes(seq, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }
}
