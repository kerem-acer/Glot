namespace Glot;

/// <summary>
/// A non-owning, zero-allocation view into a <see cref="LinkedTextUtf8"/>.
/// Represents a contiguous range of bytes that may span multiple segments.
/// </summary>
public readonly partial struct LinkedTextUtf8Span
{
    readonly LinkedTextUtf8? _data;
    readonly int _startSegment;
    readonly int _startIndex;
    readonly int _endSegment;
    readonly int _endIndex;
    readonly int _length;

    internal LinkedTextUtf8Span(
        LinkedTextUtf8 data, int startSegment, int startIndex, int endSegment, int endIndex)
    {
        _data = data;
        _startSegment = startSegment;
        _startIndex = startIndex;
        _endSegment = endSegment;
        _endIndex = endIndex;
        _length = ComputeLength(data, startSegment, startIndex, endSegment, endIndex);
    }

    internal LinkedTextUtf8Span(
        LinkedTextUtf8 data, int startSegment, int startIndex, int endSegment, int endIndex, int length)
    {
        _data = data;
        _startSegment = startSegment;
        _startIndex = startIndex;
        _endSegment = endSegment;
        _endIndex = endIndex;
        _length = length;
    }

    /// <summary>The total number of bytes in this span.</summary>
    public int Length => _length;

    /// <summary>Returns <c>true</c> if this span has no content.</summary>
    public bool IsEmpty => _length == 0;

    /// <summary>Gets the byte at the specified index.</summary>
    public byte this[int index]
    {
        get
        {
            if ((uint)index >= (uint)_length)
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
        LinkedTextUtf8 data, int startSeg, int startIdx, int endSeg, int endIdx)
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
