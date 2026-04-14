using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    ReadOnlySequence<char>? _cachedSequence;
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
    public ReadOnlySequence<char> AsSequence()
    {
        Thread.MemoryBarrier();
        if (_cachedSequence is { } cached)
        {
            return cached;
        }

        if (SegmentCount == 0)
        {
            return ReadOnlySequence<char>.Empty;
        }

        if (SegmentCount == 1)
        {
            var seq = new ReadOnlySequence<char>(GetSegment(0));
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

        var sequence = new ReadOnlySequence<char>(head, 0, tail, tail.Memory.Length);

        var existing = Interlocked.CompareExchange(ref _cachedSequenceHead, head, null);
        if (existing is not null)
        {
            // Another thread won — return our nodes to the pool
            var node = head;
            while (node is not null)
            {
                var next = node.NextNode;
                SequenceSegmentNode.NodePool.Return(node);
                node = next;
            }

            // Rebuild from the winner's nodes
            var existingTail = existing;
            while (existingTail.NextNode is { } n)
            {
                existingTail = n;
            }

            return new ReadOnlySequence<char>(existing, 0, existingTail, existingTail.Memory.Length);
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

    internal sealed class SequenceSegmentNode : ReadOnlySequenceSegment<char>
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

        internal void SetMemory(ReadOnlyMemory<char> memory)
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
