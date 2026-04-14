#if NET8_0_OR_GREATER
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    /// <summary>Handler constructor for interpolated string support.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public LinkedTextUtf8(int literalLength, int formattedCount)
    {
        EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Appends a literal string segment. Encodes to UTF-8 into the format buffer.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendLiteral(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        var maxBytes = Encoding.UTF8.GetMaxByteCount(value.Length);
        EnsureFormatBuffer(maxBytes);
        var written = Encoding.UTF8.GetBytes(value, _formatBuffer.AsSpan(_formatPosition));
        AddFormattedSegment(written);
    }

    /// <summary>Appends a string value. Encodes to UTF-8 into the format buffer.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(string? value) => AppendLiteral(value!);

    /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment. Zero-copy.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(ReadOnlyMemory<byte> value)
    {
        if (!value.IsEmpty)
        {
            AddSegment(value);
        }
    }

    /// <summary>Appends a <see cref="Text"/> value. Zero-copy when UTF-8 backed, transcodes otherwise.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(Text value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (value.TryGetUtf8Memory(out var memory))
        {
            AddSegment(memory);
        }
        else
        {
            AppendTextSpan(value.AsSpan());
        }
    }

    /// <summary>
    /// Appends an <see cref="OwnedText"/> value. Behavior depends on <see cref="OwnedTextHandling"/>:
    /// <see cref="Glot.OwnedTextHandling.Copy"/> copies data into the format buffer (safe default).
    /// <see cref="Glot.OwnedTextHandling.TakeOwnership"/> detaches the buffer for zero-copy ownership transfer.
    /// <see cref="Glot.OwnedTextHandling.Borrow"/> references the buffer without ownership (caller retains lifetime).
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(OwnedText? value)
    {
        if (value is null or { IsEmpty: true })
        {
            return;
        }

        switch (OwnedTextHandling)
        {
            case OwnedTextHandling.Copy:
                // Safe default: copy into format buffer. No lifetime coupling.
                AppendTextSpan(value.Text.AsSpan());
                break;

            case OwnedTextHandling.TakeOwnership:
                // Capture text view before detaching (Detach nulls the buffer).
                var text = value.Text;
                var detached = value.Detach();

                // Zero-copy: detach the buffer from OwnedText so its Dispose becomes a no-op.
                if (text.TryGetUtf8Memory(out var memory)
                    && MemoryMarshal.TryGetArray(memory, out var segment))
                {
                    AddSegment(memory, segment.Array);
                }
                else
                {
                    // Encoding mismatch: transcode into format buffer, return detached buffer.
                    AppendTextSpan(text.AsSpan());
                    ReturnDetachedBuffer(detached);
                }
                break;

            case OwnedTextHandling.Borrow:
                // Zero-copy: reference the buffer without ownership. Caller keeps it alive.
                AppendFormatted(value.Text);
                break;
        }
    }

    static void ReturnDetachedBuffer(object? buffer)
    {
        switch (buffer)
        {
            case byte[] bytes:
                System.Buffers.ArrayPool<byte>.Shared.Return(bytes);
                break;
            case char[] chars:
                System.Buffers.ArrayPool<char>.Shared.Return(chars);
                break;
            case int[] ints:
                System.Buffers.ArrayPool<int>.Shared.Return(ints);
                break;
        }
    }

    /// <summary>Appends a <see cref="TextSpan"/> value. Transcodes to UTF-8 if needed.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(TextSpan value) => AppendTextSpan(value);

    /// <summary>Appends any formattable value. Zero-alloc for <see cref="IUtf8SpanFormattable"/>.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted<T>(T value) => AppendFormattedCore(value, null);

    /// <summary>Appends a formattable value with format specifier.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted<T>(T value, string? format) => AppendFormattedCore(value, format);

    void AppendFormattedCore<T>(T value, string? format)
    {
        if (value is string s)
        {
            AppendFormatted(s);
            return;
        }

        if (value is IUtf8SpanFormattable utf8Formattable)
        {
            EnsureFormatBuffer(256);

            while (true)
            {
                var dest = _formatBuffer.AsSpan(_formatPosition);
                if (utf8Formattable.TryFormat(dest, out var written, format, null))
                {
                    AddFormattedSegment(written);
                    return;
                }

                EnsureFormatBuffer(dest.Length * 2);
            }
        }

        if (value is ISpanFormattable spanFormattable)
        {
            Span<char> charBuf = stackalloc char[256];
            if (spanFormattable.TryFormat(charBuf, out var charsWritten, format, null))
            {
                var maxBytes = Encoding.UTF8.GetMaxByteCount(charsWritten);
                EnsureFormatBuffer(maxBytes);
                var written = Encoding.UTF8.GetBytes(charBuf[..charsWritten], _formatBuffer.AsSpan(_formatPosition));
                AddFormattedSegment(written);
                return;
            }
        }

        var str = value is IFormattable f
            ? f.ToString(format, null)
            : value?.ToString();

        if (!string.IsNullOrEmpty(str))
        {
            AppendLiteral(str);
        }
    }
}
#endif
