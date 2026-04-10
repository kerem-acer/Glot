#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Creates a UTF-8 <see cref="Text"/> from an interpolated string. No intermediate string allocation.</summary>
    public static Text Format(TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a <see cref="Text"/> in the specified encoding from an interpolated string.</summary>
    public static Text Format(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")] TextInterpolatedStringHandler handler)
        => handler.ToText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> from an interpolated string. Caller must dispose.</summary>
    public static OwnedText FormatPooled(TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();

    /// <summary>Creates a pooled <see cref="OwnedText"/> in the specified encoding from an interpolated string.</summary>
    public static OwnedText FormatPooled(
        TextEncoding encoding,
        [InterpolatedStringHandlerArgument("encoding")] TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();
}
#endif
