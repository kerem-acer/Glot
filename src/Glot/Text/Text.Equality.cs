namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(Text other) => AsSpan().Equals(other.AsSpan());

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

    /// <summary>Returns an encoding-independent hash code based on the rune sequence. O(1) for factory-created values; O(n) for sliced values.</summary>
    /// <remarks>
    /// Factory methods store the hash with <c>| 1</c> so it is never 0. A zero <c>_hashCode</c>
    /// means "not cached" (e.g. after slicing), triggering a recompute here.
    /// </remarks>
    public override int GetHashCode()
        => _hashCode != 0 ? _hashCode : TextSpan.ComputeHashCode(AsSpan().Bytes, Encoding) | 1;

    /// <summary>Compares two texts lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(Text other) => AsSpan().CompareTo(other.AsSpan());

    /// <inheritdoc cref="Equals(Text)"/>
    public static bool operator ==(Text left, Text right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two texts do not contain the same rune sequence.</summary>
    public static bool operator !=(Text left, Text right) => !left.Equals(right);
}
