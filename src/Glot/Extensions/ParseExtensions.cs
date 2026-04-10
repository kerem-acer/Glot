#if NET7_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;

namespace Glot;

/// <summary>
/// Extends any <see cref="ISpanParsable{TSelf}"/> type with <c>Parse</c> and <c>TryParse</c>
/// overloads that accept <see cref="Text"/> directly.
/// </summary>
public static class ParseExtensions
{
    extension<T>(T) where T : ISpanParsable<T>
    {
        /// <summary>Parses a <see cref="Text"/> value into <typeparamref name="T"/>.</summary>
        public static T Parse(Text text, IFormatProvider? provider = null)
            => T.Parse(ToChars(text), provider);

        /// <summary>Tries to parse a <see cref="Text"/> value into <typeparamref name="T"/>.</summary>
        public static bool TryParse(Text text, [MaybeNullWhen(false)] out T result)
            => T.TryParse(ToChars(text), null, out result);

        /// <summary>Tries to parse a <see cref="Text"/> value into <typeparamref name="T"/> with a format provider.</summary>
        public static bool TryParse(Text text, IFormatProvider? provider, [MaybeNullWhen(false)] out T result)
            => T.TryParse(ToChars(text), provider, out result);
    }

#if NET8_0_OR_GREATER
    extension<T>(T) where T : IUtf8SpanParsable<T>
    {
        /// <summary>Parses a <see cref="Text"/> value using the UTF-8 fast path. No allocation for UTF-8 backed text.</summary>
        public static T ParseUtf8(Text text, IFormatProvider? provider = null)
            => T.Parse(text.Bytes, provider);

        /// <summary>Tries to parse a <see cref="Text"/> value using the UTF-8 fast path.</summary>
        public static bool TryParseUtf8(Text text, [MaybeNullWhen(false)] out T result)
            => T.TryParse(text.Bytes, null, out result);

        /// <summary>Tries to parse a <see cref="Text"/> value using the UTF-8 fast path with a format provider.</summary>
        public static bool TryParseUtf8(Text text, IFormatProvider? provider, [MaybeNullWhen(false)] out T result)
            => T.TryParse(text.Bytes, provider, out result);
    }
#endif

    /// <summary>
    /// Returns chars without allocation when UTF-16 backed, falls back to ToString for other encodings.
    /// </summary>
    static ReadOnlySpan<char> ToChars(Text text)
        => text.Encoding == TextEncoding.Utf16
            ? text.Chars
            : text.AsSpan().Chars;
}
#endif
