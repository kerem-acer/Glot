#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif
using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Glot;

/// <remarks>
/// <para>
/// Zero-copy factories defer rune counting (runeLength=0, computed on first access).
/// Copy factories count runes during the copy by default (countRunes=true).
/// </para>
/// </remarks>
public readonly partial struct Text
{
    /// <summary>Creates a <see cref="Text"/> from a <see cref="string"/>. Zero-copy. Rune count deferred.</summary>
    public static Text From(string value, bool countRunes = true)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(value.AsSpan()), TextEncoding.Utf16) : 0;
        return new Text(
            value,
            0,
            value.Length * 2,
            TextEncoding.Utf16,
            runeLength);
    }

    // --- ASCII ---

    /// <summary>Creates a UTF-8 <see cref="Text"/> from ASCII bytes. Zero-copy, O(1) — rune count equals byte count.</summary>
    public static Text FromAscii(byte[] value)
    {
        if (value.Length == 0)
        {
            return default;
        }

        return new Text(
            value,
            0,
            value.Length,
            TextEncoding.Utf8,
            value.Length);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from an ASCII string. Zero-copy, O(1) rune count.</summary>
    public static Text FromAscii(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        return new Text(
            value,
            0,
            value.Length * 2,
            TextEncoding.Utf16,
            value.Length);
    }

    // --- UTF-8 ---

    /// <summary>Creates a UTF-8 <see cref="Text"/> by copying the bytes.</summary>
    public static Text FromUtf8(ReadOnlySpan<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var array = value.ToArray();
        var runeLength = countRunes ? RuneCount.Count(value, TextEncoding.Utf8) : 0;
        return new Text(
            array,
            0,
            value.Length,
            TextEncoding.Utf8,
            runeLength);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/>. Zero-copy — stores the array reference directly.</summary>
    public static Text FromUtf8(byte[] value, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(value, TextEncoding.Utf8) : 0;
        return new Text(value, 0, value.Length, TextEncoding.Utf8, runeLength);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/>. Zero-copy — references the segment's backing array.</summary>
    public static Text FromUtf8(ArraySegment<byte> value, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(array.AsSpan(value.Offset, value.Count), TextEncoding.Utf8) : 0;
        return new Text(array, value.Offset, value.Count, TextEncoding.Utf8, runeLength);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/>. Zero-copy when array-backed; copies otherwise.</summary>
    public static Text FromUtf8(ReadOnlyMemory<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ? RuneCount.Count(array.AsSpan(segment.Offset, segment.Count), TextEncoding.Utf8) : 0;
            return new Text(array, segment.Offset, segment.Count, TextEncoding.Utf8, runeLength);
        }

        return FromUtf8(value.Span, countRunes);
    }

#if NET8_0_OR_GREATER
    /// <summary>Creates a UTF-8 <see cref="Text"/>. Zero-copy via underlying array. Rune count deferred.</summary>
    public static Text FromUtf8(ImmutableArray<byte> value, bool countRunes = true)
    {
        if (value.IsDefaultOrEmpty)
        {
            return default;
        }

        var array = ImmutableCollectionsMarshal.AsArray(value)!;
        var runeLength = countRunes ? RuneCount.Count(array, TextEncoding.Utf8) : 0;
        return new Text(
            array,
            0,
            array.Length,
            TextEncoding.Utf8,
            runeLength);
    }
#endif

    // --- UTF-16 ---

    /// <summary>Creates a UTF-16 <see cref="Text"/> by copying the chars.</summary>
    public static Text FromChars(ReadOnlySpan<char> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var array = value.ToArray();
        var bytes = MemoryMarshal.AsBytes(value);
        var runeLength = countRunes ? RuneCount.Count(bytes, TextEncoding.Utf16) : 0;
        return new Text(
            array,
            0,
            bytes.Length,
            TextEncoding.Utf16,
            runeLength);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/>. Zero-copy — stores the array reference directly.</summary>
    public static Text FromChars(char[] value, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(value.AsSpan()), TextEncoding.Utf16) : 0;
        return new Text(value, 0, value.Length * 2, TextEncoding.Utf16, runeLength);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/>. Zero-copy — references the segment's backing array.</summary>
    public static Text FromChars(ArraySegment<char> value, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(array.AsSpan(value.Offset, value.Count)), TextEncoding.Utf16) : 0;
        return new Text(array, value.Offset * 2, value.Count * 2, TextEncoding.Utf16, runeLength);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/>. Zero-copy when string/array-backed; copies otherwise.</summary>
    public static Text FromChars(ReadOnlyMemory<char> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetString(value, out var text, out var start, out var length))
        {
            var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(text.AsSpan(start, length)), TextEncoding.Utf16) : 0;
            return new Text(text, start * 2, length * 2, TextEncoding.Utf16, runeLength);
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(array.AsSpan(segment.Offset, segment.Count)), TextEncoding.Utf16) : 0;
            return new Text(array, segment.Offset * 2, segment.Count * 2, TextEncoding.Utf16, runeLength);
        }

        return FromChars(value.Span, countRunes);
    }

    // --- UTF-32 ---

    /// <summary>Creates a UTF-32 <see cref="Text"/> by copying the code points.</summary>
    public static Text FromUtf32(ReadOnlySpan<int> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        // UTF-32: each int is one rune
        return new Text(
            value.ToArray(),
            0,
            value.Length * 4,
            TextEncoding.Utf32,
            value.Length);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/>. Zero-copy. Rune count = array length.</summary>
    public static Text FromUtf32(int[] value)
    {
        if (value.Length == 0)
        {
            return default;
        }

        return new Text(
            value,
            0,
            value.Length * 4,
            TextEncoding.Utf32,
            value.Length);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/>. Zero-copy. Rune count = segment count.</summary>
    public static Text FromUtf32(ArraySegment<int> value)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        return new Text(
            array,
            value.Offset * 4,
            value.Count * 4,
            TextEncoding.Utf32,
            value.Count);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/>. Zero-copy when array-backed.</summary>
    public static Text FromUtf32(ReadOnlyMemory<int> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            return new Text(
                array,
                segment.Offset * 4,
                segment.Count * 4,
                TextEncoding.Utf32,
                segment.Count);
        }

        return FromUtf32(value.Span);
    }

    // --- Generic encoding ---

    /// <summary>Creates a <see cref="Text"/> by copying raw bytes.</summary>
    public static Text FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var array = value.ToArray();
        var runeLength = countRunes ? RuneCount.Count(value, encoding) : 0;
        return new Text(
            array,
            0,
            value.Length,
            encoding,
            runeLength);
    }

    /// <summary>Creates a <see cref="Text"/>. Zero-copy — stores the array reference directly.</summary>
    public static Text FromBytes(byte[] value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(value, encoding) : 0;
        return new Text(value, 0, value.Length, encoding, runeLength);
    }

    /// <summary>Creates a <see cref="Text"/>. Zero-copy — references the segment's backing array.</summary>
    public static Text FromBytes(ArraySegment<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(array.AsSpan(value.Offset, value.Count), encoding) : 0;
        return new Text(array, value.Offset, value.Count, encoding, runeLength);
    }

    /// <summary>Creates a <see cref="Text"/>. Zero-copy when array-backed; copies otherwise.</summary>
    public static Text FromBytes(ReadOnlyMemory<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ? RuneCount.Count(array.AsSpan(segment.Offset, segment.Count), encoding) : 0;
            return new Text(array, segment.Offset, segment.Count, encoding, runeLength);
        }

        return FromBytes(value.Span, encoding, countRunes);
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a UTF-8 <see cref="Text"/> from an interpolated string.</summary>
    public static Text Create(TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a <see cref="Text"/> in the specified encoding from an interpolated string.</summary>
    public static Text Create(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToText();
#endif

    /// <summary>Implicitly converts a <see cref="string"/> to <see cref="Text"/>. Equivalent to <see cref="From"/>.</summary>
    public static implicit operator Text(string value) => From(value);
}
