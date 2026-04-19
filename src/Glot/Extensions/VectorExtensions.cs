using System.Numerics;
using System.Runtime.CompilerServices;
#if !NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace Glot;

static class VectorExtensions
{
    extension<T>(ReadOnlySpan<T> span) where T : struct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> LoadVector(int offset)
        {
#if NET6_0_OR_GREATER
            return new Vector<T>(span[offset..]);
#else
            return Unsafe.ReadUnaligned<Vector<T>>(
                ref Unsafe.As<T, byte>(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset)));
#endif
        }
    }

    extension(Vector<byte> vec)
    {
        public int HorizontalSum()
        {
            Vector.Widen(vec, out var lo16, out var hi16);
            var sum16 = Vector.Add(lo16, hi16);

            Vector.Widen(sum16, out var lo32, out var hi32);
            var sum32 = Vector.Add(lo32, hi32);

            var sum = 0;
            for (var i = 0; i < Vector<uint>.Count; i++)
            {
                sum += (int)sum32[i];
            }

            return sum;
        }
    }

    extension(Vector<ushort> vec)
    {
        public int HorizontalSum()
        {
            Vector.Widen(vec, out var lo32, out var hi32);
            var sum32 = Vector.Add(lo32, hi32);

            var sum = 0;
            for (var i = 0; i < Vector<uint>.Count; i++)
            {
                sum += (int)sum32[i];
            }

            return sum;
        }
    }
}
