#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

/// <summary>
/// An interpolation handler that builds <see cref="Text"/> directly from interpolated strings
/// without allocating intermediate strings. Wraps <see cref="TextBuilder"/> internally.
/// </summary>
[InterpolatedStringHandler]
public ref struct TextInterpolatedStringHandler : IDisposable
{
    TextBuilder _builder;

    /// <summary>Creates a handler with UTF-8 target encoding.</summary>
    public TextInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _builder = new TextBuilder(Math.Max(literalLength + formattedCount * 16, 64), TextEncoding.Utf8);
    }

    /// <summary>Creates a handler with the specified target encoding.</summary>
    public TextInterpolatedStringHandler(int literalLength, int formattedCount, TextEncoding encoding)
    {
        _builder = new TextBuilder(Math.Max(literalLength + formattedCount * 16, 64), encoding);
    }

    // Literal parts

    /// <summary>Appends a literal string segment.</summary>
    public void AppendLiteral(string value) => _builder.Append(value);

    // Formatted holes — special Glot types (no ToString)

    /// <summary>Appends a <see cref="Text"/> value directly (no ToString, cross-encoding aware).</summary>
    public void AppendFormatted(Text value) => _builder.Append(value);

    /// <summary>Appends a <see cref="TextSpan"/> value directly (no ToString, cross-encoding aware).</summary>
    public void AppendFormatted(TextSpan value) => _builder.Append(value);

    /// <summary>Appends a string value.</summary>
    public void AppendFormatted(string? value)
    {
        if (value is not null)
        {
            _builder.Append(value);
        }
    }

    /// <summary>Appends a char span.</summary>
    public void AppendFormatted(ReadOnlySpan<char> value) => _builder.Append(value);

    // Formatted holes — generic

    /// <summary>Appends a formatted value. Uses IUtf8SpanFormattable, <see cref="ISpanFormattable"/>, or <see cref="IFormattable"/> when available.</summary>
    public void AppendFormatted<T>(T value) => AppendFormattedCore(value, 0, null);

    /// <summary>Appends a formatted value with a format specifier.</summary>
    public void AppendFormatted<T>(T value, string? format) => AppendFormattedCore(value, 0, format);

    /// <summary>Appends a formatted value with alignment.</summary>
    public void AppendFormatted<T>(T value, int alignment) => AppendFormattedCore(value, alignment, null);

    /// <summary>Appends a formatted value with alignment and format specifier.</summary>
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

    /// <summary>Returns the builder's pooled buffer.</summary>
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
        for (var i = 0; i < count; i++)
        {
            _builder.AppendRune(new System.Text.Rune(' '));
        }
    }
}
#endif
