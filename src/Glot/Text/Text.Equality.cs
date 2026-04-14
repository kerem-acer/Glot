namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(Text other)
    {
        // Same-encoding fast path: compare raw bytes directly, skip AsSpan() overhead.
        if (Encoding == other.Encoding)
        {
            return RawBytes.SequenceEqual(other.RawBytes);
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

        return AsSpan().Equals(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().Equals(value, encoding);

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<char> value)
        => AsSpan().Equals(value);

    /// <inheritdoc cref="Equals(Text)"/>
    public bool Equals(ReadOnlySpan<int> value)
        => AsSpan().Equals(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Text other && Equals(other);

    /// <summary>Returns a hash code based on the raw byte content via XxHash3. O(n) — not cached.</summary>
    public override int GetHashCode()
    {
        var bytes = RawBytes;
        if (bytes.IsEmpty)
        {
            return 0;
        }

        var hash = System.IO.Hashing.XxHash3.HashToUInt64(bytes, (long)Encoding);
        return (int)hash ^ (int)(hash >> 32);
    }

    /// <summary>Compares two texts lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(Text other)
    {
        if (Encoding == other.Encoding)
        {
            return RawBytes.SequenceCompareTo(other.RawBytes);
        }

        return AsSpan().CompareTo(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    public static bool operator ==(Text left, Text right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two texts do not contain the same rune sequence.</summary>
    public static bool operator !=(Text left, Text right) => !left.Equals(right);
}
