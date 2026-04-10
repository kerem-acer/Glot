namespace Glot;

public readonly partial struct LinkedTextUtf8Span
{
    /// <summary>Returns a sub-span starting at the given byte offset.</summary>
    public LinkedTextUtf8Span Slice(int offset)
    {
        if (offset == 0)
        {
            return this;
        }

        if (offset == _length)
        {
            return default;
        }

        if ((uint)offset > (uint)_length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        var (seg, idx) = FindPosition(offset);
        return new LinkedTextUtf8Span(_data!, seg, idx, _endSegment, _endIndex, _length - offset);
    }

    /// <summary>Returns a sub-span of the specified length starting at the given byte offset.</summary>
    public LinkedTextUtf8Span Slice(int offset, int count)
    {
        if (count == 0)
        {
            return default;
        }

        if (offset == 0 && count == _length)
        {
            return this;
        }

        if ((uint)offset > (uint)_length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if ((uint)count > (uint)(_length - offset))
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var (startSeg, startIdx) = FindPosition(offset);
        var (endSeg, endIdx) = FindPosition(offset + count);
        return new LinkedTextUtf8Span(_data!, startSeg, startIdx, endSeg, endIdx, count);
    }

#if NET6_0_OR_GREATER
    /// <summary>Slices by range.</summary>
    public LinkedTextUtf8Span this[Range range]
    {
        get
        {
            var (offset, count) = range.GetOffsetAndLength(_length);
            return Slice(offset, count);
        }
    }
#endif

    (int segment, int index) FindPosition(int byteOffset)
    {
        var remaining = byteOffset;

        for (var seg = _startSegment; seg <= _endSegment; seg++)
        {
            var segMem = _data!.GetSegment(seg);
            var segStart = seg == _startSegment ? _startIndex : 0;
            var segEnd = seg == _endSegment ? _endIndex : segMem.Length;
            var segLen = segEnd - segStart;

            if (remaining < segLen)
            {
                return (seg, segStart + remaining);
            }

            if (remaining == segLen && seg < _endSegment)
            {
                return (seg + 1, 0);
            }

            remaining -= segLen;
        }

        return (_endSegment, _endIndex);
    }
}
