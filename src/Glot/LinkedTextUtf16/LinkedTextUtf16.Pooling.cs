using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    static LinkedTextUtf16? s_pool;

    static LinkedTextUtf16 Rent()
    {
        LinkedTextUtf16? node;
        do
        {
            node = Volatile.Read(ref s_pool);
            if (node is null)
            {
                return new LinkedTextUtf16();
            }
        }
        while (Interlocked.CompareExchange(ref s_pool, node._poolNext, node) != node);

        node._poolNext = null;
        return node;
    }

    static void Return(LinkedTextUtf16 instance)
    {
        instance.Reset();

        LinkedTextUtf16? current;
        do
        {
            current = Volatile.Read(ref s_pool);
            instance._poolNext = current;
        }
        while (Interlocked.CompareExchange(ref s_pool, instance, current) != current);
    }

    LinkedTextUtf16? _poolNext; // intrusive linked list for lock-free pool

    void Reset()
    {
#if NET8_0_OR_GREATER
        if (_overflowSegments is null && _segmentCount > 0)
        {
            ((Span<ReadOnlyMemory<char>>)_inlineSegments)[.._segmentCount].Clear();
        }
#endif

        if (_overflowSegments is not null)
        {
            ArrayPool<ReadOnlyMemory<char>>.Shared.Return(_overflowSegments, clearArray: true);
            _overflowSegments = null;
        }

        _formatPosition = 0;

        ReturnSequenceNodes();

        _segmentCount = 0;
        _totalLength = 0;
        _cachedSequence = null;
    }

    /// <summary>
    /// A disposable handle that returns the <see cref="LinkedTextUtf16"/> and all its
    /// rented resources (overflow arrays, sequence nodes) to their pools on dispose.
    /// </summary>
    public struct Owned : IDisposable
    {
        LinkedTextUtf16? _data;

        internal Owned(LinkedTextUtf16 data)
        {
            _data = data;
        }

        /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
        public readonly bool IsDisposed => _data is null;

        /// <summary>The total char count.</summary>
        public readonly int Length => _data?.Length ?? 0;

        /// <summary>Returns <c>true</c> if empty or disposed.</summary>
        public readonly bool IsEmpty => _data is null || _data.IsEmpty;

        /// <summary>The underlying <see cref="LinkedTextUtf16"/>. Valid only while not disposed.</summary>
        public readonly LinkedTextUtf16? Data => _data;

        /// <summary>Returns a <see cref="LinkedTextUtf16Span"/> over the full content.</summary>
        public readonly LinkedTextUtf16Span AsSpan()
        {
            if (_data is null)
            {
                return default;
            }

            return _data.AsSpan();
        }

        /// <summary>
        /// Returns overflow array to <see cref="ArrayPool{T}"/>, sequence nodes to their pool,
        /// and the <see cref="LinkedTextUtf16"/> instance to the object pool.
        /// </summary>
        public void Dispose()
        {
            if (_data is null)
            {
                return;
            }

            var data = _data;
            _data = null;
            LinkedTextUtf16.Return(data);
        }
    }
}
