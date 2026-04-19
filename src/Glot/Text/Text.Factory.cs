using System.Buffers;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif
using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Creates a <see cref="Text"/> from a <see cref="string"/>.</summary>
    /// <param name="value">The source string.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy the string — wraps the reference directly.</remarks>
    /// <example>
    /// <code>
    /// Text text = Text.From("hello world");
    /// </code>
    /// </example>
    public static Text From(string value, bool countRunes = true)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(value.AsUnsafeSpan()), TextEncoding.Utf16) : 0;
        return new Text(
            value,
            0,
            value.Length * 2,
            TextEncoding.Utf16,
            runeLength,
            BackingType.String);
    }

    // --- ASCII ---

    /// <summary>Creates a UTF-8 <see cref="Text"/> from ASCII bytes.</summary>
    /// <param name="value">The ASCII byte array.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the array reference. The rune count equals the byte count for ASCII.</remarks>
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
            value.Length,
            BackingType.ByteArray);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from an ASCII string.</summary>
    /// <param name="value">The ASCII string.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the string reference. The rune count equals the string length for ASCII.</remarks>
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
            value.Length,
            BackingType.String);
    }

    // --- UTF-8 ---

    /// <summary>Creates a UTF-8 <see cref="Text"/> by copying the bytes.</summary>
    /// <param name="value">The UTF-8 bytes to copy.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Copies the bytes into a new array. For a non-copying alternative when you already have a <c>byte[]</c>, use <see cref="FromUtf8(byte[], bool)"/>.</remarks>
    /// <example>
    /// <code>
    /// Text text = Text.FromUtf8("hello"u8);
    /// </code>
    /// </example>
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
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> from a byte array.</summary>
    /// <param name="value">The UTF-8 byte array.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the array reference directly. The caller must not modify the array after this call.</remarks>
    /// <example>
    /// <code>
    /// byte[] bytes = Encoding.UTF8.GetBytes("hello");
    /// Text text = Text.FromUtf8(bytes);
    /// </code>
    /// </example>
    public static Text FromUtf8(byte[] value, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(value, TextEncoding.Utf8) : 0;
        return new Text(
            value,
            0,
            value.Length,
            TextEncoding.Utf8,
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> from an array segment.</summary>
    /// <param name="value">The UTF-8 array segment.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — references the segment's underlying array with the segment's offset and count.</remarks>
    public static Text FromUtf8(ArraySegment<byte> value, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(array.AsSpan(value.Offset, value.Count), TextEncoding.Utf8) : 0;
        return new Text(
            array,
            value.Offset,
            value.Count,
            TextEncoding.Utf8,
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a UTF-8 <see cref="Text"/> from a memory region.</summary>
    /// <param name="value">The UTF-8 memory region.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the memory is backed by a <c>byte[]</c>. Copies otherwise.</remarks>
    public static Text FromUtf8(ReadOnlyMemory<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ?
                RuneCount.Count(
                    array.AsUnsafeSpan(segment.Offset, segment.Count),
                    TextEncoding.Utf8) :
                0;

            return new Text(
                array,
                segment.Offset,
                segment.Count,
                TextEncoding.Utf8,
                runeLength,
                BackingType.ByteArray);
        }

        return FromUtf8(value.Span, countRunes);
    }

#if NET8_0_OR_GREATER
    /// <summary>Creates a UTF-8 <see cref="Text"/> from an immutable array.</summary>
    /// <param name="value">The UTF-8 immutable array.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — accesses the underlying array via <see cref="System.Runtime.InteropServices.ImmutableCollectionsMarshal"/>.</remarks>
    public static Text FromUtf8(ImmutableArray<byte> value, bool countRunes = true)
    {
        if (value.IsDefaultOrEmpty)
        {
            return default;
        }

        var array = ImmutableCollectionsMarshal.AsArray(value) ?? [];
        var runeLength = countRunes ? RuneCount.Count(array, TextEncoding.Utf8) : 0;
        return new Text(
            array,
            0,
            array.Length,
            TextEncoding.Utf8,
            runeLength,
            BackingType.ByteArray);
    }
#endif

    /// <summary>Creates a UTF-8 <see cref="Text"/> from a byte sequence.</summary>
    /// <param name="value">The UTF-8 byte sequence.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the sequence is a single array-backed segment. Copies into a new array for multi-segment sequences.</remarks>
    public static Text FromUtf8(ReadOnlySequence<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (value.IsSingleSegment)
        {
            return FromUtf8(value.First, countRunes);
        }

        var length = checked((int)value.Length);
        var array = new byte[length];
        value.CopyTo(array);
        var runeLength = countRunes ? RuneCount.Count(array, TextEncoding.Utf8) : 0;
        return new Text(
            array,
            0,
            length,
            TextEncoding.Utf8,
            runeLength,
            BackingType.ByteArray);
    }

    // --- UTF-16 ---

    /// <summary>Creates a UTF-16 <see cref="Text"/> by copying the chars.</summary>
    /// <param name="value">The UTF-16 chars to copy.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Copies the chars into a new array. For a non-copying alternative when you already have a <c>char[]</c>, use <see cref="FromChars(char[], bool)"/>.</remarks>
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
            runeLength,
            BackingType.CharArray);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from a char array.</summary>
    /// <param name="value">The UTF-16 char array.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the array reference directly. The caller must not modify the array after this call.</remarks>
    public static Text FromChars(char[] value, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(value.AsUnsafeSpan()), TextEncoding.Utf16) : 0;
        return new Text(
            value,
            0,
            value.Length * 2,
            TextEncoding.Utf16,
            runeLength,
            BackingType.CharArray);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from an array segment.</summary>
    /// <param name="value">The UTF-16 array segment.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — references the segment's underlying array with the segment's offset and count.</remarks>
    public static Text FromChars(ArraySegment<char> value, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(array.AsUnsafeSpan(value.Offset, value.Count)), TextEncoding.Utf16) : 0;
        return new Text(
            array,
            value.Offset * 2,
            value.Count * 2,
            TextEncoding.Utf16,
            runeLength,
            BackingType.CharArray);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from a memory region.</summary>
    /// <param name="value">The UTF-16 memory region.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the memory is backed by a <see cref="string"/> or <c>char[]</c>. Copies otherwise.</remarks>
    public static Text FromChars(ReadOnlyMemory<char> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetString(
            value,
            out var text,
            out var start,
            out var length))
        {
            var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(text.AsUnsafeSpan(start, length)), TextEncoding.Utf16) : 0;
            return new Text(
                text,
                start * 2,
                length * 2,
                TextEncoding.Utf16,
                runeLength,
                BackingType.String);
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ? RuneCount.Count(MemoryMarshal.AsBytes(array.AsSpan(segment.Offset, segment.Count)), TextEncoding.Utf16) : 0;
            return new Text(
                array,
                segment.Offset * 2,
                segment.Count * 2,
                TextEncoding.Utf16,
                runeLength,
                BackingType.CharArray);
        }

        return FromChars(value.Span, countRunes);
    }

    /// <summary>Creates a UTF-16 <see cref="Text"/> from a char sequence.</summary>
    /// <param name="value">The UTF-16 char sequence.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the sequence is a single string- or array-backed segment. Copies into a new array for multi-segment sequences.</remarks>
    public static Text FromChars(ReadOnlySequence<char> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (value.IsSingleSegment)
        {
            return FromChars(value.First, countRunes);
        }

        var length = checked((int)value.Length);
        var array = new char[length];
        value.CopyTo(array);
        var bytes = MemoryMarshal.AsBytes(array.AsUnsafeSpan());
        var runeLength = countRunes ? RuneCount.Count(bytes, TextEncoding.Utf16) : 0;
        return new Text(
            array,
            0,
            bytes.Length,
            TextEncoding.Utf16,
            runeLength,
            BackingType.CharArray);
    }

    // --- UTF-32 ---

    /// <summary>Creates a UTF-32 <see cref="Text"/> by copying the code points.</summary>
    /// <param name="value">The UTF-32 code points to copy.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Copies the code points into a new array. For a non-copying alternative when you already have an <c>int[]</c>, use <see cref="FromUtf32(int[])"/>.</remarks>
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
            value.Length,
            BackingType.IntArray);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> from an int array.</summary>
    /// <param name="value">The UTF-32 code points.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the array reference directly. The caller must not modify the array after this call.</remarks>
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
            value.Length,
            BackingType.IntArray);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> from an array segment.</summary>
    /// <param name="value">The UTF-32 array segment.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — references the segment's underlying array with the segment's offset and count.</remarks>
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
            value.Count,
            BackingType.IntArray);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> from a memory region.</summary>
    /// <param name="value">The UTF-32 memory region.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the memory is backed by an <c>int[]</c>. Copies otherwise.</remarks>
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
                segment.Count,
                BackingType.IntArray);
        }

        return FromUtf32(value.Span);
    }

    /// <summary>Creates a UTF-32 <see cref="Text"/> from an int sequence.</summary>
    /// <param name="value">The UTF-32 int sequence.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the sequence is a single array-backed segment. Copies into a new array for multi-segment sequences.</remarks>
    public static Text FromUtf32(ReadOnlySequence<int> value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (value.IsSingleSegment)
        {
            return FromUtf32(value.First);
        }

        var length = checked((int)value.Length);
        var array = new int[length];
        value.CopyTo(array);
        return new Text(
            array,
            0,
            length * 4,
            TextEncoding.Utf32,
            length,
            BackingType.IntArray);
    }

    // --- Generic encoding ---

    /// <summary>Creates a <see cref="Text"/> by copying raw bytes.</summary>
    /// <param name="value">The raw bytes to copy.</param>
    /// <param name="encoding">The encoding of the input bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Copies the bytes into a new array. For a non-copying alternative when you already have a <c>byte[]</c>, use <see cref="FromBytes(byte[], TextEncoding, bool)"/>.</remarks>
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
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a <see cref="Text"/> from a byte array.</summary>
    /// <param name="value">The raw byte array.</param>
    /// <param name="encoding">The encoding of the input bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — wraps the array reference directly. The caller must not modify the array after this call.</remarks>
    public static Text FromBytes(byte[] value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.Length == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(value, encoding) : 0;
        return new Text(
            value,
            0,
            value.Length,
            encoding,
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a <see cref="Text"/> from an array segment.</summary>
    /// <param name="value">The raw byte array segment.</param>
    /// <param name="encoding">The encoding of the input bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy — references the segment's underlying array with the segment's offset and count.</remarks>
    public static Text FromBytes(ArraySegment<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.Array is not { } array || value.Count == 0)
        {
            return default;
        }

        var runeLength = countRunes ? RuneCount.Count(array.AsUnsafeSpan(value.Offset, value.Count), encoding) : 0;
        return new Text(
            array,
            value.Offset,
            value.Count,
            encoding,
            runeLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates a <see cref="Text"/> from a memory region.</summary>
    /// <param name="value">The raw byte memory region.</param>
    /// <param name="encoding">The encoding of the input bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the memory is backed by a <c>byte[]</c>. Copies otherwise.</remarks>
    public static Text FromBytes(ReadOnlyMemory<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (MemoryMarshal.TryGetArray(value, out var segment) && segment.Array is { } array)
        {
            var runeLength = countRunes ? RuneCount.Count(array.AsUnsafeSpan(segment.Offset, segment.Count), encoding) : 0;
            return new Text(
                array,
                segment.Offset,
                segment.Count,
                encoding,
                runeLength,
                BackingType.ByteArray);
        }

        return FromBytes(value.Span, encoding, countRunes);
    }

    /// <summary>Creates a <see cref="Text"/> from a byte sequence.</summary>
    /// <param name="value">The raw byte sequence.</param>
    /// <param name="encoding">The encoding of the input bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction. When <c>false</c>, the rune count is computed on first access.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Does not copy when the sequence is a single array-backed segment. Copies into a new array for multi-segment sequences.</remarks>
    public static Text FromBytes(ReadOnlySequence<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        if (value.IsSingleSegment)
        {
            return FromBytes(value.First, encoding, countRunes);
        }

        var length = checked((int)value.Length);
        var array = new byte[length];
        value.CopyTo(array);
        var runeLength = countRunes ? RuneCount.Count(array, encoding) : 0;
        return new Text(
            array,
            0,
            length,
            encoding,
            runeLength,
            BackingType.ByteArray);
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a UTF-8 <see cref="Text"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Uses a pooled <see cref="TextBuilder"/> internally. The result is a new <see cref="Text"/> with an exact-size backing array.</remarks>
    /// <example>
    /// <code>
    /// int count = 42;
    /// Text text = Text.Create($"items: {count}");
    /// </code>
    /// </example>
    public static Text Create(TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a <see cref="Text"/> in the specified encoding from an interpolated string.</summary>
    /// <param name="encoding">The target encoding.</param>
    /// <param name="handler">The interpolated string handler.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <remarks>Uses a pooled <see cref="TextBuilder"/> internally. The result encoding matches <paramref name="encoding"/>.</remarks>
    public static Text Create(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToText();
#endif

    /// <summary>Implicitly converts a <see cref="string"/> to <see cref="Text"/>. Equivalent to <see cref="From"/>.</summary>
    /// <param name="value">The source string.</param>
    /// <returns>A <see cref="Text"/> containing the provided data.</returns>
    /// <example>
    /// <code>
    /// Text text = "hello";
    /// </code>
    /// </example>
    public static implicit operator Text(string value) => From(value);
}
