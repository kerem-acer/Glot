#if NET7_0_OR_GREATER
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Glot;

/// <summary>
/// Extends any <see cref="ISpanParsable{TSelf}"/> type with <c>Parse</c> and <c>TryParse</c>
/// overloads that accept <see cref="Text"/> directly.
/// </summary>
public static class ParseExtensions
{
    const int StackAllocThreshold = 256;

    extension<T>(T) where T : ISpanParsable<T>
    {
        /// <summary>Parses a <see cref="Text"/> value into <typeparamref name="T"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <returns>The parsed value.</returns>
        /// <exception cref="FormatException">The text is not in a recognized format.</exception>
        /// <remarks>When the text is UTF-16, parses directly from the char span. Other encodings transcode to a stack-allocated char buffer (heap-allocated for large texts).</remarks>
        /// <example>
        /// <code>
        /// var text = Text.From("42");
        /// int value = int.Parse(text);
        /// </code>
        /// </example>
        public static T Parse(Text text, IFormatProvider? provider = null)
        {
            if (text.Encoding == TextEncoding.Utf16)
            {
                return T.Parse(text.Chars, provider);
            }

            var maxChars = text.RuneLength * 2;
            char[]? rented = null;
            Span<char> buffer = maxChars <= StackAllocThreshold
                ? stackalloc char[maxChars]
                : (rented = ArrayPool<char>.Shared.Rent(maxChars));
            try
            {
                return T.Parse(buffer[..text.EncodeToUtf16(buffer)], provider);
            }
            finally
            {
                if (rented is not null)
                {
                    ArrayPool<char>.Shared.Return(rented);
                }
            }
        }

        /// <summary>Tries to parse a <see cref="Text"/> value into <typeparamref name="T"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="result">When successful, the parsed value.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        /// <remarks>When the text is UTF-16, parses directly from the char span. Other encodings transcode to a temporary char buffer.</remarks>
        public static bool TryParse(Text text, [MaybeNullWhen(false)] out T result)
            => TryParse(text, null, out result);

        /// <summary>Tries to parse a <see cref="Text"/> value into <typeparamref name="T"/> with a format provider.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <param name="result">When successful, the parsed value.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryParse(Text text, IFormatProvider? provider, [MaybeNullWhen(false)] out T result)
        {
            if (text.Encoding == TextEncoding.Utf16)
            {
                return T.TryParse(text.Chars, provider, out result);
            }

            var maxChars = text.RuneLength * 2;
            char[]? rented = null;
            Span<char> buffer = maxChars <= StackAllocThreshold
                ? stackalloc char[maxChars]
                : (rented = ArrayPool<char>.Shared.Rent(maxChars));
            try
            {
                return T.TryParse(buffer[..text.EncodeToUtf16(buffer)], provider, out result);
            }
            finally
            {
                if (rented is not null)
                {
                    ArrayPool<char>.Shared.Return(rented);
                }
            }
        }
    }

#if NET8_0_OR_GREATER
    extension<T>(T) where T : IUtf8SpanParsable<T>
    {
        /// <summary>Parses a <see cref="Text"/> value via UTF-8.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <returns>The parsed value.</returns>
        /// <exception cref="FormatException">The text is not in a recognized format.</exception>
        /// <remarks>When the text is UTF-8, parses directly from the byte span with no allocation. Other encodings transcode to a temporary byte buffer.</remarks>
        /// <example>
        /// <code>
        /// var text = Text.FromUtf8("3.14"u8);
        /// double value = double.ParseUtf8(text);
        /// </code>
        /// </example>
        public static T ParseUtf8(Text text, IFormatProvider? provider = null)
        {
            if (text.Encoding == TextEncoding.Utf8)
            {
                return T.Parse(text.Bytes, provider);
            }

            var maxBytes = text.RuneLength * 4;
            byte[]? rented = null;
            Span<byte> buffer = maxBytes <= StackAllocThreshold
                ? stackalloc byte[maxBytes]
                : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));
            try
            {
                return T.Parse(buffer[..text.EncodeToUtf8(buffer)], provider);
            }
            finally
            {
                if (rented is not null)
                {
                    ArrayPool<byte>.Shared.Return(rented);
                }
            }
        }

        /// <summary>Tries to parse a <see cref="Text"/> value via UTF-8.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="result">When successful, the parsed value.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        /// <remarks>When the text is UTF-8, parses directly from the byte span with no allocation. Other encodings transcode to a temporary buffer.</remarks>
        public static bool TryParseUtf8(Text text, [MaybeNullWhen(false)] out T result)
            => TryParseUtf8(text, null, out result);

        /// <summary>Tries to parse a <see cref="Text"/> value via UTF-8 with a format provider.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <param name="result">When successful, the parsed value.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryParseUtf8(Text text, IFormatProvider? provider, [MaybeNullWhen(false)] out T result)
        {
            if (text.Encoding == TextEncoding.Utf8)
            {
                return T.TryParse(text.Bytes, provider, out result);
            }

            var maxBytes = text.RuneLength * 4;
            byte[]? rented = null;
            Span<byte> buffer = maxBytes <= StackAllocThreshold
                ? stackalloc byte[maxBytes]
                : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));
            try
            {
                return T.TryParse(buffer[..text.EncodeToUtf8(buffer)], provider, out result);
            }
            finally
            {
                if (rented is not null)
                {
                    ArrayPool<byte>.Shared.Return(rented);
                }
            }
        }
    }
#endif
}
#endif
