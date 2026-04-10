#if NET6_0_OR_GREATER
using System.Numerics;
using System.Runtime.CompilerServices;
#endif
using System.Runtime.InteropServices;
using static Glot.EncodingConstants;

namespace Glot;

/// <summary>
/// Counts the number of Unicode runes (scalar values) in encoded byte spans.
/// </summary>
static class RuneCount
{
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

#if NET6_0_OR_GREATER
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
                    var vecBytes = new Vector<byte>(bytes[offset..]);
                    var vec = Unsafe.As<Vector<byte>, Vector<sbyte>>(ref vecBytes);
                    var mask = Vector.GreaterThanOrEqual(vec, threshold);
                    var maskBytes = Unsafe.As<Vector<sbyte>, Vector<byte>>(ref mask);
                    accumulated = Vector.Subtract(accumulated, maskBytes);
                    offset += vectorSize;
                }

                count += accumulated.HorizontalSum();
            }
        }
#endif

        // Scalar tail (handles remaining bytes, or all bytes on netstandard)
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

#if NET6_0_OR_GREATER
        // SIMD path using Vector<T> — auto-selects widest hardware vector.
        // Count low surrogates and subtract from total chars to get rune count.
        // Low surrogates: (char & SurrogateMask) == LowSurrogateMarker.
        // Equals produces all-ones for surrogates — subtract to accumulate surrogate count.
        // ushort lanes overflow at MaxUshortLaneBatch — flush periodically (~1M chars, practically never).
        if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count)
        {
            var mask = new Vector<ushort>(Utf16SurrogateMask);
            var marker = new Vector<ushort>(Utf16LowSurrogateMarker);
            var vectorSize = Vector<ushort>.Count;

            while (offset + vectorSize <= length)
            {
                var surrogates = Vector<ushort>.Zero;
                var batchEnd = Math.Min(MaxUshortLaneBatch, (length - offset) / vectorSize);

                for (var i = 0; i < batchEnd; i++)
                {
                    var vec = new Vector<ushort>(MemoryMarshal.Cast<char, ushort>(chars[offset..]));
                    var masked = Vector.BitwiseAnd(vec, mask);
                    var isSurrogate = Vector.Equals(masked, marker);
                    surrogates = Vector.Subtract(surrogates, isSurrogate);
                    offset += vectorSize;
                }

                count += batchEnd * vectorSize - surrogates.HorizontalSum();
            }
        }
#endif

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
