#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

public sealed partial class LinkedTextUtf8
{
    const int InlineCapacity = 8;

    [InlineArray(InlineCapacity)]
    struct InlineSegmentBuffer
    {
        ReadOnlyMemory<byte> _element0;
    }
}
#else
namespace Glot;

public sealed partial class LinkedTextUtf8
{
    const int InlineCapacity = 0;
}
#endif
