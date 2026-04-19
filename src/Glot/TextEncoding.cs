namespace Glot;

/// <summary>
/// Specifies the Unicode encoding used to represent text.
/// </summary>
/// <remarks>
/// <see cref="Text"/> operations accept cross-encoding inputs and transcode on the fly.
/// The encoding determines the internal byte layout and affects byte-level indexing and slicing.
/// </remarks>
public enum TextEncoding : byte
{
    /// <summary>Variable-width encoding using 1–4 bytes per scalar value.</summary>
    Utf8,

    /// <summary>Variable-width encoding using 2 or 4 bytes per scalar value.</summary>
    Utf16,

    /// <summary>Fixed-width encoding using 4 bytes per scalar value.</summary>
    Utf32,
}
