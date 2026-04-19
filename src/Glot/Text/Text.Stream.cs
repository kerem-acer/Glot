using System.Buffers;
#if NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif

namespace Glot;

public readonly partial struct Text
{
    /// <summary>
    /// Writes this text as UTF-8 bytes to <paramref name="destination"/>.
    /// Zero-copy when UTF-8 backed; transcodes through a pooled buffer otherwise.
    /// </summary>
    public async Task CopyToAsync(Stream destination, CancellationToken cancellationToken = default)
    {
        if (IsEmpty)
        {
            return;
        }

        if (TryGetUtf8Memory(out var memory))
        {
#if NETSTANDARD2_0
            MemoryMarshal.TryGetArray(memory, out var segment);
            await destination.WriteAsync(segment.Array, segment.Offset, segment.Count, cancellationToken);
#else
            await destination.WriteAsync(memory, cancellationToken);
#endif
            return;
        }

        // UTF-8 path returned early via TryGetUtf8Memory above.
        // UTF-16: each 2-byte code unit → max 3 UTF-8 bytes. UTF-32: each 4-byte rune → max 4 UTF-8 bytes.
        var maxBytes = Encoding == TextEncoding.Utf16 ? ByteLength / 2 * 3 : ByteLength;
        var buffer = ArrayPool<byte>.Shared.Rent(maxBytes);
        try
        {
            var written = EncodeToUtf8(buffer);
#if NETSTANDARD2_0
            await destination.WriteAsync(buffer, 0, written, cancellationToken);
#else
            await destination.WriteAsync(buffer.AsMemory(0, written), cancellationToken);
#endif
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// Reads exactly <paramref name="length"/> bytes and creates a <see cref="Text"/> with the specified encoding.
    /// One allocation for the <c>byte[]</c>; the <see cref="Text"/> wraps it zero-copy.
    /// </summary>
    public static async Task<Text> FromBytesAsync(
        Stream stream, TextEncoding encoding, int length, CancellationToken cancellationToken = default)
    {
        if (length == 0)
        {
            return default;
        }

        var buffer = new byte[length];
        var totalRead = await ReadToBufferAsync(stream, buffer, length, cancellationToken);

        if (totalRead == 0)
        {
            return default;
        }

        return FromBytes(new ArraySegment<byte>(buffer, 0, totalRead), encoding);
    }

    /// <summary>
    /// Reads the stream to the end and creates a <see cref="Text"/> with the specified encoding.
    /// Uses pooled buffers during reading; one final allocation for the exact-size <c>byte[]</c>.
    /// Prefer the length overload when the size is known.
    /// </summary>
    public static async Task<Text> FromBytesAsync(
        Stream stream, TextEncoding encoding, CancellationToken cancellationToken = default)
    {
        if (stream.CanSeek)
        {
            return await FromBytesAsync(stream, encoding, (int)(stream.Length - stream.Position), cancellationToken);
        }

        var (buffer, totalRead) = await ReadToEndAsync(stream, cancellationToken);
        try
        {
            if (totalRead == 0)
            {
                return default;
            }

            var result = new byte[totalRead];
            buffer.AsSpan(0, totalRead).CopyTo(result);
            return FromBytes(result, encoding);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> from a stream with known length.</summary>
    public static Task<Text> FromUtf8Async(Stream stream, int length, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf8, length, cancellationToken);

    /// <summary>Creates a UTF-8 <see cref="Text"/> by reading the stream to the end.</summary>
    public static Task<Text> FromUtf8Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf8, cancellationToken);

    /// <summary>Creates a UTF-16 <see cref="Text"/> from a stream with known byte length.</summary>
    public static Task<Text> FromUtf16Async(Stream stream, int byteLength, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf16, byteLength, cancellationToken);

    /// <summary>Creates a UTF-16 <see cref="Text"/> by reading the stream to the end.</summary>
    public static Task<Text> FromUtf16Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf16, cancellationToken);

    /// <summary>Creates a UTF-32 <see cref="Text"/> from a stream with known byte length.</summary>
    public static Task<Text> FromUtf32Async(Stream stream, int byteLength, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf32, byteLength, cancellationToken);

    /// <summary>Creates a UTF-32 <see cref="Text"/> by reading the stream to the end.</summary>
    public static Task<Text> FromUtf32Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf32, cancellationToken);

    // Shared read helpers

    internal static async Task<int> ReadToBufferAsync(
        Stream stream, byte[] buffer, int length, CancellationToken cancellationToken)
    {
        var totalRead = 0;
        while (totalRead < length)
        {
#if NETSTANDARD2_0
            var read = await stream.ReadAsync(buffer, totalRead, length - totalRead, cancellationToken);
#else
            var read = await stream.ReadAsync(buffer.AsMemory(totalRead, length - totalRead), cancellationToken);
#endif
            if (read == 0)
            {
                break;
            }

            totalRead += read;
        }

        return totalRead;
    }

    internal static async Task<(byte[] Buffer, int TotalRead)> ReadToEndAsync(
        Stream stream, CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(4096);
        var totalRead = 0;
        try
        {
            while (true)
            {
                if (totalRead == buffer.Length)
                {
                    var larger = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
                    buffer.AsSpan(0, totalRead).CopyTo(larger);
                    ArrayPool<byte>.Shared.Return(buffer);
                    buffer = larger;
                }

#if NETSTANDARD2_0
                var read = await stream.ReadAsync(buffer, totalRead, buffer.Length - totalRead, cancellationToken);
#else
                var read = await stream.ReadAsync(buffer.AsMemory(totalRead, buffer.Length - totalRead), cancellationToken);
#endif
                if (read == 0)
                {
                    break;
                }

                totalRead += read;
            }

            return (buffer, totalRead);
        }
        catch
        {
            ArrayPool<byte>.Shared.Return(buffer);
            throw;
        }
    }
}
