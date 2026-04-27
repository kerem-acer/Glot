using System.Buffers;
using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    /// <summary>Returns <c>true</c> if this and <paramref name="other"/> contain the same UTF-8 byte sequence.</summary>
    public bool Equals(LinkedTextUtf8? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null || Length != other.Length)
        {
            return false;
        }

        var bEnum = other.EnumerateSegments();
        ReadOnlySpan<byte> bBuf = default;

        foreach (var aSeg in EnumerateSegments())
        {
            var aBuf = aSeg.Span;
            while (!aBuf.IsEmpty)
            {
                if (bBuf.IsEmpty)
                {
                    if (!bEnum.MoveNext())
                    {
                        return false;
                    }
                    bBuf = bEnum.Current.Span;
                }

                var n = Math.Min(aBuf.Length, bBuf.Length);
                if (!aBuf[..n].SequenceEqual(bBuf[..n]))
                {
                    return false;
                }

                aBuf = aBuf[n..];
                bBuf = bBuf[n..];
            }
        }

        return bBuf.IsEmpty && !bEnum.MoveNext();
    }

    /// <summary>Returns <c>true</c> if this and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(Text other)
    {
        if (other.Encoding == TextEncoding.Utf8)
        {
            if (Length != other.ByteLength)
            {
                return false;
            }

            var otherBytes = other.Bytes;
            var offset = 0;
            foreach (var seg in EnumerateSegments())
            {
                var s = seg.Span;
                if (!s.SequenceEqual(otherBytes.Slice(offset, s.Length)))
                {
                    return false;
                }
                offset += s.Length;
            }

            return true;
        }

        return EqualsCrossEncoding(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(string? other)
    {
        if (other is null)
        {
            return false;
        }

        return EqualsCrossEncoding(new TextSpan(MemoryMarshal.AsBytes(other.AsSpan()), TextEncoding.Utf16));
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (encoding == TextEncoding.Utf8)
        {
            if (Length != value.Length)
            {
                return false;
            }

            var offset = 0;
            foreach (var seg in EnumerateSegments())
            {
                var s = seg.Span;
                if (!s.SequenceEqual(value.Slice(offset, s.Length)))
                {
                    return false;
                }
                offset += s.Length;
            }

            return true;
        }

        return EqualsCrossEncoding(new TextSpan(value, encoding));
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<char> value)
        => EqualsCrossEncoding(new TextSpan(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<int> value)
        => EqualsCrossEncoding(new TextSpan(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    bool EqualsCrossEncoding(TextSpan other)
    {
        if (Length == 0)
        {
            return other.IsEmpty;
        }

        var rented = ArrayPool<byte>.Shared.Rent(Length);
        try
        {
            CopyTo(rented);
            return new TextSpan(rented.AsSpan(0, Length), TextEncoding.Utf8).Equals(other);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj switch
    {
        LinkedTextUtf8 l => Equals(l),
        Text t => Equals(t),
        string s => Equals(s),
        _ => false,
    };

    /// <summary>Returns a hash code based on the raw UTF-8 byte content.</summary>
    /// <remarks>Uses streaming XxHash3 over all segment bytes with a UTF-8 seed. Matches <see cref="Text.GetHashCode"/> for the same UTF-8 content.</remarks>
    public override int GetHashCode()
    {
        if (Length == 0)
        {
            return 0;
        }

        var hasher = new XxHash3((long)TextEncoding.Utf8);
        foreach (var seg in EnumerateSegments())
        {
            hasher.Append(seg.Span);
        }

        var hash = hasher.GetCurrentHashAsUInt64();
        return (int)hash ^ (int)(hash >> 32);
    }

    /// <summary>Compares this and <paramref name="other"/> lexicographically by byte order.</summary>
    /// <remarks>A <c>null</c> operand sorts before any non-null instance.</remarks>
    public int CompareTo(LinkedTextUtf8? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        var bEnum = other.EnumerateSegments();
        ReadOnlySpan<byte> bBuf = default;

        foreach (var aSeg in EnumerateSegments())
        {
            var aBuf = aSeg.Span;
            while (!aBuf.IsEmpty)
            {
                if (bBuf.IsEmpty)
                {
                    if (!bEnum.MoveNext())
                    {
                        return 1;
                    }
                    bBuf = bEnum.Current.Span;
                }

                var n = Math.Min(aBuf.Length, bBuf.Length);
                var cmp = aBuf[..n].SequenceCompareTo(bBuf[..n]);
                if (cmp != 0)
                {
                    return cmp;
                }

                aBuf = aBuf[n..];
                bBuf = bBuf[n..];
            }
        }

        return (!bBuf.IsEmpty || bEnum.MoveNext()) ? -1 : 0;
    }

    /// <summary>Compares this and <paramref name="other"/> lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(Text other)
    {
        if (other.Encoding == TextEncoding.Utf8)
        {
            var otherBytes = other.Bytes;
            var consumed = 0;
            foreach (var seg in EnumerateSegments())
            {
                var s = seg.Span;
                var available = otherBytes.Length - consumed;
                if (available <= 0)
                {
                    return 1;
                }

                var n = Math.Min(s.Length, available);
                var cmp = s[..n].SequenceCompareTo(otherBytes.Slice(consumed, n));
                if (cmp != 0)
                {
                    return cmp;
                }

                if (s.Length > available)
                {
                    return 1;
                }

                consumed += s.Length;
            }

            return Length == other.ByteLength ? 0 : -1;
        }

        return CompareToCrossEncoding(other.AsSpan());
    }

    int CompareToCrossEncoding(TextSpan other)
    {
        if (Length == 0)
        {
            return other.IsEmpty ? 0 : -1;
        }

        var rented = ArrayPool<byte>.Shared.Rent(Length);
        try
        {
            CopyTo(rented);
            return new TextSpan(rented.AsSpan(0, Length), TextEncoding.Utf8).CompareTo(other);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    void CopyTo(byte[] dest)
    {
        var offset = 0;
        foreach (var seg in EnumerateSegments())
        {
            seg.Span.CopyTo(dest.AsSpan(offset));
            offset += seg.Length;
        }
    }

    /// <summary>Returns <c>true</c> if both operands are <c>null</c> or contain the same UTF-8 byte sequence.</summary>
    public static bool operator ==(LinkedTextUtf8? left, LinkedTextUtf8? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left is not null && left.Equals(right);
    }

    /// <summary>Returns <c>true</c> if the operands differ in nullity or byte content.</summary>
    public static bool operator !=(LinkedTextUtf8? left, LinkedTextUtf8? right) => !(left == right);
}
