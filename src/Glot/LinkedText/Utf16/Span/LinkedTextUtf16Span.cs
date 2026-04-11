namespace Glot;

/// <summary>
/// A non-owning, zero-allocation view into a <see cref="LinkedTextUtf16"/>.
/// Represents a contiguous range of characters that may span multiple segments.
/// Slicing produces a new struct with adjusted boundaries — no allocation.
/// </summary>
public readonly partial struct LinkedTextUtf16Span
{
    readonly LinkedTextUtf16? _data;
    readonly int _startSegment;
    readonly int _startIndex;
    readonly int _endSegment;
    readonly int _endIndex;

    internal LinkedTextUtf16Span(
        LinkedTextUtf16 data, int startSegment, int startIndex, int endSegment, int endIndex)
    {
        _data = data;
        _startSegment = startSegment;
        _startIndex = startIndex;
        _endSegment = endSegment;
        _endIndex = endIndex;
        Length = ComputeLength(data, startSegment, startIndex, endSegment, endIndex);
    }

    internal LinkedTextUtf16Span(
        LinkedTextUtf16 data, int startSegment, int startIndex, int endSegment, int endIndex, int length)
    {
        _data = data;
        _startSegment = startSegment;
        _startIndex = startIndex;
        _endSegment = endSegment;
        _endIndex = endIndex;
        Length = length;
    }

    /// <summary>The total number of chars in this span.</summary>
    public int Length { get; }

    /// <summary>Returns <c>true</c> if this span has no content.</summary>
    public bool IsEmpty => Length == 0;

    /// <summary>Gets the char at the specified index. O(n) — walks segments from the start.</summary>
    /// <remarks>
    /// For sequential access, use <see cref="EnumerateSegments"/> instead of a for-loop
    /// with this indexer, which would be O(n²) over the full span.
    /// </remarks>
    public char this[int index]
    {
        get
        {
            if ((uint)index >= (uint)Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var remaining = index;
            for (var seg = _startSegment; seg <= _endSegment; seg++)
            {
                var segMem = _data!.GetSegment(seg);
                var segStart = seg == _startSegment ? _startIndex : 0;
                var segEnd = seg == _endSegment ? _endIndex : segMem.Length;
                var segLen = segEnd - segStart;

                if (remaining < segLen)
                {
                    return segMem.Span[segStart + remaining];
                }

                remaining -= segLen;
            }

            throw new InvalidOperationException("Unreachable");
        }
    }

    static int ComputeLength(
        LinkedTextUtf16 data, int startSeg, int startIdx, int endSeg, int endIdx)
    {
        if (startSeg == endSeg)
        {
            return endIdx - startIdx;
        }

        var len = data.GetSegment(startSeg).Length - startIdx;
        for (var i = startSeg + 1; i < endSeg; i++)
        {
            len += data.GetSegment(i).Length;
        }

        len += endIdx;
        return len;
    }
}
