using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Glot;

/// <summary>
/// A heap-safe, encoding-aware text value. Stores backing data (<see cref="string"/>,
/// <c>byte[]</c>, <c>char[]</c>, or <c>int[]</c>) as-is and delegates operations
/// to <see cref="TextSpan"/> via <see cref="AsSpan"/>.
/// </summary>
/// <remarks>
/// <para><see cref="Text"/> is a 24-byte readonly struct on x64. It wraps a managed reference
/// (string, byte[], char[], or int[]) with offset and length — no copying on construction
/// from array-backed sources.</para>
/// <para>All operations that accept cross-encoding input (e.g. searching a UTF-8 text with a UTF-16 needle)
/// transcode on the fly without allocating.</para>
/// </remarks>
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
    // 24-byte layout on x64: object?(8) + int(4) + int(4) + int(4) + byte(1) + byte(1) + byte(1) + 1B pad.
    readonly object? _data;
    readonly int _runeLength;
    readonly int _byteLength;
    readonly int _start;
    readonly BackingType _backingType;
#if NET6_0_OR_GREATER
    readonly byte _dataOffset;
#endif
    readonly TextEncoding _encoding;

    internal Text(object? data,
        int start,
        int byteLength,
        TextEncoding encoding,
        int runeLength,
        BackingType backingType)
    {
        _data = data;
        _start = start;
        _byteLength = byteLength;
        _runeLength = runeLength;
        _backingType = backingType;
        _encoding = encoding;
#if NET6_0_OR_GREATER
        _dataOffset = backingType == BackingType.String ? StringDataOffset.Value : (byte)0;
#endif
    }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encoding;

    /// <summary>The number of Unicode scalar values.</summary>
    /// <remarks>Cached when the <see cref="Text"/> was created with rune counting enabled; otherwise computed on first access.</remarks>
    public int RuneLength
        => _runeLength != 0 ? _runeLength : RuneCount.Count(Bytes, Encoding);

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _byteLength;

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => _byteLength == 0;

    /// <summary>The backing data object (byte[], string, char[], or int[]).</summary>
    internal object? Data => _data;

    /// <summary>The backing type discriminator.</summary>
    internal BackingType DataBackingType => _backingType;

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

    /// <summary>
    /// Raw byte view assuming <see cref="BackingType.ByteArray"/> backing. Branchless.
    /// Caller must ensure <c>_backingType == BackingType.ByteArray</c> and <c>_data</c> is not null.
    /// Unlike <see cref="UnsafeBytes"/>, skips the string/array header adjustment — valid only for byte[] backing.
    /// </summary>
    internal ReadOnlySpan<byte> UnsafeByteArrayBytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var arr = Unsafe.As<object?, byte[]>(ref Unsafe.AsRef(in _data));
            return arr.AsUnsafeSpan(_start, _byteLength);
        }
    }

    /// <summary>
    /// Raw char view assuming <see cref="BackingType.String"/> backing. Branchless.
    /// Caller must ensure <c>_backingType == BackingType.String</c> and <c>_data</c> is not null.
    /// </summary>
    internal ReadOnlySpan<char> UnsafeStringChars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var s = Unsafe.As<object?, string>(ref Unsafe.AsRef(in _data));
            return s.AsUnsafeSpan(_start >> 1, _byteLength >> 1);
        }
    }

    /// <summary>
    /// Raw char view assuming <see cref="BackingType.CharArray"/> backing. Branchless.
    /// Caller must ensure <c>_backingType == BackingType.CharArray</c> and <c>_data</c> is not null.
    /// </summary>
    internal ReadOnlySpan<char> UnsafeCharArrayChars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var arr = Unsafe.As<object?, char[]>(ref Unsafe.AsRef(in _data));
            return arr.AsUnsafeSpan(_start >> 1, _byteLength >> 1);
        }
    }

    /// <summary>
    /// Raw int view assuming <see cref="BackingType.IntArray"/> backing. Branchless.
    /// Caller must ensure <c>_backingType == BackingType.IntArray</c> and <c>_data</c> is not null.
    /// </summary>
    internal ReadOnlySpan<int> UnsafeIntArrayInts
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var arr = Unsafe.As<object?, int[]>(ref Unsafe.AsRef(in _data));
            return arr.AsUnsafeSpan(_start >> 2, _byteLength >> 2);
        }
    }
