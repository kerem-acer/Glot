namespace Glot;

/// <summary>
/// Specifies the Unicode encoding used to represent text.
/// </summary>
public enum TextEncoding : byte
{
    /// <summary>Variable-width encoding (1–4 bytes per rune). The dominant encoding for the web, JSON, and files.</summary>
    Utf8,

    /// <summary>Variable-width encoding (2 or 4 bytes per rune). The native encoding of .NET <see cref="string"/> and <see cref="char"/>.</summary>
    Utf16,

    /// <summary>Fixed-width encoding (4 bytes per rune). Each element maps 1:1 to a Unicode scalar value.</summary>
    Utf32,
}
