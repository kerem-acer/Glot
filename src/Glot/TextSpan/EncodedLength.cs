namespace Glot;

/// <summary>
/// Packs a <see cref="TextEncoding"/> and a length into a single <see cref="int"/>.
/// Bits [1:0] = encoding, bits [31:2] = length (rune length or byte length depending on context).
/// </summary>
readonly struct EncodedLength
{
    const int EncodingMask = 0b11;

    readonly int _value;

    internal EncodedLength(TextEncoding encoding, int length)
    {
        _value = (int)encoding | (length << 2);
    }

    internal TextEncoding Encoding => (TextEncoding)(_value & EncodingMask);
    internal int RuneLength => _value >>> 2;
    internal int Length => _value >>> 2;
}
