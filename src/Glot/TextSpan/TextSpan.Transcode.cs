using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    int Transcode(Span<byte> destination, TextEncoding targetEncoding, out bool fullyEncoded, out int sourceBytesConsumed)
    {
        var remaining = _bytes;
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
