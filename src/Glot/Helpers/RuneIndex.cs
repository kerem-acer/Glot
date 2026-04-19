using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Glot.EncodingConstants;

namespace Glot;

/// <summary>
/// Converts rune-level offsets to byte offsets within encoded byte spans.
/// </summary>
static class RuneIndex
{

    public static int ToByteOffset(ReadOnlySpan<byte> bytes, TextEncoding encoding, int runeOffset)
    {
        if (runeOffset == 0)
        {
            return 0;
        }

        return encoding switch
        {
            TextEncoding.Utf32 => runeOffset * 4,
            TextEncoding.Utf8 => ToByteOffsetUtf8(bytes, runeOffset),
            TextEncoding.Utf16 => ToByteOffsetUtf16(
                MemoryMarshal.Cast<byte, char>(bytes), runeOffset),
            _ => throw new InvalidEncodingException(encoding),
        };
    }

    static int ToByteOffsetUtf8(ReadOnlySpan<byte> bytes, int runeOffset)
    {
        var offset = 0;
        var runesRemaining = runeOffset;

        // SIMD path: count rune starts per vector, skip full vectors when possible.
        if (Vector.IsHardwareAccelerated && bytes.Length >= Vector<byte>.Count)
        {
            var threshold = new Vector<sbyte>(Utf8RuneStartThreshold);
            var vectorSize = Vector<byte>.Count;

            while (offset + vectorSize <= bytes.Length && runesRemaining >= vectorSize)
            {
                var vecBytes = bytes.LoadVector(offset);
                var vec = Unsafe.As<Vector<byte>, Vector<sbyte>>(ref vecBytes);
                var mask = Vector.GreaterThanOrEqual(vec, threshold);
                var maskBytes = Unsafe.As<Vector<sbyte>, Vector<byte>>(ref mask);
                // maskBytes lanes are 0xFF or 0x00, so sum is always a multiple of 255.
                var runeStartCount = maskBytes.HorizontalSum() / byte.MaxValue;

                if (runesRemaining >= runeStartCount)
                {
                    runesRemaining -= runeStartCount;
                    offset += vectorSize;
                }
                else
                {
                    break;
                }
            }
        }

        // Scalar tail: skip runesRemaining rune starts, return byte offset of the next one.
        while (offset < bytes.Length)
        {
            if ((bytes[offset] & Utf8LeadByteMask) != Utf8ContinuationMarker)
            {
                if (runesRemaining == 0)
                {
                    return offset;
                }

                runesRemaining--;
            }

            offset++;
        }

        // Landed exactly at end — valid for offset == Length
        return runesRemaining == 0 ? offset : throw new ArgumentOutOfRangeException(nameof(runeOffset));
    }

    static int ToByteOffsetUtf16(ReadOnlySpan<char> chars, int runeOffset)
    {
        var charOffset = 0;
        var runesRemaining = runeOffset;

        // SIMD path: count non-surrogates per vector, skip full vectors when possible.
        if (Vector.IsHardwareAccelerated && chars.Length >= Vector<ushort>.Count)
        {
            var ushorts = MemoryMarshal.Cast<char, ushort>(chars);
            var mask = new Vector<ushort>(Utf16SurrogateMask);
            var marker = new Vector<ushort>(Utf16LowSurrogateMarker);
            var vectorSize = Vector<ushort>.Count;

            while (charOffset + vectorSize <= chars.Length && runesRemaining >= vectorSize)
            {
                var vec = ushorts.LoadVector(charOffset);
                var masked = Vector.BitwiseAnd(vec, mask);
                var isSurrogate = Vector.Equals(masked, marker);
                // isSurrogate lanes are 0xFFFF or 0x0000, so sum is always a multiple of ushort.MaxValue.
                var surrogateCount = isSurrogate.HorizontalSum() / ushort.MaxValue;
                var runeStartCount = vectorSize - surrogateCount;

                if (runesRemaining >= runeStartCount)
                {
                    runesRemaining -= runeStartCount;
                    charOffset += vectorSize;
                }
                else
                {
                    break;
                }
            }
        }

        // Scalar tail: skip runesRemaining rune starts, return byte offset of the next one.
        while (charOffset < chars.Length)
        {
            if (!char.IsLowSurrogate(chars[charOffset]))
            {
                if (runesRemaining == 0)
                {
                    return charOffset * 2;
                }

                runesRemaining--;
            }

            charOffset++;
        }

        if (runesRemaining == 0)
        {
            return charOffset * 2;
        }

        throw new ArgumentOutOfRangeException(nameof(runeOffset));
    }
}
