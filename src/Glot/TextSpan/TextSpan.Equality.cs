using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Returns <c>true</c> if this span and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(TextSpan other)
    {
        // Same-encoding: byte equality implies rune equality. This assumes well-formed
        // input — two different malformed byte sequences could be byte-unequal yet decode
        // to the same replacement-char sequence. The library treats ordinal byte identity
        // as canonical for same-encoding comparison.
        if (Encoding == other.Encoding)
        {
            return Bytes.SequenceEqual(other.Bytes);
        }

        var a = Bytes;
        var b = other.Bytes;

        while (!a.IsEmpty && !b.IsEmpty)
        {
            Rune.TryDecodeFirst(a, Encoding, out var ra, out var ca);
            Rune.TryDecodeFirst(b, other.Encoding, out var rb, out var cb);

            if (ra != rb)
            {
                return false;
            }

            a = a[ca..];
            b = b[cb..];
        }

        return a.IsEmpty && b.IsEmpty;
    }

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => Equals(Uncounted(value, encoding));

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<char> value)
        => Equals(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<int> value)
        => Equals(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <inheritdoc/>
    public override bool Equals(object? obj) => false;

    /// <summary>Returns an encoding-independent hash code based on the rune sequence.</summary>
    public override int GetHashCode() => ComputeHashCode(Bytes, Encoding);

    internal static int ComputeHashCode(ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        if (bytes.IsEmpty)
        {
            return 0;
        }

        var hash = new HashCode();
        var remaining = bytes;

        while (!remaining.IsEmpty)
        {
            Rune.TryDecodeFirst(remaining, encoding, out var rune, out var consumed);
            hash.Add(rune.Value);
            remaining = remaining[consumed..];
        }

        return hash.ToHashCode();
    }

    /// <summary>Compares two spans lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(TextSpan other)
    {
        // UTF-8 byte ordering preserves Unicode scalar value ordering
        if (Encoding == other.Encoding && Encoding == TextEncoding.Utf8)
        {
            return Bytes.SequenceCompareTo(other.Bytes);
        }

        var a = Bytes;
        var b = other.Bytes;

        while (!a.IsEmpty && !b.IsEmpty)
        {
            Rune.TryDecodeFirst(a, Encoding, out var ra, out var ca);
            Rune.TryDecodeFirst(b, other.Encoding, out var rb, out var cb);

            var cmp = ra.CompareTo(rb);
            if (cmp != 0)
            {
                return cmp;
            }

            a = a[ca..];
            b = b[cb..];
        }

        return a.Length.CompareTo(b.Length);
    }

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public static bool operator ==(TextSpan left, TextSpan right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two spans do not contain the same rune sequence.</summary>
    public static bool operator !=(TextSpan left, TextSpan right) => !left.Equals(right);
}
