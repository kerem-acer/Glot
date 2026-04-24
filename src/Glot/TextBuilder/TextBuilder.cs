using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
#if NET6_0_OR_GREATER
using System.Text.Unicode;
#else
using static System.Text.Encoding;
#endif
using static Glot.EncodingConstants;

namespace Glot;

/// <summary>
/// A mutable, pooled builder for constructing <see cref="Text"/> or <see cref="OwnedText"/> values.
/// </summary>
/// <remarks>
/// <para>The internal buffer is rented from <see cref="System.Buffers.ArrayPool{T}"/>. Use <c>using</c>
/// to ensure the buffer is returned. Do not copy this struct — copies share the same buffer.</para>
/// </remarks>
public struct TextBuilder : IDisposable
{
    const int DefaultCapacity = 256;

    byte[] _buffer;

    /// <summary>Creates a builder with the specified target encoding.</summary>
    /// <param name="encoding">The target encoding for the builder.</param>
    /// <example>
    /// <code>
    /// using var builder = new TextBuilder(TextEncoding.Utf8);
    /// builder.Append("hello ");
    /// builder.Append(Text.FromUtf8("world"u8));
    /// Text result = builder.ToText();
    /// </code>
    /// </example>
    public TextBuilder(TextEncoding encoding)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);
        ByteLength = 0;
        RuneLength = 0;
        Encoding = encoding;
    }

    /// <summary>Creates a builder with the specified initial capacity and target encoding.</summary>
    /// <param name="initialCapacity">The initial buffer capacity in bytes.</param>
    /// <param name="encoding">The target encoding for the builder.</param>
    public TextBuilder(int initialCapacity, TextEncoding encoding = TextEncoding.Utf8)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
        ByteLength = 0;
        RuneLength = 0;
        Encoding = encoding;
    }

    /// <summary>The target encoding for this builder.</summary>
    public TextEncoding Encoding { get; }

    /// <summary>The number of bytes written so far.</summary>
    public int ByteLength { get; private set; }

    /// <summary>The number of runes written so far.</summary>
    public int RuneLength { get; internal set; }

    /// <summary>Returns <c>true</c> if no content has been written.</summary>
    public readonly bool IsEmpty => ByteLength == 0;

    // Append — Text / TextSpan

    /// <summary>Appends the content of a <see cref="Text"/> value, transcoding if needed.</summary>
    /// <param name="value">The text to append.</param>
    /// <remarks>Transcodes automatically if the value's encoding differs from the builder's target encoding.</remarks>
    public void Append(Text value) => Append(value.AsSpan());

    /// <summary>Appends the content of a <see cref="TextSpan"/>, transcoding if needed.</summary>
    /// <param name="value">The text span to append.</param>
    /// <remarks>Transcodes automatically if the value's encoding differs from the builder's target encoding.</remarks>
    public void Append(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (value.Encoding == Encoding)
        {
            AppendBytes(value.Bytes);
            RuneLength += value.RuneLength;
            return;
        }

        switch (value.Encoding, Encoding)
        {
            case (TextEncoding.Utf16, TextEncoding.Utf8):
                AppendBulkUtf16ToUtf8(value);
                break;

            case (TextEncoding.Utf8, TextEncoding.Utf16):
                AppendBulkUtf8ToUtf16(value);
                break;

            case (TextEncoding.Utf32, TextEncoding.Utf8):
                AppendBulkUtf32ToUtf8(value);
                break;

            case (TextEncoding.Utf32, TextEncoding.Utf16):
                AppendBulkUtf32ToUtf16(value);
                break;

            case (TextEncoding.Utf8, TextEncoding.Utf32):
                AppendBulkUtf8ToUtf32(value);
                break;

            case (TextEncoding.Utf16, TextEncoding.Utf32):
                AppendBulkUtf16ToUtf32(value);
                break;

            default:
                foreach (var rune in value.EnumerateRunes())
                {
                    AppendRune(rune);
                }

                break;
        }
    }

    // Append — string / spans

    /// <summary>Appends a string.</summary>
    /// <param name="value">The string to append.</param>
    public void Append(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        Append(MemoryMarshal.AsBytes(value.AsUnsafeSpan()), TextEncoding.Utf16);
    }

    /// <summary>Appends raw bytes in the specified encoding.</summary>
    /// <param name="value">The bytes to append.</param>
    /// <param name="encoding">The encoding of the bytes.</param>
    public void Append(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (encoding == Encoding)
        {
            AppendBytes(value);
            RuneLength += RuneCount.Count(value, encoding);
            return;
        }

        Append(new TextSpan(value, encoding));
    }

    /// <summary>Appends UTF-16 chars.</summary>
    /// <param name="value">The characters to append.</param>
    public void Append(ReadOnlySpan<char> value)
        => Append(MemoryMarshal.AsBytes(value), TextEncoding.Utf16);

    // Append — single rune

    /// <summary>Appends a single Unicode rune in the target encoding.</summary>
    /// <param name="rune">The rune to append.</param>
    public void AppendRune(Rune rune)
    {
        var byteCount = rune.GetByteCount(Encoding);
        EnsureCapacity(ByteLength + byteCount);
        rune.EncodeTo(_buffer.AsSpan(ByteLength), Encoding);
        ByteLength += byteCount;
        RuneLength++;
    }

    /// <summary>Appends a newline (<c>\n</c>) in the target encoding.</summary>
    public void AppendLine() => AppendRune(new Rune('\n'));

    // Output

    /// <summary>Creates a <see cref="Text"/> from the current content.</summary>
    /// <returns>A <see cref="Text"/> containing the builder's content, or <see cref="Text.Empty"/> if empty.</returns>
    /// <remarks>Allocates a new exact-size <c>byte[]</c> and copies the builder's content into it. The builder remains usable after this call.</remarks>
    public readonly Text ToText()
    {
        if (ByteLength == 0)
        {
            return Text.Empty;
        }

        var bytes = new byte[ByteLength];
        _buffer.AsSpan(0, ByteLength).CopyTo(bytes);
        return new Text(
            bytes,
            0,
            ByteLength,
            Encoding,
            RuneLength,
            BackingType.ByteArray);
    }

    /// <summary>Creates an <see cref="OwnedText"/> from the current content.</summary>
    /// <returns>An <see cref="OwnedText"/> containing the builder's content, or <see cref="OwnedText.Empty"/> if empty.</returns>
    /// <remarks>Transfers ownership of the current buffer to the <see cref="OwnedText"/>. The builder rents a fresh buffer and resets for continued use.</remarks>
    public OwnedText ToOwnedText()
    {
        if (ByteLength == 0)
        {
            return OwnedText.Empty;
        }

        var result = OwnedText.Create(
            _buffer,
            ByteLength,
            Encoding,
            RuneLength);

        _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);
        ByteLength = 0;
        RuneLength = 0;
        return result;
    }

    /// <summary>Returns a <see cref="TextSpan"/> view of the current content.</summary>
    /// <returns>A <see cref="TextSpan"/> over the builder's current bytes.</returns>
    /// <remarks>The returned span references the builder's internal buffer. It is invalidated by any subsequent mutation (Append, AppendRune, Clear, Dispose).</remarks>
    public readonly TextSpan AsSpan()
        => new(_buffer.AsSpan(0, ByteLength), Encoding, RuneLength);

    /// <summary>Converts the current content to a string.</summary>
    /// <returns>A string representation of the builder's current content.</returns>
    public override readonly string ToString() => AsSpan().ToString();

    // Lifecycle

    /// <summary>Resets the builder to empty, keeping the current buffer for reuse.</summary>
    public void Clear()
    {
        ByteLength = 0;
        RuneLength = 0;
    }

    /// <summary>Releases resources used by this builder.</summary>
    /// <remarks>Returns the internal buffer to <see cref="System.Buffers.ArrayPool{T}"/>. The builder must not be used after disposal.</remarks>
    public void Dispose()
    {
        var buffer = _buffer;
        _buffer = null!;
        ByteLength = 0;
        RuneLength = 0;

        if (buffer is not null)
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    // Internal

    void AppendBytes(ReadOnlySpan<byte> bytes)
    {
        EnsureCapacity(ByteLength + bytes.Length);
        bytes.CopyTo(_buffer.AsSpan(ByteLength));
        ByteLength += bytes.Length;
    }

    /// <summary>
    /// Appends raw bytes that are already in the builder's encoding, with a pre-computed rune count.
    /// Callers must ensure the encoding matches and the rune count is correct.
    /// </summary>
    internal void AppendCounted(ReadOnlySpan<byte> value, int runeCount)
    {
        AppendBytes(value);
        RuneLength += runeCount;
    }

    void AppendBulkUtf16ToUtf8(TextSpan value)
    {
        AppendCharsAsUtf8(value.Chars);
        RuneLength += value.RuneLength;
    }

    void AppendBulkUtf8ToUtf16(TextSpan value)
    {
        // Max 1 char (2 bytes) per UTF-8 byte.
        EnsureCapacity(ByteLength + (value.Bytes.Length * 2));
        var charDest = MemoryMarshal.Cast<byte, char>(_buffer.AsSpan(ByteLength));
#if NET6_0_OR_GREATER
        Utf8.ToUtf16(value.Bytes, charDest, out _, out var charsWritten);
#else
        var charsWritten = UTF8.GetChars(value.Bytes, charDest);
#endif
        ByteLength += charsWritten * 2;
        RuneLength += value.RuneLength;
    }

    /// <summary>Converts UTF-16 chars to UTF-8 and appends to the builder buffer. Max 3 UTF-8 bytes per char.</summary>
    void AppendCharsAsUtf8(ReadOnlySpan<char> chars)
    {
        EnsureCapacity(ByteLength + (chars.Length * 3));
#if NET6_0_OR_GREATER
        Utf8.FromUtf16(chars, _buffer.AsSpan(ByteLength), out _, out var bytesWritten);
#else
        var bytesWritten = UTF8.GetBytes(chars, _buffer.AsSpan(ByteLength));
#endif
        ByteLength += bytesWritten;
    }

    // Direct UTF-32 encoders: BCL Encoding.UTF32 is scalar (no SIMD), and Rune.EncodeTo
    // adds per-call method overhead. Inlined branches on code-point range are ~20× faster.

    void AppendBulkUtf32ToUtf8(TextSpan value)
    {
        var ints = value.Ints;
        EnsureCapacity(ByteLength + (ints.Length * sizeof(int)));
        var dest = _buffer.AsSpan(ByteLength);
        var w = 0;
        for (var i = 0; i < ints.Length; i++)
        {
            var cp = (uint)ints[i];
            if (cp < Utf8TwoByteThreshold)
            {
                dest[w++] = (byte)cp;
            }
            else if (cp < Utf8ThreeByteThreshold)
            {
                dest[w++] = (byte)(Utf8TwoByteLead | (cp >> 6));
                dest[w++] = (byte)(Utf8ContinuationMarker | (cp & Utf8ContinuationMask));
            }
            else if (cp < SupplementaryPlaneStart)
            {
                dest[w++] = (byte)(Utf8ThreeByteLead | (cp >> 12));
                dest[w++] = (byte)(Utf8ContinuationMarker | ((cp >> 6) & Utf8ContinuationMask));
                dest[w++] = (byte)(Utf8ContinuationMarker | (cp & Utf8ContinuationMask));
            }
            else
            {
                dest[w++] = (byte)(Utf8FourByteLead | (cp >> 18));
                dest[w++] = (byte)(Utf8ContinuationMarker | ((cp >> 12) & Utf8ContinuationMask));
                dest[w++] = (byte)(Utf8ContinuationMarker | ((cp >> 6) & Utf8ContinuationMask));
                dest[w++] = (byte)(Utf8ContinuationMarker | (cp & Utf8ContinuationMask));
            }
        }

        ByteLength += w;
        RuneLength += ints.Length;
    }

    void AppendBulkUtf32ToUtf16(TextSpan value)
    {
        var ints = value.Ints;
        EnsureCapacity(ByteLength + (ints.Length * sizeof(int)));
        var dest = MemoryMarshal.Cast<byte, char>(_buffer.AsSpan(ByteLength));
        var w = 0;
        for (var i = 0; i < ints.Length; i++)
        {
            var cp = (uint)ints[i];
            if (cp < SupplementaryPlaneStart)
            {
                dest[w++] = (char)cp;
            }
            else
            {
                cp -= SupplementaryPlaneStart;
                dest[w++] = (char)(Utf16HighSurrogateMarker | (cp >> Utf16SurrogateBits));
                dest[w++] = (char)(Utf16LowSurrogateMarker | (cp & Utf16LowTenBitsMask));
            }
        }

        ByteLength += w * sizeof(char);
        RuneLength += ints.Length;
    }

    void AppendBulkUtf8ToUtf32(TextSpan value)
    {
        var src = value.Bytes;
        // Worst case: 1 UTF-8 byte (ASCII) → 4 UTF-32 bytes.
        EnsureCapacity(ByteLength + (src.Length * sizeof(int)));
        var dest = MemoryMarshal.Cast<byte, int>(_buffer.AsSpan(ByteLength));
        var r = 0;
        var w = 0;
        while (r < src.Length)
        {
            uint b0 = src[r];
            int cp;
            if (b0 < Utf8TwoByteThreshold)
            {
                cp = (int)b0;
                r += 1;
            }
            else if (b0 < Utf8ThreeByteLead)
            {
                cp = (int)(((b0 & Utf8TwoByteMask) << 6) | (src[r + 1] & Utf8ContinuationMask));
                r += 2;
            }
            else if (b0 < Utf8FourByteLead)
            {
                cp = (int)(((b0 & Utf8ThreeByteMask) << 12) | ((src[r + 1] & Utf8ContinuationMask) << 6) | (src[r + 2] & Utf8ContinuationMask));
                r += 3;
            }
            else
            {
                cp = (int)(((b0 & Utf8FourByteMask) << 18) | ((src[r + 1] & Utf8ContinuationMask) << 12) | ((src[r + 2] & Utf8ContinuationMask) << 6) | (src[r + 3] & Utf8ContinuationMask));
                r += 4;
            }

            dest[w++] = cp;
        }

        ByteLength += w * sizeof(int);
        RuneLength += w;
    }

    void AppendBulkUtf16ToUtf32(TextSpan value)
    {
        var chars = value.Chars;
        // Worst case: 1 UTF-16 char → 4 UTF-32 bytes (surrogate pairs compress 2:1).
        EnsureCapacity(ByteLength + (chars.Length * sizeof(int)));
        var dest = MemoryMarshal.Cast<byte, int>(_buffer.AsSpan(ByteLength));
        var r = 0;
        var w = 0;
        while (r < chars.Length)
        {
            int c = chars[r];
            if ((c & Utf16SurrogateMask) == Utf16HighSurrogateMarker && r + 1 < chars.Length)
            {
                int lo = chars[r + 1];
                dest[w++] = (int)SupplementaryPlaneStart + ((c - Utf16HighSurrogateMarker) << Utf16SurrogateBits) + (lo - Utf16LowSurrogateMarker);
                r += 2;
            }
            else
            {
                dest[w++] = c;
                r += 1;
            }
        }

        ByteLength += w * sizeof(int);
        RuneLength += w;
    }

    void EnsureCapacity(int required)
    {
        if (required <= _buffer.Length)
        {
            return;
        }

        var newSize = Math.Max((int)Math.Min((long)_buffer.Length * 2, int.MaxValue), required);
        var newBuffer = ArrayPool<byte>.Shared.Rent(newSize);
        _buffer.AsSpan(0, ByteLength).CopyTo(newBuffer);
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = newBuffer;
    }
}
