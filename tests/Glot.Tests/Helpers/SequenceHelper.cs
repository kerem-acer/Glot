using System.Buffers;

namespace Glot.Tests;

/// <summary>
/// Builds multi-segment <see cref="ReadOnlySequence{T}"/> values for testing.
/// </summary>
static class SequenceHelper
{
    /// <summary>Creates a multi-segment sequence by splitting the array at <paramref name="splitAt"/>.</summary>
    public static ReadOnlySequence<T> CreateMultiSegment<T>(T[] data, int splitAt)
    {
        var first = new Segment<T>(data.AsMemory(0, splitAt));
        var second = first.Append(data.AsMemory(splitAt));
        return new ReadOnlySequence<T>(first, 0, second, second.Memory.Length);
    }

    sealed class Segment<T> : ReadOnlySequenceSegment<T>
    {
        public Segment(ReadOnlyMemory<T> memory) => Memory = memory;

        public Segment<T> Append(ReadOnlyMemory<T> memory)
        {
            var next = new Segment<T>(memory) { RunningIndex = RunningIndex + Memory.Length };
            Next = next;
            return next;
        }
    }
}
