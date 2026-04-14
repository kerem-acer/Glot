using System.Buffers;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Glot;

/// <summary>
/// An immutable text value composed of non-contiguous UTF-16 segments.
/// Holds <see cref="ReadOnlyMemory{T}"/> references to the original data
/// without copying character data.
/// </summary>
#if NET6_0_OR_GREATER
[InterpolatedStringHandler]
#endif
public sealed partial class LinkedTextUtf16
{
#if NET8_0_OR_GREATER
    InlineSegmentBuffer _inlineSegments;
#endif
    Segment[]? _overflowSegments;
    char[]? _formatBuffer;
    int _formatPosition;

    LinkedTextUtf16() { }

    /// <summary>Controls how <see cref="OwnedText"/> values are handled during interpolation.</summary>
    internal OwnedTextHandling OwnedTextHandling { get; set; }

    /// <summary>The number of segments in this linked text.</summary>
    public int SegmentCount { get; private set; }

    /// <summary>The total number of chars across all segments.</summary>
    public int Length { get; private set; }

    /// <summary>Returns <c>true</c> if this linked text has no content.</summary>
    public bool IsEmpty => Length == 0;

    /// <summary>An empty <see cref="LinkedTextUtf16"/>.</summary>
    public static LinkedTextUtf16 Empty { get; } = new();

    /// <summary>Gets the memory at the specified segment index.</summary>
    internal ReadOnlyMemory<char> GetSegment(int index) => GetSegmentEntry(index).Memory;

    internal Segment GetSegmentEntry(int index)
    {
#if NET8_0_OR_GREATER
        if (index < InlineCapacity)
        {
            return _inlineSegments[index];
        }
#endif
        return _overflowSegments![index - InlineCapacity];
    }

    void EnsureFormatBuffer(int additionalChars)
    {
        var required = _formatPosition + additionalChars;

        if (_formatBuffer is not null && required <= _formatBuffer.Length)
        {
            return;
        }

        var newSize = Math.Max(required, _formatBuffer is null ? 256 : (int)Math.Min((long)_formatBuffer.Length * 2, int.MaxValue));
        var newBuffer = ArrayPool<char>.Shared.Rent(newSize);

        if (_formatBuffer is not null)
        {
            _formatBuffer.AsSpan(0, _formatPosition).CopyTo(newBuffer);
            ArrayPool<char>.Shared.Return(_formatBuffer);
        }

        _formatBuffer = newBuffer;
    }

    void AddFormattedSegment(int charsWritten)
    {
        var memory = new ReadOnlyMemory<char>(_formatBuffer, _formatPosition, charsWritten);
        AddSegment(memory);
        _formatPosition += charsWritten;
    }

    void AppendTextSpan(TextSpan span)
    {
        if (span.IsEmpty)
        {
            return;
        }

        if (span.Encoding == TextEncoding.Utf16)
        {
            var chars = span.Chars;
            EnsureFormatBuffer(chars.Length);
            chars.CopyTo(_formatBuffer.AsSpan(_formatPosition));
            AddFormattedSegment(chars.Length);
            return;
        }

        // Transcode UTF-8/UTF-32 to UTF-16 into format buffer
        var maxChars = span.Encoding == TextEncoding.Utf32
            ? span.Bytes.Length / 4 * 2
            : span.Bytes.Length;
        EnsureFormatBuffer(maxChars);
        var start = _formatPosition;

        foreach (var rune in span.EnumerateRunes())
        {
            var written = rune.EncodeToUtf16(_formatBuffer.AsSpan(_formatPosition));
            _formatPosition += written;
        }

        var totalChars = _formatPosition - start;
        var memory = new ReadOnlyMemory<char>(_formatBuffer, start, totalChars);
        AddSegment(memory);
    }

    /// <summary>Returns a zero-allocation enumerator over all segments.</summary>
    public LinkedTextUtf16Span.SegmentEnumerator EnumerateSegments() => AsSpan().EnumerateSegments();

    /// <summary>Creates a <see cref="LinkedTextUtf16Span"/> covering all content.</summary>
    public LinkedTextUtf16Span AsSpan()
    {
        if (SegmentCount == 0)
        {
            return default;
        }

        return new LinkedTextUtf16Span(this, 0, 0, SegmentCount - 1, GetSegment(SegmentCount - 1).Length);
    }
}
