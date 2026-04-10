using System.Runtime.InteropServices;

namespace Glot;

/// <summary>
/// A heap-safe, encoding-aware text value. Stores backing data (<see cref="string"/>,
/// <c>byte[]</c>, <c>char[]</c>, or <c>int[]</c>) as-is and delegates operations
/// to <see cref="TextSpan"/> via <see cref="AsSpan"/>.
/// </summary>
public readonly partial struct Text :
    IEquatable<Text>,
    IComparable<Text>
#if NET6_0_OR_GREATER
    , ISpanFormattable
#endif
#if NET8_0_OR_GREATER
    , IUtf8SpanFormattable
#endif
#if NET7_0_OR_GREATER
    , ISpanParsable<Text>
#endif
#if NET8_0_OR_GREATER
    , IUtf8SpanParsable<Text>
#endif
{
    readonly object? _data;
    readonly int _start;
    readonly int _byteLength;
    readonly EncodedLength _encodedLength;

    internal Text(object? data, int start, int byteLength, TextEncoding encoding, int runeLength)
    {
        _data = data;
        _start = start;
        _byteLength = byteLength;
        _encodedLength = new EncodedLength(encoding, runeLength);
    }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values). O(1) — computed at construction.</summary>
    public int RuneLength => _encodedLength.RuneLength;

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _byteLength;

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => _byteLength == 0;

    /// <summary>The raw underlying bytes, regardless of encoding.</summary>
    public ReadOnlySpan<byte> Bytes => AsSpan().Bytes;

    /// <summary>Reinterprets the underlying bytes as <see cref="char"/> elements. Zero-copy cast.</summary>
    public ReadOnlySpan<char> Chars => AsSpan().Chars;

    /// <summary>Reinterprets the underlying bytes as <see cref="int"/> elements. Zero-copy cast.</summary>
    public ReadOnlySpan<int> Ints => AsSpan().Ints;

    /// <summary>Creates a stack-only <see cref="TextSpan"/> view over this text. O(1) — no rune recount.</summary>
    public TextSpan AsSpan() => _data switch
    {
        byte[] bytes => new TextSpan(
            bytes.AsSpan(_start, _byteLength), Encoding, RuneLength),
        string s => new TextSpan(
            MemoryMarshal.AsBytes(s.AsSpan()).Slice(_start, _byteLength), Encoding, RuneLength),
        char[] chars => new TextSpan(
            MemoryMarshal.AsBytes(chars.AsSpan()).Slice(_start, _byteLength), Encoding, RuneLength),
        int[] ints => new TextSpan(
            MemoryMarshal.AsBytes(ints.AsSpan()).Slice(_start, _byteLength), Encoding, RuneLength),
        _ => default,
    };

    /// <summary>An empty <see cref="Text"/> value.</summary>
    public static Text Empty => default;
}
