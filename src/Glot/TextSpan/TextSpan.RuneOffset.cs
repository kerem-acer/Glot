namespace Glot;

public readonly ref partial struct TextSpan
{
    int RuneOffsetToByteOffset(int runeOffset)
        => RuneIndex.ToByteOffset(_bytes, Encoding, runeOffset);
}
