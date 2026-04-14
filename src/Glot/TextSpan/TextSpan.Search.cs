using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    // Maximum bytes to stack-allocate for the cross-encoding search pattern.
    // When the search value exceeds this limit in the target encoding, only a prefix
    // is used for SIMD scanning; each hit is verified rune-by-rune for the remainder.
    // 512 bytes covers patterns up to ~128 runes (UTF-32) or ~512 ASCII chars (UTF-8).
    const int CrossEncodingPatternLimit = 512;

    // Maximum codepoints for the ASCII narrowing fast path.
    // Patterns within this limit are checked for all-ASCII and narrowed directly to
    // single-byte form, bypassing Transcode and generic dispatch entirely.
    const int AsciiNarrowLimit = 128;

    /// <summary>Returns <c>true</c> if this span contains the specified value, compared rune-by-rune across encodings.</summary>
    public bool Contains(TextSpan value) => IndexOfByteOffset(value) >= 0;

    public bool Contains(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => Contains(Uncounted(value, encoding));

    public bool Contains(ReadOnlySpan<char> value)
        => Contains(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public bool Contains(ReadOnlySpan<int> value)
        => Contains(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based rune index of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int RuneIndexOf(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        var bytePos = IndexOfByteOffset(value);
        if (bytePos < 0)
        {
            return -1;
        }

        return RuneCount.Count(Bytes[..bytePos], Encoding);
    }

    public int RuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => RuneIndexOf(Uncounted(value, encoding));

    public int RuneIndexOf(ReadOnlySpan<char> value)
        => RuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int RuneIndexOf(ReadOnlySpan<int> value)
        => RuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based rune index of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int LastRuneIndexOf(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return RuneLength;
        }

        var bytePos = LastIndexOfByteOffset(value);
        if (bytePos < 0)
        {
            return -1;
        }

        return RuneCount.Count(Bytes[..bytePos], Encoding);
    }

    public int LastRuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => LastRuneIndexOf(Uncounted(value, encoding));

    public int LastRuneIndexOf(ReadOnlySpan<char> value)
        => LastRuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int LastRuneIndexOf(ReadOnlySpan<int> value)
        => LastRuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ByteIndexOf(TextSpan value) => IndexOfByteOffset(value);

    public int ByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => ByteIndexOf(Uncounted(value, encoding));

    public int ByteIndexOf(ReadOnlySpan<char> value)
        => ByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int ByteIndexOf(ReadOnlySpan<int> value)
        => ByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LastByteIndexOf(TextSpan value) => LastIndexOfByteOffset(value);

    public int LastByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => LastByteIndexOf(Uncounted(value, encoding));

    public int LastByteIndexOf(ReadOnlySpan<char> value)
        => LastByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int LastByteIndexOf(ReadOnlySpan<int> value)
        => LastByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns <c>true</c> if this span begins with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool StartsWith(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == value.Encoding)
        {
            return Bytes.StartsWith(value.Bytes);
        }

        return RunePrefix.TryMatch(Bytes, Encoding, value.Bytes, value.Encoding, out _);
    }

    public bool StartsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => StartsWith(Uncounted(value, encoding));

    public bool StartsWith(ReadOnlySpan<char> value)
        => StartsWith(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public bool StartsWith(ReadOnlySpan<int> value)
        => StartsWith(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns <c>true</c> if this span ends with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool EndsWith(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == value.Encoding)
        {
            return Bytes.EndsWith(value.Bytes);
        }

        // Cross-encoding: compare from the end, rune by rune. No offset calculation needed.
        var s = Bytes;
        var v = value.Bytes;

        while (!v.IsEmpty)
        {
            if (s.IsEmpty)
            {
                return false;
            }

            Rune.TryDecodeLast(
                s,
                Encoding,
                out var sr,
                out var sc);

            Rune.TryDecodeLast(
                v,
                value.Encoding,
                out var vr,
                out var vc);

            if (sr != vr)
            {
                return false;
            }

            s = s[..^sc];
            v = v[..^vc];
        }

        return true;
    }

    public bool EndsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => EndsWith(Uncounted(value, encoding));

    public bool EndsWith(ReadOnlySpan<char> value)
        => EndsWith(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public bool EndsWith(ReadOnlySpan<int> value)
        => EndsWith(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    // Internal byte-offset search.
    // Same encoding: runtime's SIMD Span.IndexOf.
    // Cross-encoding: encode min(limit, value) as a prefix in this encoding (stackalloc),
    // SIMD-scan for the prefix, verify remainder at each hit.
    // If the full value fits in the limit, no verification needed.

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IndexOfByteOffset(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        return Encoding == value.Encoding ? Bytes.IndexOf(value.Bytes) : IndexOfCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int LastIndexOfByteOffset(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return Bytes.Length;
        }

        return Encoding == value.Encoding ? Bytes.LastIndexOf(value.Bytes) : LastIndexOfCrossEncoding(value);
    }

    // Cross-encoding search: encode as many value runes as fit in the limit, in this encoding.
    // SIMD-scan for that prefix. Dispatch to typed spans for correct alignment.

    int IndexOfCrossEncoding(TextSpan value)
    {
        // ASCII fast path: when the haystack is UTF-8 and the needle is all-ASCII,
        // narrow the needle directly to single-byte form and use SIMD IndexOf.
        if (Encoding == TextEncoding.Utf8)
        {
            Span<byte> narrowBuf = stackalloc byte[AsciiNarrowLimit];
            if (TryNarrowAsciiToUtf8(value, narrowBuf, out var narrowLen))
            {
                return Bytes.IndexOf(narrowBuf[..narrowLen]);
            }
        }

        var patternBufLen = Math.Min(CrossEncodingPatternLimit, Math.Max(value.Bytes.Length, 4));
        Span<byte> patternBuf = stackalloc byte[patternBufLen];
        var patternLen = value.Transcode(
            patternBuf,
            Encoding,
            out var fullyEncoded,
            out var valuePrefixByteLen);

        var pattern = patternBuf[..patternLen];

        return Encoding switch
        {
            TextEncoding.Utf8 => FindForward(
                Bytes,
                pattern,
                1,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf16 => FindForward(
                MemoryMarshal.Cast<byte, char>(Bytes),
                MemoryMarshal.Cast<byte, char>(pattern),
                2,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf32 => FindForward(
                MemoryMarshal.Cast<byte, int>(Bytes),
                MemoryMarshal.Cast<byte, int>(pattern),
                4,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            _ => throw new InvalidEncodingException(Encoding),
        };
    }

    int LastIndexOfCrossEncoding(TextSpan value)
    {
        // ASCII fast path: when the haystack is UTF-8 and the needle is all-ASCII,
        // narrow the needle directly to single-byte form and use SIMD LastIndexOf.
        if (Encoding == TextEncoding.Utf8)
        {
            Span<byte> narrowBuf = stackalloc byte[AsciiNarrowLimit];
            if (TryNarrowAsciiToUtf8(value, narrowBuf, out var narrowLen))
            {
                return Bytes.LastIndexOf(narrowBuf[..narrowLen]);
            }
        }

        var patternBufLen = Math.Min(CrossEncodingPatternLimit, Math.Max(value.Bytes.Length, 4));
        Span<byte> patternBuf = stackalloc byte[patternBufLen];
        var patternLen = value.Transcode(
            patternBuf,
            Encoding,
            out var fullyEncoded,
            out var valuePrefixByteLen);

        var pattern = patternBuf[..patternLen];

        return Encoding switch
        {
            TextEncoding.Utf8 => FindBackward(
                Bytes,
                pattern,
                1,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf16 => FindBackward(
                MemoryMarshal.Cast<byte, char>(Bytes),
                MemoryMarshal.Cast<byte, char>(pattern),
                2,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf32 => FindBackward(
                MemoryMarshal.Cast<byte, int>(Bytes),
                MemoryMarshal.Cast<byte, int>(pattern),
                4,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            _ => throw new InvalidEncodingException(Encoding),
        };
    }

    // Generic forward/backward search — one implementation for all encodings.
    // T = byte (UTF-8), char (UTF-16), or int (UTF-32). elementSize converts element offset → byte offset.

    int FindForward<T>(
        ReadOnlySpan<T> source,
        ReadOnlySpan<T> pattern,
        int elementSize,
        TextSpan value,
        bool fullyEncoded,
        int valuePrefixByteLen)
        where T : IEquatable<T>
    {
        var offset = 0;

        while (offset + pattern.Length <= source.Length)
        {
            var pos = source[offset..].IndexOf(pattern);
            if (pos < 0)
            {
                return -1;
            }

            offset += pos;
            var byteOffset = offset * elementSize;

            if (fullyEncoded ||
                VerifyRuneSuffix(byteOffset + pattern.Length * elementSize, value, valuePrefixByteLen))
            {
                return byteOffset;
            }

            offset += pattern.Length;
        }

        return -1;
    }

    int FindBackward<T>(
        ReadOnlySpan<T> source,
        ReadOnlySpan<T> pattern,
        int elementSize,
        TextSpan value,
        bool fullyEncoded,
        int valuePrefixByteLen)
        where T : IEquatable<T>
    {
        var searchLen = source.Length;

        while (searchLen >= pattern.Length)
        {
            var pos = source[..searchLen].LastIndexOf(pattern);
            if (pos < 0)
            {
                return -1;
            }

            var byteOffset = pos * elementSize;

            if (fullyEncoded ||
                VerifyRuneSuffix(byteOffset + pattern.Length * elementSize, value, valuePrefixByteLen))
            {
                return byteOffset;
            }

            searchLen = pos;
        }

        return -1;
    }

    // Verify remainder of value after the prefix already matched via SIMD.
    bool VerifyRuneSuffix(int remainderByteOffset, TextSpan value, int valuePrefixByteLen)
    {
        var valueRemainder = value.Bytes[valuePrefixByteLen..];

        if (valueRemainder.IsEmpty)
        {
            return true;
        }

        return RunePrefix.TryMatch(
            Bytes[remainderByteOffset..],
            Encoding,
            valueRemainder,
            value.Encoding,
            out _);
    }

    // Try to narrow a UTF-16 or UTF-32 pattern to UTF-8 bytes when every codepoint is ASCII.
    // Returns false when the pattern is non-ASCII, too large, or already UTF-8.
    static bool TryNarrowAsciiToUtf8(TextSpan value, Span<byte> utf8, out int length)
    {
        switch (value.Encoding)
        {
            case TextEncoding.Utf16:
            {
                var chars = MemoryMarshal.Cast<byte, char>(value.Bytes);
                if (chars.Length > utf8.Length)
                {
                    length = 0;
                    return false;
                }

                for (var i = 0; i < chars.Length; i++)
                {
                    if (chars[i] > 0x7F)
                    {
                        length = 0;
                        return false;
                    }

                    utf8[i] = (byte)chars[i];
                }

                length = chars.Length;
                return true;
            }

            case TextEncoding.Utf32:
            {
                var ints = MemoryMarshal.Cast<byte, int>(value.Bytes);
                if (ints.Length > utf8.Length)
                {
                    length = 0;
                    return false;
                }

                for (var i = 0; i < ints.Length; i++)
                {
                    if ((uint)ints[i] > 0x7F)
                    {
                        length = 0;
                        return false;
                    }

                    utf8[i] = (byte)ints[i];
                }

                length = ints.Length;
                return true;
            }

            default:
                length = 0;
                return false;
        }
    }
}
