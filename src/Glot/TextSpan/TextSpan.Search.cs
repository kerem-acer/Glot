using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    const int CrossEncodingPatternLimit = 512;

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

        return RuneCount.Count(_bytes[..bytePos], Encoding);
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

        return RuneCount.Count(_bytes[..bytePos], Encoding);
    }

    public int LastRuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => LastRuneIndexOf(Uncounted(value, encoding));

    public int LastRuneIndexOf(ReadOnlySpan<char> value)
        => LastRuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int LastRuneIndexOf(ReadOnlySpan<int> value)
        => LastRuneIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int ByteIndexOf(TextSpan value) => IndexOfByteOffset(value);

    public int ByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => ByteIndexOf(Uncounted(value, encoding));

    public int ByteIndexOf(ReadOnlySpan<char> value)
        => ByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    public int ByteIndexOf(ReadOnlySpan<int> value)
        => ByteIndexOf(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <summary>Returns the zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
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
            return _bytes.StartsWith(value._bytes);
        }

        return RunePrefix.TryMatch(_bytes, Encoding, value._bytes, value.Encoding, out _);
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
            return _bytes.EndsWith(value._bytes);
        }

        // Cross-encoding: compare from the end, rune by rune. No offset calculation needed.
        var s = _bytes;
        var v = value._bytes;

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

    int IndexOfByteOffset(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        return Encoding == value.Encoding ? _bytes.IndexOf(value._bytes) : IndexOfCrossEncoding(value);
    }

    int LastIndexOfByteOffset(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return _bytes.Length;
        }

        return Encoding == value.Encoding ? _bytes.LastIndexOf(value._bytes) : LastIndexOfCrossEncoding(value);
    }

    // Cross-encoding search: encode as many value runes as fit in the limit, in this encoding.
    // SIMD-scan for that prefix. Dispatch to typed spans for correct alignment.

    int IndexOfCrossEncoding(TextSpan value)
    {
        var patternBufLen = Math.Min(CrossEncodingPatternLimit, Math.Max(value._bytes.Length, 4));
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
                _bytes,
                pattern,
                1,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf16 => FindForward(
                MemoryMarshal.Cast<byte, char>(_bytes),
                MemoryMarshal.Cast<byte, char>(pattern),
                2,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf32 => FindForward(
                MemoryMarshal.Cast<byte, int>(_bytes),
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
        var patternBufLen = Math.Min(CrossEncodingPatternLimit, Math.Max(value._bytes.Length, 4));
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
                _bytes,
                pattern,
                1,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf16 => FindBackward(
                MemoryMarshal.Cast<byte, char>(_bytes),
                MemoryMarshal.Cast<byte, char>(pattern),
                2,
                value,
                fullyEncoded,
                valuePrefixByteLen),
            TextEncoding.Utf32 => FindBackward(
                MemoryMarshal.Cast<byte, int>(_bytes),
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
        var valueRemainder = value._bytes[valuePrefixByteLen..];

        if (valueRemainder.IsEmpty)
        {
            return true;
        }

        return RunePrefix.TryMatch(
            _bytes[remainderByteOffset..],
            Encoding,
            valueRemainder,
            value.Encoding,
            out _);
    }
}
