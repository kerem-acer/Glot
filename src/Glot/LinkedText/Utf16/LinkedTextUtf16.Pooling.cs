using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    internal static readonly ObjectPool<LinkedTextUtf16> Pool =
        new DefaultObjectPool<LinkedTextUtf16>(new Policy(), 32);

    void Reset()
    {
        // Return pooled buffers from owned segments
        for (var i = 0; i < SegmentCount; i++)
        {
            var entry = GetSegmentEntry(i);
            if (entry.PooledBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(entry.PooledBuffer);
            }
        }

#if NET8_0_OR_GREATER
        if (SegmentCount > 0)
        {
            var inlineCount = Math.Min(SegmentCount, InlineCapacity);
            ((Span<Segment>)_inlineSegments)[..inlineCount].Clear();
        }
#endif

        if (_overflowSegments is not null)
        {
            ArrayPool<Segment>.Shared.Return(_overflowSegments, clearArray: true);
            _overflowSegments = null;
        }

        if (_formatBuffer is not null)
        {
            ArrayPool<char>.Shared.Return(_formatBuffer);
            _formatBuffer = null;
            _formatPosition = 0;
        }

        ReturnSequenceNodes();

        SegmentCount = 0;
        Length = 0;
        OwnedTextHandling = OwnedTextHandling.Copy;
        _cachedSequence = null;
    }

    sealed class Policy : PooledObjectPolicy<LinkedTextUtf16>
    {
        public override LinkedTextUtf16 Create() => new();

        public override bool Return(LinkedTextUtf16 obj)
        {
            obj.Reset();
            return true;
        }
    }
}
