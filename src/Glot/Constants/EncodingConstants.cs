namespace Glot;

/// <summary>
/// Shared bit-mask constants for UTF-8 and UTF-16 encoding operations.
/// </summary>
static class EncodingConstants
{
    public const byte Utf8LeadByteMask = 0b1100_0000;
    public const byte Utf8ContinuationMarker = 0b1000_0000;
    public const sbyte Utf8RuneStartThreshold = unchecked((sbyte)0b1100_0000); // -64 signed
    public const ushort Utf16SurrogateMask = 0b1111_1100_0000_0000;
    public const ushort Utf16LowSurrogateMarker = 0b1101_1100_0000_0000;
    public const int MaxByteLaneBatch = byte.MaxValue;
    public const int MaxUshortLaneBatch = ushort.MaxValue;
}
