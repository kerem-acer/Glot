using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Returns <c>true</c> if this span and <paramref name="other"/> contain the same rune sequence, regardless of encoding.</summary>
    public bool Equals(TextSpan other)
    {
        // Same-encoding: byte equality implies rune equality. This assumes well-formed
        // input — two different malformed byte sequences could be byte-unequal yet decode
        // to the same replacement-char sequence. The library treats ordinal byte identity
        // as canonical for same-encoding comparison.
        if (Encoding == other.Encoding)
        {
            return Bytes.SequenceEqual(other.Bytes);
        }

        // Rune-length short-circuit: if both sides have a cached rune count and they
        // differ, the spans cannot be equal. O(1) when cached, skipped when uncached (0).
        var cachedRuneLength = _encodedLength.RuneLength;
        var otherCachedRuneLength = other._encodedLength.RuneLength;
        if (cachedRuneLength != 0 && otherCachedRuneLength != 0 && cachedRuneLength != otherCachedRuneLength)
        {
            return false;
        }

        if (Bytes.IsEmpty && other.Bytes.IsEmpty)
        {
            return true;
        }

        if (Bytes.IsEmpty || other.Bytes.IsEmpty)
        {
            return false;
        }

        return EqualsCrossEncoding(other);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    bool EqualsCrossEncoding(TextSpan other)
    {
        // Transcode `other` to match this encoding, then use SIMD SequenceEqual.
        var maxOutputBytes = EstimateTranscodeSize(other.Bytes.Length, other.Encoding, Encoding);

        byte[]? rented = null;
        Span<byte> buffer = maxOutputBytes <= 512
            ? stackalloc byte[maxOutputBytes]
            : (rented = ArrayPool<byte>.Shared.Rent(maxOutputBytes));

        try
        {
            var written = TranscodeToEncoding(other, buffer, Encoding);
            return Bytes.SequenceEqual(buffer[..written]);
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => Equals(Uncounted(value, encoding));

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<char> value)
        => Equals(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf16));

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public bool Equals(ReadOnlySpan<int> value)
        => Equals(Uncounted(MemoryMarshal.AsBytes(value), TextEncoding.Utf32));

    /// <inheritdoc/>
    public override bool Equals(object? obj) => false;

    /// <summary>Returns an encoding-independent hash code based on the rune sequence.</summary>
    public override int GetHashCode() => ComputeHashCode(Bytes, Encoding);

    internal static int ComputeHashCode(ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        if (bytes.IsEmpty)
        {
            return 0;
        }

        var hash = new HashCode();
        var remaining = bytes;

        while (!remaining.IsEmpty)
        {
            // ASCII fast path: ASCII byte value equals rune value, skip full decode.
            if (AsciiHelper.TryReadAscii(remaining, encoding, out var ascii, out var size))
            {
                hash.Add((int)ascii);
                remaining = remaining[size..];
                continue;
            }

            Rune.TryDecodeFirst(remaining, encoding, out var rune, out var consumed);
            hash.Add(rune.Value);
            remaining = remaining[consumed..];
        }

        return hash.ToHashCode();
    }

    /// <summary>Compares two spans lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(TextSpan other)
    {
        // UTF-8 byte ordering preserves Unicode scalar value ordering.
        if (Encoding == other.Encoding && Encoding == TextEncoding.Utf8)
        {
            return Bytes.SequenceCompareTo(other.Bytes);
        }

        // Same-encoding non-UTF-8: byte comparison may not equal rune ordering
        // (UTF-16 surrogates, UTF-32 endianness), so we must normalize.
        // Cross-encoding: likewise.
        return CompareToCrossEncoding(other);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int CompareToCrossEncoding(TextSpan other)
    {
        // Transcode both sides to UTF-8 where byte order = codepoint order,
        // then use SIMD SequenceCompareTo.
        if (Encoding == TextEncoding.Utf8)
        {
            return CompareUtf8WithTranscoded(Bytes, other);
        }

        if (other.Encoding == TextEncoding.Utf8)
        {
            return -CompareUtf8WithTranscoded(other.Bytes, this);
        }

        // Neither is UTF-8 — transcode both.
        return CompareBothTranscoded(this, other);
    }

    static int CompareUtf8WithTranscoded(ReadOnlySpan<byte> utf8Bytes, TextSpan toTranscode)
    {
        var maxBytes = EstimateTranscodeSize(toTranscode.Bytes.Length, toTranscode.Encoding, TextEncoding.Utf8);

        byte[]? rented = null;
        Span<byte> buffer = maxBytes <= 512
            ? stackalloc byte[maxBytes]
            : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));

        try
        {
            var written = toTranscode.EncodeToUtf8(buffer);
            return utf8Bytes.SequenceCompareTo(buffer[..written]);
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    static int CompareBothTranscoded(TextSpan a, TextSpan b)
    {
        var maxA = EstimateTranscodeSize(a.Bytes.Length, a.Encoding, TextEncoding.Utf8);
        var maxB = EstimateTranscodeSize(b.Bytes.Length, b.Encoding, TextEncoding.Utf8);

        byte[]? rentedA = null, rentedB = null;
        Span<byte> bufA = maxA <= 256
            ? stackalloc byte[maxA]
            : (rentedA = ArrayPool<byte>.Shared.Rent(maxA));
        Span<byte> bufB = maxB <= 256
            ? stackalloc byte[maxB]
            : (rentedB = ArrayPool<byte>.Shared.Rent(maxB));

        try
        {
            var writtenA = a.EncodeToUtf8(bufA);
            var writtenB = b.EncodeToUtf8(bufB);
            ReadOnlySpan<byte> spanA = bufA[..writtenA];
            return spanA.SequenceCompareTo(bufB[..writtenB]);
        }
        finally
        {
            if (rentedA is not null)
            {
                ArrayPool<byte>.Shared.Return(rentedA);
            }

            if (rentedB is not null)
            {
                ArrayPool<byte>.Shared.Return(rentedB);
            }
        }
    }

    internal static int TranscodeToEncoding(TextSpan source, Span<byte> destination, TextEncoding targetEncoding)
    {
        return targetEncoding switch
        {
            TextEncoding.Utf8 => source.EncodeToUtf8(destination),
            TextEncoding.Utf16 => source.EncodeToUtf16(MemoryMarshal.Cast<byte, char>(destination)) * 2,
            TextEncoding.Utf32 => source.Transcode(destination, TextEncoding.Utf32, out _, out _),
            _ => throw new InvalidEncodingException(targetEncoding)
        };
    }

    internal static int EstimateTranscodeSize(int sourceBytesLength, TextEncoding source, TextEncoding target)
    {
        return (source, target) switch
        {
            (TextEncoding.Utf16, TextEncoding.Utf8) => sourceBytesLength / 2 * 3,
            (TextEncoding.Utf8, TextEncoding.Utf16) => sourceBytesLength * 2,
            (TextEncoding.Utf32, TextEncoding.Utf8) => sourceBytesLength,
            (TextEncoding.Utf8, TextEncoding.Utf32) => sourceBytesLength * 4,
            (TextEncoding.Utf32, TextEncoding.Utf16) => sourceBytesLength,
            (TextEncoding.Utf16, TextEncoding.Utf32) => sourceBytesLength * 2,
            _ => sourceBytesLength * 4,
        };
    }

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public static bool operator ==(TextSpan left, TextSpan right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two spans do not contain the same rune sequence.</summary>
    public static bool operator !=(TextSpan left, TextSpan right) => !left.Equals(right);
}
