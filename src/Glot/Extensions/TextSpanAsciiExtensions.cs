#if NET8_0_OR_GREATER
using System.Buffers;
using System.Text;
#endif
using System.Runtime.InteropServices;

namespace Glot;

static class TextSpanAsciiExtensions
{
    extension(TextSpan span)
    {
        // Convert an all-ASCII span into the given target encoding directly (no BCL transcoder call).
        // Validates ASCII and writes into `destination` in a single pass.
        // Returns false if this span is not all-ASCII, is already in the target encoding (no conversion needed),
        // or exceeds the destination buffer.
        // On success, `byteLength` is the number of bytes written, interpretable per `targetEncoding`.
        internal bool TryConvertAscii(TextEncoding targetEncoding, Span<byte> destination, out int byteLength)
        {
            var sourceEncoding = span.Encoding;
            if (sourceEncoding == targetEncoding)
            {
                byteLength = 0;
                return false;
            }

            switch (sourceEncoding, targetEncoding)
            {
                case (TextEncoding.Utf8, TextEncoding.Utf16):
                    return TryWriteAsciiAsChars(span.Bytes, destination, out byteLength);

                case (TextEncoding.Utf8, TextEncoding.Utf32):
                    return TryWriteAsciiAsInts(span.Bytes, destination, out byteLength);

                case (TextEncoding.Utf16, TextEncoding.Utf8):
                    return TryWriteAsciiCharsAsBytes(span.Chars, destination, out byteLength);

                case (TextEncoding.Utf16, TextEncoding.Utf32):
                    return TryWriteAsciiCharsAsInts(span.Chars, destination, out byteLength);

                case (TextEncoding.Utf32, TextEncoding.Utf8):
                    return TryWriteAsciiIntsAsBytes(span.Ints, destination, out byteLength);

                case (TextEncoding.Utf32, TextEncoding.Utf16):
                    return TryWriteAsciiIntsAsChars(span.Ints, destination, out byteLength);

                default:
                    byteLength = 0;
                    return false;
            }
        }
    }

    // ASCII-validating converters. Each widens / narrows source elements directly into the
    // destination as the target type, bailing on the first non-ASCII element.

    internal static bool TryWriteAsciiAsChars(ReadOnlySpan<byte> source, Span<byte> destination, out int byteLength)
    {
        var required = source.Length * 2;
        if (required > destination.Length)
        {
            byteLength = 0;
            return false;
        }

        var chars = MemoryMarshal.Cast<byte, char>(destination[..required]);
#if NET8_0_OR_GREATER
        // Ascii.ToUtf16 validates ASCII and widens bytes → chars in a single SIMD pass.
        if (Ascii.ToUtf16(source, chars, out var written) != OperationStatus.Done
            || written != source.Length)
        {
            byteLength = 0;
            return false;
        }
#else
        for (var i = 0; i < source.Length; i++)
        {
            if (source[i] > 0x7F)
            {
                byteLength = 0;
                return false;
            }

            chars[i] = (char)source[i];
        }
#endif

        byteLength = required;
        return true;
    }

    internal static bool TryWriteAsciiAsInts(ReadOnlySpan<byte> source, Span<byte> destination, out int byteLength)
    {
        var required = source.Length * 4;
        if (required > destination.Length)
        {
            byteLength = 0;
            return false;
        }
#if NET8_0_OR_GREATER
        if (!Ascii.IsValid(source))
        {
            byteLength = 0;
            return false;
        }
#endif
        var ints = MemoryMarshal.Cast<byte, int>(destination[..required]);
        for (var i = 0; i < source.Length; i++)
        {
#if !NET8_0_OR_GREATER
            if (source[i] > 0x7F) { byteLength = 0; return false; }
#endif
            ints[i] = source[i];
        }

        byteLength = required;
        return true;
    }

    internal static bool TryWriteAsciiCharsAsBytes(ReadOnlySpan<char> source, Span<byte> destination, out int byteLength)
    {
        if (source.Length > destination.Length)
        {
            byteLength = 0;
            return false;
        }

#if NET8_0_OR_GREATER
        // Ascii.FromUtf16 validates ASCII and narrows chars → bytes in a single SIMD pass.
        if (Ascii.FromUtf16(source, destination, out var written) != OperationStatus.Done
            || written != source.Length)
        {
            byteLength = 0;
            return false;
        }
#else
        for (var i = 0; i < source.Length; i++)
        {
            var c = source[i];
            if (c > 0x7F)
            {
                byteLength = 0;
                return false;
            }

            destination[i] = (byte)c;
        }
#endif

        byteLength = source.Length;
        return true;
    }

    internal static bool TryWriteAsciiCharsAsInts(ReadOnlySpan<char> source, Span<byte> destination, out int byteLength)
    {
        var required = source.Length * 4;
        if (required > destination.Length)
        {
            byteLength = 0;
            return false;
        }

        var ints = MemoryMarshal.Cast<byte, int>(destination[..required]);
        for (var i = 0; i < source.Length; i++)
        {
            var c = source[i];
            if (c > 0x7F)
            {
                byteLength = 0;
                return false;
            }

            ints[i] = c;
        }

        byteLength = required;
        return true;
    }

    internal static bool TryWriteAsciiIntsAsBytes(ReadOnlySpan<int> source, Span<byte> destination, out int byteLength)
    {
        if (source.Length > destination.Length)
        {
            byteLength = 0;
            return false;
        }

        for (var i = 0; i < source.Length; i++)
        {
            var v = source[i];
            if ((uint)v > 0x7F)
            {
                byteLength = 0;
                return false;
            }

            destination[i] = (byte)v;
        }

        byteLength = source.Length;
        return true;
    }

    internal static bool TryWriteAsciiIntsAsChars(ReadOnlySpan<int> source, Span<byte> destination, out int byteLength)
    {
        var required = source.Length * 2;
        if (required > destination.Length)
        {
            byteLength = 0;
            return false;
        }

        var chars = MemoryMarshal.Cast<byte, char>(destination[..required]);
        for (var i = 0; i < source.Length; i++)
        {
            var v = source[i];
            if ((uint)v > 0x7F)
            {
                byteLength = 0;
                return false;
            }

            chars[i] = (char)v;
        }

        byteLength = required;
        return true;
    }
}
