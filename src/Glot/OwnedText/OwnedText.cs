using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable text value that owns a pool-backed buffer.
/// Returns the buffer to <see cref="ArrayPool{T}"/> and the wrapper to <see cref="ObjectPool{T}"/> on dispose.
/// Use <see cref="Text"/> to read the content; use <c>using</c> to manage lifetime.
/// </summary>
public sealed partial class OwnedText : IDisposable
{
    static readonly ObjectPool<OwnedText> Pool =
        new DefaultObjectPool<OwnedText>(new Policy(), 32);

    object? _buffer; // byte[], char[], or int[] — returned to the appropriate ArrayPool on dispose
    EncodedLength _encodedLength;
    BackingType _backingType;

    OwnedText() { }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values).</summary>
    public int RuneLength => _encodedLength.RuneLength;

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength { get; private set; }

    /// <summary>Returns <c>true</c> if this instance has been disposed.</summary>
    public bool IsDisposed { get; private set; }

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => ByteLength == 0;

    /// <summary>
    /// Returns a <see cref="Glot.Text"/> view over the pooled buffer. O(1).
    /// The returned value is valid only while this <see cref="OwnedText"/> has not been disposed.
    /// </summary>
    public Text Text => _buffer is null ? default : new(_buffer, 0, ByteLength, Encoding, RuneLength, _backingType);

    internal void Initialize(object buffer, int byteLength, TextEncoding encoding, int runeLength, BackingType backingType)
    {
        _buffer = buffer;
        ByteLength = byteLength;
        _encodedLength = new EncodedLength(encoding, runeLength);
        _backingType = backingType;
    }

    /// <summary>
    /// Detaches the pooled buffer from this instance, transferring ownership to the caller.
    /// After detach, <see cref="Dispose"/> will not return the data buffer to <see cref="ArrayPool{T}"/>.
    /// </summary>
    internal object? Detach()
    {
        var buffer = _buffer;
        _buffer = null;
        ByteLength = 0;
        _encodedLength = default;
        return buffer;
    }

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying bytes into a pooled buffer.</summary>
    public static OwnedText? FromUtf8(ReadOnlySpan<byte> value, bool countRunes = true)
        => FromBytes(value, TextEncoding.Utf8, countRunes);

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying a multi-segment sequence into a pooled buffer.</summary>
    public static OwnedText? FromUtf8(ReadOnlySequence<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return null;
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
    public static OwnedText? FromChars(ReadOnlySpan<char> value, bool countRunes = true)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf16, countRunes);

    /// <summary>Creates a UTF-32 <see cref="OwnedText"/> by copying code points into a pooled byte buffer.</summary>
    public static OwnedText? FromUtf32(ReadOnlySpan<int> value)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf32, countRunes: true);

    /// <summary>Creates an <see cref="OwnedText"/> by copying raw bytes with the specified encoding into a pooled buffer.</summary>
    public static OwnedText? FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return null;
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

    static OwnedText GetFromPool()
    {
        var owned = Pool.Get();
        owned.IsDisposed = false;
        return owned;
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedText"/> from an interpolated string. Caller must dispose.</summary>
    public static OwnedText? Create(TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> in the specified encoding from an interpolated string.</summary>
    public static OwnedText? Create(
        TextEncoding encoding,
        [System.Runtime.CompilerServices.InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();
#endif

    /// <summary>Finalizer — returns the data buffer if Dispose was not called. Does not return the wrapper to the pool.</summary>
    ~OwnedText() => ReturnBuffer();

    /// <summary>Returns the pooled buffer to the appropriate <see cref="ArrayPool{T}"/> and the wrapper to the object pool.</summary>
    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        ReturnBuffer();
        GC.SuppressFinalize(this);
        Pool.Return(this);
    }

    void ReturnBuffer()
    {
        if (_buffer is null)
        {
            return;
        }

        switch (_buffer)
        {
            case byte[] bytes:
                ArrayPool<byte>.Shared.Return(bytes);
                break;
            case char[] chars:
                ArrayPool<char>.Shared.Return(chars);
                break;
            case int[] ints:
                ArrayPool<int>.Shared.Return(ints);
                break;
        }

        _buffer = null;
        ByteLength = 0;
        _encodedLength = default;
    }

    sealed class Policy : PooledObjectPolicy<OwnedText>
    {
        public override OwnedText Create() => new();

        public override bool Return(OwnedText obj) => true;
    }
}
