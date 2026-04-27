namespace Glot;

public sealed partial class OwnedText
{
    /// <summary>Returns <c>true</c> if this and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    /// <remarks>Disposed instances compare as empty.</remarks>
    public bool Equals(OwnedText? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        return _text.Equals(other._text);
    }

    /// <inheritdoc cref="Equals(OwnedText)"/>
    public bool Equals(Text other) => _text.Equals(other);

    /// <inheritdoc cref="Equals(OwnedText)"/>
    public bool Equals(string? other) => _text.Equals(other);

    /// <inheritdoc cref="Equals(OwnedText)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => _text.Equals(value, encoding);

    /// <inheritdoc cref="Equals(OwnedText)"/>
    public bool Equals(ReadOnlySpan<char> value) => _text.Equals(value);

    /// <inheritdoc cref="Equals(OwnedText)"/>
    public bool Equals(ReadOnlySpan<int> value) => _text.Equals(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj switch
    {
        OwnedText o => Equals(o),
        Text t => Equals(t),
        string s => Equals(s),
        _ => false,
    };

    /// <summary>Returns a hash code based on the raw byte content of the wrapped <see cref="Text"/>.</summary>
    /// <remarks>The hash is encoding-dependent — two values with the same rune content but different encodings produce different hash codes.</remarks>
    public override int GetHashCode() => _text.GetHashCode();

    /// <summary>Compares this and <paramref name="other"/> lexicographically by rune value, regardless of encoding.</summary>
    /// <remarks>A <c>null</c> operand sorts before any non-null instance.</remarks>
    public int CompareTo(OwnedText? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return _text.CompareTo(other._text);
    }

    /// <inheritdoc cref="CompareTo(OwnedText)"/>
    public int CompareTo(Text other) => _text.CompareTo(other);

    /// <summary>Returns <c>true</c> if both operands are <c>null</c> or contain the same rune sequence.</summary>
    public static bool operator ==(OwnedText? left, OwnedText? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left is not null && left.Equals(right);
    }

    /// <summary>Returns <c>true</c> if the operands differ in nullity or rune content.</summary>
    public static bool operator !=(OwnedText? left, OwnedText? right) => !(left == right);
}
