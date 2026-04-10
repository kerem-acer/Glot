namespace Glot;

public readonly partial struct Text
{
#if NET7_0_OR_GREATER
    // IParsable<Text>

    /// <summary>Parses a string into a <see cref="Text"/>. Always succeeds — any string is valid text.</summary>
    public static Text Parse(string s, IFormatProvider? provider) => From(s);

    /// <summary>Tries to parse a string into a <see cref="Text"/>. Always returns <c>true</c>.</summary>
    public static bool TryParse(string? s, IFormatProvider? provider, out Text result)
    {
        result = s is null ? default : From(s);
        return true;
    }

    // ISpanParsable<Text>

    /// <summary>Parses a char span into a UTF-16 <see cref="Text"/>.</summary>
    public static Text Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => FromChars(s);

    /// <summary>Tries to parse a char span into a <see cref="Text"/>. Always returns <c>true</c>.</summary>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Text result)
    {
        result = FromChars(s);
        return true;
    }
#endif

#if NET8_0_OR_GREATER
    // IUtf8SpanParsable<Text>

    /// <summary>Parses a UTF-8 byte span into a <see cref="Text"/>.</summary>
    public static Text Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => FromUtf8(utf8Text);

    /// <summary>Tries to parse a UTF-8 byte span into a <see cref="Text"/>. Always returns <c>true</c>.</summary>
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Text result)
    {
        result = FromUtf8(utf8Text);
        return true;
    }
#endif
}
