using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Glot.EncodingConstants;

namespace Glot;

/// <summary>
/// Counts the number of Unicode runes (scalar values) in encoded byte spans.
/// </summary>
static class RuneCount
{
    /// <summary>
    /// Returns the rune count in <paramref name="bytes"/>[..<paramref name="bytePos"/>].
    /// When <paramref name="totalRuneLength"/> is positive, picks the shorter scan direction.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountPrefix(
        ReadOnlySpan<byte> bytes,
        TextEncoding encoding,
        int bytePos,
        int totalRuneLength)
    {
        // UTF-32: every rune is exactly 4 bytes; byte offset divides cleanly to rune index.
        if (encoding == TextEncoding.Utf32)
        {
            return bytePos >> 2;
        }

        // Ascii-only UTF-8: rune count equals byte length, so rune index equals byte offset.
        // Detected via the invariant that Text populates `totalRuneLength` at construction.
        if (encoding == TextEncoding.Utf8 && totalRuneLength > 0 && totalRuneLength == bytes.Length)
        {
            return bytePos;
        }

        if (totalRuneLength > 0 && bytePos > (bytes.Length >> 1))
        {
            return totalRuneLength - Count(bytes[bytePos..], encoding);
        }

        return Count(bytes[..bytePos], encoding);
    }

    public static int Count(ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        return encoding switch
        {
            TextEncoding.Utf32 => bytes.Length / 4,
            TextEncoding.Utf8 => CountUtf8(bytes),
            TextEncoding.Utf16 => CountUtf16(MemoryMarshal.Cast<byte, char>(bytes)),
            _ => throw new InvalidEncodingException(encoding)
        };
    }

    static int CountUtf8(ReadOnlySpan<byte> bytes)
    {
        var count = 0;
        var offset = 0;
        var length = bytes.Length;

        // SIMD path using Vector<T> — auto-selects widest hardware vector (128/256/512).
        // Reinterpret bytes as signed: continuation bytes (10xxxxxx) map to signed [-128, -65].
        // All rune-start bytes (ASCII + lead bytes) are >= -64 signed.
        if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count)
        {
            var threshold = new Vector<sbyte>(Utf8RuneStartThreshold);
            var vectorSize = Vector<byte>.Count;

            // Process in batches of up to MaxByteLaneBatch vectors (prevents byte-lane overflow).
            while (offset + vectorSize <= length)
            {
                var accumulated = Vector<byte>.Zero;
                var batchEnd = Math.Min(MaxByteLaneBatch, (length - offset) / vectorSize);

                for (var i = 0; i < batchEnd; i++)
                {
                    var vecBytes = bytes.LoadVector(offset);
                    var vec = Unsafe.As<Vector<byte>, Vector<sbyte>>(ref vecBytes);
                    var mask = Vector.GreaterThanOrEqual(vec, threshold);
                    var maskBytes = Unsafe.As<Vector<sbyte>, Vector<byte>>(ref mask);
                    accumulated = Vector.Subtract(accumulated, maskBytes);
                    offset += vectorSize;
                }

                count += accumulated.HorizontalSum();
            }
        }

        // Scalar tail
        for (var i = offset; i < length; i++)
        {
            if ((bytes[i] & Utf8LeadByteMask) != Utf8ContinuationMarker)
            {
                count++;
            }
        }

        return count;
    }

    static int CountUtf16(ReadOnlySpan<char> chars)
    {
        var count = 0;
        var offset = 0;
        var length = chars.Length;

        // SIMD path using Vector<T> — auto-selects widest hardware vector.
        // Count low surrogates and subtract from total chars to get rune count.
        // Low surrogates: (char & SurrogateMask) == LowSurrogateMarker.
        // Equals produces all-ones for surrogates — subtract to accumulate surrogate count.
        // ushort lanes overflow at MaxUshortLaneBatch — flush periodically (~1M chars, practically never).
        if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count)
        {
            var ushorts = MemoryMarshal.Cast<char, ushort>(chars);
            var mask = new Vector<ushort>(Utf16SurrogateMask);
            var marker = new Vector<ushort>(Utf16LowSurrogateMarker);
            var vectorSize = Vector<ushort>.Count;

            while (offset + vectorSize <= length)
            {
                var surrogates = Vector<ushort>.Zero;
                var batchEnd = Math.Min(MaxUshortLaneBatch, (length - offset) / vectorSize);

                for (var i = 0; i < batchEnd; i++)
                {
                    var vec = ushorts.LoadVector(offset);
                    var masked = Vector.BitwiseAnd(vec, mask);
                    var isSurrogate = Vector.Equals(masked, marker);
                    surrogates = Vector.Subtract(surrogates, isSurrogate);
                    offset += vectorSize;
                }

                count += (batchEnd * vectorSize) - surrogates.HorizontalSum();
            }
        }

        // Scalar tail
        for (var i = offset; i < length; i++)
        {
            if (!char.IsLowSurrogate(chars[i]))
            {
                count++;
            }
        }

        return count;
    }
}
