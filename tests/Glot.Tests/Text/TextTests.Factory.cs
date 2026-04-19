using System.Buffers;
using System.Collections.Immutable;
using System.Text;

namespace Glot.Tests;

public partial class TextTests
{
    // FromAscii(byte[])

    [Test]
    public async Task FromAscii_ByteArray_CreatesUtf8WithRuneCountEqualsByteCount()
    {
        // Arrange
        var bytes = "hello"u8.ToArray();

        // Act
        var text = Text.FromAscii(bytes);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(bytes.Length);
        await Assert.That(text.ByteLength).IsEqualTo(bytes.Length);
    }

    [Test]
    public async Task FromAscii_ByteArray_Empty_ReturnsEmpty()
    {
        // Act
        var text = Text.FromAscii([]);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromAscii(string)

    [Test]
    public async Task FromAscii_String_CreatesUtf16WithRuneCountEqualsLength()
    {
        // Arrange
        const string value = "hello";

        // Act
        var text = Text.FromAscii(value);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(value.Length);
        await Assert.That(text.ByteLength).IsEqualTo(value.Length * 2);
    }

    [Test]
    public async Task FromAscii_String_Null_ReturnsEmpty()
    {
        // Act
        var text = Text.FromAscii((string)null!);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // From(string, countRunes: false)

    [Test]
    public async Task From_String_CountRunesFalse_DeferredRuneCountStillWorks()
    {
        // Arrange
        const string value = "hello";

        // Act
        var text = Text.From(value, countRunes: false);
        var runeLength = text.RuneLength;

        // Assert
        await Assert.That(runeLength).IsEqualTo(value.Length);
        await Assert.That(text.ToString()).IsEqualTo(value);
    }

    // FromUtf8(byte[], countRunes: false)

    [Test]
    public async Task FromUtf8_ByteArray_CountRunesFalse_DeferredRuneCountStillWorks()
    {
        // Arrange
        var bytes = "hello"u8.ToArray();

        // Act
        var text = Text.FromUtf8(bytes, countRunes: false);
        var runeLength = text.RuneLength;

        // Assert
        await Assert.That(runeLength).IsEqualTo(5);
        await Assert.That(text.ToString()).IsEqualTo("hello");
    }

    // FromUtf8(ArraySegment<byte>)

    [Test]
    public async Task FromUtf8_ArraySegment_NonEmpty_CreatesText()
    {
        // Arrange
        const string expected = "ello";
        var bytes = "hello"u8.ToArray();
        var segment = new ArraySegment<byte>(bytes, 1, 4);

        // Act
        var text = Text.FromUtf8(segment);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(4);
    }

    [Test]
    public async Task FromUtf8_ArraySegment_NullArray_ReturnsEmpty()
    {
        // Arrange
        var segment = default(ArraySegment<byte>);

        // Act
        var text = Text.FromUtf8(segment);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromUtf8(ReadOnlyMemory<byte>) — array-backed

    [Test]
    public async Task FromUtf8_ReadOnlyMemory_ArrayBacked_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        ReadOnlyMemory<byte> memory = bytes;

        // Act
        var text = Text.FromUtf8(memory);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // FromUtf8(ImmutableArray<byte>)

    [Test]
    public async Task FromUtf8_ImmutableArray_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        var immutable = ImmutableArray.Create(bytes);

        // Act
        var text = Text.FromUtf8(immutable);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task FromUtf8_ImmutableArray_Empty_ReturnsEmpty()
    {
        // Act
        var text = Text.FromUtf8(ImmutableArray<byte>.Empty);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromUtf8(ReadOnlySequence<byte>) — multi-segment

    [Test]
    public async Task FromUtf8_ReadOnlySequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var first = "hel"u8.ToArray();
        var second = "lo"u8.ToArray();
        var seq = CreateMultiSegmentBytes(first, second);

        // Act
        var text = Text.FromUtf8(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // FromChars(char[])

    [Test]
    public async Task FromChars_CharArray_CreatesUtf16()
    {
        // Arrange
        const string expected = "hello";
        var chars = expected.ToCharArray();

        // Act
        var text = Text.FromChars(chars);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // FromChars(ArraySegment<char>)

    [Test]
    public async Task FromChars_ArraySegment_NonEmpty_CreatesText()
    {
        // Arrange
        const string expected = "ell";
        var chars = "hello".ToCharArray();
        var segment = new ArraySegment<char>(chars, 1, 3);

        // Act
        var text = Text.FromChars(segment);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromChars_ArraySegment_NullArray_ReturnsEmpty()
    {
        // Arrange
        var segment = default(ArraySegment<char>);

        // Act
        var text = Text.FromChars(segment);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromChars(ReadOnlyMemory<char>) — string-backed

    [Test]
    public async Task FromChars_ReadOnlyMemory_StringBacked_CreatesStringBackedText()
    {
        // Arrange
        const string expected = "hello";
        ReadOnlyMemory<char> memory = expected.AsMemory();

        // Act
        var text = Text.FromChars(memory);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // FromChars(ReadOnlyMemory<char>) — char[]-backed

    [Test]
    public async Task FromChars_ReadOnlyMemory_CharArrayBacked_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var chars = expected.ToCharArray();
        ReadOnlyMemory<char> memory = chars;

        // Act
        var text = Text.FromChars(memory);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // FromChars(ReadOnlySequence<char>) — multi-segment

    [Test]
    public async Task FromChars_ReadOnlySequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var first = "hel".ToCharArray();
        var second = "lo".ToCharArray();
        var seq = CreateMultiSegmentChars(first, second);

        // Act
        var text = Text.FromChars(seq);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // FromUtf32(int[])

    [Test]
    public async Task FromUtf32_IntArray_CreatesUtf32()
    {
        // Arrange
        int[] codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello

        // Act
        var text = Text.FromUtf32(codePoints);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.ByteLength).IsEqualTo(20);
    }

    // FromUtf32(ArraySegment<int>)

    [Test]
    public async Task FromUtf32_ArraySegment_NonEmpty_CreatesText()
    {
        // Arrange
        int[] codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello
        var segment = new ArraySegment<int>(codePoints, 1, 3); // e, l, l

        // Act
        var text = Text.FromUtf32(segment);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(3);
        await Assert.That(text.ByteLength).IsEqualTo(12);
    }

    [Test]
    public async Task FromUtf32_ArraySegment_NullArray_ReturnsEmpty()
    {
        // Arrange
        var segment = default(ArraySegment<int>);

        // Act
        var text = Text.FromUtf32(segment);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromUtf32(ReadOnlyMemory<int>) — array-backed

    [Test]
    public async Task FromUtf32_ReadOnlyMemory_ArrayBacked_CreatesText()
    {
        // Arrange
        int[] codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello
        ReadOnlyMemory<int> memory = codePoints;

        // Act
        var text = Text.FromUtf32(memory);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // FromUtf32(ReadOnlySequence<int>) — multi-segment

    [Test]
    public async Task FromUtf32_ReadOnlySequence_MultiSegment_CreatesText()
    {
        // Arrange
        int[] first = [0x48, 0x65]; // H, e
        int[] second = [0x6C, 0x6C, 0x6F]; // l, l, o
        var seq = CreateMultiSegmentInts(first, second);

        // Act
        var text = Text.FromUtf32(seq);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(text.RuneLength).IsEqualTo(5);
        await Assert.That(text.ByteLength).IsEqualTo(20);
    }

    // FromBytes(ArraySegment<byte>)

    [Test]
    public async Task FromBytes_ArraySegment_NonEmpty_CreatesText()
    {
        // Arrange
        const string expected = "ello";
        var bytes = Encoding.UTF8.GetBytes("hello");
        var segment = new ArraySegment<byte>(bytes, 1, 4);

        // Act
        var text = Text.FromBytes(segment, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task FromBytes_ArraySegment_NullArray_ReturnsEmpty()
    {
        // Arrange
        var segment = default(ArraySegment<byte>);

        // Act
        var text = Text.FromBytes(segment, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromBytes(ReadOnlyMemory<byte>) — array-backed

    [Test]
    public async Task FromBytes_ReadOnlyMemory_ArrayBacked_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        ReadOnlyMemory<byte> memory = bytes;

        // Act
        var text = Text.FromBytes(memory, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // FromBytes(ReadOnlySequence<byte>) — multi-segment

    [Test]
    public async Task FromBytes_ReadOnlySequence_MultiSegment_CreatesText()
    {
        // Arrange
        const string expected = "hello";
        var first = "hel"u8.ToArray();
        var second = "lo"u8.ToArray();
        var seq = CreateMultiSegmentBytes(first, second);

        // Act
        var text = Text.FromBytes(seq, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    // Multi-segment helpers

    static ReadOnlySequence<byte> CreateMultiSegmentBytes(byte[] first, byte[] second)
    {
        var firstSegment = new BufferSegment<byte>(first);
        var lastSegment = firstSegment.Append(second);
        return new ReadOnlySequence<byte>(firstSegment, 0, lastSegment, second.Length);
    }

    static ReadOnlySequence<char> CreateMultiSegmentChars(char[] first, char[] second)
    {
        var firstSegment = new BufferSegment<char>(first);
        var lastSegment = firstSegment.Append(second);
        return new ReadOnlySequence<char>(firstSegment, 0, lastSegment, second.Length);
    }

    static ReadOnlySequence<int> CreateMultiSegmentInts(int[] first, int[] second)
    {
        var firstSegment = new BufferSegment<int>(first);
        var lastSegment = firstSegment.Append(second);
        return new ReadOnlySequence<int>(firstSegment, 0, lastSegment, second.Length);
    }

    sealed class BufferSegment<T> : ReadOnlySequenceSegment<T>
    {
        public BufferSegment(ReadOnlyMemory<T> memory) => Memory = memory;

        public BufferSegment<T> Append(ReadOnlyMemory<T> nextMemory)
        {
            var next = new BufferSegment<T>(nextMemory) { RunningIndex = RunningIndex + Memory.Length };
            Next = next;
            return next;
        }
    }
}
