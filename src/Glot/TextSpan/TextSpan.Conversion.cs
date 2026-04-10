using System.Runtime.InteropServices;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Converts this span to a <see cref="string"/>. Allocates for non-string-backed data.</summary>
    public override string ToString()
    {
        if (_bytes.IsEmpty)
        {
            return string.Empty;
        }

        return Encoding switch
        {
            TextEncoding.Utf8 => System.Text.Encoding.UTF8.GetString(_bytes),
            TextEncoding.Utf16 => MemoryMarshal.Cast<byte, char>(_bytes).ToString(),
            TextEncoding.Utf32 => System.Text.Encoding.UTF32.GetString(_bytes),
            _ => throw new InvalidEncodingException(Encoding),
        };
    }

    /// <summary>Converts the content to a string with the specified format.</summary>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>Writes the text as UTF-16 chars to <paramref name="destination"/>.</summary>
    public bool TryFormat(Span<char> destination, out int charsWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        var str = ToString();
        if (str.AsSpan().TryCopyTo(destination))
        {
            charsWritten = str.Length;
            return true;
        }

        charsWritten = 0;
        return false;
    }

    /// <summary>Writes the text as UTF-8 bytes to <paramref name="utf8Destination"/>.</summary>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten,
        ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Encoding == TextEncoding.Utf8)
        {
            if (_bytes.TryCopyTo(utf8Destination))
            {
                bytesWritten = _bytes.Length;
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

    /// <summary>Reinterprets the underlying bytes as <see cref="char"/> elements. Zero-copy cast — no encoding validation.</summary>
    public ReadOnlySpan<char> Chars => MemoryMarshal.Cast<byte, char>(_bytes);

    /// <summary>Reinterprets the underlying bytes as <see cref="int"/> elements. Zero-copy cast — no encoding validation.</summary>
    public ReadOnlySpan<int> Ints => MemoryMarshal.Cast<byte, int>(_bytes);
}
