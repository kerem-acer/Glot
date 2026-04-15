namespace Glot;

/// <summary>
/// A stack-only, zero-allocation view over encoded text bytes.
/// Stores <see cref="ReadOnlySpan{T}"/> of bytes internally — <c>char[]</c> and <c>string</c>
/// sources are normalized via <see cref="System.Runtime.InteropServices.MemoryMarshal.AsBytes{T}(ReadOnlySpan{T})"/>
/// at zero cost. Cannot escape the stack.
/// </summary>
public readonly ref partial struct TextSpan
#if NET6_0_OR_GREATER
    : ISpanFormattable
#endif
#if NET8_0_OR_GREATER
    , IUtf8SpanFormattable
#endif
#if NET9_0_OR_GREATER
    , IEquatable<TextSpan>
    , IComparable<TextSpan>
#endif
{
    readonly EncodedLength _encodedLength;

    /// <summary>
    /// Creates a <see cref="TextSpan"/> over the given bytes with the specified encoding.
    /// Counts runes during construction (O(n), SIMD-accelerated).
    /// </summary>
    public TextSpan(ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        Bytes = bytes;
        _encodedLength = new EncodedLength(encoding, RuneCount.Count(bytes, encoding));
    }

    internal TextSpan(ReadOnlySpan<byte> bytes, TextEncoding encoding, int runeLength)
    {
        Bytes = bytes;
        _encodedLength = new EncodedLength(encoding, runeLength);
    }

    /// <summary>
    /// Creates a <see cref="TextSpan"/> without counting runes.
    /// The resulting span has <see cref="RuneLength"/> == 0.
    /// Use only when the span is an intermediate value whose <see cref="RuneLength"/> is never read.
    /// </summary>
    static TextSpan Uncounted(ReadOnlySpan<byte> bytes, TextEncoding encoding)
        => new(bytes, encoding, 0);

    /// <summary>The raw underlying bytes, regardless of encoding.</summary>
    public ReadOnlySpan<byte> Bytes { get; }

    /// <summary>The number of bytes in this span.</summary>
    public int ByteLength => Bytes.Length;

    /// <summary>The Unicode encoding of the text in this span.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>Returns <c>true</c> if this span contains no bytes.</summary>
    public bool IsEmpty => Bytes.IsEmpty;

    /// <summary>The number of Unicode runes (scalar values) in this span. O(1) when cached; SIMD O(n) when not.</summary>
    public int RuneLength
    {
        get
        {
            var cached = _encodedLength.RuneLength;
            return cached != 0 || Bytes.IsEmpty ? cached : RuneCount.Count(Bytes, Encoding);
        }
    }
}
