using System.Text;

namespace Glot.Tests;

public partial class OwnedTextTests
{
    // OwnedText.FromBytesAsync — with length

    [Test]
    public async Task FromBytesAsync_WithLength_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "hello";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8, bytes.Length);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // OwnedText.FromBytesAsync — length zero

    [Test]
    public async Task FromBytesAsync_LengthZero_ReturnsNull()
    {
        // Arrange
        using var stream = new MemoryStream([1, 2, 3]);

        // Act
        var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8, 0);

        // Assert
        await Assert.That(result).IsNull();
    }

    // OwnedText.FromBytesAsync — seekable (no length param)

    [Test]
    public async Task FromBytesAsync_SeekableStream_UsesStreamLength()
    {
        // Arrange
        const string expected = "world";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
    }

    // OwnedText.FromBytesAsync — non-seekable stream

    [Test]
    public async Task FromBytesAsync_NonSeekableStream_ReadsToEnd()
    {
        // Arrange
        const string expected = "hello world";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var inner = new MemoryStream(bytes);
        using var stream = new OwnedTextNonSeekableStream(inner);

        // Act
        using var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
    }

    // OwnedText.FromBytesAsync — empty stream (seekable)

    [Test]
    public async Task FromBytesAsync_EmptySeekableStream_ReturnsNull()
    {
        // Arrange
        using var stream = new MemoryStream([]);

        // Act
        var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNull();
    }

    // OwnedText.FromBytesAsync — empty non-seekable stream

    [Test]
    public async Task FromBytesAsync_EmptyNonSeekableStream_ReturnsNull()
    {
        // Arrange
        using var inner = new MemoryStream([]);
        using var stream = new OwnedTextNonSeekableStream(inner);

        // Act
        var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNull();
    }

    // OwnedText.FromBytesAsync — empty stream via length overload with non-zero length but empty data

    [Test]
    public async Task FromBytesAsync_WithLength_EmptyData_ReturnsNull()
    {
        // Arrange — stream has 0 bytes but length claims 5
        using var stream = new MemoryStream([]);

        // Act
        var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8, 5);

        // Assert
        await Assert.That(result).IsNull();
    }

    // OwnedText.FromUtf8Async — with length

    [Test]
    public async Task FromUtf8Async_WithLength_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "test";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf8Async(stream, bytes.Length);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // OwnedText.FromUtf8Async — without length

    [Test]
    public async Task FromUtf8Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "stream";
        var bytes = Encoding.UTF8.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf8Async(stream);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
    }

    // OwnedText.FromUtf16Async — with length

    [Test]
    public async Task FromUtf16Async_WithLength_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "hi";
        var bytes = Encoding.Unicode.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf16Async(stream, bytes.Length);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    // OwnedText.FromUtf16Async — without length

    [Test]
    public async Task FromUtf16Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "hi";
        var bytes = Encoding.Unicode.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf16Async(stream);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
    }

    // OwnedText.FromUtf32Async — with length

    [Test]
    public async Task FromUtf32Async_WithLength_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "ok";
        var bytes = Encoding.UTF32.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf32Async(stream, bytes.Length);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    // OwnedText.FromUtf32Async — without length

    [Test]
    public async Task FromUtf32Async_WithoutLength_ReadsToEnd()
    {
        // Arrange
        const string expected = "ok";
        var bytes = Encoding.UTF32.GetBytes(expected);
        using var stream = new MemoryStream(bytes);

        // Act
        using var result = await OwnedText.FromUtf32Async(stream);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo(expected);
    }

    // OwnedText.FromBytesAsync — seekable with partial position

    [Test]
    public async Task FromBytesAsync_SeekablePartialPosition_ReadsFromCurrentPosition()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("XXXhello");
        using var stream = new MemoryStream(bytes);
        stream.Position = 3; // skip "XXX"

        // Act
        using var result = await OwnedText.FromBytesAsync(stream, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo("hello");
    }

    // NonSeekableStream helper
    sealed class OwnedTextNonSeekableStream(Stream inner) : Stream
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
