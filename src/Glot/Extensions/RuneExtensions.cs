using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using static Glot.EncodingConstants;

namespace Glot;

static class RuneExtensions
{

    extension(Rune rune)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EncodeTo(Span<byte> destination, TextEncoding encoding)
        {
            switch (encoding)
            {
                case TextEncoding.Utf8:
                    rune.TryEncodeToUtf8(destination, out var utf8Written);
                    return utf8Written;

                case TextEncoding.Utf16:
                    Span<char> utf16Buf = stackalloc char[2];
                    rune.TryEncodeToUtf16(utf16Buf, out var charsWritten);
                    MemoryMarshal.AsBytes(utf16Buf[..charsWritten]).CopyTo(destination);
                    return charsWritten * 2;

                case TextEncoding.Utf32:
                    Span<int> utf32Buf = [rune.Value];
                    MemoryMarshal.AsBytes(utf32Buf).CopyTo(destination);
                    return 4;

                default:
                    throw new InvalidEncodingException(encoding);
            }
        }
    }

    extension(Rune)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                        var available = bytes.Length / 2;
                        if (available == 0)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        Span<char> chars = stackalloc char[2];
                        var charCount = Math.Min(available, 2);
                        for (var i = 0; i < charCount; i++)
                        {
                            chars[i] = MemoryMarshal.Read<char>(bytes[(i * 2)..]);
                        }
                        var status = Rune.DecodeFromUtf16(chars[..charCount], out rune, out var charsConsumed);
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
                        var value = MemoryMarshal.Read<int>(bytes);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryDecodeLast(ReadOnlySpan<byte> bytes, TextEncoding encoding, out Rune rune, out int bytesConsumed)
        {
            switch (encoding)
            {
                case TextEncoding.Utf8:
                    {
                        var i = bytes.Length - 1;
                        var minIndex = Math.Max(0, bytes.Length - 4);
                        while (i >= minIndex && (bytes[i] & Utf8LeadByteMask) == Utf8ContinuationMarker)
                        {
                            i--;
                        }

                        bytesConsumed = bytes.Length - i;
                        var status = Rune.DecodeFromUtf8(bytes[i..], out rune, out var decoded);

                        // Verify the decoded sequence covers exactly the bytes to the end.
                        // On malformed data (e.g. orphan continuation bytes), the lead byte may
                        // decode a shorter sequence that doesn't reach the end — treat as invalid.
                        if (status == OperationStatus.Done && decoded == bytesConsumed)
                        {
                            return true;
                        }

                        rune = Rune.ReplacementChar;
                        bytesConsumed = 1;
                        return false;
                    }

                case TextEncoding.Utf16:
                    {
                        var charLen = bytes.Length / 2;
                        if (charLen == 0)
                        {
                            rune = Rune.ReplacementChar;
                            bytesConsumed = bytes.Length;
                            return false;
                        }
                        var lastChar = MemoryMarshal.Read<char>(bytes[((charLen - 1) * 2)..]);
                        var startIndex = charLen - 1;
                        if (charLen > 1 && char.IsLowSurrogate(lastChar))
                        {
                            var prevChar = MemoryMarshal.Read<char>(bytes[((charLen - 2) * 2)..]);
                            if (char.IsHighSurrogate(prevChar))
                            {
                                startIndex = charLen - 2;
                            }
                        }
                        var runeCharCount = charLen - startIndex;
                        Span<char> buf = stackalloc char[2];
                        for (var j = 0; j < runeCharCount; j++)
                        {
                            buf[j] = MemoryMarshal.Read<char>(bytes[((startIndex + j) * 2)..]);
                        }
                        bytesConsumed = runeCharCount * 2;
                        var status = Rune.DecodeFromUtf16(buf[..runeCharCount], out rune, out _);
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
                        var value = MemoryMarshal.Read<int>(bytes[^4..]);
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
