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

            var seg = new Segment
            {
                Memory = t
            };

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
    /// <param name="segments">The UTF-8 memory segments to compose.</param>
    /// <returns>A <see cref="LinkedTextUtf8"/> containing the provided segments, or <see cref="Empty"/> if all segments are empty.</returns>
    /// <remarks>Does not copy byte data — stores references to the provided memory regions. Empty segments are skipped.</remarks>
    /// <example>
    /// <code>
    /// ReadOnlyMemory&lt;byte&gt; seg1 = "hello"u8.ToArray();
    /// ReadOnlyMemory&lt;byte&gt; seg2 = " world"u8.ToArray();
    /// var linked = LinkedTextUtf8.Create(seg1, seg2);
    /// </code>
    /// </example>
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

#if NET8_0_OR_GREATER
    /// <summary>Creates a <see cref="LinkedTextUtf8"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A <see cref="LinkedTextUtf8"/> containing the interpolated content, or <see cref="Empty"/> if the result is empty.</returns>
    /// <remarks>String literals and UTF-8 values are stored as segments. Formatted values (int, double, etc.) are written into a pooled format buffer.</remarks>
    /// <example>
    /// <code>
    /// int count = 42;
    /// LinkedTextUtf8 linked = $"count: {count}";
    /// </code>
    /// </example>
    public static LinkedTextUtf8 Create(LinkedTextUtf8 handler) => handler.IsEmpty ? Empty : handler;
#endif

    /// <summary>Creates a <see cref="LinkedTextUtf8"/> from <see cref="Text"/> values.</summary>
    /// <param name="segments">The <see cref="Text"/> values to compose.</param>
    /// <returns>A <see cref="LinkedTextUtf8"/> containing the provided values, or <see cref="Empty"/> if all values are empty.</returns>
    /// <remarks>UTF-8 texts are referenced directly (no copy). Texts in other encodings are transcoded into a pooled format buffer.</remarks>
    /// <example>
    /// <code>
    /// var linked = LinkedTextUtf8.Create(
    ///     Text.FromUtf8("hello"u8),
    ///     Text.From(" world")); // transcoded from UTF-16
    /// </code>
    /// </example>
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

            var seg = new Segment
            {
                Memory = t
            };

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
        var seg = new Segment
        {
            Memory = memory,
            PooledBuffer = pooledBuffer
        };

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
