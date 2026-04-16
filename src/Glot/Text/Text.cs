#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
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
    // 24-byte layout on x64: object?(8) + EncodedLength(4) + int(4) + int(4) + byte(1) + byte(1) + byte(1) + 1B pad.
    readonly object? _data;
    readonly EncodedLength _encodedLength; // encoding(2 bits) + runeLength(30 bits)
#pragma warning disable IDE0032 // Use auto property — field name differs from property
    readonly int _byteLength;
#pragma warning restore IDE0032
    readonly int _start;
    readonly BackingType _backingType;
#pragma warning disable CS0414, IDE0052 // _dataOffset is read only on NET6+ (branchless path); on netstandard it occupies padding
    readonly byte _dataOffset;
#pragma warning restore CS0414, IDE0052
    readonly byte _encoding; // redundant with _encodedLength.Encoding but avoids bit-mask on hot path

#if NET6_0_OR_GREATER
    static readonly byte StringDataOffset = CalcStringDataOffset();
#endif

    internal Text(object? data, int start, int byteLength, TextEncoding encoding,
        int runeLength, BackingType backingType)
    {
        _data = data;
        _start = start;
        _byteLength = byteLength;
        _encodedLength = new EncodedLength(encoding, runeLength);
        _backingType = backingType;
        _encoding = (byte)encoding;
#if NET6_0_OR_GREATER
        _dataOffset = backingType == BackingType.String ? StringDataOffset : (byte)0;
#else
        _dataOffset = 0;
#endif
    }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values). O(1) when cached; SIMD O(n) when not.</summary>
    public int RuneLength
    {
        get
        {
            var cached = _encodedLength.RuneLength;
            return cached != 0 ? cached : RuneCount.Count(Bytes, Encoding);
        }
    }

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _byteLength;

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => _byteLength == 0;

    /// <summary>The raw underlying bytes, regardless of encoding.</summary>
#if NET6_0_OR_GREATER
    public ReadOnlySpan<byte> Bytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _data is null ? default : UnsafeBytes;
    }

    /// <summary>
    /// Branchless byte access. Caller must ensure <c>_data</c> is not null.
    /// Uses a pre-computed <see cref="_dataOffset"/> to adjust for string-vs-array header differences.
    /// </summary>
    internal ReadOnlySpan<byte> UnsafeBytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ref byte first = ref Unsafe.AddByteOffset(
                ref MemoryMarshal.GetArrayDataReference(
                    Unsafe.As<object?, byte[]>(ref Unsafe.AsRef(in _data))),
                _start - _dataOffset);
            return MemoryMarshal.CreateReadOnlySpan(ref first, _byteLength);
        }
    }
#else
    public ReadOnlySpan<byte> Bytes => _data switch
    {
        byte[] b => b.AsSpan(_start, _byteLength),
        string s => MemoryMarshal.AsBytes(s.AsSpan()).Slice(_start, _byteLength),
        char[] c => MemoryMarshal.AsBytes(c.AsSpan()).Slice(_start, _byteLength),
        int[] n => MemoryMarshal.AsBytes(n.AsSpan()).Slice(_start, _byteLength),
        _ => default,
    };

    /// <summary>Byte access. Delegates to <see cref="Bytes"/> on netstandard.</summary>
    internal ReadOnlySpan<byte> UnsafeBytes => Bytes;
#endif

    /// <summary>Reinterprets the underlying bytes as <see cref="char"/> elements. Zero-copy cast.</summary>
    public ReadOnlySpan<char> Chars => AsSpan().Chars;

    /// <summary>Reinterprets the underlying bytes as <see cref="int"/> elements. Zero-copy cast.</summary>
    public ReadOnlySpan<int> Ints => AsSpan().Ints;

    /// <summary>Creates a stack-only <see cref="TextSpan"/> view over this text. O(1) — no rune recount.</summary>
    public TextSpan AsSpan() => new(Bytes, Encoding, _encodedLength.RuneLength);

    public bool TryGetUtf8Memory(out ReadOnlyMemory<byte> memory)
    {
        if (Encoding == TextEncoding.Utf8 && _data is byte[] bytes)
        {
            memory = new ReadOnlyMemory<byte>(bytes, _start, _byteLength);
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
                    memory = s.AsMemory(_start / 2, _byteLength / 2);
                    return true;
                case char[] chars:
                    memory = new ReadOnlyMemory<char>(chars, _start / 2, _byteLength / 2);
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

#if NET6_0_OR_GREATER
    static byte CalcStringDataOffset()
    {
        var s = "X";
        ref byte asArray = ref MemoryMarshal.GetArrayDataReference(Unsafe.As<string, byte[]>(ref s));
        ref byte asString = ref Unsafe.As<char, byte>(
            ref MemoryMarshal.GetReference(s.AsSpan()));
        return (byte)-(int)Unsafe.ByteOffset(ref asArray, ref asString);
    }
#endif

    /// <summary>An empty <see cref="Text"/> value.</summary>
    public static Text Empty => default;
}
