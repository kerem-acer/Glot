using System.Runtime.InteropServices;
using System.Text;
using static System.Text.Encoding;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Converts this span to a <see cref="string"/>. Allocates for non-string-backed data.</summary>
    public override string ToString()
    {
        if (Bytes.IsEmpty)
        {
            return string.Empty;
        }

        return Encoding switch
        {
            TextEncoding.Utf8 => UTF8.GetString(Bytes),
            TextEncoding.Utf16 => MemoryMarshal.Cast<byte, char>(Bytes).ToString(),
            TextEncoding.Utf32 => UTF32.GetString(Bytes),
            _ => throw new InvalidEncodingException(Encoding),
        };
    }

    /// <summary>Converts the content to a string with the specified format.</summary>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>Writes the text as UTF-16 chars to <paramref name="destination"/>.</summary>
    public bool TryFormat(Span<char> destination, out int charsWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Encoding == TextEncoding.Utf16)
        {
            var chars = MemoryMarshal.Cast<byte, char>(Bytes);
            if (chars.TryCopyTo(destination))
            {
                charsWritten = chars.Length;
                return true;
            }

            charsWritten = 0;
            return false;
        }

        // Bulk path: UTF-8→char and UTF-32→char via BCL TryGetChars (Polyfill on netstandard).
        var encoding = Encoding == TextEncoding.Utf8
            ? UTF8
            : UTF32;

        return encoding.TryGetChars(Bytes, destination, out charsWritten);
    }

    /// <summary>Writes the text as UTF-8 bytes to <paramref name="utf8Destination"/>.</summary>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Encoding == TextEncoding.Utf8)
        {
            if (Bytes.TryCopyTo(utf8Destination))
            {
                bytesWritten = Bytes.Length;
                return true;
            }

            bytesWritten = 0;
            return false;
        }

        // Bulk path: UTF-16→UTF-8 via BCL TryGetBytes (Polyfill on netstandard).
        if (Encoding == TextEncoding.Utf16)
        {
            return UTF8.TryGetBytes(
                MemoryMarshal.Cast<byte, char>(Bytes), utf8Destination, out bytesWritten);
        }

        // UTF-32→UTF-8: no direct bulk API, rune-by-rune.
        var offset = 0;
        var remaining = Bytes;

        while (!remaining.IsEmpty)
        {
            Rune.TryDecodeFirst(remaining, Encoding, out var rune, out var consumed);
            var runeByteCount = rune.Utf8SequenceLength;

            if (offset + runeByteCount > utf8Destination.Length)
            {
                bytesWritten = 0;
                return false;
            }

            rune.TryEncodeToUtf8(utf8Destination[offset..], out var written);
            offset += written;
            remaining = remaining[consumed..];
        }

        bytesWritten = offset;
        return true;
    }

    /// <summary>Reinterprets the underlying bytes as <see cref="char"/> elements. Zero-copy cast — no encoding validation.</summary>
    public ReadOnlySpan<char> Chars => MemoryMarshal.Cast<byte, char>(Bytes);

    /// <summary>Reinterprets the underlying bytes as <see cref="int"/> elements. Zero-copy cast — no encoding validation.</summary>
    public ReadOnlySpan<int> Ints => MemoryMarshal.Cast<byte, int>(Bytes);
}
