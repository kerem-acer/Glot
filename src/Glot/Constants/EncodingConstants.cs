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
    public const ushort Utf16HighSurrogateMarker = 0b1101_1000_0000_0000;
    public const ushort Utf16LowSurrogateMarker = 0b1101_1100_0000_0000;
    public const int MaxByteLaneBatch = byte.MaxValue;
    public const int MaxUshortLaneBatch = ushort.MaxValue;

    // UTF-8 lead-byte prefixes.
    public const byte Utf8TwoByteLead = 0b1100_0000;
    public const byte Utf8ThreeByteLead = 0b1110_0000;
    public const byte Utf8FourByteLead = 0b1111_0000;

    // UTF-8 payload masks (bits available after the lead-byte prefix).
    // Typed as uint to avoid sign-extension warnings (CS0675) when mixed with uint arithmetic.
    public const uint Utf8ContinuationMask = 0b0011_1111;
    public const uint Utf8TwoByteMask = 0b0001_1111;
    public const uint Utf8ThreeByteMask = 0b0000_1111;
    public const uint Utf8FourByteMask = 0b0000_0111;

    // UTF-8 code-point range thresholds.
    public const uint Utf8TwoByteThreshold = 0x80;
    public const uint Utf8ThreeByteThreshold = 0x800;
    public const uint SupplementaryPlaneStart = 0x10000;

    // UTF-16 surrogate-pair arithmetic.
    public const int Utf16SurrogateBits = 10;
    public const uint Utf16LowTenBitsMask = 0b0011_1111_1111;
}
