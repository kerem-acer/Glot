using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

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
    int _position;
    int _runeCount;
    readonly TextEncoding _encoding;

    /// <summary>Creates a builder with the specified target encoding. Uses default initial capacity of 256 bytes.</summary>
    public TextBuilder(TextEncoding encoding)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);
        _position = 0;
        _runeCount = 0;
        _encoding = encoding;
    }

    /// <summary>Creates a builder with the specified initial capacity and target encoding.</summary>
    public TextBuilder(int initialCapacity, TextEncoding encoding = TextEncoding.Utf8)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
        _position = 0;
        _runeCount = 0;
        _encoding = encoding;
    }

    /// <summary>The target encoding for this builder.</summary>
    public readonly TextEncoding Encoding => _encoding;

    /// <summary>The number of bytes written so far.</summary>
    public readonly int ByteLength => _position;

    /// <summary>The number of runes written so far.</summary>
    public readonly int RuneLength => _runeCount;

    /// <summary>Returns <c>true</c> if no content has been written.</summary>
    public readonly bool IsEmpty => _position == 0;

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

        if (value.Encoding == _encoding)
        {
            AppendBytes(value.Bytes);
            _runeCount += value.RuneLength;
            return;
        }

        foreach (var rune in value.EnumerateRunes())
        {
            AppendRune(rune);
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

        Append(new TextSpan(
            MemoryMarshal.AsBytes(value.AsSpan()), TextEncoding.Utf16, 0));
    }

    /// <summary>Appends raw bytes in the specified encoding, transcoding if needed.</summary>
    public void Append(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (encoding == _encoding)
        {
            AppendBytes(value);
            _runeCount += RuneCount.Count(value, encoding);
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
        var byteCount = rune.GetByteCount(_encoding);
        EnsureCapacity(_position + byteCount);
        rune.EncodeTo(_buffer.AsSpan(_position), _encoding);
        _position += byteCount;
        _runeCount++;
    }

    /// <summary>Appends a newline (<c>\n</c>) in the target encoding.</summary>
    public void AppendLine() => AppendRune(new Rune('\n'));

    // Output

    /// <summary>Creates a <see cref="Text"/> by copying the current content to an exact-size array.</summary>
    public readonly Text ToText()
    {
        if (_position == 0)
        {
            return Text.Empty;
        }

        var bytes = new byte[_position];
        _buffer.AsSpan(0, _position).CopyTo(bytes);
        return new Text(bytes, 0, _position, _encoding, _runeCount);
    }

    /// <summary>
    /// Creates an <see cref="OwnedText"/> by transferring the current buffer.
    /// The builder resets and rents a fresh buffer for continued use.
    /// </summary>
    public OwnedText ToOwnedText()
    {
        if (_position == 0)
        {
            return default;
        }

        var result = OwnedText.Create(_buffer, _position, _encoding);
        _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);
        _position = 0;
        _runeCount = 0;
        return result;
    }

    /// <summary>Returns a <see cref="TextSpan"/> view of the current content. Valid until the next mutation.</summary>
    public readonly TextSpan AsSpan()
        => new(_buffer.AsSpan(0, _position), _encoding, _runeCount);

    /// <summary>Converts the current content to a string.</summary>
    public override readonly string ToString() => AsSpan().ToString();

    // Lifecycle

    /// <summary>Resets the builder to empty, keeping the current buffer for reuse.</summary>
    public void Clear()
    {
        _position = 0;
        _runeCount = 0;
    }

    /// <summary>Returns the pooled buffer to <see cref="ArrayPool{T}"/>.</summary>
    public void Dispose()
    {
        var buffer = _buffer;
        _buffer = null!;
        _position = 0;
        _runeCount = 0;

        if (buffer is not null)
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    // Internal

    void AppendBytes(ReadOnlySpan<byte> bytes)
    {
        EnsureCapacity(_position + bytes.Length);
        bytes.CopyTo(_buffer.AsSpan(_position));
        _position += bytes.Length;
    }

    void EnsureCapacity(int required)
    {
        if (required <= _buffer.Length)
        {
            return;
        }

        var newSize = Math.Max(_buffer.Length * 2, required);
        var newBuffer = ArrayPool<byte>.Shared.Rent(newSize);
        _buffer.AsSpan(0, _position).CopyTo(newBuffer);
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = newBuffer;
    }
}
