namespace Glot;

public readonly partial struct Text
{
    /// <summary>Converts this text to a <see cref="string"/>.</summary>
    /// <returns>A <see cref="string"/> representation of this text.</returns>
    /// <remarks>Returns the original <see cref="string"/> reference when the text is string-backed and covers the full string. Allocates a new string otherwise.</remarks>
    public override string ToString()
    {
        if (_data is string s && _start == 0 && ByteLength == s.Length * 2)
        {
            return s;
        }

        if (IsEmpty)
        {
            return string.Empty;
        }

        return AsSpan().ToString();
    }

    /// <inheritdoc cref="ToString()"/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>Encodes this text as UTF-16 chars into <paramref name="destination"/>.</summary>
    /// <param name="destination">The buffer to write UTF-16 chars into.</param>
    /// <returns>The number of chars written.</returns>
    /// <remarks>When the text is already UTF-16, this is a direct copy. Other encodings are transcoded.</remarks>
    public int EncodeToUtf16(Span<char> destination) => AsSpan().EncodeToUtf16(destination);

    /// <summary>Encodes this text as UTF-8 bytes into <paramref name="destination"/>.</summary>
    /// <param name="destination">The buffer to write UTF-8 bytes into.</param>
    /// <returns>The number of bytes written.</returns>
    /// <remarks>When the text is already UTF-8, this is a direct copy. Other encodings are transcoded.</remarks>
    public int EncodeToUtf8(Span<byte> destination) => AsSpan().EncodeToUtf8(destination);

    /// <summary>Writes the text as UTF-16 chars to <paramref name="destination"/>.</summary>
    /// <param name="destination">The buffer to write UTF-16 chars into.</param>
    /// <param name="charsWritten">The number of chars written to <paramref name="destination"/>.</param>
    /// <param name="format">The format string (unused).</param>
    /// <param name="provider">The format provider (unused).</param>
    /// <returns><c>true</c> if the text was written; <c>false</c> if the destination was too small.</returns>
    /// <remarks>When the text is string- or char-array-backed, copies directly without transcoding.</remarks>
    public bool TryFormat(Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null)
    {
        if (IsEmpty)
        {
            charsWritten = 0;
            return true;
        }

        // Fast path: when UTF-16 backed (string or char[]), point a ReadOnlySpan<char> directly
        // at the backing data via AsUnsafeSpan — skips the null / bounds / range validation that
        // the BCL AsSpan overloads perform. `_backingType` already guarantees the type.
        switch (_backingType)
        {
            case BackingType.String:
            {
                var chars = UnsafeStringChars;
                if (chars.TryCopyTo(destination))
                {
                    charsWritten = chars.Length;
                    return true;
                }
                charsWritten = 0;
                return false;
            }
            case BackingType.CharArray:
            {
                var chars = UnsafeCharArrayChars;
                if (chars.TryCopyTo(destination))
                {
                    charsWritten = chars.Length;
                    return true;
                }
                charsWritten = 0;
                return false;
            }

            default:
                return AsSpan().TryFormat(
                    destination,
                    out charsWritten,
                    format,
                    provider);
        }
    }

    /// <summary>Writes the text as UTF-8 bytes to <paramref name="utf8Destination"/>.</summary>
    /// <param name="utf8Destination">The buffer to write UTF-8 bytes into.</param>
    /// <param name="bytesWritten">The number of bytes written to <paramref name="utf8Destination"/>.</param>
    /// <param name="format">The format string (unused).</param>
    /// <param name="provider">The format provider (unused).</param>
    /// <returns><c>true</c> if the text was written; <c>false</c> if the destination was too small.</returns>
    /// <remarks>When the text is byte-array-backed (UTF-8), copies directly without transcoding.</remarks>
    public bool TryFormat(Span<byte> utf8Destination,
        out int bytesWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null)
    {
        if (IsEmpty)
        {
            bytesWritten = 0;
            return true;
        }

        // Fast path: UTF-8 is always byte-array backed. Copy bytes directly without going
        // through AsSpan() + TextSpan.TryFormat (which re-fetches Bytes via the same switch).
        if (_backingType == BackingType.ByteArray)
        {
            var bytes = UnsafeByteArrayBytes;
            if (bytes.TryCopyTo(utf8Destination))
            {
                bytesWritten = bytes.Length;
                return true;
            }
            bytesWritten = 0;
            return false;
        }

        return AsSpan().TryFormat(
            utf8Destination,
            out bytesWritten,
            format,
            provider);
    }
}
