using System.Buffers;

namespace Glot;

public readonly partial struct LinkedTextUtf16Span
{
    /// <summary>Materializes this span into a contiguous string.</summary>
    public override string ToString()
    {
        if (IsEmpty)
        {
            return string.Empty;
        }

        if (_startSegment == _endSegment)
        {
            var seg = _data!.GetSegment(_startSegment);
            var slice = seg.Span.Slice(_startIndex, _endIndex - _startIndex);
#if NETSTANDARD2_0
            return new string(slice.ToArray());
#else
            return new string(slice);
#endif
        }

#if NET6_0_OR_GREATER
        return string.Create(_length, this, static (dest, span) =>
        {
            var offset = 0;
            foreach (var segment in span.EnumerateSegments())
            {
                segment.Span.CopyTo(dest[offset..]);
                offset += segment.Length;
            }
        });
#else
        var buffer = new char[_length];
        var offset = 0;
        foreach (var segment in EnumerateSegments())
        {
            segment.Span.CopyTo(buffer.AsSpan(offset));
            offset += segment.Length;
        }

        return new string(buffer);
#endif
    }

    /// <summary>Writes all segments to the specified buffer writer without intermediate allocation.</summary>
    public void WriteTo(IBufferWriter<char> writer)
    {
        foreach (var segment in EnumerateSegments())
        {
            var dest = writer.GetSpan(segment.Length);
            segment.Span.CopyTo(dest);
            writer.Advance(segment.Length);
        }
    }
}
