using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    /// <param name="other">The text to compare with.</param>
    /// <returns><c>true</c> if both texts contain the same rune sequence; otherwise <c>false</c>.</returns>
    /// <remarks>When both texts share the same encoding, compares raw bytes directly. Cross-encoding comparison transcodes on the fly without allocating.</remarks>
    /// <example>
    /// <code>
    /// var utf8 = Text.FromUtf8("hello"u8);
    /// var utf16 = Text.From("hello");
    /// bool equal = utf8.Equals(utf16); // true — cross-encoding comparison
    /// </code>
    /// </example>
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

    /// <summary>Returns a hash code based on the raw byte content.</summary>
    /// <returns>A 32-bit hash code.</returns>
    /// <remarks>Uses XxHash3 over the raw bytes. The hash is encoding-dependent — two texts with the same rune content but different encodings may produce different hash codes.</remarks>
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
    /// <param name="other">The text to compare with.</param>
    /// <returns>A negative value if this text precedes <paramref name="other"/>, zero if equal, or a positive value if it follows.</returns>
    /// <remarks>When both texts share the same encoding, compares raw bytes directly. Cross-encoding comparison transcodes on the fly.</remarks>
    public int CompareTo(Text other)
    {
        if (_encoding == other._encoding)
        {
            return Bytes.SequenceCompareTo(other.Bytes);
        }

        return AsSpan().CompareTo(other.AsSpan());
    }

    /// <inheritdoc cref="Equals(Text)"/>
    /// <param name="left">The first text.</param>
    /// <param name="right">The second text.</param>
    /// <returns><c>true</c> if both texts contain the same rune sequence; otherwise <c>false</c>.</returns>
    public static bool operator ==(Text left, Text right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two texts do not contain the same rune sequence.</summary>
    /// <param name="left">The first text.</param>
    /// <param name="right">The second text.</param>
    /// <returns><c>true</c> if the texts differ; otherwise <c>false</c>.</returns>
    public static bool operator !=(Text left, Text right) => !left.Equals(right);
}
