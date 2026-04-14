using System.Buffers;

namespace Glot;

public sealed partial class OwnedText
{
    /// <summary>
    /// Reads exactly <paramref name="length"/> bytes into a pooled buffer and creates an <see cref="OwnedText"/>.
    /// Zero allocations — the buffer is rented from <see cref="ArrayPool{T}"/>.
    /// Caller must dispose to return the buffer to the pool.
    /// </summary>
    public static async Task<OwnedText?> FromBytesAsync(
        Stream stream, TextEncoding encoding, int length, CancellationToken cancellationToken = default)
    {
        if (length == 0)
        {
            return null;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(length);
        var transferred = false;
        try
        {
            var totalRead = await Text.ReadToBufferAsync(stream, buffer, length, cancellationToken);

            if (totalRead == 0)
            {
                return null;
            }

            var result = Create(buffer, totalRead, encoding);
            transferred = true;
            return result;
        }
        finally
        {
            if (!transferred)
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    /// <summary>
    /// Reads the stream to the end into pooled buffers and creates an <see cref="OwnedText"/>.
    /// Zero allocations — the buffer is rented from <see cref="ArrayPool{T}"/>.
    /// Caller must dispose to return the buffer to the pool.
    /// </summary>
    public static async Task<OwnedText?> FromBytesAsync(
        Stream stream, TextEncoding encoding, CancellationToken cancellationToken = default)
    {
        if (stream.CanSeek)
        {
            return await FromBytesAsync(stream, encoding, (int)(stream.Length - stream.Position), cancellationToken);
        }

        var (buffer, totalRead) = await Text.ReadToEndAsync(stream, cancellationToken);
        var transferred = false;
        try
        {
            if (totalRead == 0)
            {
                return null;
            }

            var result = Create(buffer, totalRead, encoding);
            transferred = true;
            return result;
        }
        finally
        {
            if (!transferred)
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    /// <summary>Creates a pooled UTF-8 <see cref="OwnedText"/> from a stream with known length.</summary>
    public static Task<OwnedText?> FromUtf8Async(Stream stream, int length, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf8, length, cancellationToken);

    /// <summary>Creates a pooled UTF-8 <see cref="OwnedText"/> by reading the stream to the end.</summary>
    public static Task<OwnedText?> FromUtf8Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf8, cancellationToken);

    /// <summary>Creates a pooled UTF-16 <see cref="OwnedText"/> from a stream with known byte length.</summary>
    public static Task<OwnedText?> FromUtf16Async(Stream stream, int byteLength, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf16, byteLength, cancellationToken);

    /// <summary>Creates a pooled UTF-16 <see cref="OwnedText"/> by reading the stream to the end.</summary>
    public static Task<OwnedText?> FromUtf16Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf16, cancellationToken);

    /// <summary>Creates a pooled UTF-32 <see cref="OwnedText"/> from a stream with known byte length.</summary>
    public static Task<OwnedText?> FromUtf32Async(Stream stream, int byteLength, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf32, byteLength, cancellationToken);

    /// <summary>Creates a pooled UTF-32 <see cref="OwnedText"/> by reading the stream to the end.</summary>
    public static Task<OwnedText?> FromUtf32Async(Stream stream, CancellationToken cancellationToken = default)
        => FromBytesAsync(stream, TextEncoding.Utf32, cancellationToken);
}
