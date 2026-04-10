using System.Buffers;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    ReadOnlySequence<byte>? _cachedSequence;
    SequenceSegmentNode? _cachedSequenceHead;

    /// <summary>
    /// Returns a <see cref="ReadOnlySequence{T}"/> over this linked text's segments.
    /// Lazily constructed and cached.
    /// </summary>
    public ReadOnlySequence<byte> AsSequence()
    {
        if (_cachedSequence is { } cached)
        {
            return cached;
        }

        if (_segmentCount == 0)
        {
            return ReadOnlySequence<byte>.Empty;
        }

        if (_segmentCount == 1)
        {
            var seq = new ReadOnlySequence<byte>(GetSegment(0));
            _cachedSequence = seq;
            return seq;
        }

        var head = SequenceSegmentNode.Rent();
        head.SetMemory(GetSegment(0));
        var tail = head;

        for (var i = 1; i < _segmentCount; i++)
        {
            var next = SequenceSegmentNode.Rent();
            next.SetMemory(GetSegment(i));
            tail.SetNext(next);
            tail = next;
        }

        _cachedSequenceHead = head;
        var sequence = new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
        _cachedSequence = sequence;
        return sequence;
    }

    void ReturnSequenceNodes()
    {
        var node = _cachedSequenceHead;
        while (node is not null)
        {
            var next = node.NextNode;
            SequenceSegmentNode.Return(node);
            node = next;
        }

        _cachedSequenceHead = null;
    }

    internal sealed class SequenceSegmentNode : ReadOnlySequenceSegment<byte>
    {
        static SequenceSegmentNode? s_pool;

        internal static SequenceSegmentNode Rent()
        {
            SequenceSegmentNode? node;
            do
            {
                node = Volatile.Read(ref s_pool);
                if (node is null)
                {
                    return new SequenceSegmentNode();
                }
            }
            while (Interlocked.CompareExchange(ref s_pool, node.NextNode, node) != node);

            node.Memory = default;
            node.RunningIndex = 0;
            node.Next = null;
            return node;
        }

        internal static void Return(SequenceSegmentNode node)
        {
            node.Memory = default;
            node.RunningIndex = 0;

            SequenceSegmentNode? current;
            do
            {
                current = Volatile.Read(ref s_pool);
                node.Next = current;
            }
            while (Interlocked.CompareExchange(ref s_pool, node, current) != current);
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
