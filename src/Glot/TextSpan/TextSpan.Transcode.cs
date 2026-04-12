#if NET6_0_OR_GREATER
using System.Buffers;
using System.Text.Unicode;
#endif
using System.Runtime.InteropServices;
using System.Text;
using static System.Text.Encoding;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Encodes this text as UTF-16 chars into <paramref name="destination"/>.</summary>
    /// <returns>The number of chars written.</returns>
    public int EncodeToUtf16(Span<char> destination)
    {
        switch (Encoding)
        {
            case TextEncoding.Utf16:
                {
                    var chars = Chars;
                    chars.CopyTo(destination);
                    return chars.Length;
                }

            // Bulk path: BCL Encoding handles UTF-8→UTF-16 and UTF-32→UTF-16 with
            // SIMD acceleration on .NET 6+ (via Polyfill span overloads on netstandard).
            case TextEncoding.Utf8:
                return UTF8.GetChars(Bytes, destination);

            case TextEncoding.Utf32:
                return UTF32.GetChars(Bytes, destination);

            default:
                throw new InvalidEncodingException(Encoding);
        }
    }

    /// <summary>Encodes this text as UTF-8 bytes into <paramref name="destination"/>.</summary>
    /// <returns>The number of bytes written.</returns>
    public int EncodeToUtf8(Span<byte> destination)
    {
        if (Encoding == TextEncoding.Utf8)
        {
            Bytes.CopyTo(destination);
            return Bytes.Length;
        }

        // Bulk path: UTF-16→UTF-8 via BCL (SIMD on .NET 6+).
        if (Encoding == TextEncoding.Utf16)
        {
            return UTF8.GetBytes(
                MemoryMarshal.Cast<byte, char>(Bytes),
                destination);
        }

        // UTF-32→UTF-8: no direct bulk API, rune-by-rune.
        var offset = 0;
        foreach (var rune in EnumerateRunes())
        {
            offset += rune.EncodeToUtf8(destination[offset..]);
        }

        return offset;
    }

    int Transcode(Span<byte> destination,
        TextEncoding targetEncoding,
        out bool fullyEncoded,
        out int sourceBytesConsumed)
    {
#if NET6_0_OR_GREATER
        // Bulk path for UTF-8↔UTF-16 via System.Text.Unicode.Utf8 (SIMD-accelerated).
        // Handles partial writes and reports exact source consumption.
        if (Encoding == TextEncoding.Utf16 && targetEncoding == TextEncoding.Utf8)
        {
            var status = Utf8.FromUtf16(
                Chars,
                destination,
                out var charsRead,
                out var bytesWritten);

            fullyEncoded = status == OperationStatus.Done;
            sourceBytesConsumed = charsRead * 2;
            return bytesWritten;
        }

        if (Encoding == TextEncoding.Utf8 && targetEncoding == TextEncoding.Utf16)
        {
            var charDest = MemoryMarshal.Cast<byte, char>(destination);
            var status = Utf8.ToUtf16(
                Bytes,
                charDest,
                out var bytesRead,
                out var charsWritten);

            fullyEncoded = status == OperationStatus.Done;
            sourceBytesConsumed = bytesRead;
            return charsWritten * 2;
        }
#endif

        // Scalar fallback for UTF-32 paths and netstandard targets.
        var remaining = Bytes;
        var written = 0;
        sourceBytesConsumed = 0;

        while (!remaining.IsEmpty)
        {
            Rune.TryDecodeFirst(
                remaining,
                Encoding,
                out var rune,
                out var consumed);

            var runeByteCount = rune.GetByteCount(targetEncoding);

            if (written + runeByteCount > destination.Length)
            {
                break;
            }

            rune.EncodeTo(destination[written..], targetEncoding);
            written += runeByteCount;
            sourceBytesConsumed += consumed;
            remaining = remaining[consumed..];
        }

        fullyEncoded = remaining.IsEmpty;
        return written;
    }
}
