using System.Buffers;
#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Glot;

/// <summary>
/// An immutable text value composed of non-contiguous UTF-8 segments.
/// Holds <see cref="ReadOnlyMemory{T}"/> references to the original data
/// without copying byte data.
/// </summary>
#if NET8_0_OR_GREATER
[InterpolatedStringHandler]
#endif
public sealed partial class LinkedTextUtf8
{
#if NET8_0_OR_GREATER
    InlineSegmentBuffer _inlineSegments;
#endif
    Segment[]? _overflowSegments;
    byte[]? _formatBuffer;
    int _formatPosition;

    LinkedTextUtf8() { }

    /// <summary>Controls how <see cref="OwnedText"/> values are handled during interpolation.</summary>
    internal OwnedTextHandling OwnedTextHandling { get; set; }

    /// <summary>The number of segments in this linked text.</summary>
    public int SegmentCount { get; private set; }

    /// <summary>The total number of bytes across all segments.</summary>
    public int Length { get; private set; }

    /// <summary>Returns <c>true</c> if this linked text has no content.</summary>
    public bool IsEmpty => Length == 0;

    /// <summary>An empty <see cref="LinkedTextUtf8"/>.</summary>
    public static LinkedTextUtf8 Empty { get; } = new();

    /// <summary>Gets the memory at the specified segment index.</summary>
    internal ReadOnlyMemory<byte> GetSegment(int index) => GetSegmentEntry(index).Memory;

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

    void EnsureFormatBuffer(int additionalBytes)
    {
        var required = _formatPosition + additionalBytes;

        if (_formatBuffer is not null && required <= _formatBuffer.Length)
        {
            return;
        }

        var newSize = Math.Max(
            required,
            _formatBuffer is null ? 256 : (int)Math.Min((long)_formatBuffer.Length * 2, int.MaxValue));

        var newBuffer = ArrayPool<byte>.Shared.Rent(newSize);

        if (_formatBuffer is not null)
        {
            _formatBuffer.AsSpan(0, _formatPosition).CopyTo(newBuffer);
            ArrayPool<byte>.Shared.Return(_formatBuffer);
        }

        _formatBuffer = newBuffer;
    }

    void AddFormattedSegment(int bytesWritten)
    {
        var memory = new ReadOnlyMemory<byte>(_formatBuffer, _formatPosition, bytesWritten);
        AddSegment(memory);
        _formatPosition += bytesWritten;
    }

    void AppendTextSpan(TextSpan span)
    {
        if (span.IsEmpty)
        {
            return;
        }

        if (span.Encoding == TextEncoding.Utf8)
        {
            var bytes = span.Bytes;
            EnsureFormatBuffer(bytes.Length);
            bytes.CopyTo(_formatBuffer.AsSpan(_formatPosition));
            AddFormattedSegment(bytes.Length);
            return;
        }

        // Transcode UTF-16/UTF-32 to UTF-8 into format buffer (SIMD for UTF-16)
        var maxBytes = span.Encoding == TextEncoding.Utf16 ? span.Bytes.Length / 2 * 3 : span.Bytes.Length;
        EnsureFormatBuffer(maxBytes);
        var written = span.EncodeToUtf8(_formatBuffer.AsSpan(_formatPosition));
        AddFormattedSegment(written);
    }

    /// <summary>Returns a zero-allocation enumerator over all segments.</summary>
    public LinkedTextUtf8Span.SegmentEnumerator EnumerateSegments() => AsSpan().EnumerateSegments();

    /// <summary>Creates a <see cref="LinkedTextUtf8Span"/> covering all content.</summary>
    public LinkedTextUtf8Span AsSpan()
    {
        if (SegmentCount == 0)
        {
            return default;
        }

        return new LinkedTextUtf8Span(
            this,
            0,
            0,
            SegmentCount - 1,
            GetSegment(SegmentCount - 1).Length);
    }
}
