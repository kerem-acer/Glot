using System.Buffers;
using System.Runtime.InteropServices;

namespace Glot;

public sealed partial class OwnedText
{
    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying bytes into a pooled buffer.</summary>
    public static OwnedText FromUtf8(ReadOnlySpan<byte> value, bool countRunes = true)
        => FromBytes(value, TextEncoding.Utf8, countRunes);

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying a multi-segment sequence into a pooled buffer.</summary>
    public static OwnedText FromUtf8(ReadOnlySequence<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        if (value.IsSingleSegment)
        {
            return FromUtf8(value.First.Span, countRunes);
        }

        var length = checked((int)value.Length);
        var buffer = ArrayPool<byte>.Shared.Rent(length);
        value.CopyTo(buffer);
        var runeLength = countRunes ? RuneCount.Count(buffer.AsSpan(0, length), TextEncoding.Utf8) : 0;

        var owned = GetFromPool();
        owned.Initialize(buffer, length, TextEncoding.Utf8, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>Creates a UTF-16 <see cref="OwnedText"/> by copying chars into a pooled byte buffer.</summary>
    public static OwnedText FromChars(ReadOnlySpan<char> value, bool countRunes = true)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf16, countRunes);

    /// <summary>Creates a UTF-32 <see cref="OwnedText"/> by copying code points into a pooled byte buffer.</summary>
    public static OwnedText FromUtf32(ReadOnlySpan<int> value)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf32, countRunes: true);

    /// <summary>Creates an <see cref="OwnedText"/> by copying raw bytes with the specified encoding into a pooled buffer.</summary>
    public static OwnedText FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(value.Length);
        value.CopyTo(buffer);
        var runeLength = countRunes ? RuneCount.Count(value, encoding) : 0;

        var owned = GetFromPool();
        owned.Initialize(buffer, value.Length, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>byte[]</c> buffer. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(byte[] buffer, int byteLength, TextEncoding encoding)
    {
        var runeLength = RuneCount.Count(buffer.AsSpan(0, byteLength), encoding);
        var owned = GetFromPool();
        owned.Initialize(buffer, byteLength, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>byte[]</c> buffer with a pre-computed rune length. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    internal static OwnedText Create(byte[] buffer, int byteLength, TextEncoding encoding, int runeLength)
    {
        var owned = GetFromPool();
        owned.Initialize(buffer, byteLength, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>char[]</c> buffer as UTF-16. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(char[] buffer, int charLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, charLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf16);
        var owned = GetFromPool();
        owned.Initialize(buffer, charLength * 2, TextEncoding.Utf16, runeLength, BackingType.CharArray);
        return owned;
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>int[]</c> buffer as UTF-32. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(int[] buffer, int intLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, intLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf32);
        var owned = GetFromPool();
        owned.Initialize(buffer, intLength * 4, TextEncoding.Utf32, runeLength, BackingType.IntArray);
        return owned;
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedText"/> from an interpolated string. Caller must dispose.</summary>
    public static OwnedText Create(TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> in the specified encoding from an interpolated string.</summary>
    public static OwnedText Create(
        TextEncoding encoding,
        [System.Runtime.CompilerServices.InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();
#endif
}
