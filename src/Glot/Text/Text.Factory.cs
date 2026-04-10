using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Creates a <see cref="Text"/> from a <see cref="string"/>. Zero-copy — stores the string reference directly.</summary>
    public static Text From(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        var bytes = MemoryMarshal.AsBytes(value.AsSpan());
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf16);
        return new Text(value, 0, bytes.Length, TextEncoding.Utf16, runeLength);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> by copying the bytes into a new <c>byte[]</c>.</summary>
    public static Text FromUtf8(ReadOnlySpan<byte> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var bytes = value.ToArray();
        var runeLength = RuneCount.Count(value, TextEncoding.Utf8);
        return new Text(bytes, 0, bytes.Length, TextEncoding.Utf8, runeLength);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> by copying the chars into a new <c>char[]</c>.</summary>
    public static Text FromChars(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var chars = value.ToArray();
        var bytes = MemoryMarshal.AsBytes(value);
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf16);
        return new Text(chars, 0, bytes.Length, TextEncoding.Utf16, runeLength);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> by copying the code points into a new <c>int[]</c>.</summary>
    public static Text FromUtf32(ReadOnlySpan<int> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var ints = value.ToArray();
        var bytes = MemoryMarshal.AsBytes(value);
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf32);
        return new Text(ints, 0, bytes.Length, TextEncoding.Utf32, runeLength);
    }

    /// <summary>Creates a <see cref="Text"/> by copying raw bytes with the specified encoding.</summary>
    public static Text FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var bytes = value.ToArray();
        var runeLength = RuneCount.Count(value, encoding);
        return new Text(bytes, 0, bytes.Length, encoding, runeLength);
    }

    /// <summary>Implicitly converts a <see cref="string"/> to <see cref="Text"/>. Equivalent to <see cref="From"/>.</summary>
    public static implicit operator Text(string value) => From(value);
}
