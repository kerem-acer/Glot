using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    internal static readonly ObjectPool<LinkedTextUtf8> Pool =
        new DefaultObjectPool<LinkedTextUtf8>(new Policy(), 32);

    void Reset()
    {
#if NET8_0_OR_GREATER
        if (SegmentCount > 0)
        {
            var inlineCount = Math.Min(SegmentCount, InlineCapacity);
            ((Span<ReadOnlyMemory<byte>>)_inlineSegments)[..inlineCount].Clear();
        }
#endif

        if (_overflowSegments is not null)
        {
            ArrayPool<ReadOnlyMemory<byte>>.Shared.Return(_overflowSegments, clearArray: true);
            _overflowSegments = null;
        }

        ReturnSequenceNodes();

        SegmentCount = 0;
        Length = 0;
        _cachedSequence = null;
    }

    sealed class Policy : PooledObjectPolicy<LinkedTextUtf8>
    {
        public override LinkedTextUtf8 Create() => new();

        public override bool Return(LinkedTextUtf8 obj)
        {
            obj.Reset();
            return true;
        }
    }
}
