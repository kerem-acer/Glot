using System.Buffers;
using System.Runtime.InteropServices;

namespace Glot;

/// <summary>
/// A disposable text value that owns a pool-backed buffer.
/// Returns the buffer to <see cref="ArrayPool{T}"/> on dispose.
/// Use <see cref="Text"/> to read the content; use <c>using</c> to manage lifetime.
/// </summary>
/// <remarks>
/// This is a <c>readonly struct</c>. The caller is responsible for not disposing copies independently.
/// Any <see cref="Text"/> or <see cref="TextSpan"/> obtained before dispose becomes invalid after dispose.
/// </remarks>
public readonly struct OwnedText : IDisposable
{
    readonly object? _buffer; // byte[], char[], or int[] — returned to the appropriate ArrayPool on dispose
    readonly int _byteLength;
    readonly EncodedLength _encodedLength;

    OwnedText(object buffer, int byteLength, TextEncoding encoding, int runeLength)
    {
        _buffer = buffer;
        _byteLength = byteLength;
        _encodedLength = new EncodedLength(encoding, runeLength);
    }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>The number of Unicode runes (scalar values).</summary>
    public int RuneLength => _encodedLength.RuneLength;

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _byteLength;

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => _byteLength == 0;

    /// <summary>
    /// Returns a <see cref="Glot.Text"/> view over the pooled buffer. O(1).
    /// The returned value is valid only while this <see cref="OwnedText"/> has not been disposed.
    /// </summary>
    public Text Text => new(_buffer, 0, _byteLength, Encoding, RuneLength);

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying bytes into a pooled buffer.</summary>
    public static OwnedText FromUtf8(ReadOnlySpan<byte> value)
        => FromBytes(value, TextEncoding.Utf8);

    /// <summary>Creates a UTF-16 <see cref="OwnedText"/> by copying chars into a pooled byte buffer.</summary>
    public static OwnedText FromChars(ReadOnlySpan<char> value)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf16);

    /// <summary>Creates a UTF-32 <see cref="OwnedText"/> by copying code points into a pooled byte buffer.</summary>
    public static OwnedText FromUtf32(ReadOnlySpan<int> value)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf32);

    /// <summary>Creates an <see cref="OwnedText"/> by copying raw bytes with the specified encoding into a pooled buffer.</summary>
    public static OwnedText FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(value.Length);
        value.CopyTo(buffer);
        var runeLength = RuneCount.Count(value, encoding);
        return new OwnedText(buffer, value.Length, encoding, runeLength);
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>byte[]</c> buffer. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(byte[] buffer, int byteLength, TextEncoding encoding)
    {
        var runeLength = RuneCount.Count(buffer.AsSpan(0, byteLength), encoding);
        return new OwnedText(buffer, byteLength, encoding, runeLength);
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>char[]</c> buffer as UTF-16. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(char[] buffer, int charLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, charLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf16);
        return new OwnedText(buffer, charLength * 2, TextEncoding.Utf16, runeLength);
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>int[]</c> buffer as UTF-32. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    public static OwnedText Create(int[] buffer, int intLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, intLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf32);
        return new OwnedText(buffer, intLength * 4, TextEncoding.Utf32, runeLength);
    }

    /// <summary>Returns the pooled buffer to the appropriate <see cref="ArrayPool{T}"/>.</summary>
    public void Dispose()
    {
        switch (_buffer)
        {
            case byte[] bytes: ArrayPool<byte>.Shared.Return(bytes); break;
            case char[] chars: ArrayPool<char>.Shared.Return(chars); break;
            case int[] ints: ArrayPool<int>.Shared.Return(ints); break;
        }
    }
}
