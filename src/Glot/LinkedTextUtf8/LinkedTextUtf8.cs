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
