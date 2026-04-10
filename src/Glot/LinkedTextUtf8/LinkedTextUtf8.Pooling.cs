using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    static LinkedTextUtf8? s_pool;

    static LinkedTextUtf8 Rent()
    {
        LinkedTextUtf8? node;
        do
        {
            node = Volatile.Read(ref s_pool);
            if (node is null)
            {
                return new LinkedTextUtf8();
            }
        }
        while (Interlocked.CompareExchange(ref s_pool, node._poolNext, node) != node);

        node._poolNext = null;
        return node;
    }

    static void Return(LinkedTextUtf8 instance)
    {
        instance.Reset();

        LinkedTextUtf8? current;
        do
        {
            current = Volatile.Read(ref s_pool);
            instance._poolNext = current;
        }
        while (Interlocked.CompareExchange(ref s_pool, instance, current) != current);
    }

    LinkedTextUtf8? _poolNext;

    void Reset()
    {
#if NET8_0_OR_GREATER
        if (_overflowSegments is null && _segmentCount > 0)
        {
            ((Span<ReadOnlyMemory<byte>>)_inlineSegments)[.._segmentCount].Clear();
        }
#endif

        if (_overflowSegments is not null)
        {
            ArrayPool<ReadOnlyMemory<byte>>.Shared.Return(_overflowSegments, clearArray: true);
            _overflowSegments = null;
        }

        ReturnSequenceNodes();

        _segmentCount = 0;
        _totalLength = 0;
        _cachedSequence = null;
    }

    /// <summary>
    /// A disposable handle that returns the <see cref="LinkedTextUtf8"/> and all its
    /// rented resources to their pools on dispose.
    /// </summary>
    public struct Owned : IDisposable
    {
        LinkedTextUtf8? _data;

        internal Owned(LinkedTextUtf8 data)
        {
            _data = data;
        }

        /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
        public readonly bool IsDisposed => _data is null;

        /// <summary>The total byte count.</summary>
        public readonly int Length => _data?.Length ?? 0;

        /// <summary>Returns <c>true</c> if empty or disposed.</summary>
        public readonly bool IsEmpty => _data is null || _data.IsEmpty;

        /// <summary>The underlying <see cref="LinkedTextUtf8"/>. Valid only while not disposed.</summary>
        public readonly LinkedTextUtf8? Data => _data;

        /// <summary>Returns a <see cref="LinkedTextUtf8Span"/> over the full content.</summary>
        public readonly LinkedTextUtf8Span AsSpan()
        {
            if (_data is null)
            {
                return default;
            }

            return _data.AsSpan();
        }

        /// <summary>
        /// Returns all rented resources to their pools.
        /// </summary>
        public void Dispose()
        {
            if (_data is null)
            {
                return;
            }

            var data = _data;
            _data = null;
            LinkedTextUtf8.Return(data);
        }
    }
}