#else
    public ReadOnlySpan<byte> Bytes => _backingType switch
    {
        BackingType.ByteArray => Unsafe.As<object, byte[]>(
                ref Unsafe.AsRef(in _data)!)
            .AsSpan(_start, _byteLength),
        BackingType.String => MemoryMarshal.AsBytes(
                Unsafe.As<object, string>(ref Unsafe.AsRef(in _data)!).AsSpan())
            .Slice(_start, _byteLength),
        BackingType.CharArray => MemoryMarshal.AsBytes(
                Unsafe.As<object, char[]>(ref Unsafe.AsRef(in _data)!).AsSpan())
            .Slice(_start, _byteLength),
        BackingType.IntArray => MemoryMarshal.AsBytes(
                Unsafe.As<object, int[]>(ref Unsafe.AsRef(in _data)!).AsSpan())
            .Slice(_start, _byteLength),
        _ => default,
    };

    /// <summary>Byte access. Delegates to <see cref="Bytes"/> on netstandard.</summary>
    internal ReadOnlySpan<byte> UnsafeBytes => Bytes;

    /// <inheritdoc cref="UnsafeBytes"/>
    internal ReadOnlySpan<byte> UnsafeByteArrayBytes => Bytes;

    internal ReadOnlySpan<char> UnsafeStringChars => MemoryMarshal.Cast<byte, char>(Bytes);

    internal ReadOnlySpan<char> UnsafeCharArrayChars => MemoryMarshal.Cast<byte, char>(Bytes);

    internal ReadOnlySpan<int> UnsafeIntArrayInts => MemoryMarshal.Cast<byte, int>(Bytes);
#endif

    /// <summary>Reinterprets the underlying bytes as <see cref="char"/> elements.</summary>
    public ReadOnlySpan<char> Chars => AsSpan().Chars;

    /// <summary>Reinterprets the underlying bytes as <see cref="int"/> elements.</summary>
    public ReadOnlySpan<int> Ints => AsSpan().Ints;

    /// <summary>Creates a stack-only <see cref="TextSpan"/> view over this text.</summary>
    /// <returns>A <see cref="TextSpan"/> referencing the same backing data.</returns>
    /// <example>
    /// <code>
    /// var text = Text.FromUtf8("hello"u8);
    /// TextSpan span = text.AsSpan();
    /// </code>
    /// </example>
    public TextSpan AsSpan() => new(Bytes, Encoding, _runeLength);

    /// <summary>Attempts to obtain a <see cref="ReadOnlyMemory{T}"/> over the backing UTF-8 bytes without copying.</summary>
    /// <param name="memory">When this method returns <c>true</c>, contains the UTF-8 memory region; otherwise <c>default</c>.</param>
    /// <returns><c>true</c> if the text is UTF-8 and array-backed; otherwise <c>false</c>.</returns>
    /// <remarks>Succeeds only when the text is UTF-8 encoded and backed by a <c>byte[]</c>. String-backed UTF-8 text is not supported.</remarks>
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

    /// <summary>Attempts to obtain a <see cref="ReadOnlyMemory{T}"/> over the backing UTF-16 chars without copying.</summary>
    /// <param name="memory">When this method returns <c>true</c>, contains the UTF-16 memory region; otherwise <c>default</c>.</param>
    /// <returns><c>true</c> if the text is UTF-16 and string- or array-backed; otherwise <c>false</c>.</returns>
    /// <remarks>Succeeds when the text is UTF-16 encoded and backed by a <see cref="string"/> or <c>char[]</c>.</remarks>
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
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> of the underlying UTF-8 bytes.</returns>
    /// <exception cref="InvalidOperationException">The text is not backed by UTF-8.</exception>
    /// <remarks>This is a non-copying operation. The returned memory references the same backing array.</remarks>
    public ReadOnlyMemory<byte> AsUtf8Memory()
        => TryGetUtf8Memory(out var memory) ? memory : throw new InvalidOperationException($"Cannot obtain UTF-8 memory from {Encoding}-encoded text.");

    /// <summary>Returns a <see cref="ReadOnlyMemory{T}"/> over the backing UTF-16 chars without copying.</summary>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> of the underlying UTF-16 chars.</returns>
    /// <exception cref="InvalidOperationException">The text is not backed by UTF-16.</exception>
    /// <remarks>This is a non-copying operation. The returned memory references the same backing array.</remarks>
    public ReadOnlyMemory<char> AsUtf16Memory()
        => TryGetUtf16Memory(out var memory) ? memory : throw new InvalidOperationException($"Cannot obtain UTF-16 memory from {Encoding}-encoded text.");

    /// <summary>An empty <see cref="Text"/> value.</summary>
    /// <remarks>Equivalent to <c>default(Text)</c>. The encoding is <see cref="TextEncoding.Utf8"/> and all lengths are zero.</remarks>
    public static Text Empty => default;
}
