using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(Text other)
    {
        // Same-encoding fast path: compare raw bytes directly, skip AsSpan() overhead.
        if (_encoding == other._encoding)
        {
            return Bytes.SequenceEqual(other.Bytes);
        }

        return AsSpan().Equals(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(string? other)
    {
        if (other is null)
        {
            return false;
        }

        if (_encoding == TextEncoding.Utf16)
        {
            return Bytes.SequenceEqual(MemoryMarshal.AsBytes(other.AsUnsafeSpan()));
        }

        return AsSpan().Equals(other.AsUnsafeSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (_encoding == encoding)
        {
            return Bytes.SequenceEqual(value);
        }

        return AsSpan().Equals(value, encoding);
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<char> value)
    {
        if (_encoding == TextEncoding.Utf16)
        {
            return Bytes.SequenceEqual(MemoryMarshal.AsBytes(value));
        }

        return AsSpan().Equals(value);
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<int> value)
    {
        if (_encoding == TextEncoding.Utf32)
        {
            return Bytes.SequenceEqual(MemoryMarshal.AsBytes(value));
        }

        return AsSpan().Equals(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Text other && Equals(other);

    /// <summary>Returns a hash code based on the raw byte content via XxHash3. O(n) — not cached.</summary>
    public override int GetHashCode()
    {
        var bytes = Bytes;
        if (bytes.IsEmpty)
        {
            return 0;
        }

        var hash = XxHash3.HashToUInt64(bytes, (long)Encoding);
        return (int)hash ^ (int)(hash >> 32);
    }

    /// <summary>Compares two texts lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(Text other)
    {
        if (_encoding == other._encoding)
        {
            return Bytes.SequenceCompareTo(other.Bytes);
        }

        return AsSpan().CompareTo(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public static bool operator ==(Text left, Text right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two texts do not contain the same rune sequence.</summary>
    public static bool operator !=(Text left, Text right) => !left.Equals(right);
}
