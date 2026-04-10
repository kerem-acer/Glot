using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from a single string.</summary>
    public static LinkedTextUtf16 Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Empty;
        }

        return Create(value.AsMemory());
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from a single memory segment.</summary>
    public static LinkedTextUtf16 Create(ReadOnlyMemory<char> segment)
    {
        if (segment.IsEmpty)
        {
            return Empty;
        }

        var lt = new LinkedTextUtf16();
        lt.AddSegment(segment);
        return lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from two strings.</summary>
    public static LinkedTextUtf16 Create(string s1, string s2)
    {
        var lt = new LinkedTextUtf16();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from three strings.</summary>
    public static LinkedTextUtf16 Create(string s1, string s2, string s3)
    {
        var lt = new LinkedTextUtf16();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        if (!string.IsNullOrEmpty(s3)) lt.AddSegment(s3.AsMemory());
        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from four strings.</summary>
    public static LinkedTextUtf16 Create(string s1, string s2, string s3, string s4)
    {
        var lt = new LinkedTextUtf16();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        if (!string.IsNullOrEmpty(s3)) lt.AddSegment(s3.AsMemory());
        if (!string.IsNullOrEmpty(s4)) lt.AddSegment(s4.AsMemory());
        return lt._segmentCount == 0 ? Empty : lt;
    }

#if NET9_0_OR_GREATER
    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from multiple strings.</summary>
    public static LinkedTextUtf16 Create(params ReadOnlySpan<string> segments)
    {
        var lt = new LinkedTextUtf16();
        lt.EnsureCapacity(segments.Length);
        for (var i = 0; i < segments.Length; i++)
        {
            if (!string.IsNullOrEmpty(segments[i]))
            {
                lt.AddSegment(segments[i].AsMemory());
            }
        }

        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from multiple memory segments.</summary>
    public static LinkedTextUtf16 Create(params ReadOnlySpan<ReadOnlyMemory<char>> segments)
    {
        var lt = new LinkedTextUtf16();
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
#endif

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from a single <see cref="Text"/>. Transcodes if needed.</summary>
    public static LinkedTextUtf16 Create(Text value)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        var lt = new LinkedTextUtf16();
        lt.AppendTextSpan(value.AsSpan());
        return lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from two <see cref="Text"/> values.</summary>
    public static LinkedTextUtf16 Create(Text t1, Text t2)
    {
        var lt = new LinkedTextUtf16();
        if (!t1.IsEmpty) lt.AppendTextSpan(t1.AsSpan());
        if (!t2.IsEmpty) lt.AppendTextSpan(t2.AsSpan());
        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from three <see cref="Text"/> values.</summary>
    public static LinkedTextUtf16 Create(Text t1, Text t2, Text t3)
    {
        var lt = new LinkedTextUtf16();
        if (!t1.IsEmpty) lt.AppendTextSpan(t1.AsSpan());
        if (!t2.IsEmpty) lt.AppendTextSpan(t2.AsSpan());
        if (!t3.IsEmpty) lt.AppendTextSpan(t3.AsSpan());
        return lt._segmentCount == 0 ? Empty : lt;
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from a single <see cref="Text"/>.</summary>
    public static Owned CreateOwned(Text value)
    {
        var lt = Rent();
        if (!value.IsEmpty) lt.AppendTextSpan(value.AsSpan());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from two <see cref="Text"/> values.</summary>
    public static Owned CreateOwned(Text t1, Text t2)
    {
        var lt = Rent();
        if (!t1.IsEmpty) lt.AppendTextSpan(t1.AsSpan());
        if (!t2.IsEmpty) lt.AppendTextSpan(t2.AsSpan());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from three <see cref="Text"/> values.</summary>
    public static Owned CreateOwned(Text t1, Text t2, Text t3)
    {
        var lt = Rent();
        if (!t1.IsEmpty) lt.AppendTextSpan(t1.AsSpan());
        if (!t2.IsEmpty) lt.AppendTextSpan(t2.AsSpan());
        if (!t3.IsEmpty) lt.AppendTextSpan(t3.AsSpan());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from a single string.</summary>
    public static Owned CreateOwned(string value)
    {
        var lt = Rent();
        if (!string.IsNullOrEmpty(value)) lt.AddSegment(value.AsMemory());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from two strings.</summary>
    public static Owned CreateOwned(string s1, string s2)
    {
        var lt = Rent();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from three strings.</summary>
    public static Owned CreateOwned(string s1, string s2, string s3)
    {
        var lt = Rent();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        if (!string.IsNullOrEmpty(s3)) lt.AddSegment(s3.AsMemory());
        return new Owned(lt);
    }

    /// <summary>Creates a pooled <see cref="Owned"/> from four strings.</summary>
    public static Owned CreateOwned(string s1, string s2, string s3, string s4)
    {
        var lt = Rent();
        if (!string.IsNullOrEmpty(s1)) lt.AddSegment(s1.AsMemory());
        if (!string.IsNullOrEmpty(s2)) lt.AddSegment(s2.AsMemory());
        if (!string.IsNullOrEmpty(s3)) lt.AddSegment(s3.AsMemory());
        if (!string.IsNullOrEmpty(s4)) lt.AddSegment(s4.AsMemory());
        return new Owned(lt);
    }

    void AddSegment(ReadOnlyMemory<char> segment)
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

        var newArr = ArrayPool<ReadOnlyMemory<char>>.Shared.Rent(Math.Max(required, 8));

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
            ArrayPool<ReadOnlyMemory<char>>.Shared.Return(_overflowSegments, clearArray: true);
        }

        _overflowSegments = newArr;
    }
}
