using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    ReadOnlySequence<byte>? _cachedSequence;
    SequenceSegmentNode? _cachedSequenceHead;

    /// <summary>
    /// Returns a <see cref="ReadOnlySequence{T}"/> over this linked text's segments.
    /// Lazily constructed and cached. Thread-safe: if two threads race, the loser
    /// returns its nodes to the pool.
    /// </summary>
    /// <remarks>
    /// Thread-safety: uses <see cref="Thread.MemoryBarrier"/> around reads/writes of
    /// <c>_cachedSequence</c> (a nullable value type, incompatible with <see cref="Volatile"/>
    /// on net6.0) and <see cref="Interlocked.CompareExchange{T}"/> on <c>_cachedSequenceHead</c>
    /// to handle concurrent construction without locks.
    /// </remarks>
    public ReadOnlySequence<byte> AsSequence()
    {
        Thread.MemoryBarrier();
        if (_cachedSequence is { } cached)
        {
            return cached;
        }

        if (SegmentCount == 0)
        {
            return ReadOnlySequence<byte>.Empty;
        }

        if (SegmentCount == 1)
        {
            var seq = new ReadOnlySequence<byte>(GetSegment(0));
            _cachedSequence = seq;
            Thread.MemoryBarrier();
            return seq;
        }

        var head = SequenceSegmentNode.NodePool.Get();
        head.SetMemory(GetSegment(0));
        var tail = head;

        for (var i = 1; i < SegmentCount; i++)
        {
            var next = SequenceSegmentNode.NodePool.Get();
            next.SetMemory(GetSegment(i));
            tail.SetNext(next);
            tail = next;
        }

        var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);

        var existing = Interlocked.CompareExchange(ref _cachedSequenceHead, head, null);
        if (existing is not null)
        {
            var node = head;
            while (node is not null)
            {
                var next = node.NextNode;
                SequenceSegmentNode.NodePool.Return(node);
                node = next;
            }

            var existingTail = existing;
            while (existingTail.NextNode is { } n)
            {
                existingTail = n;
            }

            return new ReadOnlySequence<byte>(existing, 0, existingTail, existingTail.Memory.Length);
        }

        _cachedSequence = sequence;
        Thread.MemoryBarrier();
        return sequence;
    }

    void ReturnSequenceNodes()
    {
        var node = _cachedSequenceHead;
        while (node is not null)
        {
            var next = node.NextNode;
            SequenceSegmentNode.NodePool.Return(node);
            node = next;
        }

        _cachedSequenceHead = null;
    }

    internal sealed class SequenceSegmentNode : ReadOnlySequenceSegment<byte>
    {
        internal static readonly ObjectPool<SequenceSegmentNode> NodePool =
            new DefaultObjectPool<SequenceSegmentNode>(new NodePolicy(), 64);

        sealed class NodePolicy : PooledObjectPolicy<SequenceSegmentNode>
        {
            public override SequenceSegmentNode Create() => new();

            public override bool Return(SequenceSegmentNode obj)
            {
                obj.Memory = default;
                obj.RunningIndex = 0;
                obj.Next = null;
                return true;
            }
        }

        internal void SetMemory(ReadOnlyMemory<byte> memory)
        {
            Memory = memory;
        }

        internal void SetNext(SequenceSegmentNode next)
        {
            next.RunningIndex = RunningIndex + Memory.Length;
            Next = next;
        }

        internal SequenceSegmentNode? NextNode => (SequenceSegmentNode?)Next;
    }
}
