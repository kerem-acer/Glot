using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    LinkedTextUtf8(ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var count = 0;
        var totalLength = 0;

        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            count++;
            totalLength += t.Length;
        }

        if (count == 0)
        {
            return;
        }

        var segIndex = 0;
#if NET8_0_OR_GREATER
        var overflowCount = count > InlineCapacity ? count - InlineCapacity : 0;
#else
        var overflowCount = count;
#endif

        if (overflowCount > 0)
        {
            _overflowSegments = ArrayPool<Segment>.Shared.Rent(overflowCount);
        }

        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            var seg = new Segment { Memory = t };

#if NET8_0_OR_GREATER
            if (segIndex < InlineCapacity)
            {
                _inlineSegments[segIndex] = seg;
            }
            else
            {
                _overflowSegments![segIndex - InlineCapacity] = seg;
            }
#else
            _overflowSegments![segIndex] = seg;
#endif
            segIndex++;
        }

        SegmentCount = count;
        Length = totalLength;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf8"/> from memory segments.</summary>
    public static LinkedTextUtf8 Create(params ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var hasContent = false;
        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            hasContent = true;
            break;
        }

        return hasContent ? new LinkedTextUtf8(segments) : Empty;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf8"/> from <see cref="Text"/> values. Transcodes if needed.</summary>
    public static LinkedTextUtf8 Create(params ReadOnlySpan<Text> segments)
    {
        var hasContent = false;
        foreach (var t in segments)
        {
            if (!t.IsEmpty)
            {
                hasContent = true;
                break;
            }
        }

        if (!hasContent)
        {
            return Empty;
        }

        var linked = new LinkedTextUtf8();
        linked.PopulateTexts(segments);
        return linked;
    }

    internal void PopulateTexts(ReadOnlySpan<Text> segments)
    {
        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            if (t.TryGetUtf8Memory(out var memory))
            {
                AddSegment(memory);
            }
            else
            {
                AppendTextSpan(t.AsSpan());
            }
        }
    }

    internal void Populate(ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            var seg = new Segment { Memory = t };

#if NET8_0_OR_GREATER
            if (SegmentCount < InlineCapacity)
            {
                _inlineSegments[SegmentCount] = seg;
            }
            else
            {
                var overflowIndex = SegmentCount - InlineCapacity;
                EnsureOverflowCapacity(overflowIndex + 1);
                _overflowSegments![overflowIndex] = seg;
            }
#else
            EnsureOverflowCapacity(SegmentCount + 1);
            _overflowSegments![SegmentCount] = seg;
#endif

            SegmentCount++;
            Length += t.Length;
        }
    }

    void AddSegment(ReadOnlyMemory<byte> memory, byte[]? pooledBuffer = null)
    {
        var seg = new Segment { Memory = memory, PooledBuffer = pooledBuffer };

#if NET8_0_OR_GREATER
        if (SegmentCount < InlineCapacity)
        {
            _inlineSegments[SegmentCount] = seg;
        }
        else
        {
            var overflowIndex = SegmentCount - InlineCapacity;
            EnsureOverflowCapacity(overflowIndex + 1);
            _overflowSegments![overflowIndex] = seg;
        }
#else
        EnsureOverflowCapacity(SegmentCount + 1);
        _overflowSegments![SegmentCount] = seg;
#endif

        SegmentCount++;
        Length += memory.Length;
    }

    internal void EnsureCapacity(int required)
    {
#if NET8_0_OR_GREATER
        if (required <= InlineCapacity)
        {
            return;
        }

        EnsureOverflowCapacity(required - InlineCapacity);
#else
        EnsureOverflowCapacity(required);
#endif
    }

    void EnsureOverflowCapacity(int required)
    {
        if (_overflowSegments is not null && required <= _overflowSegments.Length)
        {
            return;
        }

        var newArr = ArrayPool<Segment>.Shared.Rent(Math.Max(required, 8));

        if (_overflowSegments is not null)
        {
            Array.Copy(_overflowSegments, newArr, _overflowSegments.Length < newArr.Length ? _overflowSegments.Length : newArr.Length);
            ArrayPool<Segment>.Shared.Return(_overflowSegments, clearArray: true);
        }

        _overflowSegments = newArr;
    }
}
