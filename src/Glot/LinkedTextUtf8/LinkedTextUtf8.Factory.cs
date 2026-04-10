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
            _overflowSegments = ArrayPool<ReadOnlyMemory<byte>>.Shared.Rent(overflowCount);
        }

        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

#if NET8_0_OR_GREATER
            if (segIndex < InlineCapacity)
            {
                _inlineSegments[segIndex] = t;
            }
            else
            {
                _overflowSegments![segIndex - InlineCapacity] = t;
            }
#else
            _overflowSegments![segIndex] = t;
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

    /// <summary>Creates a pooled <see cref="LinkedTextUtf8Owned"/> from memory segments.</summary>
    public static LinkedTextUtf8Owned CreateOwned(params ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var linked = Pool.Get();
        linked.Populate(segments);
        return new LinkedTextUtf8Owned(linked);
    }

    void Populate(ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

#if NET8_0_OR_GREATER
            if (SegmentCount < InlineCapacity)
            {
                _inlineSegments[SegmentCount] = t;
            }
            else
            {
                var overflowIndex = SegmentCount - InlineCapacity;
                EnsureOverflowCapacity(overflowIndex + 1);
                _overflowSegments![overflowIndex] = t;
            }
#else
            EnsureOverflowCapacity(SegmentCount + 1);
            _overflowSegments![SegmentCount] = t;
#endif

            SegmentCount++;
            Length += t.Length;
        }
    }

    void EnsureOverflowCapacity(int required)
    {
        if (_overflowSegments is not null && required <= _overflowSegments.Length)
        {
            return;
        }

        var newArr = ArrayPool<ReadOnlyMemory<byte>>.Shared.Rent(Math.Max(required, 8));

        if (_overflowSegments is not null)
        {
            Array.Copy(_overflowSegments, newArr, _overflowSegments.Length < newArr.Length ? _overflowSegments.Length : newArr.Length);
            ArrayPool<ReadOnlyMemory<byte>>.Shared.Return(_overflowSegments, clearArray: true);
        }

        _overflowSegments = newArr;
    }
}
