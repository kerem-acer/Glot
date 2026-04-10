using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
using static Glot.EncodingConstants;

namespace Glot;

static class RuneExtensions
{

    extension(Rune rune)
    {
        public int GetByteCount(TextEncoding encoding)
        {
            return encoding switch
            {
                TextEncoding.Utf8 => rune.Utf8SequenceLength,
                TextEncoding.Utf16 => rune.Utf16SequenceLength * 2,
                TextEncoding.Utf32 => 4,
                _ => throw new InvalidEncodingException(encoding),
            };
        }

        public int EncodeTo(Span<byte> destination, TextEncoding encoding)
        {
            switch (encoding)
            {
                case TextEncoding.Utf8:
                    rune.TryEncodeToUtf8(destination, out var utf8Written);
                    return utf8Written;

                case TextEncoding.Utf16:
                    rune.TryEncodeToUtf16(MemoryMarshal.Cast<byte, char>(destination), out var charsWritten);
                    return charsWritten * 2;

                case TextEncoding.Utf32:
                    MemoryMarshal.Cast<byte, int>(destination)[0] = rune.Value;
                    return 4;

                default:
                    throw new InvalidEncodingException(encoding);
            }
        }
    }

    extension(Rune)
    {
        public static bool TryDecodeFirst(ReadOnlySpan<byte> bytes, TextEncoding encoding, out Rune rune, out int bytesConsumed)
        {
            switch (encoding)
            {
                case TextEncoding.Utf8:
                    {
                        var status = Rune.DecodeFromUtf8(bytes, out rune, out bytesConsumed);
                        if (status == OperationStatus.Done)
                        {
                            return true;
                        }

                        rune = Rune.ReplacementChar;
                        bytesConsumed = Math.Max(bytesConsumed, 1);
                        return false;
                    }

                case TextEncoding.Utf16:
                    {
                        var chars = MemoryMarshal.Cast<byte, char>(bytes);
                        if (chars.IsEmpty)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        var status = Rune.DecodeFromUtf16(chars, out rune, out var charsConsumed);
                        if (status == OperationStatus.Done)
                        {
                            bytesConsumed = charsConsumed * 2;
                            return true;
                        }
                        rune = Rune.ReplacementChar;
                        bytesConsumed = Math.Max(charsConsumed, 1) * 2;
                        return false;
                    }

                case TextEncoding.Utf32:
                    {
                        if (bytes.Length < 4)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        var value = MemoryMarshal.Cast<byte, int>(bytes)[0];
                        bytesConsumed = 4;
                        if (Rune.IsValid(value))
                        {
                            rune = new Rune(value);
                            return true;
                        }
                        rune = Rune.ReplacementChar;
                        return false;
                    }

                default:
                    throw new InvalidEncodingException(encoding);
            }
        }

        public static bool TryDecodeLast(ReadOnlySpan<byte> bytes, TextEncoding encoding, out Rune rune, out int bytesConsumed)
        {
            switch (encoding)
            {
                case TextEncoding.Utf8:
                    {
                        var i = bytes.Length - 1;
                        while (i > 0 && (bytes[i] & Utf8LeadByteMask) == Utf8ContinuationMarker)
                        {
                            i--;
                        }

                        bytesConsumed = bytes.Length - i;
                        var status = Rune.DecodeFromUtf8(bytes[i..], out rune, out _);
                        if (status == OperationStatus.Done)
                        {
                            return true;
                        }

                        rune = Rune.ReplacementChar;
                        return false;
                    }

                case TextEncoding.Utf16:
                    {
                        var chars = MemoryMarshal.Cast<byte, char>(bytes);
                        if (chars.IsEmpty)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        var i = chars.Length - 1;
                        if (i > 0 && char.IsLowSurrogate(chars[i]) && char.IsHighSurrogate(chars[i - 1]))
                        {
                            i--;
                        }

                        bytesConsumed = (chars.Length - i) * 2;
                        var status = Rune.DecodeFromUtf16(chars[i..], out rune, out _);
                        if (status == OperationStatus.Done)
                        {
                            return true;
                        }

                        rune = Rune.ReplacementChar;
                        return false;
                    }

                case TextEncoding.Utf32:
                    {
                        if (bytes.Length < 4)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        var value = MemoryMarshal.Cast<byte, int>(bytes[^4..])[0];
                        bytesConsumed = 4;
                        if (Rune.IsValid(value))
                        {
                            rune = new Rune(value);
                            return true;
                        }
                        rune = Rune.ReplacementChar;
                        return false;
                    }

                default:
                    throw new InvalidEncodingException(encoding);
            }
        }
    }

}
