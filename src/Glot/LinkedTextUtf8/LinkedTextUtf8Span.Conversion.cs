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
            return Encoding.UTF8.GetString(slice);
        }

        // Count chars first, then decode directly into string
        var charCount = 0;
        foreach (var segment in EnumerateSegments())
        {
            charCount += Encoding.UTF8.GetCharCount(segment.Span);
        }

        return string.Create(charCount, this, static (dest, span) =>
        {
            var offset = 0;
            foreach (var segment in span.EnumerateSegments())
            {
                var written = Encoding.UTF8.GetChars(segment.Span, dest[offset..]);
                offset += written;
            }
        });
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
