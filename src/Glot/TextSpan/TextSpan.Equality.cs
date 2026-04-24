using System.Buffers;
using System.IO.Hashing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    // Size of the stackalloc buffer used for streaming cross-encoding compare/equals.
    // Sized to balance per-iteration overhead vs short-circuit granularity on large inputs.
    const int CrossEncodingChunkBytes = 512;

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
        // Streaming compare: transcode `other` into a fixed-size chunk of this encoding,
        // compare that chunk to the equivalent slice of `Bytes`, and short-circuit on the
        // first byte mismatch.
        Span<byte> chunk = stackalloc byte[CrossEncodingChunkBytes];

        var thisBytes = Bytes;
        var thisOffset = 0;
        var otherRemaining = other.Bytes;
        var otherEncoding = other.Encoding;

        while (!otherRemaining.IsEmpty)
        {
            var chunkSource = new TextSpan(otherRemaining, otherEncoding, 0);
            var written = chunkSource.Transcode(chunk, Encoding, out _, out var consumed);

            if (written == 0)
            {
                // No forward progress — malformed or impossible transcode.
                return false;
            }

            if (thisOffset + written > thisBytes.Length
                || !thisBytes.Slice(thisOffset, written).SequenceEqual(chunk[..written]))
            {
                return false;
            }

            thisOffset += written;
            otherRemaining = otherRemaining[consumed..];
        }

        return thisOffset == thisBytes.Length;
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

        // UTF-32: each 4-byte int is a rune value — hash directly.
        if (encoding == TextEncoding.Utf32)
        {
            var hash = XxHash3.HashToUInt64(bytes);
            return (int)hash ^ (int)(hash >> 32);
        }

        // UTF-8/UTF-16: decode runes to int values, then XxHash3 the buffer.
        var maxRunes = encoding == TextEncoding.Utf8 ? bytes.Length : bytes.Length / 2;

        int[]? rented = null;
        Span<int> runes = maxRunes <= 256
            ? stackalloc int[maxRunes]
            : (rented = ArrayPool<int>.Shared.Rent(maxRunes));

        try
        {
            var count = 0;
            var remaining = bytes;

            while (!remaining.IsEmpty)
            {
                Rune.TryDecodeFirst(remaining, encoding, out var rune, out var consumed);
                runes[count++] = rune.Value;
                remaining = remaining[consumed..];
            }

            var runeBytes = MemoryMarshal.AsBytes(runes[..count]);
            var hash = XxHash3.HashToUInt64(runeBytes);
            return (int)hash ^ (int)(hash >> 32);
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<int>.Shared.Return(rented);
            }
        }
    }

    /// <summary>Compares two spans lexicographically by rune value, regardless of encoding.</summary>
    public int CompareTo(TextSpan other)
    {
        if (Encoding == other.Encoding)
        {
            // UTF-8 byte order preserves Unicode scalar order.
            if (Encoding == TextEncoding.Utf8)
            {
                return Bytes.SequenceCompareTo(other.Bytes);
            }

            // UTF-32 stored in native-endian ints: each int is a Unicode scalar, so
            // int-by-int compare is rune-ordered. Byte compare is NOT rune-ordered
            // because bytes within an int don't carry scalar sign.
            if (Encoding == TextEncoding.Utf32)
            {
                return CompareUtf32(Bytes, other.Bytes);
            }

            // UTF-16: byte order breaks at surrogate boundaries. Walk rune-by-rune.
            return CompareSameEncodingByRune(other);
        }

        return CompareToCrossEncoding(other);
    }

    static int CompareUtf32(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        var aInts = MemoryMarshal.Cast<byte, int>(a);
        var bInts = MemoryMarshal.Cast<byte, int>(b);
        var shared = Math.Min(aInts.Length, bInts.Length);
        for (var i = 0; i < shared; i++)
        {
            if (aInts[i] != bInts[i])
            {
                return aInts[i].CompareTo(bInts[i]);
            }
        }

        return aInts.Length.CompareTo(bInts.Length);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int CompareSameEncodingByRune(TextSpan other)
    {
        // Same-encoding UTF-16: decode runes and compare by scalar value.
        var aRem = Bytes;
        var bRem = other.Bytes;
        var enc = Encoding;

        while (!aRem.IsEmpty && !bRem.IsEmpty)
        {
            Rune.TryDecodeFirst(aRem, enc, out var ar, out var ac);
            Rune.TryDecodeFirst(bRem, enc, out var br, out var bc);
            if (ar.Value != br.Value)
            {
                return ar.Value.CompareTo(br.Value);
            }

            aRem = aRem[ac..];
            bRem = bRem[bc..];
        }

        return aRem.IsEmpty ? (bRem.IsEmpty ? 0 : -1) : 1;
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
        // Streaming compare: transcode a chunk at a time and short-circuit on the first
        // non-zero SequenceCompareTo. UTF-8 byte order preserves Unicode scalar order, so
        // any byte-level difference reflects the correct rune ordering.
        Span<byte> chunk = stackalloc byte[CrossEncodingChunkBytes];

        var otherRemaining = toTranscode.Bytes;
        var otherEncoding = toTranscode.Encoding;
        var utf8Offset = 0;

        while (!otherRemaining.IsEmpty)
        {
            var chunkSource = new TextSpan(otherRemaining, otherEncoding, 0);
            var written = chunkSource.Transcode(chunk, TextEncoding.Utf8, out _, out var consumed);

            if (written == 0)
            {
                break;
            }

            var utf8Remaining = utf8Bytes.Length - utf8Offset;
            var compareLen = Math.Min(written, utf8Remaining);
            var cmp = utf8Bytes.Slice(utf8Offset, compareLen).SequenceCompareTo(chunk[..compareLen]);
            if (cmp != 0)
            {
                return cmp;
            }

            if (written > utf8Remaining)
            {
                // utf8Bytes exhausted but transcoded chunk has more — toTranscode is longer.
                return -1;
            }

            utf8Offset += written;
            otherRemaining = otherRemaining[consumed..];
        }

        return utf8Bytes.Length - utf8Offset;
    }

    static int CompareBothTranscoded(TextSpan a, TextSpan b)
    {
        // Streaming compare for both-non-UTF-8: transcode each side into its own
        // stackalloc UTF-8 chunk, SequenceCompareTo the shared prefix, short-circuit
        // on the first non-zero compare, advance by the consumed prefix and refill.
        // UTF-8 byte order preserves Unicode scalar order, so byte-level differences
        // reflect correct rune ordering.
        Span<byte> chunkA = stackalloc byte[CrossEncodingChunkBytes];
        Span<byte> chunkB = stackalloc byte[CrossEncodingChunkBytes];

        var aRem = a.Bytes;
        var bRem = b.Bytes;
        var aEnc = a.Encoding;
        var bEnc = b.Encoding;
        scoped ReadOnlySpan<byte> aBuf = default;
        scoped ReadOnlySpan<byte> bBuf = default;

        while (true)
        {
            if (aBuf.IsEmpty)
            {
                if (aRem.IsEmpty)
                {
                    break;
                }

                var src = new TextSpan(aRem, aEnc, 0);
                var written = src.Transcode(chunkA, TextEncoding.Utf8, out _, out var consumed);
                if (written == 0)
                {
                    break;
                }

                aBuf = chunkA[..written];
                aRem = aRem[consumed..];
            }

            if (bBuf.IsEmpty)
            {
                if (bRem.IsEmpty)
                {
                    break;
                }

                var src = new TextSpan(bRem, bEnc, 0);
                var written = src.Transcode(chunkB, TextEncoding.Utf8, out _, out var consumed);
                if (written == 0)
                {
                    break;
                }

                bBuf = chunkB[..written];
                bRem = bRem[consumed..];
            }

            var shared = Math.Min(aBuf.Length, bBuf.Length);
            var cmp = aBuf[..shared].SequenceCompareTo(bBuf[..shared]);
            if (cmp != 0)
            {
                return cmp;
            }

            aBuf = aBuf[shared..];
            bBuf = bBuf[shared..];
        }

        // Loop exited with one or both sides exhausted: whichever still has bytes is greater.
        var aHasMore = !aBuf.IsEmpty || !aRem.IsEmpty;
        var bHasMore = !bBuf.IsEmpty || !bRem.IsEmpty;
        if (aHasMore && !bHasMore)
        {
            return 1;
        }

        if (!aHasMore && bHasMore)
        {
            return -1;
        }

        return 0;
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

    /// <inheritdoc cref="Equals(TextSpan)"/>
    public static bool operator ==(TextSpan left, TextSpan right) => left.Equals(right);

    /// <summary>Returns <c>true</c> if the two spans do not contain the same rune sequence.</summary>
    public static bool operator !=(TextSpan left, TextSpan right) => !left.Equals(right);
}
