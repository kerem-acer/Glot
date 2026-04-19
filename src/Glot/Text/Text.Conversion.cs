namespace Glot;

public readonly partial struct Text
{
    /// <summary>
    /// Converts this text to a <see cref="string"/>.
    /// Returns the original string reference when string-backed; allocates otherwise.
    /// </summary>
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
    /// <returns>The number of chars written.</returns>
    public int EncodeToUtf16(Span<char> destination) => AsSpan().EncodeToUtf16(destination);

    /// <summary>Encodes this text as UTF-8 bytes into <paramref name="destination"/>.</summary>
    /// <returns>The number of bytes written.</returns>
    public int EncodeToUtf8(Span<byte> destination) => AsSpan().EncodeToUtf8(destination);

    /// <summary>Writes the text as UTF-16 chars to <paramref name="destination"/>.</summary>
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

    /// <summary>Writes the text as UTF-8 bytes to <paramref name="utf8Destination"/>. Direct copy when UTF-8 backed; transcodes rune-by-rune otherwise (zero-alloc).</summary>
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
