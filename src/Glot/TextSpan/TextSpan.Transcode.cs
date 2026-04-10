using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Encodes this text as UTF-16 chars into <paramref name="destination"/>.</summary>
    /// <returns>The number of chars written.</returns>
    public int EncodeToUtf16(Span<char> destination)
    {
        if (Encoding == TextEncoding.Utf16)
        {
            var chars = Chars;
            chars.CopyTo(destination);
            return chars.Length;
        }

        var offset = 0;
        foreach (var rune in EnumerateRunes())
        {
            offset += rune.EncodeToUtf16(destination[offset..]);
        }

        return offset;
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

        var offset = 0;
        foreach (var rune in EnumerateRunes())
        {
            offset += rune.EncodeToUtf8(destination[offset..]);
        }

        return offset;
    }

    int Transcode(Span<byte> destination, TextEncoding targetEncoding, out bool fullyEncoded, out int sourceBytesConsumed)
    {
        var remaining = Bytes;
        var written = 0;
        sourceBytesConsumed = 0;

        while (!remaining.IsEmpty)
        {
            Rune.TryDecodeFirst(remaining, Encoding, out var rune, out var consumed);
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
