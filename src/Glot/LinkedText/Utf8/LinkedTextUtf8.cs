using System.Buffers;

namespace Glot;

/// <summary>
/// An immutable text value composed of non-contiguous UTF-8 segments.
/// Holds <see cref="ReadOnlyMemory{T}"/> references to the original data
/// without copying byte data.
/// </summary>
public sealed partial class LinkedTextUtf8
{
#if NET8_0_OR_GREATER
    InlineSegmentBuffer _inlineSegments;
#endif
    ReadOnlyMemory<byte>[]? _overflowSegments;
    byte[]? _formatBuffer;
    int _formatPosition;

    LinkedTextUtf8() { }

    /// <summary>The number of segments in this linked text.</summary>
    public int SegmentCount { get; private set; }

    /// <summary>The total number of bytes across all segments.</summary>
    public int Length { get; private set; }

    /// <summary>Returns <c>true</c> if this linked text has no content.</summary>
    public bool IsEmpty => Length == 0;

    /// <summary>An empty <see cref="LinkedTextUtf8"/>.</summary>
    public static LinkedTextUtf8 Empty { get; } = new();

    /// <summary>Gets the segment at the specified index.</summary>
    internal ReadOnlyMemory<byte> GetSegment(int index)
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

        var newSize = Math.Max(required, _formatBuffer is null ? 256 : (int)Math.Min((long)_formatBuffer.Length * 2, int.MaxValue));
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

        // Transcode UTF-16/UTF-32 to UTF-8 into format buffer
        var maxBytes = span.Encoding == TextEncoding.Utf16
            ? span.Bytes.Length / 2 * 3  // worst case: each UTF-16 code unit -> 3 UTF-8 bytes
            : span.Bytes.Length;          // UTF-32: each 4-byte scalar -> at most 4 UTF-8 bytes
        EnsureFormatBuffer(maxBytes);
        var start = _formatPosition;

        foreach (var rune in span.EnumerateRunes())
        {
            var written = rune.EncodeToUtf8(_formatBuffer.AsSpan(_formatPosition));
            _formatPosition += written;
        }

        var totalBytes = _formatPosition - start;
        var memory = new ReadOnlyMemory<byte>(_formatBuffer, start, totalBytes);
        AddSegment(memory);
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
