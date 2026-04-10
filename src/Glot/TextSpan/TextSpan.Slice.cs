namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Returns a sub-span starting at the given rune offset. O(n) to locate the byte boundary.</summary>
    public TextSpan RuneSlice(int runeOffset)
    {
        var byteOffset = RuneOffsetToByteOffset(runeOffset);
        return new TextSpan(Bytes[byteOffset..], Encoding, RuneLength - runeOffset);
    }

    /// <summary>Returns a sub-span of <paramref name="runeCount"/> runes starting at <paramref name="runeOffset"/>.</summary>
    public TextSpan RuneSlice(int runeOffset, int runeCount)
    {
        var startByte = RuneOffsetToByteOffset(runeOffset);
        var countBytes = RuneIndex.ToByteOffset(Bytes[startByte..], Encoding, runeCount);
        return new TextSpan(Bytes.Slice(startByte, countBytes), Encoding, runeCount);
    }

    /// <summary>Returns a sub-span starting at the given byte offset. Rune count is recomputed.</summary>
    public TextSpan ByteSlice(int byteOffset)
        => new(Bytes[byteOffset..], Encoding);

    /// <summary>Returns a sub-span of <paramref name="byteCount"/> bytes starting at <paramref name="byteOffset"/>. Rune count is recomputed.</summary>
    public TextSpan ByteSlice(int byteOffset, int byteCount)
        => new(Bytes.Slice(byteOffset, byteCount), Encoding);

    /// <summary>Slices by rune range. Equivalent to <c>RuneSlice(offset, count)</c>.</summary>
    public TextSpan this[Range range]
    {
        get
        {
            var (offset, count) = range.GetOffsetAndLength(RuneLength);
            return RuneSlice(offset, count);
        }
    }
}
