using System.Buffers;
using System.Text;

namespace Glot;

public readonly partial struct LinkedTextUtf8Span
{
    /// <summary>Materializes this span into a UTF-16 string by decoding the UTF-8 bytes.</summary>
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
#if NET6_0_OR_GREATER
            return Encoding.UTF8.GetString(slice);
#else
            return Encoding.UTF8.GetString(slice.ToArray());
#endif
        }

        // Multi-segment: copy bytes to contiguous buffer, then decode
        var buffer = new byte[_length];
        var offset = 0;
        foreach (var segment in EnumerateSegments())
        {
            segment.Span.CopyTo(buffer.AsSpan(offset));
            offset += segment.Length;
        }

#if NET6_0_OR_GREATER
        return Encoding.UTF8.GetString(buffer);
#else
        return Encoding.UTF8.GetString(buffer);
#endif
    }

    /// <summary>Writes all segments to the specified buffer writer without intermediate allocation.</summary>
    public void WriteTo(IBufferWriter<byte> writer)
    {
        foreach (var segment in EnumerateSegments())
        {
            var dest = writer.GetSpan(segment.Length);
            segment.Span.CopyTo(dest);
            writer.Advance(segment.Length);
        }
    }
}
