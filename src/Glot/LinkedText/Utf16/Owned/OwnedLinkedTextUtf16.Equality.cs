namespace Glot;

public sealed partial class OwnedLinkedTextUtf16
{
    LinkedTextUtf16 DataOrEmpty => Data ?? LinkedTextUtf16.Empty;

    /// <summary>Returns <c>true</c> if this and <paramref name="other"/> contain the same UTF-16 char sequence. Disposed instances compare as empty.</summary>
    public bool Equals(OwnedLinkedTextUtf16? other)
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

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(LinkedTextUtf16? other) => DataOrEmpty.Equals(other ?? LinkedTextUtf16.Empty);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(Text other) => DataOrEmpty.Equals(other);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(string? other) => DataOrEmpty.Equals(other);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => DataOrEmpty.Equals(value, encoding);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(ReadOnlySpan<char> value) => DataOrEmpty.Equals(value);

    /// <inheritdoc cref="Equals(OwnedLinkedTextUtf16)"/>
    public bool Equals(ReadOnlySpan<int> value) => DataOrEmpty.Equals(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj switch
    {
        OwnedLinkedTextUtf16 o => Equals(o),
        LinkedTextUtf16 l => Equals(l),
        Text t => Equals(t),
        string s => Equals(s),
        _ => false,
    };

    /// <summary>Returns a hash code based on the raw UTF-16 byte content. Disposed instances hash as empty.</summary>
    public override int GetHashCode() => DataOrEmpty.GetHashCode();

    /// <summary>Compares this and <paramref name="other"/> lexicographically by UTF-16 char order. Disposed instances compare as empty; <c>null</c> sorts before any non-null instance.</summary>
    public int CompareTo(OwnedLinkedTextUtf16? other)
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

    /// <inheritdoc cref="CompareTo(OwnedLinkedTextUtf16)"/>
    public int CompareTo(LinkedTextUtf16? other) => DataOrEmpty.CompareTo(other ?? LinkedTextUtf16.Empty);

    /// <inheritdoc cref="CompareTo(OwnedLinkedTextUtf16)"/>
    public int CompareTo(Text other) => DataOrEmpty.CompareTo(other);

    /// <summary>Returns <c>true</c> if both operands are <c>null</c> or contain the same UTF-16 char sequence.</summary>
    public static bool operator ==(OwnedLinkedTextUtf16? left, OwnedLinkedTextUtf16? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left is not null && left.Equals(right);
    }

    /// <summary>Returns <c>true</c> if the operands differ in nullity or char content.</summary>
    public static bool operator !=(OwnedLinkedTextUtf16? left, OwnedLinkedTextUtf16? right) => !(left == right);
}
