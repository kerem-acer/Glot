namespace Glot;

public readonly partial struct Text
{
    /// <summary>
    /// Converts this text to a <see cref="string"/>.
    /// Returns the original string reference when string-backed; allocates otherwise.
    /// </summary>
    public override string ToString()
    {
        if (_data is string s && _start == 0 && _byteLength == s.Length * 2)
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

    /// <summary>Writes the text as UTF-16 chars to <paramref name="destination"/>.</summary>
    public bool TryFormat(Span<char> destination, out int charsWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (_data is string s && _start == 0 && _byteLength == s.Length * 2)
        {
            if (s.AsSpan().TryCopyTo(destination))
            {
                charsWritten = s.Length;
                return true;
            }

            charsWritten = 0;
            return false;
        }

        var str = ToString();
        if (str.AsSpan().TryCopyTo(destination))
        {
            charsWritten = str.Length;
            return true;
        }

        charsWritten = 0;
        return false;
    }

    /// <summary>Writes the text as UTF-8 bytes to <paramref name="utf8Destination"/>. Direct copy when UTF-8 backed; transcodes otherwise.</summary>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        var span = AsSpan();

        if (Encoding == TextEncoding.Utf8)
        {
            if (span.Bytes.TryCopyTo(utf8Destination))
            {
                bytesWritten = span.ByteLength;
                return true;
            }

            bytesWritten = 0;
            return false;
        }

        var str = ToString();
        var byteCount = System.Text.Encoding.UTF8.GetByteCount(str);
        if (byteCount > utf8Destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten = System.Text.Encoding.UTF8.GetBytes(str.AsSpan(), utf8Destination);
        return true;
    }
}
