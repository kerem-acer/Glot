using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    /// <summary>Creates a <see cref="LinkedTextUtf8"/> from multiple memory segments.</summary>
    public static LinkedTextUtf8 Create(params ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var lt = new LinkedTextUtf8();
        lt.EnsureCapacity(segments.Length);
        for (var i = 0; i < segments.Length; i++)
        {
            if (!segments[i].IsEmpty)
            {
                lt.AddSegment(segments[i]);
            }
        }

        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from a single segment.</summary>
    public static Owned CreateOwned(ReadOnlyMemory<byte> segment)
    {
        var lt = Rent();
        if (!segment.IsEmpty) lt.AddSegment(segment);
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from two segments.</summary>
    public static Owned CreateOwned(ReadOnlyMemory<byte> s1, ReadOnlyMemory<byte> s2)
    {
        var lt = Rent();
        if (!s1.IsEmpty) lt.AddSegment(s1);
        if (!s2.IsEmpty) lt.AddSegment(s2);
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from three segments.</summary>
    public static Owned CreateOwned(ReadOnlyMemory<byte> s1, ReadOnlyMemory<byte> s2, ReadOnlyMemory<byte> s3)
    {
        var lt = Rent();
        if (!s1.IsEmpty) lt.AddSegment(s1);
        if (!s2.IsEmpty) lt.AddSegment(s2);
        if (!s3.IsEmpty) lt.AddSegment(s3);
        return new Owned(lt);
    }

    void AddSegment(ReadOnlyMemory<byte> segment)
    {
        EnsureCapacity(_segmentCount + 1);
#if NET8_0_OR_GREATER
        if (_overflowSegments is null)
        {
            _inlineSegments[_segmentCount] = segment;
        }
        else
        {
            _overflowSegments[_segmentCount] = segment;
        }
#else
        _overflowSegments![_segmentCount] = segment;
#endif
        _segmentCount++;
        _totalLength += segment.Length;
    }

    void EnsureCapacity(int required)
    {
        if (_overflowSegments is not null && required <= _overflowSegments.Length)
        {
            return;
        }

#if NET8_0_OR_GREATER
        if (_overflowSegments is null && required <= InlineCapacity)
        {
            return;
        }
#endif

        var newArr = ArrayPool<ReadOnlyMemory<byte>>.Shared.Rent(Math.Max(required, 8));

#if NET8_0_OR_GREATER
        if (_overflowSegments is null)
        {
            for (var i = 0; i < _segmentCount; i++)
            {
                newArr[i] = _inlineSegments[i];
            }
        }
        else
#endif
        if (_overflowSegments is not null)
        {
            Array.Copy(_overflowSegments, newArr, _segmentCount);
            ArrayPool<ReadOnlyMemory<byte>>.Shared.Return(_overflowSegments, clearArray: true);
        }

        _overflowSegments = newArr;
    }
}
