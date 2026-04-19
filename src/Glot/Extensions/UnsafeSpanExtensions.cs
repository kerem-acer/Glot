using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace Glot;

static class UnsafeSpanExtensions
{
    extension(string s)
    {
        /// <summary>
        /// Returns a <see cref="ReadOnlySpan{T}"/> over a char slice of the string without
        /// the null / bounds / range validation that <see cref="MemoryExtensions.AsSpan(string, int, int)"/> performs.
        /// Callers must guarantee <paramref name="s"/> is non-null and the slice is in range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ReadOnlySpan<char> AsUnsafeSpan()
        {
#if NET6_0_OR_GREATER
            return MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.AsRef(in s.GetPinnableReference()),
                s.Length);
#else
            return s.AsSpan();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ReadOnlySpan<char> AsUnsafeSpan(int start, int length)
        {
#if NET6_0_OR_GREATER
            ref var first = ref Unsafe.Add(
                ref Unsafe.AsRef(in s.GetPinnableReference()),
                start);
            return MemoryMarshal.CreateReadOnlySpan(ref first, length);
#else
            return s.AsSpan(start, length);
#endif
        }
    }

    extension<T>(T[] array)
    {
        /// <summary>
        /// Returns a <see cref="Span{T}"/> over an element slice of the array without the
        /// null / bounds validation that <see cref="MemoryExtensions.AsSpan{T}(T[], int, int)"/> performs.
        /// Callers must guarantee <paramref name="array"/> is non-null and the slice is in range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Span<T> AsUnsafeSpan()
        {
#if NET6_0_OR_GREATER
            return MemoryMarshal.CreateSpan(
                ref MemoryMarshal.GetArrayDataReference(array),
                array.Length);
#else
            return array.AsSpan();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Span<T> AsUnsafeSpan(int start, int length)
        {
#if NET6_0_OR_GREATER
            ref var first = ref Unsafe.Add(
                ref MemoryMarshal.GetArrayDataReference(array),
                start);
            return MemoryMarshal.CreateSpan(ref first, length);
#else
            return array.AsSpan(start, length);
#endif
        }
    }
}
