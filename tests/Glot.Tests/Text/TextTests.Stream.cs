using System.Text;

namespace Glot.Tests;

public partial class TextTests
{
    // CopyToAsync — empty

    [Test]
    public async Task CopyToAsync_Empty_WritesNothing()
    {
        // Arrange
        var text = Text.Empty;
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        await Assert.That(stream.Length).IsEqualTo(0);
    }

    // CopyToAsync — UTF-8 (zero-copy path)

    [Test]
    public async Task CopyToAsync_Utf8_WritesUtf8Bytes()
    {
        // Arrange
        const string content = "hello";
        var expectedBytes = Encoding.UTF8.GetBytes(content);
        var text = Text.FromUtf8("hello"u8);
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        var written = stream.ToArray();
        await Assert.That(written.Length).IsEqualTo(expectedBytes.Length);
        await Assert.That(written.SequenceEqual(expectedBytes)).IsTrue();
    }

    // CopyToAsync — UTF-16 (transcode path)

    [Test]
    public async Task CopyToAsync_Utf16_TranscodesToUtf8()
    {
        // Arrange
        const string content = "hello";
        var expectedUtf8 = Encoding.UTF8.GetBytes(content);
        var text = Text.From(content);
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        var written = stream.ToArray();
        await Assert.That(written.SequenceEqual(expectedUtf8)).IsTrue();
    }

    // CopyToAsync — UTF-32 (transcode path)

    [Test]
    public async Task CopyToAsync_Utf32_TranscodesToUtf8()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello
        var expectedUtf8 = Encoding.UTF8.GetBytes("Hello");
        var text = Text.FromUtf32(cp);
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        var written = stream.ToArray();
        await Assert.That(written.SequenceEqual(expectedUtf8)).IsTrue();
    }

    // CopyToAsync — UTF-8 with multi-byte characters

    [Test]
    public async Task CopyToAsync_Utf8MultiByte_WritesCorrectBytes()
    {
        // Arrange
        var expectedBytes = "caf\u00e9"u8.ToArray();
        var text = Text.FromUtf8("caf\u00e9"u8);
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        var written = stream.ToArray();
        await Assert.That(written.SequenceEqual(expectedBytes)).IsTrue();
    }

    // FromBytesAsync — with length

    [Test]
    public async Task FromBytesAsync_WithLength_ReadsExactBytes()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8, bytes.Length);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // FromBytesAsync — with length zero

    [Test]
    public async Task FromBytesAsync_LengthZero_ReturnsDefault()
    {
        // Arrange
        using var stream = new MemoryStream([1, 2, 3]);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8, 0);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromBytesAsync — seekable stream (no length param)

    [Test]
    public async Task FromBytesAsync_SeekableStream_UsesStreamLength()
    {
        // Arrange
        const string expected = "world";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
    }

    // FromBytesAsync — non-seekable stream

    [Test]
    public async Task FromBytesAsync_NonSeekableStream_ReadsToEnd()
    {
        // Arrange
        const string expected = "hello world";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var inner = new MemoryStream(bytes);
        using var stream = new NonSeekableStream(inner);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
    }

    // FromBytesAsync — empty stream

    [Test]
    public async Task FromBytesAsync_EmptyStream_ReturnsEmpty()
    {
        // Arrange
        using var stream = new MemoryStream([]);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromBytesAsync — empty non-seekable stream

    [Test]
    public async Task FromBytesAsync_EmptyNonSeekableStream_ReturnsEmpty()
    {
        // Arrange
        using var inner = new MemoryStream([]);
        using var stream = new NonSeekableStream(inner);

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    // FromUtf8Async — with length

    [Test]
    public async Task FromUtf8Async_WithLength_ReadsUtf8()
    {
        // Arrange
        const string expected = "test";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf8Async(stream, bytes.Length);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // FromUtf8Async — without length

    [Test]
    public async Task FromUtf8Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "stream";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf8Async(stream);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
    }

    // FromUtf16Async — with length

    [Test]
    public async Task FromUtf16Async_WithLength_ReadsUtf16()
    {
        // Arrange
        const string expected = "hi";
        var bytes = Encoding.Unicode.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf16Async(stream, bytes.Length);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // FromUtf16Async — without length

    [Test]
    public async Task FromUtf16Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "hi";
        var bytes = Encoding.Unicode.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf16Async(stream);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // FromUtf32Async — with length

    [Test]
    public async Task FromUtf32Async_WithLength_ReadsUtf32()
    {
        // Arrange
        const string expected = "ok";
        var bytes = Encoding.UTF32.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf32Async(stream, bytes.Length);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    // FromUtf32Async — without length

    [Test]
    public async Task FromUtf32Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "ok";
        var bytes = Encoding.UTF32.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        var text = await Text.FromUtf32Async(stream);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(expected);
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    // FromBytesAsync — seekable with partial position

    [Test]
    public async Task FromBytesAsync_SeekablePartialPosition_ReadsFromCurrentPosition()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("XXXhello");
        using var stream = new MemoryStream(bytes);
        stream.Position = 3; // skip "XXX"

        // Act
        var text = await Text.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello");
    }

    // CopyToAsync — UTF-16 char[]-backed via FromChars

    [Test]
    public async Task CopyToAsync_Utf16CharBacked_TranscodesToUtf8()
    {
        // Arrange
        const string content = "world";
        var expectedUtf8 = Encoding.UTF8.GetBytes(content);
        var text = Text.FromChars(content.AsSpan());
        using var stream = new MemoryStream();

        // Act
        await text.CopyToAsync(stream);

        // Assert
        var written = stream.ToArray();
        await Assert.That(written.SequenceEqual(expectedUtf8)).IsTrue();
    }

    // NonSeekableStream helper
    sealed class NonSeekableStream(Stream inner) : Stream
    {
        public override bool CanSeek => false;
        public override bool CanRead => inner.CanRead;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override void Flush() => inner.Flush();
        public override int Read(byte[] buffer, int offset, int count) => inner.Read(buffer, offset, count);

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken ct)
            => inner.ReadAsync(buffer, offset, count, ct);

        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken ct = default)
            => inner.ReadAsync(buffer, ct);

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
