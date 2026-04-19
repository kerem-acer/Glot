#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Glot;

/// <summary>
/// Computes the byte offset between the array data reference and the string char data
/// for the branchless <see cref="Text.UnsafeBytes"/> path.
/// </summary>
static class StringDataOffset
{
    internal static readonly byte Value = Calc();

    static byte Calc()
    {
        var s = "X";
        ref byte asArray = ref MemoryMarshal.GetArrayDataReference(Unsafe.As<string, byte[]>(ref s));
        ref byte asString = ref Unsafe.As<char, byte>(
            ref MemoryMarshal.GetReference(s.AsSpan()));
        return (byte)-(int)Unsafe.ByteOffset(ref asArray, ref asString);
    }
}
#endif
