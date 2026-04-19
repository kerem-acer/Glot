#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

/// <summary>
/// An interpolation handler that builds <see cref="Text"/> from interpolated strings.
/// </summary>
/// <remarks>Uses a pooled <see cref="TextBuilder"/> internally. Prefer <see cref="Text.Create(TextInterpolatedStringHandler)"/> or <see cref="OwnedText.Create(TextInterpolatedStringHandler)"/> over using this handler directly.</remarks>
[InterpolatedStringHandler]
public struct TextInterpolatedStringHandler : IDisposable
{
    TextBuilder _builder;

    /// <summary>Creates a handler with UTF-8 target encoding.</summary>
    /// <param name="literalLength">The total length of all literal string segments in the interpolated string.</param>
    /// <param name="formattedCount">The number of formatted holes in the interpolated string.</param>
    public TextInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _builder = new TextBuilder(Math.Max(literalLength + formattedCount * 16, 64), TextEncoding.Utf8);
    }

    /// <summary>Creates a handler with the specified target encoding.</summary>
    /// <param name="literalLength">The total length of all literal string segments in the interpolated string.</param>
    /// <param name="formattedCount">The number of formatted holes in the interpolated string.</param>
    /// <param name="encoding">The target encoding for the resulting <see cref="Text"/>.</param>
    public TextInterpolatedStringHandler(int literalLength, int formattedCount, TextEncoding encoding)
    {
        _builder = new TextBuilder(Math.Max(literalLength + formattedCount * 16, 64), encoding);
    }

    // Literal parts

    /// <summary>Appends a literal string segment.</summary>
    /// <param name="value">The literal string to append.</param>
    public void AppendLiteral(string value) => _builder.Append(value);

    // Formatted holes — special Glot types (no ToString)

    /// <summary>Appends a <see cref="Text"/> value.</summary>
    /// <param name="value">The <see cref="Text"/> to append.</param>
    public void AppendFormatted(Text value) => _builder.Append(value);

    /// <summary>Appends a <see cref="TextSpan"/> value.</summary>
    /// <param name="value">The <see cref="TextSpan"/> to append.</param>
    public void AppendFormatted(TextSpan value) => _builder.Append(value);

    /// <summary>Appends an <see cref="OwnedText"/> value.</summary>
    /// <param name="value">The <see cref="OwnedText"/> to append.</param>
    public void AppendFormatted(OwnedText? value)
    {
        if (value is not null)
        {
            _builder.Append(value.Text);
        }
    }

    /// <summary>Appends a string value.</summary>
    /// <param name="value">The string to append.</param>
    public void AppendFormatted(string? value)
    {
        if (value is not null)
        {
            _builder.Append(value);
        }
    }

    /// <summary>Appends a char span.</summary>
    /// <param name="value">The character span to append.</param>
    public void AppendFormatted(ReadOnlySpan<char> value) => _builder.Append(value);

    // Formatted holes — generic

    /// <summary>Appends a formatted value.</summary>
    /// <param name="value">The value to format and append.</param>
    public void AppendFormatted<T>(T value) => AppendFormattedCore(value, 0, null);

    /// <summary>Appends a formatted value with a format specifier.</summary>
    /// <param name="value">The value to format and append.</param>
    /// <param name="format">The format specifier.</param>
    public void AppendFormatted<T>(T value, string? format) => AppendFormattedCore(value, 0, format);

    /// <summary>Appends a formatted value with alignment.</summary>
    /// <param name="value">The value to format and append.</param>
    /// <param name="alignment">The minimum field width; positive for right-aligned, negative for left-aligned.</param>
    public void AppendFormatted<T>(T value, int alignment) => AppendFormattedCore(value, alignment, null);

    /// <summary>Appends a formatted value with alignment and format specifier.</summary>
    /// <param name="value">The value to format and append.</param>
    /// <param name="alignment">The minimum field width; positive for right-aligned, negative for left-aligned.</param>
    /// <param name="format">The format specifier.</param>
    public void AppendFormatted<T>(T value, int alignment, string? format) => AppendFormattedCore(value, alignment, format);

    void AppendFormattedCore<T>(T value, int alignment, string? format)
    {
#if NET8_0_OR_GREATER
        // Fast path: IUtf8SpanFormattable — format directly into UTF-8 bytes (no char buffer)
        if (_builder.Encoding == TextEncoding.Utf8 && value is IUtf8SpanFormattable utf8Formattable)
        {
            Span<byte> buffer = stackalloc byte[256];
            if (utf8Formattable.TryFormat(buffer, out var bytesWritten, format, null))
            {
                if (alignment == 0)
                {
                    _builder.Append(buffer[..bytesWritten]);
                    return;
                }

                var runeLen = RuneCount.Count(buffer[..bytesWritten], TextEncoding.Utf8);
                var padTotal = Math.Abs(alignment) - runeLen;
                if (padTotal <= 0)
                {
                    _builder.Append(buffer[..bytesWritten]);
                }
                else if (alignment > 0)
                {
                    AppendSpaces(padTotal);
                    _builder.Append(buffer[..bytesWritten]);
                }
                else
                {
                    _builder.Append(buffer[..bytesWritten]);
                    AppendSpaces(padTotal);
                }

                return;
            }
            // Didn't fit in 256 bytes — fall through to char-based path
        }
#endif

        // ISpanFormattable — format into a char buffer on the stack (no string alloc)
        if (value is ISpanFormattable spanFormattable)
        {
            Span<char> buffer = stackalloc char[256];
            if (spanFormattable.TryFormat(buffer, out var charsWritten, format, null))
            {
                if (alignment == 0)
                {
                    _builder.Append(buffer[..charsWritten]);
                    return;
                }

                var padTotal = Math.Abs(alignment) - charsWritten;
                if (padTotal <= 0)
                {
                    _builder.Append(buffer[..charsWritten]);
                }
                else if (alignment > 0)
                {
                    AppendSpaces(padTotal);
                    _builder.Append(buffer[..charsWritten]);
                }
                else
                {
                    _builder.Append(buffer[..charsWritten]);
                    AppendSpaces(padTotal);
                }

                return;
            }
            // Didn't fit in 256 chars — fall through to ToString
        }

        // IFormattable — ToString(format, provider)
        // Fallback — ToString()
        var str = value is IFormattable f
            ? f.ToString(format, null)
            : value?.ToString();

        var span = (str ?? "").AsSpan();
        if (alignment == 0)
        {
            _builder.Append(span);
        }
        else
        {
            AppendAligned(span, alignment);
        }
    }

    /// <summary>Appends a char span with alignment.</summary>
    /// <param name="value">The character span to append.</param>
    /// <param name="alignment">The minimum field width; positive for right-aligned, negative for left-aligned.</param>
    /// <param name="format">The format specifier (unused for spans).</param>
    public void AppendFormatted(ReadOnlySpan<char> value, int alignment, string? format = null)
        => AppendAligned(value, alignment);

    // Output

    internal Text ToText()
    {
        var result = _builder.ToText();
        _builder.Dispose();
        return result;
    }

    internal OwnedText ToOwnedText()
    {
        var result = _builder.ToOwnedText();
        _builder.Dispose();
        return result;
    }

    /// <summary>Releases resources used by this handler.</summary>
    public void Dispose() => _builder.Dispose();

    // Internal

    void AppendAligned(ReadOnlySpan<char> value, int alignment)
    {
        var padTotal = Math.Abs(alignment) - value.Length;
        if (padTotal <= 0)
        {
            _builder.Append(value);
            return;
        }

        if (alignment > 0)
        {
            AppendSpaces(padTotal);
            _builder.Append(value);
        }
        else
        {
            _builder.Append(value);
            AppendSpaces(padTotal);
        }
    }

    void AppendSpaces(int count)
    {
        const int maxBatch = 256;

        switch (_builder.Encoding)
        {
            case TextEncoding.Utf8:
            {
                Span<byte> buf = stackalloc byte[Math.Min(count, maxBatch)];
                buf.Fill((byte)' ');
                var remaining = count;
                while (remaining > 0)
                {
                    var batch = Math.Min(remaining, buf.Length);
                    _builder.Append(buf[..batch]);
                    remaining -= batch;
                }
                break;
            }

            case TextEncoding.Utf16:
            {
                Span<char> buf = stackalloc char[Math.Min(count, maxBatch / 2)];
                buf.Fill(' ');
                var remaining = count;
                while (remaining > 0)
                {
                    var batch = Math.Min(remaining, buf.Length);
                    _builder.Append(buf[..batch]);
                    remaining -= batch;
                }
                break;
            }

            case TextEncoding.Utf32:
            {
                Span<int> buf = stackalloc int[Math.Min(count, maxBatch / 4)];
                buf.Fill(' ');
                var remaining = count;
                while (remaining > 0)
                {
                    var batch = Math.Min(remaining, buf.Length);
                    _builder.Append(
                        System.Runtime.InteropServices.MemoryMarshal.AsBytes(buf[..batch]),
                        TextEncoding.Utf32);
                    remaining -= batch;
                }
                break;
            }
        }
    }
}
#endif
