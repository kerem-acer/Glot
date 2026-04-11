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
    readonly EncodedLength _encodedLength;
    readonly int _hashCode;

    internal Text(object? data, int start, int byteLength, TextEncoding encoding, int runeLength, int hashCode = 0)
    {
        _data = data;
        _start = start;
        ByteLength = byteLength;
        _encodedLength = new EncodedLength(encoding, runeLength);
        _hashCode = hashCode;
    }

    internal Text(object data, int start, ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        _data = data;
        _start = start;
        ByteLength = bytes.Length;
        _encodedLength = new EncodedLength(encoding, RuneCount.Count(bytes, encoding));
        _hashCode = TextSpan.ComputeHashCode(bytes, encoding) | 1;
    }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values). O(1) — computed at construction.</summary>
    public int RuneLength => _encodedLength.RuneLength;

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength { get; }

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => ByteLength == 0;

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
            bytes.AsSpan(_start, ByteLength), Encoding, RuneLength),
        string s => new TextSpan(
            MemoryMarshal.AsBytes(s.AsSpan()).Slice(_start, ByteLength), Encoding, RuneLength),
        char[] chars => new TextSpan(
            MemoryMarshal.AsBytes(chars.AsSpan()).Slice(_start, ByteLength), Encoding, RuneLength),
        int[] ints => new TextSpan(
            MemoryMarshal.AsBytes(ints.AsSpan()).Slice(_start, ByteLength), Encoding, RuneLength),
        _ => default,
    };

    internal bool TryGetUtf8Memory(out ReadOnlyMemory<byte> memory)
    {
        if (Encoding == TextEncoding.Utf8 && _data is byte[] bytes)
        {
            memory = new ReadOnlyMemory<byte>(bytes, _start, ByteLength);
            return true;
        }

        memory = default;
        return false;
    }

    internal bool TryGetUtf16Memory(out ReadOnlyMemory<char> memory)
    {
        if (Encoding == TextEncoding.Utf16)
        {
            switch (_data)
            {
                case string s:
                    memory = s.AsMemory(_start / 2, ByteLength / 2);
                    return true;
                case char[] chars:
                    memory = new ReadOnlyMemory<char>(chars, _start / 2, ByteLength / 2);
                    return true;
            }
        }

        memory = default;
        return false;
    }

    /// <summary>An empty <see cref="Text"/> value.</summary>
    public static Text Empty => default;
}
