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
            _overflowSegments = ArrayPool<Segment>.Shared.Rent(overflowCount);
        }

        foreach (var t in segments)
        {
            if (string.IsNullOrEmpty(t))
            {
                continue;
            }

            var seg = new Segment { Memory = t.AsMemory() };

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

#if NET6_0_OR_GREATER
    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A <see cref="LinkedTextUtf16"/> containing the interpolated content, or <see cref="Empty"/> if the result is empty.</returns>
    /// <remarks>String literals and string values are stored as segments. Formatted values are written into a pooled format buffer.</remarks>
    /// <example>
    /// <code>
    /// string name = "world";
    /// LinkedTextUtf16 linked = $"hello {name}!";
    /// </code>
    /// </example>
    public static LinkedTextUtf16 Create(LinkedTextUtf16 handler) => handler.IsEmpty ? Empty : handler;
#endif

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from strings.</summary>
    /// <param name="segments">The string values to compose.</param>
    /// <returns>A <see cref="LinkedTextUtf16"/> containing the provided strings, or <see cref="Empty"/> if all strings are empty.</returns>
    /// <remarks>Does not copy string data — stores <see cref="ReadOnlyMemory{T}"/> references via <c>string.AsMemory()</c>. Null or empty strings are skipped.</remarks>
    /// <example>
    /// <code>
    /// var linked = LinkedTextUtf16.Create("hello", " ", "world");
    /// </code>
    /// </example>
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
    /// <param name="segments">The UTF-16 memory segments to compose.</param>
    /// <returns>A <see cref="LinkedTextUtf16"/> containing the provided segments, or <see cref="Empty"/> if all segments are empty.</returns>
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

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from <see cref="Text"/> values.</summary>
    /// <param name="segments">The <see cref="Text"/> values to compose.</param>
    /// <returns>A <see cref="LinkedTextUtf16"/> containing the provided values, or <see cref="Empty"/> if all values are empty.</returns>
    /// <remarks>UTF-16 texts are referenced directly (no copy). Texts in other encodings are transcoded into a pooled format buffer.</remarks>
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

    internal void PopulateTexts(ReadOnlySpan<Text> segments)
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

    internal void PopulateStrings(ReadOnlySpan<string> segments)
    {
        foreach (var t in segments)
        {
            if (string.IsNullOrEmpty(t))
            {
                continue;
            }

            var seg = new Segment { Memory = t.AsMemory() };

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

    internal void Populate(ReadOnlySpan<ReadOnlyMemory<char>> segments)
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

    void AddSegment(ReadOnlyMemory<char> memory, char[]? pooledBuffer = null)
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
