namespace Glot;

/// <summary>
/// Estimates the maximum byte count when transcoding between Unicode encodings.
/// </summary>
static class TranscodeSize
{
    /// <summary>
    /// Returns an upper-bound byte count for transcoding <paramref name="sourceBytesLength"/> bytes
    /// from <paramref name="source"/> encoding to <paramref name="target"/> encoding.
    /// </summary>
    internal static int Estimate(int sourceBytesLength, TextEncoding source, TextEncoding target)
    {
        return (source, target) switch
        {
            (TextEncoding.Utf16, TextEncoding.Utf8) => sourceBytesLength / 2 * 3,
            (TextEncoding.Utf8, TextEncoding.Utf16) => sourceBytesLength * 2,
            (TextEncoding.Utf32, TextEncoding.Utf8) => sourceBytesLength,
            (TextEncoding.Utf8, TextEncoding.Utf32) => sourceBytesLength * 4,
            (TextEncoding.Utf32, TextEncoding.Utf16) => sourceBytesLength,
            (TextEncoding.Utf16, TextEncoding.Utf32) => sourceBytesLength * 2,
            _ => sourceBytesLength * 4,
        };
    }
}
