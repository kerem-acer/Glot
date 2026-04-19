#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    const int InlineCapacity = 4;

    [InlineArray(InlineCapacity)]
    struct InlineSegmentBuffer
    {
        Segment _element0;
    }

    internal struct Segment
    {
        internal ReadOnlyMemory<byte> Memory;
        internal byte[]? PooledBuffer;
    }
}
#else
namespace Glot;

public sealed partial class LinkedTextUtf8
{
    const int InlineCapacity = 0;

    internal struct Segment
    {
        internal ReadOnlyMemory<byte> Memory;
        internal byte[]? PooledBuffer;
    }
}
#endif
