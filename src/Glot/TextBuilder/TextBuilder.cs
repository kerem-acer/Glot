using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
#if NET6_0_OR_GREATER
using System.Text.Unicode;
#else
using static System.Text.Encoding;
#endif

namespace Glot;

/// <summary>
/// A mutable, pooled builder for constructing <see cref="Text"/> or <see cref="OwnedText"/> values.
/// Accumulates encoded bytes in a target encoding. Appends from any encoding are transcoded automatically.
/// </summary>
/// <remarks>
/// This is a value type. Use <c>using</c> to ensure the pooled buffer is returned.
/// Do not copy — copies share the same buffer.
/// </remarks>
public struct TextBuilder : IDisposable
{
    const int DefaultCapacity = 256;

    byte[] _buffer;

    /// <summary>Creates a builder with the specified target encoding. Uses default initial capacity of 256 bytes.</summary>
    public TextBuilder(TextEncoding encoding)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);
        ByteLength = 0;
        RuneLength = 0;
        Encoding = encoding;
    }

    /// <summary>Creates a builder with the specified initial capacity and target encoding.</summary>
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
    public int RuneLength { get; private set; }

    /// <summary>Returns <c>true</c> if no content has been written.</summary>
    public readonly bool IsEmpty => ByteLength == 0;

    // Append — Text / TextSpan

    /// <summary>Appends the content of a <see cref="Text"/> value, transcoding if needed.</summary>
    public void Append(Text value) => Append(value.AsSpan());

    /// <summary>Appends the content of a <see cref="TextSpan"/>, transcoding if needed.</summary>
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

            default:
                // UTF-32 paths: rune-by-rune (BCL Encoding.UTF32 is scalar, no SIMD benefit).
                foreach (var rune in value.EnumerateRunes())
                {
                    AppendRune(rune);
                }

                break;
        }
    }

    // Append — string / spans

    /// <summary>Appends a string, transcoding from UTF-16 to the target encoding.</summary>
    public void Append(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        Append(MemoryMarshal.AsBytes(value.AsSpan()), TextEncoding.Utf16);
    }

    /// <summary>Appends raw bytes in the specified encoding, transcoding if needed.</summary>
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

    /// <summary>Appends UTF-16 chars, transcoding to the target encoding.</summary>
    public void Append(ReadOnlySpan<char> value)
        => Append(MemoryMarshal.AsBytes(value), TextEncoding.Utf16);

    // Append — single rune

    /// <summary>Appends a single Unicode rune in the target encoding.</summary>
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

    /// <summary>Creates a <see cref="Text"/> by copying the current content to an exact-size array.</summary>
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
            RuneLength);
    }

    /// <summary>
    /// Creates an <see cref="OwnedText"/> by transferring the current buffer.
    /// The builder resets and rents a fresh buffer for continued use.
    /// </summary>
    public OwnedText? ToOwnedText()
    {
        if (ByteLength == 0)
        {
            return null;
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

    /// <summary>Returns a <see cref="TextSpan"/> view of the current content. Valid until the next mutation.</summary>
    public readonly TextSpan AsSpan()
        => new(_buffer.AsSpan(0, ByteLength), Encoding, RuneLength);

    /// <summary>Converts the current content to a string.</summary>
    public override readonly string ToString() => AsSpan().ToString();

    // Lifecycle

    /// <summary>Resets the builder to empty, keeping the current buffer for reuse.</summary>
    public void Clear()
    {
        ByteLength = 0;
        RuneLength = 0;
    }

    /// <summary>Returns the pooled buffer to <see cref="ArrayPool{T}"/>.</summary>
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
