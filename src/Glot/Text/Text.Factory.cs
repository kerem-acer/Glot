using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Glot;

/// <remarks>
/// <para>
/// Factory methods do not validate that input is well-formed Unicode. Callers are responsible
/// for ensuring well-formedness; malformed input will decode with U+FFFD replacement characters
/// during enumeration, search, hashing, and comparison. This is a deliberate design choice —
/// validation would add an O(n) cost at construction for every value.
/// </para>
/// </remarks>
public readonly partial struct Text
{
    /// <summary>Creates a <see cref="Text"/> from a <see cref="string"/>. Zero-copy — stores the string reference directly.</summary>
    public static Text From(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        return new Text(
            value,
            0,
            MemoryMarshal.AsBytes(value.AsSpan()),
            TextEncoding.Utf16);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> by copying the bytes into a new <c>byte[]</c>.</summary>
    public static Text FromUtf8(ReadOnlySpan<byte> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        return new Text(
            value.ToArray(),
            0,
            value,
            TextEncoding.Utf8);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> by copying the chars into a new <c>char[]</c>.</summary>
    public static Text FromChars(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        return new Text(
            value.ToArray(),
            0,
            MemoryMarshal.AsBytes(value),
            TextEncoding.Utf16);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> by copying the code points into a new <c>int[]</c>.</summary>
    public static Text FromUtf32(ReadOnlySpan<int> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        return new Text(
            value.ToArray(),
            0,
            MemoryMarshal.AsBytes(value),
            TextEncoding.Utf32);
    }

    /// <summary>Creates a <see cref="Text"/> by copying raw bytes with the specified encoding.</summary>
    public static Text FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        return new Text(
            value.ToArray(),
            0,
            value,
            encoding);
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a UTF-8 <see cref="Text"/> from an interpolated string. No intermediate string allocation.</summary>
    public static Text Create(TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a <see cref="Text"/> in the specified encoding from an interpolated string.</summary>
    public static Text Create(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> from an interpolated string. Caller must dispose.</summary>
    public static OwnedText CreatePooled(TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> in the specified encoding from an interpolated string.</summary>
    public static OwnedText CreatePooled(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();
#endif

    /// <summary>Implicitly converts a <see cref="string"/> to <see cref="Text"/>. Equivalent to <see cref="From"/>.</summary>
    public static implicit operator Text(string value) => From(value);
}
