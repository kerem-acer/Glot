using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Glot;

/// <summary>
/// Fast-path ASCII detection for cross-encoding operations.
/// When both sides of a comparison have an ASCII code unit at the current position,
/// the rune value equals the byte value — no full rune decoding needed.
/// </summary>
static class AsciiHelper
{
    /// <summary>
    /// Reads the leading code unit. If it is ASCII (0x00–0x7F), returns <c>true</c>
    /// with the ASCII value and the number of bytes that code unit occupies.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadAscii(ReadOnlySpan<byte> bytes, TextEncoding encoding, out byte ascii, out int size)
    {
        switch (encoding)
        {
            case TextEncoding.Utf8:
                if (!bytes.IsEmpty && bytes[0] < 0x80)
                {
                    ascii = bytes[0];
                    size = 1;
                    return true;
                }

                break;

            case TextEncoding.Utf16:
                if (bytes.Length >= 2)
                {
                    var ch = MemoryMarshal.Read<char>(bytes);
                    if (ch < (char)0x80)
                    {
                        ascii = (byte)ch;
                        size = 2;
                        return true;
                    }
                }

                break;

            case TextEncoding.Utf32:
                if (bytes.Length >= 4)
                {
                    var cp = MemoryMarshal.Read<int>(bytes);
                    if ((uint)cp < 0x80)
                    {
                        ascii = (byte)cp;
                        size = 4;
                        return true;
                    }
                }

                break;
        }

        ascii = 0;
        size = 0;
        return false;
    }
}
