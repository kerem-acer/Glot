namespace Glot;

/// <summary>
/// Packs a <see cref="TextEncoding"/> and a rune length into a single <see cref="int"/>.
/// Bits [1:0] = encoding, bits [31:2] = rune length.
/// </summary>
readonly struct EncodedLength
{
    const int EncodingMask = 0b11;

    readonly int _value;

    internal EncodedLength(TextEncoding encoding, int runeLength)
    {
        _value = (int)encoding | (runeLength << 2);
    }

    internal TextEncoding Encoding => (TextEncoding)(_value & EncodingMask);
    internal int RuneLength => _value >>> 2;
}
