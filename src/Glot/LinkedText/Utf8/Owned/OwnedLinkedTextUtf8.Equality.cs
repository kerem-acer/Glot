namespace Glot;

public sealed partial class OwnedLinkedTextUtf8
{
    LinkedTextUtf8 DataOrEmpty => Data ?? LinkedTextUtf8.Empty;

    /// <summary>Returns <c>true</c> if this and <paramref name="other"/> contain the same UTF-8 byte sequence. Disposed instances compare as empty.</summary>
    public bool Equals(OwnedLinkedTextUtf8? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        return DataOrEmpty.Equals(other.DataOrEmpty);
    }

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(LinkedTextUtf8? other) => DataOrEmpty.Equals(other ?? LinkedTextUtf8.Empty);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(Text other) => DataOrEmpty.Equals(other);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(string? other) => DataOrEmpty.Equals(other);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => DataOrEmpty.Equals(value, encoding);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(ReadOnlySpan<char> value) => DataOrEmpty.Equals(value);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf8)"/>
    public bool Equals(ReadOnlySpan<int> value) => DataOrEmpty.Equals(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj switch
    {
        OwnedLinkedTextUtf8 o => Equals(o),
        LinkedTextUtf8 l => Equals(l),
        Text t => Equals(t),
        string s => Equals(s),
        _ => false,
    };

    /// <summary>Returns a hash code based on the raw UTF-8 byte content. Disposed instances hash as empty.</summary>
    public override int GetHashCode() => DataOrEmpty.GetHashCode();

    /// <summary>Compares this and <paramref name="other"/> lexicographically by byte order. Disposed instances compare as empty; <c>null</c> sorts before any non-null instance.</summary>
    public int CompareTo(OwnedLinkedTextUtf8? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return DataOrEmpty.CompareTo(other.DataOrEmpty);
    }

    /// <inheritdoc cref="CompareTo(OwnedLinkedTextUtf8)"/>
    public int CompareTo(LinkedTextUtf8? other) => DataOrEmpty.CompareTo(other ?? LinkedTextUtf8.Empty);

    /// <inheritdoc cref="CompareTo(OwnedLinkedTextUtf8)"/>
    public int CompareTo(Text other) => DataOrEmpty.CompareTo(other);

    /// <summary>Returns <c>true</c> if both operands are <c>null</c> or contain the same UTF-8 byte sequence.</summary>
    public static bool operator ==(OwnedLinkedTextUtf8? left, OwnedLinkedTextUtf8? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left is not null && left.Equals(right);
    }

    /// <summary>Returns <c>true</c> if the operands differ in nullity or byte content.</summary>
    public static bool operator !=(OwnedLinkedTextUtf8? left, OwnedLinkedTextUtf8? right) => !(left == right);
}
