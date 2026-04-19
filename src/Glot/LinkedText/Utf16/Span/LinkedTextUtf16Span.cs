using System.Diagnostics;

namespace Glot;

/// <summary>
/// A read-only view into a <see cref="LinkedTextUtf16"/> that may span multiple segments.
/// </summary>
/// <remarks>This is a value type that references the parent <see cref="LinkedTextUtf16"/>'s segment data. It does not own or copy any data.</remarks>
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

    /// <summary>Gets the char at the specified index.</summary>
    /// <param name="index">The zero-based character position within this span.</param>
    /// <returns>The character at the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is negative or greater than or equal to <see cref="Length"/>.</exception>
    /// <remarks>Walks segments from the start to find the target character. For sequential access, prefer <see cref="EnumerateSegments"/>.</remarks>
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

            throw new UnreachableException();
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
