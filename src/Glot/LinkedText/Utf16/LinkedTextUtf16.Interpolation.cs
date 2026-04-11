#if NET6_0_OR_GREATER
using System.ComponentModel;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    /// <summary>Handler constructor for interpolated string support.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public LinkedTextUtf16(int literalLength, int formattedCount)
    {
        EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Appends a literal string segment. Zero-copy.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendLiteral(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            AddSegment(value.AsMemory());
        }
    }

    /// <summary>Appends a string value. Zero-copy.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            AddSegment(value.AsMemory());
        }
    }

    /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment. Zero-copy.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(ReadOnlyMemory<char> value)
    {
        if (!value.IsEmpty)
        {
            AddSegment(value);
        }
    }

    /// <summary>Appends a <see cref="Text"/> value. Zero-copy when already UTF-16, transcodes otherwise.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(Text value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (value.TryGetUtf16Memory(out var memory))
        {
            AddSegment(memory);
        }
        else
        {
            AppendTextSpan(value.AsSpan());
        }
    }

    /// <summary>Appends a <see cref="TextSpan"/> value. Transcodes to UTF-16 if needed.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AppendFormatted(TextSpan value) => AppendTextSpan(value);

    /// <summary>Appends any formattable value. Zero-alloc for <see cref="ISpanFormattable"/>.</summary>
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

        if (value is ISpanFormattable spanFormattable)
        {
            EnsureFormatBuffer(256);

            while (true)
            {
                var dest = _formatBuffer.AsSpan(_formatPosition);
                if (spanFormattable.TryFormat(dest, out var written, format, null))
                {
                    AddFormattedSegment(written);
                    return;
                }

                EnsureFormatBuffer(dest.Length * 2);
            }
        }

        var str = value is IFormattable f
            ? f.ToString(format, null)
            : value?.ToString();

        if (!string.IsNullOrEmpty(str))
        {
            AddSegment(str.AsMemory());
        }
    }
}
#endif
