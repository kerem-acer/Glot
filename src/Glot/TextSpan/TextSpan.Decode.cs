using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Decodes the first rune from this span. Returns <c>false</c> if the span is empty.</summary>
    /// <param name="rune">The decoded rune, or <see cref="Rune.ReplacementChar"/> for malformed data.</param>
    /// <param name="bytesConsumed">The number of bytes consumed from the start of the span.</param>
    public bool DecodeFirstRune(out Rune rune, out int bytesConsumed)
        => Rune.TryDecodeFirst(_bytes, Encoding, out rune, out bytesConsumed);

    /// <summary>Decodes the last rune from this span. Returns <c>false</c> if the span is empty.</summary>
    /// <param name="rune">The decoded rune, or <see cref="Rune.ReplacementChar"/> for malformed data.</param>
    /// <param name="bytesConsumed">The number of bytes consumed from the end of the span.</param>
    public bool DecodeLastRune(out Rune rune, out int bytesConsumed)
        => Rune.TryDecodeLast(_bytes, Encoding, out rune, out bytesConsumed);
}
