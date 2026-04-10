using System.Text;

namespace Glot;

/// <summary>
/// Rune-level comparison helpers for encoded byte spans.
/// </summary>
static class RunePrefix
{
    /// <summary>
    /// Checks whether <paramref name="source"/> starts with <paramref name="prefix"/>
    /// rune-by-rune, and if so, returns the byte length consumed in <paramref name="source"/>.
    /// </summary>
    public static bool TryMatch(
        ReadOnlySpan<byte> source, TextEncoding sourceEncoding,
        ReadOnlySpan<byte> prefix, TextEncoding prefixEncoding,
        out int sourceBytesConsumed)
    {
        if (prefix.IsEmpty)
        {
            sourceBytesConsumed = 0;
            return true;
        }

        var remaining = source;
        var expected = prefix;
        var totalBytes = 0;

        while (!expected.IsEmpty)
        {
            if (remaining.IsEmpty)
            {
                sourceBytesConsumed = 0;
                return false;
            }

            Rune.TryDecodeFirst(remaining, sourceEncoding, out var sourceRune, out var sourceConsumed);
            Rune.TryDecodeFirst(expected, prefixEncoding, out var prefixRune, out var prefixConsumed);

            if (sourceRune != prefixRune)
            {
                sourceBytesConsumed = 0;
                return false;
            }

            totalBytes += sourceConsumed;
            remaining = remaining[sourceConsumed..];
            expected = expected[prefixConsumed..];
        }

        sourceBytesConsumed = totalBytes;
        return true;
    }
}
