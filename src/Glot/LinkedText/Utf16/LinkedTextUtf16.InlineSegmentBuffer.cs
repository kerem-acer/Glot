#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    const int InlineCapacity = 8;

    [InlineArray(InlineCapacity)]
    struct InlineSegmentBuffer
    {
        Segment _element0;
    }

    internal struct Segment
    {
        internal ReadOnlyMemory<char> Memory;
        internal char[]? PooledBuffer;
    }
}
#else
namespace Glot;

public sealed partial class LinkedTextUtf16
{
    const int InlineCapacity = 0;

    internal struct Segment
    {
        internal ReadOnlyMemory<char> Memory;
        internal char[]? PooledBuffer;
    }
}
#endif
