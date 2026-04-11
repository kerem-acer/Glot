using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    LinkedTextUtf16(ReadOnlySpan<string> segments)
    {
        var count = 0;
        var totalLength = 0;

        foreach (var t in segments)
        {
            if (string.IsNullOrEmpty(t))
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
            _overflowSegments = ArrayPool<ReadOnlyMemory<char>>.Shared.Rent(overflowCount);
        }

        foreach (var t in segments)
        {
            if (string.IsNullOrEmpty(t))
            {
                continue;
            }

#if NET8_0_OR_GREATER
            if (segIndex < InlineCapacity)
            {
                _inlineSegments[segIndex] = t.AsMemory();
            }
            else
            {
                _overflowSegments![segIndex - InlineCapacity] = t.AsMemory();
            }
#else
            _overflowSegments![segIndex] = t.AsMemory();
#endif
            segIndex++;
        }

        SegmentCount = count;
        Length = totalLength;
    }

    LinkedTextUtf16(ReadOnlySpan<ReadOnlyMemory<char>> segments)
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
            _overflowSegments = ArrayPool<ReadOnlyMemory<char>>.Shared.Rent(overflowCount);
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

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from strings.</summary>
    public static LinkedTextUtf16 Create(params ReadOnlySpan<string> segments)
    {
        foreach (var t in segments)
        {
            if (!string.IsNullOrEmpty(t))
            {
                return new LinkedTextUtf16(segments);
            }
        }

        return Empty;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from memory segments.</summary>
    public static LinkedTextUtf16 Create(params ReadOnlySpan<ReadOnlyMemory<char>> segments)
    {
        foreach (var t in segments)
        {
            if (!t.IsEmpty)
            {
                return new LinkedTextUtf16(segments);
            }
        }

        return Empty;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from <see cref="Text"/> values. Transcodes if needed.</summary>
    public static LinkedTextUtf16 Create(params ReadOnlySpan<Text> segments)
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

        var linked = new LinkedTextUtf16();
        linked.PopulateTexts(segments);
        return linked;
    }

    /// <summary>Creates a pooled <see cref="LinkedTextUtf16Owned"/> from strings.</summary>
    public static LinkedTextUtf16Owned CreateOwned(params ReadOnlySpan<string> segments)
    {
        var linked = Pool.Get();
        linked.PopulateStrings(segments);
        return new LinkedTextUtf16Owned(linked);
    }

    /// <summary>Creates a pooled <see cref="LinkedTextUtf16Owned"/> from memory segments.</summary>
    public static LinkedTextUtf16Owned CreateOwned(params ReadOnlySpan<ReadOnlyMemory<char>> segments)
    {
        var linked = Pool.Get();
        linked.Populate(segments);
        return new LinkedTextUtf16Owned(linked);
    }

    /// <summary>Creates a pooled <see cref="LinkedTextUtf16Owned"/> from <see cref="Text"/> values.</summary>
    public static LinkedTextUtf16Owned CreateOwned(params ReadOnlySpan<Text> segments)
    {
        var linked = Pool.Get();
        linked.PopulateTexts(segments);
        return new LinkedTextUtf16Owned(linked);
    }

    void PopulateTexts(ReadOnlySpan<Text> segments)
    {
        foreach (var t in segments)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            if (t.TryGetUtf16Memory(out var memory))
            {
                AddSegment(memory);
            }
            else
            {
                AppendTextSpan(t.AsSpan());
            }
        }
    }

    void PopulateStrings(ReadOnlySpan<string> segments)
    {
        foreach (var t in segments)
        {
            if (string.IsNullOrEmpty(t))
            {
                continue;
            }

#if NET8_0_OR_GREATER
            if (SegmentCount < InlineCapacity)
            {
                _inlineSegments[SegmentCount] = t.AsMemory();
            }
            else
            {
                var overflowIndex = SegmentCount - InlineCapacity;
                EnsureOverflowCapacity(overflowIndex + 1);
                _overflowSegments![overflowIndex] = t.AsMemory();
            }
#else
            EnsureOverflowCapacity(SegmentCount + 1);
            _overflowSegments![SegmentCount] = t.AsMemory();
#endif

            SegmentCount++;
            Length += t.Length;
        }
    }

    void Populate(ReadOnlySpan<ReadOnlyMemory<char>> segments)
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

    void AddSegment(ReadOnlyMemory<char> segment)
    {
#if NET8_0_OR_GREATER
        if (SegmentCount < InlineCapacity)
        {
            _inlineSegments[SegmentCount] = segment;
        }
        else
        {
            var overflowIndex = SegmentCount - InlineCapacity;
            EnsureOverflowCapacity(overflowIndex + 1);
            _overflowSegments![overflowIndex] = segment;
        }
#else
        EnsureOverflowCapacity(SegmentCount + 1);
        _overflowSegments![SegmentCount] = segment;
#endif

        SegmentCount++;
        Length += segment.Length;
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

        var newArr = ArrayPool<ReadOnlyMemory<char>>.Shared.Rent(Math.Max(required, 8));

        if (_overflowSegments is not null)
        {
            Array.Copy(_overflowSegments, newArr, _overflowSegments.Length < newArr.Length ? _overflowSegments.Length : newArr.Length);
            ArrayPool<ReadOnlyMemory<char>>.Shared.Return(_overflowSegments, clearArray: true);
        }

        _overflowSegments = newArr;
    }
}
