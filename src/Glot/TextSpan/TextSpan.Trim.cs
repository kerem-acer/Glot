using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Removes leading and trailing whitespace runes. Returns a non-copying sub-span.</summary>
    public TextSpan Trim() => TrimStart().TrimEnd();

    /// <summary>Removes leading whitespace runes. Returns a non-copying sub-span.</summary>
    public TextSpan TrimStart()
    {
        var remaining = _bytes;
        var trimmedRunes = 0;

        while (!remaining.IsEmpty)
        {
            Rune.TryDecodeFirst(remaining, Encoding, out var rune, out var consumed);
            if (!Rune.IsWhiteSpace(rune))
            {
                break;
            }

            trimmedRunes++;
            remaining = remaining[consumed..];
        }

        return new TextSpan(remaining, Encoding, RuneLength - trimmedRunes);
    }

    /// <summary>Removes trailing whitespace runes. Returns a non-copying sub-span.</summary>
    public TextSpan TrimEnd()
    {
        var bytes = _bytes;
        var trimmedRunes = 0;

        while (!bytes.IsEmpty)
        {
            Rune.TryDecodeLast(bytes, Encoding, out var rune, out var consumed);
            if (!Rune.IsWhiteSpace(rune))
            {
                break;
            }

            trimmedRunes++;
            bytes = bytes[..^consumed];
        }

        return new TextSpan(bytes, Encoding, RuneLength - trimmedRunes);
    }
}
