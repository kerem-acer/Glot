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
    // 20-byte layout (24B with alignment): hash dropped, rune count lazy.
    readonly object? _data;
    readonly EncodedLength _encodedLength; // encoding(2 bits) + byteLength(30 bits)
    readonly int _start;
#pragma warning disable IDE0032 // Use auto property — getter has conditional logic
    readonly int _cachedRuneLength; // 0 = not cached, compute on demand
#pragma warning restore IDE0032

    internal Text(object? data, int start, int byteLength, TextEncoding encoding, int runeLength)
    {
        _data = data;
        _start = start;
        _encodedLength = new EncodedLength(encoding, byteLength);
        _cachedRuneLength = runeLength;
    }


    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values). O(1) when cached; SIMD O(n) when not.</summary>
    public int RuneLength => _cachedRuneLength != 0 ? _cachedRuneLength : RuneCount.Count(RawBytes, Encoding);

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _encodedLength.Length;

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => ByteLength == 0;

    /// <summary>The raw underlying bytes, regardless of encoding.</summary>
    public ReadOnlySpan<byte> Bytes => RawBytes;

    /// <summary>Direct byte access without constructing a <see cref="TextSpan"/>.</summary>
    internal ReadOnlySpan<byte> RawBytes => _data switch
    {
        byte[] b => b.AsSpan(_start, ByteLength),
        string s => MemoryMarshal.AsBytes(s.AsSpan()).Slice(_start, ByteLength),
        char[] c => MemoryMarshal.AsBytes(c.AsSpan()).Slice(_start, ByteLength),
        int[] n => MemoryMarshal.AsBytes(n.AsSpan()).Slice(_start, ByteLength),
        _ => default,
    };

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

    public bool TryGetUtf8Memory(out ReadOnlyMemory<byte> memory)
    {
        if (Encoding == TextEncoding.Utf8 && _data is byte[] bytes)
        {
            memory = new ReadOnlyMemory<byte>(bytes, _start, ByteLength);
            return true;
        }

        memory = default;
        return false;
    }

    public bool TryGetUtf16Memory(out ReadOnlyMemory<char> memory)
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

    /// <summary>Returns a <see cref="ReadOnlyMemory{T}"/> over the backing UTF-8 bytes without copying.</summary>
    /// <exception cref="InvalidOperationException">The text is not backed by UTF-8.</exception>
    public ReadOnlyMemory<byte> AsUtf8Memory()
        => TryGetUtf8Memory(out var memory)
            ? memory
            : throw new InvalidOperationException($"Cannot obtain UTF-8 memory from {Encoding}-encoded text.");

    /// <summary>Returns a <see cref="ReadOnlyMemory{T}"/> over the backing UTF-16 chars without copying.</summary>
    /// <exception cref="InvalidOperationException">The text is not backed by UTF-16.</exception>
    public ReadOnlyMemory<char> AsUtf16Memory()
        => TryGetUtf16Memory(out var memory)
            ? memory
            : throw new InvalidOperationException($"Cannot obtain UTF-16 memory from {Encoding}-encoded text.");

    /// <summary>An empty <see cref="Text"/> value.</summary>
    public static Text Empty => default;
}
